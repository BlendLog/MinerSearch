using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;

namespace MSearch.Core.Scanners
{
    public class UserProfileScanner : IThreatScanner
    {
        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<IThreatObject> Scan()
        {
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanUserProfiles");
                        _headerLogged = true;
                    }
                }
            }

            List<string> users = OSExtensions.GetUsers();
            string specialAccountsPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\SpecialAccounts\UserList";

            foreach (string userName in users)
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_Username", userName, ConsoleColor.White);

                bool isHidden = false;
                string userRegistryPath = null;

                try
                {
                    using (RegistryKey userKey = Registry.LocalMachine.OpenSubKey(specialAccountsPath))
                    {
                        if (userKey != null)
                        {
                            object value = userKey.GetValue(userName);
                            if (value != null)
                            {
                                int dwordValue = Convert.ToInt32(value);
                                if (dwordValue == 0)
                                {
                                    isHidden = true;
                                    userRegistryPath = $@"{specialAccountsPath}\{userName}";
                                    string logMessage = string.Format(AppConfig.GetInstance.LL.GetLocalizedString("_HiddenUserFound"), userName);
                                    Logger.WriteLog($"\t[!] {logMessage}", Logger.warnMedium);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, userName, "_Username");
                }

                if (isHidden)
                {
                    yield return (IThreatObject)new UserProfileThreatObject
                    {
                        UserName = userName,
                        IsHidden = isHidden,
                        RegistryKeyPath = userRegistryPath,
                        ShouldDelete = false
                    };
                }
            }
        }
    }
}
