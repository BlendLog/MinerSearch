using DBase;
using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.Scanners;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MSearch.Core
{
    /// <summary>
    /// Scan stages. Used to display progress in the GUI.
    /// </summary>
    public enum ScanStage
    {
        Idle = 0,
        Rootkit,
        Processes,
        General,
        Signatures,
        Review,
        Cleanup,
        Completed
    }

    public sealed class ScanStageEventArgs : EventArgs
    {
        public ScanStage Stage { get; private set; }

        /// <summary>Scanner class name for the sub-stage label (for example, RegistryScanner).</summary>
        public string ScannerId { get; private set; }

        public ScanStageEventArgs(ScanStage stage, string scannerId = null)
        {
            Stage = stage;
            ScannerId = scannerId;
        }
    }

    public sealed class ScanProgressEventArgs : EventArgs
    {
        public ScanStage Stage { get; private set; }
        public string CurrentItem { get; private set; }
        public int Done { get; private set; }
        public int Total { get; private set; }

        public ScanProgressEventArgs(ScanStage stage, string currentItem, int done, int total)
        {
            Stage = stage;
            CurrentItem = currentItem;
            Done = done;
            Total = total;
        }
    }

    public sealed class ScanCountersEventArgs : EventArgs
    {
        public int Found { get; private set; }
        public int Neutralized { get; private set; }
        public int Suspicious { get; private set; }
        public int Skipped { get; private set; }

        public ScanCountersEventArgs(int found, int neutralized, int suspicious, int skipped)
        {
            Found = found;
            Neutralized = neutralized;
            Suspicious = suspicious;
            Skipped = skipped;
        }
    }

    public sealed class ScanCompletedEventArgs : EventArgs
    {
        public ScanWorkflowResult Result { get; private set; }

        public ScanCompletedEventArgs(ScanWorkflowResult result)
        {
            Result = result;
        }
    }

    public sealed class ScanWorkflowResult
    {
        public ScanStatistics Statistics { get; set; }
        public IList<ScanResult> Results { get; set; }
        public bool Canceled { get; set; }
    }

    /// <summary>
    /// User interaction during a scan.
    /// The engine does not know whether it is a console or the GUI.
    /// </summary>
    public interface IScanInteraction
    {
        /// <summary>Ask whether unknown threats should be accepted automatically.</summary>
        bool ConfirmAutoAcceptUnknown();

        /// <summary>Show the unknown-threat review and apply the user's choice.</summary>
        void ShowThreatReview(List<ThreatDecision> decisions);

        /// <summary>Pause before cleanup (--pause).</summary>
        void WaitBeforeCleanup();
    }

    internal sealed class NoopScanInteraction : IScanInteraction
    {
        public bool ConfirmAutoAcceptUnknown() { return false; }
        public void ShowThreatReview(List<ThreatDecision> decisions) { }
        public void WaitBeforeCleanup() { }
    }

    /// <summary>
    /// Scan engine: rootkits → processes → everything else → signatures → cleanup.
    /// UI-agnostic; progress is reported through events.
    /// </summary>
    public sealed class ScanWorkflow
    {
        private readonly ScanInfrastructure _infrastructure;
        private readonly ScanRequest _request;
        private readonly IScanInteraction _interaction;
        private readonly LaunchOptions _options;
        private readonly MinerSearchScanState _state;
        private ScanStage _currentStage;

        private readonly object _detectedLock = new object();
        private readonly HashSet<string> _detectedFoundKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _detectedSuspiciousKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public event EventHandler<ScanStageEventArgs> StageChanged;
        public event EventHandler<ScanProgressEventArgs> ItemProgress;
        public event EventHandler<ScanCountersEventArgs> CountersChanged;
        public event EventHandler<ScanCompletedEventArgs> Completed;

        public ScanWorkflow(ScanInfrastructure infrastructure, ScanRequest request, IScanInteraction interaction)
        {
            if (infrastructure == null)
                throw new ArgumentNullException("infrastructure");

            _infrastructure = infrastructure;
            _request = request;
            _interaction = interaction ?? new NoopScanInteraction();
            _options = LaunchOptions.GetInstance;
            _state = infrastructure.State;
        }

        /// <summary>
        /// Runs the full scan and cleanup cycle.
        /// </summary>
        public ScanWorkflowResult Run(CancellationToken token)
        {
            // Живой лог объекта нужен только GUI-запускам (request != null).
            // В консольных режимах не подписываемся, чтобы не собирать строки зря.
            bool reportActivity = _request != null;
            if (reportActivity)
                ScanActivity.ObjectChanged += OnScanActivity;

            _infrastructure.ScanManager.ScannerStarted += OnScannerStarted;
            _infrastructure.ThreatManager.DecisionDetected += OnDecisionDetected;

            // Лог живёт ровно один скан: новый файл на каждый запуск.
            Logger.BeginScanLog();

            try
            {
                return RunCore(token);
            }
            finally
            {
                _infrastructure.ScanManager.ScannerStarted -= OnScannerStarted;
                _infrastructure.ThreatManager.DecisionDetected -= OnDecisionDetected;

                if (reportActivity)
                    ScanActivity.ObjectChanged -= OnScanActivity;

                Logger.EndScanLog();
            }
        }

        private void OnScanActivity(string value)
        {
            RaiseProgress(value, 0, 0);
        }

        /// <summary>
        /// Counts found objects as soon as analyzers produce them, so the GUI counters
        /// grow during scanning instead of only at cleanup.
        /// </summary>
        private void OnDecisionDetected(ThreatDecision decision)
        {
            if (decision == null || decision.Target == null)
                return;

            // Заблокированные каталоги из obfStr5 — служебные находки, в результаты они не попадают.
            DirectoryThreatObject dir = decision.Target as DirectoryThreatObject;
            if (dir != null && string.Equals(dir.SourceTag, "locked", StringComparison.Ordinal))
                return;

            if (!IsCountedType(decision.ObjectType))
                return;

            bool changed = false;
            string key = GetDedupKey(decision);

            lock (_detectedLock)
            {
                if (_detectedFoundKeys.Add(key))
                {
                    _state.IncrementFoundThreats();
                    changed = true;
                }

                if (decision.ObjectType == ScanObjectType.Suspicious && _detectedSuspiciousKeys.Add(key))
                {
                    _state.IncrementFoundSuspicious();
                    changed = true;
                }
            }

            if (!changed)
                return;

            RaiseCounters();
        }

        /// <summary>
        /// Types included in the Found/Neutralized/Skipped statistics.
        /// </summary>
        private static bool IsCountedType(ScanObjectType type)
        {
            return type == ScanObjectType.Malware ||
                   type == ScanObjectType.Unsafe ||
                   type == ScanObjectType.Infected ||
                   type == ScanObjectType.Rootkit ||
                   type == ScanObjectType.Suspicious;
        }

        private ScanWorkflowResult RunCore(CancellationToken token)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            ScanWorkflowResult result = new ScanWorkflowResult();
            List<ThreatDecision> processDecisions = null;

            if (_request != null)
            {
                ResetSessionState();
                _request.Apply(_options);
            }

            try
            {
                return RunScan(result, stopwatch, token, ref processDecisions);
            }
            catch (OperationCanceledException)
            {
                return Cancel(result, stopwatch, processDecisions);
            }
        }

        private ScanWorkflowResult RunScan(ScanWorkflowResult result, Stopwatch stopwatch, CancellationToken token, ref List<ThreatDecision> processDecisions)
        {
            RaiseStage(ScanStage.Rootkit);
            List<ThreatDecision> rootkitDecisions = Analyze(_infrastructure.ScanManager.ScanRootkitFirst(token), token);
            token.ThrowIfCancellationRequested();
            _infrastructure.CleanManager.ApplyDecisions(rootkitDecisions, CleanupPhase.Finalize, token);
            RaiseCounters();

            token.ThrowIfCancellationRequested();

            RaiseStage(ScanStage.Processes);
            processDecisions = Analyze(_infrastructure.ScanManager.ScanProcessesOnly(token), token);
            token.ThrowIfCancellationRequested();
            _infrastructure.CleanManager.ApplyDecisions(processDecisions, CleanupPhase.SuspendOnly, token);
            RaiseCounters();

            token.ThrowIfCancellationRequested();

            RaiseStage(ScanStage.General);
            List<ThreatDecision> commonDecisions = Analyze(_infrastructure.ScanManager.ScanEverythingElse(token), token);
            token.ThrowIfCancellationRequested();
            _infrastructure.CleanManager.ApplyDecisions(commonDecisions, CleanupPhase.DisableExecuteOnly, token);
            RaiseCounters();

            token.ThrowIfCancellationRequested();

            if (_options.pause)
                _interaction.WaitBeforeCleanup();

            List<ThreatDecision> allFinalizeDecisions = new List<ThreatDecision>();
            allFinalizeDecisions.AddRange(processDecisions);
            allFinalizeDecisions.AddRange(commonDecisions);

            List<ThreatDecision> signatureDecisions = new List<ThreatDecision>();

            if (!_options.nosignaturescan)
            {
                RaiseStage(ScanStage.Signatures);

                SignatureScanner signatureScanner = new SignatureScanner();
                signatureScanner.CollectFilesAsync(token).GetAwaiter().GetResult();

                token.ThrowIfCancellationRequested();

                List<FileThreatObject> allSignatureFiles = signatureScanner.GetFiles().ToList();
                signatureDecisions = _infrastructure.SignatureAnalyzer.AnalyzeFiles(allSignatureFiles, OnDecisionDetected, token)
                    .Where(d => d != null)
                    .ToList();
                allFinalizeDecisions.AddRange(signatureDecisions);
                RaiseCounters();

                token.ThrowIfCancellationRequested();
            }

            allFinalizeDecisions = DeduplicateDecisions(allFinalizeDecisions);

            // Один знаменатель для всей обработки: после dedup список решений фиксирован.
            int processingTotal = allFinalizeDecisions.Count;
            int processedDecisions = 0;

            // Заблокированные Defender-ом объекты не показываем в review,
            // но отражаем в результатах. Обрабатываем их отдельным батчем ровно один раз.
            List<ThreatDecision> lockedByAvDecisions = allFinalizeDecisions.Where(d => IsLockedByAv(d)).ToList();
            List<ThreatDecision> knownDecisions = allFinalizeDecisions.Where(d => !IsLockedByAv(d) && IsKnownThreat(d)).ToList();
            List<ThreatDecision> unknownDecisions = allFinalizeDecisions.Where(d => !IsLockedByAv(d) && !IsKnownThreat(d)).ToList();

            // Все неизвестные угрозы из всех типов сканов → обзор.
            // Rootkit (Other) всегда обрабатывается автоматически — исключаем из review.
            List<ThreatDecision> allUnknownDecisions = unknownDecisions
                .Where(d => d.Target.Kind != ThreatObjectKind.Other)
                .ToList();

            token.ThrowIfCancellationRequested();

            _infrastructure.CleanManager.BeginFinalCleanup();
            RaiseStage(ScanStage.Cleanup);

            if (lockedByAvDecisions.Count > 0)
            {
                _infrastructure.CleanManager.ApplyDecisions(lockedByAvDecisions, CleanupPhase.Finalize, token);
                processedDecisions += lockedByAvDecisions.Count;
                RaiseProgress(null, processedDecisions, processingTotal);
            }

            List<ThreatDecision> sortedKnownDecisions = knownDecisions
                .OrderBy(d => CleanManager.GetCleanupOrder(d.Target.Kind))
                .ToList();

            foreach (ThreatDecision decision in sortedKnownDecisions)
            {
                token.ThrowIfCancellationRequested();

                _infrastructure.CleanManager.ApplyDecisions(new[] { decision }, CleanupPhase.Finalize, token);
                RaiseCounters();

                processedDecisions++;
                RaiseProgress(decision.Target != null ? decision.Target.Id : null, processedDecisions, processingTotal);
            }

            if (!_options.ScanOnly && !_options.no_review_interact && allUnknownDecisions.Count > 0)
            {
                int autoAcceptValue = 2;
                try
                {
                    using (RegistryKey baseKey = Registry.LocalMachine)
                    using (RegistryKey key = baseKey.OpenSubKey(AppConfig.GetInstance.RegistryPathMain))
                    {
                        if (key != null)
                        {
                            object value = key.GetValue(AppConfig.GetInstance.AutoAcceptUnknownValueName);
                            if (value != null)
                                autoAcceptValue = (int)value;
                        }
                    }
                }
                catch { }

                if (autoAcceptValue == 1)
                {
                    _options.no_review_interact = true;
                }
                else if (autoAcceptValue == 0)
                {
                    _options.no_review_interact = false;
                }
                else
                {
                    bool accepted = _interaction.ConfirmAutoAcceptUnknown();
                    if (accepted)
                        _options.no_review_interact = true;

                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(AppConfig.GetInstance.RegistryPathMain, true)
                                ?? Registry.LocalMachine.CreateSubKey(AppConfig.GetInstance.RegistryPathMain))
                    {
                        if (key != null)
                        {
                            key.SetValue(AppConfig.GetInstance.AutoAcceptUnknownValueName,
                                accepted ? 1 : 0, RegistryValueKind.DWord);
                            key.Close();
                        }
                    }
                }

                if (!_options.no_review_interact)
                {
                    token.ThrowIfCancellationRequested();

                    RaiseStage(ScanStage.Review);
                    _interaction.ShowThreatReview(allUnknownDecisions);

                    token.ThrowIfCancellationRequested();

                    // После обзора возвращаем лейбл и блоки-бар очистки.
                    RaiseStage(ScanStage.Cleanup);
                }
            }

            // Решения со Skip не применяем через Handler — записываем как пропущенные напрямую.
            List<ThreatDecision> decisionsToApply = new List<ThreatDecision>();
            List<ThreatDecision> decisionsToSkip = new List<ThreatDecision>();

            foreach (ThreatDecision decision in allUnknownDecisions)
            {
                if (decision.UserOverrideAction == ScanActionTypeUserSelected.Skip)
                    decisionsToSkip.Add(decision);
                else
                    decisionsToApply.Add(decision);
            }

            if (decisionsToApply.Count > 0)
            {
                // Пакет из review применяем по одному в порядке очистки:
                // это даёт прогресс в UI и возможность остановки между решениями,
                // сохраняя требование "службы раньше файлов".
                List<ThreatDecision> sortedToApply = decisionsToApply
                    .OrderBy(d => d.Target != null ? CleanManager.GetCleanupOrder(d.Target.Kind) : int.MaxValue)
                    .ToList();

                foreach (ThreatDecision decision in sortedToApply)
                {
                    token.ThrowIfCancellationRequested();

                    _infrastructure.CleanManager.ApplyDecisions(new[] { decision }, CleanupPhase.Finalize, token);
                    RaiseCounters();

                    processedDecisions++;
                    RaiseProgress(decision.Target != null ? decision.Target.Id : null, processedDecisions, processingTotal);
                }
            }

            if (decisionsToSkip.Count > 0)
            {
                foreach (ThreatDecision decision in decisionsToSkip)
                {
                    token.ThrowIfCancellationRequested();

                    _infrastructure.CleanManager.ApplySkippedDecisions(new[] { decision }, token);
                    RaiseCounters();

                    processedDecisions++;
                    RaiseProgress(decision.Target != null ? decision.Target.Id : null, processedDecisions, processingTotal);
                }
            }

            // Ссылочное множество уже обработанных Finalize-решений.
            // Ключ по Target.Id здесь ненадёжен: у сигнатурных файлов Id пустой,
            // а разные объекты могут иметь совпадающий Id.
            HashSet<ThreatDecision> alreadyApplied = new HashSet<ThreatDecision>(
                knownDecisions
                    .Concat(decisionsToApply)
                    .Concat(decisionsToSkip)
                    .Concat(lockedByAvDecisions));

            // Применяем только оставшиеся Finalize-решения (те, что не попали
            // ни в knownDecisions, ни в unknownDecisions)
            List<ThreatDecision> finalizeOnlyDecisions = allFinalizeDecisions
                .Where(d => d != null && !alreadyApplied.Contains(d))
                .ToList();

            if (finalizeOnlyDecisions.Count > 0)
            {
                List<ThreatDecision> sortedRemaining = finalizeOnlyDecisions
                    .OrderBy(d => d.Target != null ? CleanManager.GetCleanupOrder(d.Target.Kind) : int.MaxValue)
                    .ToList();

                foreach (ThreatDecision decision in sortedRemaining)
                {
                    token.ThrowIfCancellationRequested();

                    _infrastructure.CleanManager.ApplyDecisions(new[] { decision }, CleanupPhase.Finalize, token);
                    RaiseCounters();

                    processedDecisions++;
                    RaiseProgress(decision.Target != null ? decision.Target.Id : null, processedDecisions, processingTotal);
                }
            }

            return Finish(result, stopwatch);
        }

        private ScanWorkflowResult Cancel(ScanWorkflowResult result, Stopwatch stopwatch, List<ThreatDecision> suspendedProcesses)
        {
            result.Canceled = true;

            // Пользователь остановил скан: уже приостановленные процессы завершаем,
            // обзор и оставшуюся очистку не запускаем.
            if (suspendedProcesses != null && suspendedProcesses.Count > 0)
            {
                try
                {
                    _infrastructure.CleanManager.ApplyDecisions(suspendedProcesses, CleanupPhase.Finalize, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                }
            }

            AppConfig.GetInstance.LL.LogJustDisplayMessage("\t\t", "_ScanCanceled", "\n", ConsoleColor.Yellow);

            return Finish(result, stopwatch);
        }

        private ScanWorkflowResult Finish(ScanWorkflowResult result, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            result.Statistics = ComputeAndLogStatistics(_state, stopwatch.Elapsed);
            result.Results = _state.GetScanResults();

            SyncCountersWithStatistics(result.Statistics);

            RaiseStage(ScanStage.Completed);

            EventHandler<ScanCompletedEventArgs> handler = Completed;
            if (handler != null)
                handler(this, new ScanCompletedEventArgs(result));

            return result;
        }

        /// <summary>
        /// Settles the live GUI counters on the statistics values so the scan page and the results page agree.
        /// </summary>
        private void SyncCountersWithStatistics(ScanStatistics statistics)
        {
            if (_request == null || statistics == null)
                return;

            AppConfig config = AppConfig.GetInstance;
            config.totalFoundThreats = statistics.Found;
            config.totalFoundSuspiciousObjects = statistics.Suspicious;
            config.totalNeutralizedThreats = statistics.Neutralized;
            config.userSkippedThreats = statistics.Skipped;

            RaiseCounters();
        }

        private List<ThreatDecision> Analyze(IEnumerable<IThreatObject> threats, CancellationToken token)
        {
            return _infrastructure.ThreatManager.Analyze(threats, token)
                .Where(d => d != null)
                .ToList();
        }

        private void ResetSessionState()
        {
            AppConfig config = AppConfig.GetInstance;
            config.totalFoundThreats = 0;
            config.totalFoundSuspiciousObjects = 0;
            config.totalNeutralizedThreats = 0;
            config.userSkippedThreats = 0;

            List<ScanResult> results = _state.GetScanResults();
            if (results != null)
                results.Clear();

            lock (_detectedLock)
            {
                _detectedFoundKeys.Clear();
                _detectedSuspiciousKeys.Clear();
            }

            CleanManager.ResetSessionState();
        }

        private void RaiseStage(ScanStage stage, string scannerId = null)
        {
            _currentStage = stage;

            EventHandler<ScanStageEventArgs> handler = StageChanged;
            if (handler != null)
                handler(this, new ScanStageEventArgs(stage, scannerId));
        }

        private void OnScannerStarted(IThreatScanner scanner)
        {
            if (scanner == null)
                return;

            RaiseStage(ScanStage.General, scanner.GetType().Name);
        }

        private void RaiseCounters()
        {
            EventHandler<ScanCountersEventArgs> handler = CountersChanged;
            if (handler == null)
                return;

            AppConfig config = AppConfig.GetInstance;
            handler(this, new ScanCountersEventArgs(
                config.totalFoundThreats,
                config.totalNeutralizedThreats,
                config.totalFoundSuspiciousObjects,
                config.userSkippedThreats));
        }

        private void RaiseProgress(string item, int done, int total)
        {
            EventHandler<ScanProgressEventArgs> handler = ItemProgress;
            if (handler == null)
                return;

            handler(this, new ScanProgressEventArgs(_currentStage, item, done, total));
        }

        /// <summary>
        /// Checks whether the threat is "known" per MSData (obfStr1-obfStr4).
        /// Known threats are applied automatically without review.
        /// </summary>
        public static bool IsKnownThreat(ThreatDecision decision)
        {
            if (decision?.Target == null) return false;
            IThreatObject target = decision.Target;

            // Файлы и каталоги — проверяем SourceTag
            FileThreatObject file = target as FileThreatObject;
            if (file != null && !string.IsNullOrEmpty(file.SourceTag))
            {
                if (file.SourceTag.StartsWith("obfStr", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            DirectoryThreatObject dir = target as DirectoryThreatObject;
            if (dir != null && !string.IsNullOrEmpty(dir.SourceTag))
            {
                if (dir.SourceTag.StartsWith("obfStr", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Файл может быть найден SignatureScanner с UNC-путём (\\?\C:\...),
            // а в MSData.obfStr2 тот же путь без UNC. Нормализуем и сверяем.
            if (file != null && !string.IsNullOrEmpty(file.FilePath))
            {
                string normalizedFilePath = file.FilePath.StartsWith(@"\\?\") ? file.FilePath.Substring(4) : file.FilePath;
                if (MSData.GetInstance.obfStr2.Any(s =>
                {
                    string normalized = s.StartsWith(@"\\?\") ? s.Substring(4) : s;
                    return normalized.Equals(normalizedFilePath, StringComparison.OrdinalIgnoreCase);
                }))
                {
                    return true;
                }
            }

            // Каталог может быть найден с UNC-путём — аналогичная нормализация
            if (dir != null && !string.IsNullOrEmpty(dir.DirectoryPath))
            {
                string normalizedDirPath = dir.DirectoryPath.StartsWith(@"\\?\") ? dir.DirectoryPath.Substring(4) : dir.DirectoryPath;
                if (MSData.GetInstance.obfStr1.Any(s =>
                {
                    string normalized = s.StartsWith(@"\\?\") ? s.Substring(4) : s;
                    return normalized.Equals(normalizedDirPath, StringComparison.OrdinalIgnoreCase);
                }))
                {
                    return true;
                }
            }

            // Процессы — проверяем путь связанного файла (obfStr2 + известные каталоги obfStr1)
            ProcessThreatObject proc = target as ProcessThreatObject;
            if (proc?.FileProcess != null)
            {
                string filePath = proc.FileProcess.FilePath;
                if (!string.IsNullOrEmpty(filePath) &&
                    MSData.GetInstance.IsKnownMaliciousPath(filePath))
                {
                    return true;
                }
            }

            // Службы — проверяем путь бинарника (obfStr2 + известные каталоги obfStr1)
            ServiceThreatObject svc = target as ServiceThreatObject;
            if (svc != null)
            {
                if (!string.IsNullOrEmpty(svc.ServicePath) &&
                    MSData.GetInstance.IsKnownMaliciousPath(svc.ServicePath))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(svc.ServicePathWithArgs) &&
                    MSData.GetInstance.ContainsKnownMaliciousDir(svc.ServicePathWithArgs))
                {
                    return true;
                }
            }

            // Задачи — проверяем связанные файлы (payload из аргументов/исполняемый файл)
            // и сами аргументы задачи по obfStr1/obfStr2
            TaskThreatObject task = target as TaskThreatObject;
            if (task != null)
            {
                // Пустая задача (executable отсутствует) удаляется сразу при RemoveEmptyTasks,
                // а не показывается в review как неизвестная угроза.
                if (string.Equals(task.DetectionReasonRes, "_EmptyTask", StringComparison.Ordinal))
                    return true;

                if (task.LinkedFileFromArgs != null &&
                    !string.IsNullOrEmpty(task.LinkedFileFromArgs.FilePath) &&
                    MSData.GetInstance.IsKnownMaliciousPath(task.LinkedFileFromArgs.FilePath))
                {
                    return true;
                }

                if (task.LinkedFile != null &&
                    !string.IsNullOrEmpty(task.LinkedFile.FilePath) &&
                    MSData.GetInstance.IsKnownMaliciousPath(task.LinkedFile.FilePath))
                {
                    return true;
                }

                if (task.Info != null && task.Info.ExecActions != null)
                {
                    foreach (var action in task.Info.ExecActions)
                    {
                        if (action == null) continue;

                        if (!string.IsNullOrEmpty(action.Path) &&
                            (MSData.GetInstance.ContainsKnownMaliciousDir(action.Path) ||
                             MSData.GetInstance.IsUnderKnownMaliciousDir(action.Path)))
                        {
                            return true;
                        }

                        if (!string.IsNullOrEmpty(action.Arguments) &&
                            MSData.GetInstance.ContainsKnownMaliciousDir(action.Arguments))
                        {
                            return true;
                        }
                    }
                }
            }

            // Реестр — проверяем ValueName по obfStr3/obfStr4 (аналогично RegistryThreatAnalyzer)
            RegistryThreatObject reg = target as RegistryThreatObject;
            if (reg != null)
            {
                if (!string.IsNullOrEmpty(reg.ValueName))
                {
                    if (MSData.GetInstance.obfStr3.Any(s => reg.ValueName.Equals(s, StringComparison.OrdinalIgnoreCase)) ||
                        MSData.GetInstance.obfStr4.Any(s => reg.ValueName.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        return true;
                    }
                }
            }

            // Для остальных типов — не считаем известными по умолчанию
            return false;
        }

        /// <summary>
        /// Removes duplicate decisions: SignatureScanner may find the same file that is already
        /// in processDecisions/commonDecisions, but with a UNC prefix (\\?\).
        /// Groups by Target.Id, and for files/directories by normalized path.
        /// </summary>
        public static List<ThreatDecision> DeduplicateDecisions(List<ThreatDecision> decisions)
        {
            List<ThreatDecision> result = new List<ThreatDecision>();
            Dictionary<string, ThreatDecision> byKey = new Dictionary<string, ThreatDecision>(StringComparer.OrdinalIgnoreCase);

            foreach (ThreatDecision d in decisions)
            {
                if (d == null || d.Target == null) continue;

                string key = GetDedupKey(d);
                ThreatDecision existing;
                if (!byKey.TryGetValue(key, out existing))
                {
                    byKey[key] = d;
                    result.Add(d);
                    continue;
                }

                // Дубликат из другой фазы — переносим флаги, не теряем более сильное действие
                ThreatManager.MergeTargetFlags(existing.Target, d.Target);
                if (ThreatManager.ActionPriority(d.ActionType) < ThreatManager.ActionPriority(existing.ActionType))
                    existing.ActionType = d.ActionType;
            }

            return result;
        }

        /// <summary>
        /// The file is locked by Defender: the object is left alone, not shown in review,
        /// but reflected in the results (exactly once).
        /// </summary>
        public static bool IsLockedByAv(ThreatDecision decision)
        {
            if (decision == null || decision.Target == null)
                return false;

            if (decision.ActionType == ScanActionType.LockedByAntivirus)
                return true;

            FileThreatObject file = decision.Target as FileThreatObject;
            return file != null && file.IsLockedByAntivirus;
        }

        private static string GetDedupKey(ThreatDecision d)
        {
            // Для файлов: всегда нормализованный путь (без \\?\ / \??\).
            // Хеш не используется — разные сканеры могут вычислять хеш по-разному или не вычислять вовсе.
            FileThreatObject file = d.Target as FileThreatObject;
            if (file != null && !string.IsNullOrEmpty(file.FilePath))
            {
                return "File:" + MSData.NormalizeKnownPath(file.FilePath);
            }

            DirectoryThreatObject dir = d.Target as DirectoryThreatObject;
            if (dir != null && !string.IsNullOrEmpty(dir.DirectoryPath))
            {
                return "Dir:" + MSData.NormalizeKnownPath(dir.DirectoryPath);
            }

            return d.Target.Kind + ":" + (d.Target.Id ?? "");
        }

        /// <summary>
        /// Key that identifies the result row object for statistics.
        /// </summary>
        private static string GetResultKey(ScanResult result)
        {
            return result.RawClass + "|" + result.Path;
        }

        /// <summary>
        /// Computes and logs scanning results.
        /// </summary>
        public static ScanStatistics ComputeAndLogStatistics(MinerSearchScanState state, TimeSpan elapsed)
        {
            List<ScanResult> scanResults = state.GetScanResults();

            // Ключ — класс объекта и его описание (путь/имя/PID), а не SHA1:
            // одинаковые по содержимому файлы в разных местах — разные объекты,
            // и Distinct не должен их схлопывать.
            List<ScanResult> countedResults = scanResults
                .Where(r => r != null && IsCountedType(r.RawType))
                .ToList();

            int foundCount = countedResults
                .Select(GetResultKey)
                .Distinct()
                .Count();

            // Заблокированные Defender-ом объекты считаются обезвреженными (их блокирует антивирус),
            // поэтому входят в нейтрализованные; отдельный подсчёт — для отдельного текста итога.
            int neutralizedCount = countedResults
                .Where(r => r.RawAction == ScanActionType.Deleted ||
                            r.RawAction == ScanActionType.Terminated ||
                            r.RawAction == ScanActionType.Quarantine ||
                            r.RawAction == ScanActionType.Cured ||
                            r.RawAction == ScanActionType.Disabled ||
                            r.RawAction == ScanActionType.Suspended ||
                            r.RawAction == ScanActionType.RebootPending ||
                            r.RawAction == ScanActionType.LockedByAntivirus)
                .Select(GetResultKey)
                .Distinct()
                .Count();

            int lockedByAvCount = countedResults
                .Where(r => r.RawAction == ScanActionType.LockedByAntivirus)
                .Select(GetResultKey)
                .Distinct()
                .Count();

            int suspiciousCount = countedResults
                .Where(r => r.RawType == ScanObjectType.Suspicious)
                .Select(GetResultKey)
                .Distinct()
                .Count();

            // Считаем намеренно пропущенные — чтобы отличать их от неудавшихся нейтрализаций
            int skippedCount = countedResults
                .Where(r => r.RawAction == ScanActionType.Skipped)
                .Select(GetResultKey)
                .Distinct()
                .Count();

            string elapsedTime = elapsed.Hours.ToString("00") + ":" + elapsed.Minutes.ToString("00") + ":" + elapsed.Seconds.ToString("00") + "." + elapsed.Milliseconds.ToString("000");

            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(foundCount, neutralizedCount, suspiciousCount, skippedCount);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

            return new ScanStatistics
            {
                Found = foundCount,
                Neutralized = neutralizedCount,
                LockedByAv = lockedByAvCount,
                Suspicious = suspiciousCount,
                Skipped = skippedCount,
                ElapsedTime = elapsedTime
            };
        }
    }
}
