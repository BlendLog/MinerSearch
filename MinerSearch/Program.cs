//#define BETA

using DBase;
using Microsoft.Win32;
using MSearch.Properties;
using netlib;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        public static int maxSubfolders = 8;
        public static int totalFoundThreats = 0;
        public static int totalNeutralizedThreats = 0;
        public static string drive_letter = "C";
        public static string ActiveLanguage = "";
        internal static BootMode bootMode = Utils.GetBootMode();
        public static LocalizedLogger LL = new LocalizedLogger();
        public static Utils _utils = new Utils();

        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExeptionHandler.HookExeption);
            ActiveLanguage = Utils.GetSystemLanguage();
            try
            {
                Init(args);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(LL.GetLocalizedString("_ErrorNotFoundComponent"), Utils.GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

        }

        static void Init(string[] args)
        {
            if (!Utils.IsDotNetInstalled())
            {
                MessageBox.Show(LL.GetLocalizedString("_ErrorNoDotNet"), Utils.GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }


            if (!Utils.IsOneAppCopy())
            {
                MessageBox.Show(LL.GetLocalizedString("_AppAlreadyRunning"), Console.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                Environment.Exit(1);
            }

            Console.Title = Utils.GetRndString();

            var bitmap = (Bitmap)Utils.GetSmallWindowIcon(Process.GetCurrentProcess().MainWindowHandle);
            Random rnd = new Random();
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            Utils.SetConsoleWindowIcon(bitmap);

            if (Screen.PrimaryScreen.Bounds.Width > 1024 && Screen.PrimaryScreen.Bounds.Height > 634)
            {
                if (Console.LargestWindowWidth >= 150)
                {
                    Console.SetWindowSize(150, 40);
                    WaterMark();
                }
            }
            Logger.WriteLog("\t\tLogID:" + Logger.LogID, true, false);

#if !DEBUG
            LL.LogJustDisplayMessage("\t\t", $"_RelevantVer", "https://github.com/BlendLog/Mi?ne?rSea?rch/releases \n".Replace("?", ""), ConsoleColor.White);
#endif

            if (Utils.IsStartedFromArchive())
            {
                MessageBox.Show(LL.GetLocalizedString("_ArchiveWarn"), LL.GetLocalizedString("_ArchiveWarn_caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            licenseForm.richTextBox1.Text = Resources._License_EN;
                            licenseForm.Accept_btn.Text = Resources._accept_en;
                            licenseForm.Exit_btn.Text = Resources._exit_en;
                        }
                        if (ActiveLanguage == "RU")
                        {

                            licenseForm.richTextBox1.Text = Resources._License_RU;
                            licenseForm.Accept_btn.Text = Resources._accept_ru;
                            licenseForm.Exit_btn.Text = Resources._exit_ru;
                        }

                        licenseForm.ShowDialog();
                    }
                }
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    arg.ToLower();
                    if (arg == "--help")
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

                    if (arg == "--no-logs")
                    {
                        no_logs = true;
                    }
                    else if (arg == "--no-scantime")
                    {
                        no_scantime = true;
                    }
                    else if (arg == "--no-signature-scan")
                    {
                        nosignaturescan = true;
                    }
                    else if (arg == "--no-runtime")
                    {
                        no_runtime = true;
                    }
                    else if (arg == "--no-scan-tasks")
                    {
                        no_scan_tasks = true;
                    }
                    else if (arg == "--pause")
                    {
                        pause = true;
                    }
                    else if (arg == "--remove-empty-tasks")
                    {
                        RemoveEmptyTasks = true;
                    }
                    else if (arg.StartsWith("--depth="))
                    {
                        int.TryParse(arg.Remove(0, 8), out maxSubfolders);
                        if (maxSubfolders <= 0 || maxSubfolders > 16)
                        {
                            Console.WriteLine($"\nThe number in {arg} command cannot be negative, = 0 or > 16");
                            Console.ReadLine();
                            return;
                        }
                    }
                    else if (arg == "--no-roo?tki?t-check".Replace("?", ""))
                    {
                        NoRootkitCheck = true;
                    }
                    else if (arg == "--no-services")
                    {
                        no_services = true;
                    }
                    else if (arg == "--scan-only")
                    {
                        ScanOnly = true;
                        NoRootkitCheck = true;
                        RemoveEmptyTasks = false;

                    }
                    else if (arg == "--full-scan")
                    {
                        fullScan = true;
                    }
                    else if (arg.StartsWith("--restore="))
                    {
                        var path = arg.Remove(0, 10);
                        if (File.Exists(path))
                        {
                            Utils.RestoreFromQuarantine(path, Path.GetFileNameWithoutExtension(path).Split('_')[0], Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", "")));
                        }
                        else
                        {
                            LL.LogWarnMessage("_FileIsNotFound", path);
                        }
                        return;
                    }
                    else if (arg == "--winpemode")
                    {
                    drive_letter_lbl:
                        LocalizedLogger.LogSpecifyDrive();
                        drive_letter = Console.ReadLine();
                        if (drive_letter.Length > 1 || Utils.IsDigit(drive_letter))
                        {
                            LocalizedLogger.LogIncorrectDrive(drive_letter);
                            Console.ReadKey();
                            goto drive_letter_lbl;
                        }
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
                drive_letter = Environment.GetEnvironmentVariable("systemdrive").Remove(1);
                Drive.Letter = drive_letter;
            }

#if !DEBUG
            if (!help)
            {
                Utils.SwitchMouseSelection();
            }

#endif

            if (!no_logs)
            {
                if (!Directory.Exists(Logger.LogsFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(Logger.LogsFolder);
                    }
                    catch (IOException)
                    {
                        Logger.LogsFolder += Utils.GetRndString(16);
                        Directory.CreateDirectory(Logger.LogsFolder);
                    }
                }
            }

            LL.LogMessage("\t\t", "_Version", new Version(Application.ProductVersion).ToString(), ConsoleColor.White, false);


            if (!help)
            {
                LocalizedLogger.LogHelpHint();
            }

            if (no_runtime && no_scantime && WinPEMode)
            {
                LocalizedLogger.LogErrorDisabledScan();
                Console.ReadKey();
                return;
            }

            if (WinPEMode && nosignaturescan)
            {
                nosignaturescan = false;
            }


            LocalizedLogger.LogPCInfo(Utils.GetWindowsVersion(), Environment.UserName, Environment.MachineName, bootMode);
            Utils.CheckStartupCount();

            ProcessModuleCollection pmodules = Process.GetCurrentProcess().Modules;
            foreach (var module in pmodules)
            {
                if (module.ToString().Contains("c~md~vrt~6~4.d~ll".Replace("~", "")))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please, add the program to ant1v1rus whitelist!");
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.ReadLine();
                    Environment.Exit(10);
                }
            }
            pmodules = null;

            _utils.CheckWMI();

            Stopwatch startTime = Stopwatch.StartNew();

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;


            MinerSearch mk = new MinerSearch();
            LL.LogHeadMessage("_PreparingToScan");
            Process.EnterDebugMode();

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
            if (mk.mlwrPids.Count > 0)
            {
                LL.LogCautionMessage("_MlwrProcessesCount", mk.mlwrPids.Count.ToString());
            }
            mk.Clean();

            if (!nosignaturescan)
            {
                mk.SignatureScan();
            }

            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogElapsedTime(elapsedTime);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            LocalizedLogger.LogTotalScanResult(totalFoundThreats, totalNeutralizedThreats + totalFoundThreats);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);

            if (!no_logs)
            {
                LL.LogWriteWithoutDisplay(true, false);
            }

            Utils.SwitchMouseSelection(true);

            Native.ShowWindow(Native.GetConsoleWindow(), Native.SW_MINIMIZE);

            Finish finish = new Finish(totalFoundThreats, totalNeutralizedThreats + totalFoundThreats, elapsedTime)
            {
                TopMost = true
            };
            finish.ShowDialog();
            Console.ReadLine();
            Environment.Exit(0);
        }
        private static void WaterMark()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

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
            Console.WriteLine("\t\tby: Bl?end??Log".Replace("?", ""));
        }
    }
}