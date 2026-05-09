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
        public string LaunchCountValueName { get; }
        public string QuarantineKeyPath { get; }


        public bool WinPEMode { get; set; }
        public bool RunAsSystem { get; set; }
        public bool IsGuiAvailable { get; set; }
        public bool IsRedirectedInput { get; set; }

        public bool hasLockedObjectsByAV { get; set; }
        public bool hasEmptyTasks { get; set; }

        public int totalFoundThreats { get; set; }
        public int totalFoundSuspiciousObjects { get; set; }
        public int totalNeutralizedThreats { get; set; }

        public string quarantineListEnum { get; set; } = "";
        public string drive_letter { get; set; } = "C";
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
            LaunchCountValueName = "runcount";
            QuarantineKeyPath = Path.Combine(RegistryPathMain, "Quarantine");
            using (Process p = Process.GetCurrentProcess())
            {
                ExecutablePath = p.MainModule.FileName;
                CurrentVersion = FileVersionInfo.GetVersionInfo(ExecutablePath).ProductVersion;
            }
        }
    }
}
