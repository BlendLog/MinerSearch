using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Win32Wrapper;

namespace MSearch.Core.ThreatHandlers
{
    internal sealed class FileThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.File;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var fileThreat = decision.Target as FileThreatObject;
            if (fileThreat == null) return ApplyResult.NotApplicable;

            string path = fileThreat.FilePath;

            // Файл заблокирован Defender (ERROR_VIRUS_INFECTED) — не трогаем вообще:
            // ни ACL/атрибуты, ни карантин, ни удаление, ни отложенное удаление, ни Deny-Execute.
            if (fileThreat.IsLockedByAntivirus)
            {
                decision.ActionType = ScanActionType.LockedByAntivirus;
                if (phase == CleanupPhase.Finalize)
                    AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", path);
                return ApplyResult.LockedByAntivirus;
            }

#if DEBUG
            Console.WriteLine($"[DBG FileSystemThreatHandler] Phase={phase}, Path={path}, Delete={fileThreat.ShouldDeleteFile}, Quarantine={fileThreat.ShouldMoveFileToQuarantine}, DisableExec={fileThreat.ShouldDisableExecute}");
#endif

            if (phase == CleanupPhase.DisableExecuteOnly || phase == CleanupPhase.SuspendOnly)
            {
                if (!fileThreat.ShouldDisableExecute) return ApplyResult.Skipped;
                return HandleDisableExecutePhase(path, decision);
            }

            if (phase == CleanupPhase.Finalize)
            {
                if (fileThreat.ShouldMoveFileToQuarantine)
                    return HandleMoveToQuarantine(path, decision);

                if (fileThreat.ShouldDeleteFile)
                    return HandleDeleteFile(path, decision);

                // Deny-Execute уже применён в промежуточной фазе — фиксируем результат
                if (fileThreat.ShouldDisableExecute)
                {
                    decision.ActionType = ScanActionType.Disabled;
                    return ApplyResult.Success;
                }

#if DEBUG
                Console.WriteLine($"[DBG FileSystemThreatHandler] SKIPPED — no flags set");
#endif
                return ApplyResult.Skipped;
            }

            return ApplyResult.NotApplicable;
        }

        /// <summary>
        /// Страховка для файлов, не прошедших контент-анализ: пробуем открыть и ловим ERROR_VIRUS_INFECTED.
        /// При срабатывании помечаем файл и запрещаем любые операции над ним.
        /// </summary>
        private static bool IsDefenderLocked(string path, ThreatDecision decision, bool log = true)
        {
            if (!FileChecker.IsBlockedByDefender(path))
                return false;

            var fileThreat = decision.Target as FileThreatObject;
            if (fileThreat != null)
                fileThreat.IsLockedByAntivirus = true;

            decision.ActionType = ScanActionType.LockedByAntivirus;
            if (log)
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", path);
            return true;
        }

        private ApplyResult HandleMoveToQuarantine(string path, ThreatDecision decision)
        {
            if (!File.Exists(path))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.Gray);
                return ApplyResult.NotApplicable;
            }

            if (IsDefenderLocked(path, decision))
                return ApplyResult.LockedByAntivirus;

            try
            {
                Utils.AddToQuarantine(path);
                if (!File.Exists(path))
                {
                    decision.ActionType = ScanActionType.Quarantine;
                    return ApplyResult.Success;
                }

                UnlockObjectClass.ResetObjectACL(new FileInfo(path).DirectoryName);
                Native.SetFileAttributes(path, FileAttributes.Normal);

                Utils.AddToQuarantine(path);

                if (File.Exists(path))
                {
                    decision.ActionType = ScanActionType.Error;
                    return ApplyResult.Failed;
                }
                else
                {
                    decision.ActionType = ScanActionType.Quarantine;
                    return ApplyResult.Success;
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_ObjectType_File");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleDeleteFile(string path, ThreatDecision decision)
        {
            if (!File.Exists(path))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.Gray);
                return ApplyResult.NotApplicable;
            }

            if (IsDefenderLocked(path, decision))
                return ApplyResult.LockedByAntivirus;

            try
            {
                // Сбросить ACL перед удалением
                UnlockObjectClass.ResetObjectACL(path);

                int lastError = 0;
                if (!NativeFileOperations.DeleteFileWithRetry(path))
                    lastError = Marshal.GetLastWin32Error();

                if (!File.Exists(path))
                {
                    decision.ActionType = ScanActionType.Deleted;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MaliciousFileDeleted", path);
                    return ApplyResult.Success;
                }

                UnlockObjectClass.KillAndDelete(path);
                if (!File.Exists(path))
                {
                    decision.ActionType = ScanActionType.Deleted;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MaliciousFileDeleted", path);
                    return ApplyResult.Success;
                }

                if (CanScheduleDeleteOnReboot() && NativeFileOperations.ScheduleDeleteOnReboot(path))
                {
                    var fileThreat = decision.Target as FileThreatObject;
                    if (fileThreat != null)
                        fileThreat.DeleteScheduledOnReboot = true;

                    decision.ActionType = ScanActionType.RebootPending;
                    string rebootPendingNote = AppConfig.GetInstance.LL.GetLocalizedString("_RebootPendingNote");
                    if (!string.IsNullOrEmpty(rebootPendingNote) &&
                        (string.IsNullOrEmpty(decision.Note) || decision.Note.IndexOf(rebootPendingNote, StringComparison.Ordinal) < 0))
                    {
                        decision.Note = string.IsNullOrEmpty(decision.Note) ? rebootPendingNote : decision.Note + " | " + rebootPendingNote;
                    }
                    AppConfig.GetInstance.LL.LogWarnMessage("_FileScheduledDeleteOnReboot", path);
                    return ApplyResult.Success;
                }

                decision.ApplyErrorMessage = lastError != 0 ? new Win32Exception(lastError).Message : null;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", null, path, "_ObjectType_File");
                return ApplyResult.Failed;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_ObjectType_File");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleDisableExecutePhase(string path, ThreatDecision decision)
        {
            if (IsDefenderLocked(path, decision))
                return ApplyResult.LockedByAntivirus;

            try
            {
                UnlockObjectClass.DisableExecute(path);
                // Промежуточная фаза - не устанавливаем финальное действие
                decision.ActionType = ScanActionType.Skipped;
                return ApplyResult.Success;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_WarnCannotDisableExecution", ex, path, "_ObjectType_File");
                return ApplyResult.Error;
            }
        }

        private static bool CanScheduleDeleteOnReboot()
        {
            return !LaunchOptions.GetInstance.winpemode &&
                   AppConfig.GetInstance.bootMode == BootMode.Normal;
        }
    }
}
