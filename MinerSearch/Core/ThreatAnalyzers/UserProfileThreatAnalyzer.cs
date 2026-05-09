using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.ThreatAnalyzers
{
    public sealed class UserProfileThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.UserProfile;

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
                        _headerLogged = true;
                    }
                }
            }

            var userProfile = threat as UserProfileThreatObject;
            if (userProfile == null) yield break;

            int risk = 0;
            bool isMalicious = false;

            // Проверяем точное совпадение имени "john" (case-insensitive)
            if (userProfile.UserName.Equals("john", StringComparison.OrdinalIgnoreCase))
            {
                if (userProfile.IsHidden)
                {
                    risk = 3;
                    isMalicious = true;
                    userProfile.ShouldDelete = true;
                    AppConfig.GetInstance.LL.LogCautionMessage("_JohnUserDetected");
                }
            }

            if (risk == 0) yield break;

            ScanObjectType objType = isMalicious ? ScanObjectType.Unsafe : ScanObjectType.Suspicious;

            yield return new ThreatDecision(userProfile, risk, objType);
        }
    }
}
