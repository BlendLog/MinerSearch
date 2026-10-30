using MSearch.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MSearch
{
    public static class Logger
    {
        private const string LogsFolderName = "_MinerSearchLogs";
        private const int MaxStartupLogBufferChars = 512 * 1024;

        static string _logsFolder;
        static string _pendingLogWarning;
        static readonly List<string> _startupLogLines = new List<string>();
        static int _startupLogBufferChars;
        static bool _captureStartupLog;

        /// <summary>
        /// Папка логов. Вычисляется лениво (drive_letter в WinPE задаётся позже старта),
        /// создаётся только при начале сканирования.
        /// </summary>
        public static string LogsFolder
        {
            get { return _logsFolder ?? Path.Combine(AppConfig.GetInstance.drive_letter + ":\\", LogsFolderName); }
            private set { _logsFolder = value; }
        }

        /// <summary>Путь к логу последнего сканирования; пусто, если сканов ещё не было.</summary>
        public static string LogFilePath { get; private set; }

        /// <summary>Есть ли готовый лог последнего скана (файл существует).</summary>
        public static bool HasScanLog
        {
            get { return !string.IsNullOrEmpty(LogFilePath) && File.Exists(LogFilePath); }
        }


        public static readonly ConsoleColor error = ConsoleColor.Red;
        public static readonly ConsoleColor success = ConsoleColor.Green;
        public static readonly ConsoleColor warn = ConsoleColor.Yellow;
        public static readonly ConsoleColor head = ConsoleColor.Cyan;
        public static readonly ConsoleColor caution = ConsoleColor.Magenta;
        public static readonly ConsoleColor warnMedium = ConsoleColor.DarkYellow;
        public static string previousWhiteText = "";
        public static string previousNonWhiteText = "";
        public static ConsoleColor previousColor;

        static bool _no_logs = LaunchOptions.GetInstance.no_logs;

        internal static readonly object _logLock = new object();
        static StreamWriter _writer;

        /// <summary>
        /// Настраивает логирование на сессию. Файл здесь НЕ создаётся:
        /// лог открывается только на время сканирования (BeginScanLog/EndScanLog).
        /// </summary>
        public static void InitLogger(bool logSupression)
        {
            _no_logs = logSupression;
        }

        /// <summary>
        /// Буферизует заголовок старого сессионного лога до открытия первого лога сканирования.
        /// </summary>
        internal static void StartHeaderCapture()
        {
            lock (_logLock)
            {
                _startupLogLines.Clear();
                _startupLogBufferChars = 0;
                _captureStartupLog = !_no_logs;
            }
        }

        /// <summary>
        /// Открывает новый файл лога для очередного сканирования.
        /// </summary>
        public static void BeginScanLog()
        {
            if (_no_logs)
            {
                lock (_logLock)
                    _captureStartupLog = false;
                return;
            }

            lock (_logLock)
            {
                _captureStartupLog = false;
                CloseWriter();

                LogFilePath = null;
                _pendingLogWarning = null;
                ResetDedup();

                string path = CreateLogFile();
                if (path == null)
                    return;

                try
                {
                    _writer = new StreamWriter(path, false, new System.Text.UTF8Encoding(false));
                    _writer.AutoFlush = true;

                    foreach (string startupLine in _startupLogLines)
                        _writer.WriteLine(startupLine);

                    LogFilePath = path;
                }
                catch (Exception ex)
                {
                    CloseWriter();
                    WarnNoLogFile(ex.Message);
                    return;
                }

                if (!string.IsNullOrEmpty(_pendingLogWarning))
                    WritePlainLine("Warning: " + _pendingLogWarning);

                _pendingLogWarning = null;
            }
        }

        /// <summary>
        /// Закрывает файл лога по завершении сканирования. Путь остаётся доступен для просмотра.
        /// </summary>
        public static void EndScanLog()
        {
            lock (_logLock)
            {
                if (_writer == null)
                    return;

                try
                {
                    _writer.Flush();
                }
                catch (Exception)
                {
                }

                CloseWriter();
            }
        }

        /// <summary>
        /// Готовит папку логов: основной диск (drive_letter), при неудаче — рядом с приложением.
        /// </summary>
        private static string PrepareLogsFolder()
        {
            string primary = Path.Combine(AppConfig.GetInstance.drive_letter + ":\\", LogsFolderName);

            if (TryPrepareFolder(primary))
            {
                LogsFolder = primary;
                return primary;
            }

            string fallback = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogsFolderName);
            _pendingLogWarning = "log folder " + primary + " is unavailable; using " + fallback;

            if (TryPrepareFolder(fallback))
            {
                LogsFolder = fallback;
                return fallback;
            }

            WarnNoLogFile("log folder " + primary + " and fallback " + fallback + " are unavailable");
            return null;
        }

        private static bool TryPrepareFolder(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    File.SetAttributes(folder, FileAttributes.Normal);
                    return true;
                }

                if (!UnlockObjectClass.ResetObjectACL(folder))
                    return false;

                File.SetAttributes(folder, FileAttributes.Normal);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string CreateLogFile()
        {
            string folder = PrepareLogsFolder();
            if (folder == null)
                return null;

            string baseName = "MinerSearch_" + DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss");
            string path = Path.Combine(folder, baseName + ".log");

            int suffix = 1;
            while (File.Exists(path))
            {
                suffix++;
                path = Path.Combine(folder, baseName + "_" + suffix + ".log");
            }

            return path;
        }

        private static void WarnNoLogFile(string reason)
        {
            string message = "Warning: scan log file is not created";
            if (!string.IsNullOrEmpty(reason))
                message += " (" + reason + ")";

            WriteLog(message, warnMedium);
        }

        private static void WritePlainLine(string text)
        {
            if (_writer == null)
                return;

            _writer.WriteLine($"[{DateTime.Now}]: {text}");
        }

        /// <summary>
        /// Пишет в активный лог или буферизует строки заголовка до начала сканирования.
        /// Вызывающий код должен удерживать _logLock.
        /// </summary>
        private static void WriteLogLine(string logMessage, bool force = false)
        {
            if (_writer != null)
            {
                if (!_no_logs || force)
                    _writer.WriteLine(logMessage);
                return;
            }

            if (_no_logs || !_captureStartupLog)
                return;

            int lineChars = logMessage.Length + Environment.NewLine.Length;
            if (lineChars > MaxStartupLogBufferChars - _startupLogBufferChars)
                return;

            _startupLogLines.Add(logMessage);
            _startupLogBufferChars += lineChars;
        }

        private static void ResetDedup()
        {
            previousWhiteText = "";
            previousNonWhiteText = "";
        }

        private static void CloseWriter()
        {
            if (_writer == null)
                return;

            try
            {
                _writer.Dispose();
            }
            catch (Exception)
            {
            }

            _writer = null;
        }


        public static void DisposeLogger()
        {
            lock (_logLock)
            {
                _writer?.Dispose();
                _writer = null;
            }
        }

        public static void WriteLog(string currentText, ConsoleColor LogLevel)
        {
            if (!LaunchOptions.GetInstance.verbose)
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
                    if (!LaunchOptions.GetInstance.silent)
                    {
                        Console.ForegroundColor = LogLevel;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!LaunchOptions.GetInstance.verbose)
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


                    WriteLogLine(logMessage);
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
                if (!LaunchOptions.GetInstance.verbose)
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

                    if (!LaunchOptions.GetInstance.silent)
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (!ignorePrevious)
                    {
                        if (!LaunchOptions.GetInstance.verbose)
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

                    WriteLogLine(logMessage);
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
            if (!LaunchOptions.GetInstance.verbose)
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

                    if (!LaunchOptions.GetInstance.silent)
                    {

                        Console.ForegroundColor = color;
                        Console.WriteLine(logMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                    }



                    if (!LaunchOptions.GetInstance.verbose)
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


                    WriteLogLine(logMessage);
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

                    WriteLogLine(logMessage);
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

                    WriteLogLine(logMessage, force);
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
