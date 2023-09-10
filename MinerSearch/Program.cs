using MinerSearch.Properties;
using System;
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

        static void Main(string[] args)
        {
            InitPrivileges();

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            if (!File.Exists("Microsoft.Win32.TaskScheduler.dll"))
            {
                try
                {
                    File.WriteAllBytes("Microsoft.Win32.TaskScheduler.dll", Resources.TaskScheduler);
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"Scan Task error: {ex.Message}", Logger.error);
                }
            }

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    switch (arg.ToLower().Trim())
                    {
                        case "--help":
                            help = true;
                            Console.WriteLine("Available commands: \n");
                            Console.WriteLine("--help                        This help message");
                            Console.WriteLine("--no-logs                     Don't write logs in text file");
                            Console.WriteLine("--no-scantime                 Scan processes only");
                            Console.WriteLine("--no-runtime                  Static scan only (Malware dirs, files, registry keys, etc)");
                            Console.WriteLine("--no-signature-scan           Skip scanning files by signatures");
                            Console.WriteLine("--pause                       Pause before cleanup");
                            Console.WriteLine("--remove-empty-tasks          Delete a task from the Task Scheduler if the application file does not exist in it");
                            Console.ReadKey();
                            return;
                        case "--no-logs":
                            no_logs = true;
                            break;
                        case "--no-scantime":
                            no_scantime = true;
                            break;
                        case "--no-runtime":
                            no_runtime = true;
                            break;
                        case "--pause":
                            pause = true;
                            break;
                        case "--remove-empty-tasks":
                            RemoveEmptyTasks = true;
                            break;
                        case "--no-signature-scan":
                            nosignaturescan = true;
                            break;
                        default:
                            Console.WriteLine("\nUnknown command");
                            Console.ReadKey(true);
                            return;
                    }
                }
            }

            if (!help)
            {
                Console.WriteLine($"\n\tUse \"--help\" option to display list of available commands\n");
            }

            if (no_runtime && no_scantime)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: you cannot disable both types of scanning");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return;
            }

            Console.Title = utils.GetHash();
            WaterMark();

            utils.CheckWMI();

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            MinerSearch mk = new MinerSearch();

            if (!no_runtime)
            {
                Logger.WriteLog("\t\tPreparing to scan processes, please wait...", Logger.head, false);
                mk.Scan();
            }
            if (!no_scantime)
            {
                Logger.WriteLog("\t\tStarting static scan...", Logger.head, false);
                mk.StaticScan();
            }

            int warningsCount = mk.malware_pids.Count + mk.founded_malwarePaths.Count + mk.founded_suspiciousLockedPaths.Count;
            if (warningsCount == 0 && !mk.CleanupHosts)
            {
                Logger.WriteLog("\t[+] The system is clean. No malicious files or processes have been detected!", Logger.success, false);
            }
            else
            {
                if (mk.malware_pids.Count > 0)
                {
                    Logger.WriteLog($"\t[!!!] Malicious processes: {mk.malware_pids.Count}", Logger.caution);
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
                mk.Clean();
            }
            Logger.WriteLog("\tAll Done. You can close this window", ConsoleColor.DarkCyan, false);
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
            Console.WriteLine(@"                                                             ");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t\tby: BlendLog, Spectrum735");
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)}\n");
            Console.WriteLine($"\t\tRelevant versions on https://github.com/BlendLog/MinerSearch/releases/latest \n");

            Logger.WriteLog($"\t\tWindows version: {utils.GetWindowsVersion()} {utils.getBitVersion()}\n", ConsoleColor.White, false);
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