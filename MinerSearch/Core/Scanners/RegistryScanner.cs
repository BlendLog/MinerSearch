using DBase;
using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;

namespace MSearch.Core.Scanners
{
    public class RegistryScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            const string HKLM = "HKEY_LOCAL_MACHINE";
            const string HKCU = "HKEY_CURRENT_USER";

            List<IThreatObject> results = new List<IThreatObject>();
            var msData = MSData.GetInstance;

            // --- 1. DisallowRun (HKCU) ---
            CollectKeyAndValues(results, HKCU, msData.queries["ExplorerPolicies"], sectionName: "DisallowRun");

            // --- 2. Appinit_dlls (HKLM) ---
            CollectKeyAndValues(results, HKLM, msData.queries["WindowsNT_CurrentVersion_Windows"], sectionName: "AppInitDLL");

            // --- 3. IFEO и WOW6432_IFEO (HKLM) ---
            CollectSubkeysAndTheirValues(results, HKLM, msData.queries["IFEO"], "IFEO");
            CollectSubkeysAndTheirValues(results, HKLM, msData.queries["Wow6432Node_IFEO"], "IFEO WOW6432");

            // --- 4. SilentProcessExit (HKLM) ---
            CollectSubkeysAndTheirValues(results, HKLM, msData.queries["SilentProcessExit"], "Silent_Exit_Process");

            // --- 5. Autorun (HKLM, HKCU, WOW6432) ---
            CollectKeyAndValues(results, HKLM, msData.queries["StartupRun"], attachFiles: true, sectionName: "HKLM Autorun");
            CollectKeyAndValues(results, HKCU, msData.queries["StartupRun"], attachFiles: true, sectionName: "HKCU Autorun");
            CollectKeyAndValues(results, HKLM, msData.queries["Wow6432Node_StartupRun"], attachFiles: true, sectionName: "Wow64Node Autorun");

            // --- 6. Winlogon System settings (HKLM) ---
            CollectKeyAndValues(results, HKLM, msData.queries["WindowsNT_CurrentVersion_Winlogon"], sectionName: "HKLM System settings");

            // --- 7. System policies (HKCU) ---
            CollectKeyAndValues(results, HKCU, msData.queries["SystemPolicies"], sectionName: "HKCU System policies");

            // --- 8. Tekt0nit (RMS) (HKCU) ---
            CollectKeyAndValues(results, HKCU, msData.queries["Tekt0nitParameters"], sectionName: "TektonIT");

            // --- 9. Lsa Authentication Packages (HKLM) ---
            CollectKeyAndValues(results, HKLM, msData.queries["LsaAuthenticationPackages"], sectionName: "LSA Authentication Packages");

            // --- 10. Applocker (HKLM) ---
            CollectSubkeysOnly(results, HKLM, msData.queries["appl0cker"], "Applocker");

            // --- 11. Windows Defender Exclusions (HKLM - Local и Policies) ---
            string[] wdBaseKeys = { msData.queries["WDExclusionsLocal"], msData.queries["WDExclusionsPolicies"] };
            string[] wdSubKeys = { "Paths", "Processes", "Extensions" };

            foreach (string wdBaseKey in wdBaseKeys)
            {
                foreach (string subKey in wdSubKeys)
                {
                    CollectKeyAndValues(results, HKLM, $@"{wdBaseKey}\{subKey}", sectionName: $"WindowsDefender ({subKey})");
                }
            }

            // --- 12. App Paths (HKLM) ---
            CollectSubkeysAndSpecifiedValue(results, HKLM, msData.queries["AppPaths"], "App Paths");

            return results;
        }


        RegistryKey GetBaseHive(string hiveName)
        {
            return hiveName == "HKEY_LOCAL_MACHINE" ? Registry.LocalMachine : Registry.CurrentUser;
        }

        FileThreatObject TryExtractLinkedFile(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine)) return null;

            try
            {
                string path = FileSystemManager.ExtractExecutableFromCommand(commandLine);
                if (string.IsNullOrEmpty(path) || !File.Exists(path) || FileSystemManager.IsAppExecutionAlias(path))
                {
                    return null;
                }

                WinVerifyTrustResult trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(path, true);
                long fileSize = new FileInfo(path).Length;

                var fileInfo = FileVersionInfo.GetVersionInfo(path);
                string fileDescription = fileInfo.FileDescription;
                string fileOriginalName = fileInfo.OriginalFilename;

                string hash = trustResult != WinVerifyTrustResult.Success ? FileChecker.CalculateSHA1(path) : "";
                return new FileThreatObject(path, Path.GetFileName(path), fileSize, fileOriginalName, fileDescription, hash, trustResult);
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            return null;
        }

        void CollectKeyAndValues(List<IThreatObject> results, string hive, string keyPath, bool attachFiles = false, string sectionName = null)
        {
            RegistryKey baseReg = GetBaseHive(hive);
            try
            {
                using (RegistryKey key = baseReg.OpenSubKey(keyPath))
                {
                    if (key != null)
                    {
                        var regObj = new RegistryThreatObject(hive, keyPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, false, null)
                        {
                            SectionName = sectionName
                        };
                        results.Add(regObj);

                        foreach (string valName in key.GetValueNames())
                        {
                            object rawValue = key.GetValue(valName);
                            if (rawValue != null)
                            {
                                RegistryValueKind kind = key.GetValueKind(valName);
                                string stringVal = rawValue.ToString();
                                FileThreatObject linkedFile = attachFiles ? TryExtractLinkedFile(stringVal) : null;
                                var valRegObj = new RegistryThreatObject(hive, keyPath, RegistryNodeType.Value, valName, stringVal, kind, false, linkedFile)
                                {
                                    SectionName = sectionName
                                };
                                
                                // Для MULTI_SZ заполняем массив элементов
                                if (kind == RegistryValueKind.MultiString && rawValue is string[] arr)
                                {
                                    valRegObj.ValueDataArray = arr;
                                }
                                
                                results.Add(valRegObj);
                            }
                        }

                        // Проверяем наличие вложенных ключей и записываем их тоже (нужно например для DisallowRun)
                        foreach (string subKeyName in key.GetSubKeyNames())
                        {
                            var subRegObj = new RegistryThreatObject(hive, $@"{keyPath}\{subKeyName}", RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, false, null)
                            {
                                SectionName = sectionName
                            };
                            results.Add(subRegObj);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_AccessDenied", $"{hive}\\{keyPath}");
                var regObj = new RegistryThreatObject(hive, keyPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { /* Игнорируем недоступные/сломанные пути */ }
        }

        void CollectSubkeysAndTheirValues(List<IThreatObject> results, string hive, string parentPath, string sectionName)
        {
            RegistryKey baseReg = GetBaseHive(hive);
            try
            {
                using (RegistryKey parentKey = baseReg.OpenSubKey(parentPath))
                {
                    if (parentKey != null)
                    {
                        foreach (string subKeyName in parentKey.GetSubKeyNames())
                        {
                            // Рекурсивно вызываем нашу же функцию для каждого дочернего элемента
                            CollectKeyAndValues(results, hive, $@"{parentPath}\{subKeyName}", false, sectionName);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_AccessDenied", $"{hive}\\{parentPath}");
                var regObj = new RegistryThreatObject(hive, parentPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { }
        }

        void CollectSubkeysOnly(List<IThreatObject> results, string hive, string parentPath, string sectionName)
        {
            RegistryKey baseReg = GetBaseHive(hive);
            try
            {
                using (RegistryKey parentKey = baseReg.OpenSubKey(parentPath))
                {
                    if (parentKey != null)
                    {
                        foreach (string subKeyName in parentKey.GetSubKeyNames())
                        {
                            var regObj = new RegistryThreatObject(hive, $@"{parentPath}\{subKeyName}", RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, false, null)
                            {
                                SectionName = sectionName
                            };
                            results.Add(regObj);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_AccessDenied", $"{hive}\\{parentPath}");
                var regObj = new RegistryThreatObject(hive, parentPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { }
        }

        void CollectSubkeysAndSpecifiedValue(List<IThreatObject> results, string hive, string parentPath, string sectionName)
        {
            RegistryKey baseReg = GetBaseHive(hive);
            try
            {
                using (RegistryKey parentKey = baseReg.OpenSubKey(parentPath))
                {
                    if (parentKey != null)
                    {
                        foreach (string subKeyName in parentKey.GetSubKeyNames())
                        {
                            string subKeyPath = $@"{parentPath}\{subKeyName}";
                            try
                            {
                                using (RegistryKey subKey = baseReg.OpenSubKey(subKeyPath))
                                {
                                    if (subKey != null)
                                    {
                                        // Читаем значение по умолчанию (null имя)
                                        object defaultValue = subKey.GetValue(null);
                                        string valueData = defaultValue?.ToString() ?? string.Empty;

                                        // Добавляем Key-объект со значением для дальнейшего анализа
                                        var regObj = new RegistryThreatObject(hive, subKeyPath, RegistryNodeType.Key, "(default)", valueData, RegistryValueKind.Unknown, false, null)
                                        {
                                            SectionName = sectionName
                                        };
                                        results.Add(regObj);
                                    }
                                }
                            }
                            catch (SecurityException)
                            {
                                AppConfig.GetInstance.LL.LogCautionMessage("_AccessDenied", $@"{hive}\{subKeyPath}");
                                var regObj = new RegistryThreatObject(hive, subKeyPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                                {
                                    SectionName = sectionName
                                };
                                results.Add(regObj);
                            }
                            catch (Exception) { /* Игнорируем недоступные/сломанные пути */ }
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_AccessDenied", $"{hive}\\{parentPath}");
                var regObj = new RegistryThreatObject(hive, parentPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { }
        }
    }
}
