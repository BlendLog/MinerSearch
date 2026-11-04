using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MSearch.Core.Scanners
{
    public class ProcessScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan(CancellationToken token)
        {
            string myExePath = FileSystemManager.NormalizeExtendedPath(AppConfig.GetInstance.ExecutablePath);

            var results = new List<IThreatObject>();

            AppConfig.GetInstance.LL.LogHeadMessage("_PreparingToScan");
            List<Process> procs = ProcessManager.SafeGetProcesses();

            var portMap = AppConfig.GetInstance.bootMode != BootMode.SafeMinimal
                ? ProcessManager.BuildPortMap()
                : null;
            bool processScanWarningReported = false;

            Action<Exception> reportProcessScanError = delegate(Exception error)
            {
                if (processScanWarningReported) return;
                processScanWarningReported = true;

                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorScanProcesses", error);

                // В GUI DialogDispatcher идёт через SynchronizationContext.Send:
                // модальное окно останавливает поток скана и не даёт нажать Stop.
                if (LaunchOptions.GetInstance.UseConsoleUi && !LaunchOptions.GetInstance.silent)
                {
                    DialogDispatcher.Show(
                        AppConfig.GetInstance.LL.GetLocalizedString("_ErrorScanProcesses"),
                        AppConfig.GetInstance._title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            };

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    if (p.HasExited) continue;

                    string filePath = string.Empty;

                    try
                    {
                        filePath = FileSystemManager.NormalizeExtendedPath(Path.GetFullPath(p.MainModule.FileName));
                    }
                    catch { }

                    if (filePath.Equals(myExePath, StringComparison.OrdinalIgnoreCase)) continue;

                    string processName = p.ProcessName;

                    ScanActivity.Report(string.IsNullOrEmpty(filePath) ? processName : filePath);

                    int processId = p.Id;
                    long memorySize = p.PrivateMemorySize64;
                    TimeSpan cpuTime = p.TotalProcessorTime;
                    DateTime startDateTime = p.StartTime;
                    string args = ProcessManager.GetProcessCommandLine(p);

                    string hash = string.Empty;
                    long fileSize = 0;
                    WinVerifyTrustResult trustResult = WinVerifyTrustResult.Error;
                    string originalFileName = null;
                    string fileDescription = null;

                    if (File.Exists(filePath))
                    {
                        trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(filePath);

                        try { fileSize = new FileInfo(filePath).Length; } catch { }

                        try
                        {
                            originalFileName = FileVersionInfo.GetVersionInfo(filePath).OriginalFilename;
                            fileDescription = FileVersionInfo.GetVersionInfo(filePath).FileDescription;
                        }
                        catch { }

                        hash = trustResult != WinVerifyTrustResult.Success ? FileChecker.CalculateSHA1(filePath) : string.Empty;
                    }
                    else
                    {
                        filePath = string.Empty;
                    }

                    var fileThreat = new FileThreatObject(filePath, Path.GetFileName(filePath), fileSize, originalFileName, fileDescription, hash, trustResult);

                    ProcessModuleCollection moduleCollection = p.Modules;

                    bool isDotnet = ProcessManager.IsDotnetProcess(p);

                    int remotePort = -1;
                    if (portMap != null && portMap.TryGetValue(p.Id, out int portByPid))
                    {
                        remotePort = portByPid;
                    }

                    bool hasSystemPrivilege = ProcessManager.IsSystemProcess(processId);

                    var processThreat = new ProcessThreatObject(processId, processName, args, cpuTime, startDateTime, memorySize, remotePort, moduleCollection, isDotnet, hasSystemPrivilege, fileThreat);

                    results.Add(processThreat);
                }
                catch (ArgumentException)
                {
                    continue;
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
                catch (Win32Exception w32e)
                {
                    if (w32e.NativeErrorCode == 299 || w32e.HResult == unchecked((int)0x8007012B))
                        reportProcessScanError(w32e);
                    continue;
                }
                catch (Exception ex)
                {
                    // Ошибка чтения одного процесса не должна завершать приложение.
                    // Пропускаем этот процесс и продолжаем проверку остальных.
                    reportProcessScanError(ex);
                    continue;
                }
            }

            procs.Clear();
            return results;
        }
    }
}
