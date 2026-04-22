using DBase;
using Microsoft.Win32;
using MSearch.Core;
using MSearch.Core.Handlers;
using MSearch.Core.Managers;
using MSearch.Core.Scanners;
using MSearch.Core.ThreatAnalyzers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatHandlers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using MSearch.Properties;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

            if (_options.noLogs)
                AppConfig.GetInstance.no_logs = true;

            Logger.InitLogger(AppConfig.GetInstance.no_logs);
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
            NameValueCollection section = ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
            if (section != null)
            {
                section["DpiAwareness"] = "PerMonitorV2";
            }

            if (!OSExtensions.IsDotNetInstalled())
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNoDotNet"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            if (!Utils.IsOneAppCopy())
            {
                if (!_options.silent)
                {
                    var result = DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_AppAlreadyRunning"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!Utils.IsRebootMtx())
            {
                Utils.mutex.ReleaseMutex();
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_RebootRequired"), AppConfig.GetInstance._title);
                return;
            }

            if (OSExtensions.IsWinPEEnv())
            {
                List<string> newargs = new List<string> { "--winpemode" };
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        newargs.Add(arg);
                    }
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

            if (ProcessManager.IsStartedFromArchive())
            {
                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn"), AppConfig.GetInstance.LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            const string registryKeyPath = @"Software\M1nerSearch";
            const string registryValueName = "acceptedEula";
            const string registryValueOutdatedOS = "SupressOutdatedOSWarning";


            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                var value = key.GetValue(registryValueName);
                if (value == null || value != null && (int)value != 1)
                {
                    if (!_options.accept_eula)
                    {
                        if (!_options.console_mode)
                        {
                            try
                            {
                                using (License licenseForm = new License())
                                {
                                    if (AppConfig.GetInstance.ActiveLanguage == "EN")
                                    {
                                        licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_EN;
                                        licenseForm.richTextBox1.Rtf = Resources._License_EN;
                                        licenseForm.Accept_btn.Text = Resources._accept_en;
                                        licenseForm.Exit_btn.Text = Resources._exit_EN;
                                    }
                                    if (AppConfig.GetInstance.ActiveLanguage == "RU")
                                    {
                                        licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_RU;
                                        licenseForm.richTextBox1.Rtf = Resources._License_RU;
                                        licenseForm.Accept_btn.Text = Resources._accept_ru;
                                        licenseForm.Exit_btn.Text = Resources._exit_RU;
                                    }

                                    licenseForm.ShowDialog();
                                }
                            }
                            catch (InvalidOperationException ioe)
                            {
                                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_Error") + $"\n\n{ioe.Message}", AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            catch (FileNotFoundException notfoundEx)
                            {
                                DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}", AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
#if DEBUG
                            Logger.WriteLog("[DBG] UserIneractive false, Console mode...", ConsoleColor.White);
#endif

                            DialogResult agreementResult = DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_License_console"), AppConfig.GetInstance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (agreementResult == DialogResult.Yes)
                            {
                                key.SetValue(registryValueName, 1);
                            }
                            else return;
                        }
                    }
                    else
                    {
                        key.SetValue(registryValueName, 1);
                    }

                }

                var SupressWarningValue = key.GetValue(registryValueOutdatedOS);

                //Outdated OS Check
                if ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1) && (SupressWarningValue == null || (int)SupressWarningValue == 0))
                {
                    DialogResult dialogResult = MessageBoxCustom.Show(AppConfig.GetInstance.LL.GetLocalizedString("_WarnOutdatedOS"), AppConfig.GetInstance._title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        key.SetValue(registryValueOutdatedOS, 1);
                        Utils.mutex.ReleaseMutex();
                        Process.Start(AppConfig.GetInstance.ExecutablePath, "-so");
                        return;
                    }
                    else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.None)
                    {
                        return;
                    }
                }
            }



            Directory.SetCurrentDirectory(Path.GetDirectoryName(AppConfig.GetInstance.ExecutablePath));

            if (_options.hasErrors)
            {
                foreach (var error in _options.errors)
                {
                    DialogDispatcher.Show(error, AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            // Применяем флаги из LaunchOptions
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
                _options.ScanOnly = true;
                _options.NoRootkitCheck = true;
                _options.RemoveEmptyTasks = false;
            }

            //if (_options.maxSubfolders.HasValue)
            //{
            //    _options.maxSubfolders = _options.maxSubfolders.Value;
            //}

            if (!string.IsNullOrEmpty(_options.selectedPath))
            {
                _options.selectedPath = _options.selectedPath;
            }

            if (_options.QuarantineRestoreOption)
            {
                _options.QuarantineRestoreOption = true;
                if (!string.IsNullOrEmpty(_options.quarantineListEnum))
                {
                    AppConfig.GetInstance.quarantineListEnum = _options.quarantineListEnum;
                }
            }

            if (_options.QuarantineDeleteOption)
            {
                _options.QuarantineDeleteOption = true;
                if (!string.IsNullOrEmpty(_options.quarantineListEnum))
                {
                    AppConfig.GetInstance.quarantineListEnum = _options.quarantineListEnum;
                }
            }

            // Обработка --winpemode (требует интерактивного ввода)
            if (_options.winpemode)
            {
                if (!_options.console_mode)
                {
                drive_letter_lbl:
                    LocalizedLogger.LogSpecifyDrive();
                    AppConfig.GetInstance.drive_letter = Console.ReadLine();
                    if (AppConfig.GetInstance.drive_letter.Length > 1 || FileChecker.IsDigit(AppConfig.GetInstance.drive_letter))
                    {
                        LocalizedLogger.LogIncorrectDrive(AppConfig.GetInstance.drive_letter);
                        goto drive_letter_lbl;
                    }

                    if (!System.IO.Directory.Exists(AppConfig.GetInstance.drive_letter + ":\\"))
                    {
                        LocalizedLogger.LogIncorrectDrive(AppConfig.GetInstance.drive_letter);
                        goto drive_letter_lbl;
                    }

                    Drive.Letter = AppConfig.GetInstance.drive_letter;
                    _options.NoRootkitCheck = true;
                    _options.no_runtime = true;
                    _options.no_services = true;
                    AppConfig.GetInstance.WinPEMode = true;
                    LocalizedLogger.LogWinPEMode(AppConfig.GetInstance.drive_letter);
                }
            }

            // Обработка --select= (требует доступ к args для получения следующего элемента)
            // Заменено: параметры обрабатываются в LaunchOptions.ParseArgs() выше

            if (!AppConfig.GetInstance.WinPEMode)
            {
                AppConfig.GetInstance.drive_letter = Environment.GetEnvironmentVariable("SYSTEMDRIVE").Remove(1);
                Drive.Letter = AppConfig.GetInstance.drive_letter;
            }

            if (ProcessManager.GetCurrentProcessOwner().IsSystem && !AppConfig.GetInstance.WinPEMode)
            {
                if (!_options.silent)
                {
                    if (!_options.Force)
                    {
                        if (AppConfig.GetInstance.IsGuiAvailable)
                        {
                            var msg = DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_MessageRunAsSystemWarn"), AppConfig.GetInstance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (msg != DialogResult.Yes)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                }
                else
                {
                    Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
                }
            }


#if !DEBUG
            if (!_options.help)
            {
                Utils.SwitchMouseSelection();
            }

#endif


#if !DEBUG
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

            LocalizedLogger.LogPCInfo(OSExtensions.GetWindowsVersion() + " " + OSExtensions.GetPlatform(), Environment.UserName, Environment.MachineName, AppConfig.GetInstance.bootMode);
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

            if (!AppConfig.GetInstance.no_logs)
            {
                AppConfig.GetInstance.LL.LogWriteWithoutDisplay(true, false);
            }

            if (_options.demandSelection && !_options.silent)
            {
                if (!AppConfig.GetInstance.WinPEMode && string.IsNullOrEmpty(_options.selectedPath))
                {
                    using (var dialog = new FolderBrowserDialog())
                    {
                        dialog.Description = AppConfig.GetInstance.LL.GetLocalizedString("_SelectFolderDialog");
                        dialog.ShowNewFolderButton = false;
                        dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
#if DEBUG
                            Console.WriteLine($"\t[DBG] Selected path: {dialog.SelectedPath}");
#endif
                            _options.selectedPath = FileSystemManager.NormalizeExtendedPath(dialog.SelectedPath);
                        }
                        else
                        {
                            var noFolderDialog = MessageBox.Show(AppConfig.GetInstance.LL.GetLocalizedString("_MessageCancelFolderDialog"), AppConfig.GetInstance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (noFolderDialog != DialogResult.Yes)
                            {
                                Environment.Exit(0);
                            }

                        }
                    }
                }
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

            ProcessManager.InitPrivileges();

            if (!_options.NoRootkitCheck || !_options.no_runtime)
            {
                if (!ProcessManager.HasDebugPrivilege())
                {
                    string privilegename = "S?eDe??bug?Pr?iv?il?ege".Replace("?", "");
                    string groupName = OSExtensions.ConvertWellKnowSIDToGroupName("S-1-5-32-544"); //Admin group


                    if (Native.GrantPrivilegeToGroup(groupName, privilegename))
                    {
                        DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_SuccessGrantPrivilege"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        var result = DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_RebootPCNowDialog"), AppConfig.GetInstance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo()
                            {
                                FileName = "shutdown",
                                Arguments = "/r /t 0",
                                UseShellExecute = false,
                                CreateNoWindow = true
                            });
                        }
                        Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
                        Utils.mutex.ReleaseMutex();
                        if (_options.console_mode)
                        {
                            return;
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorGrantPrivilege"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(0);
                    }
                }
            }

            Stopwatch startTime = Stopwatch.StartNew();
            //---------------------------------
            //MinerSearch minerSearch = new MinerSearch();
            MinerSearchScanState state = new MinerSearchScanState();
            SignatureFileAnalyzer singatureFileAnalyzer = new SignatureFileAnalyzer();

            // RootkitThreatScanner идёт первым — до любого сканирования
            ScanManager scanManager = new ScanManager(new RootkitScanner(), new ProcessScanner(),
                new IThreatScanner[] {
                    new FileSystemThreatScanner(),
                    new ShellStartupScanner(),
                    new RegistryScanner(),
                    new ServiceScanner(),
                    new TaskScanner(),
                    new FirewallScanner(),
                    new WmiScanner(),
                    new HostsThreatScanner(),
                });
            ThreatManager threatManager = new ThreatManager(new IThreatAnalyzer[]
            {
                new RootkitThreatAnalyzer(),
                new ProcessThreatAnalyzer(singatureFileAnalyzer),
                new FileSystemThreatAnalyzer(singatureFileAnalyzer),
                new ShellStartupThreatAnalyzer(singatureFileAnalyzer),
                new RegistryThreatAnalyzer(singatureFileAnalyzer),
                new ServiceThreatAnalyzer(singatureFileAnalyzer),
                new TaskThreatAnalyzer(singatureFileAnalyzer),
                new FirewallThreatAnalyzer(),
                new DirectoryThreatAnalyzer(),
                new WmiThreatAnalyzer(),
                new HostsThreatAnalyzer(),
            });
            CleanManager cleanManager = new CleanManager(new IThreatHandler[]
            {
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
            }, state);

            List<ThreatDecision> allDecisions = new List<ThreatDecision>();

            // 1. Сначала сканируем руткиты (мгновенная обработка, до любого другого сканирования)
            IEnumerable<IThreatObject> rootkitThreats = scanManager.ScanRootkitFirst();
            IEnumerable<ThreatDecision> rootkitDecisions = threatManager.Analyze(rootkitThreats).Where(d => d != null).ToList();
            cleanManager.ApplyDecisions(rootkitDecisions, CleanupPhase.Finalize);
            allDecisions.AddRange(rootkitDecisions);

            // 2. Сканируем процессы
            IEnumerable<IThreatObject> processThreats = scanManager.ScanProcessesOnly();
            IEnumerable<ThreatDecision> processDecisions = threatManager.Analyze(processThreats).Where(d => d != null).ToList();
            cleanManager.ApplyDecisions(processDecisions, CleanupPhase.SuspendOnly);
            allDecisions.AddRange(processDecisions);

            // 3. Всё остальное
            IEnumerable<IThreatObject> commonThreats = scanManager.ScanEverythingElse();
            IEnumerable<ThreatDecision> commonDecisions = threatManager.Analyze(commonThreats).Where(d => d != null).ToList();
            allDecisions.AddRange(commonDecisions);
            cleanManager.ApplyDecisions(commonDecisions, CleanupPhase.DisableExecuteOnly);

            if (_options.pause) //pause before first cleanup
            {
                LocalizedLogger.LogPAUSE();
                if (AppConfig.GetInstance.IsGuiAvailable)
                {
                    Console.ReadKey(true);
                }
            }

            if (!LaunchOptions.GetInstance.nosignaturescan)
            {
                var signatureScanner = new SignatureScanner();
                signatureScanner.CollectFilesAsync().Wait();

                var allSignatureFiles = signatureScanner.GetFiles().ToList();
                singatureFileAnalyzer.AnalyzeFiles(allSignatureFiles);
                var signatureDecisions = threatManager.Analyze(allSignatureFiles).Where(d => d != null).ToList();
                allDecisions.AddRange(signatureDecisions);

                cleanManager.BeginFinalCleanup();
                cleanManager.ApplyDecisions(processDecisions, CleanupPhase.Finalize);
                cleanManager.ApplyDecisions(commonDecisions, CleanupPhase.Finalize);
            }

            cleanManager.ApplyDecisions(allDecisions, CleanupPhase.Finalize);
            startTime.Stop();

            TimeSpan resultTime = startTime.Elapsed;

            // Подсчёт статистики из реальных результатов (без отрицательных значений)
            var scanResults = state.GetScanResults();
            int foundCount = scanResults.Count(r =>
                r.RawType == ScanObjectType.Malware ||
                r.RawType == ScanObjectType.Unsafe ||
                r.RawType == ScanObjectType.Infected);

            int neutralizedCount = scanResults.Count(r =>
                r.RawAction == ScanActionType.Deleted ||
                r.RawAction == ScanActionType.Terminated ||
                r.RawAction == ScanActionType.Quarantine ||
                r.RawAction == ScanActionType.Cured ||
                r.RawAction == ScanActionType.Disabled ||
                r.RawAction == ScanActionType.Suspended);

            int suspiciousCount = AppConfig.GetInstance.totalFoundSuspiciousObjects;

            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(foundCount, neutralizedCount, suspiciousCount);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

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
                }

                using (FinishEx finish = new FinishEx(foundCount, neutralizedCount, suspiciousCount, elapsedTime, state.GetScanResults())
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
            Console.WriteLine("\t\t?by: Ble?nd??Log?".Replace("?", ""));
        }
    }
}