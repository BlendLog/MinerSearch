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
    /// Этапы сканирования. Используются для отображения прогресса в GUI.
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

        /// <summary>Имя класса сканера для уточнения подэтапа (например, RegistryScanner).</summary>
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
    /// Взаимодействие с пользователем во время сканирования.
    /// Движок не знает, консоль это или GUI.
    /// </summary>
    public interface IScanInteraction
    {
        /// <summary>Спросить, принимать ли неизвестные угрозы автоматически.</summary>
        bool ConfirmAutoAcceptUnknown();

        /// <summary>Показать обзор неизвестных угроз и применить выбор пользователя.</summary>
        void ShowThreatReview(List<ThreatDecision> decisions);

        /// <summary>Пауза перед очисткой (--pause).</summary>
        void WaitBeforeCleanup();
    }

    internal sealed class NoopScanInteraction : IScanInteraction
    {
        public bool ConfirmAutoAcceptUnknown() { return false; }
        public void ShowThreatReview(List<ThreatDecision> decisions) { }
        public void WaitBeforeCleanup() { }
    }

    /// <summary>
    /// Движок сканирования: руткиты → процессы → остальное → сигнатуры → очистка.
    /// Не зависит от UI, прогресс отдаётся событиями.
    /// </summary>
    public sealed class ScanWorkflow
    {
        private readonly ScanInfrastructure _infrastructure;
        private readonly ScanRequest _request;
        private readonly IScanInteraction _interaction;
        private readonly LaunchOptions _options;
        private readonly MinerSearchScanState _state;
        private ScanStage _currentStage;

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
        /// Выполняет полный цикл сканирования и очистки.
        /// </summary>
        public ScanWorkflowResult Run(CancellationToken token)
        {
            // Живой лог объекта нужен только GUI-запускам (request != null).
            // В консольных режимах не подписываемся, чтобы не собирать строки зря.
            bool reportActivity = _request != null;
            if (reportActivity)
                ScanActivity.ObjectChanged += OnScanActivity;

            _infrastructure.ScanManager.ScannerStarted += OnScannerStarted;

            try
            {
                return RunCore(token);
            }
            finally
            {
                _infrastructure.ScanManager.ScannerStarted -= OnScannerStarted;

                if (reportActivity)
                    ScanActivity.ObjectChanged -= OnScanActivity;
            }
        }

        private void OnScanActivity(string value)
        {
            RaiseProgress(value, 0, 0);
        }

        private ScanWorkflowResult RunCore(CancellationToken token)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            ScanWorkflowResult result = new ScanWorkflowResult();

            if (_request != null)
            {
                ResetSessionState();
                _request.Apply(_options);
            }

            RaiseStage(ScanStage.Rootkit);
            List<ThreatDecision> rootkitDecisions = Analyze(_infrastructure.ScanManager.ScanRootkitFirst());
            _infrastructure.CleanManager.ApplyDecisions(rootkitDecisions, CleanupPhase.Finalize);
            RaiseCounters();

            if (token.IsCancellationRequested)
                return Cancel(result, stopwatch, null);

            RaiseStage(ScanStage.Processes);
            List<ThreatDecision> processDecisions = Analyze(_infrastructure.ScanManager.ScanProcessesOnly());
            _infrastructure.CleanManager.ApplyDecisions(processDecisions, CleanupPhase.SuspendOnly);
            RaiseCounters();

            if (token.IsCancellationRequested)
                return Cancel(result, stopwatch, processDecisions);

            RaiseStage(ScanStage.General);
            List<ThreatDecision> commonDecisions = Analyze(_infrastructure.ScanManager.ScanEverythingElse());
            _infrastructure.CleanManager.ApplyDecisions(commonDecisions, CleanupPhase.DisableExecuteOnly);
            RaiseCounters();

            if (token.IsCancellationRequested)
                return Cancel(result, stopwatch, processDecisions);

            if (_options.pause)
                _interaction.WaitBeforeCleanup();

            // Собираем все решения для Finalize-фазы
            List<ThreatDecision> allFinalizeDecisions = new List<ThreatDecision>();
            allFinalizeDecisions.AddRange(processDecisions);
            allFinalizeDecisions.AddRange(commonDecisions);

            List<ThreatDecision> signatureDecisions = new List<ThreatDecision>();

            if (!_options.nosignaturescan)
            {
                RaiseStage(ScanStage.Signatures);

                SignatureScanner signatureScanner = new SignatureScanner();
                signatureScanner.CollectFilesAsync().Wait();

                if (token.IsCancellationRequested)
                    return Cancel(result, stopwatch, processDecisions);

                List<FileThreatObject> allSignatureFiles = signatureScanner.GetFiles().ToList();
                signatureDecisions = _infrastructure.SignatureAnalyzer.AnalyzeFiles(allSignatureFiles)
                    .Where(d => d != null)
                    .ToList();
                allFinalizeDecisions.AddRange(signatureDecisions);
                RaiseCounters();

                if (token.IsCancellationRequested)
                    return Cancel(result, stopwatch, processDecisions);
            }

            allFinalizeDecisions = DeduplicateDecisions(allFinalizeDecisions);

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

            if (token.IsCancellationRequested)
                return Cancel(result, stopwatch, processDecisions);

            _infrastructure.CleanManager.BeginFinalCleanup();

            if (lockedByAvDecisions.Count > 0)
                _infrastructure.CleanManager.ApplyDecisions(lockedByAvDecisions, CleanupPhase.Finalize);

            RaiseStage(ScanStage.Cleanup);

            List<ThreatDecision> sortedKnownDecisions = knownDecisions
                .OrderBy(d => CleanManager.GetCleanupOrder(d.Target.Kind))
                .ToList();

            int knownIndex = 0;
            foreach (ThreatDecision decision in sortedKnownDecisions)
            {
                if (token.IsCancellationRequested)
                    return Cancel(result, stopwatch, null);

                _infrastructure.CleanManager.ApplyDecisions(new[] { decision }, CleanupPhase.Finalize);
                RaiseCounters();

                knownIndex++;
                RaiseProgress(decision.Target != null ? decision.Target.Id : null, knownIndex, sortedKnownDecisions.Count);
            }

            if (!_options.no_review_interact && allUnknownDecisions.Count > 0)
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
                    if (token.IsCancellationRequested)
                        return Cancel(result, stopwatch, null);

                    RaiseStage(ScanStage.Review);
                    _interaction.ShowThreatReview(allUnknownDecisions);

                    if (token.IsCancellationRequested)
                        return Cancel(result, stopwatch, null);
                }
            }

            // Применяем все неизвестные угрозы (с учётом UserOverrideAction).
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
                _infrastructure.CleanManager.ApplyDecisions(decisionsToApply, CleanupPhase.Finalize);

            if (decisionsToSkip.Count > 0)
                _infrastructure.CleanManager.ApplySkippedDecisions(decisionsToSkip);

            RaiseCounters();

            // Ссылочное множество уже обработанных Finalize-решений.
            // Ключ по Target.Id здесь ненадёжен: у сигнатурных файлов Id пустой,
            // а разные объекты могут иметь совпадающий Id.
            HashSet<ThreatDecision> alreadyApplied = new HashSet<ThreatDecision>(
                knownDecisions
                    .Concat(decisionsToApply)
                    .Concat(decisionsToSkip)
                    .Concat(lockedByAvDecisions));

            // Применяем только оставшиеся Finalize-решения (например signatureDecisions,
            // которые не попали ни в knownDecisions, ни в unknownDecisions)
            List<ThreatDecision> finalizeOnlyDecisions = allFinalizeDecisions
                .Where(d => d != null && !alreadyApplied.Contains(d))
                .ToList();

            if (finalizeOnlyDecisions.Count > 0)
            {
                if (token.IsCancellationRequested)
                    return Cancel(result, stopwatch, null);

                _infrastructure.CleanManager.ApplyDecisions(finalizeOnlyDecisions, CleanupPhase.Finalize);
                RaiseCounters();
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
                    _infrastructure.CleanManager.ApplyDecisions(suspendedProcesses, CleanupPhase.Finalize);
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

            RaiseStage(ScanStage.Completed);

            EventHandler<ScanCompletedEventArgs> handler = Completed;
            if (handler != null)
                handler(this, new ScanCompletedEventArgs(result));

            return result;
        }

        private List<ThreatDecision> Analyze(IEnumerable<IThreatObject> threats)
        {
            return _infrastructure.ThreatManager.Analyze(threats)
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
            config.hasLockedObjectsByAV = false;
            config.hasEmptyTasks = false;

            List<ScanResult> results = _state.GetScanResults();
            if (results != null)
                results.Clear();

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
        /// Проверяет, является ли угроза «известной» по MSData (obfStr1-obfStr4).
        /// Известные угрозы применяются автоматически без review.
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
        /// Удаляет дубликаты решений: SignatureScanner может найти тот же файл, что уже есть
        /// в processDecisions/commonDecisions, но с UNC-префиксом (\\?\).
        /// Группируем по Target.Id, а для файлов/каталогов — по нормализованному пути.
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
        /// Файл заблокирован Defender-ом: объект не трогаем, в review не показываем,
        /// но отражаем в результатах (ровно один раз).
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

            // Для каталогов: нормализованный путь
            DirectoryThreatObject dir = d.Target as DirectoryThreatObject;
            if (dir != null && !string.IsNullOrEmpty(dir.DirectoryPath))
            {
                return "Dir:" + MSData.NormalizeKnownPath(dir.DirectoryPath);
            }

            // Для всех остальных: Target.Id
            return d.Target.Kind + ":" + (d.Target.Id ?? "");
        }

        /// <summary>
        /// Computes and logs scanning results.
        /// </summary>
        public static ScanStatistics ComputeAndLogStatistics(MinerSearchScanState state, TimeSpan elapsed)
        {
            List<ScanResult> scanResults = state.GetScanResults();

            // Группируем по уникальным Id ThreatObject для корректного подсчёта.
            // Если ThreatObjectId пустой (напр. для файлов с валидной подписью hash="",
            // или bad_bat где hash="") — используем составной ключ RawClass + Path,
            // чтобы Distinct не схлопывал все пустые строки в 1.
            int foundCount = scanResults
                .Where(r => r.RawType == ScanObjectType.Malware ||
                            r.RawType == ScanObjectType.Unsafe ||
                            r.RawType == ScanObjectType.Infected ||
                            r.RawType == ScanObjectType.Rootkit ||
                            r.RawType == ScanObjectType.Suspicious)
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            // Заблокированные Defender-ом объекты считаются обезвреженными (их блокирует антивирус),
            // поэтому входят в нейтрализованные; отдельный подсчёт — для отдельного текста итога.
            int neutralizedCount = scanResults
                .Where(r => r.RawAction == ScanActionType.Deleted ||
                            r.RawAction == ScanActionType.Terminated ||
                            r.RawAction == ScanActionType.Quarantine ||
                            r.RawAction == ScanActionType.Cured ||
                            r.RawAction == ScanActionType.Disabled ||
                            r.RawAction == ScanActionType.Suspended ||
                            r.RawAction == ScanActionType.RebootPending ||
                            r.RawAction == ScanActionType.LockedByAntivirus)
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            int lockedByAvCount = scanResults
                .Where(r => r.RawAction == ScanActionType.LockedByAntivirus)
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            int suspiciousCount = AppConfig.GetInstance.totalFoundSuspiciousObjects;

            // Считаем намеренно пропущенные — чтобы отличать их от неудавшихся нейтрализаций
            int skippedCount = scanResults
                .Where(r => r.RawAction == ScanActionType.Skipped &&
                            (r.RawType == ScanObjectType.Malware ||
                             r.RawType == ScanObjectType.Unsafe ||
                             r.RawType == ScanObjectType.Infected ||
                             r.RawType == ScanObjectType.Rootkit ||
                             r.RawType == ScanObjectType.Suspicious))
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
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
