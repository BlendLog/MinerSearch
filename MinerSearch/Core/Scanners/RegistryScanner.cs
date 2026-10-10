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
            CollectKeyAndValues(results, HKCU, msData.queries["ExplorerDisallowRun"], sectionName: "DisallowRun");

            // --- 2. Appinit_dlls (HKLM) ---
            CollectKeyAndValues(results, HKLM, msData.queries["WindowsNT_CurrentVersion_Windows"], sectionName: "AppInitDLL");

            // --- 3. IFEO и WOW6432_IFEO (HKLM) ---
            // IFEO — shared-ключ WOW64: 64-bit и WOW6432Node пути указывают на одно хранилище.
            // Собираем имена 64-битного прохода и пропускаем их во втором, чтобы не дублировать угрозы.
            var ifeo64Names = GetSubKeyNames(HKLM, msData.queries["IFEO"]);
            CollectSubkeysAndTheirValues(results, HKLM, msData.queries["IFEO"], "IFEO");
            CollectSubkeysAndTheirValues(results, HKLM, msData.queries["Wow6432Node_IFEO"], "IFEO WOW6432", ifeo64Names);

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
            string[] wdSubKeys = {
                msData.regValueNames[MSKeys.DefenderPaths],
                msData.regValueNames[MSKeys.DefenderProcesses],
                msData.regValueNames[MSKeys.DefenderExtensions]
            };

            foreach (string wdBaseKey in wdBaseKeys)
            {
                foreach (string subKey in wdSubKeys)
                {
                    CollectKeyAndValues(results, HKLM, $@"{wdBaseKey}\{subKey}", sectionName: $"WindowsDefender ({subKey})");
                }
            }

            // --- 12. App Paths (HKLM) ---
            CollectSubkeysAndSpecifiedValue(results, HKLM, msData.queries["AppPaths"], "App Paths");

            // --- 13. Custom CLSIDs (пользовательские COM-обработчики, HKU) ---
            var clsidLinkedFileCache = new Dictionary<string, FileThreatObject>(StringComparer.OrdinalIgnoreCase);
            CollectHkuClsidHandlers(results, clsidLinkedFileCache);

            // --- 14. Custom CLSIDs (общие COM-обработчики, HKLM) ---
            CollectHklmClsidHandlers(results, clsidLinkedFileCache);

            return results;
        }


        RegistryKey GetBaseHive(string hiveName)
        {
            return hiveName == "HKEY_LOCAL_MACHINE" ? Registry.LocalMachine : Registry.CurrentUser;
        }

        FileThreatObject TryExtractLinkedFile(string commandLine, bool showUnsigned = true)
        {
            if (string.IsNullOrEmpty(commandLine)) return null;

            try
            {
                string path = FileSystemManager.ExtractExecutableFromCommand(commandLine);
                if (string.IsNullOrEmpty(path) || !File.Exists(path) || FileSystemManager.IsAppExecutionAlias(path))
                {
                    return null;
                }

                WinVerifyTrustResult trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(path, showUnsigned);
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
            catch (SecurityException se)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $"{hive}\\{keyPath}");
                var regObj = new RegistryThreatObject(hive, keyPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { /* Игнорируем недоступные/сломанные пути */ }
        }

        /// <summary>
        /// Имена непосредственных подразделов ключа (для дедупликации shared-ключей WOW64).
        /// </summary>
        HashSet<string> GetSubKeyNames(string hive, string parentPath)
        {
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                RegistryKey baseReg = GetBaseHive(hive);
                using (RegistryKey parentKey = baseReg.OpenSubKey(parentPath))
                {
                    if (parentKey != null)
                    {
                        foreach (string name in parentKey.GetSubKeyNames())
                            names.Add(name);
                    }
                }
            }
            catch { }
            return names;
        }

        void CollectSubkeysAndTheirValues(List<IThreatObject> results, string hive, string parentPath, string sectionName, HashSet<string> skipNames = null)
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
                            // shared-ключи WOW64 (IFEO) уже отданы 64-битным проходом — не дублируем
                            if (skipNames != null && skipNames.Contains(subKeyName))
                                continue;

                            // Рекурсивно вызываем нашу же функцию для каждого дочернего элемента
                            CollectKeyAndValues(results, hive, $@"{parentPath}\{subKeyName}", false, sectionName);
                        }
                    }
                }
            }
            catch (SecurityException se)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $"{hive}\\{parentPath}");
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
            catch (SecurityException se)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $"{hive}\\{parentPath}");
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
                                        var regObj = new RegistryThreatObject(hive, subKeyPath, RegistryNodeType.Key, MSData.GetInstance.regValueNames[MSKeys.Default], valueData, RegistryValueKind.Unknown, false, null)
                                        {
                                            SectionName = sectionName
                                        };
                                        results.Add(regObj);
                                    }
                                }
                            }
                            catch (SecurityException se)
                            {
                                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $@"{hive}\{subKeyPath}");
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
            catch (SecurityException se)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $"{hive}\\{parentPath}");
                var regObj = new RegistryThreatObject(hive, parentPath, RegistryNodeType.Key, null, null, RegistryValueKind.Unknown, true, null)
                {
                    SectionName = sectionName
                };
                results.Add(regObj);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Пользовательские COM-обработчики (default-значение подраздела InprocServer32) из веток Classes
        /// загруженных профилей: HKEY_USERS\&lt;SID&gt;_Classes\CLSID\&lt;GUID&gt;\InprocServer32.
        /// HKCU\Software\Classes — merged-представление этих же данных, поэтому отдельно не сканируется.
        /// </summary>
        void CollectHkuClsidHandlers(List<IThreatObject> results, Dictionary<string, FileThreatObject> linkedFileCache)
        {
            const string sectionName = "HKU Custom CLSIDs";

            try
            {
                RegistryKey users = Registry.Users;
                if (users == null) return;

                foreach (string sidKeyName in users.GetSubKeyNames())
                {
                    if (!sidKeyName.EndsWith("_Classes", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string clsidPath = $@"{sidKeyName}\CLSID";

                    try
                    {
                        CollectClsidHandlers(results, "HKEY_USERS", "HKU", users, clsidPath, sectionName, linkedFileCache);
                    }
                    catch (SecurityException se)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $@"HKEY_USERS\{clsidPath}");
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Общие COM-обработчики из HKLM\SOFTWARE\Classes\CLSID (64-битное представление, без Wow6432Node).
        /// </summary>
        void CollectHklmClsidHandlers(List<IThreatObject> results, Dictionary<string, FileThreatObject> linkedFileCache)
        {
            const string sectionName = "HKLM Custom CLSIDs";
            const string clsidPath = @"SOFTWARE\Classes\CLSID";

            try
            {
                CollectClsidHandlers(results, "HKEY_LOCAL_MACHINE", "HKLM", Registry.LocalMachine, clsidPath, sectionName, linkedFileCache);
            }
            catch (SecurityException se)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $@"HKEY_LOCAL_MACHINE\{clsidPath}");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Общий перебор CLSID\{GUID}\InprocServer32 под указанным корнем.
        /// linkedFileCache исключает повторные WinTrust/SHA1/FileVersionInfo для одинаковых путей.
        /// </summary>
        void CollectClsidHandlers(
            List<IThreatObject> results,
            string hive,
            string hiveShort,
            RegistryKey baseKey,
            string clsidPath,
            string sectionName,
            Dictionary<string, FileThreatObject> linkedFileCache)
        {
            using (RegistryKey clsidKey = baseKey.OpenSubKey(clsidPath))
            {
                if (clsidKey == null)
                    return;

                foreach (string guidName in clsidKey.GetSubKeyNames())
                {
                    string guidPath = $@"{clsidPath}\{guidName}";
                    string inprocPath = $@"{guidPath}\{MSData.GetInstance.regValueNames[MSKeys.InprocServer32]}";

                    try
                    {
                        using (RegistryKey inprocKey = baseKey.OpenSubKey(inprocPath))
                        {
                            if (inprocKey == null)
                                continue;

                            // Путь к DLL хранится в default-значении подраздела InprocServer32
                            object rawValue = inprocKey.GetValue(null);
                            if (rawValue == null)
                                continue;

                            string valueData = rawValue.ToString();
                            if (string.IsNullOrWhiteSpace(valueData))
                                continue;

                            RegistryValueKind kind = inprocKey.GetValueKind(null);

                            FileThreatObject linkedFile;
                            if (!linkedFileCache.TryGetValue(valueData, out linkedFile))
                            {
                                // showUnsigned: false — выводом управляет анализатор (по verbose)
                                linkedFile = TryExtractLinkedFile(valueData, showUnsigned: false);
                                linkedFileCache[valueData] = linkedFile;
                            }

                            var regObj = new RegistryThreatObject(
                                hive, guidPath, RegistryNodeType.Key,
                                MSData.GetInstance.regValueNames[MSKeys.InprocServer32], valueData, kind, false, linkedFile)
                            {
                                SectionName = sectionName
                            };
                            results.Add(regObj);
                        }
                    }
                    catch (SecurityException se)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_AccessDenied", se, $@"{hiveShort}\{inprocPath}");
                    }
                    catch (Exception) { /* Игнорируем недоступные/сломанные пути */ }
                }
            }
        }
    }
}
