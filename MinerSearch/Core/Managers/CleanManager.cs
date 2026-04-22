using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.Managers
{
    public enum CleanupPhase
    {
        SuspendOnly = 0,
        DisableExecuteOnly,
        Finalize
    }

    public sealed class CleanManager
    {
        private readonly Dictionary<ThreatObjectKind, IThreatHandler> _handlers;
        private readonly IScanState _state;

        private static bool _processingHeaderLogged = false;
        private static readonly object _headerLock = new object();

        // Дедупликация файлов — чтобы один файл не обрабатывался несколько раз
        // (один и тот же файл может быть обнаружен ProcessScanner, RegistryScanner, TaskScanner)
        private readonly HashSet<string> _processedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        // Дедупликация задач — чтобы одна задача не записывалась в results дважды
        private readonly HashSet<string> _processedTasks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly object _fileLock = new object();
        private readonly object _taskLock = new object();
        private readonly LaunchOptions _options;

        public CleanManager(IEnumerable<IThreatHandler> handlers, IScanState state)
        {
            _handlers = handlers.ToDictionary(h => h.Kind);
            _state = state;
            _options = LaunchOptions.GetInstance;
        }

        public void BeginFinalCleanup()
        {
            if (!_processingHeaderLogged)
            {
                lock (_headerLock)
                {
                    if (!_processingHeaderLogged)
                    {
                        Logger.WriteLog("    =======================================", Logger.head, false);
                        AppConfig.GetInstance.LL.LogHeadMessage("_ProcessingThreats");
                        Logger.WriteLog("    =======================================", Logger.head, false);
                        _processingHeaderLogged = true;
                    }
                }
            }
        }

        public void ApplyDecisions(IEnumerable<ThreatDecision> decisions, CleanupPhase phase)
        {
            // ScanOnly режим — только записываем угрозы, без выполнения действий
            if (_options.ScanOnly)
            {
                foreach (ThreatDecision decision in decisions)
                {
                    if (decision == null || decision.Target == null) continue;

                    _state.AddScanResult(new ScanResult(decision.ObjectType, GetDescription(decision), ScanActionType.Skipped));

                    if (decision.ObjectType == ScanObjectType.Malware || decision.ObjectType == ScanObjectType.Unsafe || decision.ObjectType == ScanObjectType.Infected)
                        _state.IncrementFoundThreats();
                    else if (decision.ObjectType == ScanObjectType.Suspicious)
                        _state.IncrementFoundSuspicious();
                }
                return;
            }

            // Группируем решения по типу для раздельного логирования этапов
            var groupedByKind = decisions
                .Where(d => d != null && d.Target != null)
                .GroupBy(d => d.Target.Kind)
                .OrderBy(g => (int)g.Key)
                .ToList();

            // Логируем заголовок этапа, если это Finalize и есть несколько типов
            bool logPhaseHeaders = phase == CleanupPhase.Finalize && groupedByKind.Count > 1;

            foreach (var group in groupedByKind)
            {
                // Логируем заголовок этапа (например "Обработка процессов...", "Обработка файлов...")
                if (logPhaseHeaders)
                {
                    string phaseHeaderResource = GetPhaseHeaderResource(group.Key);
                    if (!string.IsNullOrEmpty(phaseHeaderResource))
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage(phaseHeaderResource);
                    }
                }

                foreach (ThreatDecision decision in group)
                {
                    // Дедупликация задач: проверяем ДО вызова handler
                    if (decision.Target.Kind == ThreatObjectKind.ScheduledTask)
                    {
                        var taskTarget = decision.Target as TaskThreatObject;
                        if (taskTarget != null)
                        {
                            lock (_taskLock)
                            {
                                if (!_processedTasks.Add(taskTarget.Info.Path))
                                {
                                    // Задача уже обработана — пропускаем
                                    continue;
                                }
                            }
                        }
                    }

                    IThreatHandler handler;
                    if (!_handlers.TryGetValue(decision.Target.Kind, out handler))
                        continue;

                    ApplyResult result;
                    try
                    {
                        result = handler.Apply(decision, phase);
                    }
                    catch (Exception ex)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_HandlerApplyError", ex, decision.Target.Kind.ToString(), "_Error");
                        result = ApplyResult.Error;
                    }

                    // Дедупликация файлов: помечаем файл как обработанный только если он был реально обработан
                    // (успешно или с ошибкой), а не пропущен из-за неподходящей фазы
                    if (result != ApplyResult.NotApplicable && result != ApplyResult.Skipped)
                    {
                        if (decision.Target.Kind == ThreatObjectKind.File)
                        {
                            var fileTarget = decision.Target as FileThreatObject;
                            if (fileTarget != null)
                            {
                                lock (_fileLock)
                                {
                                    _processedFiles.Add(fileTarget.FilePath);
                                }
                            }
                        }
                    }

                    // SuspendOnly — промежуточная фаза (заморозка процессов перед уничтожением).
                    // Не записываем ScanResult — финальный результат будет записан в Finalize.
                    if (phase == CleanupPhase.SuspendOnly) continue;
                    if (decision.Target.Kind == ThreatObjectKind.Directory)
                    {
                        DirectoryThreatObject lockedDirTarget = decision.Target as DirectoryThreatObject;
                        string dirTag = lockedDirTarget.SourceTag;

                        if (dirTag.Equals("locked") || dirTag.Equals("empty")) continue;
                    }

                    RecordResult(decision, result, phase);
                }
            }
        }

        private string GetPhaseHeaderResource(ThreatObjectKind kind)
        {
            switch (kind)
            {
                case ThreatObjectKind.Process: return "_ProcessingProcesses";
                case ThreatObjectKind.File: return "_ProcessingFiles";
                case ThreatObjectKind.RegistryObject: return "_ProcessingRegistry";
                case ThreatObjectKind.ScheduledTask: return "_ProcessingTasks";
                case ThreatObjectKind.Service: return "_ProcessingServices";
                case ThreatObjectKind.FirewallRule: return "_ProcessingFirewall";
                case ThreatObjectKind.ShellStartupFile: return "_ProcessingShellStartup";
                case ThreatObjectKind.Hosts: return "_ProcessingHosts";
                default: return null;
            }
        }

        private void RecordResult(ThreatDecision decision, ApplyResult result, CleanupPhase phase)
        {
            // Записываем в state только значимые результаты
            if (result == ApplyResult.NotApplicable || result == ApplyResult.Skipped)
                return;

            // Определяем ScanActionType на основе ApplyResult и фазы
            ScanActionType actionType = MapResultToActionType(result, decision, phase);

            // Формируем описание для лога
            string description = GetDescription(decision);

            // Если была ошибка — добавляем сообщение в примечание
            string note = (result == ApplyResult.Error) ? decision.ApplyErrorMessage : null;

            // Записываем в ScanResult
            _state.AddScanResult(new ScanResult(decision.ObjectType, description, actionType, note));

            // Счётчики
            if (result == ApplyResult.Success)
            {
                if (decision.ObjectType == ScanObjectType.Malware || decision.ObjectType == ScanObjectType.Unsafe || decision.ObjectType == ScanObjectType.Infected)
                    _state.IncrementFoundThreats();
                else if (decision.ObjectType == ScanObjectType.Suspicious)
                    _state.IncrementFoundSuspicious();
            }
        }

        private ScanActionType MapResultToActionType(ApplyResult result, ThreatDecision decision, CleanupPhase phase)
        {
            switch (result)
            {
                case ApplyResult.Success:
                    // Finalize — определяем по типу угрозы и флагам
                    return GetActionTypeForSuccess(decision);

                case ApplyResult.Failed:
                    return GetActionTypeForFailed(decision);

                case ApplyResult.Error:
                    return ScanActionType.Error;

                default:
                    return ScanActionType.Skipped;
            }
        }

        private ScanActionType GetActionTypeForSuccess(ThreatDecision decision)
        {
            switch (decision.Target.Kind)
            {
                case ThreatObjectKind.Process:
                    return ScanActionType.Terminated;

                case ThreatObjectKind.File:
                    var file = decision.Target as FileThreatObject;
                    if (file != null)
                    {
                        if (file.ShouldMoveFileToQuarantine) return ScanActionType.Quarantine;
                        if (file.ShouldDeleteFile) return ScanActionType.Deleted;
                    }
                    // Фоллбэк — если флаги не установлены, используем decision.ActionType
                    return decision.ActionType != ScanActionType.Cured
                        ? decision.ActionType
                        : ScanActionType.Deleted;

                case ThreatObjectKind.Directory:
                    return ScanActionType.Deleted;

                case ThreatObjectKind.RegistryObject:
                    var reg = decision.Target as RegistryThreatObject;
                    if (reg != null)
                    {
                        // ActionSetData = исправлено значение → Cured
                        if (reg.ActionSetData || reg.ActionSetSibling) return ScanActionType.Cured;
                    }
                    return ScanActionType.Deleted;

                case ThreatObjectKind.ScheduledTask:
                    return ScanActionType.Deleted;

                case ThreatObjectKind.WmiSubscription:
                    return ScanActionType.Deleted;

                case ThreatObjectKind.FirewallRule:
                    return ScanActionType.Deleted;

                case ThreatObjectKind.UserProfile:
                    return ScanActionType.Deleted;

                case ThreatObjectKind.Service:
                    var svc = decision.Target as ServiceThreatObject;
                    if (svc != null)
                    {
                        if (svc.ShouldDeleteService) return ScanActionType.Deleted;
                        if (svc.ShouldDisableService) return ScanActionType.Disabled;
                    }
                    return ScanActionType.Deleted;

                case ThreatObjectKind.ShellStartupFile:
                    var shellFile = decision.Target as ShellStartupFileThreatObject;
                    if (shellFile != null)
                    {
                        if (shellFile.ShouldMoveToQuarantine) return ScanActionType.Quarantine;
                        if (shellFile.ShouldDeleteFile) return ScanActionType.Deleted;
                    }
                    return ScanActionType.Deleted;

                case ThreatObjectKind.Hosts:
                    var hosts = decision.Target as HostsThreatObject;
                    if (hosts != null)
                    {
                        if (hosts.ShouldQuarantineFile) return ScanActionType.Quarantine;
                        if (hosts.ShouldRemoveInfectedLines) return ScanActionType.Cured;
                    }
                    return ScanActionType.Cured;

                default:
                    return decision.ActionType;
            }
        }

        private ScanActionType GetActionTypeForFailed(ThreatDecision decision)
        {
            switch (decision.Target.Kind)
            {
                case ThreatObjectKind.Process:
                    return ScanActionType.Active;

                case ThreatObjectKind.File:
                case ThreatObjectKind.Directory:
                case ThreatObjectKind.ShellStartupFile:
                case ThreatObjectKind.FirewallRule:
                    // Файл/директория/правило не удалены — вероятно заблокированы
                    return ScanActionType.Inaccessible;

                default:
                    // Registry, Task, Firewall, Service, etc.
                    return ScanActionType.Error;
            }
        }

        private string GetDescription(ThreatDecision decision)
        {
            var target = decision.Target;
            switch (target.Kind)
            {
                case ThreatObjectKind.Process:
                    var proc = target as ProcessThreatObject;
                    return $"{AppConfig.GetInstance.LL.GetLocalizedString("_Just_Process")} {proc?.ProcessName} - PID: {proc?.ProcessId}";

                case ThreatObjectKind.ScheduledTask:
                    var task = target as TaskThreatObject;
                    return $"{AppConfig.GetInstance.LL.GetLocalizedString("_ScheduledTask")} -> {task?.Info.Path}\\{task?.Info.Name}";

                case ThreatObjectKind.RegistryObject:
                    var reg = target as RegistryThreatObject;
                    string hiveShort = reg?.Hive == "HKEY_LOCAL_MACHINE" ? "HKLM" : "HKCU";
                    return $@"{hiveShort}\{reg?.KeyPath}\{reg?.ValueName}";

                case ThreatObjectKind.File:
                    var file = target as FileThreatObject;
                    return file?.FilePath;

                case ThreatObjectKind.ShellStartupFile:
                    var shellFile = target as ShellStartupFileThreatObject;
                    if (shellFile != null && shellFile.IsShortcut)
                    {
                        return $"{AppConfig.GetInstance.LL.GetLocalizedString("_Just_Shortcut")} {shellFile.FileName} --> {shellFile.ShortcutTargetPath}";
                    }
                    return shellFile?.FilePath;

                case ThreatObjectKind.FirewallRule:
                    var fwRule = target as FirewallRuleThreatObject;
                    return fwRule != null ? $"{fwRule.RuleName} → {fwRule.ApplicationName}" : target.Id;

                case ThreatObjectKind.WmiSubscription:
                    var wmi = target as WmiSubscriptionThreatObject;
                    return wmi != null ? $"WMI: {wmi.Name} -> {wmi.CommandLineTemplate}" : target.Id;

                case ThreatObjectKind.Hosts:
                    var hosts = target as HostsThreatObject;
                    return hosts != null ? $"{AppConfig.GetInstance.LL.GetLocalizedString("_HostsFile")}: {hosts.HostsFilePath}" : target.Id;

                default:
                    return target.Id;
            }
        }
    }
}
