using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// Анализирует правила брандмауэра.
    /// </summary>
    internal sealed class FirewallThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.FirewallRule;

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
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanFirewall");
                        _headerLogged = true;
                    }
                }
            }

            FirewallRuleThreatObject rule = threat as FirewallRuleThreatObject;
            if (rule == null) yield break;

            int risk = 0;
            bool isMalicious = false;

            if (IsKnownMaliciousFile(FileSystemManager.NormalizeExtendedPath(rule.ApplicationName)))
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_FirewallRule_Matched", $"{rule.RuleName} → {rule.ApplicationName}", ConsoleColor.Gray);
                risk += 3;
                isMalicious = true;
                rule.ShouldDelete = true;
            }

            if (risk == 0) yield break;

            ScanObjectType objType = isMalicious ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            yield return new ThreatDecision(rule, risk, objType);
        }

        private bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
