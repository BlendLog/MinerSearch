using DBase;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using MSearch.Core;
using MSearch.Core.Managers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Win32Wrapper;

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

        private static bool PathStartsWith(string path, string directory)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(directory))
                return false;

            string normalizedDir = directory.TrimEnd('\\') + "\\";
            return path.StartsWith(normalizedDir, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Разрешает сброс ACL (смена владельца/DACL) только для пользовательских и прикладных каталогов.
        /// В системных и защищённых AV-деревьях отказ 0x80070005 ожидаем — там доступно только чтение.
        /// Список запретов хранится в MSData (обфусцированные строки).
        /// </summary>
        internal static bool IsFileAclResetAllowed(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            string path = filePath.StartsWith(@"\\?\") ? filePath.Substring(4) : filePath;

            try
            {
                string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                foreach (string dir in MSData.GetInstance.aclResetForbiddenDirs)
                {
                    if (PathStartsWith(path, dir))
                        return false;
                }

                foreach (string fragment in MSData.GetInstance.aclResetForbiddenFragments)
                {
                    if (!string.IsNullOrEmpty(fragment) &&
                        path.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0)
                        return false;
                }

                string[] allowed =
                {
                    programFiles,
                    programFilesX86,
                    programData,
                    userProfile,
                    Path.GetTempPath(),
                };

                foreach (string dir in allowed)
                {
                    if (PathStartsWith(path, dir))
                        return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Открывает файл на чтение в обход DACL через SeBackupPrivilege + FILE_FLAG_BACKUP_SEMANTICS.
        /// Файл не изменяется. Возвращает null, если открыть не удалось.
        /// share по умолчанию широкий; карантин передаёт FileShare.None, чтобы сохранить эксклюзивность снимка.
        /// </summary>
        internal static FileStream OpenReadWithBackupSemantics(string filePath, FileShare share = FileShare.ReadWrite | FileShare.Delete)
        {
            try
            {
                string longPath = filePath.StartsWith(@"\\?\") ? filePath : @"\\?\" + filePath;

                SafeFileHandle safeHandle = Native.CreateFile(
                    longPath,
                    FileAccess.Read,
                    share,
                    IntPtr.Zero,
                    FileMode.Open,
                    (FileAttributes)Native.FILE_FLAG_BACKUP_SEMANTICS,
                    IntPtr.Zero);

                if (safeHandle.IsInvalid)
                {
                    safeHandle.Dispose();
                    return null;
                }

                try
                {
                    return new FileStream(safeHandle, FileAccess.Read);
                }
                catch
                {
                    safeHandle.Dispose();
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Крайняя мера для файлов с deny-ACL: забирает владение (SeTakeOwnershipPrivilege) и выставляет
        /// разрешающий DACL (наследование включено, deny-ACE убраны), после чего файл снова читается.
        /// </summary>
        internal static bool RestoreFileAclForRead(string filePath)
        {
            try
            {
                string longPath = filePath.StartsWith(@"\\?\") ? filePath : @"\\?\" + filePath;

                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                if (identity == null || identity.User == null)
                    return false;

                byte[] ownerBytes = new byte[identity.User.BinaryLength];
                identity.User.GetBinaryForm(ownerBytes, 0);

                IntPtr pOwner = Marshal.AllocHGlobal(ownerBytes.Length);
                try
                {
                    Marshal.Copy(ownerBytes, 0, pOwner, ownerBytes.Length);

                    Native.SetNamedSecurityInfo(
                        longPath,
                        Native.SE_FILE_OBJECT,
                        Native.OWNER_SECURITY_INFORMATION,
                        pOwner,
                        IntPtr.Zero,
                        IntPtr.Zero,
                        IntPtr.Zero);
                }
                finally
                {
                    Marshal.FreeHGlobal(pOwner);
                }

                FileSecurity security = new FileSecurity();
                security.SetAccessRuleProtection(false, false);
                security.AddAccessRule(new FileSystemAccessRule(
                    identity.User, FileSystemRights.FullControl, AccessControlType.Allow));
                security.AddAccessRule(new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null),
                    FileSystemRights.FullControl, AccessControlType.Allow));

                File.SetAccessControl(longPath, security);

                using (new FileStream(longPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"[DBG] RestoreFileAclForRead failed: {filePath}: {ex.Message}");
#endif
                return false;
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
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_ErrorLockedByWD", path);
                return true;
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCheckingLock", ex, path);
                return true;
            }

            return false;
        }

        internal static bool IsRegistryKeyBlocked(string keyPath)
        {
            return IsRegistryKeyBlocked(keyPath, RegistryHive.CurrentUser);
        }

        internal static bool IsRegistryKeyBlocked(string keyPath, RegistryHive hive)
        {
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
            using (RegistryKey key = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadPermissions))
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

        internal static bool UnblockRegistry(string keyPath)
        {
            return UnblockRegistry(keyPath, RegistryHive.CurrentUser);
        }

        internal static bool UnblockRegistry(string keyPath, RegistryHive hive)
        {
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64))
            using (RegistryKey hk = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions))
            {
                if (hk != null)
                {
                    var security = hk.GetAccessControl(AccessControlSections.Access);
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
                        hk.SetAccessControl(security);
#if DEBUG
                        Console.WriteLine($"[DBG] Update Access rules for key: {hk.Name}");
#endif
                    }

                    return changesMade;
                }
            }

            return false;
        }

        /// <summary>
        /// Определяет корень реестра по префиксу пути ("HKLM\", "HKEY_LOCAL_MACHINE\", "HKCU\", "HKEY_CURRENT_USER\").
        /// Без префикса — совместимость: HKEY_CURRENT_USER.
        /// </summary>
        private static IntPtr ResolveRegistryRoot(string keyPath, out string subKeyPath)
        {
            subKeyPath = keyPath;

            if (keyPath.StartsWith("HKLM\\", StringComparison.OrdinalIgnoreCase))
            {
                subKeyPath = keyPath.Substring(5);
                return (IntPtr)Native.HKEY_LOCAL_MACHINE;
            }
            if (keyPath.StartsWith("HKEY_LOCAL_MACHINE\\", StringComparison.OrdinalIgnoreCase))
            {
                subKeyPath = keyPath.Substring(17);
                return (IntPtr)Native.HKEY_LOCAL_MACHINE;
            }
            if (keyPath.StartsWith("HKCU\\", StringComparison.OrdinalIgnoreCase))
            {
                subKeyPath = keyPath.Substring(5);
                return (IntPtr)Native.HKEY_CURRENT_USER;
            }
            if (keyPath.StartsWith("HKEY_CURRENT_USER\\", StringComparison.OrdinalIgnoreCase))
            {
                subKeyPath = keyPath.Substring(18);
                return (IntPtr)Native.HKEY_CURRENT_USER;
            }
            return (IntPtr)Native.HKEY_CURRENT_USER;
        }

        //https://learn.microsoft.com/en-us/answers/questions/726748/how-can-i-change-particular-registry-owner-in-c
        public static bool TakeownRegKey(string keyPath)
        {
            string subKeyPath;
            IntPtr hRoot = ResolveRegistryRoot(keyPath, out subKeyPath);

            string sName = Environment.UserName;
            NTAccount ntAccount = new NTAccount(sName);
            string sSid = ntAccount.Translate(typeof(SecurityIdentifier)).Value;
            IntPtr pSid = IntPtr.Zero;
            Native.ConvertStringSidToSid(sSid, out pSid);
            IntPtr hKey = IntPtr.Zero;
            uint dwErr = Native.RegOpenKeyEx(hRoot, subKeyPath, 0, Native.KEY_WOW64_64KEY | Native.WRITE_OWNER, ref hKey);
            if (dwErr == 0)
            {
                try
                {
                    uint dwRet = Native.SetSecurityInfo(hKey,
                          Native.SE_OBJECT_TYPE.SE_REGISTRY_KEY,
                          Native.OWNER_SECURITY_INFORMATION,
                          pSid,
                          IntPtr.Zero,
                          IntPtr.Zero,
                          IntPtr.Zero);
                    return dwRet == 0;
                }
                finally
                {
                    Native.RegCloseKey(hKey);
                }
            }
            return false;
        }

        public static bool ResetPermissionsToDefault(string keyPath)
        {
            string subKeyPath;
            IntPtr hRoot = ResolveRegistryRoot(keyPath, out subKeyPath);

            IntPtr hKey = IntPtr.Zero;
            uint err = Native.RegOpenKeyEx(
                hRoot,
                subKeyPath,
                0,
                Native.KEY_WOW64_64KEY | Native.WRITE_DAC,
                ref hKey);

            if (err != 0 || hKey == IntPtr.Zero)
                return false;

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

                return result == 0;
            }
            finally
            {
                Native.RegCloseKey(hKey);
            }
        }

        /// <summary>
        /// Создаёт собственные ключи настроек приложения (в HKCU и карантин в HKLM), если их ещё нет,
        /// затем проверяет и восстанавливает доступ к ним: снимает Deny-ACE, а если отказа недостаточно —
        /// забирает владение (WRITE_OWNER, при включённом SeTakeOwnershipPrivilege) и сбрасывает ACL через WRITE_DAC.
        /// </summary>
        internal static void EnsureOwnSettingsKeyAccessible()
        {
            try
            {
                string keyPath = AppConfig.GetInstance.RegistryPathMain;
                string quarantinePath = AppConfig.GetInstance.QuarantineKeyPath;

                EnsureKeyExists(keyPath, RegistryHive.CurrentUser);
                EnsureKeyExists(quarantinePath, RegistryHive.LocalMachine);

                if (!IsHiveKeyWritable(keyPath, RegistryHive.CurrentUser))
                {
                    RepairOwnKeyPath(keyPath, RegistryHive.CurrentUser);
                }

                if (!IsHiveKeyWritable(quarantinePath, RegistryHive.LocalMachine))
                {
                    RepairOwnKeyPath(quarantinePath, RegistryHive.LocalMachine);
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, AppConfig.GetInstance.RegistryPathMain);
            }
        }

        private static bool RepairOwnKeyPath(string keyPath, RegistryHive hive)
        {
            try { UnblockRegistry(keyPath, hive); } catch { }

            if (IsHiveKeyWritable(keyPath, hive))
                return true;

            string prefix = hive == RegistryHive.LocalMachine ? @"HKLM\" : @"HKCU\";
            if (TakeownRegKey(prefix + keyPath))
                ResetPermissionsToDefault(prefix + keyPath);

            return IsHiveKeyWritable(keyPath, hive);
        }

        /// <summary>
        /// Возвращает корневый дескриптор (HKEY_*) для заданного улья реестра.
        /// </summary>
        private static IntPtr GetHiveRoot(RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHive.LocalMachine:
                    return (IntPtr)Native.HKEY_LOCAL_MACHINE;
                case RegistryHive.ClassesRoot:
                    return (IntPtr)Native.HKEY_CLASSES_ROOT;
                default:
                    return (IntPtr)Native.HKEY_CURRENT_USER;
            }
        }

        /// <summary>
        /// Создаёт ключ (и промежуточные родительские) через RegCreateKeyEx, если его ещё нет.
        /// Не бросает исключений: при отказе в доступе просто возвращает false.
        /// </summary>
        private static bool EnsureKeyExists(string keyPath, RegistryHive hive)
        {
            IntPtr hRoot = GetHiveRoot(hive);

            IntPtr hKey = IntPtr.Zero;
            uint disposition;
            uint result = Native.RegCreateKeyEx(hRoot, keyPath, 0, null, 0,
                Native.KEY_WRITE | Native.KEY_WOW64_64KEY, IntPtr.Zero, out hKey, out disposition);

            if (result == 0 && hKey != IntPtr.Zero)
            {
                Native.RegCloseKey(hKey);
                return true;
            }
            return false;
        }

        private static bool IsHiveKeyWritable(string keyPath, RegistryHive hive)
        {
            IntPtr hRoot = GetHiveRoot(hive);

            IntPtr hKey = IntPtr.Zero;
            uint result = Native.RegOpenKeyEx(hRoot, keyPath, 0,
                Native.KEY_WRITE | Native.KEY_WOW64_64KEY, ref hKey);

            if (result == 0 && hKey != IntPtr.Zero)
            {
                Native.RegCloseKey(hKey);
                return true;
            }
            return false;
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
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
            }
            catch (Exception)
            {
                // Отказ доступа/блокировка — попробуем снять процесс-держатель ниже.
                // Итоговая ошибка удаления логируется вызывающим кодом, без дублей.
            }

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
                        AppConfig.GetInstance.LL.LogSuccessMessage("_BlockingProcessClosed", $"{tmpProcName} | PID: {processId}");
                    }
                    else AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning");
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80070057)) ||
                                      e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {processId}");
            }
            catch (Exception)
            {

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
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
            }
            catch (Exception)
            {
                // Не логируем: вызывающий код напишет одну итоговую ошибку (_ErrorCannotRemove)
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
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
            }
            catch (Exception e)
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_WarnCannotDisableExecution", e.Message);
            }

        }
    }
}
