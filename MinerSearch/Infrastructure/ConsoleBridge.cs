using System;
using System.IO;
using System.Text;
using Win32Wrapper;

namespace MSearch
{
    /// <summary>
    /// Мост к консоли для WinExe-сборки: подключается к родительской консоли,
    /// создаёт новую при необходимости и переоткрывает стандартные потоки.
    /// </summary>
    internal static class ConsoleBridge
    {
        private const int StdOutputHandle = -11;

        public static bool IsAttached { get; private set; }

        /// <summary>
        /// Признак того, что stdin/stdout подключены к файлу или каналу,
        /// а не к консоли (перенаправление ввода-вывода).
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
        /// Подключается к консоли родительского процесса или создаёт новую.
        /// </summary>
        public static void EnsureConsole()
        {
            if (Native.GetConsoleWindow() != IntPtr.Zero)
            {
                IsAttached = true;
                return;
            }

            // Перенаправленный вывод уже работает через унаследованные handles:
            // не подменяем его консолью.
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

            // Console.Out/In могли быть закэшированы как пустые потоки до подключения.
            try
            {
                StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput(), Encoding.UTF8);
                stdout.AutoFlush = true;
                Console.SetOut(stdout);

                StreamWriter stderr = new StreamWriter(Console.OpenStandardError(), Encoding.UTF8);
                stderr.AutoFlush = true;
                Console.SetError(stderr);

                StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Encoding.UTF8);
                Console.SetIn(stdin);
            }
            catch { }
        }

        public static void Hide()
        {
            IntPtr handle = Native.GetConsoleWindow();
            if (handle != IntPtr.Zero)
                Native.ShowWindow(handle, Native.SW_HIDE);
        }

        public static void Minimize()
        {
            IntPtr handle = Native.GetConsoleWindow();
            if (handle != IntPtr.Zero)
                Native.ShowWindow(handle, Native.SW_MINIMIZE);
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
