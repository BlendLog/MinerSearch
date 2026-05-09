using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;

namespace MSearch.Core.ThreatAnalyzers
{

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

            // Пропускаем легитимные WMI события — проверяем на наличие `..\` в CommandLine
            if (!IsCommandLineSuspicious(wmi.CommandLineTemplate))
            {
                yield break;
            }

            AppConfig.GetInstance.LL.LogWarnMediumMessage("_WMIEvent", $"{wmi.Name} -> {wmi.CommandLineTemplate}");

            // WMI Event Consumers с `..\` в CommandLine считаются вредоносными
            var decision = new ThreatDecision(wmi, riskLevel: 3, ScanObjectType.Malware);
            yield return decision;
        }


        private static bool IsCommandLineSuspicious(string commandLine)
        {
            return !string.IsNullOrEmpty(commandLine) && (commandLine.Contains("..\\") || commandLine.Contains("cmd.exe /c "));
        }
    }
}
