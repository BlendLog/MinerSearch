using Microsoft.Win32;
using MSearch;
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

        public string SelectedCustomPath { get; private set; } = string.Empty;

        public void Execute()
        {
            string[] args = Environment.GetCommandLineArgs();

            if ((LaunchOptions.GetInstance.QuarantineRestoreOption || LaunchOptions.GetInstance.QuarantineDeleteOption) && args.Length >= 3)
            {

                if (LaunchOptions.GetInstance.QuarantineRestoreOption && LaunchOptions.GetInstance.QuarantineDeleteOption)
                {
                    PrintError(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_InvlaidOptionSelection"));
                    return;
                }

                string targets = LaunchOptions.GetInstance.quarantineListEnum;

                if (!LaunchOptions.GetInstance.Force && LaunchOptions.GetInstance.QuarantineRestoreOption)
                {
                    PrintError(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_ForceRequired"));
                    return;
                }

                var indexes = IndexParse(targets, int.MaxValue);
                if (indexes.Count == 0)
                {
                    PrintError(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_IndexListInvalid"));
                    return;
                }

                RunNonInteractive(indexes, LaunchOptions.GetInstance.QuarantineDeleteOption);
            }
            else
            {
                ShowQuarantineList();
                InteractiveQuarantineMenu();
            }
        }

        public void ShowQuarantineList()
        {
            var entries = QuarantineManager.List();

            if (entries.Count == 0)
            {
                PrintInfo(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            Console.WriteLine(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_Title"));
            Console.WriteLine();

            string path = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Path");
            string hash = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_FileHash");
            string size = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_FileSize");
            string type = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Type");

            Console.WriteLine($"Index | {path,-35} | {hash,-6} | {size,-8} | {type}");
            Console.WriteLine(new string('-', 80));
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                string typeLabel = GetTypeLabel(e.ItemType);
                Console.WriteLine(string.Format("{0,5} | {1,-35} | {2}... | {3,-8} | {4}", i + 1, Shorten(e.OriginalPath, 35), e.FileHash.Substring(0, 6), e.FileSize, typeLabel));
            }
        }

        public void RunNonInteractive(List<int> indexes, bool isDelete)
        {
            var entries = QuarantineManager.List();

            if (entries.Count == 0)
            {
                PrintInfo(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            var affected = new List<string>();
            var failed = new List<string>();

            indexes.Sort();
            indexes.Reverse();

            foreach (int i in indexes)
            {
                if (i < 0 || i >= entries.Count)
                    continue;

                var item = entries[i];
                bool ok = false;

                if (isDelete)
                {
                    ok = QuarantineManager.Delete(item.FileHash);
                }
                else
                {
                    switch (item.ItemType)
                    {
                        case QuarantineItemType.Service:
                            ok = QuarantineManager.RestoreService(item.FileHash);
                            break;
                        case QuarantineItemType.Task:
                            ok = QuarantineManager.RestoreTask(item.FileHash);
                            break;
                        case QuarantineItemType.Registry:
                            ok = QuarantineManager.RestoreRegistry(item.FileHash);
                            break;
                        default:
                            ok = QuarantineManager.RestoreFile(item.FileHash, item.OriginalPath);
                            break;
                    }
                }

                if (ok)
                    affected.Add(item.OriginalPath);
                else
                    failed.Add(item.OriginalPath);
            }

            if (affected.Count > 0)
            {
                Console.WriteLine((isDelete ? AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileDeleted") : AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileRestored")).Replace("#COUNT#", affected.Count.ToString()));
                foreach (string p in affected)
                {
                    Console.WriteLine("  - " + p);
                }
            }

            if (failed.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileFailed").Replace("#COUNT#", failed.Count.ToString()));
                foreach (string p in failed)
                {
                    Console.WriteLine("  - " + p);
                }
            }
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

        void InteractiveQuarantineMenu()
        {
            var entries = QuarantineManager.List();
            if (entries.Count == 0)
            {
                PrintInfo(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            Console.WriteLine();
            Console.Write(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_EnterIndexes") + " > ");
            string indexesInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(indexesInput))
                return;

            var indexes = IndexParse(indexesInput, entries.Count);
            if (indexes.Count == 0)
            {
                PrintError(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_IndexListInvalid"));
                return;
            }

            Console.WriteLine();
            Console.WriteLine(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_ActionHint"));
            Console.Write("  > ");

            string input = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(input))
                return;

            bool isDelete = input == "d" || input == "del" || input == "delete";
            bool isRestore = input == "r" || input == "res" || input == "restore";

            if (!isDelete && !isRestore)
            {
                PrintWarning(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_NoAction"));
                return;
            }

            if (isRestore)
            {
                PromptRestoreDestination();
            }

            RunInteractive(indexes, isDelete);
        }

        void PromptRestoreDestination()
        {
            Console.WriteLine();
            Console.WriteLine("1)" + AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionOriginalPath"));
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("  " + AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionOriginalDesc"));
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("2)" + AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionCustomPath"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionCustomDesc"));
            Console.WriteLine();
            Console.ResetColor();
            Console.Write(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_DestinationChoice") + " [1/2]: ");

            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                SelectedCustomPath = string.Empty;
                return;
            }
            if (input == "1")
            {
                SelectedCustomPath = string.Empty;
            }
            if (input == "2")
            {
                Console.Write(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_EnterPath") + " > ");
                SelectedCustomPath = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(SelectedCustomPath) || !Directory.Exists(SelectedCustomPath))
                {
                    PrintWarning(AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormPathInvalid"));
                    SelectedCustomPath = string.Empty;
                }
            }
            else
            {
                SelectedCustomPath = string.Empty;
            }
        }

        void RunInteractive(List<int> indexes, bool isDelete)
        {
            var entries = QuarantineManager.List();

            if (entries.Count == 0)
            {
                PrintInfo(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_Empty"));
                return;
            }

            var affected = new List<string>();
            var failed = new List<string>();

            indexes.Sort();
            indexes.Reverse();

            foreach (int i in indexes)
            {
                if (i < 0 || i >= entries.Count)
                    continue;

                var item = entries[i];
                string restorePath = item.OriginalPath;

                if (!isDelete && !string.IsNullOrEmpty(SelectedCustomPath) && item.ItemType == QuarantineItemType.File)
                {
                    restorePath = Path.Combine(SelectedCustomPath, Path.GetFileName(item.OriginalPath));
                }

                bool ok = false;

                if (isDelete)
                {
                    ok = QuarantineManager.Delete(item.FileHash);
                }
                else
                {
                    switch (item.ItemType)
                    {
                        case QuarantineItemType.Service:
                            ok = QuarantineManager.RestoreService(item.FileHash);
                            break;
                        case QuarantineItemType.Task:
                            ok = QuarantineManager.RestoreTask(item.FileHash);
                            break;
                        case QuarantineItemType.Registry:
                            ok = QuarantineManager.RestoreRegistry(item.FileHash);
                            break;
                        default:
                            ok = QuarantineManager.RestoreFile(item.FileHash, restorePath);
                            break;
                    }
                }

                if (ok)
                    affected.Add(restorePath);
                else
                    failed.Add(restorePath);
            }

            if (affected.Count > 0)
            {
                Console.WriteLine((isDelete ? AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileDeleted") : AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileRestored")).Replace("#COUNT#", affected.Count.ToString()));
                foreach (string p in affected)
                {
                    Console.WriteLine("  - " + p);
                }
            }

            if (failed.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_FileFailed").Replace("#COUNT#", failed.Count.ToString()));
                foreach (string p in failed)
                {
                    Console.WriteLine("  - " + p);
                }
            }

            if (Environment.UserInteractive)
            {
                Console.ReadLine();
            }
        }

        string GetTypeLabel(QuarantineItemType type)
        {
            switch (type)
            {
                case QuarantineItemType.Service:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_Service");
                case QuarantineItemType.Task:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_Task");
                case QuarantineItemType.Registry:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_Registry");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_File");
            }
        }

        void PrintInfo(string msg)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[INFO] " + msg);
            Console.ForegroundColor = original;
        }

        void PrintError(string msg)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + msg);
            Console.ForegroundColor = original;
        }

        void PrintWarning(string msg)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARN] " + msg);
            Console.ForegroundColor = original;
        }
    }
}
