using Microsoft.Win32;
using MSearch.Core;
using System;
using System.ComponentModel;
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
            catch (Exception ex) when (ex.HResult.Equals(unchecked((int)0x800700E1)) || ex.HResult.Equals(0xE1))
            {
                AppConfig.Instance.LL.LogWarnMediumMessage("_ErrorLockedByWD", path);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, path, ScanActionType.LockedByAntivirus));
                return true;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCheckingLock", ex, path);
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

        //https://learn.microsoft.com/en-us/answers/questions/726748/how-can-i-change-particular-registry-owner-in-c
        public static void TakeownRegKey(string keyPath)
        {
            string sName = Environment.UserName;
            NTAccount ntAccount = new NTAccount(sName);
            string sSid = ntAccount.Translate(typeof(SecurityIdentifier)).Value;
            IntPtr pSid = IntPtr.Zero;
            Native.ConvertStringSidToSid(sSid, out pSid);
            IntPtr hKey = IntPtr.Zero;
            uint dwErr = Native.RegOpenKeyEx((IntPtr)Native.HKEY_CURRENT_USER, keyPath, 0, Native.KEY_WOW64_64KEY | Native.WRITE_OWNER, ref hKey);
            if (dwErr == 0)
            {
                uint dwRet = Native.SetSecurityInfo(hKey,
                      Native.SE_OBJECT_TYPE.SE_REGISTRY_KEY,
                      Native.OWNER_SECURITY_INFORMATION,
                      pSid,
                      IntPtr.Zero,
                      IntPtr.Zero,
                      IntPtr.Zero);
                Native.RegCloseKey(hKey);
            }
        }

        public static void ResetPermissionsToDefault(string keyPath)
        {
            IntPtr hKey = IntPtr.Zero;
            uint err = Native.RegOpenKeyEx(
                (IntPtr)Native.HKEY_CURRENT_USER,
                keyPath,
                0,
                Native.KEY_WOW64_64KEY | Native.WRITE_DAC,
                ref hKey);

            if (err != 0 || hKey == IntPtr.Zero)
                throw new Win32Exception((int)err, "RegOpenKeyEx failed");

            try
            {
                uint result = Native.SetSecurityInfo(
                    hKey,
                    Native.SE_OBJECT_TYPE.SE_REGISTRY_KEY,
                    Native.DACL_SECURITY_INFORMATION,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero 
                );

                if (result != 0)
                    throw new Win32Exception((int)result, "SetSecurityInfo failed");
            }
            finally
            {
                Native.RegCloseKey(hKey);
            }
        }

        internal static bool KillAndDelete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                    if (!File.Exists(filePath))
                        return true;
                }
            }
            catch (Exception) { }

            uint processId = 0;
            try
            {
                processId = ProcessManager.GetProcessIdByFilePath(filePath);
                string tmpProcName = "";
                if (processId != 0)
                {
                    Process process = Process.GetProcessById((int)processId);
                    if (process != null && !process.HasExited)
                    {
                        tmpProcName = process.ProcessName;
                        ProcessManager.UnProtect(new int[] { process.Id });
                        process.Kill();
                        process.WaitForExit(1000);
                        AppConfig.Instance.LL.LogSuccessMessage("_BlockingProcessClosed", $"{tmpProcName} | PID: {processId}");
                    }
                    else AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning");
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, filePath, ScanActionType.LockedByAntivirus));
            }
            catch (InvalidOperationException ioe) when (ioe.HResult.Equals(unchecked((int)0x80070057)))
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {processId}");
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {processId}");
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, filePath, "_File");
            }

            try
            {
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                    if (!File.Exists(filePath))
                        return true;
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, filePath, ScanActionType.LockedByAntivirus));
            }
            catch (InvalidOperationException ioe) when (ioe.HResult.Equals(unchecked((int)0x80070057)))
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {processId}");
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {processId}");
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, filePath, "_File");
            }

            return false;
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
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, filePath, ScanActionType.LockedByAntivirus));
            }
            catch (Exception e)
            {
                AppConfig.Instance.LL.LogWarnMessage("_WarnCannotDisableExecution", e.Message);
            }

        }
    }
}
