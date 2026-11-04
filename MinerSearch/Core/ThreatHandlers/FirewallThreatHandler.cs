using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using NetFwTypeLib;
using System;
using System.Threading;

namespace MSearch.Core.ThreatHandlers
{
    /// <summary>
    /// Windows Firewall rule handler.
    /// Deletes rules associated with malicious files.
    /// </summary>
    internal sealed class FirewallThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.FirewallRule;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var fwRule = decision.Target as FirewallRuleThreatObject;
            if (fwRule == null) return ApplyResult.NotApplicable;

            if (phase != CleanupPhase.Finalize) return ApplyResult.NotApplicable;

            if (!fwRule.ShouldDelete) return ApplyResult.Skipped;

            return HandleDeleteRule(fwRule, decision, token);
        }

        private ApplyResult HandleDeleteRule(FirewallRuleThreatObject rule, ThreatDecision decision, CancellationToken token)
        {
            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID(MSData.GetInstance.consts[MSKeys.FwPolicyProgId]);
                if (typeFWPolicy2 == null) return ApplyResult.Error;

                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);
                INetFwRules rules = fwPolicy2.Rules;

                foreach (INetFwRule existingRule in rules)
                {
                    token.ThrowIfCancellationRequested();

                    if (existingRule.Name.Equals(rule.RuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        rules.Remove(rule.RuleName);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_FirewallRule_Deleted", rule.RuleName);
                        decision.ActionType = ScanActionType.Deleted;
                        return ApplyResult.Success;
                    }
                }

                // Правило уже не существует
                decision.ActionType = ScanActionType.Skipped;
                return ApplyResult.NotApplicable;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemoveFWRule", ex, rule.RuleName);
                return ApplyResult.Error;
            }
        }
    }
}
