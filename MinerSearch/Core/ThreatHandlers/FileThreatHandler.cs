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

#if DEBUG
                Console.WriteLine($"[DBG FileSystemThreatHandler] SKIPPED — no flags set");
#endif
                return ApplyResult.Skipped;
            }

            return ApplyResult.NotApplicable;
        }

        private ApplyResult HandleMoveToQuarantine(string path, ThreatDecision decision)
        {
            if (!File.Exists(path))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.Gray);
                return ApplyResult.NotApplicable;
            }

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
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
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

            try
            {
                // Сбросить ACL перед удалением
                UnlockObjectClass.ResetObjectACL(path);
                
                if (NativeFileOperations.DeleteFileWithRetry(path))
                {
                    if (!File.Exists(path))
                    {
                        decision.ActionType = ScanActionType.Deleted;
                        return ApplyResult.Success;
                    }
                    else
                    {
                        decision.ActionType = ScanActionType.Error;
                        return ApplyResult.Failed;
                    }
                }
                else
                {
                    int lastError = Marshal.GetLastWin32Error();
                    decision.ApplyErrorMessage = new Win32Exception(lastError).Message;
                    decision.ActionType = ScanActionType.Error;
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", null, path, "_File");
                    return ApplyResult.Failed;
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleDisableExecutePhase(string path, ThreatDecision decision)
        {
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
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotDisableExecute", ex, path, "_File");
                return ApplyResult.Error;
            }
        }
    }
}
