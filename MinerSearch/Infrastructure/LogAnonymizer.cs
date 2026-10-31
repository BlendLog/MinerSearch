using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSearch
{
    internal static class LogAnonymizer
    {
        private const string UserPlaceholder = "<user>";
        private const string UploadCopyFolderPrefix = "MinerSearchUpload_";
        private const string ProfileListKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList";

        // Встроенные аккаунты и служебные профили не содержат персональных данных.
        private static readonly string[] IgnoredNames =
        {
            "Public", "Default", "Default User", "All Users", "defaultuser0", "systemprofile",
            "LocalService", "NetworkService",
            "Administrator", "Guest", "DefaultAccount", "WDAGUtilityAccount", "sshd"
        };

        public static string CreateUploadCopy(string logPath)
        {
            string folder = Path.Combine(Path.GetTempPath(), UploadCopyFolderPrefix + Guid.NewGuid().ToString("N"));
            string copyPath = Path.Combine(folder, Path.GetFileName(logPath));

            try
            {
                Directory.CreateDirectory(folder);

                List<Replacement> replacements = BuildReplacements();
                StringBuilder builder = new StringBuilder(512);

                using (StreamReader reader = new StreamReader(logPath, Encoding.UTF8, true))
                using (StreamWriter writer = new StreamWriter(copyPath, false, new UTF8Encoding(false)))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        writer.WriteLine(RedactLine(line, replacements, builder));
                }

                return copyPath;
            }
            catch
            {
                TryDelete(copyPath);
                return null;
            }
        }

        public static void TryDelete(string copyPath)
        {
            if (string.IsNullOrEmpty(copyPath))
                return;

            try
            {
                string folder = Path.GetDirectoryName(copyPath);
                if (!string.IsNullOrEmpty(folder) &&
                    Path.GetFileName(folder).StartsWith(UploadCopyFolderPrefix, StringComparison.Ordinal))
                {
                    Directory.Delete(folder, true);
                }
            }
            catch
            {
            }
        }

        private static List<Replacement> BuildReplacements()
        {
            List<Replacement> replacements = new List<Replacement>();
            HashSet<string> known = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> profileRoots = GetProfileRoots();

            foreach (string root in profileRoots)
            {
                string directory = Path.GetDirectoryName(root);
                string value = string.IsNullOrEmpty(directory)
                    ? UserPlaceholder
                    : directory.TrimEnd('\\') + Path.DirectorySeparatorChar + UserPlaceholder;

                AddReplacement(replacements, known, root, value, false);
                AddReplacement(replacements, known, Path.GetFileName(root), UserPlaceholder, true);
            }

            foreach (string name in GetAccountNames())
                AddReplacement(replacements, known, name, UserPlaceholder, true);

            replacements.Sort((left, right) => right.Text.Length.CompareTo(left.Text.Length));
            return replacements;
        }

        private static void AddReplacement(List<Replacement> replacements, HashSet<string> known, string text, string value, bool isName)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            text = text.Trim();
            if (text.Length < 2 || IsIgnored(text) || !known.Add(text))
                return;

            replacements.Add(new Replacement { Text = text, Value = value, IsName = isName });
        }

        private static List<string> GetProfileRoots()
        {
            List<string> roots = new List<string>();
            AddProfileRoot(roots, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

            try
            {
                using (RegistryKey profileList = Registry.LocalMachine.OpenSubKey(ProfileListKey))
                {
                    if (profileList != null)
                    {
                        foreach (string subKeyName in profileList.GetSubKeyNames())
                        {
                            using (RegistryKey profileKey = profileList.OpenSubKey(subKeyName))
                            {
                                if (profileKey != null)
                                    AddProfileRoot(roots, profileKey.GetValue("ProfileImagePath") as string);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return roots;
        }

        private static void AddProfileRoot(List<string> roots, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            path = path.Trim().TrimEnd('\\');
            if (path.Length < 4 || path.IndexOf('%') >= 0 || IsIgnored(Path.GetFileName(path)))
                return;

            if (!roots.Exists(root => string.Equals(root, path, StringComparison.OrdinalIgnoreCase)))
                roots.Add(path);
        }

        private static List<string> GetAccountNames()
        {
            List<string> names = new List<string> { Environment.UserName };

            try
            {
                names.AddRange(OSExtensions.GetUsers());
            }
            catch
            {
            }

            return names;
        }

        private static bool IsIgnored(string value)
        {
            foreach (string ignored in IgnoredNames)
            {
                if (string.Equals(ignored, value, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static string RedactLine(string line, List<Replacement> replacements, StringBuilder builder)
        {
            if (string.IsNullOrEmpty(line))
                return line;

            List<Match> matches = null;

            foreach (Replacement replacement in replacements)
            {
                int index = 0;
                while ((index = line.IndexOf(replacement.Text, index, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    bool isMatch = replacement.IsName
                        ? IsNameBoundary(line, index, replacement.Text.Length)
                        : IsTokenBoundary(line, index, replacement.Text.Length);

                    if (isMatch)
                    {
                        if (matches == null)
                            matches = new List<Match>();

                        matches.Add(new Match(index, replacement.Text.Length, replacement.Value));
                        index += replacement.Text.Length;
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            CollectUserProfileSegments(line, ref matches);

            if (matches == null)
                return line;

            matches.Sort((left, right) =>
            {
                int result = left.Start.CompareTo(right.Start);
                return result != 0 ? result : right.Length.CompareTo(left.Length);
            });

            builder.Clear();
            int position = 0;

            foreach (Match match in matches)
            {
                if (match.Start < position)
                    continue;

                builder.Append(line, position, match.Start - position);
                builder.Append(match.Value);
                position = match.Start + match.Length;
            }

            builder.Append(line, position, line.Length - position);
            return builder.ToString();
        }

        private static bool IsTokenBoundary(string line, int start, int length)
        {
            if (start > 0 && char.IsLetterOrDigit(line[start - 1]))
                return false;

            int end = start + length;
            return end >= line.Length || !char.IsLetterOrDigit(line[end]);
        }

        // Ловит профили, которых нет в ProfileList (удалённые аккаунты, чужие пути в задачах/реестре).
        private static void CollectUserProfileSegments(string line, ref List<Match> matches)
        {
            int index = 0;

            while ((index = line.IndexOf(@"\Users\", index, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                int start = index + 7;
                int end = start;

                while (end < line.Length && !IsPathSegmentEnd(line[end]))
                    end++;

                if (end == start)
                {
                    index = start;
                    continue;
                }

                string segment = line.Substring(start, end - start);
                if (!IsIgnored(segment))
                {
                    if (matches == null)
                        matches = new List<Match>();

                    matches.Add(new Match(start, end - start, UserPlaceholder));
                }

                index = end;
            }
        }

        private static bool IsPathSegmentEnd(char value)
        {
            return value == '\\' || value == '/' || value == '"' || value == '\'' || value == '|' ||
                   value == '<' || value == '>' || value == '*' || value == '?' || value == '\t' ||
                   value == ',' || value == ';' || value == '(' || value == ')' ||
                   value == '[' || value == ']' || value == '{' || value == '}';
        }

        // Имя аккаунта заменяется только как сегмент пути или как значение в конце строки,
        // иначе совпадения со словами сообщений ("user") портят текст.
        private static bool IsNameBoundary(string line, int start, int length)
        {
            int end = start + length;
            bool segmentEnd = end >= line.Length ||
                              line[end] == '\\' || line[end] == '/' ||
                              line[end] == '"' || line[end] == '\'';

            if (start > 0 && (line[start - 1] == '\\' || line[start - 1] == '/'))
                return segmentEnd;

            int valueEnd = end;
            while (valueEnd < line.Length && line[valueEnd] == ' ')
                valueEnd++;

            bool isValueEnd = valueEnd >= line.Length ||
                              line[valueEnd] == '|' || line[valueEnd] == '"' || line[valueEnd] == '\'' ||
                              line[valueEnd] == ')' || line[valueEnd] == ']' || line[valueEnd] == ',' || line[valueEnd] == ';';

            return isValueEnd && (start == 0 || !char.IsLetterOrDigit(line[start - 1]));
        }

        private sealed class Replacement
        {
            public string Text;
            public string Value;
            public bool IsName;
        }

        private struct Match
        {
            public readonly int Start;
            public readonly int Length;
            public readonly string Value;

            public Match(int start, int length, string value)
            {
                Start = start;
                Length = length;
                Value = value;
            }
        }
    }
}
