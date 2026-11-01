using MSearch.UI;

namespace MSearch.Core.ThreatDecisions
{
    public class ThreatDecision
    {
        public IThreatObject Target { get; }
        public int RiskLevel { get; }

        public ScanObjectType ObjectType { get; internal set; }
        public ScanActionType ActionType { get; internal set; } = ScanActionType.Skipped;

        /// <summary>
        /// Action selected by the user in the threat review UI.
        /// </summary>
        public ScanActionTypeUserSelected? UserOverrideAction { get; set; }

        /// <summary>
        /// Error message produced while applying the action (Handler.Apply).
        /// </summary>
        public string ApplyErrorMessage { get; internal set; }

        /// <summary>
        /// Informative note for the Note column in scan results.
        /// </summary>
        public string Note { get; internal set; }

        public ThreatDecision(
            IThreatObject threatObject,
            int riskLevel,
            ScanObjectType objectType)
        {
            Target = threatObject;
            RiskLevel = riskLevel;
            ObjectType = objectType;
        }
    }
}
