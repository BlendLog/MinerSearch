using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.IO;

namespace MSearch.Core.ThreatHandlers
{
    /// <summary>
    /// Обработчик файлов из папки автозагрузки Shell.
    /// Удаляет/карантинит файл из автозагрузки, DisableExecute на ShortcutTargetFile.
    /// </summary>
    internal sealed class ShellStartupThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.ShellStartupFile;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var shellFile = decision.Target as ShellStartupFileThreatObject;
            if (shellFile == null) return ApplyResult.NotApplicable;

            if (phase == CleanupPhase.DisableExecuteOnly)
            {
                // Отключаем выполнение целевого файла ярлыка
                if (shellFile.ShortcutTargetFile != null && shellFile.ShortcutTargetFile.ShouldDisableExecute)
                {
                    return HandleDisableExecute(shellFile.ShortcutTargetFile.FilePath, decision);
                }

                return ApplyResult.Skipped;
            }

            if (phase == CleanupPhase.Finalize)
            {
                // Удаление файла из автозагрузки (ярлык или файл с cmd.exe /c, invisible chars, bad args)
                if (shellFile.ShouldDeleteFile)
                {
                    return HandleDeleteFile(shellFile.FilePath, decision);
                }

                // Карантин подозрительных файлов (SFX, hidden, high entropy .NET, обнаружено анализатором)
                if (shellFile.ShouldMoveToQuarantine)
                {
                    return HandleMoveToQuarantine(shellFile.FilePath, decision);
                }

                return ApplyResult.Skipped;
            }

            return ApplyResult.NotApplicable;
        }

        private ApplyResult HandleMoveToQuarantine(string path, ThreatDecision decision)
        {
            if (!File.Exists(path))
            {
                AppConfig.GetInstance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.DarkGray);
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
                File.SetAttributes(path, FileAttributes.Normal);

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
                AppConfig.GetInstance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.DarkGray);
                return ApplyResult.NotApplicable;
            }

            try
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);

                if (!File.Exists(path))
                {
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MaliciousFileDeleted", path);
                    decision.ActionType = ScanActionType.Deleted;
                    return ApplyResult.Success;
                }

                decision.ActionType = ScanActionType.Error;
                return ApplyResult.Failed;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleDisableExecute(string path, ThreatDecision decision)
        {
            try
            {
                UnlockObjectClass.DisableExecute(path);
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
