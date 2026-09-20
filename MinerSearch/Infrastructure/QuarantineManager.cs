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
                                case "Registry": itemType = QuarantineItemType.Registry; break;
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

                case QuarantineItemType.Registry:
                    return "Registry";

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
                // Never silently overwrite a different existing file (data loss).
                // Identical content (same MD5 as the quarantine key) is harmless to rewrite.
                if (!string.IsNullOrEmpty(restorePath) && File.Exists(restorePath) && !IsSameFileContent(restorePath, subKeyName))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_QuarantineRestoreTargetExists", restorePath);
                    return false;
                }

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

        #region AddRegistry

        const string REG_HIVE = "RegHive";
        const string REG_KEY_PATH = "RegKeyPath";
        const string REG_NODE_KIND = "RegNodeKind";
        const string REG_VALUE_NAME = "RegValueName";

        public static bool AddRegistry(RegistryThreatObject reg)
        {
            if (reg == null) return false;

            try
            {
                EnsureRegistryAccessible();

                RegistryKey baseKey = reg.Hive == "HKEY_LOCAL_MACHINE"
                    ? Registry.LocalMachine
                    : Registry.CurrentUser;

                bool wholeKey = reg.NodeType == RegistryNodeType.Key || reg.ActionDeleteParentKey;

                using (RegistryKey source = baseKey.OpenSubKey(reg.KeyPath))
                {
                    if (source == null) return false;

                    byte[] blob;
                    using (var ms = new MemoryStream())
                    {
                        using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                        {
                            if (wholeKey)
                                WriteRegistryKey(writer, source, true);
                            else
                                WriteSingleRegistryValue(writer, source, reg.ValueName);
                        }
                        blob = ms.ToArray();
                    }

                    string displayName = reg.NodeType == RegistryNodeType.Value && !string.IsNullOrEmpty(reg.ValueName)
                        ? $@"{reg.Hive}\{reg.KeyPath}\{reg.ValueName}"
                        : $@"{reg.Hive}\{reg.KeyPath}";

                    string subKeyName = ComputeMd5Hex($"{reg.Hive}|{reg.KeyPath}|{reg.ValueName}");

                    using (var baseQuarantine = Registry.LocalMachine.CreateSubKey(QUARANTINE_PATH))
                    {
                        if (baseQuarantine == null) return false;

                        using (var subKey = baseQuarantine.CreateSubKey(subKeyName))
                        {
                            if (subKey == null) return false;

                            subKey.SetValue(ITEM_TYPE, "Registry");
                            subKey.SetValue(QUARANTINED_AT, DateTime.Now.ToString("o"));
                            subKey.SetValue(ORIGINAL_PATH, displayName, RegistryValueKind.String);
                            subKey.SetValue(REG_HIVE, reg.Hive, RegistryValueKind.String);
                            subKey.SetValue(REG_KEY_PATH, reg.KeyPath, RegistryValueKind.String);
                            subKey.SetValue(REG_NODE_KIND, wholeKey ? "Key" : "Value", RegistryValueKind.String);
                            subKey.SetValue(REG_VALUE_NAME, reg.ValueName ?? string.Empty, RegistryValueKind.String);

                            const int blockSize = 1024 * 512;
                            int totalParts = (int)Math.Ceiling((double)blob.Length / blockSize);
                            subKey.SetValue(TOTAL_PARTS, totalParts, RegistryValueKind.DWord);

                            for (int i = 0; i < totalParts; i++)
                            {
                                int offset = i * blockSize;
                                int length = Math.Min(blockSize, blob.Length - offset);
                                byte[] chunk = new byte[length];
                                Array.Copy(blob, offset, chunk, 0, length);
                                subKey.SetValue($"FileData_Part{i}", chunk, RegistryValueKind.Binary);
                            }
                        }
                    }
                }

                return true;
            }
            catch { return false; }
        }

        static void WriteSingleRegistryValue(BinaryWriter writer, RegistryKey key, string valueName)
        {
            string name = valueName ?? string.Empty;

            writer.Write(1); // values count
            writer.Write(name);

            object value = key.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            RegistryValueKind kind = key.GetValueKind(name);
            writer.Write((int)kind);
            WriteRegistryValueData(writer, kind, value);

            writer.Write(0); // no subkeys
        }

        static void WriteRegistryKey(BinaryWriter writer, RegistryKey key, bool includeSubKeys)
        {
            string[] names = key.GetValueNames();
            writer.Write(names.Length);
            foreach (string name in names)
            {
                writer.Write(name ?? string.Empty);

                object value = key.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                RegistryValueKind kind = key.GetValueKind(name);
                writer.Write((int)kind);
                WriteRegistryValueData(writer, kind, value);
            }

            string[] subKeys = includeSubKeys ? key.GetSubKeyNames() : new string[0];
            writer.Write(subKeys.Length);
            foreach (string sub in subKeys)
            {
                writer.Write(sub);
                using (var subKey = key.OpenSubKey(sub))
                {
                    if (subKey == null)
                    {
                        WriteEmptyNode(writer);
                        continue;
                    }
                    WriteRegistryKey(writer, subKey, true);
                }
            }
        }

        /// <summary>
        /// пишется, когда подраздел не удалось открыть при снятии снимка.
        /// </summary>
        static void WriteEmptyNode(BinaryWriter writer)
        {
            writer.Write(0); // valuesCount
            writer.Write(0); // subKeysCount
        }

        static void WriteRegistryValueData(BinaryWriter writer, RegistryValueKind kind, object value)
        {
            switch (kind)
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    writer.Write(value as string ?? string.Empty);
                    break;

                case RegistryValueKind.DWord:
                    writer.Write(value is int dw ? dw : 0);
                    break;

                case RegistryValueKind.QWord:
                    writer.Write(value is long qw ? qw : 0L);
                    break;

                case RegistryValueKind.MultiString:
                    string[] arr = value as string[] ?? new string[0];
                    writer.Write(arr.Length);
                    foreach (string s in arr)
                        writer.Write(s ?? string.Empty);
                    break;

                case RegistryValueKind.Binary:
                case RegistryValueKind.None:
                    byte[] bytes = value as byte[];
                    writer.Write(bytes?.Length ?? 0);
                    if (bytes != null)
                        writer.Write(bytes);
                    break;

                default:
                    writer.Write(value?.ToString() ?? string.Empty);
                    break;
            }
        }

        #endregion

        #region RestoreRegistry

        /// <summary>
        /// Восстанавливает ветку/значение реестра из карантина.
        /// </summary>
        public static bool RestoreRegistry(string subKeyName)
        {
            try
            {
                string hive;
                string keyPath;
                bool wholeKey;

                using (var subKey = Registry.LocalMachine.OpenSubKey($"{QUARANTINE_PATH}\\{subKeyName}"))
                {
                    if (subKey == null) return false;

                    hive = subKey.GetValue(REG_HIVE) as string;
                    keyPath = subKey.GetValue(REG_KEY_PATH) as string;
                    wholeKey = string.Equals(subKey.GetValue(REG_NODE_KIND) as string, "Key", StringComparison.OrdinalIgnoreCase);
                }

                if (string.IsNullOrEmpty(hive) || string.IsNullOrEmpty(keyPath)) return false;

                byte[] blob = ReadChunkedFromHive(subKeyName);
                if (blob == null || blob.Length == 0) return false;

                RegistryKey baseKey = hive == "HKEY_LOCAL_MACHINE"
                    ? Registry.LocalMachine
                    : Registry.CurrentUser;

                using (RegistryKey key = baseKey.CreateSubKey(keyPath))
                {
                    if (key == null) return false;

                    using (var ms = new MemoryStream(blob))
                    using (var reader = new BinaryReader(ms, Encoding.UTF8))
                    {
                        if (wholeKey)
                            ReadRegistryKey(reader, key);
                        else
                            ReadRegistryValue(reader, key);
                    }
                }

                Delete(subKeyName);

                AppConfig.GetInstance.LL.LogSuccessMessage("_QuarantineRegistryRestored", $@"{hive}\{keyPath}");
                return true;
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_QuarantineRegistryRestoreFailed", ex, subKeyName);
                return false;
            }
        }

        static void ReadRegistryValue(BinaryReader reader, RegistryKey key)
        {
            int valuesCount = reader.ReadInt32();
            if (valuesCount > 0)
            {
                string name = reader.ReadString();
                RegistryValueKind kind = (RegistryValueKind)reader.ReadInt32();
                object value = ReadRegistryValueData(reader, kind);
                SetRegistryValue(key, name, kind, value);
            }

            SkipSubKeys(reader);
        }

        static void ReadRegistryKey(BinaryReader reader, RegistryKey key)
        {
            int valuesCount = reader.ReadInt32();
            for (int i = 0; i < valuesCount; i++)
            {
                string name = reader.ReadString();
                RegistryValueKind kind = (RegistryValueKind)reader.ReadInt32();
                object value = ReadRegistryValueData(reader, kind);
                SetRegistryValue(key, name, kind, value);
            }

            int subKeysCount = reader.ReadInt32();
            for (int i = 0; i < subKeysCount; i++)
            {
                string subName = reader.ReadString();
                using (var subKey = key.CreateSubKey(subName))
                {
                    if (subKey == null)
                    {
                        SkipEmptyNode(reader);
                        continue;
                    }
                    ReadRegistryKey(reader, subKey);
                }
            }
        }

        /// <summary>
        /// Пропускает узел-заглушку [valuesCount=0][subKeysCount=0] (см. WriteEmptyNode).
        /// </summary>
        static void SkipEmptyNode(BinaryReader reader)
        {
            reader.ReadInt32(); // valuesCount
            reader.ReadInt32(); // subKeysCount
        }

        static void SkipSubKeys(BinaryReader reader)
        {
            int subKeysCount = reader.ReadInt32();
            for (int i = 0; i < subKeysCount; i++)
            {
                reader.ReadString();
                SkipSubKeys(reader);
            }
        }

        static object ReadRegistryValueData(BinaryReader reader, RegistryValueKind kind)
        {
            switch (kind)
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return reader.ReadString();

                case RegistryValueKind.DWord:
                    return reader.ReadInt32();

                case RegistryValueKind.QWord:
                    return reader.ReadInt64();

                case RegistryValueKind.MultiString:
                    int count = reader.ReadInt32();
                    string[] arr = new string[count];
                    for (int i = 0; i < count; i++)
                        arr[i] = reader.ReadString();
                    return arr;

                case RegistryValueKind.Binary:
                case RegistryValueKind.None:
                    int len = reader.ReadInt32();
                    return len > 0 ? reader.ReadBytes(len) : new byte[0];

                default:
                    return reader.ReadString();
            }
        }

        static void SetRegistryValue(RegistryKey key, string name, RegistryValueKind kind, object value)
        {
            RegistryValueKind writeKind = kind == RegistryValueKind.None ? RegistryValueKind.Binary : kind;
            try
            {
                key.SetValue(name ?? string.Empty, value, writeKind);
            }
            catch { }
        }

        static string ComputeMd5Hex(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input ?? string.Empty));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
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
            string serviceName = null;
            try
            {
                using (var hklm = Registry.LocalMachine.OpenSubKey($"{QUARANTINE_PATH}\\{subKeyName}"))
                {
                    if (hklm == null) return false;

                    serviceName = hklm.GetValue(ORIGINAL_PATH) as string ?? subKeyName;
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

                } // subkey handle closed before Delete (cf. RestoreTask)

                // Delete from quarantine (was missing: counter stayed > 0 after service restore)
                Delete(subKeyName);

                AppConfig.GetInstance.LL.LogSuccessMessage("_QuarantineServiceRestored", serviceName);
                return true;
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

        static bool IsSameFileContent(string filePath, string quarantineHash)
        {
            try
            {
                string existingHash = FileChecker.CalculateMD5(filePath);
                return !string.IsNullOrEmpty(existingHash) &&
                       existingHash.Equals(quarantineHash, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
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
