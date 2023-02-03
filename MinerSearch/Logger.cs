using System;

namespace MinerSearch
{
    class Logger
    {
        public static void Log(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");

        }

        public static void LogHeader(string text)
        {

            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;

        }

        public static void LogError(string text)
        {
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogSuccess(string text)
        {
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarn(string text)
        {
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogCaution(string text)
        {
            Console.Write($"[{DateTime.Now}]: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
