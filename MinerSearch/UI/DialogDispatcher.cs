using MSearch.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    public static class DialogDispatcher
    {
        public static DialogResult Show(string message)
        {
            return Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, null);
        }

        public static DialogResult Show(string message, string title)
        {
            return Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None, null);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons)
        {
            return Show(message, title, buttons, MessageBoxIcon.None, null);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(message, title, buttons, icon, null);
        }

        public static DialogResult Show(string message, string title, Color color)
        {
            return Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None, color);
        }

        public static DialogResult Show(string message, string title, Color color, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(message, title, buttons, icon, color);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, Color? color)
        {
            if (AppConfig.Instance.console_mode)
            {
                return ShowConsoleFallback(message, title, buttons, icon);
            }

            if (color == null)
                return MessageBoxCustom.Show(message, title, buttons, icon);
            else
                return MessageBoxCustom.Show(message, title, color.Value, buttons, icon);
        }

        private static DialogResult ShowConsoleFallback(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColorFromIcon(icon);

            try
            {
                if (!string.IsNullOrWhiteSpace(title))
                    Console.WriteLine("[" + title + "] " + message);
                else
                    Console.WriteLine(message);

                Console.ForegroundColor = originalColor;

                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        return DialogResult.OK;

                    case MessageBoxButtons.OKCancel:
                        if (!AppConfig.Instance.IsGuiAvailable) return DialogResult.None;
                        return Prompt(new[] { "OK", "Cancel" }, new[] { DialogResult.OK, DialogResult.Cancel });

                    case MessageBoxButtons.YesNo:
                        if (!AppConfig.Instance.IsGuiAvailable) return DialogResult.None;
                        return Prompt(new[] { "Yes", "No" }, new[] { DialogResult.Yes, DialogResult.No });

                    case MessageBoxButtons.YesNoCancel:
                        if (!AppConfig.Instance.IsGuiAvailable) return DialogResult.None;
                        return Prompt(new[] { "Yes", "No", "Cancel" }, new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel });

                    default:
                        Console.WriteLine("Unsupported dialog type.");
                        return DialogResult.None;
                }
            }
            catch
            {
                return DialogResult.None;
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }

        private static DialogResult Prompt(string[] optionsText, DialogResult[] results)
        {
            Console.WriteLine("Options: " + string.Join(" / ", optionsText));
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (input == null) return DialogResult.None;

                input = input.Trim().ToLowerInvariant();

                for (int i = 0; i < optionsText.Length; i++)
                {
                    if (input == optionsText[i].ToLowerInvariant())
                        return results[i];
                }

                Console.WriteLine("Invalid input. Try again.");
            }
        }

        private static ConsoleColor GetColorFromIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    return ConsoleColor.Cyan;
                case MessageBoxIcon.Warning:
                    return ConsoleColor.Yellow;
                case MessageBoxIcon.Error:
                    return ConsoleColor.Red;
                case MessageBoxIcon.Question:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}
