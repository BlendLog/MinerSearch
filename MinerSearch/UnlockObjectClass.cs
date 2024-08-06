using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MSearch
{
    class UnlockObjectClass
    {
        public static void UnlockDirectory(string directoryPath)
        {
            if (!Directory.Exists(Utils.GetLongPath(directoryPath)))
            {
                return;
            }

            try
            {
                var adminSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                var adminAccount = adminSid.Translate(typeof(NTAccount)).ToString();

                // Set the owner to Built-in Administrators group
                SetOwner(directoryPath, adminAccount);

                // Apply full control to Built-in Administrators and remove Deny entries
                ApplyFullControl(directoryPath, adminAccount);

                RemoveReadOnlyAttribute(directoryPath);

            }
            catch (SystemException ex)
            {
                Program.LL.LogErrorMessage("_ErrorOnUnlock", ex, directoryPath, "_Directory");
            }
        }


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

                fileSecurity.SetAccessRuleProtection(true, false);

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
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorOnUnlock", ex, filePath, "_File");
            }
        }

        private static void SetOwner(string directoryPath, string ownerAccount)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            var directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.SetOwner(new NTAccount(ownerAccount));
            directoryInfo.SetAccessControl(directorySecurity);

            foreach (var dir in directoryInfo.GetDirectories("*", SearchOption.AllDirectories))
            {
                var childDirectorySecurity = dir.GetAccessControl();
                childDirectorySecurity.SetOwner(new NTAccount(ownerAccount));
                dir.SetAccessControl(childDirectorySecurity);
            }

            foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                var fileSecurity = file.GetAccessControl();
                fileSecurity.SetOwner(new NTAccount(ownerAccount));
                file.SetAccessControl(fileSecurity);
            }
        }

        private static void ApplyFullControl(string directoryPath, string adminAccount)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            ApplyFullControlAndRemoveDenyEntriesRecursive(directoryInfo, adminAccount);
        }

        private static void ApplyFullControlAndRemoveDenyEntriesRecursive(DirectoryInfo directoryInfo, string adminAccount)
        {
            // Apply full control and remove deny entries for the directory
            var directorySecurity = directoryInfo.GetAccessControl();
            RemoveDenyEntries(directorySecurity);
            directorySecurity.AddAccessRule(new FileSystemAccessRule(adminAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            directoryInfo.SetAccessControl(directorySecurity);

            foreach (var dir in directoryInfo.GetDirectories())
            {
                ApplyFullControlAndRemoveDenyEntriesRecursive(dir, adminAccount);
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var fileSecurity = file.GetAccessControl();
                RemoveDenyEntries(fileSecurity);
                fileSecurity.AddAccessRule(new FileSystemAccessRule(adminAccount, FileSystemRights.FullControl, AccessControlType.Allow));
                file.SetAccessControl(fileSecurity);
            }
        }

        private static void RemoveDenyEntries(FileSystemSecurity security)
        {
            var rules = security.GetAccessRules(true, true, typeof(NTAccount));
            foreach (FileSystemAccessRule rule in rules)
            {
                if (rule.AccessControlType == AccessControlType.Deny)
                {
                    security.RemoveAccessRule(rule);
                }
            }
        }

        private static void RemoveReadOnlyAttribute(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            if ((directoryInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                directoryInfo.Attributes &= ~FileAttributes.ReadOnly;
            }

            foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    file.Attributes &= ~FileAttributes.ReadOnly;
                }
            }

            foreach (var dir in directoryInfo.GetDirectories("*", SearchOption.AllDirectories))
            {
                if ((dir.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    dir.Attributes &= ~FileAttributes.ReadOnly;
                }
            }
        }
    }
}
