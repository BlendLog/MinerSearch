using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            bool isCmdSlashC = file.ShortcutTargetPath?.EndsWith("cmd.exe", StringComparison.OrdinalIgnoreCase) == true
                               && file.ShortcutTargetArgs?.StartsWith("/c ", StringComparison.OrdinalIgnoreCase) == true;

            if (isCmdSlashC || file.HasInvisibleChars)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk += 3;
                isMalicious = true;
                file.ShouldDeleteFile = true;
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
                    Logger.WriteLog($"\t\t[SHA1: {file.ShortcutTargetFile.Hash}]", ConsoleColor.White, false);
                }

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
            // Подозрительные признаки сами по себе — повод для тревоги
            if (file.HasInvisibleChars || file.IsSfx || file.HasHiddenAttr)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", file.FilePath);
                risk += 3;
                isMalicious = true;
                file.ShouldMoveToQuarantine = true;
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
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
