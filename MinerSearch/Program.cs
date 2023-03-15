using System;
using System.Diagnostics;

namespace MinerSearch
{
    public class Program
    {
        public static bool nologs = false;

        static void Main(string[] args)
        {


            Console.Title = GetRandomTitle("miner search");
            WaterMark();

            if (args.Length > 0)
            {
                if (args[0].ToLower() == "--no-logs")
                {
                    nologs = true;
                }
                else
                {
                    Console.WriteLine("\n Unknown argument");
                    Console.ReadKey();
                    return;
                }
            }

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            MinerSearch mk = new MinerSearch();
            Logger.WriteLog("Preparing to scan processes, please wait...", Logger.head);

            mk.Scan();
            Console.WriteLine("\n");
            Logger.WriteLog("Starting static scan...", Logger.head);
            mk.StaticScan();

            if (mk.malware_pids.Count == 0 && mk.suspiciousObj_count == 0 && !mk.CleanupHosts)
            {
                Logger.WriteLog("[+] The system is clean. No malicious files or processes have been detected!", Logger.success);
            }
            else
            {
                if (mk.malware_pids.Count > 0)
                {
                    Logger.WriteLog($"\t[!!!] Malicious processes: {mk.malware_pids.Count}", Logger.caution);
                }
                if (mk.suspiciousObj_count > 0)
                {
                    Logger.WriteLog($"\t[!] Suspicious objects: {mk.suspiciousObj_count}", Logger.warn);
                }
                mk.Clean();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("All Done. You can close this window");
            Console.Read();
        }

        static string GetRandomTitle(string inputString)
        {
            Random random = new Random();
            string result = "";
            foreach (char chr in inputString)
            {
                int caseNumber = random.Next(2);
                if (caseNumber == 0)
                    result += Char.ToLower(chr);
                else result += Char.ToUpper(chr);
            }
            return result;
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
            Console.WriteLine("\t\tbased on: MinerKiller");
            Console.WriteLine($"\t\tVersion: {new Version(System.Windows.Forms.Application.ProductVersion)}\n");
            Console.WriteLine($"\tFor disable logging in file use \"--no-logs\" option\n");
        }

    }
}