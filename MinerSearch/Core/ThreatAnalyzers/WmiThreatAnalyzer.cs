using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// SRP: Анализирует WMI Event Consumers.
    /// Все CommandLineEventConsumer в root\subscription считаются вредоносными.
    /// </summary>
    public sealed class WmiThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.WmiSubscription;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var wmi = threat as WmiSubscriptionThreatObject;
            if (wmi == null) yield break;

            // Заголовок логируем один раз при первом объекте
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_WMIHead");
                        _headerLogged = true;
                    }
                }
            }

            AppConfig.GetInstance.LL.LogWarnMediumMessage("_WMIEvent", $"{wmi.Name} -> {wmi.CommandLineTemplate}");

            // Все WMI Event Consumers в root\subscription считаются вредоносными
            var decision = new ThreatDecision(wmi, riskLevel: 3, ScanObjectType.Malware);
            decision.ActionType = ScanActionType.Deleted;
            yield return decision;
        }
    }
}
