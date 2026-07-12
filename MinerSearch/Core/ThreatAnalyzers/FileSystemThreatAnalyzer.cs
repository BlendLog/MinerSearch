using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSearch.Core.ThreatAnalyzers
{
    public sealed class FileSystemThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.File;

        private readonly IFileContentAnalyzer _fileAnalyzer;

        public FileSystemThreatAnalyzer(IFileContentAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
        }

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            if (threat is FileThreatObject fileThreat)
            {
                foreach (var decision in AnalyzeFile(fileThreat))
                {
                    yield return decision;
                }
                yield break;
            }

            yield break;
        }

        private IEnumerable<ThreatDecision> AnalyzeFile(FileThreatObject fileThreat)
        {
            string filePath = fileThreat.FilePath;

            if (string.IsNullOrEmpty(filePath))
                yield break;

            if (string.IsNullOrEmpty(fileThreat.SourceTag))
            {
                yield break;
            }

            if (fileThreat.SourceTag == "obfStr2")
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", filePath);
                fileThreat.ShouldDeleteFile = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 3, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Deleted;
                yield return decision;
                yield break;
            }

            if (fileThreat.SourceTag == "bloated")
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", filePath);
                fileThreat.ShouldDeleteFile = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 3, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Deleted;
                yield return decision;
                yield break;
            }

            if (fileThreat.SourceTag == "bad_bat")
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);
                fileThreat.ShouldMoveFileToQuarantine = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Quarantine;
                yield return decision;
                yield break;
            }

            if (fileThreat.SourceTag == "sfx_unsigned")
            {
                // SFX без подписи — явно подозрительно, карантиним
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);
                fileThreat.ShouldMoveFileToQuarantine = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Quarantine;
                yield return decision;
                yield break;
            }

            if (fileThreat.SourceTag == "sfx_bad_cert")
            {
                // SFX с проблемным сертификатом — проверяем расположение
                bool isInSuspiciousLocation = IsInSuspiciousFileSystemPath(filePath);

                if (isInSuspiciousLocation)
                {
                    // В подозрительной папке — карантин
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);
                    fileThreat.ShouldMoveFileToQuarantine = true;

                    var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Suspicious);
                    decision.ActionType = ScanActionType.Quarantine;
                    yield return decision;
                }
                else
                {
                    // В нормальной папке — просто помечаем как подозрительный для логов
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", filePath);

                    var decision = new ThreatDecision(fileThreat, riskLevel: 1, ScanObjectType.Unsafe);
                    decision.ActionType = ScanActionType.Quarantine;
                    yield return decision;
                }
                yield break;
            }

            if (fileThreat.SourceTag == "specific_location_unsigned_exe")
            {
                // Исполняемый файл в корневой директории AppData/LocalAppData/CommonProgramFiles
                // без валидной подписи — карантин + запрет исполнения
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_UnsignedExeInSystemLocation", filePath);
                fileThreat.ShouldMoveFileToQuarantine = true;
                fileThreat.ShouldDisableExecute = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Suspicious);
                decision.ActionType = ScanActionType.Quarantine;
                yield return decision;
                yield break;
            }

            if (fileThreat.SourceTag == "signature_scan")
            {
                if (!File.Exists(filePath))
                {
                    yield break;
                }

                var sigAnalysisResult = fileThreat.AnalysisResult;

                if (sigAnalysisResult == null)
                {
                    sigAnalysisResult = _fileAnalyzer.Analyze(fileThreat, displayProgress: false);
                    fileThreat.AnalysisResult = sigAnalysisResult;
                }

                if (sigAnalysisResult.IsMalicious)
                {
                    fileThreat.ShouldMoveFileToQuarantine = true;
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);

                    var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                    decision.ActionType = ScanActionType.Quarantine;
                    yield return decision;
                }
                else if (sigAnalysisResult.IsSuspicious)
                {
                    fileThreat.ShouldMoveFileToQuarantine = true;
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);

                    var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                    decision.ActionType = ScanActionType.Quarantine;
                    yield return decision;
                }
                else if (sigAnalysisResult.IsLockedByAntivirus)
                {
                    var decision = new ThreatDecision(fileThreat, riskLevel: 1, ScanObjectType.Malware);
                    decision.ActionType = ScanActionType.LockedByAntivirus;
                    yield return decision;
                }
                yield break;
            }

            if (!File.Exists(filePath))
            {
                yield break;
            }

            var otherAnalysisResult = _fileAnalyzer.Analyze(fileThreat, displayProgress: true);
            fileThreat.AnalysisResult = otherAnalysisResult;

            if (otherAnalysisResult.IsMalicious)
            {
                bool isInKnownList = MSData.GetInstance.obfStr2.Exists(path =>
                    path.Equals(filePath, StringComparison.OrdinalIgnoreCase));

                if (isInKnownList)
                {
                    fileThreat.ShouldDeleteFile = true;
                    AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usFile", filePath);

                    var decision = new ThreatDecision(fileThreat, riskLevel: 3, ScanObjectType.Malware);
                    decision.ActionType = ScanActionType.Deleted;
                    yield return decision;
                }
                else
                {
                    fileThreat.ShouldMoveFileToQuarantine = true;
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);

                    var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                    decision.ActionType = ScanActionType.Quarantine;
                    yield return decision;
                }
            }
            else if (otherAnalysisResult.IsSuspicious)
            {
                fileThreat.ShouldMoveFileToQuarantine = true;
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);

                var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Quarantine;
                yield return decision;
            }
            else if (otherAnalysisResult.IsLockedByAntivirus)
            {
                var decision = new ThreatDecision(fileThreat, riskLevel: 1, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.LockedByAntivirus;
                yield return decision;
            }
        }

        /// <summary>
        /// Проверяет, находится ли файл в подозрительной директории.
        /// </summary>
        private static bool IsInSuspiciousFileSystemPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            var suspiciousDirs = new[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),   // %ProgramData%
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),    // %LocalAppData%
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),    // %AppData%
                $@"{Environment.GetEnvironmentVariable("SystemRoot")}\System32\config\systemprofile"
            };

            return suspiciousDirs.Any(dir => 
                !string.IsNullOrEmpty(dir) && 
                filePath.IndexOf(dir, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
