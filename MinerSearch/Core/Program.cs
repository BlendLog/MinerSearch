using DBase;
using MSearch.Core;
using MSearch.Core.Handlers;
using MSearch.Core.Managers;
using MSearch.Core.Scanners;
using MSearch.Core.ThreatAnalyzers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatHandlers;
using MSearch.Properties;
using MSearch.UI;
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
            Console.Title = Utils.GetRndString();

            LaunchOptions _options = LaunchOptions.GetInstance;

            AppConfig.GetInstance.RunAsSystem = ProcessManager.IsSystemProcess(Process.GetCurrentProcess().Id);
            AppConfig.GetInstance.IsGuiAvailable = Environment.UserInteractive;
            AppConfig.GetInstance.IsRedirectedInput = Console.IsInputRedirected;

            _options.console_mode = !Environment.UserInteractive || Console.IsInputRedirected;

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
            }

            ProcessManager.SetSmallWindowIconRandomHash(Process.GetCurrentProcess().MainWindowHandle);

            if (!_options.silent)
            {
                if (!ProcessManager.IsPwshAsParentProcess(Process.GetCurrentProcess().Id))
                {
                    int targetW = 150;
                    int targetH = 40;

                    int maxW = Console.LargestWindowWidth;
                    int maxH = Console.LargestWindowHeight;

                    int w = Math.Min(targetW, maxW);
                    int h = Math.Min(targetH, maxH);

                    if (Console.BufferWidth < w) Console.BufferWidth = w;
                    if (Console.BufferHeight < h) Console.BufferHeight = h;

                    Console.WindowWidth = w;
                    Console.WindowHeight = h;

                    WaterMark();
                }
            }

            if (envResult.IsStartedFromArchive)
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn"), AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            // EULA and outdated OS warning (moved to EnvironmentChecker)
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
                    Console.ReadKey();
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
                    Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
                }
            }

#if !DEBUG
            if (!_options.help)
            {
                Utils.SwitchMouseSelection();
            }

            if (!_options.silent)
            {
                AppConfig.GetInstance.LL.LogJustDisplayMessage("\t\t", $"_RelevantVer", "https://github.com/BlendLog/Mi?ne?rSea?rch/releases \n".Replace("?", ""), ConsoleColor.White);
            }

#endif
            Logger.WriteLog("  \t  \tID: " + DeviceIdProvider.GetDeviceId(), ConsoleColor.White, false, true);

            if (!_options.help && !_options.silent)
            {
                LocalizedLogger.LogHelpHint();
            }

            AppConfig.GetInstance.LL.LogMessage("\t\t", "_Version", AppConfig.GetInstance.CurrentVersion, ConsoleColor.White, false);

#if !DEBUG
            if (!AppConfig.GetInstance.WinPEMode && !_options.QuarantineMode && !OSExtensions.GetWindowsVersion().Contains("Windows 7"))
            {
                Utils.CheckLatestReleaseVersion();
            }

            if (_options.no_runtime && _options.no_scantime && !AppConfig.GetInstance.WinPEMode)
            {
                LocalizedLogger.LogErrorDisabledScan();
                if (AppConfig.GetInstance.IsGuiAvailable)
                {
                    Console.ReadKey();
                }
                return;
            }
#endif

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
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
            }

            if (!_options.QuarantineMode)
            {
                Utils.CheckStartupCount();
            }

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            if (!_options.no_logs)
            {
                AppConfig.GetInstance.LL.LogWriteWithoutDisplay(true, false);
            }

            if (_options.demandSelection && !_options.silent)
            {
                _options.selectedPath = PromptScanDirectory(_options, AppConfig.GetInstance);
                if (_options.selectedPath == null) return;
            }

            if (_options.QuarantineMode)
            {
                if (_options.console_mode)
                {
                    QuarantineConsoleHandler qcm = new QuarantineConsoleHandler();
                    qcm.Execute();
                    return;
                }
                else
                {
                    Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);
                    using (QuarantineForm qForm = new QuarantineForm())
                    {
                        qForm.ShowDialog();
                    }
                }
            }

            FileChecker.RestoreSignatures(MSData.GetInstance.signatures);
            new PrivilegeManager(_options).Enable();
            Stopwatch startTime = Stopwatch.StartNew();

            // Create scanning infrastructure (moved to separate method)
            var infrastructure = CreateScanInfrastructure();
            MinerSearchScanState state = infrastructure.State;
            SignatureFileAnalyzer signatureFileAnalyzer = infrastructure.SignatureAnalyzer;
            ScanManager scanManager = infrastructure.ScanManager;
            ThreatManager threatManager = infrastructure.ThreatManager;
            CleanManager cleanManager = infrastructure.CleanManager;


            // Execute scanning (moved to separate method)
            ExecuteScanWorkflow(_options, scanManager, threatManager, cleanManager, signatureFileAnalyzer);
            startTime.Stop();

            TimeSpan resultTime = startTime.Elapsed;

            // Compute and log statistics (moved to separate method)
            var scanStats = ComputeAndLogStatistics(state, resultTime);

            Utils.SwitchMouseSelection(true);
            Logger.DisposeLogger();

            if (!_options.silent && !_options.console_mode)
            {
#if !DEBUG
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);
#endif
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

                using (FinishEx finish = new FinishEx(scanStats.Found, scanStats.Neutralized, scanStats.Suspicious, scanStats.ElapsedTime, state.GetScanResults())
                {
                    TopMost = true
                })
                {
                    finish.ShowDialog();
                }
            }

#if DEBUG
            Console.ReadLine();
#endif

            // Cleanup: release mutexes
            if (Utils.mutex != null)
            {
                Utils.mutex.ReleaseMutex();
            }
            if (Utils.rebootMtx != null)
            {
                Utils.rebootMtx.ReleaseMutex();
            }

            return;
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

        #region Extracted methods

        /// <summary>
        /// Creates all scanners, analyzers and managers for scanning.
        /// </summary>
        private static ScanInfrastructure CreateScanInfrastructure()
        {
            var state = new MinerSearchScanState();
            var signatureFileAnalyzer = new SignatureFileAnalyzer();

            // RootkitThreatScanner runs first — before any scanning
            var scanManager = new ScanManager(
                new RootkitScanner(),
                new ProcessScanner(),
                new IThreatScanner[] {
                    new FileSystemThreatScanner(),
                    new ShellStartupScanner(),
                    new RegistryScanner(),
                    new ServiceScanner(),
                    new TaskScanner(),
                    new FirewallScanner(),
                    new WmiScanner(),
                    new HostsThreatScanner(),
                    new UserProfileScanner(),
                });

            var threatManager = new ThreatManager(new IThreatAnalyzer[] {
                new RootkitThreatAnalyzer(),
                new ProcessThreatAnalyzer(signatureFileAnalyzer),
                new FileSystemThreatAnalyzer(signatureFileAnalyzer),
                new ShellStartupThreatAnalyzer(signatureFileAnalyzer),
                new RegistryThreatAnalyzer(signatureFileAnalyzer),
                new ServiceThreatAnalyzer(signatureFileAnalyzer),
                new TaskThreatAnalyzer(signatureFileAnalyzer),
                new FirewallThreatAnalyzer(),
                new DirectoryThreatAnalyzer(),
                new WmiThreatAnalyzer(),
                new HostsThreatAnalyzer(),
                new UserProfileThreatAnalyzer(),
            });

            var cleanManager = new CleanManager(new IThreatHandler[] {
                new RootkitThreatHandler(),
                new ProcessThreatHandler(),
                new RegistryThreatHandler(),
                new ServiceThreatHandler(),
                new TaskThreatHandler(),
                new ShellStartupThreatHandler(),
                new FirewallThreatHandler(),
                new DirectoryThreatHandler(),
                new FileThreatHandler(),
                new WmiThreatHandler(),
                new HostsThreatHandler(),
                new UserProfileThreatHandler(),
            }, state);

            return new ScanInfrastructure
            {
                State = state,
                SignatureAnalyzer = signatureFileAnalyzer,
                ScanManager = scanManager,
                ThreatManager = threatManager,
                CleanManager = cleanManager
            };
        }

        /// <summary>
        /// Executes main scanning loop: rootkits → processes → everything else → signatures.
        /// </summary>
        private static void ExecuteScanWorkflow(LaunchOptions options, ScanManager scanManager, ThreatManager threatManager, CleanManager cleanManager, SignatureFileAnalyzer signatureFileAnalyzer)
        {
            IEnumerable<IThreatObject> rootkitThreats = scanManager.ScanRootkitFirst();
            IEnumerable<ThreatDecision> rootkitDecisions = threatManager.Analyze(rootkitThreats).Where(d => d != null).ToList();
            cleanManager.ApplyDecisions(rootkitDecisions, CleanupPhase.Finalize);

            IEnumerable<IThreatObject> processThreats = scanManager.ScanProcessesOnly();
            IEnumerable<ThreatDecision> processDecisions = threatManager.Analyze(processThreats).Where(d => d != null).ToList();
            cleanManager.ApplyDecisions(processDecisions, CleanupPhase.SuspendOnly);

            IEnumerable<IThreatObject> commonThreats = scanManager.ScanEverythingElse();
            IEnumerable<ThreatDecision> commonDecisions = threatManager.Analyze(commonThreats).Where(d => d != null).ToList();
            cleanManager.ApplyDecisions(commonDecisions, CleanupPhase.DisableExecuteOnly);

            if (options.pause)
            {
                LocalizedLogger.LogPAUSE();
                if (AppConfig.GetInstance.IsGuiAvailable)
                {
                    Console.ReadKey(true);
                }
            }
            cleanManager.BeginFinalCleanup();
            cleanManager.ApplyDecisions(processDecisions, CleanupPhase.Finalize);
            cleanManager.ApplyDecisions(commonDecisions, CleanupPhase.Finalize);

            if (!options.nosignaturescan)
            {
                var signatureScanner = new SignatureScanner();
                signatureScanner.CollectFilesAsync().Wait();

                var allSignatureFiles = signatureScanner.GetFiles().ToList();
                var signatureDecisions = signatureFileAnalyzer.AnalyzeFiles(allSignatureFiles).Where(d => d != null).ToList();

                cleanManager.ApplyDecisions(signatureDecisions, CleanupPhase.Finalize);
            }

        }

        /// <summary>
        /// Computes and logs scanning results.
        /// </summary>
        private static ScanStatistics ComputeAndLogStatistics(MinerSearchScanState state, TimeSpan elapsed)
        {
            var scanResults = state.GetScanResults();

            // Группируем по уникальным Id ThreatObject для корректного подсчёта
            int foundCount = scanResults
                .Where(r => r.RawType == ScanObjectType.Malware ||
                            r.RawType == ScanObjectType.Unsafe ||
                            r.RawType == ScanObjectType.Infected ||
                            r.RawType == ScanObjectType.Rootkit ||
                            r.RawType == ScanObjectType.Suspicious)
                .Select(r => r.ThreatObjectId)
                .Distinct()
                .Count();

            int neutralizedCount = scanResults
                .Where(r => r.RawAction == ScanActionType.Deleted ||
                            r.RawAction == ScanActionType.Terminated ||
                            r.RawAction == ScanActionType.Quarantine ||
                            r.RawAction == ScanActionType.Cured ||
                            r.RawAction == ScanActionType.Disabled ||
                            r.RawAction == ScanActionType.Suspended)
                .Select(r => r.ThreatObjectId)
                .Distinct()
                .Count();

            int suspiciousCount = AppConfig.GetInstance.totalFoundSuspiciousObjects;
            string elapsedTime = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds:000}";

            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(foundCount, neutralizedCount, suspiciousCount);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

            return new ScanStatistics
            {
                Found = foundCount,
                Neutralized = neutralizedCount,
                Suspicious = suspiciousCount,
                ElapsedTime = elapsedTime
            };
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

        #endregion

        /// <summary>
        /// Scanning infrastructure results.
        /// </summary>
        private class ScanInfrastructure
        {
            public MinerSearchScanState State { get; set; }
            public SignatureFileAnalyzer SignatureAnalyzer { get; set; }
            public ScanManager ScanManager { get; set; }
            public ThreatManager ThreatManager { get; set; }
            public CleanManager CleanManager { get; set; }
        }

        /// <summary>
        /// Scanning statistics results.
        /// </summary>
        private class ScanStatistics
        {
            public int Found { get; set; }
            public int Neutralized { get; set; }
            public int Suspicious { get; set; }
            public string ElapsedTime { get; set; }
        }

    }
}