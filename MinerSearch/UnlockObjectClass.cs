using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

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

        internal static bool IsRegistryKeyBlocked(string keyPath)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadPermissions))
            {
                if (key != null)
                {
                    var security = key.GetAccessControl();
                    var rules = security.GetAccessRules(true, true, typeof(NTAccount));

                    foreach (AuthorizationRule rule in rules)
                    {
                        if (rule is RegistryAccessRule accessRule &&
                            accessRule.AccessControlType == AccessControlType.Deny)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal static void UnblockRegistry(string keyPath)
        {
            using (RegistryKey baseKey = Registry.CurrentUser.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions))
            {
                if (baseKey != null)
                {
                    var security = baseKey.GetAccessControl(AccessControlSections.Access);
                    var rules = security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

                    bool changesMade = false;

                    foreach (AuthorizationRule rule in rules)
                    {
                        if (rule is RegistryAccessRule accessRule &&
                            accessRule.AccessControlType == AccessControlType.Deny)
                        {
                            security.RemoveAccessRuleSpecific(accessRule);
                            changesMade = true;
#if DEBUG
                        Console.WriteLine($"[DBG] Removed rule Deny: {accessRule.IdentityReference.Value} (Inheritance: {accessRule.InheritanceFlags})");
#endif
                        }
                    }

                    if (changesMade)
                    {
                        baseKey.SetAccessControl(security);
#if DEBUG
                    Console.WriteLine($"[DBG] Update Access rules for key: {baseKey.Name}");
#endif
                    }
                }
            }
        }

        internal static void KillAndDelete(string filePath)
        {
            try
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                if (!File.Exists(filePath))
                {
                    return;
                }
            }
            catch (Exception)
            {
                Program.LL.LogMessage("\t[.]", "_FindBlockingProcess", "", ConsoleColor.White);

                try
                {
                    uint processId = Utils.GetProcessIdByFilePath(filePath);

                    if (processId != 0)
                    {
                        Process process = Process.GetProcessById((int)processId);
                        if (!process.HasExited)
                        {

                            Utils.UnProtect(new int[] { process.Id });
                            process.Kill();
                            Program.LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");

                        }
                    }
                }
                catch (System.ComponentModel.Win32Exception win32e)
                {
#if DEBUG
                    Console.WriteLine($"[DBG] Win32Error {win32e.Message}");
#endif
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine($"[DBG] Error {e.Message}");
#endif
                }
            }

            
        }
    }
}
