//#define BETA

using Microsoft.Win32;
using MinerSearch.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace MinerSearch
{
    public class Program
    {
        public static bool no_logs = false;
        public static bool no_scantime = false;
        public static bool no_runtime = false;
        public static bool no_services = false;
        public static bool pause = false;
        public static bool help = false;
        public static bool RemoveEmptyTasks = false;
        public static bool nosignaturescan = false;
        public static bool WinPEMode = false;
        public static bool NoRootkitCheck = false;
        public static bool ScanOnly = false;
        public static bool fullScan = false;
        public static int maxSubfolders = 8;
        public static string drive_letter = "C";
        public static string ActiveLanguage = "";

        static void Main(string[] args)
        {

            Console.Title = utils.GetRndString();

            Console.SetWindowSize(150, 40);

            WaterMark();

            ActiveLanguage = utils.GetSystemLanguage();


            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)}");


#if !DEBUG
            Console.WriteLine($"\t\tRelevant versions on https://github.com/BlendLog/MinerSearch/releases \n");
#endif

            if (utils.IsStartedFromArchive())
            {
                switch (ActiveLanguage)
                {
                    case "RU":
                        MessageBox.Show(Resources._ArchiveWarn_ru, Resources._ArchiveWarn_caption_ru, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case "EN":
                        MessageBox.Show(Resources._ArchiveWarn_en, Resources._ArchiveWarn_caption_en, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
                Environment.Exit(1);
            }

            const string registryKeyPath = @"Software\MinerSearch";
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

            if (!utils.IsOneAppCopy())
            {
                DialogResult message = DialogResult.None;
                switch (ActiveLanguage)
                {
                    case "RU":
                        message = MessageBox.Show(Resources._AppAlreadyRunning_RU, Console.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    case "EN":
                        message = MessageBox.Show(Resources._AppAlreadyRunning_EN, Console.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                }
                if (message == DialogResult.Yes)
                {
                    args = new string[] { "--help" };
                }
                else
                {
                    Environment.Exit(0);
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
                    else if (arg == "--no-logs")
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
                            utils.RestoreFromQuarantine(path, Path.GetFileNameWithoutExtension(path).Split('_')[0], Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", "")));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\t[!] File {path} is not exists");
                            Console.ResetColor();
                        }
                        return;
                    }
                    else if (arg == "--winpemode")
                    {
                    drive_letter_lbl:
                        Console.Write($"\n\t\tSpecify drive letter: ");
                        drive_letter = Console.ReadLine();
                        if (drive_letter.Length > 1 || utils.IsDigit(drive_letter))
                        {
                            Console.WriteLine($"Incorrect drive letter: {drive_letter}");
                            Console.ReadKey();
                            goto drive_letter_lbl;
                        }
                        no_runtime = true;
                        WinPEMode = true;
                        Console.WriteLine($"\t\t[&] Activated WinPE mode. Specified drive letter - {drive_letter}:\\");
                    }
                    else
                    {
                        Console.WriteLine($"\nUnknown command {arg}");
                        Console.ReadKey(true);
                        return;
                    }
                }
            }

            if (!help)
            {
                utils.SwitchMouseSelection();
            }

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
                        Logger.LogsFolder += utils.GetRndString(16);
                        Directory.CreateDirectory(Logger.LogsFolder);
                    }
                }
            }

            if (!help)
            {
                Console.WriteLine($"\t\tUse \"--help\" option to display list of available commands");
            }

            if (no_runtime && no_scantime && WinPEMode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\tError: you cannot disable all types of scanning");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return;
            }

            if (WinPEMode && nosignaturescan)
            {
                nosignaturescan = false;
            }

            Logger.WriteLog($"\t\tWindows version: {utils.GetWindowsVersion()} {utils.getBitVersion()}", ConsoleColor.White, false);
            Logger.WriteLog($"\t\tUsername: {Environment.UserName}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\tPC Name: {Environment.MachineName}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\tBoot mode: {utils.GetBootMode()}\n", ConsoleColor.DarkGray, false);
            utils.CheckStartupCount();

            var pmodules = Process.GetCurrentProcess().Modules;
            foreach (var module in pmodules)
            {
                if (module.ToString().Contains(Bfs.GetStr(@"㟴㟺㟳㟡㟥㟣㞡㞣㞹㟳㟻㟻", 14231))) //cmdvrt64.dll
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(Bfs.Create("po3O9ln0Bbpj48dNn9jFJSttgt/rlxKoSC4P9SDDh9gkcexURxlrMVwSywTp13iO2/BP2Wd6MygYFyBji0dPiW2o1PFAYQoTcknosZkmIOM=",
            new byte[] { 0x49, 0x75, 0xee, 0xf4, 0x85, 0xd9, 0x5e, 0x90, 0x39, 0x92, 0x22, 0x15, 0x60, 0x19, 0x5d, 0x06, 0xfa, 0xf1, 0xe1, 0xef, 0x6d, 0xa8, 0xfc, 0x67, 0x6b, 0xed, 0xd1, 0xe3, 0x31, 0xe5, 0x20, 0xaa },
            new byte[] { 0x02, 0x13, 0x1d, 0xa9, 0xf0, 0x7d, 0x2b, 0xda, 0xd3, 0x39, 0x5e, 0x90, 0x65, 0xa7, 0x72, 0x23 })); //[!!!!] COMODO VIRTUAL ENVIRONMENT DETECTED! Please, add program to white list!
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.ReadLine();
                    Environment.Exit(10);
                }
            }
            pmodules = null;

            utils.CheckWMI();

            Stopwatch startTime = Stopwatch.StartNew();

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            MinerSearch mk = new MinerSearch();
            Logger.WriteLog("\t\tPreparing to scan, please wait...", Logger.head, false);

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
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nPAUSE BEFORE CLEANUP");
                Console.WriteLine("Press any key to continue");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey(true);
            }
            if (mk.mlwrPids.Count > 0)
            {
                Logger.WriteLog($"\t[!!!] Ma??li??cio??us processes: {mk.mlwrPids.Count}".Replace("?", ""), Logger.caution);
            }
            mk.Clean();

            if (!nosignaturescan)
            {
                Logger.WriteLog("\t\tStarting signature scan...", Logger.head, false);
                mk.SignatureScan();
            }

            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Logger.WriteLog("\n\t\t-----------------------------------", ConsoleColor.White, false);
            Logger.WriteLog($"\t\t[$] Scan elapsed time: {elapsedTime}", ConsoleColor.White, false);
            Logger.WriteLog("\t\t-----------------------------------", ConsoleColor.White, false);
            Logger.WriteLog("\t\tAll Done. You can close this window", ConsoleColor.Cyan, false);
            mk = null;

            utils.SwitchMouseSelection(true);

            Console.Read();
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
            Console.WriteLine("\t\tby: Bl~end~L~og".Replace("~", ""));

        }
    }
}