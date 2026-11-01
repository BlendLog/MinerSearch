using System;
using System.IO;
using System.Text;
using Win32Wrapper;

namespace MSearch
{
    /// <summary>
    /// Console bridge for the WinExe build: attaches to the parent console,
    /// creates a new one if needed and reopens the standard streams.
    /// </summary>
    internal static class ConsoleBridge
    {
        private const int StdOutputHandle = -11;

        public static bool IsAttached { get; private set; }

        /// <summary>
        /// Whether stdin/stdout are connected to a file or pipe
        /// rather than a console (I/O redirection).
        /// </summary>
        public static bool HasRedirectedStdio()
        {
            try
            {
                IntPtr stdout = Native.GetStdHandle(StdOutputHandle);
                uint type = Native.GetFileType(stdout);
                return type == Native.FILE_TYPE_DISK || type == Native.FILE_TYPE_PIPE;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attaches to the parent process console or creates a new one.
        /// </summary>
        public static void EnsureConsole()
        {
            if (Native.GetConsoleWindow() != IntPtr.Zero)
            {
                IsAttached = true;
                return;
            }

            if (HasRedirectedStdio())
            {
                IsAttached = false;
                return;
            }

            bool attached = false;
            try
            {
                attached = Native.AttachConsole(Native.ATTACH_PARENT_PROCESS);
            }
            catch { }

            if (!attached)
            {
                try
                {
                    attached = Native.AllocConsole();
                }
                catch { }
            }

            IsAttached = attached || Native.GetConsoleWindow() != IntPtr.Zero;

            if (!IsAttached)
                return;

            try
            {
                Encoding outputEncoding = GetConsoleEncoding(Native.GetConsoleOutputCP());
                Encoding inputEncoding = GetConsoleEncoding(Native.GetConsoleCP());

                StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput(), outputEncoding);
                stdout.AutoFlush = true;
                Console.SetOut(stdout);

                StreamWriter stderr = new StreamWriter(Console.OpenStandardError(), outputEncoding);
                stderr.AutoFlush = true;
                Console.SetError(stderr);

                StreamReader stdin = new StreamReader(Console.OpenStandardInput(), inputEncoding);
                Console.SetIn(stdin);
            }
            catch { }
        }

        private static Encoding GetConsoleEncoding(uint codePage)
        {
            if (codePage == 65001)
                return new UTF8Encoding(false);

            try
            {
                if (codePage != 0)
                    return Encoding.GetEncoding((int)codePage);
            }
            catch { }

            return Console.OutputEncoding;
        }

        public static void Hide()
        {
            IntPtr handle = Native.GetConsoleWindow();
            if (handle != IntPtr.Zero)
                Native.ShowWindow(handle, Native.SW_HIDE);
        }

        public static void SafeSetTitle(string title)
        {
            try
            {
                if (Native.GetConsoleWindow() != IntPtr.Zero)
                    Console.Title = title;
            }
            catch { }
        }

        public static void SafeReadKey()
        {
            SafeReadKey(false);
        }

        public static void SafeReadKey(bool intercept)
        {
            try
            {
                if (Native.GetConsoleWindow() != IntPtr.Zero)
                    Console.ReadKey(intercept);
            }
            catch { }
        }
    }
}
