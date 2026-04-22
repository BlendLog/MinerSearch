namespace MSearch.Core.ThreatDecisions
{
    public class ThreatDecision
    {
        public IThreatObject Target { get; }
        public int RiskLevel { get; }

        public ScanObjectType ObjectType { get; internal set; }
        public ScanActionType ActionType { get; internal set; }
        
        /// <summary>
        /// Сообщение об ошибке, возникшей при выполнении действия (Handler.Apply).
        /// Заполняется обработчиком при ApplyResult.Error.
        /// </summary>
        public string ApplyErrorMessage { get; internal set; }

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
