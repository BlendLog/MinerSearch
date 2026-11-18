using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MSearch.Core
{
    public class AppConfig
    {
        static volatile AppConfig _instance;
        static readonly object _lock = new object();

        public static AppConfig GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppConfig();
                        }
                    }
                }
                return _instance;
            }
        }

        public string RegistryPathMain { get; }
        public string StatisticsValueName { get; }
        public string AutoAcceptUnknownValueName { get; }
        public string LaunchCountValueName { get; }
        public string QuarantineKeyPath { get; }


        public bool WinPEMode { get; set; }
        public bool RunAsSystem { get; set; }
        public bool IsGuiAvailable { get; set; }
        public bool IsRedirectedInput { get; set; }

        public int totalFoundThreats { get; set; }
        public int totalFoundSuspiciousObjects { get; set; }
        public int totalNeutralizedThreats { get; set; }
        public int userSkippedThreats { get; set; }

        public string drive_letter { get; set; } = GetSystemDriveLetter();
        public string ActiveLanguage { get; set; } = "EN";
        public string _title { get; } = new StringBuilder("Mi").Append("ner").Append("Sea").Append("rch").ToString();
        public BootMode bootMode { get; } = OSExtensions.GetBootMode();

        public LocalizedLogger LL { get; } = new LocalizedLogger();
        public string ExecutablePath { get; }
        public string CurrentVersion { get; }

        public int RunCount { get; set; } = 0;

        AppConfig()
        {
            RegistryPathMain = @"Software\M1nerSearch";
            StatisticsValueName = "allowstatistics";
            AutoAcceptUnknownValueName = "autoacceptunknown";
            LaunchCountValueName = "runcount";
            QuarantineKeyPath = Path.Combine(RegistryPathMain, "Quarantine");
            using (Process p = Process.GetCurrentProcess())
            {
                ExecutablePath = p.MainModule.FileName;
                CurrentVersion = FileVersionInfo.GetVersionInfo(ExecutablePath).ProductVersion;
            }
        }

        internal static string GetSystemDriveLetter()
        {
            string systemDrive = Environment.GetEnvironmentVariable("SYSTEMDRIVE");
            if (!string.IsNullOrEmpty(systemDrive) && char.IsLetter(systemDrive[0]))
                return char.ToUpperInvariant(systemDrive[0]).ToString();

            string root = Path.GetPathRoot(Environment.SystemDirectory);
            if (!string.IsNullOrEmpty(root) && char.IsLetter(root[0]))
                return char.ToUpperInvariant(root[0]).ToString();

            return "C";
        }

        internal static int ReadDword(RegistryKey hive, string path, string valueName)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path))
                {
                    if (key == null)
                        return -1;

                    object value = key.GetValue(valueName);
                    return value == null ? -1 : (int)value;
                }
            }
            catch
            {
                return -1;
            }
        }

        internal static void WriteDword(RegistryKey hive, string path, string valueName, int value)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path, true) ?? hive.CreateSubKey(path))
                {
                    if (key != null)
                        key.SetValue(valueName, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }
    }
}
