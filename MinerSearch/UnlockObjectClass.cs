using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace MSearch
{
    internal static class UnlockObjectClass
    {

        public static bool ResetObjectACL(string path)
        {
            IntPtr pAcl = IntPtr.Zero;

            try
            {
                pAcl = Marshal.AllocHGlobal(Native.ACL_SIZE);

                if (!Native.InitializeAcl(pAcl, Native.ACL_SIZE, Native.ACL_REVISION))
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }

                // Set empty DACL and enable inheritance
                int result = Native.SetNamedSecurityInfo(
                    path,
                    Native.SE_FILE_OBJECT,
                    Native.DACL_SECURITY_INFORMATION | Native.UNPROTECTED_DACL_SECURITY_INFORMATION,
                    IntPtr.Zero, 
                    IntPtr.Zero, 
                    pAcl,        
                    IntPtr.Zero  
                );

                if (result != 0)
                {
                    throw new System.ComponentModel.Win32Exception(result);
                }

#if DEBUG
                Console.WriteLine($"ACL has been resetted: {path}");
#endif
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"ResetObjACL: {ex.Message}");
#endif
                return false;
            }
            finally
            {
                if (pAcl != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pAcl);
                }
            }
        }

        internal static bool IsLockedObject(string path)
        {
            try
            {
                FileSystemSecurity security;

                if (Directory.Exists(path))
                    security = new DirectorySecurity(path, AccessControlSections.Access);
                else if (File.Exists(path))
                    security = new FileSecurity(path, AccessControlSections.Access);
                else
                    return false;

                AuthorizationRuleCollection accessRules = security.GetAccessRules(true, true, typeof(SecurityIdentifier));

                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        return true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
            catch (SecurityException)
            {
                return true;
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCheckingLock", ex, path);
                return true;
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
                    uint processId = ProcessManager.GetProcessIdByFilePath(filePath);

                    if (processId != 0)
                    {
                        Process process = Process.GetProcessById((int)processId);
                        if (!process.HasExited)
                        {

                            ProcessManager.UnProtect(new int[] { process.Id });
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

        internal static void DisableExecute(string filePath)
        {
            try
            {
                FileSecurity fileSecurity = File.GetAccessControl(filePath);

                FileSystemAccessRule denyReadExecuteRule = new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null), // Everyone group
                    FileSystemRights.ExecuteFile,
                    AccessControlType.Deny);

                fileSecurity.AddAccessRule(denyReadExecuteRule);

                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (ArgumentException) { }
            catch (FileNotFoundException) { }
            catch (Exception e)
            {
                Program.LL.LogWarnMessage("_WarnCannotDisableExecution", e.Message);
            }

        }
    }
}
