using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using NetFwTypeLib;
using System;

namespace MSearch.Core.ThreatHandlers
{
    /// <summary>
    /// Обработчик правил брандмауэра Windows.
    /// Удаляет правила, связанные с вредоносными файлами.
    /// </summary>
    internal sealed class FirewallThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.FirewallRule;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var fwRule = decision.Target as FirewallRuleThreatObject;
            if (fwRule == null) return ApplyResult.NotApplicable;

            if (phase != CleanupPhase.Finalize) return ApplyResult.NotApplicable;

            if (!fwRule.ShouldDelete) return ApplyResult.Skipped;

            return HandleDeleteRule(fwRule, decision);
        }

        private ApplyResult HandleDeleteRule(FirewallRuleThreatObject rule, ThreatDecision decision)
        {
            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                if (typeFWPolicy2 == null) return ApplyResult.Error;

                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);
                INetFwRules rules = fwPolicy2.Rules;

                // Ищем правило по имени и удаляем
                foreach (INetFwRule existingRule in rules)
                {
                    if (existingRule.Name.Equals(rule.RuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        rules.Remove(rule.RuleName);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_FirewallRule_Deleted", rule.RuleName);
                        return ApplyResult.Success;
                    }
                }

                // Правило уже не существует
                return ApplyResult.NotApplicable;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemoveFWRule", ex, rule.RuleName);
                return ApplyResult.Error;
            }
        }
    }
}
