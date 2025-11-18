using MSearch.Core;
using System;
using System.IO;
using System.Threading;

namespace MSearch
{
    public static class Logger
    {
        internal static string logFileName = $"MinerSearch_{DateTime.Now:dd.MM.yyyy_HH-mm-ss}.log";
        public static string LogsFolder = Path.Combine(AppConfig.Instance.drive_letter + ":\\", "_MinerSearchLogs");

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

        public static void InitLogger(bool logSupression)
        {
            if (!Directory.Exists(Logger.LogsFolder))
            {
                try
                {
                    Directory.CreateDirectory(Logger.LogsFolder);
                    File.SetAttributes(Logger.LogsFolder, FileAttributes.Normal);
                }
                catch (Exception)
                {
                    Logger.LogsFolder += Utils.GetRndString(16);
                    Directory.CreateDirectory(Logger.LogsFolder);
                    File.SetAttributes(Logger.LogsFolder, FileAttributes.Normal);
                }
            }
            else
            {
                bool resetAclSuccessful = UnlockObjectClass.ResetObjectACL(Logger.LogsFolder);
                bool attributesSetSuccessful = false;

                if (resetAclSuccessful)
                {
                    try
                    {
                        File.SetAttributes(Logger.LogsFolder, FileAttributes.Normal);
                        attributesSetSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Warning: Failed to reset attributes for {Logger.LogsFolder}. {ex.Message}");
                    }
                }

                if (!resetAclSuccessful || !attributesSetSuccessful)
                {
                    Logger.LogsFolder += Utils.GetRndString(16);
                    Directory.CreateDirectory(Logger.LogsFolder);
                    File.SetAttributes(Logger.LogsFolder, FileAttributes.Normal);
                }
            }

            if (!logSupression)
            {
                _writer = new StreamWriter(Path.Combine(LogsFolder, logFileName), true, new System.Text.UTF8Encoding(false));
                _writer.AutoFlush = true;
            }
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
            if (!AppConfig.Instance.verbose)
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
                    if (!AppConfig.Instance.silent)
                    {
                        Console.ForegroundColor = LogLevel;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!AppConfig.Instance.verbose)
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


                    if (!AppConfig.Instance.no_logs)
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
                if (!AppConfig.Instance.verbose)
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

                    if (!AppConfig.Instance.silent)
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!ignorePrevious)
                    {
                        if (!AppConfig.Instance.verbose)
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

                    if (!AppConfig.Instance.no_logs)
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
            if (!AppConfig.Instance.verbose)
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

                    if (!AppConfig.Instance.silent)
                    {

                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }



                    if (!AppConfig.Instance.verbose)
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


                    if (!AppConfig.Instance.no_logs)
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

                    if (!AppConfig.Instance.no_logs)
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

                    if (!AppConfig.Instance.no_logs || force)
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
