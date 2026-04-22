using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;

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

            if (fileThreat.SourceTag == "unsigned_sfx")
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_SuspiciousFile", filePath);
                fileThreat.ShouldMoveFileToQuarantine = true;

                var decision = new ThreatDecision(fileThreat, riskLevel: 2, ScanObjectType.Malware);
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
    }
}
