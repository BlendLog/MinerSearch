using System;
using System.IO;

namespace MSearch
{
    public static class Logger
    {
        internal static string logFileName = $"M?in?er?Sea?rch_{DateTime.Now}.log".Replace("/", "_").Replace("?", "").Replace(":", "-").Replace(" ", "_");
        public static string LogsFolder = Path.Combine(Program.drive_letter + ":\\", "_Mi?n?erSe?a?rchLogs".Replace("?", ""));
        public static string LogID = Utils.GetRndString(32).ToUpper();

        public static readonly ConsoleColor error = ConsoleColor.Red;
        public static readonly ConsoleColor success = ConsoleColor.Green;
        public static readonly ConsoleColor warn = ConsoleColor.Yellow;
        public static readonly ConsoleColor head = ConsoleColor.Cyan;
        public static readonly ConsoleColor caution = ConsoleColor.Magenta;
        public static readonly ConsoleColor warnMedium = ConsoleColor.DarkYellow;
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
                    using (StreamWriter writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true))
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

        public static void WriteLog(string currentText, ConsoleColor color, bool DisplayTime = true, bool ignorePrevious = false)
        {
            if (!ignorePrevious)
            {
                if (currentText == previousWhiteText || currentText == previousNonWhiteText)
                {
                    return;
                }
            }

            try
            {
                string logMessage = "";
                if (DisplayTime)
                {
                    logMessage = $"[{DateTime.Now}]: {currentText}";
                }
                else
                    logMessage = currentText;


                Console.ForegroundColor = color;
                Console.WriteLine(logMessage);
                Console.ForegroundColor = ConsoleColor.White;

                if (!ignorePrevious)
                {
                    if (color == ConsoleColor.White)
                    {
                        previousWhiteText = currentText;
                    }
                    if (color != ConsoleColor.White)
                    {
                        previousNonWhiteText = currentText;
                    }
                }

                if (!Program.no_logs)
                {
                    using (StreamWriter writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true))
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

        public static void WriteLog(string currentText, ConsoleColor color, bool DisplayTime = true)
        {

            if (currentText == previousWhiteText || currentText == previousNonWhiteText)
            {
                return;
            }

            try
            {
                string logMessage = "";
                if (DisplayTime)
                {
                    logMessage = $"[{DateTime.Now}]: {currentText}";
                }
                else
                    logMessage = currentText;


                Console.ForegroundColor = color;
                Console.WriteLine(logMessage);
                Console.ForegroundColor = ConsoleColor.White;

                if (color == ConsoleColor.White)
                {
                    previousWhiteText = currentText;
                }
                if (color != ConsoleColor.White)
                {
                    previousNonWhiteText = currentText;
                }

                if (!Program.no_logs)
                {
                    using (StreamWriter writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true))
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

        public static void WriteLog(string currentText, bool WriteOnly = false, bool DisplayTime = false)
        {
            try
            {
                string logMessage = "";
                if (DisplayTime)
                {
                    logMessage = $"[{DateTime.Now}]: {currentText}";
                }
                else
                    logMessage = currentText;

                if (!WriteOnly)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(logMessage);
                    Console.ResetColor();
                }

                if (!Program.no_logs)
                {
                    using (StreamWriter writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"\tLogger error: {ex.Message} \n{ex.StackTrace}");
#endif
            }
        }
    }
}
