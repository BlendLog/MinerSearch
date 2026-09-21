using DBase;
using Microsoft.Win32;
using MSearch.Core.Managers;
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

        private static readonly Regex EncodedCommandRegex = new Regex(
            @"(?<![a-z0-9])-(e|ec|enc(odedcommand)?)(?![a-z0-9])",
            RegexOptions.Compiled);

        private static readonly Regex TempDirRegex = new Regex(
            @"(^|\\)temp(\\|$)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex WsclInTempRegex = new Regex(
            @"\\temp\\\{[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}\\wscl\.exe",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

            Logger.WriteLog("------------", ConsoleColor.White, true, true);
            AppConfig.GetInstance.LL.LogMessage("[.]", "_ServiceName", svc.ServiceName, ConsoleColor.White);
            AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_Service", svc.ServicePathWithArgs, ConsoleColor.White);
            AppConfig.GetInstance.LL.LogMessage("[.]", "_State", svc.Status.ToString(), ConsoleColor.White);



            if (svc.LinkedServiceFile != null && svc.LinkedServiceFile.FileSize > 0)
            {
                AppConfig.GetInstance.LL.LogMessage("[.]", "_FileSize", FileChecker.GetFileSize(svc.LinkedServiceFile.FileSize), ConsoleColor.White);
            }

            FileChecker.LogUnsignedSha1(svc.LinkedServiceFile);

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
                yield break;
            }

            int risk = 0;
            bool isMalicious = false;
            bool forceQuarantineService = false;

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

            // 4. SDDL blocking pattern — реальный SACL-блок + подозрительный путь
            bool hasSddlBlocking = svc.SCMUnavailable &&
                                   (normalized.Contains("cmd.exe /c start") ||
                                    normalized.StartsWith("\\\\.\\c:\\programdata", StringComparison.OrdinalIgnoreCase));

            // Encoded PowerShell launcher
            bool hasEncodedCommand = IsEncodedCommandLaunch(normalized);

            // Random-named extensionless binary in a user-writable directory
            bool hasSuspiciousImagePath = IsSuspiciousServiceImagePath(svc);

            if (hasSddlBlocking)
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_ServiceSCMUnavailable", svc.ServiceName);
            }

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

            if (hasEncodedCommand)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ServiceEncodedCommand", $"{svc.ServiceName} {svc.ServicePath}");
                risk += 3;
                isMalicious = true;
            }

            if (hasSuspiciousImagePath)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ServiceSuspiciousImagePath", $"{svc.ServiceName} {svc.ServicePath}");
                if (svc.LinkedServiceFile == null)
                {
                    risk += 2;
                    forceQuarantineService = true;
                }
                else
                {
                    risk += 3;
                    isMalicious = true;
                    svc.LinkedServiceFile.ShouldDisableExecute = true;
                    svc.LinkedServiceFile.ShouldDeleteFile = true;
                }
            }

            if (hasSddlBlocking)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Found", $"{svc.ServiceName} {svc.ServicePath}");
                risk += 3;
                isMalicious = true;
            }

            if (hasSddlBlocking && svc.HasInSafeMode)
            {
                svc.ShouldRemoveFromSafeMode = true;
            }

            bool hasMaliciousSddl = false;
            try
            {
                string currentSddl = ServiceHelper.GetServiceSddl(svc.ServiceName);
                if (!string.IsNullOrEmpty(currentSddl))
                {
                    Regex sddlDenyRegex = new Regex(@"\(D;;[^()]*;;;(IU|SU|BA|WD)\)", RegexOptions.IgnoreCase);
                    hasMaliciousSddl = sddlDenyRegex.IsMatch(currentSddl.Replace(" ", "").ToUpperInvariant());
                }
            }
            catch { }

            if (hasMaliciousSddl)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Found", $"{svc.ServiceName} {svc.ServicePath}");
                risk += 3;
                isMalicious = true;
            }

            // 4. Анализ файла сервиса (подпись, имя, путь, сигнатуры)
            if (svc.LinkedServiceFile != null)
            {
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
                            var dllSignature = WinTrust.GetInstance.VerifyEmbeddedSignature(dll, true);
                            if (dllSignature != WinVerifyTrustResult.Success)
                            {
                                AppConfig.GetInstance.LL.LogCautionMessage("_Found",
                                    AppConfig.GetInstance.LL.GetLocalizedString("_ValidServiceDLLHijacking")
                                        .Replace("#SERVICENAME#", svc.ServiceName)
                                        .Replace("#DLLNAME#", dllName));

                                svc.LinkedServiceDll = CreateFileObject(dll, dllSignature);
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
                yield break;
            }

            ScanObjectType objType = isMalicious ? ScanObjectType.Malware : ScanObjectType.Suspicious;

            svc.ShouldDisableService = true;

            if (svc.HasUnsignedServiceDll || forceQuarantineService)
            {
                svc.ShouldQuarantineService = true;
            }
            else if (isMalicious)
            {
                svc.ShouldStopService = true;
                svc.ShouldDeleteService = true;
                svc.ShouldResetSddl = svc.SCMUnavailable || hasMaliciousSddl;
            }

            // LinkedServiceFile уже залогирован в шапке службы; здесь остаётся ServiceDll,
            // который становится известен только в ходе анализа
            FileChecker.LogUnsignedSha1(svc.LinkedServiceDll);
            yield return new ThreatDecision(svc, risk, objType);

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

        private static bool IsEncodedCommandLaunch(string normalizedServicePathWithArgs)
        {
            if (string.IsNullOrEmpty(normalizedServicePathWithArgs)) return false;

            bool hasShell = normalizedServicePathWithArgs.Contains("powershell") ||
                            normalizedServicePathWithArgs.Contains("pwsh");
            if (!hasShell) return false;

            return EncodedCommandRegex.IsMatch(normalizedServicePathWithArgs);
        }

        private static bool IsSuspiciousServiceImagePath(ServiceThreatObject svc)
        {
            if (CheckSuspiciousImagePathCandidate(svc, svc.ServicePath)) return true;

            string raw = GetRawImagePathToken(svc);
            if (!string.IsNullOrEmpty(raw) &&
                !string.Equals(raw, svc.ServicePath, StringComparison.OrdinalIgnoreCase))
            {
                return CheckSuspiciousImagePathCandidate(svc, raw);
            }

            return false;
        }

        private static bool CheckSuspiciousImagePathCandidate(ServiceThreatObject svc, string candidate)
        {
            if (string.IsNullOrEmpty(candidate)) return false;

            candidate = candidate.Trim().Trim('"').Replace('/', '\\');
            if (IsNtOrRawDrivePath(candidate))
                candidate = StripNtPrefix(candidate);

            if (WsclInTempRegex.IsMatch(candidate))
                return true;

            if (!TempDirRegex.IsMatch(candidate))
                return false;

            string fileStem = Path.GetFileNameWithoutExtension(candidate);
            if (!string.Equals(fileStem, svc.ServiceName, StringComparison.OrdinalIgnoreCase))
                return false;

            return !HasValidSignature(candidate);
        }

        private static bool HasValidSignature(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                    return false;

                var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(path);
                return trust == WinVerifyTrustResult.Success ||
                       trust == WinVerifyTrustResult.SubjectCertExpired;
            }
            catch
            {
                return false;
            }
        }

        private static string StripNtPrefix(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) return path.Substring(4);
            if (path.StartsWith(@"\??\", StringComparison.Ordinal)) return path.Substring(4);
            if (path.StartsWith(@"\\.\", StringComparison.Ordinal)) return path.Substring(4);
            if (path.StartsWith(@"\\", StringComparison.Ordinal)) return path.Substring(2);

            return path;
        }

        private static bool IsNtOrRawDrivePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            int i = 0;
            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) i = 4;
            else if (path.StartsWith(@"\??\", StringComparison.Ordinal)) i = 4;
            else if (path.StartsWith(@"\\.\", StringComparison.Ordinal)) i = 4;
            else if (path.StartsWith(@"\\", StringComparison.Ordinal)) i = 2;

            return path.Length > i + 1 && char.IsLetter(path[i]) && path[i + 1] == ':';
        }

        private static string GetRawImagePathToken(ServiceThreatObject svc)
        {
            string raw = svc.ServicePathWithArgs;
            if (string.IsNullOrEmpty(raw)) return null;

            raw = Environment.ExpandEnvironmentVariables(raw.Trim());

            if (raw.StartsWith("\""))
            {
                int closingQuote = raw.IndexOf('"', 1);
                if (closingQuote > 0)
                    return raw.Substring(1, closingQuote - 1);
            }

            int space = raw.IndexOf(' ');
            return space > 0 ? raw.Substring(0, space) : raw;
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

                        svc.HasUnsignedServiceDll = true;
                        svc.LinkedServiceDll = CreateFileObject(serviceDll, dllSignature);
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

                                if (ThreatManager.IsJohnPatternsFound())
                                {
                                    svc.ShouldStopService = true;
                                    svc.ShouldRestoreServiceDll = true;
                                    AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceMarkedForRestore");

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

        private FileThreatObject CreateFileObject(string path, WinVerifyTrustResult? trustResult = null)
        {
            try
            {
                if (!File.Exists(path)) return null;

                var trust = trustResult ?? WinTrust.GetInstance.VerifyEmbeddedSignature(path, true);
                var fileInfo = new FileInfo(path);
                var versionInfo = FileVersionInfo.GetVersionInfo(path);
                string originalName = versionInfo.OriginalFilename ?? string.Empty;
                string description = versionInfo.FileDescription ?? string.Empty;
                return new FileThreatObject(path, Path.GetFileName(path), fileInfo.Length, originalName, description, FileChecker.CalculateSHA1(path), trust);
            }
            catch
            {
                return null;
            }
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
