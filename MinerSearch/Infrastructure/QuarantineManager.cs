using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using MSearch.Core;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using Win32Wrapper;

namespace MSearch
{
    internal static class QuarantineManager
    {
        const string QUARANTINE_PATH = @"Software\M1nerSearch\Quarantine";
        const string MAIN_PATH = @"Software\M1nerSearch";

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseServiceHandle(IntPtr hSCObject);
        const string ITEM_TYPE = "ItemType";
        const string QUARANTINED_AT = "QuarantinedAt";
        const string TOTAL_PARTS = "TotalParts";
        const string ORIGINAL_PATH = "OriginalPath";

        #region List

        public static List<QuarantineItem> List()
        {
            var items = new List<QuarantineItem>();

            try
            {
                using (var hkcu = Registry.CurrentUser.OpenSubKey(QUARANTINE_PATH))
                {
                    if (hkcu != null)
                        LoadFromHive(hkcu, items);
                }
            }
            catch { }

            try
            {
                using (var hklm = Registry.LocalMachine.OpenSubKey(QUARANTINE_PATH))
                {
                    if (hklm != null)
                        LoadFromHive(hklm, items);
                }
            }
            catch { }

            return items;
        }

        static void LoadFromHive(RegistryKey baseKey, List<QuarantineItem> items)
        {
            foreach (string subKeyName in baseKey.GetSubKeyNames())
            {
                try
                {
                    using (var subKey = baseKey.OpenSubKey(subKeyName))
                    {
                        if (subKey == null) continue;

                        string originalPath = subKey.GetValue(ORIGINAL_PATH) as string;
                        if (string.IsNullOrEmpty(originalPath)) continue;

                        string itemTypeStr = subKey.GetValue(ITEM_TYPE) as string;
                        QuarantineItemType itemType = QuarantineItemType.File;

                        if (!string.IsNullOrEmpty(itemTypeStr))
                        {
                            switch (itemTypeStr)
                            {
                                case "Task": itemType = QuarantineItemType.Task; break;
                                case "Service": itemType = QuarantineItemType.Service; break;
                                default: itemType = QuarantineItemType.File; break;
                            }
                        }
                        else
                        {
                            // Legacy detection: no ItemType value
                            byte[] fileData = subKey.GetValue("FileData") as byte[];
                            object totalPartsObj = subKey.GetValue(TOTAL_PARTS);

                            if (fileData == null && totalPartsObj == null)
                                continue;

                            itemType = QuarantineItemType.File;
                        }

                        long size = 0;
                        if (itemType == QuarantineItemType.File)
                        {
                            byte[] fileData = subKey.GetValue("FileData") as byte[];
                            if (fileData != null)
                            {
                                size = fileData.Length;
                            }
                            else
                            {
                                object totalPartsObj = subKey.GetValue(TOTAL_PARTS);
                                if (totalPartsObj is int totalParts)
                                {
                                    for (int i = 0; i < totalParts; i++)
                                    {
                                        var part = subKey.GetValue($"FileData_Part{i}") as byte[];
                                        if (part != null) size += part.Length;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Task/Service: display the item name, not file size
                            size = 0;
                        }

                        string fileSize = itemType == QuarantineItemType.File
                            ? FileChecker.GetFileSize(size)
                            : GetItemSizeLabel(itemType, subKeyName, subKey);

                        items.Add(new QuarantineItem
                        {
                            OriginalPath = originalPath,
                            FileHash = subKeyName,
                            FileSize = fileSize,
                            ItemType = itemType,
                        });
                    }
                }
                catch { }
            }
        }

        static string GetItemSizeLabel(QuarantineItemType itemType, string subKeyName, RegistryKey subKey)
        {
            switch (itemType)
            {
                case QuarantineItemType.Task:
                    int totalParts = 0;
                    object tp = subKey.GetValue(TOTAL_PARTS);
                    if (tp is int tpi) totalParts = tpi;
                    long xmlSize = 0;
                    for (int i = 0; i < totalParts; i++)
                    {
                        var part = subKey.GetValue($"FileData_Part{i}") as byte[];
                        if (part != null) xmlSize += part.Length;
                    }
                    return FileChecker.GetFileSize(xmlSize);

                case QuarantineItemType.Service:
                    return "Service";

                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Delete

        public static bool Delete(string subKeyName)
        {
            try
            {
                using (var hkcu = Registry.CurrentUser.OpenSubKey(QUARANTINE_PATH, true))
                {
                    if (hkcu?.OpenSubKey(subKeyName) != null)
                    {
                        hkcu.DeleteSubKey(subKeyName);
                        return true;
                    }
                }
            }
            catch { }

            try
            {
                using (var hklm = Registry.LocalMachine.OpenSubKey(QUARANTINE_PATH, true))
                {
                    if (hklm?.OpenSubKey(subKeyName) != null)
                    {
                        hklm.DeleteSubKey(subKeyName);
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        #endregion

        #region RestoreFile

        public static bool RestoreFile(string subKeyName, string restorePath)
        {
            try
            {
                // Try HKCU first (legacy XOR-encrypted)
                using (var hkcu = Registry.CurrentUser.OpenSubKey(QUARANTINE_PATH, true))
                {
                    if (hkcu != null)
                    {
                        using (var fileKey = hkcu.OpenSubKey(subKeyName))
                        {
                            if (fileKey != null)
                            {
                                var encryptedData = fileKey.GetValue("FileData") as byte[];
                                if (encryptedData != null)
                                {
                                    byte[] decryptedData = DecryptData(encryptedData, Encoding.UTF8.GetBytes(subKeyName.Remove(8)));
                                    Directory.CreateDirectory(Path.GetDirectoryName(restorePath));
                                    File.WriteAllBytes(restorePath, decryptedData);
                                    hkcu.DeleteSubKey(subKeyName);
                                    return true;
                                }
                            }
                        }
                    }
                }

                // Try HKLM (chunked)
                using (var hklm = Registry.LocalMachine.OpenSubKey(QUARANTINE_PATH, true))
                {
                    if (hklm == null) return false;

                    using (var fileKey = hklm.OpenSubKey(subKeyName))
                    {
                        if (fileKey != null)
                        {
                            int? totalParts = fileKey.GetValue(TOTAL_PARTS) as int?;
                            if (!totalParts.HasValue) return false;

                            List<byte> data = new List<byte>();
                            for (int i = 0; i < totalParts.Value; i++)
                            {
                                byte[] part = fileKey.GetValue($"FileData_Part{i}") as byte[];
                                if (part == null) return false;
                                data.AddRange(part);
                            }

                            Directory.CreateDirectory(Path.GetDirectoryName(restorePath));
                            File.WriteAllBytes(restorePath, data.ToArray());
                            hklm.DeleteSubKey(subKeyName);
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, subKeyName);
                return false;
            }
        }

        #endregion

        #region AddFile

        public static bool AddFile(string sourceFilePath, bool deleteFromSource = true)
        {
            if (!File.Exists(sourceFilePath)) return false;

            try
            {
                EnsureRegistryAccessible();

                string fileHash = FileChecker.CalculateMD5(sourceFilePath);
                const int blockSize = 1024 * 512;

                using (var baseKey = Registry.LocalMachine.CreateSubKey(QUARANTINE_PATH))
                {
                    if (baseKey == null) return false;

                    using (var subKey = baseKey.CreateSubKey(fileHash))
                    {
                        if (subKey == null) return false;

                        subKey.SetValue(ITEM_TYPE, "File");
                        subKey.SetValue(QUARANTINED_AT, DateTime.Now.ToString("o"));
                        subKey.SetValue(ORIGINAL_PATH, sourceFilePath, RegistryValueKind.String);

                        using (var fileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            long fileSize = fileStream.Length;
                            int totalParts = (int)Math.Ceiling((double)fileSize / blockSize);
                            subKey.SetValue(TOTAL_PARTS, totalParts, RegistryValueKind.DWord);

                            byte[] buffer = new byte[blockSize];
                            for (int i = 0; i < totalParts; i++)
                            {
                                int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                                if (bytesRead > 0)
                                {
                                    byte[] actualData = buffer;
                                    if (bytesRead < blockSize)
                                    {
                                        actualData = new byte[bytesRead];
                                        Array.Copy(buffer, actualData, bytesRead);
                                    }
                                    subKey.SetValue($"FileData_Part{i}", actualData, RegistryValueKind.Binary);
                                }
                            }
                        }
                    }
                }

                if (deleteFromSource)
                    UnlockObjectClass.KillAndDelete(sourceFilePath);

                return !File.Exists(sourceFilePath);
            }
            catch { return false; }
        }

        #endregion

        #region AddTask

        public static bool AddTask(ScheduledTaskInfo info)
        {
            if (info == null) return false;

            try
            {
                EnsureRegistryAccessible();

                // Read the XML bytes via Native.CreateFile (long path support)
                byte[] xmlBytes = ReadAllBytesLongPath(info.XmlPath);
                if (xmlBytes == null || xmlBytes.Length == 0) return false;

                string subKeyName = info.Guid.ToString("B");

                using (var baseKey = Registry.LocalMachine.CreateSubKey(QUARANTINE_PATH))
                {
                    if (baseKey == null) return false;

                    using (var subKey = baseKey.CreateSubKey(subKeyName))
                    {
                        if (subKey == null) return false;

                        subKey.SetValue(ITEM_TYPE, "Task");
                        subKey.SetValue(QUARANTINED_AT, DateTime.Now.ToString("o"));

                        // Logical task path normalized to forward slashes: root -> "/Name", subfolder -> "/Folder/Name"
                        string folderPart = (info.Path ?? "\\").Replace('\\', '/').TrimEnd('/');
                        subKey.SetValue(ORIGINAL_PATH, $"{folderPart}/{info.Name}", RegistryValueKind.String);

                        // Store XML as chunked binary
                        const int blockSize = 1024 * 512;
                        int totalParts = (int)Math.Ceiling((double)xmlBytes.Length / blockSize);
                        subKey.SetValue(TOTAL_PARTS, totalParts, RegistryValueKind.DWord);

                        for (int i = 0; i < totalParts; i++)
                        {
                            int offset = i * blockSize;
                            int length = Math.Min(blockSize, xmlBytes.Length - offset);
                            byte[] chunk = new byte[length];
                            Array.Copy(xmlBytes, offset, chunk, 0, length);
                            subKey.SetValue($"FileData_Part{i}", chunk, RegistryValueKind.Binary);
                        }
                    }
                }

                return true;
            }
            catch { return false; }
        }

        #endregion

        #region AddService

        public static bool AddService(string serviceName, ServiceControllerStatus status, NativeServiceController.ServiceStartMode startMode)
        {
            try
            {
                EnsureRegistryAccessible();

                string subKeyName = serviceName;

                using (var baseKey = Registry.LocalMachine.CreateSubKey(QUARANTINE_PATH))
                {
                    if (baseKey == null) return false;

                    using (var subKey = baseKey.CreateSubKey(subKeyName))
                    {
                        if (subKey == null) return false;

                        subKey.SetValue(ITEM_TYPE, "Service");
                        subKey.SetValue(QUARANTINED_AT, DateTime.Now.ToString("o"));
                        subKey.SetValue(ORIGINAL_PATH, serviceName, RegistryValueKind.String);

                        // WasRunning flag
                        subKey.SetValue("WasRunning", (status == ServiceControllerStatus.Running) ? 1 : 0, RegistryValueKind.DWord);

                        // Read service config via QueryServiceConfig
                        try
                        {
                            var config = ServiceHelper.GetServiceInfo(serviceName);
                            subKey.SetValue("DisplayName", config.DisplayName ?? string.Empty);
                            subKey.SetValue("ImagePath", config.BinaryPathName ?? string.Empty);
                            subKey.SetValue("StartType", (int)config.StartType, RegistryValueKind.DWord);
                            subKey.SetValue("ServiceType", (int)config.ServiceType, RegistryValueKind.DWord);
                            subKey.SetValue("ServiceStartName", config.ServiceStartName ?? string.Empty);
                            subKey.SetValue("LoadOrderGroup", config.LoadOrderGroup ?? string.Empty);
                        }
                        catch
                        {
                            // Fallback: read directly from registry
                            string svcRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
                            using (var svcKey = Registry.LocalMachine.OpenSubKey(svcRegPath))
                            {
                                if (svcKey != null)
                                {
                                    CopyValue(subKey, svcKey, "DisplayName");
                                    CopyValue(subKey, svcKey, "ImagePath");
                                    CopyValue(subKey, svcKey, "Start");
                                    CopyValue(subKey, svcKey, "Type");
                                    CopyValue(subKey, svcKey, "ObjectName");
                                }
                            }
                        }

                        // Dependencies: REG_MULTI_SZ stored as "|" delimited string
                        try
                        {
                            string svcRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
                            using (var depKey = Registry.LocalMachine.OpenSubKey(svcRegPath))
                            {
                                string[] deps = depKey?.GetValue("Depend") as string[];
                                if (deps != null && deps.Length > 0)
                                    subKey.SetValue("Depend", string.Join("|", deps), RegistryValueKind.String);
                            }
                        }
                        catch { }

                        // Group (LoadOrderGroup) from registry — authoritative source
                        try
                        {
                            string svcRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
                            using (var grpKey = Registry.LocalMachine.OpenSubKey(svcRegPath))
                            {
                                string group = grpKey?.GetValue("Group") as string;
                                if (!string.IsNullOrEmpty(group))
                                    subKey.SetValue("Group", group, RegistryValueKind.String);
                            }
                        }
                        catch { }

                        // Description
                        try
                        {
                            string descRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
                            using (var descKey = Registry.LocalMachine.OpenSubKey(descRegPath))
                            {
                                string desc = descKey?.GetValue("Description") as string;
                                if (desc != null)
                                    subKey.SetValue("Description", desc);
                            }
                        }
                        catch { }

                        // ServiceDll (for svchost services)
                        try
                        {
                            string dllRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}\\Parameters";
                            using (var dllKey = Registry.LocalMachine.OpenSubKey(dllRegPath))
                            {
                                string serviceDll = dllKey?.GetValue("ServiceDll") as string;
                                if (serviceDll != null)
                                    subKey.SetValue("ServiceDll", serviceDll);
                            }
                        }
                        catch { }

                        // SDDL
                        try
                        {
                            string sddl = ServiceHelper.GetServiceSddl(serviceName);
                            if (sddl != null)
                                subKey.SetValue("Sddl", sddl);
                        }
                        catch { }
                    }
                }

                return true;
            }
            catch { return false; }
        }

        #endregion

        #region RestoreTask

        public static bool RestoreTask(string subKeyName)
        {
            if (AppConfig.GetInstance.bootMode != BootMode.Normal)
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_QuarantineTaskRestoreSafeMode", subKeyName);
                return false;
            }

            try
            {
                // Read XML from quarantine
                byte[] xmlBytes = ReadChunkedFromHive(subKeyName);
                if (xmlBytes == null || xmlBytes.Length == 0) return false;

                // Task XML files are UTF-16LE with BOM; decode with BOM detection (fallback UTF-8)
                string xmlContent;
                using (var ms = new MemoryStream(xmlBytes))
                using (var sr = new StreamReader(ms, true))
                {
                    xmlContent = sr.ReadToEnd();
                }

                // Read original task path
                string taskPath = null;
                using (var hklm = Registry.LocalMachine.OpenSubKey(QUARANTINE_PATH))
                {
                    taskPath = hklm?.OpenSubKey(subKeyName)?.GetValue(ORIGINAL_PATH) as string;
                }

                if (string.IsNullOrEmpty(taskPath)) return false;

                // Use Task Scheduler COM to register the task
                Type schedulerType = Type.GetTypeFromProgID("Schedule.Service");
                if (schedulerType == null) return false;

                object scheduler = Activator.CreateInstance(schedulerType);
                schedulerType.InvokeMember("Connect", System.Reflection.BindingFlags.InvokeMethod, null, scheduler, null);

                // taskPath may be stored with "/" (new) or "\" (legacy) — parse leniently
                string unified = taskPath.Replace('\\', '/');
                int lastSlash = unified.LastIndexOf('/');
                string taskName = lastSlash >= 0 ? unified.Substring(lastSlash + 1) : unified;
                string parentPath = lastSlash > 0 ? unified.Substring(0, lastSlash) : "";

                // Task Scheduler COM API accepts only backslash paths; root is "\"
                object rootFolder = schedulerType.InvokeMember("GetFolder",
                    System.Reflection.BindingFlags.InvokeMethod, null, scheduler, new object[] { "\\" });

                Type folderType = rootFolder.GetType();

                // Resolve target folder, walking from root with relative names; create missing folders
                object targetFolder = rootFolder;

                if (!string.IsNullOrEmpty(parentPath))
                {
                    string[] parts = parentPath.Trim('/').Split('/');
                    object currentFolder = rootFolder;
                    foreach (string part in parts)
                    {
                        if (string.IsNullOrEmpty(part)) continue;
                        object parentOfMissing = currentFolder;

                        try
                        {
                            currentFolder = folderType.InvokeMember("GetFolder",
                                System.Reflection.BindingFlags.InvokeMethod, null, parentOfMissing, new object[] { part });
                        }
                        catch
                        {
                            folderType.InvokeMember("CreateFolder",
                                System.Reflection.BindingFlags.InvokeMethod, null, parentOfMissing, new object[] { part, "" });
                            currentFolder = folderType.InvokeMember("GetFolder",
                                System.Reflection.BindingFlags.InvokeMethod, null, parentOfMissing, new object[] { part });
                        }
                    }

                    targetFolder = currentFolder;
                }

                // Remove any partial entry left from failed registrations, then register clean
                try
                {
                    targetFolder.GetType().InvokeMember("DeleteTask",
                        System.Reflection.BindingFlags.InvokeMethod, null, targetFolder,
                        new object[] { taskName, 0 });
                }
                catch { }

                // Register the task in the target folder:
                // path, xmlText, flags(TASK_CREATE_OR_UPDATE=6), userId=null, logonType=null, logonFlags(TASK_LOGON_NONE=0), sddl=null
                targetFolder.GetType().InvokeMember("RegisterTask",
                    System.Reflection.BindingFlags.InvokeMethod, null, targetFolder,
                    new object[] { taskName, xmlContent, 6, null, null, 0, null });

                // Delete from quarantine
                Delete(subKeyName);

                AppConfig.GetInstance.LL.LogSuccessMessage("_QuarantineTaskRestored", taskPath);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is System.Reflection.TargetInvocationException && ex.InnerException != null)
                    ex = ex.InnerException;
                AppConfig.GetInstance.LL.LogErrorMessage("_QuarantineTaskRestoreFailed", ex, subKeyName);
                return false;
            }
        }

        #endregion

        #region RestoreService

        public static bool RestoreService(string subKeyName)
        {
            try
            {
                using (var hklm = Registry.LocalMachine.OpenSubKey($"{QUARANTINE_PATH}\\{subKeyName}"))
                {
                    if (hklm == null) return false;

                    string serviceName = hklm.GetValue(ORIGINAL_PATH) as string ?? subKeyName;
                    string displayName = hklm.GetValue("DisplayName") as string ?? serviceName;
                    string imagePath = hklm.GetValue("ImagePath") as string;
                    int serviceType = GetIntValue(hklm, "ServiceType", GetIntValue(hklm, "Type", ServiceHelper.SERVICE_WIN32_OWN_PROCESS));
                    int startType = GetIntValue(hklm, "StartType", GetIntValue(hklm, "Start", (int)NativeServiceController.ServiceStartMode.Manual));
                    string startName = hklm.GetValue("ServiceStartName") as string ?? hklm.GetValue("ObjectName") as string;
                    string loadOrderGroup = hklm.GetValue("Group") as string ?? hklm.GetValue("LoadOrderGroup") as string;

                    // Dependencies: "|" delimited -> double-null-terminated for CreateService
                    string depsStr = hklm.GetValue("Depend") as string;
                    string dependencies = null;
                    if (!string.IsNullOrEmpty(depsStr))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string d in depsStr.Split('|'))
                        {
                            if (!string.IsNullOrEmpty(d)) sb.Append(d).Append('\0');
                        }
                        if (sb.Length > 0) dependencies = sb.ToString() + "\0";
                    }

                    if (string.IsNullOrEmpty(startName) ||
                        startName.Equals("LocalSystem", StringComparison.OrdinalIgnoreCase))
                    {
                        startName = null;
                    }

                    // Create the service
                    IntPtr svcHandle = ServiceHelper.CreateServiceEntry(
                        serviceName, displayName, serviceType, startType,
                        imagePath, startName, dependencies, loadOrderGroup);

                    if (svcHandle == IntPtr.Zero)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_QuarantineServiceRestoreFailed", null, serviceName);
                        return false;
                    }

                    CloseServiceHandle(svcHandle);

                    // Write-back: Description
                    string description = hklm.GetValue("Description") as string;
                    if (description != null)
                    {
                        try
                        {
                            string descRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}";
                            using (var descKey = Registry.LocalMachine.CreateSubKey(descRegPath))
                            {
                                descKey?.SetValue("Description", description);
                            }
                        }
                        catch { }
                    }

                    // Write-back: ServiceDll
                    string serviceDll = hklm.GetValue("ServiceDll") as string;
                    if (serviceDll != null)
                    {
                        try
                        {
                            string dllRegPath = $"SYSTEM\\CurrentControlSet\\Services\\{serviceName}\\Parameters";
                            using (var dllKey = Registry.LocalMachine.CreateSubKey(dllRegPath))
                            {
                                dllKey?.SetValue("ServiceDll", serviceDll, RegistryValueKind.ExpandString);
                            }
                        }
                        catch { }
                    }

                    // Write-back: SDDL
                    string sddl = hklm.GetValue("Sddl") as string;
                    if (!string.IsNullOrEmpty(sddl))
                    {
                        ServiceHelper.SetSddl(serviceName, sddl);
                    }

                    // Restore SafeBoot entries if they existed
                    // (Not restored for malware services — by design)

                    AppConfig.GetInstance.LL.LogSuccessMessage("_QuarantineServiceRestored", serviceName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_QuarantineServiceRestoreFailed", ex, subKeyName);
                return false;
            }
        }

        #endregion

        #region Helpers

        static void EnsureRegistryAccessible()
        {
            try
            {
                if (UnlockObjectClass.IsRegistryKeyBlocked(MAIN_PATH))
                    UnlockObjectClass.UnblockRegistry(MAIN_PATH);
            }
            catch { }

            try
            {
                if (UnlockObjectClass.IsRegistryKeyBlocked(MAIN_PATH, RegistryHive.LocalMachine))
                    UnlockObjectClass.UnblockRegistry(MAIN_PATH, RegistryHive.LocalMachine);
            }
            catch { }
        }

        static void CopyValue(RegistryKey dst, RegistryKey src, string name)
        {
            object val = src.GetValue(name);
            if (val != null)
                dst.SetValue(name, val);
        }

        static int GetIntValue(RegistryKey key, string name, int defaultValue)
        {
            object val = key.GetValue(name);
            if (val is int i) return i;
            if (val is uint u) return (int)u;
            return defaultValue;
        }

        static byte[] ReadAllBytesLongPath(string filePath)
        {
            string longPath = filePath.StartsWith(@"\\?\") ? filePath : @"\\?\" + filePath;

            using (SafeFileHandle fileHandle = Native.CreateFile(
                longPath,
                FileAccess.Read,
                FileShare.Read,
                IntPtr.Zero,
                FileMode.Open,
                0,
                IntPtr.Zero))
            {
                if (fileHandle.IsInvalid) return null;

                using (var stream = new FileStream(fileHandle, FileAccess.Read))
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        static byte[] ReadChunkedFromHive(string subKeyName)
        {
            using (var hklm = Registry.LocalMachine.OpenSubKey($"{QUARANTINE_PATH}\\{subKeyName}"))
            {
                if (hklm == null) return null;

                object totalPartsObj = hklm.GetValue(TOTAL_PARTS);
                if (!(totalPartsObj is int totalParts)) return null;

                List<byte> data = new List<byte>();
                for (int i = 0; i < totalParts; i++)
                {
                    byte[] part = hklm.GetValue($"FileData_Part{i}") as byte[];
                    if (part == null) return null;
                    data.AddRange(part);
                }
                return data.ToArray();
            }
        }

        static byte[] DecryptData(byte[] encryptedData, byte[] key)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                throw new ArgumentException("Decryption data is invalid");

            byte[] decryptedData = new byte[encryptedData.Length];
            for (int i = 0; i < encryptedData.Length; i++)
                decryptedData[i] = (byte)(encryptedData[i] ^ key[i % key.Length]);

            return decryptedData;
        }

        #endregion
    }
}
