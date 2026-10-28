using DBase;
using MSearch.Core;
using MSearch.Core.Handlers;
using MSearch.Core.Managers;
using MSearch.Core.Scanners;
using MSearch.Core.ThreatAnalyzers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatHandlers;
using MSearch.Core.ThreatObjects;
using MSearch.Properties;
using MSearch.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Win32Wrapper;

namespace MSearch
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            LaunchOptions _options = LaunchOptions.GetInstance;

            // Должно быть вызвано до создания первого окна (включая EULA и диалоги).
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Консольные режимы в WinExe-сборке: подключаемся к консоли родителя
            // до разбора аргументов, чтобы ошибки разбора было куда вывести.
            if (HasConsoleArgument(args) || Native.GetConsoleWindow() != IntPtr.Zero)
                ConsoleBridge.EnsureConsole();

            // Parse command line arguments
            LaunchOptions.ParseArgs(args);

            // Check for parsing errors
            if (_options.hasErrors)
            {
                foreach (var error in _options.errors)
                {
                    DialogDispatcher.Show(error, AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Environment.Exit(1);
            }

            bool stdioRedirected = ConsoleBridge.HasRedirectedStdio();
            bool interactive = Environment.UserInteractive;

            // В WinExe без консоли Console.IsInputRedirected всегда true,
            // поэтому ориентируемся на реальный тип stdio и явные флаги.
            _options.console_mode = _options.console_mode || stdioRedirected || !interactive;

            bool consoleFlags = _options.console_mode || _options.winpemode || _options.help;
            _options.gui_mode = interactive && !consoleFlags && !_options.silent;

            if (!_options.gui_mode && !_options.silent)
                ConsoleBridge.EnsureConsole();

            ConsoleBridge.SafeSetTitle(Utils.GetRndString());

            AppConfig.GetInstance.RunAsSystem = ProcessManager.IsSystemProcess(Process.GetCurrentProcess().Id);
            AppConfig.GetInstance.IsGuiAvailable = interactive;
            AppConfig.GetInstance.IsRedirectedInput = stdioRedirected;

            // Лог сканирования создаётся только в ScanWorkflow.Run(); здесь задаётся лишь запрет (-nl).
            Logger.InitLogger(_options.no_logs);
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
            AppConfig.GetInstance.ActiveLanguage = AppConfig.GetInstance.IsGuiAvailable ? LanguageManager.LoadLanguageSetting() : "EN";

            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.GetInstance.ActiveLanguage);
            }

            try
            {
                Init(args);
            }
            catch (FileNotFoundException notfoundEx)
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}", AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }
#else

            AppConfig.GetInstance.ActiveLanguage = LanguageManager.LoadLanguageSetting();
            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.GetInstance.ActiveLanguage);
            }
            Init(args);

#endif
        }

        static void Init(string[] args)
        {
            LaunchOptions _options = LaunchOptions.GetInstance;

            // Create DeviceId-based mutexes
            Utils.mutex = new Mutex(false, Utils.GetDeviceIdLockName());
            Utils.rebootMtx = new Mutex(false, Utils.GetDeviceIdRebootName());

            // Check environment (moved to EnvironmentChecker)
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

            // WinPE: add --winpemode flag to arguments
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

            // Apply flags from LaunchOptions
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

            // WinPE: drive letter input (moved to EnvironmentChecker)
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

            // RunAsSystem warning (moved to EnvironmentChecker)
            if (envResult.IsSystemProcess && !AppConfig.GetInstance.WinPEMode)
            {
                if (!_options.silent && !_options.Force)
                {
                    if (AppConfig.GetInstance.IsGuiAvailable)
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
            Logger.WriteLog("  \t  \tID: " + DeviceIdProvider.GetDeviceId(), ConsoleColor.White, false, true);

            if (!_options.help && !_options.silent)
            {
                LocalizedLogger.LogHelpHint();
            }

            AppConfig.GetInstance.LL.LogMessage("\t\t", "_Version", AppConfig.GetInstance.CurrentVersion, ConsoleColor.White, false);

//if !DEBUG
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

            LocalizedLogger.LogPCInfo(OSExtensions.GetWindowsVersion() + " " + OSExtensions.GetPlatform(), Environment.UserName, Environment.MachineName, AppConfig.GetInstance.bootMode, OSExtensions.GetUACState());

            if (_options.silent)
            {
                Console.WriteLine("\t\t[SILENT MODE]");
                Logger.WriteLog("\t\t[SILENT MODE]", ConsoleColor.White, false, true); //for write log
                Thread.Sleep(1000);
//#if !DEBUG
                ConsoleBridge.Hide();
//#endif
            }

            if (!_options.QuarantineMode)
            {
                Utils.CheckStartupCount();
            }

            if (!_options.gui_mode)
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            }

            if (!_options.no_logs)
            {
                AppConfig.GetInstance.LL.LogWriteWithoutDisplay(true, false);
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

            // Create scanning infrastructure
            ScanInfrastructure infrastructure = ScanFactory.CreateInfrastructure();
            MinerSearchScanState state = infrastructure.State;

            // Execute scanning (engine is UI-agnostic)
            ScanWorkflow workflow = new ScanWorkflow(infrastructure, null, new ConsoleScanInteraction());
            ScanWorkflowResult workflowResult = workflow.Run(CancellationToken.None);
            ScanStatistics scanStats = workflowResult.Statistics;

            Utils.SwitchMouseSelection(true);
            Logger.DisposeLogger();

            if (!_options.silent && !_options.console_mode)
            {
//#if !DEBUG
                ConsoleBridge.Minimize();
//#endif
                foreach (ScanResult result in state.GetScanResults())
                {
                    if (result.RawAction == ScanActionType.LockedByAntivirus)
                    {
                        AppConfig.GetInstance.hasLockedObjectsByAV = true;
                    }

                    if (result.RawType == ScanObjectType.Unknown && result.RawAction == ScanActionType.Deleted)
                    {
                        AppConfig.GetInstance.hasEmptyTasks = true;
                    }
                }

                using (FinishEx finish = new FinishEx(scanStats.Found, scanStats.Neutralized, scanStats.Suspicious, scanStats.ElapsedTime, state.GetScanResults(), scanStats.Skipped, scanStats.LockedByAv)
                {
                    TopMost = true
                })
                {
                    finish.ShowDialog();
                }
            }

            return;
        }

        /// <summary>
        /// Признак того, что аргументы требуют консоли (нужно подключиться до разбора).
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
        /// Запускает главное окно GUI.
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
