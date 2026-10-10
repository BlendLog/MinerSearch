using DBase;
using MSearch;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// Анализирует файлы из папки автозагрузки Shell.
    /// Ярлыки: анализирует ShortcutTargetFile через SignatureFileAnalyzer.
    /// Обычные файлы: прогоняет через SignatureFileAnalyzer для определения действия.
    /// </summary>
    internal sealed class ShellStartupThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.ShellStartupFile;

        private readonly SignatureFileAnalyzer _fileAnalyzer;

        public ShellStartupThreatAnalyzer(SignatureFileAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
        }

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        // ...\Microsoft\Windows\Caches\<8hex>\RuntimeHost.exe
        private static readonly Regex WindowsCachesRegex = new Regex(
            MSData.GetInstance.regexPatterns[MSKeys.WinCaches],
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanStarupFolder");
                        _headerLogged = true;
                    }
                }
            }

            ShellStartupFileThreatObject file = threat as ShellStartupFileThreatObject;
            if (file == null) yield break;

            int risk = 0;
            bool isMalicious = false;

            // Логирование найденного файла
            if (file.IsShortcut)
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_Shortcut", $"{file.FileName} --> {file.ShortcutTargetPath} {file.ShortcutTargetArgs}", ConsoleColor.Gray);
            }
            else
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_File", file.FileName, ConsoleColor.Gray);
            }

            if (file.IsShortcut)
            {
                AnalyzeShortcut(file, ref risk, ref isMalicious);

                if (IsMshtaShortcut(file, out string htaPayload))
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                    risk += 3;
                    isMalicious = true;
                    file.ShouldMoveToQuarantine = true;

                    if (!string.IsNullOrEmpty(htaPayload) && File.Exists(htaPayload))
                    {
                        FileThreatObject htaFile = CreateFileObject(htaPayload);
                        if (htaFile != null)
                        {
                            FileChecker.LogUnsignedSha1(htaFile);
                            htaFile.ShouldMoveFileToQuarantine = true;
                            yield return new ThreatDecision(htaFile, risk, ScanObjectType.Malware);
                        }
                    }
                }
            }
            else
            {
                AnalyzeRegularFile(file, ref risk, ref isMalicious);
            }

            if (risk == 0) yield break;

            ScanObjectType objType = isMalicious || risk >= 3
                ? ScanObjectType.Malware
                : ScanObjectType.Suspicious;

            // Решение для основного объекта (файл из автозагрузки)
            yield return new ThreatDecision(file, risk, objType);

            // Решение для ShortcutTargetFile (цель ярлыка) — если есть флаги действия
            if (file.ShortcutTargetFile != null &&
                (file.ShortcutTargetFile.ShouldDeleteFile ||
                 file.ShortcutTargetFile.ShouldMoveFileToQuarantine ||
                 file.ShortcutTargetFile.ShouldDisableExecute))
            {
                yield return new ThreatDecision(file.ShortcutTargetFile, risk, objType);
            }
        }

        /// <summary>
        /// Анализ ярлыка (.lnk). Проверяет целевой путь, аргументы и файл-цель.
        /// </summary>
        private void AnalyzeShortcut(ShellStartupFileThreatObject file, ref int risk, ref bool isMalicious)
        {
            // 1. Явные маркеры: cmd.exe /c или невидимые символы в имени
            bool isCmdSlashC = file.ShortcutTargetPath?.EndsWith(MSData.GetInstance.hostNames[MSKeys.CmdExe], StringComparison.OrdinalIgnoreCase) == true
                               && file.ShortcutTargetArgs?.StartsWith(MSData.GetInstance.consts[MSKeys.SlashCArg], StringComparison.OrdinalIgnoreCase) == true;

            if (isCmdSlashC || file.HasInvisibleChars)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk += 3;
                isMalicious = true;
                file.ShouldDeleteFile = true;
            }

            if (HasMicrosoftCaches(file.ShortcutTargetPath))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk += 3;
                isMalicious = true;
                file.ShouldDeleteFile = true;

                if (file.ShortcutTargetFile != null)
                    file.ShortcutTargetFile.ShouldDeleteFile = true;
            }

            // 2. Аргументы содержат вредоносные паттерны
            if (!string.IsNullOrEmpty(file.ShortcutTargetArgs))
            {
                foreach (string badArg in MSData.GetInstance.badArgStrings)
                {
                    if (file.ShortcutTargetArgs.IndexOf(badArg, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                        risk += 3;
                        isMalicious = true;
                        file.ShouldDeleteFile = true;
                        break;
                    }
                }
            }

            // 3. Анализ целевого файла ярлыка через SignatureFileAnalyzer
            if (file.ShortcutTargetFile != null)
            {
                var fileResult = _fileAnalyzer.Analyze(file.ShortcutTargetFile, false);

                // Логируем отсутствие подписи у цели
                if (file.ShortcutTargetFile.TrustResult == WinVerifyTrustResult.FileNotSigned)
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_CertFileNotSigned", file.ShortcutTargetFile.FilePath);
                }

                FileChecker.LogUnsignedSha1(file.ShortcutTargetFile);

                if (fileResult.IsMalicious)
                {
                    risk += 3;
                    isMalicious = true;
                    // Цель ярлыка — вредонос → удаляем ярлык И целевой файл
                    file.ShouldDeleteFile = true;
                    file.ShortcutTargetFile.ShouldDeleteFile = true;
                }
            }
            else if (!string.IsNullOrEmpty(file.ShortcutTargetPath))
            {
                // Сканер не смог создать ShortcutTargetFile — цель не существует
                string targetPath = Environment.ExpandEnvironmentVariables(file.ShortcutTargetPath.Replace("\"", ""));
                targetPath = FileSystemManager.NormalizeExtendedPath(targetPath);

                if (!File.Exists(targetPath))
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", file.ShortcutTargetPath);
                }
            }
        }

        /// <summary>
        /// Анализ обычного файла (не ярлык). Проверяет подозрительные признаки,
        /// затем прогоняет через SignatureFileAnalyzer для точного определения.
        /// </summary>
        private void AnalyzeRegularFile(ShellStartupFileThreatObject file, ref int risk, ref bool isMalicious)
        {
            // Подозрительные символы и скрытый атрибут — повод для тревоги независимо от подписи
            if (file.HasInvisibleChars || file.HasHiddenAttr)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk += 3;
                isMalicious = true;
                file.ShouldMoveToQuarantine = true;
            }

            // SFX — проверяем подпись для принятия решения
            if (file.IsSfx)
            {
                var sfxLevel = FileChecker.GetSfxTrustLevel(file.FilePath);
                
                switch (sfxLevel)
                {
                    case SfxTrustLevel.BadCert:
                        // Подписан, но сертификат проблемный — пониженный риск
                        AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", file.FilePath);
                        risk += 1;
                        if (!file.ShouldMoveToQuarantine)
                            file.ShouldMoveToQuarantine = true;
                        break;

                    case SfxTrustLevel.SignedValid:
                        // Валидная подпись — просто лог, не карантиним автоматически
                        AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", file.FilePath);
                        break;

                    default:
                        // Unsigned, Unknown или NotSfx — игнорируем
                        break;
                }
            }

            // .NET с высокой энтропией — дополнительный маркер
            if (file.IsDotNetHighEntropy)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk = Math.Max(risk, 3);
                isMalicious = true;
                if (!file.ShouldMoveToQuarantine)
                    file.ShouldMoveToQuarantine = true;
            }

            // Прогоняем файл через SignatureFileAnalyzer для точного определения
            long fileSize = 0;
            var trustResult = WinVerifyTrustResult.ActionUnknown;
            try
            {
                fileSize = new FileInfo(file.FilePath).Length;
                trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(file.FilePath, true);
            }
            catch (Exception) { }

            var tempFileObj = new FileThreatObject(
                file.FilePath,
                file.FileName,
                fileSize,
                string.Empty,
                string.Empty,
                string.Empty,
                trustResult);

            FileChecker.LogUnsignedSha1(tempFileObj);

            var fileResult = _fileAnalyzer.Analyze(tempFileObj, false);

            if (fileResult.IsMalicious)
            {
                risk = Math.Max(risk, 3);
                isMalicious = true;

                // Файл определён как вредоносный → удаляем (если известен по obfStr2) или карантин

                if (IsKnownMaliciousFile(file.FilePath))
                {
                    file.ShouldDeleteFile = true;
                }
                else
                {
                    file.ShouldMoveToQuarantine = true;
                }
            }
        }

        private bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.IsKnownMaliciousPath(filePath);
        }

        private static bool IsMshtaShortcut(ShellStartupFileThreatObject file, out string htaPayload)
        {
            htaPayload = null;

            string targetPath = file.ShortcutTargetPath ?? string.Empty;
            string targetArgs = file.ShortcutTargetArgs ?? string.Empty;

            string targetName = string.Empty;
            try
            {
                targetName = System.IO.Path.GetFileName(targetPath.Trim().Trim('"'));
            }
            catch { }

            string mshtaName = MSData.GetInstance.SysFileName[39];

            bool isMshta = targetName.Equals(mshtaName + ".exe", StringComparison.OrdinalIgnoreCase) ||
                           targetPath.IndexOf(mshtaName, StringComparison.OrdinalIgnoreCase) >= 0;

            bool hasPayload = MSData.ContainsAnyMarker(targetArgs, MSData.GetInstance.markerSets[MSKeys.ScriptMarkers]) ||
                              MSData.ContainsAnyMarker(targetPath, MSData.GetInstance.markerSets[MSKeys.ScriptMarkers]) ||
                              MSData.ContainsAnyMarker(targetArgs, MSData.GetInstance.markerSets[MSKeys.HtaMarkers]);

            if (!isMshta && !hasPayload)
                return false;

            Match m = Regex.Match(targetArgs, MSData.GetInstance.regexPatterns[MSKeys.HtaPath], RegexOptions.IgnoreCase);
            if (m.Success)
            {
                htaPayload = Environment.ExpandEnvironmentVariables(m.Value.Trim().Trim('"'));
            }

            return true;
        }

        private static FileThreatObject CreateFileObject(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return null;

                var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(path, true);
                var fileInfo = new FileInfo(path);
                var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(path);

                return new FileThreatObject(
                    path,
                    System.IO.Path.GetFileName(path),
                    fileInfo.Length,
                    versionInfo.OriginalFilename ?? string.Empty,
                    versionInfo.FileDescription ?? string.Empty,
                    FileChecker.CalculateSHA1(path),
                    trust);
            }
            catch
            {
                return null;
            }
        }

        private static bool HasMicrosoftCaches(string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath)) return false;

            string path = targetPath.Trim().Trim('"').Replace('/', '\\');

            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) path = path.Substring(4);
            else if (path.StartsWith(@"\??\", StringComparison.Ordinal)) path = path.Substring(4);
            else if (path.StartsWith(@"\\.\", StringComparison.Ordinal)) path = path.Substring(4);
            else if (path.StartsWith(@"\\", StringComparison.Ordinal)) path = path.Substring(2);

            return WindowsCachesRegex.IsMatch(path);
        }
    }
}
