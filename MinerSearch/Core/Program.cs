using DBase;
using Microsoft.Win32;
using MSearch.Core;
using MSearch.Properties;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32Wrapper;

namespace MSearch
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
            Application.ThreadException += new ThreadExceptionEventHandler(ExeptionHandler.HookThreadException);
            try
            {
                Run(args);
            }
            catch (FileNotFoundException notfoundEx)
            {
                ShowMissingDependencyError(notfoundEx.FileName);
                Environment.Exit(1);
            }
#else
            Run(args);
#endif
        }

        static void Run(string[] args)
        {
            CheckDependencies();

            LaunchOptions _options = LaunchOptions.GetInstance;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (HasConsoleArgument(args) || Native.GetConsoleWindow() != IntPtr.Zero)
                ConsoleBridge.EnsureConsole();

            LaunchOptions.ParseArgs(args);

            bool stdioRedirected = ConsoleBridge.HasRedirectedStdio();
            bool interactive = Environment.UserInteractive;


#if DEBUG
            _options.console_mode = _options.console_mode || !interactive;
#else
            _options.console_mode = _options.console_mode || stdioRedirected || !interactive;
#endif

            bool consoleFlags = _options.console_mode || _options.winpemode || _options.help;
            _options.gui_mode = interactive && !consoleFlags && !_options.silent;

            if (!_options.gui_mode && !_options.silent)
                ConsoleBridge.EnsureConsole();

            if (_options.hasErrors)
            {
                foreach (var error in _options.errors)
                {
                    DialogDispatcher.Show(error, AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Environment.Exit(1);
            }

            ConsoleBridge.SafeSetTitle(Utils.GetRndString());

            AppConfig.GetInstance.RunAsSystem = ProcessManager.IsSystemProcess(Process.GetCurrentProcess().Id);
            AppConfig.GetInstance.IsGuiAvailable = interactive;
            AppConfig.GetInstance.IsRedirectedInput = stdioRedirected;

            Logger.InitLogger(_options.no_logs);
#if !DEBUG
            AppConfig.GetInstance.ActiveLanguage = AppConfig.GetInstance.IsGuiAvailable ? LanguageManager.LoadLanguageSetting() : "EN";

            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.GetInstance.ActiveLanguage);
            }

            Init(args);
#else

            AppConfig.GetInstance.ActiveLanguage = LanguageManager.LoadLanguageSetting();
            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.GetInstance.ActiveLanguage);
            }
            Init(args);

#endif
        }

        static void CheckDependencies()
        {
            string[] required = { "dbase.dll", "netlib.dll", "Win32Wrapper.dll" };
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string file in required)
            {
                if (!File.Exists(Path.Combine(directory, file)))
                    throw new FileNotFoundException(null, file);
            }
        }

        static void ShowMissingDependencyError(string fileName)
        {
            string message;
            try
            {
                string language = "EN";
                try
                {
                    if (File.Exists(LanguageManager.CfgFile))
                    {
                        string code = File.ReadAllText(LanguageManager.CfgFile).Trim();
                        if (code == "RU" || code == "EN")
                            language = code;
                    }
                }
                catch { }

                message = language == "RU" ? Resources._ErrorNotFoundComponent_RU : Resources._ErrorNotFoundComponent_EN;
            }
            catch
            {
                message = "Required application component is missing";
            }

            message += "\n\n" + fileName;

            try
            {
                if (Environment.UserInteractive)
                    MessageBox.Show(message, "MinerSearch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    Console.WriteLine(message);
            }
            catch { }
        }

        static void Init(string[] args)
        {
            LaunchOptions _options = LaunchOptions.GetInstance;

            Utils.mutex = new Mutex(false, Utils.GetDeviceIdLockName());
            Utils.rebootMtx = new Mutex(false, Utils.GetDeviceIdRebootName());

            var envResult = EnvironmentChecker.Check();

            if (!envResult.DotNetInstalled)
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNoDotNet"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            if (!envResult.CanRunSingleInstance)
            {
                if (!_options.silent)
                {
                    DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_AppAlreadyRunning"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!envResult.IsRebootRequired)
            {
                Utils.mutex.ReleaseMutex();
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_RebootRequired"), AppConfig.GetInstance._title);
                return;
            }

            if (envResult.IsWinPE)
            {
                List<string> newargs = new List<string> { "--winpemode" };
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                        newargs.Add(arg);
                }
                args = newargs.ToArray();
                AppConfig.GetInstance.WinPEMode = true;

                // В WinPE GUI недоступен — работаем через консоль.
                _options.gui_mode = false;
                ConsoleBridge.EnsureConsole();
            }

            ProcessManager.SetSmallWindowIconRandomHash(Process.GetCurrentProcess().MainWindowHandle);

            if (!_options.gui_mode && !_options.silent)
            {
                if (!ProcessManager.IsPwshAsParentProcess(Process.GetCurrentProcess().Id))
                {
                    TryInitConsoleWindow();
                    WaterMark();
                }
            }

            if (envResult.IsStartedFromArchive)
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn"), AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            UnlockObjectClass.EnsureOwnSettingsKeyAccessible();

            if (!EnvironmentChecker.PromptEula(AppConfig.GetInstance))
            {
                return;
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(AppConfig.GetInstance.ExecutablePath));

            if (_options.help)
            {
                _options.help = true;

                Console.ForegroundColor = ConsoleColor.White;

                if (AppConfig.GetInstance.ActiveLanguage == "EN")
                {
                    Console.Write(Resources._Help_EN);
                }
                else if (AppConfig.GetInstance.ActiveLanguage == "RU")
                {
                    Console.Write(Resources._Help_RU);
                }
                else
                {
                    Console.Write(Resources._Help_EN);
                }

                if (AppConfig.GetInstance.IsGuiAvailable)
                {
                    ConsoleBridge.SafeReadKey();
                }
                return;
            }

            if (_options.ScanOnly)
            {
                _options.no_rootkit_check = true;
                _options.RemoveEmptyTasks = false;
            }

            // WinPE: prompt for the target drive letter
            if (_options.winpemode && !_options.console_mode)
            {
                string driveLetter = EnvironmentChecker.PromptWinPEDriveLetter(AppConfig.GetInstance);
                AppConfig.GetInstance.drive_letter = driveLetter;
                Drive.Letter = driveLetter;
                _options.no_runtime = true;
                _options.no_rootkit_check = true;
                _options.no_services = true;
                _options.no_scan_tasks = true;
                _options.no_scan_registry = true;
                _options.no_check_hosts = true;
                _options.no_firewall = true;
                _options.no_scan_users = true;
                _options.no_scan_wmi = true;

                AppConfig.GetInstance.WinPEMode = true;
                LocalizedLogger.LogWinPEMode(driveLetter);
            }

            if (!AppConfig.GetInstance.WinPEMode)
            {
                AppConfig.GetInstance.drive_letter = Environment.GetEnvironmentVariable("SYSTEMDRIVE").Remove(1);
                Drive.Letter = AppConfig.GetInstance.drive_letter;
            }

            // Warn when running as SYSTEM
            if (envResult.IsSystemProcess && !AppConfig.GetInstance.WinPEMode)
            {
                if (!_options.silent && !_options.Force)
                {
                    if (_options.gui_mode)
                    {
                        var msg = EnvironmentChecker.PromptRunAsSystemWarning(AppConfig.GetInstance, true);
                        if (msg != DialogResult.Yes)
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                else if (_options.silent)
                {
                    ConsoleBridge.Hide();
                }
            }

            //#if !DEBUG
            if (!_options.help && !_options.gui_mode)
            {
                Utils.SwitchMouseSelection();
            }

            if (!_options.silent)
            {
                AppConfig.GetInstance.LL.LogJustDisplayMessage("\t\t", $"_RelevantVer", "https://blendlog.github.io \n", ConsoleColor.White);
            }

            //#endif
            Logger.StartHeaderCapture();
            Logger.WriteLog("  \t  \tID:\u00A0" + DeviceIdProvider.GetDeviceId(), ConsoleColor.White, false, true);

            if (!_options.help && !_options.silent)
            {
                LocalizedLogger.LogHelpHint();
            }

            AppConfig.GetInstance.LL.LogMessage("\t\t", "_Version", AppConfig.GetInstance.CurrentVersion, ConsoleColor.White, false);

            //#if !DEBUG
            if (!AppConfig.GetInstance.WinPEMode && !_options.QuarantineMode && !OSExtensions.IsWindows7())
            {
                Utils.CheckLatestReleaseVersion();
            }
            //#endif

            if (AppConfig.GetInstance.WinPEMode && _options.nosignaturescan)
            {
                _options.nosignaturescan = false;
            }

            if (AppConfig.GetInstance.WinPEMode && _options.silent)
            {
                _options.silent = false;
            }

            if (_options.silent)
            {
                Console.WriteLine("\t\t[SILENT MODE]");
                Logger.WriteLog("\t\t[SILENT MODE]", ConsoleColor.White, false, true);
                Thread.Sleep(1000);
                //#if !DEBUG
                ConsoleBridge.Hide();
                //#endif
            }

            if (!_options.QuarantineMode)
            {
                Utils.IncrementStartupCount();
            }

            if (!_options.gui_mode)
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            }

            if (_options.demandSelection && !_options.silent && !_options.gui_mode)
            {
                _options.selectedPath = PromptScanDirectory(_options, AppConfig.GetInstance);
                if (_options.selectedPath == null) return;
            }

            if (_options.QuarantineMode && !_options.gui_mode)
            {
                QuarantineConsoleHandler qcm = new QuarantineConsoleHandler();
                qcm.Execute();
                return;
            }

            if (_options.gui_mode)
            {
                RunGui();
                return;
            }

            ScanFactory.PrepareEnvironment(_options);

            ScanInfrastructure infrastructure = ScanFactory.CreateInfrastructure();

            ScanWorkflow workflow = new ScanWorkflow(infrastructure, null, new ConsoleScanInteraction());
            ScanWorkflowResult workflowResult = workflow.Run(CancellationToken.None);
            ScanStatistics scanStats = workflowResult.Statistics;

            Utils.SwitchMouseSelection(true);
            Logger.DisposeLogger();

            if (!_options.silent && !_options.console_mode)
            {
                WriteConsoleFinish(scanStats);
            }

            return;
        }

        /// <summary>
        /// Console finish: final status, log path, telemetry prompt
        /// and key wait in WinPE.
        /// </summary>
        private static void WriteConsoleFinish(ScanStatistics stats)
        {
            Logger.WriteLog(GetConsoleFinishStatus(stats), GetConsoleFinishColor(stats), false);

            if (Logger.HasScanLog)
                Logger.WriteLog("\t\t" + Logger.LogFilePath, ConsoleColor.Gray, false);

            PromptConsoleTelemetry(stats);

            if (AppConfig.GetInstance.WinPEMode)
                ConsoleBridge.SafeReadKey();
        }

        private static string GetConsoleFinishStatus(ScanStatistics stats)
        {
            LocalizedLogger logger = AppConfig.GetInstance.LL;

            if (LaunchOptions.GetInstance.ScanOnly)
                return logger.GetLocalizedString("_ScanOnlyMode");

            if (stats.Found == 0)
                return logger.GetLocalizedString("_NoThreats");

            int notNeutralized = stats.Found - stats.Neutralized - stats.Skipped;

            if (stats.Neutralized == stats.Found)
            {
                return stats.LockedByAv > 0
                    ? logger.GetLocalizedString("_FinishThreatsBlockedByAV").Replace("#COUNT#", stats.LockedByAv.ToString())
                    : logger.GetLocalizedString("_FinishAllThreatsNeutralized");
            }

            if (stats.Skipped > 0 && notNeutralized <= 0)
            {
                return logger.GetLocalizedString("_FinishSomeThreatsSkipped").Replace("#COUNT#", stats.Skipped.ToString());
            }

            return logger.GetLocalizedString("_FinishNotAllThreatsNeutralized");
        }

        private static ConsoleColor GetConsoleFinishColor(ScanStatistics stats)
        {
            if (stats.Found > 0 && stats.Neutralized == stats.Found)
                return ConsoleColor.Green;

            if (stats.Skipped > 0 && stats.Found - stats.Neutralized - stats.Skipped <= 0)
                return ConsoleColor.DarkYellow;

            if (stats.Found > 0)
                return ConsoleColor.Red;

            return ConsoleColor.Gray;
        }

        /// <summary>
        /// Asks once whether the scan log may be uploaded (console mode).
        /// </summary>
        private static void PromptConsoleTelemetry(ScanStatistics stats)
        {
            if (LaunchOptions.GetInstance.no_logs || !Logger.HasScanLog)
                return;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AppConfig.GetInstance.RegistryPathMain, true)
                            ?? Registry.CurrentUser.CreateSubKey(AppConfig.GetInstance.RegistryPathMain))
                {
                    if (key == null)
                        return;

                    object value = key.GetValue(AppConfig.GetInstance.StatisticsValueName);
                    if (value == null)
                    {
                        key.SetValue(AppConfig.GetInstance.StatisticsValueName, 2, RegistryValueKind.DWord);
                        value = key.GetValue(AppConfig.GetInstance.StatisticsValueName);
                    }

                    int allowStatistics = value is int ? (int)value : 0;
                    if (allowStatistics == 2)
                    {
                        bool accepted = AskConsoleYesNo(AppConfig.GetInstance.LL.GetLocalizedString("_AllowSentStatistics"));
                        key.SetValue(AppConfig.GetInstance.StatisticsValueName, accepted ? 1 : 0, RegistryValueKind.DWord);
                        allowStatistics = accepted ? 1 : 0;
                    }

                    if (allowStatistics == 1)
                    {
                        // Task.Run: не блокируем поток с возможным WinForms SynchronizationContext.
                        Task.Run(() => MinerSearch.SentLogAsync(stats.Found, stats.Neutralized, stats.Skipped))
                            .GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }

        private static bool AskConsoleYesNo(string question)
        {
            try
            {
                Console.Write($"{question} [Y/N]: ");
                string answer = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(answer))
                    return false;

                string normalized = answer.Trim().ToLowerInvariant();
                return normalized == "y" || normalized == "yes" || normalized == "д" || normalized == "да";
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Whether the arguments require a console (must connect before parsing).
        /// </summary>
        private static bool HasConsoleArgument(string[] args)
        {
            if (args == null)
                return false;

            foreach (string arg in args)
            {
                if (arg.Equals("--console-mode", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-cm", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-h", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("--winpemode", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-w", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Starts the GUI main window.
        /// </summary>
        private static void RunGui()
        {
            Application.Run(new FormMain());
        }

        /// <summary>
        /// Prompts user for scan directory via FolderBrowserDialog.
        /// Returns normalized path or null if cancelled.
        /// </summary>
        private static string PromptScanDirectory(LaunchOptions options, AppConfig config)
        {
            if (!string.IsNullOrEmpty(options.selectedPath))
                return options.selectedPath;

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = config.LL.GetLocalizedString("_SelectFolderDialog");
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
#if DEBUG
                    Console.WriteLine($"\t[DBG] Selected path: {dialog.SelectedPath}");
#endif
                    return FileSystemManager.NormalizeExtendedPath(dialog.SelectedPath);
                }
                else
                {
                    var noFolderDialog = MessageBox.Show(
                        config.LL.GetLocalizedString("_MessageCancelFolderDialog"),
                        config._title,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (noFolderDialog != DialogResult.Yes)
                    {
                        return null; // cancel — Init() returns
                    }
                }
            }

            return options.selectedPath;
        }

        private static void TryInitConsoleWindow()
        {
            try
            {
                int maxW = Console.LargestWindowWidth;
                int maxH = Console.LargestWindowHeight;

                // 0 = нет консольного буфера (ConPTY и подобные) — ресайз невозможен
                if (maxW <= 0 || maxH <= 0)
                    return;

                int w = Math.Min(150, maxW);
                int h = Math.Min(40, maxH);

                if (Console.BufferWidth < w) Console.BufferWidth = w;
                if (Console.BufferHeight < h) Console.BufferHeight = h;

                Console.WindowWidth = w;
                Console.WindowHeight = h;
            }
            catch { }
        }

        private static void WaterMark()
        {
            if (AppConfig.GetInstance.RunAsSystem)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("\t╔════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║███╗   ███╗██╗███╗   ██╗███████╗██████╗ ███████╗███████╗ █████╗ ██████╗  ██████╗██╗  ██╗║");
            Console.WriteLine("\t║████╗ ████║██║████╗  ██║██╔════╝██╔══██╗██╔════╝██╔════╝██╔══██╗██╔══██╗██╔════╝██║  ██║║");
            Console.WriteLine("\t║██╔████╔██║██║██╔██╗ ██║█████╗  ██████╔╝███████╗█████╗  ███████║██████╔╝██║     ███████║║");
            Console.WriteLine("\t║██║╚██╔╝██║██║██║╚██╗██║██╔══╝  ██╔══██╗╚════██║██╔══╝  ██╔══██║██╔══██╗██║     ██╔══██║║");
            Console.WriteLine("\t║██║ ╚═╝ ██║██║██║ ╚████║███████╗██║  ██║███████║███████╗██║  ██║██║  ██║╚██████╗██║  ██║║");
            Console.WriteLine("\t║╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝║");
            Console.WriteLine("\t╚════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.BackgroundColor = ConsoleColor.Black;

#if !BETA
            Console.WriteLine(@"                                                                                          ");
#else
            Console.WriteLine(@"                                                              ____           _           ");
            Console.WriteLine(@"                                                             | __ )    ___  | |_    __ _ ");
            Console.WriteLine(@"                                                             |  _ \   / _ \ | __|  / _` |");
            Console.WriteLine(@"                                                             | |_) | |  __/ | |_  | (_| |");
            Console.WriteLine(@"                                                             |____/   \___|  \__|  \__,_|");
#endif
            Console.WriteLine("\t\tby: BlendLog");
        }
    }
}
