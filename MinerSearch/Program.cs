using System;

namespace MinerSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Miner Search";

            WaterMark();

            MinerSearch mk = new MinerSearch();
            Logger.LogHeader("Preparing to scan processes, please wait...");
            mk.Scan();
            Console.WriteLine("\n");
            Logger.LogHeader("Starting static scan...");
            mk.StaticScan();

            if (mk.malware_pids.Count == 0 && mk.suspiciousObj_count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The system is clean. No suspicious files or processes have been detected!");
            }
            else
            {
                if (mk.malware_pids.Count > 0)
                {
                    Logger.LogCaution($"Malicious processes: {mk.malware_pids.Count}");
                }
                if (mk.suspiciousObj_count > 0)
                {
                    Logger.LogWarn($"Suspicious objects: {mk.suspiciousObj_count}");
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nPress enter to start cleanup...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
                Logger.LogHeader("Starting clean...");
                mk.Clean();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("All Done. You can close this window");
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

            Console.WriteLine("\t\tby: BlendLog");
            Console.WriteLine("\t\tbased on: MinerKiller\n\n");
        }

    }
}