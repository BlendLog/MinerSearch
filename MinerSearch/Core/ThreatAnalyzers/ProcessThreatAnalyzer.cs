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

            FileChecker.LogUnsignedSha1(proc.FileProcess);

            int riskLevel = 0;

            if (!proc.FileProcess.IsValidSignature)
            {
                riskLevel += 1;
            }

            if (ProcessManager.IsTrustedProcess(MSData.GetInstance, proc.FileProcess.FileNameOriginal, proc.FileProcess.IsValidSignature))
            {
                yield break;
            }

            string fileDescription = proc.FileProcess.FileDescription;
            if (fileDescription != null)
            {
                if (MSData.GetInstance.markerSets[MSKeys.FakeDescriptionMarkers].Any(m => fileDescription.Equals(m, StringComparison.OrdinalIgnoreCase)))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    proc.FileProcess.IsSuspiciousPath = true;
                    riskLevel += 2;
                }
            }

            string originalFileName = proc.FileProcess.FileNameOriginal;
            if (originalFileName != null)
            {
                if (MSData.ContainsAnyMarker(originalFileName, MSData.GetInstance.markerSets[MSKeys.FakeOriginalNameMarkers]))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }
            }

            if (!File.Exists(proc.FileProcess.FilePath))
            {
                riskLevel += 1;
            }

            if (MSData.ContainsAnyMarker(proc.ProcessName, MSData.GetInstance.markerSets[MSKeys.HelperMarkers]) && !proc.FileProcess.IsValidSignature)
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

                string mshtaProcessName = MSData.GetInstance.SysFileName[39];

                bool isMshtaProcess =
                    proc.ProcessName.Equals(mshtaProcessName, StringComparison.OrdinalIgnoreCase) ||
                    (proc.FileProcess.FileName != null && proc.FileProcess.FileName.Equals(mshtaProcessName + ".exe", StringComparison.OrdinalIgnoreCase));

                if (isMshtaProcess &&
                    (MSData.ContainsAnyMarker(proc.ProcessArgs, MSData.GetInstance.markerSets[MSKeys.ScriptMarkers]) ||
                     MSData.ContainsAnyMarker(proc.ProcessArgs, MSData.GetInstance.markerSets[MSKeys.UrlMarkers]) ||
                     MSData.ContainsAnyMarker(proc.ProcessArgs, MSData.GetInstance.markerSets[MSKeys.HtaMarkers])))
                {
                    riskLevel += 3;
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProcessMshta", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                }


                if (MSData.ContainsAnyMarker(proc.ProcessArgs, MSData.GetInstance.markerSets[MSKeys.FakeSystemCheckMarkers]))
                {
                    riskLevel += 2;
                    AppConfig.GetInstance.LL.LogWarnMessage("_FakeSystemTask");

                    try
                    {
                        if (MSData.ContainsAllMarkers(proc.FileProcess.FilePath, MSData.GetInstance.markerSets[MSKeys.FakeTaskLocationMarkers]))
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


                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[4], StringComparison.OrdinalIgnoreCase) && (proc.ProcessArgs.IndexOf(MSData.GetInstance.SysFileName[4] + MSData.GetInstance.consts[MSKeys.DcomLaunchArgs], StringComparison.OrdinalIgnoreCase) >= 0))
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

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[32], StringComparison.OrdinalIgnoreCase) && MSData.ContainsAnyMarker(proc.ProcessArgs, MSData.GetInstance.markerSets[MSKeys.RegasmArgMarkers]))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[31], StringComparison.OrdinalIgnoreCase) && (DateTime.Now - proc.StartTime).TotalSeconds >= 60)
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{proc.FileProcess.FilePath} PID: {proc.ProcessId}");
                    riskLevel += 3;
                }

                if (proc.ProcessName.Equals("explorer", StringComparison.OrdinalIgnoreCase) && proc.ProcessArgs.IndexOf(AppConfig.GetInstance.drive_letter + MSData.GetInstance.consts[MSKeys.ExplorerExePath], StringComparison.OrdinalIgnoreCase) == -1)
                {
                    riskLevel++;
                }
            }

            string fullPath = proc.FileProcess.FilePath;
            string appData = FileSystemManager.NormalizeExtendedPath(Environment.GetEnvironmentVariable("AppData")) ?? "";
            if (!proc.FileProcess.IsValidSignature && fullPath.StartsWith(appData, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(Path.GetExtension(fullPath)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                proc.FileProcess.IsSuspiciousPath = true;
                riskLevel += 2;
            }

            if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[28], StringComparison.OrdinalIgnoreCase) && fullPath.IndexOf(AppConfig.GetInstance.drive_letter + MSData.GetInstance.systemPathFragments[0], StringComparison.OrdinalIgnoreCase) == -1)
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                proc.FileProcess.IsSuspiciousPath = true;
                riskLevel += 2;
            }

            for (int i = 0; i < MSData.GetInstance.SysFileName.Length; i++)
            {

                if (proc.ProcessName.Equals(MSData.GetInstance.SysFileName[i], StringComparison.OrdinalIgnoreCase))
                {

                    bool inSystemPath = false;
                    foreach (string pathFragment in MSData.GetInstance.systemPathFragments)
                    {
                        if (fullPath.IndexOf(AppConfig.GetInstance.drive_letter + pathFragment, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            inSystemPath = true;
                            break;
                        }
                    }

                    if (!inSystemPath)
                    {
                        AppConfig.GetInstance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        proc.FileProcess.IsSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    if (proc.FileProcess.FileSize >= MSData.GetInstance.constantFileSize[i] * 3 && !proc.FileProcess.IsValidSignature)
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

            if (MSData.GetInstance.markerSets[MSKeys.FakeProcessNameMarkers].Any(m => proc.ProcessName.Equals(m, StringComparison.OrdinalIgnoreCase)))
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
            if (proc.IsProcessHollowed)
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

            // Создаём ThreatDecision только для подозрительных процессов
            if (riskLevel >= 3)
            {
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
        }

        bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.IsKnownMaliciousPath(filePath);
        }
    }
}
