//#define BETA

using Microsoft.Win32;
using MinerSearch.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace MinerSearch
{
    public class Program
    {
        public static bool no_logs = false;
        public static bool no_scantime = false;
        public static bool no_runtime = false;
        public static bool pause = false;
        public static bool help = false;
        public static bool RemoveEmptyTasks = false;
        public static bool nosignaturescan = false;
        public static bool WinPEMode = false;
        public static bool NoRootkitCheck = false;
        public static int maxSubfolders = 8;
        public static string drive_letter = "C";
        public static string ActiveLanguage = utils.GetSystemLanguage();

        static void Main(string[] args)
        {
            WaterMark();


#if !BETA
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)}");
#else
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)} Beta");
#endif
#if !DEBUG
            Console.WriteLine($"\t\tRelevant versions on https://github.com/BlendLog/MinerSearch/releases/latest \n");
#endif

            const string registryKeyPath = @"Software\MinerSearch";
            const string registryValueName = "acceptedEula";

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                var value = key.GetValue(registryValueName);
                if (value == null)
                {
                    License licenseForm = new License();
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
            



            InitPrivileges();

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
                        Console.WriteLine("--no-runtime                  Static scan only (Malware dirs, files, registry keys, etc)");
                        Console.WriteLine("--no-signature-scan           Skip scanning files by signatures");
                        Console.WriteLine("--no-rootkit-check            Skip checking rootkit present");
                        Console.WriteLine("--depth=<number>              Where <number> specify the number for maximum search depth. Usage example --depth=5 (default 8)");
                        Console.WriteLine("--pause                       Pause before cleanup");
                        Console.WriteLine("--remove-empty-tasks          Delete a task from the Task Scheduler if the application file does not exist in it");
                        Console.WriteLine("--winpemode                   Start scanning in WinPE environment by specifying a different drive letter (without scanning processes, registry, firewall and task scheduler entries)");
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
                    else if (arg == "--no-rootkit-check")
                    {
                        NoRootkitCheck = true;
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
                        Logger.WriteLog($"\t\t[&] Activated WinPE mode. Specified drive letter - {drive_letter}:\\", ConsoleColor.DarkCyan, false);
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

            Console.Title = utils.GetRndString();
            Logger.WriteLog($"\t\tWindows version: {utils.GetWindowsVersion()} {utils.getBitVersion()}", ConsoleColor.White, false);
            Logger.WriteLog($"\t\tUsername: {Environment.UserName}", ConsoleColor.DarkGray, false);
            Logger.WriteLog($"\t\tPC Name: {Environment.MachineName}\n", ConsoleColor.DarkGray, false);
            utils.CheckStartupCount();

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
                Logger.WriteLog($"\t[!!!] Malicious processes: {mk.mlwrPids.Count}", Logger.caution);
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
            Console.Read();
        }

        private static void WaterMark()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"  __  __ _                   ____                      _     ");
            Console.WriteLine(@" |  \/  (_)_ __   ___ _ __  / ___|  ___  __ _ _ __ ___| |__  ");
            Console.WriteLine(@" | |\/| | | '_ \ / _ | '__| \___ \ / _ \/ _` | '__/ __| '_ \ ");
            Console.WriteLine(@" | |  | | | | | |  __| |     ___) |  __| (_| | | | (__| | | |");
            Console.WriteLine(@" |_|  |_|_|_| |_|\___|_|    |____/ \___|\__,_|_|  \___|_| |_|");
#if !BETA
            Console.WriteLine(@"                                                             ");
#else
            Console.WriteLine(@"                                                         Beta");
#endif
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\tby: BlеndLоg");

        }
        static void InitPrivileges()
        {

            IntPtr token;
            if (WinApi.OpenProcessToken(Process.GetCurrentProcess().Handle, WinApi.TOKEN_ADJUST_PRIVILEGES | WinApi.TOKEN_QUERY, out token))
            {
                try
                {
                    var seSecurityLuid = new WinApi.LUID();
                    if (WinApi.LookupPrivilegeValue(null, WinApi.SE_SECURITY_NAME, out seSecurityLuid))
                    {
                        var seTakeOwnershipLuid = new WinApi.LUID();
                        if (WinApi.LookupPrivilegeValue(null, WinApi.SE_TAKE_OWNERSHIP_NAME, out seTakeOwnershipLuid))
                        {
                            var tokenPrivileges = new WinApi.TOKEN_PRIVILEGES
                            {
                                PrivilegeCount = 2,
                                Privileges = new WinApi.LUID_AND_ATTRIBUTES[2]
                                {
                        new WinApi.LUID_AND_ATTRIBUTES { Luid = seSecurityLuid, Attributes = WinApi.SE_PRIVILEGE_ENABLED },
                        new WinApi.LUID_AND_ATTRIBUTES { Luid = seTakeOwnershipLuid, Attributes = WinApi.SE_PRIVILEGE_ENABLED }
                                }
                            };
                            if (!WinApi.AdjustTokenPrivileges(token, false, ref tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
                            {
                                Logger.WriteLog("Failed to enable both SeSecurityPrivilege and SeTakeOwnershipPrivilege with error code: " + Marshal.GetLastWin32Error(), Logger.error, false);
                            }
                        }
                        else
                        {
                            Logger.WriteLog("Failed to lookup SeTakeOwnershipPrivilege with error code: " + Marshal.GetLastWin32Error(), Logger.error, false);
                        }
                    }
                    else
                    {
                        Logger.WriteLog("Failed to lookup SeSecurityPrivilege with error code: " + Marshal.GetLastWin32Error(), Logger.error, false);
                    }
                }
                finally
                {
                    WinApi.CloseHandle(token);
                }
            }
            else
            {
                Console.WriteLine("Failed to get process handle with error code: " + Marshal.GetLastWin32Error());
            }
        }
    }
}