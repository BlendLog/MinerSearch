using System;
using System.IO;
using System.Threading;

namespace MSearch
{
    public static class Logger
    {
        internal static string logFileName = $"M?in?er?Sea?rch_{DateTime.Now}.log".Replace("/", "_").Replace("?", "").Replace(":", "-").Replace(" ", "_");
        public static string LogsFolder = Path.Combine(Program.drive_letter + ":\\", "_Mi?n?erSe?a?rchLogs".Replace("?", ""));

        public static readonly ConsoleColor error = ConsoleColor.Red;
        public static readonly ConsoleColor success = ConsoleColor.Green;
        public static readonly ConsoleColor warn = ConsoleColor.Yellow;
        public static readonly ConsoleColor head = ConsoleColor.Cyan;
        public static readonly ConsoleColor caution = ConsoleColor.Magenta;
        public static readonly ConsoleColor warnMedium = ConsoleColor.DarkYellow;
        public static string previousWhiteText = "";
        public static string previousNonWhiteText = "";
        public static ConsoleColor previousColor;

        internal static readonly object _logLock = new object();
        static StreamWriter _writer;

        public static void InitLogger()
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
            else if (!UnlockObjectClass.ResetObjectACL(Logger.LogsFolder))
            {
                Logger.LogsFolder += Utils.GetRndString(16);
                Directory.CreateDirectory(Logger.LogsFolder);
            }
            _writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true, System.Text.Encoding.UTF8);
            _writer.AutoFlush = true;
        }

        public static void DisposeLogger()
        {
            lock (_logLock)
            {
                _writer?.Dispose();
            }
        }

        public static void WriteLog(string currentText, ConsoleColor LogLevel)
        {
            if (!Program.verbose)
            {
                if (currentText == previousWhiteText || currentText == previousNonWhiteText)
                {
                    return;
                }
            }

            try
            {

                string logMessage = $"[{DateTime.Now}]: {currentText}";


                lock (_logLock)
                {
                    if (!Program.silent)
                    {
                        Console.ForegroundColor = LogLevel;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!Program.verbose)
                    {
                        if (LogLevel == ConsoleColor.White)
                        {
                            previousWhiteText = currentText;
                        }
                        if (LogLevel != ConsoleColor.White)
                        {
                            previousNonWhiteText = currentText;
                        }
                    }


                    if (!Program.no_logs)
                    {
                        _writer.WriteLine(logMessage);
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
                if (!Program.verbose)
                {
                    if (currentText == previousWhiteText || currentText == previousNonWhiteText)
                    {
                        return;
                    }
                }
            }

            try
            {
                string logMessage = "";

                lock (_logLock)
                {

                    if (DisplayTime)
                    {
                        logMessage = $"[{DateTime.Now}]: {currentText}";
                    }
                    else
                        logMessage = currentText;

                    if (!Program.silent)
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!ignorePrevious)
                    {
                        if (!Program.verbose)
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
                    }

                    if (!Program.no_logs)
                    {
                        _writer.WriteLine(logMessage);
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
            if (!Program.verbose)
            {
                if (currentText == previousWhiteText || currentText == previousNonWhiteText)
                {
                    return;
                }
            }

            try
            {
                string logMessage = "";

                lock (_logLock)
                {

                    if (DisplayTime)
                    {
                        logMessage = $"[{DateTime.Now}]: {currentText}";
                    }
                    else
                        logMessage = currentText;

                    if (!Program.silent)
                    {

                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }



                    if (!Program.verbose)
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
                        _writer.WriteLine(logMessage);
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

                lock (_logLock)
                {

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
                        _writer.WriteLine(logMessage);
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

        public static void WriteLog(string currentText, bool WriteOnly = false, bool DisplayTime = false, bool force = false)
        {
            try
            {
                string logMessage = "";

                lock (_logLock)
                {

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

                    if (!Program.no_logs || force)
                    {
                        _writer.WriteLine(logMessage);
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
