using System;

namespace MSearch.Core.ThreatObjects
{
    public sealed class WmiSubscriptionThreatObject : ThreatObject
    {
        public string Name { get; }
        public string CommandLineTemplate { get; }
        public string WmiClass { get; }
        public string WmiNamespace { get; }

        public WmiSubscriptionThreatObject(
            string name,
            string commandLineTemplate,
            string wmiClass,
            string wmiNamespace = @"root\subscription")
            : base(ThreatObjectKind.WmiSubscription, name)
        {
            Name = name;
            CommandLineTemplate = commandLineTemplate;
            WmiClass = wmiClass;
            WmiNamespace = wmiNamespace;
        }
    }
}
