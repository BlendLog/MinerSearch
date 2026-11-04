using DBase;
using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MSearch.Core.Scanners
{
    public class UserProfileScanner : IThreatScanner
    {
        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<IThreatObject> Scan(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

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
            string specialAccountsPath = MSData.GetInstance.queries["SpecialAccountsUserList"];

            RegistryKey userListKey = null;
            try
            {
                userListKey = Registry.LocalMachine.OpenSubKey(specialAccountsPath);
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }

            try
            {
                foreach (string userName in users)
                {
                    token.ThrowIfCancellationRequested();

                    ScanActivity.Report(userName);

                    bool isHidden = false;
                    string userRegistryPath = null;

                    if (userListKey != null)
                    {
                        try
                        {
                            object value = userListKey.GetValue(userName);
                            if (value != null)
                            {
                                int dwordValue = Convert.ToInt32(value);
                                if (dwordValue == 0)
                                {
                                    isHidden = true;
                                    userRegistryPath = $@"{specialAccountsPath}\{userName}";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, userName, "_Username");
                        }
                    }

                    if (!isHidden)
                        continue;

                    string logMessage = string.Format(AppConfig.GetInstance.LL.GetLocalizedString("_HiddenUserFound"), userName);
                    Logger.WriteLog($"\t[!] {logMessage}", Logger.warnMedium);

                    yield return (IThreatObject)new UserProfileThreatObject
                    {
                        UserName = userName,
                        IsHidden = isHidden,
                        RegistryKeyPath = userRegistryPath,
                        ShouldDelete = false
                    };
                }
            }
            finally
            {
                if (userListKey != null)
                    userListKey.Dispose();
            }
        }
    }
}
