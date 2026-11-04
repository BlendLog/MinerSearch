using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using System.Collections.Generic;
using System.Threading;

namespace MSearch.Core
{
    public enum ThreatObjectKind
    {
        Process,
        RegistryObject,
        File,
        Directory,
        FirewallRule,
        UserProfile,
        WmiSubscription,
        ScheduledTask,
        Service,
        ShellStartupFile,
        Hosts,
        Other
    }

    public interface IThreatObject
    {
        ThreatObjectKind Kind { get; }
        string Id { get; }
    }

    public interface IThreatAnalyzer
    {
        ThreatObjectKind Kind { get; }
        IEnumerable<ThreatDecision> Analyze(IThreatObject threat, CancellationToken token);
    }

    public interface IThreatHandler
    {
        ThreatObjectKind Kind { get; }
        ApplyResult Apply(ThreatDecision decision, CleanupPhase phase, CancellationToken token);
    }

    public abstract class ThreatObject : IThreatObject
    {
        public ThreatObjectKind Kind { get; }
        public string Id { get; }
        protected ThreatObject(ThreatObjectKind kind, string id) { Kind = kind; Id = id; }
    }
}
