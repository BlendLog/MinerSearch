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
#if !DEBUG
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
#endif
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

                using (FinishEx finish = new FinishEx(scanStats.Found, scanStats.Neutralized, scanStats.Suspicious, scanStats.ElapsedTime, state.GetScanResults(), scanStats.Skipped)
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

            // Cleanup: OS automatically releases handles on process exit.
            // Explicit ReleaseMutex calls are removed to avoid SynchronizationLockException (ownership was lost during checks).

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

            // Собираем все решения для Finalize-фазы
            var allFinalizeDecisions = new List<ThreatDecision>();
            allFinalizeDecisions.AddRange(processDecisions);
            allFinalizeDecisions.AddRange(commonDecisions);

            var signatureDecisions = new List<ThreatDecision>();

            if (!options.nosignaturescan)
            {
                var signatureScanner = new SignatureScanner();
                signatureScanner.CollectFilesAsync().Wait();

                var allSignatureFiles = signatureScanner.GetFiles().ToList();
                signatureDecisions = signatureFileAnalyzer.AnalyzeFiles(allSignatureFiles).Where(d => d != null).ToList();
                allFinalizeDecisions.AddRange(signatureDecisions);
            }

            // Удаляем дубликаты по нормализованному пути — MSData хранит пути без UNC (C:\...),
            // а SignatureScanner формирует UNC (\\?\C:\...). Один и тот же файл не должен
            // обрабатываться дважды.
            allFinalizeDecisions = DeduplicateDecisions(allFinalizeDecisions);

            // Разделяем известные (obfStr*) и неизвестные угрозы
            var knownDecisions = allFinalizeDecisions.Where(d => IsKnownThreat(d)).ToList();
            var unknownDecisions = allFinalizeDecisions.Where(d => !IsKnownThreat(d)).ToList();

            // На review показываем только неизвестные угрозы из signatureDecisions
            // (чистая Finalize-фаза, без предварительной обработки).
            // Неизвестные из processDecisions/commonDecisions уже прошли SuspendOnly/DisableExecuteOnly —
            // применяем их автоматически, как известные.
            var unknownFromSignatures = signatureDecisions
                .Where(d => d != null && !IsKnownThreat(d))
                .ToList();
            var unknownFromEarlier = unknownDecisions
                .Except(unknownFromSignatures)
                .ToList();

            cleanManager.BeginFinalCleanup();

            // Применяем известные угрозы автоматически (без review).
            // Сортируем по ThreatObjectKind: процессы (0) раньше файлов (2) и каталогов (3),
            // чтобы не пытаться удалить файл до завершения процесса.
            var sortedKnownDecisions = knownDecisions.OrderBy(d => (int)d.Target.Kind).ToList();
            foreach (var decision in sortedKnownDecisions)
            {
                cleanManager.ApplyDecisions(new[] { decision }, CleanupPhase.Finalize);
            }

            // Применяем неизвестные угрозы из ранних фаз автоматически (без review)
            if (unknownFromEarlier.Count > 0)
                cleanManager.ApplyDecisions(unknownFromEarlier, CleanupPhase.Finalize);

            // Показываем review-UI только для неизвестных угроз из сигнатур
            if (!options.no_review_interact && unknownFromSignatures.Count > 0)
            {
                var reviewForm = new FormThreatReview(unknownFromSignatures);
                reviewForm.ShowDialog();

                // Применяем выбор пользователя к флагам в ThreatObject перед выполнением очистки
                if (reviewForm.IsApplied())
                    reviewForm.ApplyUserOverrides();
            }

            // Применяем неизвестные угрозы из сигнатур (с учётом UserOverrideAction)
            // Решения со Skip не применяем через Handler — записываем как пропущенные напрямую
            var decisionsToApply = new List<ThreatDecision>();
            var decisionsToSkip = new List<ThreatDecision>();

            foreach (var decision in unknownFromSignatures)
            {
                if (decision.UserOverrideAction == ScanActionTypeUserSelected.Skip)
                    decisionsToSkip.Add(decision);
                else
                    decisionsToApply.Add(decision);
            }

            if (decisionsToApply.Count > 0)
                cleanManager.ApplyDecisions(decisionsToApply, CleanupPhase.Finalize);

            // Записываем пропущенные решения напрямую в результаты сканирования
            if (decisionsToSkip.Count > 0)
                cleanManager.ApplySkippedDecisions(decisionsToSkip);

            // Исключаем из финального применения все уже обработанные решения:
            // - processDecisions/commonDecisions: уже применены на этапах SuspendOnly/DisableExecuteOnly
            // - knownDecisions: автоматически применены в Finalize выше
            // - unknownFromEarlier: автоматически применены в Finalize выше
            // - decisionsToApply: применены пользовательским выбором (не Skip)
            // - decisionsToSkip: обработаны через ApplySkippedDecisions (повторное применение выполнит действие анализатора вопреки выбору пользователя)
            var alreadyAppliedIds = new HashSet<string>(
                processDecisions.Select(d => d.Target.Id)
                    .Concat(commonDecisions.Select(d => d.Target.Id))
                    .Concat(knownDecisions.Select(d => d.Target.Id))
                    .Concat(unknownFromEarlier.Select(d => d.Target.Id))
                    .Concat(decisionsToApply.Select(d => d.Target.Id))
                    .Concat(decisionsToSkip.Select(d => d.Target.Id)));
            
            // Применяем только оставшиеся Finalize-решения (например signatureDecisions,
            // которые не попали ни в knownDecisions, ни в unknownDecisions)
            var finalizeOnlyDecisions = allFinalizeDecisions
                .Where(d => d != null && !alreadyAppliedIds.Contains(d.Target.Id))
                .ToList();
            
            if (finalizeOnlyDecisions.Count > 0)
            {
                cleanManager.ApplyDecisions(finalizeOnlyDecisions, CleanupPhase.Finalize);
            }
        }

      /// <summary>
        /// Проверяет, является ли угроза «известной» по MSData (obfStr1-obfStr4).
        /// Известные угрозы применяются автоматически без review.
        /// </summary>
        private static bool IsKnownThreat(ThreatDecision decision)
        {
            if (decision?.Target == null) return false;
            var target = decision.Target;

            // Файлы и каталоги — проверяем SourceTag
            var file = target as FileThreatObject;
            if (file != null && !string.IsNullOrEmpty(file.SourceTag))
            {
                if (file.SourceTag.StartsWith("obfStr", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            var dir = target as DirectoryThreatObject;
            if (dir != null && !string.IsNullOrEmpty(dir.SourceTag))
            {
                if (dir.SourceTag.StartsWith("obfStr", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Файл может быть найден SignatureScanner с UNC-путём (\\?\C:\...),
            // а в MSData.obfStr2 тот же путь без UNC. Нормализуем и сверяем.
            if (file != null && !string.IsNullOrEmpty(file.FilePath))
            {
                string normalizedFilePath = file.FilePath.StartsWith(@"\\?\") ? file.FilePath.Substring(4) : file.FilePath;
                if (MSData.GetInstance.obfStr2.Any(s =>
                {
                    string normalized = s.StartsWith(@"\\?\") ? s.Substring(4) : s;
                    return normalized.Equals(normalizedFilePath, StringComparison.OrdinalIgnoreCase);
                }))
                {
                    return true;
                }
            }

            // Каталог может быть найден с UNC-путём — аналогичная нормализация
            if (dir != null && !string.IsNullOrEmpty(dir.DirectoryPath))
            {
                string normalizedDirPath = dir.DirectoryPath.StartsWith(@"\\?\") ? dir.DirectoryPath.Substring(4) : dir.DirectoryPath;
                if (MSData.GetInstance.obfStr1.Any(s =>
                {
                    string normalized = s.StartsWith(@"\\?\") ? s.Substring(4) : s;
                    return normalized.Equals(normalizedDirPath, StringComparison.OrdinalIgnoreCase);
                }))
                {
                    return true;
                }
            }

            // Процессы — проверяем путь связанного файла по obfStr2
            var proc = target as ProcessThreatObject;
            if (proc?.FileProcess != null)
            {
                var filePath = proc.FileProcess.FilePath;
                if (!string.IsNullOrEmpty(filePath) &&
                    IsPathInObfStr2(filePath))
                {
                    return true;
                }
            }

            // Службы — проверяем путь бинарника по obfStr2
            var svc = target as ServiceThreatObject;
            if (svc != null)
            {
                if (!string.IsNullOrEmpty(svc.ServicePath) &&
                    IsPathInObfStr2(svc.ServicePath))
                {
                    return true;
                }
            }

            // Задачи — проверяем XML путь по obfStr2
            var task = target as TaskThreatObject;
            if (task?.Info != null)
            {
                if (!string.IsNullOrEmpty(task.Info.XmlPath) &&
                    IsPathInObfStr2(task.Info.XmlPath))
                {
                    return true;
                }
            }

            // Для остальных типов — не считаем известными по умолчанию
            return false;
        }

        /// <summary>
        /// Проверяет, есть ли путь в MSData.obfStr2 с учётом UNC-префикса (\\?\).
        /// SignatureScanner формирует пути как \\?\C:\..., а MSData хранит C:\...
        /// </summary>
        private static bool IsPathInObfStr2(string testPath)
        {
            string normalizedTest = testPath.StartsWith(@"\\?\") ? testPath.Substring(4) : testPath;
            return MSData.GetInstance.obfStr2.Any(s =>
            {
                string normalized = s.StartsWith(@"\\?\") ? s.Substring(4) : s;
                return normalized.Equals(normalizedTest, StringComparison.OrdinalIgnoreCase);
            });
        }

        /// <summary>
        /// Удаляет дубликаты решений: SignatureScanner может найти тот же файл, что уже есть
        /// в processDecisions/commonDecisions, но с UNC-префиксом (\\?\).
        /// Группируем по Target.Id, а для файлов/каталогов — по нормализованному пути.
        /// </summary>
        private static List<ThreatDecision> DeduplicateDecisions(List<ThreatDecision> decisions)
        {
            return decisions
                .Where(d => d != null && d.Target != null)
                .GroupBy(d => GetDedupKey(d))
                .Select(g => g.First())
                .ToList();
        }

        private static string GetDedupKey(ThreatDecision d)
        {
            // Для файлов: всегда нормализованный путь (без \\?\).
            // Хеш не используется — разные сканеры могут вычислять хеш по-разному или не вычислять вовсе.
            if (d.Target is FileThreatObject file && !string.IsNullOrEmpty(file.FilePath))
            {
                string normalizedPath = file.FilePath.StartsWith(@"\\?\") ? file.FilePath.Substring(4) : file.FilePath;
                return "File:" + normalizedPath;
            }
            // Для каталогов: нормализованный путь
            if (d.Target is DirectoryThreatObject dir && !string.IsNullOrEmpty(dir.DirectoryPath))
            {
                string normalizedPath = dir.DirectoryPath.StartsWith(@"\\?\") ? dir.DirectoryPath.Substring(4) : dir.DirectoryPath;
                return "Dir:" + normalizedPath;
            }
            // Для всех остальных: Target.Id
            return d.Target.Kind + ":" + (d.Target.Id ?? "");
        }

        /// <summary>
        /// Computes and logs scanning results.
        /// </summary>
        private static ScanStatistics ComputeAndLogStatistics(MinerSearchScanState state, TimeSpan elapsed)
        {
            var scanResults = state.GetScanResults();

            // Группируем по уникальным Id ThreatObject для корректного подсчёта.
            // Если ThreatObjectId пустой (напр. для файлов с валидной подписью hash="",
            // или bad_bat где hash="") — используем составной ключ RawClass + Path,
            // чтобы Distinct не схлопывал все пустые строки в 1.
            int foundCount = scanResults
                .Where(r => r.RawType == ScanObjectType.Malware ||
                            r.RawType == ScanObjectType.Unsafe ||
                            r.RawType == ScanObjectType.Infected ||
                            r.RawType == ScanObjectType.Rootkit ||
                            r.RawType == ScanObjectType.Suspicious)
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            int neutralizedCount = scanResults
                .Where(r => r.RawAction == ScanActionType.Deleted ||
                            r.RawAction == ScanActionType.Terminated ||
                            r.RawAction == ScanActionType.Quarantine ||
                            r.RawAction == ScanActionType.Cured ||
                            r.RawAction == ScanActionType.Disabled ||
                            r.RawAction == ScanActionType.Suspended)
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            int suspiciousCount = AppConfig.GetInstance.totalFoundSuspiciousObjects;

            // Считаем намеренно пропущенные — чтобы отличать их от неудавшихся нейтрализаций
            int skippedCount = scanResults
                .Where(r => r.RawAction == ScanActionType.Skipped &&
                            (r.RawType == ScanObjectType.Malware ||
                             r.RawType == ScanObjectType.Unsafe ||
                             r.RawType == ScanObjectType.Infected ||
                             r.RawType == ScanObjectType.Rootkit ||
                             r.RawType == ScanObjectType.Suspicious))
                .Select(r => !string.IsNullOrEmpty(r.ThreatObjectId) ? r.ThreatObjectId : r.RawClass.ToString() + "|" + r.Path)
                .Distinct()
                .Count();

            string elapsedTime = elapsed.Hours.ToString("00") + ":" + elapsed.Minutes.ToString("00") + ":" + elapsed.Seconds.ToString("00") + "." + elapsed.Milliseconds.ToString("000");

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
                Skipped = skippedCount,
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
            public int Skipped { get; set; }
            public string ElapsedTime { get; set; }
        }

    }
}