using Microsoft.Win32;
using MSearch.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MSearch.UI
{
    public class QuarantineConsoleHandler
    {
        const string REGISTRY_PATH_QUARANTINE = @"Software\\M1nerSearch\\Quarantine";

        public void Execute()
        {
            string[] args = Environment.GetCommandLineArgs();

            if ((AppConfig.Instance.QuarantineRestoreOption || AppConfig.Instance.QuarantineDeleteOption) && args.Length >= 3)
            {

                if (AppConfig.Instance.QuarantineRestoreOption && AppConfig.Instance.QuarantineDeleteOption)
                {
                    DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_InvlaidOptionSelection"), "Quarantine", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                string targets = AppConfig.Instance.quarantineListEnum;

                if (!AppConfig.Instance.Force && AppConfig.Instance.QuarantineRestoreOption)
                {
                    DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_ForceRequired"), "Quarantine", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                var indexes = IndexParse(targets, int.MaxValue);
                if (indexes.Count == 0)
                {
                    DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_IndexListInvalid"), "Quarantine", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                RunNonInteractive(indexes, AppConfig.Instance.QuarantineDeleteOption);
            }
            else
            {
                ShowQuarantineList();
                Console.WriteLine("");
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_RestoreCommandSyntax"), "Quarantine", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        public void ShowQuarantineList()
        {
            var entries = LoadQuarantineItems(REGISTRY_PATH_QUARANTINE);

            if (entries.Count == 0)
            {
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_Title"));

            string path = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Path");
            string hash = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_FileHash");
            string size = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_FileSize");

            Console.WriteLine($"Index | {path,-35} | {hash,-6} | {size}");
            Console.WriteLine(new string('-', 70));
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                Console.WriteLine(string.Format("{0,5} | {1,-35} | {2}... | {3}", i + 1, Shorten(e.OriginalPath, 35), e.FileHash.Substring(0, 6), e.FileSize));
            }
        }

        public void RunNonInteractive(List<int> indexes, bool isDelete)
        {
            var entries = LoadQuarantineItems(REGISTRY_PATH_QUARANTINE);

            if (entries.Count == 0)
            {
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            var affected = new List<string>();

            indexes.Sort();
            indexes.Reverse();

            foreach (int i in indexes)
            {
                if (i < 0 || i >= entries.Count)
                    continue;

                var item = entries[i];
                bool ok = isDelete
                    ? DeleteFileFromQuarantine(REGISTRY_PATH_QUARANTINE, item.FileHash)
                    : RestoreFileFromQuarantine(REGISTRY_PATH_QUARANTINE, item.FileHash, item.OriginalPath);

                if (ok)
                    affected.Add(item.OriginalPath);
            }

            foreach (string path in affected)
            {
                Console.WriteLine(" - " + path);
            }

            DialogDispatcher.Show(isDelete ? AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_FileDeleted").Replace("#COUNT#", affected.Count.ToString()) : AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_FileRestored").Replace("#COUNT#", affected.Count.ToString()), "Quarantine", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        List<int> IndexParse(string input, int max)
        {
            List<int> result = new List<int>();
            if (string.IsNullOrWhiteSpace(input)) return result;
            string[] parts = input.Split(',');
            foreach (string s in parts)
            {
                int n;
                if (int.TryParse(s.Trim(), out n) && n >= 1 && n <= max)
                {
                    int index = n - 1;
                    if (!result.Contains(index))
                        result.Add(index);
                }
            }
            return result;
        }

        string Shorten(string path, int max)
        {
            if (path.Length <= max) return path;
            return "..." + path.Substring(path.Length - max + 3);
        }

        List<QuarantineItem> LoadQuarantineItems(string path)
        {
            var result = new List<QuarantineItem>();

            RegistryKey hkcu = Registry.CurrentUser.OpenSubKey(path);
            if (hkcu != null)
            {
                LoadFrom(hkcu, result);
                hkcu.Close();
            }

            RegistryKey hklm = Registry.LocalMachine.OpenSubKey(path);
            if (hklm != null)
            {
                LoadFrom(hklm, result);
                hklm.Close();
            }

            return result;
        }

        void LoadFrom(RegistryKey baseKey, List<QuarantineItem> result)
        {
            foreach (string subKey in baseKey.GetSubKeyNames())
            {
                RegistryKey key = baseKey.OpenSubKey(subKey);
                if (key == null) continue;

                string originalPath = key.GetValue("OriginalPath") as string;
                byte[] data = key.GetValue("FileData") as byte[];
                object totalPartsObj = key.GetValue("TotalParts");

                long size = 0;
                if (data != null) size = data.Length;
                else if (totalPartsObj is int)
                {
                    int totalParts = (int)totalPartsObj;
                    for (int i = 0; i < totalParts; i++)
                    {
                        var part = key.GetValue("FileData_Part" + i) as byte[];
                        if (part != null) size += part.Length;
                    }
                }

                if (!string.IsNullOrEmpty(originalPath) && size > 0)
                {
                    result.Add(new QuarantineItem
                    {
                        OriginalPath = originalPath,
                        FileHash = subKey,
                        FileSize = FileChecker.GetFileSize(size)
                    });
                }
                key.Close();
            }
        }

        bool DeleteFileFromQuarantine(string path, string hash)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                if (key != null && key.OpenSubKey(hash) != null)
                {
                    key.DeleteSubKey(hash);
                    key.Close();
                    return true;
                }

                key = Registry.LocalMachine.OpenSubKey(path, true);
                if (key != null && key.OpenSubKey(hash) != null)
                {
                    key.DeleteSubKey(hash);
                    key.Close();
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        bool RestoreFileFromQuarantine(string path, string hash, string restorePath)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                if (TryRestore(key, hash, restorePath))
                {
                    key.Close();
                    return true;
                }

                key = Registry.LocalMachine.OpenSubKey(path, true);
                if (TryRestore(key, hash, restorePath))
                {
                    key.Close();
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        bool TryRestore(RegistryKey baseKey, string hash, string path)
        {
            if (baseKey == null) return false;
            RegistryKey key = baseKey.OpenSubKey(hash);
            if (key == null) return false;

            byte[] encryptedData = key.GetValue("FileData") as byte[];
            object totalPartsObj = key.GetValue("TotalParts");
            byte[] data = null;

            if (encryptedData != null)
            {
                data = DecryptData(encryptedData, Encoding.UTF8.GetBytes(hash.Substring(0, 8)));
            }
            else if (totalPartsObj is int)
            {
                int totalParts = (int)totalPartsObj;
                List<byte> list = new List<byte>();
                for (int i = 0; i < totalParts; i++)
                {
                    var part = key.GetValue("FileData_Part" + i) as byte[];
                    if (part == null) return false;
                    list.AddRange(part);
                }
                data = list.ToArray();
            }
            else return false;

            key.Close();

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllBytes(path, data);
            baseKey.DeleteSubKey(hash);
            return true;
        }

        byte[] DecryptData(byte[] encrypted, byte[] key)
        {
            byte[] output = new byte[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
                output[i] = (byte)(encrypted[i] ^ key[i % key.Length]);
            return output;
        }
    }
}
