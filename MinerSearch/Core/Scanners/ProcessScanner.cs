using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MSearch.Core.Scanners
{
    public class ProcessScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            string myExePath = FileSystemManager.NormalizeExtendedPath(AppConfig.GetInstance.ExecutablePath);

            var results = new List<IThreatObject>();

            AppConfig.GetInstance.LL.LogHeadMessage("_PreparingToScan");
            List<Process> procs = ProcessManager.SafeGetProcesses();

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
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

                    int processId = p.Id;
                    long memorySize = p.PrivateMemorySize64;
                    TimeSpan cpuTime = p.TotalProcessorTime;
                    DateTime startDateTime = p.StartTime;
                    string args = ProcessManager.GetProcessCommandLine(p);

                    string hash = string.Empty;
                    long fileSize = 0;
                    WinVerifyTrustResult trustResult = WinVerifyTrustResult.Error; // Значение по умолчанию
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
                    if (AppConfig.GetInstance.bootMode != BootMode.SafeMinimal)
                    {
                        remotePort = ProcessManager.GetPortByProcessId(p.Id);
                    }

                    bool hasSystemPrivilege = ProcessManager.IsSystemProcess(processId);

                    var processThreat = new ProcessThreatObject(processId, processName, args, cpuTime, startDateTime, memorySize, remotePort, moduleCollection, isDotnet, hasSystemPrivilege, fileThreat);

                    results.Add(processThreat);
                }
                catch (ArgumentException ae)
                {
#if DEBUG
                    //Console.WriteLine($"\t[DBG] {p.ProcessName} {ae.Message}");
#endif
                    continue;
                }
                catch (InvalidOperationException ioe)
                {
#if DEBUG
                    // Console.WriteLine($"\t[DBG] {p.ProcessName} {ioe.Message}");
#endif
                    continue;
                }
                catch (Win32Exception w32e)
                {
#if DEBUG
                    //Console.WriteLine($"\t[DBG] {p.ProcessName} {w32e.Message}");
#endif
                    continue;
                }
                catch (Exception ex) when (ex.HResult.Equals(unchecked((int)0x8007012B)))
                {

                    DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorScanProcesses"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }

            procs.Clear();
            return results;
        }
    }
}
