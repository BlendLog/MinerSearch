using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Win32Wrapper;

namespace MSearch.Core.ThreatHandlers
{
    internal sealed class TaskThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.ScheduledTask;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var taskThreat = decision.Target as TaskThreatObject;
            if (taskThreat == null) return ApplyResult.NotApplicable;

            if (phase == CleanupPhase.DisableExecuteOnly)
            {
                if (taskThreat.LinkedFile != null && taskThreat.LinkedFile.ShouldDisableExecute)
                {
                    UnlockObjectClass.DisableExecute(taskThreat.LinkedFile.FilePath);
                }

                if (taskThreat.LinkedFileFromArgs != null && taskThreat.LinkedFileFromArgs.ShouldDisableExecute)
                {
                    UnlockObjectClass.DisableExecute(taskThreat.LinkedFileFromArgs.FilePath);
                }

                decision.ActionType = ScanActionType.Skipped;
            }

            if (phase == CleanupPhase.Finalize)
            {

                if (taskThreat.ActionDeleteTask)
                {
                    if (DeleteTaskDirectly(taskThreat.Info, decision))
                    {
                        string reason = !string.IsNullOrEmpty(taskThreat.DetectionReasonRes)
                            ? taskThreat.DetectionReasonRes
                            : "_Malic1ousTask";
                        AppConfig.GetInstance.LL.LogSuccessMessage(reason, $"{taskThreat.Info.Path}\\{taskThreat.Info.Name}", "_Deleted");
                        decision.ActionType = ScanActionType.Deleted;
                        return ApplyResult.Success;
                    }
                    else
                    {
                        decision.ActionType = ScanActionType.Error;
                        return ApplyResult.Failed;
                    }
                }
            }

            return ApplyResult.NotApplicable;
        }

        bool DeleteTaskDirectly(ScheduledTaskInfo taskToDelete, ThreatDecision decision)
        {

            bool success = true;

            try
            {
                string treeParentPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tree" + taskToDelete.Path;
                string treeKeyNameToDelete = taskToDelete.Name;
                if (!DeleteRegistryKeyNative(treeParentPath, treeKeyNameToDelete))
                {
                    success = false;
                }

                string cacheParentPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tasks";
                string cacheKeyNameToDelete = taskToDelete.Guid.ToString("B");
                if (!DeleteRegistryKeyNative(cacheParentPath, cacheKeyNameToDelete))
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                return false;
            }

            try
            {
                if (FileSystemManager.IsFileExistsLongPath(taskToDelete.XmlPath))
                {
                    string longPath = taskToDelete.XmlPath.StartsWith(@"\\?\") ? taskToDelete.XmlPath : @"\\?\" + taskToDelete.XmlPath;

                    if (!Native.SetFileAttributes(longPath, FileAttributes.Normal))
                    {
                        int errorCode = Marshal.GetLastWin32Error();

                        if (errorCode != 0)
                        {
                            AppConfig.GetInstance.LL.LogWarnMessage("_WarnResetAttributes", new System.ComponentModel.Win32Exception(errorCode).Message);
                        }
                    }

                    if (!Native.DeleteFile(longPath))
                    {
                        int errorCode = Marshal.GetLastWin32Error();
                        AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", new System.ComponentModel.Win32Exception(errorCode), taskToDelete.XmlPath, "_File");
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, taskToDelete.XmlPath, "_File");
                success = false;
            }

            return success;
        }

        bool DeleteRegistryKeyNative(string parentKeyPath, string subKeyToDelete)
        {
            IntPtr hParentKey = IntPtr.Zero;
            try
            {
                int openResult = Native.RegOpenKeyEx((IntPtr)Native.HKEY_LOCAL_MACHINE, parentKeyPath, 0, Native.KEY_ENUMERATE_SUB_KEYS | Native.DELETE, out hParentKey);

                if (openResult != 0)
                {
#if DEBUG
                    Console.WriteLine($"\t[DBG] Error on delete registry task key: {openResult}");
#endif

                    return false;
                }

                int deleteResult = Native.RegDeleteTree(hParentKey, subKeyToDelete);

                return deleteResult == 0;
            }
            finally
            {
                if (hParentKey != IntPtr.Zero)
                {
                    Native.RegCloseKey(hParentKey);
                }
            }
        }

    }
}
