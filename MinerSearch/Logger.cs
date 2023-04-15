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
        public static readonly ConsoleColor cautionLow = ConsoleColor.DarkYellow;
        public static string previousWhiteText = "";
        public static string previousNonWhiteText = "";
        public static ConsoleColor previousColor;

        public static void WriteLog(string currentText, ConsoleColor LogLevel)
        {

            if (currentText == previousWhiteText || currentText == previousNonWhiteText)
            {
                return;
            }

            try
            {
                string logMessage = $"[{DateTime.Now}]: {currentText}";

                Console.ForegroundColor = LogLevel;
                Console.WriteLine(logMessage);
                Console.ForegroundColor = ConsoleColor.White;

                if (LogLevel == ConsoleColor.White)
                {
                    previousWhiteText = currentText;
                }
                if (LogLevel != ConsoleColor.White)
                {
                    previousNonWhiteText = currentText;
                }

                if (!Program.no_logs)
                {
                    using (StreamWriter writer = new StreamWriter(logFileName, true))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
#if DEBUG
                Console.Write($"\tLogger error: {ex.Message} \n{ex.StackTrace}");
#else
                Console.Write($"\tLogger error: {ex.Message}");
#endif
            }
        }
    }
}
