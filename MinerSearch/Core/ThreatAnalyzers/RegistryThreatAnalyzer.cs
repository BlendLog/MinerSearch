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

        // Mapping: подраздел (из KeyPath) → default_value для App Paths, помеченных на удаление
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

            FileChecker.LogUnsignedSha1(reg.LinkedFile);

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

            string disallowRunNote = AnalyzeDisallowRun(reg, ref risk);

            AnalyzeAppInit(reg, ref risk);
            AnalyzeAppPaths(reg, ref risk, ref isMalicious);
            AnalyzeIfeo(reg, ref risk, ref isMalicious);
            AnalyzeSilentExit(reg, ref risk);
            AnalyzeSystemPolicies(reg, ref risk);
            AnalyzeWinlogon(reg, ref risk);
            AnalyzeTektonit(reg, ref risk, ref isMalicious);
            AnalyzeAppLocker(reg, ref risk);
            AnalyzeDefenderExclusions(reg, ref risk);
            AnalyzeAutorun(reg, ref risk, ref isMalicious);
            AnalyzeLsaAuthenticationPackages(reg, ref risk, ref isMalicious);
            AnalyzeCustomClsids(reg);

            if (risk == 0) yield break;

            ScanObjectType objType = isMalicious || risk >= 3 ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            var decision = new ThreatDecision(reg, risk, objType);
            if (disallowRunNote != null)
                decision.Note = disallowRunNote;
            yield return decision;

            if (reg.LinkedFile != null &&
                (reg.LinkedFile.ShouldDeleteFile ||
                 reg.LinkedFile.ShouldMoveFileToQuarantine ||
                 reg.LinkedFile.ShouldDisableExecute))
            {
                yield return new ThreatDecision(reg.LinkedFile, risk, objType);
            }
        }

        private string AnalyzeDisallowRun(RegistryThreatObject reg, ref int risk)
        {
            if (reg.NodeType == RegistryNodeType.Value && reg.KeyPath.Equals(MSData.GetInstance.queries["ExplorerDisallowRun"], StringComparison.OrdinalIgnoreCase))
            {
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
                if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.AppInitDlls], StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                {
                    risk += 2;

                    // Требуем от обработчика создать/обновить соседа
                    reg.ActionSetSibling = true;
                    reg.SiblingName = MSData.GetInstance.regValueNames[MSKeys.RequireSignedAppInitDlls];
                    reg.SiblingData = "1";
                    reg.SiblingKind = RegistryValueKind.DWord;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.KeyPath + "\\" + MSData.GetInstance.regValueNames[MSKeys.RequireSignedAppInitDlls]);
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
                    if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Debugger], StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                    {
                        AppConfig.GetInstance.LL.LogMessage("[.]", "_DebuggerString", $"{reg.KeyPath.Split('\\').Last()} {reg.ValueData}", ConsoleColor.Gray);
                    }

                    if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.GlobalFlag], StringComparison.OrdinalIgnoreCase) && reg.ValueData == "512") // 0x200
                    {
                        risk += 2;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    else if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Debugger], StringComparison.OrdinalIgnoreCase) &&
                             !string.IsNullOrEmpty(reg.ValueData) &&
                             (reg.ValueData.IndexOf(MSData.GetInstance.SysFileName[39], StringComparison.OrdinalIgnoreCase) >= 0 ||
                              MSData.ContainsAnyMarker(reg.ValueData, MSData.GetInstance.markerSets[MSKeys.ScriptMarkers])))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionQuarantine = true;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    else if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Debugger], StringComparison.OrdinalIgnoreCase) && IfeoDbgHelper.ShouldRemoveDbg(reg.ValueData))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionDelete = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                    }
                    // Cross-reference: если Debugger содержит путь, чей .exe совпадает с именем подраздела App Paths, помеченного на удаление
                    else if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Debugger], StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(reg.ValueData))
                    {
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
                    else if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.MinimumStackCommit], StringComparison.OrdinalIgnoreCase))
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
                if (reg.NodeType == RegistryNodeType.Value && reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.MonitorProcess], StringComparison.OrdinalIgnoreCase))
                {
                    risk += 3;
                    reg.ActionDeleteParentKey = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                }
            }
        }

        private void AnalyzeSystemPolicies(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["SystemPolicies"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.DisableTaskMgr], StringComparison.OrdinalIgnoreCase) ||
                    reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.DisableRegistryTools], StringComparison.OrdinalIgnoreCase))
                {
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
                if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Userinit], StringComparison.OrdinalIgnoreCase))
                {
                    string defaultData = AppConfig.GetInstance.drive_letter + MSData.GetInstance.consts[MSKeys.UserinitDefault];
                    if (!reg.ValueData.Equals(defaultData, StringComparison.InvariantCultureIgnoreCase))
                    {
                        risk += 3;
                        reg.ActionSetData = true;
                        reg.TargetData = defaultData;
                        reg.TargetKind = RegistryValueKind.String;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.ValueName);
                    }
                }
                else if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.WinlogonShell], StringComparison.OrdinalIgnoreCase))
                {
                    string def1 = MSData.GetInstance.consts[MSKeys.ExplorerExe];
                    string def2 = AppConfig.GetInstance.drive_letter + MSData.GetInstance.consts[MSKeys.ExplorerExePath];
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
                    reg.ActionDelete = true;
                    reg.ActionUnlockFirst = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                    return;
                }

                if (reg.NodeType == RegistryNodeType.Value)
                {
                    bool hasJohnPattern = MSData.GetInstance.JohnPatterns.Any(jp =>
                        reg.ValueData.IndexOf(jp, StringComparison.OrdinalIgnoreCase) >= 0);

                    if (!hasJohnPattern)
                        return;

                    if (Utils.ContainsNonAscii(reg.ValueName))
                    {
                        risk += 3;
                        isMalicious = true;
                        reg.ActionDeleteParentKey = true;
                        AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                    }

                    if (reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.FusClientPath], StringComparison.OrdinalIgnoreCase))
                    {
                        string path = reg.ValueData;
                        if (MSData.ContainsAnyMarker(path, MSData.GetInstance.markerSets[MSKeys.UserWritableMarkers]))
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
                    string subKeyName = Path.GetFileName(reg.KeyPath);
                    if (MSData.GetInstance.badApplockerRules.Contains(subKeyName, StringComparer.OrdinalIgnoreCase))
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

            if (subKey.Equals(MSData.GetInstance.regValueNames[MSKeys.DefenderProcesses], StringComparison.OrdinalIgnoreCase))
                isBad = MSData.GetInstance.obfStr4.Contains(reg.ValueName, StringComparer.OrdinalIgnoreCase);
            else if (subKey.Equals(MSData.GetInstance.regValueNames[MSKeys.DefenderExtensions], StringComparison.OrdinalIgnoreCase))
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
            // Служба должна быть запущена
            try
            {
                using (var service = new ServiceController(MSData.GetInstance.consts[MSKeys.WinDefendService]))
                {
                    if (service.Status != ServiceControllerStatus.Running) return false;
                }
            }
            catch
            {
                // Служба отсутствует/недоступна - считаем, что Defender не работает
                return false;
            }

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

                bool hasMinerCli = false;
                foreach (string minerMarker in MSData.GetInstance.badArgStrings)
                {
                    if (val.IndexOf(minerMarker, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        hasMinerCli = true;
                        break;
                    }
                }

                if (hasMinerCli ||
                    val.IndexOf(MSData.GetInstance.SysFileName[39], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    MSData.ContainsAnyMarker(val, MSData.GetInstance.markerSets[MSKeys.ScriptMarkers]))
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionQuarantine = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
                else if (MSData.ContainsAnyMarker(val, MSData.GetInstance.markerSets[MSKeys.RealtekTaskMarkers]) ||
                    MSData.ContainsAllMarkers(val, MSData.GetInstance.markerSets[MSKeys.AutorunChainMarkers]) ||
                    (val.IndexOf(MSData.GetInstance.hostNames[MSKeys.Regsvr32], StringComparison.OrdinalIgnoreCase) >= 0 &&
                     MSData.ContainsAnyMarker(val, MSData.GetInstance.markerSets[MSKeys.RegsvrFlagMarkers])))
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
                else if (MSData.ContainsAnyMarker(val, MSData.GetInstance.markerSets[MSKeys.AutorunWeakMarkers]))
                {
                    risk += 2;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }

                if (reg.LinkedFile != null)
                {
                    if (reg.LinkedFile.IsValidSignature) return;
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
                if (reg.NodeType == RegistryNodeType.Value && reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.AuthenticationPackages], StringComparison.OrdinalIgnoreCase))
                {
                    // REG_MULTI_SZ — стандартное значение содержит только "msv1_0"
                    if (reg.ValueKind == RegistryValueKind.MultiString)
                    {
                        string msv10 = MSData.GetInstance.consts[MSKeys.Msv10];

                        if (reg.ValueDataArray == null || reg.ValueDataArray.Length != 1 || !reg.ValueDataArray[0].Equals(msv10, StringComparison.OrdinalIgnoreCase))
                        {
                            risk += 3;
                            isMalicious = true;
                            reg.ActionSetData = true;
                            reg.TargetDataArray = new string[] { msv10 };
                            reg.TargetKind = RegistryValueKind.MultiString;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", reg.ValueName);

                            reg.ActionSetSibling = true;
                            reg.SiblingName = MSData.GetInstance.regValueNames[MSKeys.RunAsPpl];
                            reg.SiblingData = "1";
                            reg.SiblingKind = RegistryValueKind.DWord;
                            AppConfig.GetInstance.LL.LogSuccessMessage("_WillBeRestoredToDefault", MSData.GetInstance.regValueNames[MSKeys.RunAsPpl]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Разделы [Reg] HKU Custom CLSIDs / HKLM Custom CLSIDs: сбор информации о пользовательских
        /// и общих InprocServer32. Неподписанные обработчики выводятся предупреждением, остальные —
        /// только при --verbose. Решения не создаются: этап сбора данных, действия не применяются.
        /// </summary>
        private void AnalyzeCustomClsids(RegistryThreatObject reg)
        {
            if (reg.SectionName == null ||
                !reg.SectionName.EndsWith("Custom CLSIDs", StringComparison.Ordinal))
                return;

            string guid = GetKeyName(reg.KeyPath);
            string handlerPath = reg.ValueData;

            if (reg.LinkedFile != null && !reg.LinkedFile.IsValidSignature)
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ClsidCustomHandler",
                    $"{guid} [{reg.LinkedFile.TrustResult}] -> {handlerPath}");
            }
            else if (LaunchOptions.GetInstance.verbose)
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_ClsidHandler", $"{guid} -> {handlerPath}", ConsoleColor.Gray);
            }
        }

        private void AnalyzeAppPaths(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (!reg.KeyPath.Contains(MSData.GetInstance.queries["AppPaths"]))
                return;

            if (reg.NodeType == RegistryNodeType.Key && reg.ValueName.Equals(MSData.GetInstance.regValueNames[MSKeys.Default], StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(reg.ValueData) &&
                    MSData.GetInstance.batchExtensions.Any(ext => reg.ValueData.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                {
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.KeyPath);

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

            string trimmed = valueData.Trim().Trim('"');

            if (trimmed.IndexOf(' ') >= 0)
            {
                string[] parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string lastPart = parts.Last();
                lastPart = lastPart.Trim('"');
                return lastPart;
            }
            else
            {
                return trimmed;
            }
        }
    }
}
