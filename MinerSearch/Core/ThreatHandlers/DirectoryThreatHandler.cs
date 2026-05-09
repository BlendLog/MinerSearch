using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.IO;

namespace MSearch.Core.ThreatHandlers
{
    internal sealed class DirectoryThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Directory;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var dirThreat = decision.Target as DirectoryThreatObject;
            if (dirThreat == null) return ApplyResult.NotApplicable;

            string dirPath = dirThreat.DirectoryPath;

#if DEBUG
            Console.WriteLine($"[DBG DirectoryThreatHandler] Phase={phase}, Path={dirPath}, Delete={dirThreat.ShouldDeleteDirectory}, Unlock={dirThreat.ShouldUnlockAndDelete}");
#endif

            if (phase == CleanupPhase.SuspendOnly || phase == CleanupPhase.DisableExecuteOnly)
            {
                return ApplyResult.Skipped;
            }

            if (phase == CleanupPhase.Finalize)
            {
                if (dirThreat.ShouldUnlockAndDelete)
                    return HandleUnlockAndDelete(dirPath, decision);

                if (dirThreat.ShouldDeleteDirectory)
                    return HandleDeleteDirectory(dirPath, decision);

#if DEBUG
                Console.WriteLine($"[DBG DirectoryThreatHandler] SKIPPED — no flags set");
#endif
                return ApplyResult.Skipped;
            }

            return ApplyResult.NotApplicable;
        }

        private ApplyResult HandleDeleteDirectory(string dirPath, ThreatDecision decision)
        {
            if (!Directory.Exists(dirPath))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_DirectoryIsNotFound", dirPath, ConsoleColor.DarkGray);
                return ApplyResult.NotApplicable;
            }

            try
            {
                FileSystemManager.ResetAttributes(dirPath);
                Directory.Delete(dirPath, true);

                if (Directory.Exists(dirPath))
                {
                    decision.ActionType = ScanActionType.Error;
                    return ApplyResult.Failed;
                }
                else
                {
                    decision.ActionType = ScanActionType.Deleted;
                    return ApplyResult.Success;
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, dirPath, "_Directory");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleUnlockAndDelete(string dirPath, ThreatDecision decision)
        {
            if (!Directory.Exists(dirPath))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_DirectoryIsNotFound", dirPath, ConsoleColor.DarkGray);
                return ApplyResult.NotApplicable;
            }

            try
            {
                // Сначала пытаемся разблокировать
                if (UnlockObjectClass.IsLockedObject(dirPath))
                {
                    if (UnlockObjectClass.ResetObjectACL(dirPath))
                    {
                        AppConfig.GetInstance.LL.LogSuccessMessage("_UnlockSuccess", dirPath);
                    }
                }

                FileSystemManager.ResetAttributes(dirPath);
                Directory.Delete(dirPath, true);

                if (Directory.Exists(dirPath))
                {
                    decision.ActionType = ScanActionType.Error;
                    return ApplyResult.Failed;
                }
                else
                {
                    decision.ActionType = ScanActionType.Deleted;
                    return ApplyResult.Success;
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, dirPath, "_Directory");
                return ApplyResult.Error;
            }
        }
    }
}
