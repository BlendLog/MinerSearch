using MSearch.Properties;
using System;
using System.Reflection;
using System.Resources;

namespace MSearch
{
    public class LocalizedLogger
    {

        ResourceManager resourceManager = new ResourceManager("M$Sear$ch.Properties.Resources".Replace("$", ""), Assembly.GetExecutingAssembly());

        #region TopRegion
        internal static void LogPCInfo(string winver, string username, string pcname, BootMode bootmode)
        {
            string _winver = Resources._Winver_EN;
            string _username = Resources._Username_EN;
            string _pcname = Resources._PCName_EN;
            string _bootmode = Resources._BootMode_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    _winver = Resources._Winver_RU;
                    _username = Resources._Username_RU;
                    _pcname = Resources._PCName_RU;
                    _bootmode = Resources._BootMode_RU;
                    break;
                case "EN":
                    _winver = Resources._Winver_EN;
                    _username = Resources._Username_EN;
                    _pcname = Resources._PCName_EN;
                    _bootmode = Resources._BootMode_EN;
                    break;
            }

            Logger.WriteLog($"\t\t{_winver} {winver}".Replace("?", ""), ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{_username} {username}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{_pcname} {pcname}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\t{_bootmode} {bootmode}\n", ConsoleColor.DarkGray, false);
        }

        internal static void LogStartupCount(int count)
        {
            string message = Resources._StartupCount_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._StartupCount_RU;
                    break;
                case "EN":
                    message = Resources._StartupCount_EN;
                    break;
            }

            Logger.WriteLog($"\t\t{message}: {count}", ConsoleColor.White, false);


        }

        #endregion

        #region Errors
        public static void LogError_СriticalServiceNotInstalled()
        {
            string message = Resources._CriticalServiceNotInstalled_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._CriticalServiceNotInstalled_RU;
                    break;
                case "EN":
                    message = Resources._CriticalServiceNotInstalled_EN;
                    break;
            }
            Logger.WriteLog($"\t[xxx] wi~nmg~mt: {message}".Replace("~", ""), Logger.error);
        }

        public static void LogInvalidFile(string filePath)
        {
            string message = Resources._InvalidFile_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._InvalidFile_RU;
                    break;
                case "EN":
                    message = Resources._InvalidFile_EN;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\t[x] {message}: {filePath}");
            Console.ResetColor();
            Console.ReadKey();
        }
        #endregion

        public static void LogR00TkitPresent()
        {
            string message = Resources._R00tkitPresent_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._R00tkitPresent_RU;
                    break;
                case "EN":
                    message = Resources._R00tkitPresent_EN;
                    break;
            }

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Logger.WriteLog($"\t[!!!!] {message}".Replace("?", ""), ConsoleColor.White, false);
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public static void LogScanning(string processName)
        {
            string message = Resources._Scanning_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._Scanning_RU;
                    break;
                case "EN":
                    message = Resources._Scanning_EN;
                    break;
            }

            Logger.WriteLog($"{message} {processName}.exe", ConsoleColor.White);


        }

        public static void LogNoThreatsFound()
        {
            string message = Resources._NoThreats_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._NoThreats_RU;
                    break;
                case "EN":
                    message = Resources._NoThreats_EN;
                    break;
            }
            Logger.WriteLog($"\t[#] {message}", ConsoleColor.Blue);
        }

        public static void LogSpecifyDrive()
        {
            string message = Resources._SpecDrive_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._SpecDrive_RU;
                    break;
                case "EN":
                    message = Resources._SpecDrive_EN;
                    break;
            }
            Console.Write($"\n\t\t{message}");

        }

        public static void LogIncorrectDrive(string driveletter)
        {
            string message = Resources._IncorrectDrive_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._IncorrectDrive_RU;
                    break;
                case "EN":
                    message = Resources._IncorrectDrive_EN;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t[x] {message}: {driveletter}");
            Console.ResetColor();
        }

        public static void LogWinPEMode(string driveletter)
        {
            string message = Resources._WinPEMode_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._WinPEMode_RU;
                    break;
                case "EN":
                    message = Resources._WinPEMode_EN;
                    break;
            }
            Console.WriteLine($"\t\t[&] {message} {driveletter}:\\");

        }

        public static void LogUnknownCommand(string command)
        {
            string message = Resources._UnknownCommand_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._UnknownCommand_RU;
                    break;
                case "EN":
                    message = Resources._UnknownCommand_EN;
                    break;
            }
            Console.WriteLine($"\n{message} {command}");

        }

        public static void LogHelpHint()
        {
            string message = Resources._HelpHint_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._HelpHint_RU;
                    break;
                case "EN":
                    message = Resources._HelpHint_EN;
                    break;
            }
            Console.WriteLine($"\t\t{message}");

        }

        public static void LogErrorDisabledScan()
        {
            string message = Resources._ErrorDisabledScan_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._ErrorDisabledScan_RU;
                    break;
                case "EN":
                    message = Resources._ErrorDisabledScan_EN;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t{message}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public static void LogPAUSE()
        {
            string message = Resources._PAUSE_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._PAUSE_RU;
                    break;
                case "EN":
                    message = Resources._PAUSE_EN;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;

        }

        public static void LogScanOnlyMode()
        {
            string message = Resources._ScanOnlyMode_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._ScanOnlyMode_RU;
                    break;
                case "EN":
                    message = Resources._ScanOnlyMode_EN;
                    break;
            }
            Logger.WriteLog($"\t[i] {message}", ConsoleColor.Blue);
        }

        public static void LogRestoredFile(string filePath)
        {
            string message = Resources._RestoredFile_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._RestoredFile_RU;
                    break;
                case "EN":
                    message = Resources._RestoredFile_EN;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t[+] {message}: {filePath}");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void LogAnalyzingFile(string file)
        {
            string message = Resources._AnalyzingFile_EN;
            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._AnalyzingFile_RU;
                    break;
                case "EN":
                    message = Resources._AnalyzingFile_EN;
                    break;
            }
            Console.WriteLine($" {message}: {file}...");
        }

        //-------------------------------------------------
        public void LogMessage(string sign, string ResourceKey, string target, ConsoleColor consoleColor, bool DisplayTime = true)
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"{sign} {message} {target}".Replace("?", ""), consoleColor, DisplayTime);
        }

        public void LogJustDisplayMessage(string sign, string ResourceKey, string target, ConsoleColor consoleColor)
        {
            string message = GetLocalizedString(ResourceKey);
            Console.ForegroundColor = consoleColor;
            Console.Write($"{sign}{message} {target}");
            Console.ResetColor();
        }

        public void LogHeadMessage(string ResourceKey)
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"\t\t{message}".Replace("?", ""), Logger.head, false);
        }

        public void LogWarnMessage(string ResourceKey, string target = "")
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"\t[!] {message} {target}".Replace("?", ""), Logger.warn);
        }

        public void LogWarnMediumMessage(string ResourceKey, string target = "", string trigger = "")
        {
            string message = GetLocalizedString(ResourceKey);
            if (trigger == "")
            {
                Logger.WriteLog($"\t[!!] {message} {target}".Replace("?", ""), Logger.warnMedium);
            }
            else
            {
                Logger.WriteLog($"\t[!!] {target} \"{trigger}\" {message}".Replace("?", ""), Logger.warnMedium);
            }
        }

        public void LogCautionMessage(string ResourceKey, string subject = "")
        {
            string message = GetLocalizedString(ResourceKey);
            Logger.WriteLog($"\t[!!!] {message} {subject}".Replace("?", ""), Logger.caution);
        }

        public void LogSuccessMessage(string ResourceKey, string subject = "", string ResourceKeyAction = "")
        {
            string message = GetLocalizedString(ResourceKey);
            if (ResourceKeyAction == "")
            {
                Logger.WriteLog($"\t[+] {message} {subject}".Replace("?", ""), Logger.success);
            }
            else
            {
                string action = GetLocalizedString(ResourceKeyAction);
                Logger.WriteLog($"\t[+] {message} {subject} {action}".Replace("?", ""), Logger.success);
            }
        }

        public void LogErrorMessage(string MessageResourceKey, Exception ex, string target = "", string targetType = "")
        {
            if (targetType != "")
            {
                Logger.WriteLog($"\t[x] {GetLocalizedString(MessageResourceKey)} {GetLocalizedString(targetType)} {target} | {ex.Message}".Replace("?", ""), Logger.error);
            }
            else
            {
                Logger.WriteLog($"\t[x] {GetLocalizedString(MessageResourceKey)} {target} | {ex.Message}".Replace("?", ""), Logger.error);
            }
        }

        public void LogStatusMessage(string MessageResourceKey)
        {
            string message = GetLocalizedString(MessageResourceKey);
            Logger.WriteLog($"\t[#] {message}".Replace("?", ""), ConsoleColor.Blue);
        }

        internal string GetLocalizedString(string ResourceKey)
        {
            string message = string.Empty;
            switch (Program.ActiveLanguage)
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


        //--------------------------------------------------

        public static void LogElapsedTime(string elapsedTime)
        {
            string message = Resources._Elapse_EN;

            switch (Program.ActiveLanguage)
            {
                case "RU":
                    message = Resources._Elapse_RU;
                    break;
                case "EN":
                    message = Resources._Elapse_EN;
                    break;
            }
            Logger.WriteLog($"\t\t[$] {message}: {elapsedTime}", ConsoleColor.White, false);
        }

        public static void LogTotalScanResult(int _totalThreats, int _neutralizedThreats, int _suspObjects)
        {
            string totalThreats = Resources._TotalThreatsFound_EN;
            string neutralizedThreats = Resources._TotalNeutralizedThreats_EN;
            string totalSuspObj = Resources._TotalSuspObjects_EN;

            switch (Program.ActiveLanguage)
            {
                case "RU":
                    totalThreats = Resources._TotalThreatsFound_RU;
                    neutralizedThreats = Resources._TotalNeutralizedThreats_RU;
                    totalSuspObj = Resources._TotalSuspObjects_RU;
                    break;
                case "EN":
                    totalThreats = Resources._TotalThreatsFound_EN;
                    neutralizedThreats = Resources._TotalNeutralizedThreats_EN;
                    totalSuspObj = Resources._TotalSuspObjects_EN;
                    break;
            }

            ConsoleColor color = ConsoleColor.White;

            if (_neutralizedThreats == _totalThreats) color = ConsoleColor.Green;
            if (_neutralizedThreats <= Math.Round(Convert.ToDouble(_neutralizedThreats / 2))) color = ConsoleColor.Yellow;

            Logger.WriteLog($"\t\t[&] {totalThreats} {_totalThreats}", ConsoleColor.Magenta, false);
            Logger.WriteLog($"\t\t[&] {totalSuspObj} {_suspObjects}", ConsoleColor.Yellow, false);
            if (!Program.ScanOnly)
            {
                Logger.WriteLog($"\t\t[&] {neutralizedThreats} {_neutralizedThreats}", color, false);
            }

            Console.ResetColor();
        }

        public void LogWriteWithoutDisplay(bool NoDisplay, bool DisplayTime)
        {
            string text = Resources._LogLegend_EN;

            switch (Program.ActiveLanguage)
            {
                case "RU":
                    text = Resources._LogLegend_RU;
                    break;
                case "EN":
                    text = Resources._LogLegend_EN;
                    break;
            }

            Logger.WriteLog("\n\n" + text, NoDisplay, DisplayTime);
        }
    }
}
