namespace MSearch.Core
{
    /// <summary>
    /// Scan mode selected by the user in the main window.
    /// </summary>
    public enum ScanMode
    {
        Quick = 0,
        Full = 1,
        Selective = 2
    }

    /// <summary>
    /// Settings of one scan run.
    /// Options live only for the session and are not persisted: malware
    /// must not be able to disable scan stages permanently.
    /// </summary>
    public sealed class ScanRequest
    {
        public ScanMode Mode { get; set; }

        // Этапы проверки (true = этап включён)
        public bool ScanProcesses { get; set; }
        public bool ScanRegistry { get; set; }
        public bool ScanServices { get; set; }
        public bool ScanTasks { get; set; }
        public bool ScanFirewall { get; set; }
        public bool ScanHosts { get; set; }
        public bool ScanUserProfiles { get; set; }
        public bool ScanWmi { get; set; }
        public bool ScanRootkits { get; set; }
        public bool ScanSignatures { get; set; }
        public bool ScanFileSystem { get; set; }
        public bool ScanShellStartup { get; set; }

        public bool FullScan { get; set; }
        public int MaxDepth { get; set; }
        public string SelectedPath { get; set; }

        public bool ScanOnly { get; set; }
        public bool PauseBeforeCleanup { get; set; }
        public bool RemoveEmptyTasks { get; set; }
        public bool Verbose { get; set; }

        public ScanRequest()
        {
            Mode = ScanMode.Quick;
            ScanProcesses = true;
            ScanRegistry = true;
            ScanServices = true;
            ScanTasks = true;
            ScanFirewall = true;
            ScanHosts = true;
            ScanUserProfiles = true;
            ScanWmi = true;
            ScanRootkits = true;
            ScanSignatures = true;
            ScanFileSystem = true;
            ScanShellStartup = true;
            MaxDepth = 8;
        }

        /// <summary>Quick scan: all stages, standard depth.</summary>
        public static ScanRequest CreateQuick()
        {
            return new ScanRequest { Mode = ScanMode.Quick };
        }

        /// <summary>Full scan: all local drives, depth 16.</summary>
        public static ScanRequest CreateFull()
        {
            return new ScanRequest
            {
                Mode = ScanMode.Full,
                FullScan = true,
                MaxDepth = 16
            };
        }

        /// <summary>
        /// Selective scan: all stages enabled by default,
        /// no path set — the default set of directories is used.
        /// </summary>
        public static ScanRequest CreateSelective()
        {
            return new ScanRequest { Mode = ScanMode.Selective };
        }

        /// <summary>
        /// Applies the run settings to the global options read by scanners.
        /// </summary>
        public void Apply(LaunchOptions options)
        {
            if (options == null)
                return;

            options.fullScan = FullScan;
            options.maxSubfolders = MaxDepth;
            options.selectedPath = string.IsNullOrEmpty(SelectedPath) ? null : SelectedPath;

            options.no_runtime = !ScanProcesses;
            options.no_scan_registry = !ScanRegistry;
            options.no_services = !ScanServices;
            options.no_scan_tasks = !ScanTasks;
            options.no_firewall = !ScanFirewall;
            options.no_check_hosts = !ScanHosts;
            options.no_scan_users = !ScanUserProfiles;
            options.no_scan_wmi = !ScanWmi;
            options.no_scan_filesystem = !ScanFileSystem;
            options.no_shell_startup = !ScanShellStartup;

            // --scan-only в CLI всегда отключает проверку руткитов и удаление пустых задач.
            // Если приложение запущено с -so, GUI-запуски тоже остаются «только проверкой».
            // ScanOnlyFromCli отделяет флаг запуска от выбора текущей GUI-сессии.
            options.ScanOnly = options.ScanOnlyFromCli || ScanOnly;
            options.no_rootkit_check = !ScanRootkits || options.ScanOnly;
            options.RemoveEmptyTasks = RemoveEmptyTasks && !options.ScanOnly;

            options.nosignaturescan = !ScanSignatures;
            options.pause = PauseBeforeCleanup;
            options.verbose = Verbose;
        }
    }
}
