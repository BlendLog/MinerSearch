using MSearch.Core;
using MSearch.Properties;
using System;
using System.IO;
using System.Resources;
using System.Text;

namespace MSearch
{
    public class LocalizedLogger
    {

        ResourceManager resourceManager = Resources.ResourceManager;

        #region TopRegion
        static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }

        internal static void LogSystemHeader(string winver, BootMode bootmode, string uacState, string scanMode)
        {
            string _winver = L("_Winver");
            string _bootmode = L("_BootMode");

            Logger.WriteLog($"\t\t{Decode(_winver)} {winver}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{uacState}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{_bootmode} {bootmode}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{L("_ScanMode")} {scanMode}\n", ConsoleColor.DarkGray, false);
        }

        internal static string GetScanModeTitle(ScanMode mode)
        {
            switch (mode)
            {
                case ScanMode.Full:
                    return L("_ScanMode_Full_Title");
                case ScanMode.Selective:
                    return L("_ScanMode_Selective_Title");
                default:
                    return L("_ScanMode_Quick_Title");
            }
        }

        internal static void LogStartupCount(int count)
        {
            string message = L("_StartupCount");

            Logger.WriteLog($"\t\t{message}: [{count}]", ConsoleColor.White, false);


        }

        #endregion

        #region Errors
        public static void LogError_СriticalServiceNotInstalled()
        {
            string message = L("_CriticalServiceNotInstalled");
            Logger.WriteLog($"\t[xxx] {new StringBuilder("wi").Append("nmg").Append("mt")}: {message}", Logger.error);
        }

        public static void LogInvalidFile(string filePath)
        {
            string message = L("_InvalidFile");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\t[x] {message}: {filePath}");
            Console.ResetColor();
        }
        #endregion

        public static void LogR00TkitPresent()
        {
            string message = L("_R00tkitPresent");

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Logger.WriteLog($"\t\t[!!!!] {Decode(message)}", ConsoleColor.White, false);
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public static void LogScanning(string processName, string processArgs = "")
        {
            string message = L("_Scanning");

            if (!LaunchOptions.GetInstance.verbose)
            {
                Logger.WriteLog($"{message} {processName}.exe", ConsoleColor.White);
            }
            else Logger.WriteLog($"{message} {processName}.exe | {processArgs}", ConsoleColor.White);


        }

        public static void LogNoThreatsFound()
        {
            string message = L("_NoThreats");
            Logger.WriteLog($"\t[#] {message}", ConsoleColor.Blue);
        }

        public static void LogSpecifyDrive()
        {
            string message = L("_SpecDrive");
            Console.Write($"\n\t\t{message} ");

        }

        public static void LogIncorrectDrive(string driveletter)
        {
            string message = L("_IncorrectDrive");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t[x] {message}: {driveletter}");
            Console.ResetColor();
        }

        public static void LogWinPEMode(string driveletter)
        {
            string message = L("_WinPEMode");
            Console.WriteLine($"\t\t[&] {message} {driveletter}:\\");

        }

        public static void LogUnknownCommand(string command)
        {
            string message = L("_UnknownCommand");
            Console.WriteLine($"\n{message} {command}");

        }

        public static void LogHelpHint()
        {
            string message = L("_HelpHint");
            Console.WriteLine($"\t\t{message}");

        }

        public static void LogPAUSE()
        {
            string message = L("_PAUSE");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public static void LogScanOnlyMode()
        {
            string message = L("_ScanOnlyMode");
            Logger.WriteLog($"\t[i] {message}", ConsoleColor.Blue);
        }

        public static void LogAnalyzingFile(string file)
        {
            string message = L("_AnalyzingFile");

            lock (Logger._logLock)
            {
                if (LaunchOptions.GetInstance.verbose)
                {
                    Logger.WriteLog($" {message}: {file}...", ConsoleColor.White);
                }
                else Console.WriteLine($" {message}: {file}...");
            }


        }

        public static void LogOK()
        {
            lock (Logger._logLock)
            {

                if (LaunchOptions.GetInstance.verbose)
                {
                    Logger.WriteLog("\t[OK]", ConsoleColor.DarkGreen, false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\t[OK]");
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }
        }

        private static volatile bool _inPlaceConsoleProgressAvailable = true;

        private static void WriteInPlaceConsoleProgress(string text, ConsoleColor color)
        {
            if (!_inPlaceConsoleProgressAvailable)
                return;

            try
            {
                Console.ForegroundColor = color;
                Console.CursorLeft = 0;
                Console.Write(text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                _inPlaceConsoleProgressAvailable = false;
            }
        }

        public static void LogSignatureScanProgress(int current, int total, string filePath)
        {
            lock (Logger._logLock)
            {
                string displayName = Path.GetFileName(filePath);

                // Обрезаем имя файла до 60 символов
                if (displayName.Length > 60)
                {
                    displayName = "..." + displayName.Substring(displayName.Length - 57);
                }

                WriteInPlaceConsoleProgress($"[{current,6}/{total}] {displayName,-60}", ConsoleColor.DarkGray);

                // Лог: полный путь (если verbose)
                if (LaunchOptions.GetInstance.verbose)
                {
                    Logger.WriteLog($"[{current}/{total}] {filePath}", ConsoleColor.Gray, true);
                }
            }
        }

        public static void LogSignatureScanSkipped(int current, int total, string filePath)
        {
            lock (Logger._logLock)
            {
                string displayName = Path.GetFileName(filePath);

                // Обрезаем имя файла до 60 символов
                if (displayName.Length > 60)
                {
                    displayName = "..." + displayName.Substring(displayName.Length - 57);
                }

                string skippedLabel = L("_ActionType_Skipped");

                WriteInPlaceConsoleProgress($"[~ {current,5}/{total}] {displayName,-60} ({skippedLabel})", ConsoleColor.Gray);

                if (LaunchOptions.GetInstance.verbose)
                {
                    Logger.WriteLog($"[~ {current}/{total}] {filePath} ({skippedLabel})", ConsoleColor.Gray, true);
                }
            }
        }

        private static string Decode(string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Replace("?", "");
        }

        public void LogMessage(string sign, string ResourceKey, string target, ConsoleColor consoleColor, bool DisplayTime = true)
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"{sign} {Decode(message)} {target}", consoleColor, DisplayTime);
        }

        public void LogJustDisplayMessage(string sign, string ResourceKey, string target, ConsoleColor consoleColor)
        {
            lock (Logger._logLock)
            {
                string message = GetLocalizedString(ResourceKey);
                Console.ForegroundColor = consoleColor;
                Console.Write($"{sign}{message} {target}");
                Console.ResetColor();
            }
        }

        public void LogHeadMessage(string ResourceKey)
        {
            string message = GetLocalizedString(ResourceKey);
            if (!AppConfig.GetInstance.RunAsSystem)
            {
                Logger.WriteLog($"\t\t{Decode(message)}", Logger.head, false);
            }
            else Logger.WriteLog($"\t\t{Decode(message)}", ConsoleColor.Magenta, false);
        }

        public void LogWarnMessage(string ResourceKey, string target = "")
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"\t[!] {Decode(message)} {target}", Logger.warn);
        }

        public void LogWarnMediumMessage(string ResourceKey, string target = "", string trigger = "")
        {
            string message = GetLocalizedString(ResourceKey);
            if (trigger == "")
            {
                Logger.WriteLog($"\t[!!] {Decode(message)} {target}", Logger.warnMedium);
            }
            else
            {
                Logger.WriteLog($"\t[!!] {target} \"{trigger}\" {Decode(message)}", Logger.warnMedium);
            }
        }

        public void LogCautionMessage(string ResourceKey, string subject = "")
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"\t[!!!] {Decode(message)} {subject}", Logger.caution);
        }

        public void LogSuccessMessage(string ResourceKey, string subject = "", string ResourceKeyAction = "")
        {
            string message = GetLocalizedString(ResourceKey);
            if (ResourceKeyAction == "")
            {
                Logger.WriteLog($"\t[+] {Decode(message)} {subject}", Logger.success);
            }
            else
            {
                string action = GetLocalizedString(ResourceKeyAction);
                Logger.WriteLog($"\t[+] {Decode(message)} {subject} {Decode(action)}", Logger.success);
            }
        }

        public void LogErrorMessage(string MessageResourceKey, Exception ex = null, string target = "", string targetType = "")
        {
            if (ex == null)
            {
                Logger.WriteLog($"\t[x] {Decode(GetLocalizedString(MessageResourceKey))} {target}", Logger.error);
                return;
            }

#if !DEBUG
            if (targetType != "")
            {
                Logger.WriteLog($"\t[x] {Decode(GetLocalizedString(MessageResourceKey))} {Decode(GetLocalizedString(targetType))} {target} | {ex.Message} | HR=0x{ex.HResult:X8}", Logger.error);
            }
            else
            {
                Logger.WriteLog($"\t[x] {Decode(GetLocalizedString(MessageResourceKey))} {target} | {ex.Message} | HR=0x{ex.HResult:X8}", Logger.error);
            }
#else
            if (targetType != "")
            {
                Logger.WriteLog($"\t[x] {Decode(GetLocalizedString(MessageResourceKey))} {Decode(GetLocalizedString(targetType))} {target} | {ex.Message} | {ex.StackTrace}", Logger.error);
            }
            else
            {
                Logger.WriteLog($"\t[x] {Decode(GetLocalizedString(MessageResourceKey))} {target} | {ex.Message} | {ex.StackTrace}", Logger.error);
            }
#endif
        }



        public void LogStatusMessage(string MessageResourceKey)
        {
            string message = GetLocalizedString(MessageResourceKey);
            Logger.WriteLog($"\t[#] {Decode(message)}", ConsoleColor.Blue);
        }

        internal string GetLocalizedString(string ResourceKey)
        {
            string message = string.Empty;
            switch (AppConfig.GetInstance.ActiveLanguage)
            {
                case "RU":
                    message = GetResourceString(ResourceKey + "_RU");
                    break;
                case "EN":
                    message = GetResourceString(ResourceKey + "_EN");
                    break;
            }
            return message;
        }

        string GetResourceString(string key)
        {
            return resourceManager.GetString(key) ?? $"[ResourceStringNotFound: {key}]";
        }


        public static void LogElapsedTime(string elapsedTime)
        {
            string message = L("_Elapse");
            Logger.WriteLog($"\t\t[$] {message} {elapsedTime}", ConsoleColor.White, false);
        }

        public static void LogTotalScanResult(int _totalThreats, int _neutralizedThreats, int _suspObjects, int _skippedThreats)
        {
            string totalThreats = L("_TotalThreatsFound");
            string neutralizedThreats = L("_TotalNeutralizedThreats");
            string totalSuspObj = L("_TotalSuspObjects");
            string skippedThreats = L("_TotalSkippedThreats");

            ConsoleColor color = ConsoleColor.White;

            if (_neutralizedThreats == _totalThreats) color = ConsoleColor.Green;
            if (_neutralizedThreats <= Math.Round(Convert.ToDouble(_neutralizedThreats / 2))) color = ConsoleColor.Yellow;

            Logger.WriteLog($"\t\t[&] {totalThreats} [{_totalThreats}]", ConsoleColor.Magenta, false);
            Logger.WriteLog($"\t\t[&] {totalSuspObj} [{_suspObjects}]", ConsoleColor.Yellow, false);
            if (!LaunchOptions.GetInstance.ScanOnly)
            {
                Logger.WriteLog($"\t\t[&] {neutralizedThreats} [{_neutralizedThreats}]", color, false);
                Logger.WriteLog($"\t\t[&] {skippedThreats} [{_skippedThreats}]", ConsoleColor.Gray, false);
            }

            Console.ResetColor();
        }

        public void LogWriteWithoutDisplay(bool NoDisplay, bool DisplayTime)
        {
            string text = GetLocalizedString("_LogLegend");

            Logger.WriteLog("\n\n" + text, NoDisplay, DisplayTime);
        }
    }
}
