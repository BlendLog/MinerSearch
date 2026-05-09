using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Management;

namespace MSearch.Core.ThreatHandlers
{
    internal sealed class WmiThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.WmiSubscription;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var wmi = decision.Target as WmiSubscriptionThreatObject;
            if (wmi == null) return ApplyResult.NotApplicable;

            // WMI обрабатываем только в Finalize
            if (phase != CleanupPhase.Finalize) return ApplyResult.Skipped;

            // ScanOnly — не выполняем действия
            if (LaunchOptions.GetInstance.ScanOnly) return ApplyResult.Skipped;

            return HandleDeleteWmi(wmi, decision);
        }

        private ApplyResult HandleDeleteWmi(WmiSubscriptionThreatObject wmi, ThreatDecision decision)
        {
            try
            {
                ManagementScope scope = new ManagementScope($@"\\.\{wmi.WmiNamespace}");
                scope.Connect();

                string path = $"{wmi.WmiClass}.Name=\"{wmi.Name}\"";
                ManagementPath mgmtPath = new ManagementPath(path);
                using (ManagementObject obj = new ManagementObject(scope, mgmtPath, null))
                {
                    obj.Delete();
                }

                AppConfig.GetInstance.LL.LogSuccessMessage("_WMIEvent", wmi.Name, "_Deleted");
                decision.ActionType = ScanActionType.Deleted;
                return ApplyResult.Success;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemoveWmi", ex, wmi.Name);
                return ApplyResult.Error;
            }
        }
    }
}
