using System;
using System.IO;

namespace MinerSearch
{
    public static class Logger
    {
        private static readonly string logFileName = $"MinerSearch_{DateTime.Now:yyyy_mm_dd_hh_ss}.log";

        public static readonly ConsoleColor error = ConsoleColor.Red;
        public static readonly ConsoleColor success = ConsoleColor.Green;
        public static readonly ConsoleColor warn = ConsoleColor.Yellow;
        public static readonly ConsoleColor head = ConsoleColor.Cyan;
        public static readonly ConsoleColor caution = ConsoleColor.Magenta;


        public static void WriteLog(string text, ConsoleColor LogLevel)
        {
            string logMessage = $"[{DateTime.Now}]: {text}";

            Console.ForegroundColor = LogLevel;
            Console.WriteLine(logMessage);
            Console.ForegroundColor = ConsoleColor.White;

            if (!Program.nologs)
            {
                using (StreamWriter writer = new StreamWriter(logFileName, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
        }

        public static void Log(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");

        }
    }
}
