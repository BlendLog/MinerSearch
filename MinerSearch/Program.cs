//#define BETA

using DBase;
using Microsoft.Win32;
using MSearch.Properties;
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
        public static bool no_logs = false;
        public static bool no_scantime = false;
        public static bool no_runtime = false;
        public static bool no_services = false;
        public static bool no_scan_tasks = false;
        public static bool pause = false;
        public static bool help = false;
        public static bool RemoveEmptyTasks = false;
        public static bool nosignaturescan = false;
        public static bool WinPEMode = false;
        public static bool NoRootkitCheck = false;
        public static bool ScanOnly = false;
        public static bool fullScan = false;
        public static bool RestoredWMI = false;
        public static bool RunAsSystem = false;
        public static bool verbose = false;
        public static bool silent = false;
        private static bool demandSelection = false;
        public static int maxSubfolders = 8;
        public static int totalFoundThreats = 0;
        public static int totalFoundSuspiciousObjects = 0;
        public static int totalNeutralizedThreats = 0;
        public static string drive_letter = "C";
        public static string selectedPath = "";
        public static string ActiveLanguage = "EN";
        public static string _title = new StringBuilder("Mi").Append("ner").Append("Sea").Append("rch").ToString();
        internal static BootMode bootMode = OSExtensions.GetBootMode();
        public static LocalizedLogger LL = new LocalizedLogger();
        public static Utils _utils = new Utils();
        public static string CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        [STAThread]
        static void Main(string[] args)
        {

#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
            ActiveLanguage = OSExtensions.GetSystemLanguage();
            try
            {
                Init(args);
            }
            catch (FileNotFoundException)
            {
                MessageBoxCustom.Show(LL.GetLocalizedString("_ErrorNotFoundComponent"), _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }
#else

            ActiveLanguage = OSExtensions.GetSystemLanguage();
            Init(args);

#endif
        }

        static void Init(string[] args)
        {
            Logger.InitLogger();

            foreach (string arg in args)
            {
                arg.ToLower();
                if (arg == "--silent" || arg == "-si")
                {
                    silent = true;
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
                MessageBoxCustom.Show(LL.GetLocalizedString("_ErrorNoDotNet"), _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            if (!Utils.IsOneAppCopy())
            {
                if (!silent)
                {
                    var result = MessageBoxCustom.Show(LL.GetLocalizedString("_AppAlreadyRunning"), _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Environment.Exit(0);
            }

            if (!Utils.IsRebootMtx())
            {
                Utils.mutex.ReleaseMutex();
                MessageBoxCustom.Show(LL.GetLocalizedString("_RebootRequired"), _title);
                Environment.Exit(0);
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

                WinPEMode = true;
                RunAsSystem = true;
            }

            Console.Title = Utils.GetRndString();
            IntPtr mHandle = Process.GetCurrentProcess().MainWindowHandle;

            var bitmap = (Bitmap)ProcessManager.GetSmallWindowIcon(mHandle);
            Random rnd = new Random();
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            ProcessManager.SetConsoleWindowIcon(bitmap, mHandle);

            if (!silent)
            {
                if (Screen.PrimaryScreen.Bounds.Width > 1024 && Screen.PrimaryScreen.Bounds.Height > 634)
                {
                    if (Console.LargestWindowWidth >= 150)
                    {
                        Console.SetWindowSize(150, 40);
                        WaterMark();
                    }
                }
            }

            if (ProcessManager.IsStartedFromArchive())
            {
                MessageBoxCustom.Show(LL.GetLocalizedString("_ArchiveWarn"), LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            const string registryKeyPath = @"Software\M1nerSearch";
            const string registryValueName = "acceptedEula";


            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                var value = key.GetValue(registryValueName);
                if (value == null)
                {
                    using (License licenseForm = new License())
                    {
                        if (ActiveLanguage == "EN")
                        {
                            licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_EN;
                            licenseForm.richTextBox1.Rtf = Resources._License_EN;
                            licenseForm.Accept_btn.Text = Resources._accept_en;
                            licenseForm.Exit_btn.Text = Resources._exit_EN;
                        }
                        if (ActiveLanguage == "RU")
                        {
                            licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_RU;
                            licenseForm.richTextBox1.Rtf = Resources._License_RU;
                            licenseForm.Accept_btn.Text = Resources._accept_ru;
                            licenseForm.Exit_btn.Text = Resources._exit_RU;
                        }

                        licenseForm.ShowDialog();
                    }
                }
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    arg.ToLower();
                    if (arg == "--help" || arg == "-h")
                    {
                        help = true;

                        Console.ForegroundColor = ConsoleColor.White;

                        if (ActiveLanguage == "EN")
                        {
                            Console.Write(Resources._Help_EN);
                        }
                        else if (ActiveLanguage == "RU")
                        {
                            Console.Write(Resources._Help_RU);
                        }
                        else
                        {
                            Console.Write(Resources._Help_EN);
                        }
                        Console.ReadKey();
                        return;
                    }

                    if (arg == "--no-logs" || arg == "-nl")
                    {
                        no_logs = true;
                    }
                    else if (arg == "--no-scantime" || arg == "-nstm")
                    {
                        no_scantime = true;
                    }
                    else if (arg == "--no-signature-scan" || arg == "-nss")
                    {
                        nosignaturescan = true;
                    }
                    else if (arg == "--no-runtime" || arg == "-nr")
                    {
                        no_runtime = true;
                    }
                    else if (arg == "--no-scan-tasks" || arg == "-nst")
                    {
                        no_scan_tasks = true;
                    }
                    else if (arg == "--pause" || arg == "-p")
                    {
                        pause = true;
                    }
                    else if (arg == "--remove-empty-tasks" || arg == "-ret")
                    {
                        RemoveEmptyTasks = true;
                    }
                    else if (arg.StartsWith("--depth=") || arg.StartsWith("-d="))
                    {
                        string depthValue = arg.Substring(arg.IndexOf('=') + 1);

                        if (!int.TryParse(depthValue, out maxSubfolders) || maxSubfolders <= 0 || maxSubfolders > 16)
                        {
                            Console.WriteLine($"\nThe number in {arg} command cannot be negative, = 0 or > 16");
                            Console.ReadLine();
                            return;
                        }
                    }
                    else if (arg == "--no?-ro?otki?t-check".Replace("?", "") || arg == "-nrc")
                    {
                        NoRootkitCheck = true;
                    }
                    else if (arg == "--no-services" || arg == "-nse")
                    {
                        no_services = true;
                    }
                    else if (arg == "--scan-only" || arg == "-so")
                    {
                        ScanOnly = true;
                        NoRootkitCheck = true;
                        RemoveEmptyTasks = false;

                    }
                    else if (arg == "--full-scan" || arg == "-fs")
                    {
                        fullScan = true;
                    }
                    else if (arg.StartsWith("--open-quarantine") || arg.StartsWith("-q"))
                    {
                        Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);
                        QuarantineForm qForm = new QuarantineForm();
                        qForm.ShowDialog();
                    }
                    else if (arg == "--winpemode" || arg == "-w")
                    {
                    drive_letter_lbl:
                        LocalizedLogger.LogSpecifyDrive();
                        drive_letter = Console.ReadLine();
                        if (drive_letter.Length > 1 || FileChecker.IsDigit(drive_letter))
                        {
                            LocalizedLogger.LogIncorrectDrive(drive_letter);
                            goto drive_letter_lbl;
                        }

                        if (!Directory.Exists(drive_letter + ":\\"))
                        {
                            LocalizedLogger.LogIncorrectDrive(drive_letter);
                            goto drive_letter_lbl;
                        }

                        Drive.Letter = drive_letter;
                        NoRootkitCheck = true;
                        no_runtime = true;
                        no_services = true;
                        WinPEMode = true;
                        LocalizedLogger.LogWinPEMode(drive_letter);
                    }
                    else if (arg == "-R")
                    {
                        RestoredWMI = true;
                    }
                    else if (arg == "--verbose" || arg == "-v")
                    {
                        verbose = true;
                    }
                    else if (arg == "--silent" || arg == "-si")
                    {
                        continue;
                    }
                    else if (arg == "--run-as-system" || arg == "-ras")
                    {
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
                        Environment.Exit(0);
                    }
                    else if (arg == "--select" || arg == "-s")
                    {
                        demandSelection = true;
                    }
                    else
                    {
                        LocalizedLogger.LogUnknownCommand(arg);
                        Console.ReadKey(true);
                        return;
                    }
                }
            }

            if (!WinPEMode)
            {
                drive_letter = Environment.GetEnvironmentVariable("SYSTEMDRIVE").Remove(1);
                Drive.Letter = drive_letter;
            }

            if (ProcessManager.GetCurrentProcessOwner().IsSystem && !WinPEMode)
            {
                if (!silent)
                {
                    var msg = MessageBoxCustom.Show(LL.GetLocalizedString("_MessageRunAsSystemWarn"), _title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (msg != DialogResult.Yes)
                    {
                        Environment.Exit(0);
                    }
                }
                else Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);

                RunAsSystem = true;
            }

#if !DEBUG
            if (!help)
            {
                Utils.SwitchMouseSelection();
            }

#endif



#if !DEBUG
            if (!silent)
            {
                LL.LogJustDisplayMessage("\t\t", $"_RelevantVer", "https://github.com/BlendLog/Mi?ne?rSea?rch/releases \n".Replace("?", ""), ConsoleColor.White);
            }

#endif
            Logger.WriteLog("\t\tID: " + OSExtensions.GetDeviceId(), ConsoleColor.White, false, true);


            if (!help && !silent)
            {
                LocalizedLogger.LogHelpHint();
            }

            LL.LogMessage("\t\t", "_Version", CurrentVersion, ConsoleColor.White, false);

#if !DEBUG
            if (!WinPEMode && !OSExtensions.GetWindowsVersion().Contains("Windows 7"))
            {
                Utils.CheckLatestReleaseVersion();
            }

            if (no_runtime && no_scantime && !WinPEMode)
            {
                LocalizedLogger.LogErrorDisabledScan();
                Console.ReadKey();
                return;
            }
#endif

            if (WinPEMode && nosignaturescan)
            {
                nosignaturescan = false;
            }

            if (WinPEMode && silent)
            {
                silent = false;
            }

            LocalizedLogger.LogPCInfo(OSExtensions.GetWindowsVersion() + " " + OSExtensions.GetPlatform(), Environment.UserName, Environment.MachineName, bootMode);
            if (silent)
            {
                Console.WriteLine("\t\t[SILENT MODE]");
                Logger.WriteLog("\t\t[SILENT MODE]", ConsoleColor.White, false, true); //for write log
                Thread.Sleep(1000);
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_HIDE);
            }

            Utils.CheckStartupCount();

            ProcessModuleCollection pmodules = Process.GetCurrentProcess().Modules;
            foreach (var module in pmodules)
            {
                if (module.ToString().Contains(new StringBuilder("cm").Append("dv").Append("rt").Append("64").Append(".d").Append("ll").ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(new StringBuilder("Pl").Append("ea").Append("se").Append(", ").Append("ad").Append("d ").Append("th").Append("e ").Append("pr").Append("og").Append("ra").Append("m ").Append("to").Append(" a").Append("nt").Append("1v").Append("1r").Append("us").Append(" w").Append("hi").Append("te").Append("li").Append("st").Append("!").ToString());
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.ReadLine();
                    Environment.Exit(10);
                }
            }
            pmodules = null;

            Stopwatch startTime = Stopwatch.StartNew();

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            if (!no_logs)
            {
                LL.LogWriteWithoutDisplay(true, false);
            }

            if (demandSelection && !silent)
            {
                if (!WinPEMode && string.IsNullOrEmpty(selectedPath))
                {
                    using (var dialog = new FolderBrowserDialog())
                    {
                        dialog.Description = LL.GetLocalizedString("_SelectFolderDialog");
                        dialog.ShowNewFolderButton = false;
                        dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
#if DEBUG
                            Console.WriteLine($"\t[DBG] Selected path: {dialog.SelectedPath}");
#endif
                            selectedPath = FileSystemManager.GetLongPath(dialog.SelectedPath);
                        }
                        else
                        {
                            var noFolderDialog = MessageBoxCustom.Show(LL.GetLocalizedString("_MessageCancelFolderDialog"), _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (noFolderDialog != DialogResult.Yes)
                            {
                                Environment.Exit(0);
                            }

                        }
                    }
                }
            }


            MinerSearch mk = new MinerSearch();

            LL.LogHeadMessage("_PreparingToScan");
            FileChecker.RestoreSignatures(mk.msData.signatures);

            ProcessManager.InitPrivileges();

            if (!NoRootkitCheck || !no_runtime)
            {
                if (!ProcessManager.HasDebugPrivilege())
                {
                    string privilegename = "SeDebugPrivilege";
                    string groupName = Native.ConvertWellKnowSIDToGroupName("S-1-5-32-544"); //Admin group


                    if (Native.GrantPrivilegeToGroup(groupName, privilegename))
                    {
                        MessageBoxCustom.Show(LL.GetLocalizedString("_SuccessGrantPrivilege"), _title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        var result = MessageBoxCustom.Show(Program.LL.GetLocalizedString("_RebootPCNowDialog"), _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        Console.ReadLine();
                    }
                    else
                    {
                        MessageBoxCustom.Show(LL.GetLocalizedString("_ErrorGrantPrivilege"), _title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(0);
                    }
                }
            }

            if (!NoRootkitCheck)
            {
                mk.DetectRk();
            }

            if (!no_runtime)
            {
                mk.Scan();
            }

            if (!no_scantime)
            {
                mk.StaticScan();
            }

            if (pause)
            {
                LocalizedLogger.LogPAUSE();
                Console.ReadKey(true);
            }
            mk.Clean();

            if (!nosignaturescan)
            {
               mk.SignatureScan();
            }
            GC.Collect();

            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(totalFoundThreats, totalNeutralizedThreats + totalFoundThreats, totalFoundSuspiciousObjects);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

            Utils.SwitchMouseSelection(true);
            Logger.DisposeLogger();

            if (!silent)
            {
                Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);

                FinishEx finish = new FinishEx(totalFoundThreats, totalNeutralizedThreats + totalFoundThreats, totalFoundSuspiciousObjects, elapsedTime) //+ sign because neutralized threats is negative
                {
                    TopMost = true
                };
                finish.LoadResults(MinerSearch.scanResults);
                finish.ShowDialog();

                Console.ReadLine();
            }

            Environment.Exit(0);
        }
        private static void WaterMark()
        {
            if (RunAsSystem)
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