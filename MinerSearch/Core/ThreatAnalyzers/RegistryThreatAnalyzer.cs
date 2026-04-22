using DBase;
using Microsoft.Win32;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private static readonly HashSet<string> _loggedSections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _sectionLock = new object();

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
            AnalyzeDisallowRun(reg, ref risk);

            // 2. Appinit_DLLs
            AnalyzeAppInit(reg, ref risk);

            // 3. IFEO и WOW6432 IFEO
            AnalyzeIfeo(reg, ref risk, ref isMalicious);

            // 4. Silent Process Exit
            AnalyzeSilentExit(reg, ref risk);

            // 5. System Policies
            AnalyzeSystemPolicies(reg, ref risk);

            // 6. Winlogon (Userinit, Shell)
            AnalyzeWinlogon(reg, ref risk);

            // 7. Tekt0nit (RMS)
            AnalyzeTektonit(reg, ref risk, ref isMalicious);

            // 8. Applocker
            AnalyzeAppLocker(reg, ref risk);

            // 9. Windows Defender
            AnalyzeDefenderExclusions(reg, ref risk);

            // 10. Autorun (Run ключи)
            AnalyzeAutorun(reg, ref risk, ref isMalicious);

            if (risk == 0) yield break; // Угрозы нет

            ScanObjectType objType = isMalicious || risk >= 3 ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            // Решение для объекта реестра
            yield return new ThreatDecision(reg, risk, objType);

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

        private void AnalyzeDisallowRun(RegistryThreatObject reg, ref int risk)
        {
            if (reg.KeyPath.Equals(MSData.GetInstance.queries["ExplorerPolicies"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.ValueName.Equals("DisallowRun", StringComparison.OrdinalIgnoreCase))
                {
                    risk += 3;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
                }
            }
            else if (reg.KeyPath.Equals(MSData.GetInstance.queries["ExplorerDisallowRun"], StringComparison.OrdinalIgnoreCase))
            {
                if (reg.NodeType == RegistryNodeType.Key)
                {
                    risk += 3;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.KeyPath);
                }
            }
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
                    if (reg.ValueName.Equals("GlobalFlag", StringComparison.OrdinalIgnoreCase) && reg.ValueData == "512") // 0x200
                    {
                        risk += 2;
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
                    risk += 3;
                    reg.ActionDelete = true;
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", reg.ValueName);
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
                    risk += 3;
                    isMalicious = true;
                    reg.ActionDelete = true;       // Если ветка, то удалит всю ветку
                    reg.ActionUnlockFirst = true; // Снимаем защиту вируса
                    AppConfig.GetInstance.LL.LogSuccessMessage("_MarkedForRemoval", GetKeyName(reg.KeyPath));
                    return;
                }

                if (reg.NodeType == RegistryNodeType.Value)
                {
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
            if (reg.NodeType == RegistryNodeType.Value &&
                (reg.KeyPath.Contains(MSData.GetInstance.queries["WDExclusionsLocal"]) ||
                 reg.KeyPath.Contains(MSData.GetInstance.queries["WDExclusionsPolicies"])))
            {
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
                    if (reg.KeyPath.Contains(MSData.GetInstance.queries["WDExclusionsLocal"]))
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
        }

        private void AnalyzeAutorun(RegistryThreatObject reg, ref int risk, ref bool isMalicious)
        {
            if (reg.NodeType == RegistryNodeType.Value &&
                (reg.KeyPath.EndsWith(MSData.GetInstance.queries["StartupRun"], StringComparison.OrdinalIgnoreCase) ||
                 reg.KeyPath.EndsWith(MSData.GetInstance.queries["Wow6432Node_StartupRun"], StringComparison.OrdinalIgnoreCase)))
            {
                string val = reg.ValueData;

                // 1. Эвристика по строке
                if (val.IndexOf("RealtekHD\\task", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
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
                        if (!reg.LinkedFile.IsValidSignature && reg.LinkedFile.FileSize >= reg.LinkedFile.MAX_FILE_SIZE && !reg.ActionDelete)
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
                    }
                }
            }
        }

        bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetKeyName(string keyPath)
        {
            return Path.GetFileName(keyPath) ?? keyPath;
        }
    }
}
