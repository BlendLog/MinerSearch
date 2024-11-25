using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MSearch
{
    class UnlockObjectClass
    {
        public static void UnlockFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(currentUserIdentity);

                fileSecurity.SetAccessRuleProtection(false, true);

                AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        fileSecurity.RemoveAccessRuleSpecific(fileRule);
                    }
                }

                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(currentUserRule);

                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(administratorsRule);

                SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule usersRule = new FileSystemAccessRule(
                    usersGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(usersRule);

                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(systemRule);

                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (UnauthorizedAccessException)
            {
                Program.LL.LogWarnMediumMessage("_ErrorOnUnlock", filePath);
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorOnUnlock", ex, filePath, "_File");
            }
        }

        internal static bool IsLockedObject(string path)
        {
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            SecurityIdentifier currentUserIdentity = currentUser.User;

            FileSecurity fileSecurity = new FileSecurity(path, AccessControlSections.All);
            fileSecurity.SetOwner(currentUserIdentity);

            AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
            foreach (AuthorizationRule rule in accessRules)
            {
                if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
