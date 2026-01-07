using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using MSearch.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace MSearch.Infrastructure
{
    public class SafeModeTaskParser
    {
        private const string TASK_CACHE_TREE_REG_PATH = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tree";
        private const string TASK_CACHE_TASKS_REG_PATH = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tasks";

        public List<ScheduledTaskInfo> GetAllTasks()
        {
            var allTasks = new List<ScheduledTaskInfo>();
            try
            {
                using (var rootKey = Registry.LocalMachine.OpenSubKey(TASK_CACHE_TREE_REG_PATH))
                {
                    if (rootKey != null)
                    {
                        ParseTaskTree(rootKey, "\\", allTasks);
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }
            return allTasks;
        }

        void ParseTaskTree(RegistryKey currentFolderKey, string currentLogicalPath, List<ScheduledTaskInfo> taskList)
        {
            foreach (var subKeyName in currentFolderKey.GetSubKeyNames())
            {
                using (var subKey = currentFolderKey.OpenSubKey(subKeyName))
                {
                    if (subKey == null) continue;
                    if (subKey.GetValue("Id") is string idString)
                    {
                        if (Guid.TryParse(idString.Trim(' ', '\0'), out Guid taskId))
                        {
                            var taskInfo = ParseSingleTask(subKeyName, taskId, currentLogicalPath);
                            if (taskInfo != null) taskList.Add(taskInfo);
                        }
                    }
                    else
                    {
                        string nextPath = Path.Combine(currentLogicalPath, subKeyName).Replace(System.IO.Path.DirectorySeparatorChar, '\\');
                        ParseTaskTree(subKey, nextPath, taskList);
                    }
                }
            }
        }

        ScheduledTaskInfo ParseSingleTask(string taskName, Guid taskId, string taskPath)
        {

            FileStream fileStream = null;
            SafeFileHandle fileHandle = null;

            try
            {
                string taskRegKeyPath = Path.Combine(TASK_CACHE_TASKS_REG_PATH, taskId.ToString("B"));
                using (var taskKey = Registry.LocalMachine.OpenSubKey(taskRegKeyPath))
                {
                    if (taskKey == null) return null;
                    var logicalPath = taskKey.GetValue("Path") as string;
                    if (string.IsNullOrEmpty(logicalPath)) return null;

                    string system32Path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    string tasksFolderPath = Path.Combine(system32Path, "Tasks");
                    string relativeTaskPath = logicalPath.TrimStart('\\');
                    string fullXmlPath = Path.Combine(tasksFolderPath, relativeTaskPath);


                    if (!FileSystemManager.IsFileExistsLongPath(fullXmlPath))
                    {
                        fullXmlPath = Environment.ExpandEnvironmentVariables(logicalPath);
                        if (!FileSystemManager.IsFileExistsLongPath(fullXmlPath)) return null;
                    }

                    string longPath = fullXmlPath.StartsWith(@"\\?\") ? fullXmlPath : @"\\?\" + fullXmlPath;
                    fileHandle = Native.CreateFile(
                        longPath,
                        FileAccess.Read,
                        FileShare.Read,
                        IntPtr.Zero,
                        FileMode.Open,
                        0,
                        IntPtr.Zero);

                    if (fileHandle.IsInvalid)
                    {
                        return null;
                    }

                    fileStream = new FileStream(fileHandle, FileAccess.Read);

                    XDocument doc = XDocument.Load(fileStream);
                    XNamespace ns = "http://schemas.microsoft.com/windows/2004/02/mit/task";

                    var taskInfo = new ScheduledTaskInfo
                    {
                        Name = taskName,
                        Path = taskPath,
                        Guid = taskId,
                        XmlPath = fullXmlPath,
                        IsEnabled = !bool.TryParse(doc.Descendants(ns + "Enabled").FirstOrDefault()?.Value, out bool disabled) || !disabled,
                    };

                    var enabledNode = doc.Descendants(ns + "Enabled").FirstOrDefault();
                    if (enabledNode != null && enabledNode.Value.Equals("false", StringComparison.OrdinalIgnoreCase))
                    {
                        taskInfo.IsEnabled = false;
                    }

                    var actionsNode = doc.Descendants(ns + "Actions").FirstOrDefault();
                    if (actionsNode != null)
                    {
                        foreach (var execNode in actionsNode.Elements(ns + "Exec"))
                        {
                            taskInfo.ExecActions.Add(new ExecActionInfo
                            {
                                Path = execNode.Element(ns + "Command")?.Value,
                                Arguments = execNode.Element(ns + "Arguments")?.Value
                            });
                        }
                    }

                    return taskInfo;
                }
            }
            catch (Exception ex)
            {
                if (AppConfig.Instance.verbose)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                }

                return null;
            }
            finally
            {
                fileStream?.Close();
                fileHandle?.Close();
            }
        }

        public bool DeleteTaskDirectly(ScheduledTaskInfo taskToDelete)
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
                AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
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
                            AppConfig.Instance.LL.LogWarnMessage("_WarnResetAttributes", new System.ComponentModel.Win32Exception(errorCode).Message);
                        }
                    }

                    if (!Native.DeleteFile(longPath))
                    {
                        int errorCode = Marshal.GetLastWin32Error();
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", new System.ComponentModel.Win32Exception(errorCode), taskToDelete.XmlPath, "_File");
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, taskToDelete.XmlPath, "_File");
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