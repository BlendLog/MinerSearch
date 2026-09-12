using MSearch.UI;

namespace MSearch.Core.ThreatDecisions
{
    public class ThreatDecision
    {
        public IThreatObject Target { get; }
        public int RiskLevel { get; }

        public ScanObjectType ObjectType { get; internal set; }
        public ScanActionType ActionType { get; internal set; }

        /// <summary>
        /// Действие, выбранное пользователем в review-UI.
        /// </summary>
        public ScanActionTypeUserSelected? UserOverrideAction { get; set; }

        /// <summary>
        /// Сообщение об ошибке, возникшей при выполнении действия (Handler.Apply).
        /// </summary>
        public string ApplyErrorMessage { get; internal set; }

        /// <summary>
        /// Информативное примечание для колонки Note в FinishEx.
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
