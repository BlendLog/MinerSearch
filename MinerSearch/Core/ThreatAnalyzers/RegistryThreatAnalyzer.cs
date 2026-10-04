using DBase;
using Microsoft.Win32;
using MSearch;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MSearch.Core.ThreatAnalyzers
{
    public sealed class RegistryThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.RegistryObject;

        private readonly IFileContentAnalyzer _fileAnalyzer;

        public RegistryThreatAnalyzer(IFileContentAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
        }

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        // Активность Defender: -1 - не проверялось, 0 - отключён, 1 - активен
        private int _defenderActive = -1;
        private bool _defenderInactiveLogged;

        private static readonly HashSet<string> _loggedSections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _sectionLock = new object();

        // Mapping: подраздел_имня (из KeyPath) → default_value для App Paths, помеченных на удаление
        private static readonly Dictionary<string, string> _appPathsMarked = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private void LogSectionHeader(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName)) return;

            lock (_sectionLock)
            {
                if (!_loggedSections.Contains(sectionName))
                {
                    Logger.WriteLog($"[Reg] {sectionName}...", ConsoleColor.DarkCyan);
                    _loggedSections.Add(sectionName);
                }
            }
        }

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanRegistry");
                        _headerLogged = true;
                    }
                }
            }

            var reg = threat as RegistryThreatObject;
            if (reg == null) yield break;


            // Логирование файлов из Autorun для читаемости лога
            LogSectionHeader(reg.SectionName);
            if (reg.SectionName.Contains("Autorun"))
            {
                if (reg.LinkedFile != null)
                {
                    AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_File", $"{reg.ValueName} {reg.ValueData}", ConsoleColor.Gray);
                }
                else if (reg.NodeType == RegistryNodeType.Value)
                {
                    // Файл не найден или не извлечён из командной строки
                    AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", $"{reg.ValueName} {reg.ValueData}");
                }
            }



            int risk = 0;
            bool isMalicious = false;

            // 1. DisallowRun
            string disallowRunNote = AnalyzeDisallowRun(reg, ref risk);

            // 2. Appinit_DLLs
            AnalyzeAppInit(reg, ref risk);

            // 3. App Paths
            AnalyzeAppPaths(reg, ref risk, ref isMalicious);

            // 4. IFEO и WOW6432 IFEO
            AnalyzeIfeo(reg, ref risk, ref isMalicious);

            // 5. Silent Process Exit
            AnalyzeSilentExit(reg, ref risk);

            // 6. System Policies
            AnalyzeSystemPolicies(reg, ref risk);

            // 7. Winlogon (Userinit, Shell)
            AnalyzeWinlogon(reg, ref risk);

            // 8. Tekt0nit (RMS)
            AnalyzeTektonit(reg, ref risk, ref isMalicious);

            // 9. Applocker
            AnalyzeAppLocker(reg, ref risk);

            // 10. Windows Defender
            AnalyzeDefenderExclusions(reg, ref risk);

            // 11. Autorun (Run ключи)
            AnalyzeAutorun(reg, ref risk, ref isMalicious);

            // 12. Lsa Authentication Packages
            AnalyzeLsaAuthenticationPackages(reg, ref risk, ref isMalicious);

            if (risk == 0) yield break; // Угрозы нет

            ScanObjectType objType = isMalicious || risk >= 3 ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            // Решение для объекта реестра
            var decision = new ThreatDecision(reg, risk, objType);
            if (disallowRunNote != null)
                decision.Note = disallowRunNote;
            yield return decision;

            // Решение для связанного файла (если есть флаги действия)
            if (reg.LinkedFile != null &&
                (reg.LinkedFile.ShouldDeleteFile ||
                 reg.LinkedFile.ShouldMoveFileToQuarantine ||
                 reg.LinkedFile.ShouldDisableExecute))
            {
                yield return new ThreatDecision(reg.LinkedFile, risk, objType);
            }
        }

        // --- ПРАВИЛА (Методы-помощники) ---

        private string AnalyzeDisallowRun(RegistryThreatObject reg, ref int risk)
        {
            if (reg.NodeType == RegistryNodeType.Value && reg.KeyPath.Equals(MSData.GetInstance.queries["ExplorerDisallowRun"], StringComparison.OrdinalIgnoreCase))
            {
                // Проверка списка (common debloat targets)
                string exeName = Path.GetFileName(reg.ValueData);
                if (MSData.GetInstance.DisallowRunIgnoreList.Contains(exeName, StringComparer.OrdinalIgnoreCase))
                {
                    AppConfig.GetInstance.LL.LogMessage("[.]", "_RegistryValue", $"{reg.ValueName} | {reg.ValueData}", ConsoleColor.Gray);
                    return null;
                }


                risk += 3;
                reg.ActionDelete = true; 
                AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryValue", $"{reg.ValueName} | {reg.ValueData} |", "_MarkedForRemoval");

                string displayName = !string.IsNullOrEmpty(exeName) ? exeName : reg.ValueName;
                return AppConfig.GetInstance.LL.GetLocalizedString("_NoteDisallowRunBlocked") + " " + displayName;
            }

            return null;
        }

        private void AnalyzeAppInit(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["WindowsNT_CurrentVersion_Windows"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.ValueName.Equals("AppInit_DLLs", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                {
                    risk += 2; // Присутствие DLL в AppInit — уже риск

                    // Требуем от обработчика создать/обновить соседа
                    reg.ActionSetSibling = true;
                    reg.SiblingName = "RequireSignedAppInit_DLLs";
                    reg.SiblingData = "1";
                    reg.SiblingKind = RegistryValueKind.DWord;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.KeyPath + "\\RequireSignedAppInit_DLLs");
                }
            }
        }

        private void AnalyzeIfeo(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (reg.KeyPath.Contains(MSData.GetInstance.queries["IFEO"]) ||
                reg.KeyPath.Contains(MSData.GetInstance.queries["Wow6432Node_IFEO"]))
            {
                if (reg.IsAccessDenied)
                {
                    risk += 1; // Скорее всего антивирус, логируем, но не ломаем
                    return;
                }

                if (reg.NodeType == RegistryNodeType.Value)
                {
                    // Выводим все отладчики в лог
                    if (reg.ValueName.Equals("debugger", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                    {
                        AppConfig.GetInstance.LL.LogMessage("[.]", "_DebuggerString", $"{reg.KeyPath.Split('\\').Last()} {reg.ValueData}", ConsoleColor.Gray);
                    }

                    if (reg.ValueName.Equals("GlobalFlag", StringComparison.OrdinalIgnoreCase) && reg.ValueData == "512") // 0x200
                    {
                        risk += 2;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    else if (reg.ValueName.Equals("debugger", StringComparison.OrdinalIgnoreCase) &&
                             !string.IsNullOrEmpty(reg.ValueData) &&
                             (reg.ValueData.IndexOf(MSData.GetInstance.SysFileName[39], StringComparison.OrdinalIgnoreCase) >= 0 ||
                              reg.ValueData.IndexOf("javascript:", StringComparison.OrdinalIgnoreCase) >= 0 ||
                              reg.ValueData.IndexOf("vbscript:", StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionQuarantine = true;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    else if (reg.ValueName.Equals("debugger", StringComparison.OrdinalIgnoreCase) && IfeoDbgHelper.ShouldRemoveDbg(reg.ValueData))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    // Cross-reference: если Debugger содержит путь, чей .exe совпадает с именем подраздела App Paths, помеченного на удаление
                    else if (reg.ValueName.Equals("debugger", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                    {
                        // Извлекаем последний компонент пути (имя .exe)
                        string exeName = ExtractLastComponent(reg.ValueData);
                        if (!string.IsNullOrEmpty(exeName) && _appPathsMarked.ContainsKey(exeName))
                        {
                            risk += 3;
                            isMalicious = true;
                            reg.ActionDelete = true;
                            AppConfig.GetInstance.LL.LogWarnMediumMessage("_DebuggerAliasAppPath", exeName);
                            AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);

                        }
                    }
                    else if (reg.ValueName.Equals("MinimumStackCommitInBytes", StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(reg.ValueData, out int stack) && stack > 32768)
                        {
                            risk += 3;
                            isMalicious = true;
                            reg.ActionDelete = true;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                        }
                    }
                }
            }
        }

        private void AnalyzeSilentExit(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Contains(MSData.GetInstance.queries["SilentProcessExit"]))
            {
                if (reg.NodeType == RegistryNodeType.Value && reg.ValueName.Equals("MonitorProcess", StringComparison.OrdinalIgnoreCase))
                {
                    risk += 3;
                    reg.ActionDeleteParentKey = true; // Нужно удалить раздел программы целиком
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                }
            }
        }

        private void AnalyzeSystemPolicies(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["SystemPolicies"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.ValueName.Equals("DisableTaskMgr", StringComparison.OrdinalIgnoreCase) ||
                    reg.ValueName.Equals("DisableRegistryTools", StringComparison.OrdinalIgnoreCase))
                {
                    // Значение 0 или отсутствует — ограничений нет, не угроза.
                    // Значение != 0 — ограничения включены, угроза.
                    if (int.TryParse(reg.ValueData, out int disableValue) && disableValue != 0)
                    {
                        risk += 3;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                }
            }
        }

        private void AnalyzeWinlogon(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["WindowsNT_CurrentVersion_Winlogon"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.ValueName.Equals("Userinit", StringComparison.OrdinalIgnoreCase))
                {
                    string defaultData = $@"{AppConfig.GetInstance.drive_letter}:\windows\system32\userinit.exe,";
                    if (!reg.ValueData.Equals(defaultData, StringComparison.InvariantCultureIgnoreCase))
                    {
                        risk += 3;
                        reg.ActionSetData = true;
                        reg.TargetData = defaultData;
                        reg.TargetKind = RegistryValueKind.String;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.ValueName);
                    }
                }
                else if (reg.ValueName.Equals("Shell", StringComparison.OrdinalIgnoreCase))
                {
                    string def1 = "explorer.exe";
                    string def2 = $@"{AppConfig.GetInstance.drive_letter}:\Windows\explorer.exe";
                    if (!reg.ValueData.Equals(def1, StringComparison.InvariantCultureIgnoreCase) &&
                        !reg.ValueData.Equals(def2, StringComparison.InvariantCultureIgnoreCase))
                    {
                        risk += 3;
                        reg.ActionSetData = true;
                        reg.TargetData = def1;
                        reg.TargetKind = RegistryValueKind.String;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.ValueName);
                    }
                }
            }
        }

        private void AnalyzeTektonit(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (reg.KeyPath.EndsWith(MSData.GetInstance.queries["Tekt0nitParameters"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.IsAccessDenied)
                {
                    // Легитимный TektonIT не блокирует доступ - удаляем всегда
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;       // Если ветка, то удалит всю ветку
                    reg.ActionUnlockFirst = true; // Снимаем защиту вируса
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                    return;
                }

                if (reg.NodeType == RegistryNodeType.Value)
                {
                    // Проверяем наличие JohnPatterns в ValueData
                    bool hasJohnPattern = MSData.GetInstance.JohnPatterns.Any(jp =>
                        reg.ValueData.IndexOf(jp, StringComparison.OrdinalIgnoreCase) >= 0);

                    if (!hasJohnPattern)
                        return; // Не считаем угрозой без совпадения с JohnPatterns

                    if (Utils.ContainsNonAscii(reg.ValueName))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionDeleteParentKey = true; // Убиваем весь Tektonit из-за одного плохого параметра
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                    }

                    if (reg.ValueName.Equals("FUSClientPath", StringComparison.OrdinalIgnoreCase))
                    {
                        string path = reg.ValueData;
                        if (path.IndexOf("programdata", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            path.IndexOf("appdata", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            risk += 3;
                            isMalicious = true;
                            reg.ActionDeleteParentKey = true;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                        }
                    }
                }
            }
        }

        private void AnalyzeAppLocker(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Contains(MSData.GetInstance.queries["appl0cker"]))
            {
                if (reg.NodeType == RegistryNodeType.Key)
                {
                    string subKeyName = Path.GetFileName(reg.KeyPath); // Берем хвост пути
                    if (MSData.GetInstance.badSubkeys.Contains(subKeyName, StringComparer.OrdinalIgnoreCase))
                    {
                        risk += 3;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.KeyPath);
                    }
                }
            }
        }

        private void AnalyzeDefenderExclusions(RegistryThreatObject reg, ref int risk)
        {
            if (reg.NodeType != RegistryNodeType.Value) return;

            bool isLocal = reg.KeyPath.Contains(MSData.GetInstance.queries["WDExclusionsLocal"]);
            bool isPolicies = reg.KeyPath.Contains(MSData.GetInstance.queries["WDExclusionsPolicies"]);
            if (!isLocal && !isPolicies) return;

            // Defender отключён - его исключения ни на что не влияют, угрозой не считаем
            if (!IsDefenderActive())
            {
                LogDefenderInactiveOnce();
                return;
            }

            string subKey = Path.GetFileName(reg.KeyPath); // Paths, Processes или Extensions
            bool isBad = false;

            if (subKey.Equals("Processes", StringComparison.OrdinalIgnoreCase))
                isBad = MSData.GetInstance.obfStr4.Contains(reg.ValueName, StringComparer.OrdinalIgnoreCase);
            else if (subKey.Equals("Extensions", StringComparison.OrdinalIgnoreCase))
                isBad = reg.ValueName.Equals(".exe", StringComparison.OrdinalIgnoreCase) || reg.ValueName.Equals(".tmp", StringComparison.OrdinalIgnoreCase);
            else
                isBad = MSData.GetInstance.obfStr3.Contains(reg.ValueName, StringComparer.OrdinalIgnoreCase);

            if (isBad)
            {
                risk += 3;
                if (isLocal)
                {
                    // Локальные исключения Defender'а удаляются спец. методом (WMI)
                    reg.ActionRemoveDefenderExclusion = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRemovedFromExclusions", reg.ValueName);
                }
                else
                {
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
            }
        }

        private bool IsDefenderActive()
        {
            if (_defenderActive < 0)
            {
                _defenderActive = ComputeDefenderActive() ? 1 : 0;
            }
            return _defenderActive == 1;
        }

        private static bool ComputeDefenderActive()
        {
            // 1. Служба должна быть запущена
            try
            {
                using (var service = new ServiceController("WinDefend"))
                {
                    if (service.Status != ServiceControllerStatus.Running) return false;
                }
            }
            catch
            {
                // Служба отсутствует/недоступна - считаем, что Defender не работает
                return false;
            }

            // 2. Read windefender flags
            var queries = MSData.GetInstance.queries;
            string[] baseKeys = { queries["WDBasePolicies"], queries["WDBaseLocal"] };
            string[] flagNames = queries["WDFlags"].Split('|');

            foreach (RegistryView view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
                {
                    foreach (string baseKey in baseKeys)
                    {
                        foreach (string flagName in flagNames)
                        {
                            if (IsDisabledFlag(hklm, baseKey, flagName)) return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool IsDisabledFlag(RegistryKey hklm, string subKey, string valueName)
        {
            try
            {
                using (var key = hklm.OpenSubKey(subKey))
                {
                    object value = key?.GetValue(valueName);
                    return value != null && Convert.ToInt32(value) == 1;
                }
            }
            catch
            {
                return false;
            }
        }

        private void LogDefenderInactiveOnce()
        {
            if (_defenderInactiveLogged) return;
            _defenderInactiveLogged = true;
            AppConfig.GetInstance.LL.LogWarnMessage("_DefenderInactiveExclusionsSkipped");
        }

        private void AnalyzeAutorun(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (reg.NodeType == RegistryNodeType.Value &&
                (reg.KeyPath.EndsWith(MSData.GetInstance.queries["StartupRun"], StringComparison.OrdinalIgnoreCase) ||
                 reg.KeyPath.EndsWith(MSData.GetInstance.queries["Wow6432Node_StartupRun"], StringComparison.OrdinalIgnoreCase)))
            {
                string val = reg.ValueData;

                // 1. Эвристика по строке
                if (val.IndexOf(MSData.GetInstance.SysFileName[39], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    val.IndexOf("javascript:", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    val.IndexOf("vbscript:", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionQuarantine = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
                else if (val.IndexOf("RealtekHD\\task", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    val.IndexOf("ReaItekHD\\task", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    (val.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && val.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (val.IndexOf("regsvr32", StringComparison.OrdinalIgnoreCase) >= 0 && (val.Contains("/u") || val.Contains("/s") || val.Contains("/i:"))))
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
                else if (val.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         val.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    risk += 2;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }

                // 2. Анализ привязанного файла, если он был найден сканером
                if (reg.LinkedFile != null)
                {
                    if (reg.LinkedFile.IsValidSignature) return;
                    // Проверяем SFX-статус файла
                    bool isSfx = FileChecker.IsSfxArchive(reg.LinkedFile.FilePath);
                    
                    if (isSfx)
                    {
                        var sfxLevel = FileChecker.GetSfxTrustLevel(reg.LinkedFile.FilePath);
                        
                        switch (sfxLevel)
                        {
                            case SfxTrustLevel.BadCert:
                                // Проблемная подпись — пониженный риск
                                if (!reg.ActionDelete)
                                {
                                    risk += 1;
                                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", reg.LinkedFile.FilePath);
                                    reg.LinkedFile.ShouldMoveFileToQuarantine = true;
                                }
                                break;

                            case SfxTrustLevel.SignedValid:
                                // Валидная подпись — просто лог
                                AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", reg.LinkedFile.FilePath);
                                break;

                            default:
                                // Unsigned, Unknown или NotSfx — игнорируем
                                break;
                        }
                    }

                    // Статический анализ самого файла (SignatureFileAnalyzer)
                    var fileResult = _fileAnalyzer.Analyze(reg.LinkedFile, false);
                    if (fileResult.IsMalicious && !reg.ActionDelete)
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                        reg.LinkedFile.AnalysisResult = FileContentAnalysisResult.Malicious();

                        if (IsKnownMaliciousFile(reg.LinkedFile.FilePath))
                            reg.LinkedFile.ShouldDeleteFile = true;
                        else
                            reg.LinkedFile.ShouldMoveFileToQuarantine = true;

                    }
                    else
                    {
                        // Простые проверки фактов размера/подписи
                        if (reg.LinkedFile.FileSize >= reg.LinkedFile.MAX_FILE_SIZE)
                        {
                            AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousFileSize", FileChecker.GetFileSize(reg.LinkedFile.FileSize));
                            reg.LinkedFile.AnalysisResult = FileContentAnalysisResult.Suspicious();
                        }
                    }
                }
            }
        }

        private void AnalyzeLsaAuthenticationPackages(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["LsaAuthenticationPackages"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.NodeType == RegistryNodeType.Value && reg.ValueName.Equals("Authentication Packages", StringComparison.OrdinalIgnoreCase))
                {
                    // REG_MULTI_SZ — стандартное значение содержит только "msv1_0"
                    if (reg.ValueKind == RegistryValueKind.MultiString)
                    {
                        // Проверяем массив элементов: должен быть ровно ["msv1_0"]
                        if (reg.ValueDataArray == null || reg.ValueDataArray.Length != 1 || !reg.ValueDataArray[0].Equals("msv1_0", StringComparison.OrdinalIgnoreCase))
                        {
                            risk += 3;
                            isMalicious = true;
                            reg.ActionSetData = true;
                            reg.TargetDataArray = new string[] { "msv1_0" };
                            reg.TargetKind = RegistryValueKind.MultiString;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.ValueName);

                            // Устанавливаем RunAsPPL = 1
                            reg.ActionSetSibling = true;
                            reg.SiblingName = "RunAsPPL";
                            reg.SiblingData = "1";
                            reg.SiblingKind = RegistryValueKind.DWord;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", "RunAsPPL");
                        }
                    }
                }
            }
        }

        private void AnalyzeAppPaths(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            // Проверяем что путь содержит App Paths
            if (!reg.KeyPath.Contains(MSData.GetInstance.queries["AppPaths"]))
                return;

            // Только для Key NodeType с (default) значением
            if (reg.NodeType == RegistryNodeType.Key && reg.ValueName.Equals("(default)", StringComparison.OrdinalIgnoreCase))
            {
                // Проверяем расширение ValueData
                if (!string.IsNullOrEmpty(reg.ValueData) &&
                    (reg.ValueData.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) ||
                     reg.ValueData.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase)))
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.KeyPath);

                    // Сохраняем mapping: имя_подразела → default_value для cross-reference с IFEO
                    string subKeyName = Path.GetFileName(Path.GetDirectoryName(reg.KeyPath));
                    if (!string.IsNullOrEmpty(subKeyName))
                    {
                        _appPathsMarked[subKeyName] = reg.ValueData;
                        AppConfig.GetInstance.LL.LogMessage("[.]", "_AppPathsMapped", $"{subKeyName} → {reg.ValueData}", ConsoleColor.DarkYellow);
                    }
                }
            }
        }

        bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.IsKnownMaliciousPath(filePath);
        }

        private static string GetKeyName(string keyPath)
        {
            return Path.GetFileName(keyPath) ?? keyPath;
        }

        /// <summary>
        /// Извлекает последний компонент пути (имя .exe) из строки Debugger.
        /// Обрабатывает пути с пробелами и без.
        /// </summary>
        private static string ExtractLastComponent(string valueData)
        {
            if (string.IsNullOrEmpty(valueData))
                return null;

            // Убираем лидирующие/концевые пробелы и кавычки
            string trimmed = valueData.Trim().Trim('"');

            // Если есть пробелы — берём последний токен
            if (trimmed.IndexOf(' ') >= 0)
            {
                string[] parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string lastPart = parts.Last();
                // Убираем кавычки если остались
                lastPart = lastPart.Trim('"');
                return lastPart;
            }
            else
            {
                // Нет пробелов — весь trimmed это один компонент
                return trimmed;
            }
        }
    }
}
