namespace MSearch.Core
{
    /// <summary>
    /// Режим сканирования, выбранный пользователем в главном окне.
    /// </summary>
    public enum ScanMode
    {
        Quick = 0,
        Full = 1,
        Selective = 2
    }

    /// <summary>
    /// Настройки одного запуска сканирования.
    /// Опции живут только в рамках сессии и не сохраняются: вредоносное ПО
    /// не должно иметь возможности навсегда отключить этапы проверки.
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
            MaxDepth = 8;
        }

        /// <summary>Обычное сканирование: все этапы, стандартная глубина.</summary>
        public static ScanRequest CreateQuick()
        {
            return new ScanRequest { Mode = ScanMode.Quick };
        }

        /// <summary>Полное сканирование: все локальные диски, глубина 16.</summary>
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
        /// Выборочное сканирование: все этапы включены по умолчанию,
        /// путь не задан — используется стандартный набор каталогов.
        /// </summary>
        public static ScanRequest CreateSelective()
        {
            return new ScanRequest { Mode = ScanMode.Selective };
        }

        /// <summary>
        /// Переносит настройки запуска в глобальные опции, которые читают сканеры.
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

            // --scan-only в CLI всегда отключает проверку руткитов и удаление пустых задач.
            // Если приложение запущено с -so, GUI-запуски тоже остаются «только проверкой».
            bool effectiveScanOnly = options.ScanOnly || ScanOnly;
            options.ScanOnly = effectiveScanOnly;
            options.no_rootkit_check = !ScanRootkits || effectiveScanOnly;
            options.RemoveEmptyTasks = RemoveEmptyTasks && !effectiveScanOnly;

            options.nosignaturescan = !ScanSignatures;
            options.pause = PauseBeforeCleanup;
            options.verbose = Verbose;
        }
    }
}
