using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;

namespace MSearch.Core.Handlers
{
    internal sealed class UserProfileThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.UserProfile;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            if (phase != CleanupPhase.Finalize) return ApplyResult.NotApplicable;

            var userProfile = decision.Target as UserProfileThreatObject;
            if (userProfile == null) return ApplyResult.NotApplicable;

            if (!userProfile.ShouldDelete) return ApplyResult.Skipped;

            if (LaunchOptions.GetInstance.ScanOnly) return ApplyResult.Skipped;

            try
            {
                OSExtensions.DeleteUser(userProfile.UserName);
                string logMessage = string.Format(AppConfig.GetInstance.LL.GetLocalizedString("_UserProfileDeleted"), userProfile.UserName);
                Logger.WriteLog($"\t[+] {logMessage}", Logger.success);
                decision.ActionType = ScanActionType.Deleted;
                return ApplyResult.Success;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                string logMessage = string.Format(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorCannotDeleteUser"), userProfile.UserName);
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotDeleteUser", ex, userProfile.UserName, "_Username");
                return ApplyResult.Error;
            }
        }
    }
}
