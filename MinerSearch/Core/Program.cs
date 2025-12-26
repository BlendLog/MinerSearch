using DBase;
using Microsoft.Win32;
using MSearch.Core;
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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MSearch
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = Utils.GetRndString();
            AppConfig.Instance.RunAsSystem = ProcessManager.IsSystemProcess(Process.GetCurrentProcess().Id);
            AppConfig.Instance.IsGuiAvailable = Environment.UserInteractive;
            AppConfig.Instance.IsRedirectedInput = Console.IsInputRedirected;
            AppConfig.Instance.console_mode = !Environment.UserInteractive || Console.IsInputRedirected;

            foreach (string arg in args)
            {
                if (arg.Equals("--no-logs", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nl", StringComparison.OrdinalIgnoreCase))
                {
                    AppConfig.Instance.no_logs = true;
                }

                if (arg.Equals("--console-mode", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-cm", StringComparison.OrdinalIgnoreCase))
                {
                    AppConfig.Instance.console_mode = true;
                }
            }

            Logger.InitLogger(AppConfig.Instance.no_logs);
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
            AppConfig.Instance.ActiveLanguage = AppConfig.Instance.IsGuiAvailable ? LanguageManager.LoadLanguageSetting() : "EN";

            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.Instance.ActiveLanguage);
            }

            try
            {
                Init(args);
            }
            catch (FileNotFoundException notfoundEx)
            {
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }
#else

            AppConfig.Instance.ActiveLanguage = LanguageManager.LoadLanguageSetting();
            if (!File.Exists(LanguageManager.CfgFile))
            {
                LanguageManager.SaveLanguageSetting(AppConfig.Instance.ActiveLanguage);
            }
            Init(args);

#endif
        }

        static void Init(string[] args)
        {
            HashSet<string> passiveArgs = new HashSet<string>
            {
                "--no-logs", "-nl",
                "--accept-eula", "-a",
                "--console-mode", "-cm",
                "--silent", "-si"
            };

            foreach (string arg in args)
            {
                if (arg.Equals("--silent", StringComparison.OrdinalIgnoreCase) || arg.Equals("-si", StringComparison.OrdinalIgnoreCase))
                {
                    AppConfig.Instance.silent = true;
                    AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
                }
            }

            NameValueCollection section = ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
            if (section != null)
            {
                section["DpiAwareness"] = "PerMonitorV2";
            }

            if (!OSExtensions.IsDotNetInstalled())
            {
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNoDotNet"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            if (!Utils.IsOneAppCopy())
            {
                if (!AppConfig.Instance.silent)
                {
                    var result = DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_AppAlreadyRunning"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!Utils.IsRebootMtx())
            {
                Utils.mutex.ReleaseMutex();
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_RebootRequired"), AppConfig.Instance._title);
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

                AppConfig.Instance.WinPEMode = true;
            }

            ProcessManager.SetSmallWindowIconRandomHash(Process.GetCurrentProcess().MainWindowHandle);

            if (!AppConfig.Instance.silent)
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
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ArchiveWarn"), AppConfig.Instance.LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            foreach (string arg in args)
            {
                if (arg.Equals("--accept-eula", StringComparison.OrdinalIgnoreCase) || arg.Equals("-a", StringComparison.OrdinalIgnoreCase))
                {
                    AppConfig.Instance.accept_eula = true;
                }
            }

            const string registryKeyPath = @"Software\M1nerSearch";
            const string registryValueName = "acceptedEula";
            const string registryValueOutdatedOS = "SupressOutdatedOSWarning";


            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                var value = key.GetValue(registryValueName);
                if (value == null || value != null && (int)value != 1)
                {
                    if (!AppConfig.Instance.accept_eula)
                    {
                        if (!AppConfig.Instance.console_mode)
                        {
                            try
                            {
                                using (License licenseForm = new License())
                                {
                                    if (AppConfig.Instance.ActiveLanguage == "EN")
                                    {
                                        licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_EN;
                                        licenseForm.richTextBox1.Rtf = Resources._License_EN;
                                        licenseForm.Accept_btn.Text = Resources._accept_en;
                                        licenseForm.Exit_btn.Text = Resources._exit_EN;
                                    }
                                    if (AppConfig.Instance.ActiveLanguage == "RU")
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
                                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Error") + $"\n\n{ioe.Message}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            catch (FileNotFoundException notfoundEx)
                            {
                                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
#if DEBUG
                            Logger.WriteLog("[DBG] UserIneractive false, Console mode...", ConsoleColor.White);
#endif

                            DialogResult agreementResult = DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_License_console"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                    DialogResult dialogResult = MessageBoxCustom.Show(AppConfig.Instance.LL.GetLocalizedString("_WarnOutdatedOS"), AppConfig.Instance._title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        key.SetValue(registryValueOutdatedOS, 1);
                        Utils.mutex.ReleaseMutex();
                        Process.Start(Assembly.GetExecutingAssembly().Location, "-so");
                        return;
                    }
                    else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.None)
                    {
                        return;
                    }
                }
            }



            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    if (arg.Equals("--help", StringComparison.OrdinalIgnoreCase) || arg.Equals("-h", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.help = true;

                        Console.ForegroundColor = ConsoleColor.White;

                        if (AppConfig.Instance.ActiveLanguage == "EN")
                        {
                            Console.Write(Resources._Help_EN);
                        }
                        else if (AppConfig.Instance.ActiveLanguage == "RU")
                        {
                            Console.Write(Resources._Help_RU);
                        }
                        else
                        {
                            Console.Write(Resources._Help_EN);
                        }

                        if (AppConfig.Instance.IsGuiAvailable)
                        {
                            Console.ReadKey();
                        }
                        return;
                    }
                    else if (arg.Equals("--force", StringComparison.OrdinalIgnoreCase) || arg.Equals("-f", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.Force = true;
                    }
                    else if (arg.Equals("--no-scantime", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nstm", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_scantime = true;
                    }
                    else if (arg.Equals("--no-signature-scan", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nss", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.nosignaturescan = true;
                    }
                    else if (arg.Equals("--no-runtime", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nr", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_runtime = true;
                    }
                    else if (arg.Equals("--no-scan-tasks", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nst", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_scan_tasks = true;
                    }
                    else if (arg.Equals("--pause", StringComparison.OrdinalIgnoreCase) || arg.Equals("-p", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.pause = true;
                    }
                    else if (arg.Equals("--remove-empty-tasks", StringComparison.OrdinalIgnoreCase) || arg.Equals("-ret", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.RemoveEmptyTasks = true;
                    }
                    else if (arg.StartsWith("--depth=") || arg.StartsWith("-d="))
                    {
                        string depthValue = arg.Substring(arg.IndexOf('=') + 1);

                        if (!int.TryParse(depthValue, out int depth) || depth <= 0 || depth > 16)
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_DepthInvalidValue").Replace("#ARG#", arg), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        AppConfig.Instance.maxSubfolders = depth;
                    }
                    else if (arg.Equals("--no-rootkit-check", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nrc", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.NoRootkitCheck = true;
                    }
                    else if (arg.Equals("--no-services", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nse", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_services = true;
                    }
                    else if (arg.Equals("--scan-only", StringComparison.OrdinalIgnoreCase) || arg.Equals("-so", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.ScanOnly = true;
                        AppConfig.Instance.NoRootkitCheck = true;
                        AppConfig.Instance.RemoveEmptyTasks = false;

                    }
                    else if (arg.Equals("--full-scan", StringComparison.OrdinalIgnoreCase) || arg.Equals("-fs", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.fullScan = true;
                    }
                    else if (arg.StartsWith("--open-quarantine") || arg.StartsWith("-q"))
                    {
                        AppConfig.Instance.QuarantineMode = true;
                    }
                    else if (arg.Equals("--winpemode", StringComparison.OrdinalIgnoreCase) || arg.Equals("-w", StringComparison.OrdinalIgnoreCase))
                    {
                    drive_letter_lbl:

                        if (!AppConfig.Instance.console_mode)
                        {
                            LocalizedLogger.LogSpecifyDrive();
                            AppConfig.Instance.drive_letter = Console.ReadLine();
                            if (AppConfig.Instance.drive_letter.Length > 1 || FileChecker.IsDigit(AppConfig.Instance.drive_letter))
                            {
                                LocalizedLogger.LogIncorrectDrive(AppConfig.Instance.drive_letter);
                                goto drive_letter_lbl;
                            }

                            if (!Directory.Exists(AppConfig.Instance.drive_letter + ":\\"))
                            {
                                LocalizedLogger.LogIncorrectDrive(AppConfig.Instance.drive_letter);
                                goto drive_letter_lbl;
                            }

                            Drive.Letter = AppConfig.Instance.drive_letter;
                            AppConfig.Instance.NoRootkitCheck = true;
                            AppConfig.Instance.no_runtime = true;
                            AppConfig.Instance.no_services = true;
                            AppConfig.Instance.WinPEMode = true;
                            LocalizedLogger.LogWinPEMode(AppConfig.Instance.drive_letter);
                        }
                    }
                    else if (arg == "-R")
                    {
                        AppConfig.Instance.RestoredWMI = true;
                    }
                    else if (arg.Equals("--verbose", StringComparison.OrdinalIgnoreCase) || arg.Equals("-v", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.verbose = true;
                    }
                    else if (arg.Equals("--run-as-system", StringComparison.OrdinalIgnoreCase) || arg.Equals("-ras", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!AppConfig.Instance.IsGuiAvailable)
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_MessageRemoteSystemSessionBlocked"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
                        Utils.mutex.ReleaseMutex();
                        StringBuilder argsBuilder = new StringBuilder("");
                        foreach (string _arg in args)
                        {
                            argsBuilder.Append(" " + _arg.ToLower());
                        }
                        Native.RunAs(Bfs.Create(
                            new StringBuilder("xq").Append("vY").Append("ab").Append("pF").Append("Rk").Append("S3").Append("8f").Append("0U").Append("BX").Append("5m").Append("3g").Append("==").ToString(),
                            new StringBuilder("KV").Append("/K").Append("6+").Append("Aq").Append("Y9").Append("P0").Append("Af").Append("Bo").Append("QQ").Append("dY").Append("M5").Append("qv").Append("iJ").Append("kq").Append("e2").Append("tY").Append("HX").Append("Rn").Append("cL").Append("hy").Append("/q").Append("8=").ToString(),
                            new StringBuilder("WF").Append("EF").Append("OW").Append("Yz").Append("RK").Append("pU").Append("oG").Append("EN").Append("ce").Append("yO").Append("SQ").Append("==").ToString()),
                            argsBuilder.ToString().Replace("--run-as-system", "").Replace("-ras", ""));
                        return;
                    }
                    else if (arg.Equals("--select", StringComparison.OrdinalIgnoreCase) || arg.Equals("-s", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.demandSelection = true;
                    }
                    else if (arg.Equals("--select=", StringComparison.OrdinalIgnoreCase) || arg.Equals("-s=", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.demandSelection = true;
                        if (i + 1 >= args.Length)
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorSpecifiedPathIsNull").Replace("#ARG#", arg), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string expectedPath = args[++i];
                        if (Directory.Exists(expectedPath))
                        {
                            AppConfig.Instance.selectedPath = expectedPath;
                        }
                        else
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorSpecifiedPathNotFound").Replace("#PATH#", expectedPath), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (arg.Equals("--restore=", StringComparison.OrdinalIgnoreCase) || arg.Equals("-res=", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.QuarantineRestoreOption = true;
                        if (i + 1 >= args.Length)
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorQuarantineListEnumIsNull"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string expectedList = args[++i];
                        if (expectedList.Contains(','))
                        {
                            AppConfig.Instance.quarantineListEnum = expectedList;
                        }
                        else if (int.TryParse(expectedList, out _))
                        {
                            AppConfig.Instance.quarantineListEnum = expectedList;
                        }
                        else
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_RestoreCommandSyntax"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (arg.Equals("--delete=", StringComparison.OrdinalIgnoreCase) || arg.Equals("-del=", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.QuarantineDeleteOption = true;
                        if (i + 1 >= args.Length)
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorQuarantineListEnumIsNull"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string expectedList = args[++i];
                        if (expectedList.Contains(','))
                        {
                            AppConfig.Instance.quarantineListEnum = expectedList;
                        }
                        else if (int.TryParse(expectedList, out _))
                        {
                            AppConfig.Instance.quarantineListEnum = expectedList;
                        }
                        else
                        {
                            DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_Q_CLI_RestoreCommandSyntax"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (arg.Equals("--no-check-hosts", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nch", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_check_hosts = true;
                    }
                    else if (arg.Equals("--no-firewall", StringComparison.OrdinalIgnoreCase) || arg.Equals("-nfw", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.no_firewall = true;
                    }
                    else if (passiveArgs.Contains(arg)) continue;
                    else
                    {
                        LocalizedLogger.LogUnknownCommand(arg);
                        return;
                    }
                }
            }

            if (!AppConfig.Instance.WinPEMode)
            {
                AppConfig.Instance.drive_letter = Environment.GetEnvironmentVariable("SYSTEMDRIVE").Remove(1);
                Drive.Letter = AppConfig.Instance.drive_letter;
            }

            if (ProcessManager.GetCurrentProcessOwner().IsSystem && !AppConfig.Instance.WinPEMode)
            {
                if (!AppConfig.Instance.silent)
                {
                    if (!AppConfig.Instance.Force)
                    {
                        if (AppConfig.Instance.IsGuiAvailable)
                        {
                            var msg = DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_MessageRunAsSystemWarn"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
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
            if (!AppConfig.Instance.help)
            {
                Utils.SwitchMouseSelection();
            }

#endif


#if !DEBUG
            if (!AppConfig.Instance.silent)
            {
                AppConfig.Instance.LL.LogJustDisplayMessage("\t\t", $"_RelevantVer", "https://github.com/BlendLog/Mi?ne?rSea?rch/releases \n".Replace("?", ""), ConsoleColor.White);
            }

#endif
            Logger.WriteLog("  \t  \tID: " + DeviceIdProvider.GetDeviceId(), ConsoleColor.White, false, true);



            if (!AppConfig.Instance.help && !AppConfig.Instance.silent)
            {
                LocalizedLogger.LogHelpHint();
            }

            AppConfig.Instance.LL.LogMessage("\t\t", "_Version", AppConfig.Instance.CurrentVersion, ConsoleColor.White, false);

#if !DEBUG
            if (!AppConfig.Instance.WinPEMode && !AppConfig.Instance.QuarantineMode && !OSExtensions.GetWindowsVersion().Contains("Windows 7"))
            {
                Utils.CheckLatestReleaseVersion();
            }

            if (AppConfig.Instance.no_runtime && AppConfig.Instance.no_scantime && !AppConfig.Instance.WinPEMode)
            {
                LocalizedLogger.LogErrorDisabledScan();
                if (AppConfig.Instance.IsGuiAvailable)
                {
                    Console.ReadKey();
                }
                return;
            }
#endif

            if (AppConfig.Instance.WinPEMode && AppConfig.Instance.nosignaturescan)
            {
                AppConfig.Instance.nosignaturescan = false;
            }

            if (AppConfig.Instance.WinPEMode && AppConfig.Instance.silent)
            {
                AppConfig.Instance.silent = false;
            }

            LocalizedLogger.LogPCInfo(OSExtensions.GetWindowsVersion() + " " + OSExtensions.GetPlatform(), Environment.UserName, Environment.MachineName, AppConfig.Instance.bootMode);
            if (AppConfig.Instance.silent)
            {
                Console.WriteLine("\t\t[SILENT MODE]");
                Logger.WriteLog("\t\t[SILENT MODE]", ConsoleColor.White, false, true); //for write log
                Thread.Sleep(1000);
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
            }

            if (!AppConfig.Instance.QuarantineMode)
            {
                Utils.CheckStartupCount();
            }

            ProcessModuleCollection pmodules = Process.GetCurrentProcess().Modules;
            foreach (var module in pmodules)
            {
                if (module.ToString().Contains(new StringBuilder("cm").Append("dv").Append("rt").Append("64").Append(".d").Append("ll").ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(new StringBuilder("Pl").Append("ea").Append("se").Append(", ").Append("ad").Append("d ").Append("th").Append("e ").Append("pr").Append("og").Append("ra").Append("m ").Append("to").Append(" a").Append("nt").Append("1v").Append("1r").Append("us").Append(" w").Append("hi").Append("te").Append("li").Append("st").Append("!").ToString());
                    Console.BackgroundColor = ConsoleColor.Black;
                    return;
                }
            }
            pmodules = null;

            Stopwatch startTime = Stopwatch.StartNew();

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            if (!AppConfig.Instance.no_logs)
            {
                AppConfig.Instance.LL.LogWriteWithoutDisplay(true, false);
            }

            if (AppConfig.Instance.demandSelection && !AppConfig.Instance.silent)
            {
                if (!AppConfig.Instance.WinPEMode && string.IsNullOrEmpty(AppConfig.Instance.selectedPath))
                {
                    using (var dialog = new FolderBrowserDialog())
                    {
                        dialog.Description = AppConfig.Instance.LL.GetLocalizedString("_SelectFolderDialog");
                        dialog.ShowNewFolderButton = false;
                        dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
#if DEBUG
                            Console.WriteLine($"\t[DBG] Selected path: {dialog.SelectedPath}");
#endif
                            AppConfig.Instance.selectedPath = FileSystemManager.NormalizeExtendedPath(dialog.SelectedPath);
                        }
                        else
                        {
                            var noFolderDialog = MessageBox.Show(AppConfig.Instance.LL.GetLocalizedString("_MessageCancelFolderDialog"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (noFolderDialog != DialogResult.Yes)
                            {
                                Environment.Exit(0);
                            }

                        }
                    }
                }
            }

            MSData.InitOnce(AppConfig.Instance.RunAsSystem);

            if (AppConfig.Instance.QuarantineMode)
            {
                if (AppConfig.Instance.console_mode)
                {
                    QuarantineConsoleHandler qcm = new QuarantineConsoleHandler();
                    qcm.Execute();
                    return;
                }
                else
                {
                    Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);
                    QuarantineForm qForm = new QuarantineForm();
                    qForm.ShowDialog();
                }
            }

            MinerSearch mk = new MinerSearch();

            AppConfig.Instance.LL.LogHeadMessage("_PreparingToScan");
            FileChecker.RestoreSignatures(MSData.Instance.signatures);

            ProcessManager.InitPrivileges();

            if (!AppConfig.Instance.NoRootkitCheck || !AppConfig.Instance.no_runtime)
            {
                if (!ProcessManager.HasDebugPrivilege())
                {
                    string privilegename = "SeDebugPrivilege";
                    string groupName = Native.ConvertWellKnowSIDToGroupName("S-1-5-32-544"); //Admin group


                    if (Native.GrantPrivilegeToGroup(groupName, privilegename))
                    {
                        DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_SuccessGrantPrivilege"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        var result = DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_RebootPCNowDialog"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        if (AppConfig.Instance.console_mode)
                        {
                            return;
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorGrantPrivilege"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(0);
                    }
                }
            }

            if (!AppConfig.Instance.NoRootkitCheck)
            {
                mk.DetectRk();
            }

            if (!AppConfig.Instance.no_runtime)
            {
                mk.Scan();
            }

            if (!AppConfig.Instance.no_scantime)
            {
                mk.StaticScan();
            }

            if (AppConfig.Instance.pause)
            {
                LocalizedLogger.LogPAUSE();
                if (AppConfig.Instance.IsGuiAvailable)
                {
                    Console.ReadKey(true);
                }
            }
            mk.Clean();

            if (!AppConfig.Instance.nosignaturescan)
            {
                mk.SignatureScan();
            }
            GC.Collect();
            startTime.Stop();            
            
            TimeSpan resultTime = startTime.Elapsed;
            int NeutralizedThreatsCount = AppConfig.Instance.totalNeutralizedThreats + AppConfig.Instance.totalFoundThreats;
            if (NeutralizedThreatsCount < 0) NeutralizedThreatsCount = 0;

            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(AppConfig.Instance.totalFoundThreats, NeutralizedThreatsCount, AppConfig.Instance.totalFoundSuspiciousObjects);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

            Utils.SwitchMouseSelection(true);
            Logger.DisposeLogger();

            if (!AppConfig.Instance.silent && !AppConfig.Instance.console_mode)
            {
#if !DEBUG
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);
#endif
                foreach (ScanResult result in MinerSearch.scanResults)
                {
                    if (result.RawAction == ScanActionType.LockedByAntivirus)
                    {
                        AppConfig.Instance.hasLockedObjectsByAV = true;
                    }
                }

                FinishEx finish = new FinishEx(AppConfig.Instance.totalFoundThreats, NeutralizedThreatsCount, AppConfig.Instance.totalFoundSuspiciousObjects, elapsedTime) //+ sign because neutralized threats is negative
                {
                    TopMost = true
                };
                finish.LoadResults(MinerSearch.scanResults);
                finish.ShowDialog();
            }

#if DEBUG
            Console.ReadLine();
#endif

            return;
        }
        private static void WaterMark()
        {
            if (AppConfig.Instance.RunAsSystem)
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