using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System.Collections.Generic;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// SRP: Анализирует файл hosts, принимает решения.
    /// Если файл заражён — маркирует его как ScanObjectType.Infected.
    /// НЕ увеличивает счётчики — это делает CleanManager.
    /// </summary>
    public sealed class HostsThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Hosts;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();
        LaunchOptions _options = LaunchOptions.GetInstance;

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var hostsThreat = threat as HostsThreatObject;
            if (hostsThreat == null)
                yield break;

            // Заголовок логируем один раз
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanHosts");
                        _headerLogged = true;
                    }
                }
            }

            if (hostsThreat.InfectedLinesCount == 0) yield break;

            AppConfig.GetInstance.LL.LogCautionMessage("_InfectedHosts", hostsThreat.HostsFilePath);

            if (!_options.ScanOnly)
            {
                hostsThreat.ShouldQuarantineFile = true;
                hostsThreat.ShouldRemoveInfectedLines = true;
                AppConfig.GetInstance.LL.LogSuccessMessage("_FileCopy_MarkedToMoveQuarantine", hostsThreat.HostsFilePath);
            }

            // Возвращаем решение: файл hosts целиком как угроза типа Infected
            var decision = new ThreatDecision(hostsThreat, riskLevel: 3, ScanObjectType.Infected);

            yield return decision;
        }
    }
}
