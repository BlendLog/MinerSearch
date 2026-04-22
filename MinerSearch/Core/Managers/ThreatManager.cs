using MSearch.Core.ThreatDecisions;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.Managers
{
    public sealed class ThreatManager
    {
        private readonly Dictionary<ThreatObjectKind, IThreatAnalyzer> _analyzers;

        public ThreatManager(IEnumerable<IThreatAnalyzer> analyzers)
        {
            _analyzers = analyzers.ToDictionary(a => a.Kind);
        }

        public IEnumerable<ThreatDecision> Analyze(IEnumerable<IThreatObject> threats)
        {
            foreach (var threat in threats)
            {
                if (threat == null) continue;
                if (_analyzers.TryGetValue(threat.Kind, out var analyzer))
                {
                    var decisions = analyzer.Analyze(threat);
                    if (decisions != null)
                    {
                        foreach (var decision in decisions)
                        {
                            if (decision != null)
                                yield return decision;
                        }
                    }
                }
            }
        }
    }
}
