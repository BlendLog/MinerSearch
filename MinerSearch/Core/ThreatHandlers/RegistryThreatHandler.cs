using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Security;

namespace MSearch.Core.ThreatHandlers
{
    internal sealed class RegistryThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.RegistryObject;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var regThreat = decision.Target as RegistryThreatObject;

            // Реестр всегда лечим/удаляем только в финальной стадии.
            // Если флагов действий нет, значит делать ничего не нужно.
            if (regThreat == null || decision.RiskLevel == 0 || phase != CleanupPhase.Finalize) return ApplyResult.NotApplicable;

            bool hasAction = regThreat.ActionDelete || regThreat.ActionDeleteParentKey ||
                             regThreat.ActionSetData || regThreat.ActionSetSibling ||
                             regThreat.ActionRemoveDefenderExclusion;

            if (!hasAction) return ApplyResult.Skipped;

            string hiveShort = regThreat.Hive == "HKEY_LOCAL_MACHINE" ? "HKLM" : "HKCU";
            string logPath = $@"{hiveShort}\{regThreat.KeyPath}" + (string.IsNullOrEmpty(regThreat.ValueName) ? "" : $@"\{regThreat.ValueName}");

            if (LaunchOptions.GetInstance.ScanOnly)
            {
                return ApplyResult.Skipped;
            }

            try
            {
                // 0. Взлом прав доступа (если приказал анализатор: IFEO, Tekt0nit)
                if (regThreat.ActionUnlockFirst)
                {
                    string unlockPath = $@"{hiveShort}\{regThreat.KeyPath}";
                    UnlockObjectClass.TakeownRegKey(unlockPath);
                    UnlockObjectClass.ResetPermissionsToDefault(unlockPath);
                }

                RegistryKey baseKey = regThreat.Hive == "HKEY_LOCAL_MACHINE" ? Registry.LocalMachine : Registry.CurrentUser;

                // 1. Спец-удаление для Windows Defender (WMI/Powershell)
                if (regThreat.ActionRemoveDefenderExclusion)
                {
                    string subKey = GetKeyName(regThreat.KeyPath); // "Paths", "Processes" и т.д.
                    Utils.RemoveDefenderExclusion(subKey, regThreat.ValueName);
                }

                // 2. Удаление родительского ключа (для SilentExit и плохих параметров Tektonit)
                if (regThreat.ActionDeleteParentKey)
                {
                    string parentPath = GetParentPath(regThreat.KeyPath);
                    string targetKeyName = GetKeyName(regThreat.KeyPath);

                    using (RegistryKey parent = baseKey.OpenSubKey(parentPath, writable: true))
                    {
                        parent?.DeleteSubKeyTree(targetKeyName, throwOnMissingSubKey: false);
                    }
                }
                // 3. Стандартное удаление
                else if (regThreat.ActionDelete)
                {
                    if (regThreat.NodeType == RegistryNodeType.Key)
                    {
                        string parentPath = GetParentPath(regThreat.KeyPath);
                        string targetKeyName = GetKeyName(regThreat.KeyPath);

                        using (RegistryKey parent = baseKey.OpenSubKey(parentPath, writable: true))
                        {
                            parent?.DeleteSubKeyTree(targetKeyName, throwOnMissingSubKey: false);
                        }
                    }
                    else if (regThreat.NodeType == RegistryNodeType.Value)
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(regThreat.KeyPath, writable: true))
                        {
                            key?.DeleteValue(regThreat.ValueName, throwOnMissingValue: false);
                        }
                    }
                }

                // 4. Восстановление исходного значения (Userinit, Shell)
                if (regThreat.ActionSetData)
                {
                    using (RegistryKey key = baseKey.OpenSubKey(regThreat.KeyPath, writable: true))
                    {
                        key?.SetValue(regThreat.ValueName, regThreat.TargetData, regThreat.TargetKind);
                    }
                }

                // 5. Создание/Корректировка соседнего параметра (RequireSignedAppInit_DLLs)
                if (regThreat.ActionSetSibling)
                {
                    using (RegistryKey key = baseKey.OpenSubKey(regThreat.KeyPath, writable: true))
                    {
                        if (key != null)
                        {
                            // Для DWord нужен тип int, парсим строку "1" в int
                            object valData = regThreat.SiblingKind == RegistryValueKind.DWord ?
                                (object)int.Parse(regThreat.SiblingData) : regThreat.SiblingData;

                            key.SetValue(regThreat.SiblingName, valData, regThreat.SiblingKind);
                        }
                    }
                }

                // 6. Если в реестре была ссылка на вредоносный файл (Autorun) — блокируем скрипт/exe
                if (regThreat.LinkedFile != null &&
                    regThreat.LinkedFile.AnalysisResult != null &&
                    regThreat.LinkedFile.AnalysisResult.IsMalicious)
                {
                    UnlockObjectClass.DisableExecute(regThreat.LinkedFile.FilePath);
                }

                if (regThreat.ActionSetData || regThreat.ActionSetSibling)
                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValueRestoredDefault", logPath);
                else
                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValueRemoved", logPath);

                return ApplyResult.Success;
            }
            catch (SecurityException ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, logPath);
                return ApplyResult.Error;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, logPath);
                return ApplyResult.Error;
            }
        }

        // Безопасное разделение пути реестра (не падает на запрещенных символах файлов)
        private string GetParentPath(string fullKeyPath)
        {
            int lastSlash = fullKeyPath.LastIndexOf('\\');
            return lastSlash >= 0 ? fullKeyPath.Substring(0, lastSlash) : string.Empty;
        }

        private string GetKeyName(string fullKeyPath)
        {
            int lastSlash = fullKeyPath.LastIndexOf('\\');
            return lastSlash >= 0 ? fullKeyPath.Substring(lastSlash + 1) : fullKeyPath;
        }
    }
}
