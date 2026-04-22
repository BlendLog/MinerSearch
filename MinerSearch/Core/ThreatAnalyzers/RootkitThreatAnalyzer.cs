using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// Анализатор для руткит-угроз.
    /// Поскольку RootkitThreatScanner уже выполняет мгновенную обработку,
    /// этот анализатор только маркирует угрозу и возвращает решение об удалении.
    /// </summary>
    internal sealed class RootkitThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Other;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var rootkit = threat as RootkitThreatObject;
            if (rootkit == null) yield break;

            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_R00TkitDetected");
                        _headerLogged = true;
                    }
                }
            }

            // Руткит — всегда критическая угроза (riskLevel = 10)
            int riskLevel = 10;
            ScanObjectType scanType = ScanObjectType.Rootkit;

            yield return new ThreatDecision(threat, riskLevel, scanType);

            // Если конфигурация не была удалена — маркируем для удаления
            if (!rootkit.ConfigRemoved)
            {
                yield return new ThreatDecision(threat, riskLevel, scanType);
            }
        }
    }
}
