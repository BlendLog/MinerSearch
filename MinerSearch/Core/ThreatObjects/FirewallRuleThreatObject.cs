using NetFwTypeLib;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// Представляет правило брандмауэра Windows, связанное с вредоносным файлом.
    /// </summary>
    public sealed class FirewallRuleThreatObject : ThreatObject
    {
        public string RuleName { get; }
        public string ApplicationName { get; }
        public NET_FW_RULE_DIRECTION_ Direction { get; }
        public NET_FW_ACTION_ Action { get; }
        public string Protocol { get; }

        // Флаги действий (заполняет Анализатор)
        public bool ShouldDelete { get; internal set; }

        public FirewallRuleThreatObject(
            string ruleName,
            string applicationName,
            NET_FW_RULE_DIRECTION_ direction,
            NET_FW_ACTION_ action,
            string protocol)
            : base(ThreatObjectKind.FirewallRule, ruleName)
        {
            RuleName = ruleName;
            ApplicationName = applicationName;
            Direction = direction;
            Action = action;
            Protocol = protocol;
        }
    }
}
