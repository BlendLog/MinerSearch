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

            if (regThreat.ActionQuarantine && !regThreat.ActionDelete && !regThreat.ActionDeleteParentKey)
                regThreat.ActionQuarantine = false;

            bool hasAction = regThreat.ActionDelete || regThreat.ActionDeleteParentKey ||
                             regThreat.ActionSetData || regThreat.ActionSetSibling ||
                             regThreat.ActionRemoveDefenderExclusion || regThreat.ActionQuarantine;

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

                // 0.1. Карантин (только по выбору пользователя): снимок обязан быть создан ДО удаления.
                bool quarantined = false;
                if (regThreat.ActionQuarantine &&
                    (regThreat.ActionDelete || regThreat.ActionDeleteParentKey))
                {
                    // Источник мог исчезнуть: shared-ключ WOW64 (IFEO) уже снят первым дубликатом.
                    if (!RegistrySourceExists(regThreat, baseKey))
                    {
                        AppConfig.GetInstance.LL.LogWarnMessage("_QuarantineSourceMissing", logPath);
                        return ApplyResult.NotApplicable;
                    }

                    if (!QuarantineManager.AddRegistry(regThreat))
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_QuarantineSaveFailed", null, logPath);
                        decision.ActionType = ScanActionType.Error;
                        return ApplyResult.Failed;
                    }
                    quarantined = true;
                }

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
                        object setValue = regThreat.TargetKind == RegistryValueKind.MultiString ?
                            (object)regThreat.TargetDataArray : regThreat.TargetData;
                        key?.SetValue(regThreat.ValueName, setValue, regThreat.TargetKind);
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

                // Определяем ActionType на основе выполненных действий
                if (quarantined)
                {
                    decision.ActionType = ScanActionType.Quarantine;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValueQuarantined", logPath);
                }
                else if (regThreat.ActionSetData || regThreat.ActionSetSibling)
                {
                    decision.ActionType = ScanActionType.Cured;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValueRestoredDefault", logPath);
                }
                else
                {
                    decision.ActionType = ScanActionType.Deleted;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValueRemoved", logPath);
                }

                return ApplyResult.Success;
            }
            catch (SecurityException ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, logPath);
                return ApplyResult.Error;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, logPath);
                return ApplyResult.Error;
            }
        }

        private static bool RegistrySourceExists(RegistryThreatObject regThreat, RegistryKey baseKey)
        {
            try
            {
                bool wholeKey = regThreat.NodeType == RegistryNodeType.Key || regThreat.ActionDeleteParentKey;

                using (RegistryKey key = baseKey.OpenSubKey(regThreat.KeyPath))
                {
                    if (key == null) return false;
                    if (wholeKey) return true;

                    return key.GetValue(regThreat.ValueName, null, RegistryValueOptions.DoNotExpandEnvironmentNames) != null;
                }
            }
            catch
            {
                return true;
            }
        }

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
