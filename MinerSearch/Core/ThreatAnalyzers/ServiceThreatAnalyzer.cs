using DBase;
using Microsoft.Win32;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Win32Wrapper;

namespace MSearch.Core.ThreatAnalyzers
{
    public sealed class ServiceThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Service;

        private readonly IFileContentAnalyzer _fileAnalyzer;

        public ServiceThreatAnalyzer(IFileContentAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
        }

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanServices");
                        _headerLogged = true;
                    }
                }
            }

            var svc = threat as ServiceThreatObject;
            if (svc == null) yield break;

            AppConfig.GetInstance.LL.LogMessage("[.]", "_ServiceName", svc.ServiceName, ConsoleColor.White);
            AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_Service", svc.ServicePathWithArgs, ConsoleColor.White);
            AppConfig.GetInstance.LL.LogMessage("[.]", "_State", svc.Status.ToString(), ConsoleColor.White);

            // Размер файла
            if (svc.LinkedServiceFile != null && svc.LinkedServiceFile.FileSize > 0)
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_FileSize", FileChecker.GetFileSize(svc.LinkedServiceFile.FileSize), ConsoleColor.White);
            }

            // Whitelist по имени
            string[] specialScan = { "TermService" };
            foreach (string name in specialScan)
            {
                if (svc.ServiceName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    var decision = CheckTermServiceSpecial(svc);
                    if (decision != null)
                        yield return decision;
                    yield break;
                }
            }

            // Если уже отключён и подписан — пропускаем
            if (svc.StartMode == NativeServiceController.ServiceStartMode.Disabled &&
                svc.LinkedServiceFile != null && svc.LinkedServiceFile.IsValidSignature)
            {
                Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                Logger.WriteLog("------------", ConsoleColor.White, true, true);
                yield break;
            }

            int risk = 0;
            bool isMalicious = false;

            string servicePathWithArgs = svc.ServicePathWithArgs;
            string normalized = servicePathWithArgs.ToLowerInvariant();
            normalized = normalized.Replace("^|", "|").Replace("\\\"", "\"").Replace("'", "");

            // 1. Download+Exec pattern (irm + iex)
            bool hasDownloadExec =
                (normalized.Contains("iex") || normalized.Contains("invoke-expression")) &&
                (normalized.Contains("irm") || normalized.Contains("invoke-restmethod")) &&
                Regex.IsMatch(normalized, @"https?://[^\s""]+");

            // 2. Fileless persistence
            bool hasFilelessPersistence = normalized.Contains("[reflection.assembly]::") ||
                                          normalized.Contains("[runtime.interopservices.marshal]::") ||
                                          normalized.Contains("[reflection.emit") ||
                                          normalized.Contains("[microsoft.win32.registry]::");

            // 3. Malicious pattern
            bool hasMaliciousPattern = normalized.Contains("e=access&y=guest&h=");

            if (hasDownloadExec || hasFilelessPersistence)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Found", $"{svc.ServiceName} {svc.ServicePath}");
                risk += 3;
                isMalicious = true;
            }

            if (hasMaliciousPattern)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Found", $"{svc.ServiceName} {svc.ServicePath}");
                risk += 3;
                isMalicious = true;
            }

            // 4. Анализ файла сервиса (подпись, имя, путь, сигнатуры)
            if (svc.LinkedServiceFile != null)
            {
                // displayProgress = false — чтобы не спамить "Анализ: svchost.exe" для каждой службы
                var fileResult = _fileAnalyzer.Analyze(svc.LinkedServiceFile, false);

                if (fileResult.IsMalicious)
                {
                    risk += 3;
                    isMalicious = true;
                    svc.LinkedServiceFile.AnalysisResult = FileContentAnalysisResult.Malicious();
                    MarkFileForAction(svc.LinkedServiceFile);
                }
                else if (!svc.LinkedServiceFile.IsValidSignature &&
                         svc.LinkedServiceFile.TrustResult != WinVerifyTrustResult.Error)
                {
                    // Неподписанный файл — дополнительная эвристика
                    Regex nameRegex = new Regex(@"^[a-zA-Z]{8}$");
                    Regex pathRegex = new Regex(@"^(\\\\\?\\)?[a-fA-F]:\\ProgramData\\[a-zA-Z]{12}\\[a-zA-Z]{12}\.exe$");

                    bool suspiciousNameAndPath = nameRegex.IsMatch(svc.ServiceName) && pathRegex.IsMatch(svc.ServicePath);
                    bool tooLarge = svc.LinkedServiceFile.FileSize >= svc.LinkedServiceFile.MAX_FILE_SIZE;

                    if (suspiciousNameAndPath || tooLarge)
                    {
                        risk += 3;
                        isMalicious = true;
                        svc.LinkedServiceFile.AnalysisResult = FileContentAnalysisResult.Malicious();
                        MarkFileForAction(svc.LinkedServiceFile);
                    }
                }
            }

            // 5. DLL Hijacking — sideloadable DLLs без подписи
            if (!string.IsNullOrEmpty(svc.ServicePath) && File.Exists(svc.ServicePath))
            {
                string serviceDir = Path.GetDirectoryName(svc.ServicePath);
                if (Directory.Exists(serviceDir))
                {
                    foreach (string dll in Directory.EnumerateFiles(serviceDir, "*.dll"))
                    {
                        string dllName = Path.GetFileName(dll);
                        if (MSData.GetInstance.sideloadableDlls.Any(s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            var dllSignature = WinTrust.GetInstance.VerifyEmbeddedSignature(dll);
                            if (dllSignature != WinVerifyTrustResult.Success)
                            {
                                AppConfig.GetInstance.LL.LogCautionMessage("_Found",
                                    AppConfig.GetInstance.LL.GetLocalizedString("_ValidServiceDLLHijacking")
                                        .Replace("#SERVICENAME#", svc.ServiceName)
                                        .Replace("#DLLNAME#", dllName));

                                svc.LinkedServiceDll = CreateFileObject(dll);
                                if (svc.LinkedServiceDll != null)
                                {
                                    svc.LinkedServiceDll.ShouldDisableExecute = true;
                                    svc.LinkedServiceDll.ShouldMoveFileToQuarantine = true;
                                }

                                risk += 3;
                                isMalicious = true;
                                break;
                            }
                        }
                    }
                }
            }

            // 6. ServiceDll без подписи
            CheckServiceDll(svc, ref risk, ref isMalicious);

            if (risk == 0)
            {
                Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                Logger.WriteLog("------------", ConsoleColor.White, true, true);
                yield break;
            }

            ScanObjectType objType = isMalicious ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            // Устанавливаем флаги действий
            svc.ShouldDisableService = true;
            if (isMalicious)
            {
                svc.ShouldStopService = true;
                svc.ShouldDeleteService = true;
            }

            // Решение для сервиса
            yield return new ThreatDecision(svc, risk, objType);

            // Решения для связанных файлов
            if (svc.LinkedServiceFile != null &&
                (svc.LinkedServiceFile.ShouldDeleteFile ||
                 svc.LinkedServiceFile.ShouldMoveFileToQuarantine ||
                 svc.LinkedServiceFile.ShouldDisableExecute))
            {
                yield return new ThreatDecision(svc.LinkedServiceFile, risk, objType);
            }

            if (svc.LinkedServiceDll != null &&
                (svc.LinkedServiceDll.ShouldDeleteFile ||
                 svc.LinkedServiceDll.ShouldMoveFileToQuarantine ||
                 svc.LinkedServiceDll.ShouldDisableExecute))
            {
                yield return new ThreatDecision(svc.LinkedServiceDll, risk, objType);
            }
        }

        private void CheckServiceDll(ServiceThreatObject svc, ref int risk, ref bool isMalicious)
        {
            string registryPath = new StringBuilder("SY").Append("ST").Append("EM").Append("\\C").Append("ur").Append("re").Append("nt").Append("Co").Append("nt").Append("ro").Append("lS").Append("et").Append("\\S").Append("er").Append("vi").Append("ce").Append("s").ToString();

            using (RegistryKey servicesKey = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (servicesKey == null) return;

                using (RegistryKey serviceKey = servicesKey.OpenSubKey(svc.ServiceName + @"\Parameters"))
                {
                    if (serviceKey == null) return;

                    object serviceDllValue = serviceKey.GetValue("ServiceDll");
                    if (serviceDllValue == null) return;

                    string serviceDll = Environment.ExpandEnvironmentVariables(serviceDllValue.ToString());
                    Logger.WriteLog($"[.] ServiceDll: {serviceDll}", ConsoleColor.White);

                    if (!File.Exists(serviceDll))
                    {
                        AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", serviceDll);
                        return;
                    }

                    var dllSignature = WinTrust.GetInstance.VerifyEmbeddedSignature(serviceDll, true);
                    if (dllSignature != WinVerifyTrustResult.Success &&
                        dllSignature != WinVerifyTrustResult.SubjectCertExpired &&
                        dllSignature != WinVerifyTrustResult.Error)
                    {
                        AppConfig.GetInstance.LL.LogCautionMessage("_Found", $"{svc.ServiceName} {serviceDll}");

                        svc.LinkedServiceDll = CreateFileObject(serviceDll);
                        if (svc.LinkedServiceDll != null)
                        {
                            svc.LinkedServiceDll.ShouldDisableExecute = true;
                            svc.LinkedServiceDll.ShouldMoveFileToQuarantine = true;
                        }

                        risk += 3;
                        isMalicious = true;
                    }
                }
            }
        }

        private ThreatDecision CheckTermServiceSpecial(ServiceThreatObject svc)
        {
            if (MSData.GetInstance.queries == null) return null;

            if (!MSData.GetInstance.queries.ContainsKey("TermServiceParameters") || !MSData.GetInstance.queries.ContainsKey("TermsrvDll"))
                return null;

            string registryPath = MSData.GetInstance.queries["TermServiceParameters"];
            string desiredValue = MSData.GetInstance.queries["TermsrvDll"];
            string paramName = "ServiceDll";

            try
            {
                using (var regkey = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    if (regkey != null)
                    {
                        string currentValue = (string)regkey.GetValue(paramName);
                        if (currentValue != null)
                        {
                            string expandedDesired = Environment.ExpandEnvironmentVariables(desiredValue);
                            if (!string.Equals(currentValue, expandedDesired, StringComparison.OrdinalIgnoreCase))
                            {
                                AppConfig.GetInstance.LL.LogWarnMessage("_TermServiceInvalidPath", currentValue);

                                bool isInfectedService = false;
                                if (!string.IsNullOrEmpty(svc.ServicePathWithArgs))
                                {
                                    foreach (string pattern in MSData.GetInstance.JohnPatterns)
                                    {
                                        if (svc.ServicePathWithArgs.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                                        {
                                            isInfectedService = true;
                                            break;
                                        }
                                    }
                                }

                                if (isInfectedService)
                                {
                                    svc.ShouldStopService = true;
                                    svc.ShouldRestoreService = true;
                                    return new ThreatDecision(svc, 3, ScanObjectType.Infected);
                                }
                                else
                                {
                                    return new ThreatDecision(svc, 1, ScanObjectType.Suspicious);
                                }
                            }
                        }
                    }
                    else
                    {
                        AppConfig.GetInstance.LL.LogWarnMediumMessage("_ServiceNotInstalled", "TermService");
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }

            return null;
        }

        private FileThreatObject CreateFileObject(string path)
        {
            if (!File.Exists(path)) return null;
            var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(path, true);
            var fileInfo = new FileInfo(path);
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            string originalName = versionInfo.OriginalFilename ?? string.Empty;
            string description = versionInfo.FileDescription ?? string.Empty;
            return new FileThreatObject(path, Path.GetFileName(path), fileInfo.Length, originalName, description, FileChecker.CalculateSHA1(path), trust);
        }

        private void MarkFileForAction(FileThreatObject file)
        {
            if (file == null) return;

            if (IsKnownMaliciousFile(file.FilePath))
            {
                file.ShouldDeleteFile = true;
            }
            else if (!file.IsValidSignature)
            {
                file.ShouldMoveFileToQuarantine = true;
            }
        }

        private bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
