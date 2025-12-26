using System.Reflection;
using System.Text;

namespace MSearch.Core
{
    public sealed class AppConfig
    {
        static readonly AppConfig _instance = new AppConfig();

        public static AppConfig Instance => _instance;

        AppConfig() { }

        public bool accept_eula { get; set; } = false;
        public bool no_logs { get; set; } = false;
        public bool no_scantime { get; set; } = false;
        public bool no_runtime { get; set; } = false;
        public bool no_services { get; set; } = false;
        public bool no_scan_tasks { get; set; } = false;
        public bool no_firewall { get; set; } = false;
        public bool pause { get; set; } = false;
        public bool help { get; set; } = false;
        public bool RemoveEmptyTasks { get; set; } = false;
        public bool nosignaturescan { get; set; } = false;
        public bool WinPEMode { get; set; } = false;
        public bool NoRootkitCheck { get; set; } = false;
        public bool ScanOnly { get; set; } = false;
        public bool fullScan { get; set; } = false;
        public bool RestoredWMI { get; set; } = false;
        public bool RunAsSystem { get; set; } = false;
        public bool verbose { get; set; } = false;
        public bool silent { get; set; } = false;
        public bool console_mode { get; set; } = false;
        public bool IsGuiAvailable { get; set; } = false;
        public bool IsRedirectedInput { get; set; } = false;
        public bool no_check_hosts { get; set; } = false;
        public bool demandSelection { get; set; } = false;
        public bool hasLockedObjectsByAV { get; set; } = false;
        public bool hasEmptyTasks { get; set; } = false;
        public int maxSubfolders { get; set; } = 8;
        public int totalFoundThreats { get; set; }
        public int totalFoundSuspiciousObjects { get; set; }
        public int totalNeutralizedThreats { get; set; }
        public bool QuarantineMode { get; set; } = false;
        public bool QuarantineRestoreOption { get; set; } = false;
        public bool QuarantineDeleteOption { get; set; } = false;
        public bool Force { get; set; } = false;
        public string quarantineListEnum { get; set; } = ""; 
        public string drive_letter { get; set; } = "C";
        public string selectedPath { get; set; } = "";
        public string ActiveLanguage { get; set; } = "EN";
        public string _title { get; } = new StringBuilder("Mi").Append("ner").Append("Sea").Append("rch").ToString();
        public BootMode bootMode { get; } = OSExtensions.GetBootMode();

        public LocalizedLogger LL { get; } = new LocalizedLogger();
        public Utils _utils { get; } = new Utils();
        public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public int RunCount { get; set; } = 0;
    }
}
