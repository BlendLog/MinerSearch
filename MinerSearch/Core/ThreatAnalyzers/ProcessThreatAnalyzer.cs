using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MSearch.Core.ThreatAnalyzers
{
    internal sealed class ProcessThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Process;

        readonly IFileContentAnalyzer _fileAnalyzer;

        public ProcessThreatAnalyzer(IFileContentAnalyzer fileAnalyzer)
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
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanProcesses");
                        _headerLogged = true;
                    }
                }
            }

            ProcessThreatObject proc = threat as ProcessThreatObject;
            if (proc == null) yield break;

            if (string.IsNullOrEmpty(proc.ProcessArgs))
            {
                LocalizedLogger.LogScanning(proc.ProcessName);
            }
            else
            {
                LocalizedLogger.LogScanning(proc.ProcessName, proc.ProcessArgs);
            }

            int riskLevel = 0;
            bool isValidProcess = false;

            if (!proc.FileProcess.IsValidSignature)
            {
                riskLevel += 1;
            }

            if (ProcessManager.IsTrustedProcess(MSData.GetInstance, proc.FileProcess.FileNameOriginal, isValidProcess))
            {
                yield break;
            }

            string fileDescription = proc.FileProcess.FileDescription;
            if (fileDescription != null)
            {
                if (fileDescription.Equals("svhost", StringComparison.OrdinalIgnoreCase))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    proc.FileProcess.IsSuspiciousPath = true;
                    riskLevel += 2;
                }
            }

            string originalFileName = proc.FileProcess.FileNameOriginal;
            if (originalFileName != null)
            {
                if (originalFileName.IndexOf(new StringBuilder("Spot").Append("ifySta").Append("rtupTas").Append("k.exe").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }
            }

            if (!File.Exists(proc.FileProcess.FilePath))
            {
                riskLevel += 1;
            }

            if (proc.ProcessName.IndexOf("helper", StringComparison.OrdinalIgnoreCase) >= 0 && !isValidProcess)
            {
                riskLevel += 1;
            }


            foreach (ProcessModule pMod in proc.ProcessModules)
            {
                proc.GPULibsCount += MSData.GetInstance._nvdlls.Count(name => pMod.ModuleName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (proc.GPULibsCount > 2)
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_GPULibsUsage", $"{proc.ProcessName}.exe, PID: {proc.ProcessId}");
                riskLevel += 1;
            }

            if (AppConfig.GetInstance.bootMode != BootMode.SafeMinimal)
            {

                if (proc.ProcessRemotePort != -1 && proc.ProcessRemotePort != 0)
                {
                    if (MSData.GetInstance._PortList.Contains(proc.ProcessRemotePort))
                    {
                        AppConfig.GetInstance.LL.LogWarnMessage("_BlacklistedPort", $"{proc.ProcessRemotePort} - {proc.ProcessName}");
                        riskLevel += 1;
                    }
                }
            }

            if (!string.IsNullOrEmpty(proc.ProcessArgs))
            {
                foreach (int port in MSData.GetInstance._PortList)
                {
                    if (proc.ProcessArgs.IndexOf($":{port}", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        riskLevel += 1;
                        AppConfig.GetInstance.LL.LogWarnMessage("_BlacklistedPortCMD", $"{port} : {proc.ProcessName}.exe");
                    }
                }

                if (MSData.GetInstance.badArgStrings.Any(s => proc.ProcessArgs.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    riskLevel += 3;
                    proc.IsBadArgsPatternPresent = true;
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_PresentInCmdArgs", proc.ProcessArgs);
                }


                if (proc.ProcessArgs.IndexOf("-systemcheck", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    riskLevel += 2;
                    AppConfig.GetInstance.LL.LogWarnMessage("_FakeSystemTask");

                    try
                    {
                        if (proc.FileProcess.FilePath.IndexOf("appdata", StringComparison.OrdinalIgnoreCase) >= 0 && proc.FileProcess.FilePath.IndexOf("windows", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            riskLevel += 1;
                            proc.FileProcess.IsSuspiciousPath = true;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                        yield break;
                    }

                }

                if ((proc.ProcessName.Equals(MSData.GetInstance.SysFileName[3], StringComparison.OrdinalIgnoreCase) && proc.ProcessArgs.IndexOf($"\\??\\{AppConfig.GetInstance.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) == -1))
                {
                    riskLevel += 3;
                    if (proc.ProcessArgs.IndexOf($"\\\\?\\{AppConfig.GetInstance.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        riskLevel--;
                    }
                    else
                    {
                        AppConfig.GetInstance.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {proc.ProcessId}");
                    }
                }
                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[4], StringComparison.OrdinalIgnoreCase) && (proc.ProcessArgs.IndexOf($"{MSData.GetInstance.SysFileName[4]}.exe -k dcomlaunch", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    foreach (ProcessModule pMod in proc.ProcessModules)
                    {
                        WinVerifyTrustResult pModSignature = WinTrust.GetInstance.VerifyEmbeddedSignature(pMod.FileName);
                        if (pModSignature != WinVerifyTrustResult.Success && pModSignature != WinVerifyTrustResult.Error)
                        {
                            AppConfig.GetInstance.LL.LogWarnMediumMessage("_ServiceDcomAbusing", pMod.FileName + $" | PID: {proc.ProcessId}");
                        }
                    }
                }

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[32], StringComparison.OrdinalIgnoreCase) && proc.ProcessArgs.IndexOf("#system32", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[31], StringComparison.OrdinalIgnoreCase) && (DateTime.Now - proc.StartTime).TotalSeconds >= 60)
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }

                if (proc.ProcessName.Equals("explorer", StringComparison.OrdinalIgnoreCase) && proc.ProcessArgs.IndexOf($@"{AppConfig.GetInstance.drive_letter}:\Windows\Explorer.exe", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    riskLevel++;
                }
            }

            string fullPath = proc.FileProcess.FilePath;
            string appData = FileSystemManager.NormalizeExtendedPath(Environment.GetEnvironmentVariable("AppData")) ?? "";
            if (!isValidProcess && fullPath.StartsWith(appData, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(Path.GetExtension(fullPath)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                proc.FileProcess.IsSuspiciousPath = true;
                riskLevel += 2;
            }

            if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[28], StringComparison.OrdinalIgnoreCase) && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1)
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                proc.FileProcess.IsSuspiciousPath = true;
                riskLevel += 2;
            }

            for (int i = 0; i < MSData.GetInstance.SysFileName.Length; i++)
            {

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[i], StringComparison.OrdinalIgnoreCase))
                {

                    if (fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\system32\\wbem", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\syswow64", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\winsxs\\amd64", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\winsxs\\x86", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\microsoft.net\\framework64", StringComparison.OrdinalIgnoreCase) == -1
                        && fullPath.IndexOf($"{AppConfig.GetInstance.drive_letter}:\\windows\\microsoft.net\\framework", StringComparison.OrdinalIgnoreCase) == -1)
                    {

                        AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        proc.FileProcess.IsSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    if (proc.FileProcess.FileSize >= MSData.GetInstance.constantFileSize[i] * 3 && !isValidProcess)
                    {
                        AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousFileSize", FileChecker.GetFileSize(proc.FileProcess.FileSize));
                        riskLevel += 1;
                    }

                }

            }

            if (proc.FileProcess.FileSize >= proc.FileProcess.MAX_FILE_SIZE)
            {
                riskLevel += 1;
                proc.FileProcess.IsFileTooLarge = true;
            }

            try
            {
                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[17], StringComparison.OrdinalIgnoreCase) && (proc.IsDotnetProcess || proc.CpuTime > new TimeSpan(0, 0, 30)))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {proc.ProcessId}");
                    riskLevel += 3;
                }
            }
            catch (InvalidOperationException ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                yield break;
            }

            if (proc.ProcessName.Equals("rundll", StringComparison.OrdinalIgnoreCase) || proc.ProcessName.Equals("system", StringComparison.OrdinalIgnoreCase) || proc.ProcessName.Equals("wi?ns?er?v".Replace("?", ""), StringComparison.OrdinalIgnoreCase))
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");

                proc.FileProcess.IsSuspiciousPath = true;
                riskLevel += 3;
            }

            if (proc.ProcessName.Equals("explorer", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    int ParentProcessId = ProcessManager.GetParentProcessId(proc.ProcessId);
                    if (ParentProcessId != 0)
                    {
                        Process ParentProcess = Process.GetProcessById(ParentProcessId);
                        if (ParentProcess.ProcessName.Equals("explorer", StringComparison.OrdinalIgnoreCase))
                        {
                            AppConfig.GetInstance.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {proc.ProcessId}");
                            riskLevel += 3;
                        }
                    }
                }
                catch (Win32Exception w32e)
                {
#if DEBUG
                            Console.WriteLine($"[DBG Scan()] {w32e.Message}");
#endif
                }
                catch (ArgumentException ae)
                {
#if DEBUG
                            Console.WriteLine($"[DBG Scan()] {ae.Message}");
#endif
                }

                if (proc.HasSystemPrivilege && !AppConfig.GetInstance.RunAsSystem)
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {proc.ProcessId}");
                    riskLevel += 3;
                }

                if (proc.IsDotnetProcess)
                {
                    riskLevel += 1;
                }
            }

            if (proc.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase))
            {
                if (proc.HasSystemPrivilege && !OSExtensions.IsWinPEEnv())
                {
                    riskLevel += 2;
                }

                if (proc.CpuTime > new TimeSpan(0, 1, 0) || (proc.UsedMemorySize / (1024 * 1024) >= 2048))
                {
                    riskLevel += 2;
                }

                if (proc.IsDotnetProcess)
                {
                    riskLevel += 1;
                }
            }

            // --- Маркирование файла: ОДНО действие (удаление ИЛИ карантин) ---
            bool isKnownMalicious = IsKnownMaliciousFile(proc.FileProcess.FilePath);

            // Проверка по obfStr2 (главный приоритет — удаление)
            if (isKnownMalicious)
            {
                riskLevel += 3;
                proc.FileProcess.AnalysisResult = FileContentAnalysisResult.Malicious();
                proc.FileProcess.ShouldDisableExecute = true;
                proc.FileProcess.ShouldDeleteFile = true;
            }
            // Блок анализа содержимого файла (CPU/Memory эвристика)
            else if (proc.CpuTime > new TimeSpan(0, 1, 0) || (proc.UsedMemorySize / (1024 * 1024) >= 2048))
            {
                if (File.Exists(proc.FileProcess.FilePath))
                {
                    var analyzeResult = _fileAnalyzer.Analyze(proc.FileProcess, false);

                    if (analyzeResult.IsMalicious)
                    {
                        riskLevel += 3;
                        proc.FileProcess.AnalysisResult = FileContentAnalysisResult.Malicious();
                        proc.FileProcess.ShouldMoveFileToQuarantine = true;
                    }
                }
                else
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", proc.FileProcess.FilePath);
                }
            }

            if (proc.FileProcess.FilePath.Contains(@":\Windows\Microsoft.NET\Framework"))
            {
                try
                {
                    int processParentId = ProcessManager.GetParentProcessId(proc.ProcessId);
                    if (processParentId == 0)
                    {
                        riskLevel += 1;
                    }
                    else
                    {
                        Process parentProcess = Process.GetProcessById(processParentId);
                        if (parentProcess != null)
                        {
                            if (parentProcess.ProcessName.Equals(MSData.GetInstance.SysFileName[9], StringComparison.OrdinalIgnoreCase))
                            {
                                riskLevel -= 1;
                            }

                            try
                            {
                                _ = parentProcess.MainModule.FileName;
                                riskLevel += 1;
                            }
                            catch (Win32Exception) { }
                        }
                    }

                }
                catch (Exception)
                {
                    riskLevel += 1;
                }


                if (proc.CpuTime <= new TimeSpan(0, 0, 15) && (proc.UsedMemorySize / (1024 * 1024) <= 100))
                {
                    riskLevel += 3;
                }
            }

            proc.IsProcessHollowed = ProcessManager.IsProcessHollowed(proc.ProcessId);
            if (isValidProcess && proc.IsProcessHollowed)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {proc.ProcessId}");
                riskLevel += 3;
            }

            proc.ShouldSuspend = riskLevel >= 3;

            if (proc.FileProcess.IsSuspiciousPath && riskLevel >= 3)
            {
                // Карантин только если файл НЕ в obfStr2
                if (!proc.FileProcess.ShouldDeleteFile)
                {
                    proc.FileProcess.ShouldMoveFileToQuarantine = true;
                }
            }

            ScanObjectType scanType = proc.ShouldSuspend ? ScanObjectType.Malware : ScanObjectType.Unknown;

            if (scanType == ScanObjectType.Malware)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ProcessFound", ProcessManager.GetLocalizedRiskLevel(riskLevel));

                if (proc.FileProcess.ShouldDeleteFile)
                {
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessFile_MarkedToDelete");
                }
                if (proc.FileProcess.ShouldMoveFileToQuarantine)
                {
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessFile_MarkedToMoveQuarantine");
                }
            }

            // Решение для процесса
            yield return new ThreatDecision(threat, riskLevel, scanType);

            // Решение для связанного файла (если есть флаги действия)
            if (proc.FileProcess.ShouldDeleteFile ||
                proc.FileProcess.ShouldMoveFileToQuarantine ||
                proc.FileProcess.ShouldDisableExecute)
            {
                yield return new ThreatDecision(proc.FileProcess, riskLevel, scanType);
            }
        }

        bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
