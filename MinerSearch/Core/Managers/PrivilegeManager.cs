using MSearch.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32Wrapper;

namespace MSearch.Core.Managers
{
    /// <summary>
    /// Windows system privileges
    /// </summary>
    public enum SystemPrivilege
    {
        SeDebugPrivilege,
        SeBackupPrivilege,
        SeRestorePrivilege,
        SeTakeOwnershipPrivilege,
        SeSecurityPrivilege
    }

    /// <summary>
    /// Manages the privileges of the current process token.
    /// Enables only the privileges required by the active set of scanners.
    /// </summary>
    public sealed class PrivilegeManager
    {
        private readonly LaunchOptions _options;
        private readonly HashSet<SystemPrivilege> _enabledPrivileges;

        private static readonly Dictionary<SystemPrivilege, string> PrivilegeNames = new Dictionary<SystemPrivilege, string>
        {
            { SystemPrivilege.SeDebugPrivilege, "SeDebugPrivilege" },
            { SystemPrivilege.SeBackupPrivilege, "SeBackupPrivilege" },
            { SystemPrivilege.SeRestorePrivilege, "SeRestorePrivilege" },
            { SystemPrivilege.SeTakeOwnershipPrivilege, "SeTakeOwnershipPrivilege" },
            { SystemPrivilege.SeSecurityPrivilege, "SeSecurityPrivilege" }
        };

        public PrivilegeManager(LaunchOptions options)
        {
            _options = options;
            _enabledPrivileges = new HashSet<SystemPrivilege>();
        }

        public void Enable()
        {
            if (AppConfig.GetInstance.RunAsSystem) return;
            EnablePrivileges();

        }

        /// <summary>
        /// Enables only the privileges required by LaunchOptions flags
        /// </summary>
        public void EnablePrivileges()
        {
            _enabledPrivileges.Clear();
            var privilegesToEnable = new List<SystemPrivilege>();

            if (!_options.no_runtime && !_options.no_rootkit_check)
            {
                privilegesToEnable.Add(SystemPrivilege.SeDebugPrivilege);
            }

            if (!_options.no_runtime || !_options.nosignaturescan)
            {
                // чтение файлов с deny-ACL через FILE_FLAG_BACKUP_SEMANTICS
                privilegesToEnable.Add(SystemPrivilege.SeBackupPrivilege);
            }

            if (!_options.ScanOnly)
            {
                privilegesToEnable.Add(SystemPrivilege.SeRestorePrivilege);
            }

            if ((!_options.no_scan_registry && !_options.ScanOnly) || !_options.nosignaturescan)
            {
                privilegesToEnable.Add(SystemPrivilege.SeTakeOwnershipPrivilege);
            }

            if (!_options.no_rootkit_check)
            {
                privilegesToEnable.Add(SystemPrivilege.SeSecurityPrivilege);
            }

            _enablePrivilegesInternal(privilegesToEnable);

            if (!_options.no_runtime && !_options.no_rootkit_check)
            {
                EnsurePrivilege(SystemPrivilege.SeDebugPrivilege);
            }
        }


        /// <summary>
        /// Checks whether the token holds the privilege
        /// </summary>
        public bool HasPrivilege(SystemPrivilege privilege)
        {
            string privName = PrivilegeNames[privilege];
            IntPtr hToken;
            if (!Native.OpenProcessToken(Native.GetCurrentProcess(), Native.TOKEN_QUERY, out hToken))
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(Marshal.GetLastWin32Error()));
                return false;
            }
            try
            {
                if (!Native.LookupPrivilegeValue(null, privName, out Native.LUID luid))
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(Marshal.GetLastWin32Error()));
                    return false;
                }
                Native.PRIVILEGE_SET privilegeSet = new Native.PRIVILEGE_SET
                {
                    PrivilegeCount = 1,
                    Control = 1,
                    Privilege = new Native.LUID_AND_ATTRIBUTES
                    {
                        Luid = luid,
                        Attributes = Native.SE_PRIVILEGE_ENABLED
                    }
                };
                if (!Native.PrivilegeCheck(hToken, ref privilegeSet, out bool hasPrivilege))
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(Marshal.GetLastWin32Error()));
                    return false;
                }
                return hasPrivilege;
            }
            finally
            {
                Native.CloseHandle(hToken);
            }
        }

        /// <summary>
        /// If the privilege is missing, tries to assign it through LSA and suggests a reboot
        /// </summary>
        public bool EnsurePrivilege(SystemPrivilege privilege)
        {
            if (HasPrivilege(privilege))
                return true;

            string privName = PrivilegeNames[privilege];
            string adminGroup = OSExtensions.ConvertWellKnowSIDToGroupName("S-1-5-32-544");

            if (!Native.GrantPrivilegeToGroup(adminGroup, privName))
            {
                return false;
            }

            string rebootMessage = AppConfig.GetInstance.LL.GetLocalizedString("_PrivilegeRequiresReboot").Replace("{0}", privName);

            var result = DialogDispatcher.Show(rebootMessage, AppConfig.GetInstance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }

            return false;
        }

        private void _enablePrivilegesInternal(IEnumerable<SystemPrivilege> privileges)
        {
            var privilegeList = new List<SystemPrivilege>();
            foreach (var priv in privileges)
            {
                if (!_enabledPrivileges.Contains(priv))
                {
                    privilegeList.Add(priv);
                    _enabledPrivileges.Add(priv);
                }
            }

            if (privilegeList.Count == 0)
                return;

            IntPtr hToken;
            if (!Native.OpenProcessToken(Native.GetCurrentProcess(), Native.TOKEN_ADJUST_PRIVILEGES | Native.TOKEN_QUERY, out hToken))
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(Marshal.GetLastWin32Error()));
                return;
            }

            try
            {
                foreach (var priv in privilegeList)
                {
                    string privName = PrivilegeNames[priv];

                    if (!Native.LookupPrivilegeValue(null, privName, out Native.LUID luid))
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(Marshal.GetLastWin32Error()));
                        continue;
                    }

                    Native.TOKEN_PRIVILEGES tkpPrivileges = new Native.TOKEN_PRIVILEGES
                    {
                        PrivilegeCount = 1,
                        Privilege = new Native.LUID_AND_ATTRIBUTES
                        {
                            Luid = luid,
                            Attributes = Native.SE_PRIVILEGE_ENABLED
                        }
                    };

                    Native.AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
                    int error = Marshal.GetLastWin32Error();
                    if (error != 0)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Win32Exception(error));
                    }
                }
            }
            finally
            {
                Native.CloseHandle(hToken);
            }

#if DEBUG
            Console.WriteLine($"[DBG] Enabled privileges: {string.Join(", ", _enabledPrivileges)}");
#endif
        }

    }
}
