using MSearch.Core.ThreatDecisions;
using System;

namespace MSearch.Core.ThreatObjects
{
    public class UserProfileThreatObject : IThreatObject
    {
        public ThreatObjectKind Kind => ThreatObjectKind.UserProfile;
        public string Id => UserName;

        public string UserName { get; set; }
        public bool IsHidden { get; set; }
        public string RegistryKeyPath { get; set; }

        public bool ShouldDelete { get; set; }

        public int RiskLevel { get; set; }
        public ScanObjectType ObjectType { get; set; }
        public ScanActionType ActionType { get; set; }
    }
}
