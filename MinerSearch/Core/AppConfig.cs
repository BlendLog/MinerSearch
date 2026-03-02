using System.Diagnostics;
using System.IO;
//using System.Reflection;
using System.Text;

namespace MSearch.Core
{
    public class AppConfig
    {
        static volatile AppConfig _instance = new AppConfig();

        public static AppConfig Instance => _instance;

        public string RegistryPathMain { get; }
        public string StatisticsValueName { get; }
        public string LaunchCountValueName { get; }
        public string QuarantineKeyPath { get; }

        public bool accept_eula { get; set; }
        public bool no_logs { get; set; }
        public bool no_scantime { get; set; }
        public bool no_runtime { get; set; }
        public bool no_services { get; set; }
        public bool no_scan_tasks { get; set; }
        public bool no_firewall { get; set; }
        public bool pause { get; set; }
        public bool help { get; set; }
        public bool RemoveEmptyTasks { get; set; }
        public bool nosignaturescan { get; set; }
        public bool WinPEMode { get; set; }
        public bool NoRootkitCheck { get; set; }
        public bool ScanOnly { get; set; }
        public bool fullScan { get; set; }
        public bool RestoredWMI { get; set; }
        public bool RunAsSystem { get; set; }
        public bool verbose { get; set; }
        public bool silent { get; set; }
        public bool console_mode { get; set; }
        public bool IsGuiAvailable { get; set; }
        public bool IsRedirectedInput { get; set; }
        public bool no_check_hosts { get; set; }
        public bool demandSelection { get; set; }
        public bool hasLockedObjectsByAV { get; set; }
        public bool hasEmptyTasks { get; set; }
        public bool noScanWmi { get; set; }
        public int maxSubfolders { get; set; } = 8;
        public int totalFoundThreats { get; set; }
        public int totalFoundSuspiciousObjects { get; set; }
        public int totalNeutralizedThreats { get; set; }
        public bool QuarantineMode { get; set; }
        public bool QuarantineRestoreOption { get; set; }
        public bool QuarantineDeleteOption { get; set; }
        public bool Force { get; set; }
        public string quarantineListEnum { get; set; } = "";
        public string drive_letter { get; set; } = "C";
        public string selectedPath { get; set; } = "";
        public string ActiveLanguage { get; set; } = "EN";
        public string _title { get; } = new StringBuilder("Mi").Append("ner").Append("Sea").Append("rch").ToString();
        public BootMode bootMode { get; } = OSExtensions.GetBootMode();

        public LocalizedLogger LL { get; } = new LocalizedLogger();
        public Utils _utils { get; } = new Utils();
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
