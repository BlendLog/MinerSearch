using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// Сканирует папки автозагрузки Shell (User Startup + Common Startup).
    /// Создаёт ShellStartupFileThreatObject для каждого найденного файла.
    /// </summary>
    public class ShellStartupScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            List<IThreatObject> results = new List<IThreatObject>();

            string[] wellKnownStartupFolders =
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup),
            };

            foreach (string wkFolder in wellKnownStartupFolders)
            {
                if (string.IsNullOrEmpty(wkFolder)) continue;
                if (!Directory.Exists(wkFolder)) continue;

                // Прямой перебор файлов — FileEnumerator может пропускать файлы с невидимыми символами
                EnumerateFolder(results, wkFolder, 0, 3);
            }

            return results;
        }

        private void EnumerateFolder(List<IThreatObject> results, string folder, int depth, int maxDepth)
        {
            if (depth > maxDepth) return;
            if (!Directory.Exists(folder)) return;

            try
            {
                // Файлы в текущей папке
                foreach (string file in Directory.EnumerateFiles(folder, "*.*", SearchOption.TopDirectoryOnly))
                {
                    string uncFile = FileSystemManager.NormalizeExtendedPath(file);

                    // Пропускаем .ini файлы (как в оригинальном ScanShellStartup)
                    if (string.Equals(Path.GetExtension(uncFile), ".ini", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (!File.Exists(uncFile)) continue;

                    string fileName = Path.GetFileName(uncFile);
                    string extension = Path.GetExtension(uncFile)?.ToLowerInvariant();
                    bool hasInvisibleChars = FileSystemManager.IsOnlyInvisibleCharacters(Path.GetFileNameWithoutExtension(uncFile));
                    bool isSfx = FileChecker.IsSfxArchive(uncFile);
                    bool hasHiddenAttr = FileSystemManager.HasHiddenAttribute(uncFile);

                    string shortcutTargetPath = null;
                    string shortcutTargetArgs = null;
                    FileThreatObject shortcutTargetFile = null;

                    // Резолвим ярлык
                    bool isShortcut = string.Equals(extension, ".lnk", StringComparison.OrdinalIgnoreCase);
                    if (isShortcut)
                    {
                        var shortcutInfo = ShortcutResolver.GetShortcutInfo(uncFile);
                        shortcutTargetPath = shortcutInfo.TargetPath;
                        shortcutTargetArgs = shortcutInfo.Arguments;

                        // Создаём FileThreatObject для цели ярлыка
                        if (!string.IsNullOrEmpty(shortcutTargetPath))
                        {
                            string resolvedPath = Environment.ExpandEnvironmentVariables(shortcutTargetPath.Replace("\"", ""));
                            resolvedPath = FileSystemManager.NormalizeExtendedPath(resolvedPath);

                            if (File.Exists(resolvedPath))
                            {
                                try
                                {
                                    var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(resolvedPath);
                                    var fileInfo = new FileInfo(resolvedPath);
                                    var versionInfo = FileVersionInfo.GetVersionInfo(resolvedPath);

                                    shortcutTargetFile = new FileThreatObject(
                                        resolvedPath,
                                        Path.GetFileName(resolvedPath),
                                        fileInfo.Length,
                                        versionInfo.OriginalFilename ?? string.Empty,
                                        versionInfo.FileDescription ?? string.Empty,
                                        FileChecker.CalculateSHA1(resolvedPath),
                                        trust);
                                }
                                catch (Exception) { /* Игнорируем ошибки доступа */ }
                            }
                        }
                    }

                    // Проверяем .NET + высокая энтропия (только для не-ярлыков)
                    bool isDotNetHighEntropy = false;
                    if (!isShortcut)
                    {
                        var trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(uncFile);
                        if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown)
                        {
                            try
                            {
                                if (FileChecker.IsDotNetAssembly(uncFile))
                                {
                                    double entropy = FileChecker.CalculateShannonEntropy(File.ReadAllBytes(uncFile));
                                    if (entropy > 7.6)
                                    {
                                        isDotNetHighEntropy = true;
                                    }
                                }
                            }
                            catch (Exception) { /* Игнорируем */ }
                        }
                    }

#if DEBUG
                    Console.WriteLine($"[DBG] ShellStartupScanner: {uncFile}\n" +
                        $"FileName: {fileName}\n" +
                        $"Extension: {extension}\n" +
                        $"Target: {shortcutTargetPath}\n" +
                        $"Args: {shortcutTargetArgs}\n" +
                        $"HasInvisibleChars: {hasInvisibleChars}\n" +
                        $"isSfx: {isSfx}\n" +
                        $"Hidded: {hasHiddenAttr}\n" +
                        $"IsDotNet: {isDotNetHighEntropy}\n" +
                        $"----------------------------------");
#endif
                    results.Add(new ShellStartupFileThreatObject(
                        uncFile,
                        fileName,
                        extension,
                        shortcutTargetPath,
                        shortcutTargetArgs,
                        hasInvisibleChars,
                        isSfx,
                        hasHiddenAttr,
                        isDotNetHighEntropy,
                        shortcutTargetFile));
                }

                // Рекурсивно обходим подпапки
                foreach (string subDir in Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly))
                {
                    if (FileSystemManager.IsReparsePoint(subDir)) continue;
                    EnumerateFolder(results, subDir, depth + 1, maxDepth);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception) { }
        }
    }
}
