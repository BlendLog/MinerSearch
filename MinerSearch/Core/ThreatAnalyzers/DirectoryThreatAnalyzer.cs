using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// SRP: Анализирует каталоги, принимает решения об удалении.
    /// НЕ увеличивает счётчики — это делает CleanManager.
    /// </summary>
    public sealed class DirectoryThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Directory;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var dirThreat = threat as DirectoryThreatObject;
            if (dirThreat == null) yield break;

            // Заголовок логируем один раз при первом каталоге
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanDirectories");
                        _headerLogged = true;
                    }
                }
            }

            string dirPath = dirThreat.DirectoryPath;

            if (string.IsNullOrEmpty(dirPath))
                yield break;

            // === РЕШЕНИЕ 1: Каталог из obfStr1 (100% вредоносный) ===
            if (dirThreat.SourceTag == "obfStr1")
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_MaliciousDir", dirPath);

                dirThreat.ShouldDeleteDirectory = true;

                var decision = new ThreatDecision(dirThreat, riskLevel: 3, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Deleted;
                yield return decision;

                yield break;
            }

            // === РЕШЕНИЕ 2: Заблокированный каталог (нужно разблокировать и удалить) ===
            if (dirThreat.SourceTag == "locked")
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_LockedDirectory", dirPath);

                dirThreat.ShouldUnlockAndDelete = true;

                var decision = new ThreatDecision(dirThreat, riskLevel: 2, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Deleted;
                yield return decision;

                yield break;
            }

            // === РЕШЕНИЕ 3: Пустой каталог (autologger, av_block_remover) — удалить ===
            if (dirThreat.SourceTag == "empty")
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", dirPath);

                dirThreat.ShouldDeleteDirectory = true;

                var decision = new ThreatDecision(dirThreat, riskLevel: 2, ScanObjectType.Malware);
                decision.ActionType = ScanActionType.Deleted;
                yield return decision;

                yield break;
            }
        }
    }
}
