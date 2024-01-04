//#define BETA

using Microsoft.Win32;
using MinerSearch.Properties;
using System;
using System.Diagnostics;
using System.IO;
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

            Console.BufferWidth = 150;
            Console.BufferHeight = 100;

            int desiredConsoleWidth = Console.LargestWindowWidth - 10;
            int desiredConsoleHeight = Console.LargestWindowHeight - 10;

            Console.SetWindowSize(desiredConsoleWidth, desiredConsoleHeight);
            Console.SetWindowPosition(0, 0);

            WaterMark();

            ActiveLanguage = utils.GetSystemLanguage();

#if !BETA
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)}");
#else
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)} Beta");
#endif
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


            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    arg.ToLower();
                    if (arg == "--help")
                    {
                        help = true;
                        Console.WriteLine("Available commands: \n");
                        Console.WriteLine("--help                        This help message");
                        Console.WriteLine("--no-logs                     Don't write logs in text file");
                        Console.WriteLine("--no-scantime                 Scan processes only");
                        Console.WriteLine("--no-runtime                  Static scan only (Mal?wa?re dirs, files, registry keys, etc)".Replace("?", ""));
                        Console.WriteLine("--no-services                 Skip scan services");
                        Console.WriteLine("--no-signature-scan           Skip scan files by signatures");
                        Console.WriteLine("--no-ro?o?t?ki?t-check            Skip checking ro?ot?kit present".Replace("?", ""));
                        Console.WriteLine("--depth=<number>              Where <number> specify the number for maximum search depth. Usage example --depth=5 (default 8)");
                        Console.WriteLine("--pause                       Pause before cleanup");
                        Console.WriteLine("--remove-empty-tasks          Delete a task from the Task Scheduler if the application file does not exist in it");
                        Console.WriteLine("--winpemode                   Start scanning in WinPE environment by specifying a different drive letter (without scanning processes, registry, firewall and task scheduler entries)");
                        Console.WriteLine("--scan-only                   Display mali?cious or suspicious objects, but do nothing".Replace("?", ""));
                        Console.WriteLine("--full-scan                   Add other entire local drives for signature scan");
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
            Logger.WriteLog($"\t\tPC Name: {Environment.MachineName}\n", ConsoleColor.DarkGray, false);
            utils.CheckStartupCount();

            var pmodules = Process.GetCurrentProcess().Modules;
            foreach (var module in pmodules)
            {
                if (module.ToString().Contains(Bfs.GetStr(@"䖜䖒䖛䖉䖍䖋䗉䗋䗑䖛䖓䖓", 17919))) //cmdvrt64.dll
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(Bfs.Create("Wk3B63KEJuzwmN0LiXM9ckEhNM1GOtFvmi0Iq15YBmY6jjx35GP8EyqZ3vID0NZ/tAmbk0QE+9WUB7wYqjq3zfz2zksQ9g/oYISfmofjOz0zSboixjeuxhvLU8V4AkjZ",
            new byte[] { 0x19, 0x7b, 0xab, 0x8a, 0x3c, 0xc9, 0xc2, 0xe7, 0x35, 0xc2, 0xcb, 0x29, 0x3d, 0x7f, 0x7a, 0x25, 0xcf, 0x3b, 0x26, 0xe5, 0x36, 0x4b, 0x16, 0xa9, 0x49, 0x32, 0xe0, 0x4a, 0xe7, 0x98, 0x50, 0x99 },
            new byte[] { 0x33, 0xe8, 0xc3, 0x09, 0x7b, 0xa6, 0xed, 0xb3, 0x6d, 0x6e, 0xe1, 0x7a, 0x86, 0xe6, 0x65, 0x23 })); //[!!!!] COMODO VIRTUAL ENVIRONMENT DETECTED! Please, add program to white list!
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
            Console.WriteLine(@"                                                         Beta");
#endif
            Console.WriteLine("\t\tby: Bl~end~L~og".Replace("~", ""));

        }

    }
}