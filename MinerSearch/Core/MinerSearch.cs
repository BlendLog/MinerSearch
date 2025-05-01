using DBase;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using NetFwTypeLib;
using netlib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSearch
{
    public class MinerSearch
    {

        int[] _PortList = new[]
        {
            1111,
            1112,
            2020,
            3333,
            4028,
            4040,
            4141,
            4444,
            5555,
            6633,
            6666,
            7777,
            9980,
            9999,
            10191,
            14433,
            20009,
        };

        readonly string[] _nvdlls = new[]
        {
            "nvcompiler.dll",
            "nvopencl.dll",
            "nvfatbinaryLoader.dll",
            "nvapi64.dll",
            "OpenCL.dll",
        };

        HashSet<string> suspFls_path = new HashSet<string>();

        byte[] startSequence = { 0xFF, 0xC7, 0x05, 0xC5 };
        byte[] endSequence = { 0xE8, 0x54, 0xFF, 0xFF, 0xFF };

        long maxFileSize = 200 * 1024 * 1024;
        long minFileSize = 2112;


        public List<int> mlwrPids = new List<int>();
        public HashSet<string> founded_suspLockedPaths = new HashSet<string>();
        public HashSet<string> founded_mlwrFiles = new HashSet<string>();
        public HashSet<string> founded_mlwrDirs = new HashSet<string>();

        public static List<ScanResult> scanResults = new List<ScanResult>() { };

        WinTrust winTrust = new WinTrust();


        public void DetectRk()
        {
            Program.LL.LogHeadMessage("_ChekingR00tkit");

            Native.R77_PROCESS[] r77Processes = new Native.R77_PROCESS[Native.MaxProcesses];
            uint processCount = Native.MaxProcesses;

            RkRemover remover = new RkRemover();
            remover.GetR77Processes(ref r77Processes, ref processCount);
            if (processCount > 0)
            {
                LocalizedLogger.LogR00TkitPresent();
                Program.totalFoundThreats++;

                remover.DetachAllInjectedProcesses(r77Processes, processCount);
                remover.TerminateR77Service(-1);
                remover.RemoveR77Config();

                remover.GetR77Processes(ref r77Processes, ref processCount);
                if (processCount == 0)
                {
                    Program.LL.LogSuccessMessage("_SuccessR00tkitNeutralized");
                }


                foreach (Process process in ProcessManager.SafeGetProcesses())
                {
                    if (!process.ProcessName.StartsWith(new StringBuilder("di").Append("al").Append("er").ToString())) continue;
                    try
                    {
                        ProcessManager.SuspendProcess(process);
                        mlwrPids.Add(process.Id);
                        Program.totalFoundThreats++;
                    }
                    catch (Exception) { }

                }

            }
            else
            {
                LocalizedLogger.LogNoThreatsFound();
            }

        }

        public void Scan()
        {
            Program.LL.LogHeadMessage("_ScanProcesses");

            string myExePath = FileSystemManager.GetUNCPath(Assembly.GetExecutingAssembly().Location);

            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<Process> procs = ProcessManager.SafeGetProcesses();

            HashSet<string> badStringInArgs = new HashSet<string>()
            {
               new StringBuilder("--").Append("pa").Append("ss").ToString(),
               new StringBuilder("--").Append("al").Append("go").ToString(),
               new StringBuilder("po").Append("ol").Append(".").ToString(),
               new StringBuilder("st").Append("ra").Append("tu").Append("m").ToString(),
               new StringBuilder("na").Append("no").Append("po").Append("ol").ToString(),
               new StringBuilder("mi").Append("ni").Append("ng").Append("oc").Append("ea").Append("n.").ToString(),
            };

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
                string args = null;
                bool isMaliciousProcess = false;
                bool isBadArgsPatternPresent = false;
                bool isFileTooBig = false;

                try
                {
                    string processPath = FileSystemManager.GetUNCPath(Path.GetFullPath(p.MainModule.FileName));
                    if (processPath.Equals(myExePath))
                    {
                        continue;
                    }

                    if (!p.HasExited)
                    {
                        processName = p.ProcessName;
                        processId = p.Id;

                        args = ProcessManager.GetProcessCommandLine(p);

                        if (string.IsNullOrEmpty(args))
                        {
                            LocalizedLogger.LogScanning(processName);
                        }
                        else LocalizedLogger.LogScanning(processName, args);
                    }
                    else
                    {
                        processId = -1;
                        continue;
                    }

                    riskLevel = 0;
                    isValidProcess = false;

                    if (File.Exists(processPath))
                    {

                        if (winTrust.VerifyEmbeddedSignature(processPath) != WinVerifyTrustResult.Success)
                        {
                            riskLevel += 1;
                            isValidProcess = false;
                        }
                        else
                        {
                            isValidProcess = true;
                        }


                        try
                        {
                            string originalFileName = p.MainModule.FileVersionInfo.OriginalFilename;
                            string fileDescription = p.MainModule.FileVersionInfo.FileDescription;


                            if (ProcessManager.IsTrustedProcess(MSData.Instance, originalFileName, isValidProcess))
                            {
                                continue;
                            }


                            if (fileDescription != null)
                            {
                                if (fileDescription.Equals("svhost", StringComparison.OrdinalIgnoreCase))
                                {
                                    Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                                    suspFls_path.Add(p.MainModule.FileName);
                                    riskLevel += 2;
                                }
                            }

                            if (originalFileName != null)
                            {
                                if (originalFileName.IndexOf(new StringBuilder("Spot").Append("ifySta").Append("rtupTas").Append("k.exe").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                                    riskLevel += 3;
                                    founded_mlwrFiles.Add(p.MainModule.FileName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            Program.LL.LogErrorMessage("_Error", ex);
                        }
                    }
                    else
                    {
                        Program.LL.LogWarnMessage("_FileIsNotFound", processPath);
                        riskLevel += 1;
                    }

                    if (processName.IndexOf("helper", StringComparison.OrdinalIgnoreCase) >= 0 && !isValidProcess)
                    {
                        riskLevel += 1;
                    }

                    int modCount = 0;
                    try
                    {
                        foreach (ProcessModule pMod in p.Modules)
                        {
                            modCount += _nvdlls.Count(name => pMod.ModuleName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                    }


                    if (modCount > 2)
                    {
                        Program.LL.LogWarnMessage("_GPULibsUsage", $"{processName}.exe, PID: {processId}");
                        riskLevel += 1;

                    }

                    if (Program.bootMode != BootMode.SafeMinimal)
                    {
                        try
                        {
                            int remoteport = ProcessManager.GetPortByProcessId(p.Id);
                            if (remoteport != -1 && remoteport != 0)
                            {
                                if (_PortList.Contains(remoteport))
                                {
                                    Program.LL.LogWarnMessage("_BlacklistedPort", $"{remoteport} - {processName}");
                                    riskLevel += 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_Error", ex);
                        }
                    }

                    if (processName.Equals(MSData.Instance.SysFileName[19], StringComparison.OrdinalIgnoreCase))
                    {
                        Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                        riskLevel += 3;
                    }

                    if (!string.IsNullOrEmpty(args))
                    {
                        foreach (int port in _PortList)
                        {
                            if (args.IndexOf($":{port}", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                riskLevel += 1;
                                Program.LL.LogWarnMessage("_BlacklistedPortCMD", $"{port} : {processName}.exe");
                            }
                        }

                        if (badStringInArgs.Any(s => args.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            riskLevel += 3;
                            isBadArgsPatternPresent = true;
                            Program.LL.LogWarnMediumMessage("_PresentInCmdArgs");
                        }


                        if (args.IndexOf("-systemcheck", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            riskLevel += 2;
                            Program.LL.LogWarnMessage("_FakeSystemTask");

                            try
                            {
                                if (p.MainModule.FileName.IndexOf("appdata", StringComparison.OrdinalIgnoreCase) >= 0 && p.MainModule.FileName.IndexOf("windows", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    riskLevel += 1;
                                    suspFls_path.Add(p.MainModule.FileName);
                                }
                            }
                            catch (InvalidOperationException ex)
                            {
                                Program.LL.LogErrorMessage("_Error", ex);
                                continue;

                            }

                        }

                        if ((processName.Equals(MSData.Instance.SysFileName[3], StringComparison.OrdinalIgnoreCase) && args.IndexOf($"\\??\\{Program.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) == -1))
                        {
                            riskLevel += 3;
                            if (args.IndexOf($"\\\\?\\{Program.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                riskLevel--;
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                            }
                        }
                        if (processName.Equals(MSData.Instance.SysFileName[4], StringComparison.OrdinalIgnoreCase) && args.IndexOf($"{MSData.Instance.SysFileName[4]}.exe -k", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }
                        if (processName.Equals(MSData.Instance.SysFileName[4], StringComparison.OrdinalIgnoreCase) && (args.IndexOf($"{MSData.Instance.SysFileName[4]}.exe -k dcomlaunch", StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            foreach (ProcessModule pMod in p.Modules)
                            {
                                WinVerifyTrustResult pModSignature = winTrust.VerifyEmbeddedSignature(pMod.FileName);
                                if (pModSignature != WinVerifyTrustResult.Success && pModSignature != WinVerifyTrustResult.Error)
                                {
                                    Program.LL.LogWarnMediumMessage("_ServiceDcomAbusing", pMod.FileName + $" | PID: {p.Id}");
                                }
                            }
                        }
                        if (processName.Equals(MSData.Instance.SysFileName[5], StringComparison.OrdinalIgnoreCase) && !Program.RunAsSystem)
                        {
                            bool isFakeDwm = false;
                            if (OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) == -1) //is not Win7
                            {
                                if (!ProcessManager.GetProcessOwner(p.Id).StartsWith("Window Manager\\DWM-", StringComparison.OrdinalIgnoreCase))
                                {
                                    isFakeDwm = true;
                                }

                                int dwmParentPID = ProcessManager.GetParentProcessId(p.Id);
                                if (dwmParentPID != 0)
                                {
                                    try
                                    {
                                        Process dwmParent = Process.GetProcessById(dwmParentPID);
                                        if (dwmParent.HasExited || !dwmParent.ProcessName.Equals(MSData.Instance.SysFileName[7], StringComparison.OrdinalIgnoreCase))
                                        {
                                            isFakeDwm = true;
                                        }
                                    }
                                    catch (ArgumentException)
                                    {
                                        isFakeDwm = true;
                                    }

                                }
                                else isFakeDwm = true;
                            }
                            else
                            {
                                int dwmParentPID = ProcessManager.GetParentProcessId(p.Id);
                                if (dwmParentPID != 0)
                                {
                                    try
                                    {
                                        Process dwmParent = Process.GetProcessById(dwmParentPID);
                                        if (dwmParent.HasExited || !dwmParent.ProcessName.Equals(MSData.Instance.SysFileName[4], StringComparison.OrdinalIgnoreCase))
                                        {
                                            isFakeDwm = true;
                                        }
                                    }
                                    catch (ArgumentException)
                                    {
                                        isFakeDwm = true;
                                    }

                                }
                                else isFakeDwm = true;
                            }

                            try
                            {
                                if (ProcessManager.IsSystemProcess(p.Id))
                                {
                                    isFakeDwm = true;
                                }
                            }
                            catch { isFakeDwm = true; }

                            if (isFakeDwm)
                            {
                                Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                                riskLevel += 3;
                            }
                        }
                        if (processName.Equals(MSData.Instance.SysFileName[17], StringComparison.OrdinalIgnoreCase))
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals(MSData.Instance.SysFileName[32], StringComparison.OrdinalIgnoreCase) && args.IndexOf("#system32", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals(MSData.Instance.SysFileName[31], StringComparison.OrdinalIgnoreCase) && !p.HasExited ? (DateTime.Now - p.StartTime).TotalSeconds >= 60 : false)
                        {
                            Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals("explorer", StringComparison.OrdinalIgnoreCase) && args.IndexOf($@"{Program.drive_letter}:\Windows\Explorer.exe", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            riskLevel++;
                        }

                    }

                    bool isSuspiciousPath = false;
                    string fullPath = p.MainModule.FileName;
                    string appData = Environment.GetEnvironmentVariable("AppData") ?? "";
                    if (!isValidProcess && fullPath.StartsWith(appData, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(Path.GetExtension(fullPath)))
                    {
                        Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        isSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    if (processName.Equals(MSData.Instance.SysFileName[28], StringComparison.OrdinalIgnoreCase) && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        isSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    try
                    {
                        fileSize = new FileInfo(processPath).Length;
                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                    }

                    for (int i = 0; i < MSData.Instance.SysFileName.Length; i++)
                    {

                        if (processName.Equals(MSData.Instance.SysFileName[i], StringComparison.OrdinalIgnoreCase))
                        {

                            if (fullPath.IndexOf($"{Program.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\syswow64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\winsxs\\amd64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\winsxs\\x86", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\microsoft.net\\framework64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\microsoft.net\\framework", StringComparison.OrdinalIgnoreCase) == -1)
                            {

                                Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }

                            if (fileSize >= MSData.Instance.constantFileSize[i] * 3 && !isValidProcess)
                            {
                                Program.LL.LogWarnMessage("_SuspiciousFileSize", FileChecker.GetFileSize(fileSize));
                                riskLevel += 1;
                            }

                        }

                    }

                    if (fileSize >= maxFileSize)
                    {
                        riskLevel += 1;
                        isFileTooBig = true;
                    }

                    try
                    {
                        if (processName.Equals(MSData.Instance.SysFileName[17], StringComparison.OrdinalIgnoreCase) && p.MainModule.FileName.Equals("?di?al?er.ex?e".Replace("?", ""), StringComparison.OrdinalIgnoreCase))
                        {
                            Program.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                            riskLevel += 3;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                        continue;
                    }


                    if (processName.Equals("rundll", StringComparison.OrdinalIgnoreCase) || processName.Equals("system", StringComparison.OrdinalIgnoreCase) || processName.Equals("wi?ns?er?v".Replace("?", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");

                        isSuspiciousPath = true;
                        riskLevel += 3;
                    }

                    if (processName.Equals("explorer", StringComparison.OrdinalIgnoreCase))
                    {

                        try
                        {
                            int ParentProcessId = ProcessManager.GetParentProcessId(processId);
                            if (ParentProcessId != 0)
                            {
                                Process ParentProcess = Process.GetProcessById(ParentProcessId);
                                if (ParentProcess.ProcessName.Equals("explorer", StringComparison.OrdinalIgnoreCase))
                                {
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


                        if (ProcessManager.IsSystemProcess(p.Id) && !Program.RunAsSystem)
                        {
                            riskLevel += 3;
                        }
                    }

                    if (processName.Equals("notepad", StringComparison.OrdinalIgnoreCase))
                    {
                        if ((ProcessManager.IsSystemProcess(p.Id) && !OSExtensions.IsWinPEEnv()))
                        {
                            riskLevel += 2;
                        }

                        if (p.TotalProcessorTime > new TimeSpan(0, 1, 0) || (p.PagedMemorySize64 / (1024 * 1024) >= 2048))
                        {
                            riskLevel += 2;
                        }
                    }

                    if (p.TotalProcessorTime > new TimeSpan(0, 1, 0) || (p.PagedMemorySize64 / (1024 * 1024) >= 2048))
                    {

                        if (File.Exists(processPath))
                        {
                            if (IsMalic1ousFile(FileSystemManager.GetUNCPath(processPath), false))
                            {
                                riskLevel += 3;
                                isMaliciousProcess = true;
                            }

                        }
                        else
                        {
                            Program.LL.LogWarnMessage("_FileIsNotFound", processPath);
                        }
                    }

                    if (processPath.Contains(@":\Windows\Microsoft.NET\Framework"))
                    {
                        try
                        {
                            int processParentId = ProcessManager.GetParentProcessId(p.Id);
                            if (processParentId == 0)
                            {
                                riskLevel += 1;
                            }
                            else
                            {
                                Process parentProcess = Process.GetProcessById(processParentId);
                                if (parentProcess != null)
                                {
                                    if (parentProcess.ProcessName.Equals(MSData.Instance.SysFileName[9], StringComparison.OrdinalIgnoreCase))
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


                        if (p.TotalProcessorTime <= new TimeSpan(0, 0, 15) && (p.PrivateMemorySize64 / (1024 * 1024) <= 100))
                        {
                            riskLevel += 3;
                        }
                    }

                    if (MSData.Instance.obfStr2.Any(s => FileSystemManager.GetUNCPath(s).Equals(processPath, StringComparison.OrdinalIgnoreCase)))
                    {
                        riskLevel += 3;
                        isMaliciousProcess = true;
                    }

                    if (riskLevel >= 3)
                    {
                        Program.LL.LogCautionMessage("_ProcessFound", ProcessManager.GetLocalizedRiskLevel(riskLevel));
                        Program.totalFoundThreats++;
                        if (!Program.ScanOnly)
                        {
                            ProcessManager.SuspendProcess(p);
                        }



                        if (isSuspiciousPath || isMaliciousProcess)
                        {
                            if (!Program.ScanOnly)
                            {
                                UnlockObjectClass.DisableExecute(processPath);
                                suspFls_path.Add(processPath);

                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, processPath, ScanActionType.Skipped));
                            }
                        }

                        if (isBadArgsPatternPresent && isFileTooBig && !isValidProcess)
                        {
                            if (!Program.ScanOnly)
                            {
                                UnlockObjectClass.DisableExecute(processPath);
                                founded_mlwrFiles.Add(processPath);

                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, processPath, ScanActionType.Skipped));
                            }
                        }

                        mlwrPids.Add(processId);
                    }

                }
                catch (ArgumentException ae)
                {
#if DEBUG
                    Console.WriteLine($"\t[DBG] {p.ProcessName} {ae.Message}");
#endif
                    continue;
                }
                catch (InvalidOperationException ioe)
                {
#if DEBUG
                    Console.WriteLine($"\t[DBG] {p.ProcessName} {ioe.Message}");
#endif
                    continue;
                }
                catch (Win32Exception w32e)
                {
#if DEBUG
                    Console.WriteLine($"\t[DBG] {p.ProcessName} {w32e.Message}");
#endif
                    continue;
                }
                catch (Exception ex) when (ex.HResult.Equals(unchecked((int)0x8007012B)))
                {

                    MessageBoxCustom.Show(Program.LL.GetLocalizedString("_ErrorScanProcesses"), Program._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }

                if ((riskLevel == 0 || !isMaliciousProcess) && Program.verbose)
                {
                    LocalizedLogger.LogOK();
                }
            }

            procs.Clear();

        }
        public void StaticScan()
        {
            Program.LL.LogHeadMessage("_ScanDirectories");

            if (Program.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(Program.drive_letter + ":\\");
            }

            if (!Program.WinPEMode)
            {

                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (DesktopPath != null && !Program.RunAsSystem)
                {
                    MSData.Instance.obfStr5.Add(Path.Combine(DesktopPath, "aut~olo~~gger".Replace("~", "")));
                    MSData.Instance.obfStr5.Add(Path.Combine(DesktopPath, "av~_bl~~ock~_rem~~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DesktopPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !Program.WinPEMode && !Program.fullScan)
                    {
                        MSData.Instance.obfStr6.Add(DesktopPath);
                    }
                }

                string DownloadsPath = FileSystemManager.GetDownloadsPath();
                if (DownloadsPath != null && !Program.RunAsSystem)
                {
                    MSData.Instance.obfStr5.Add(Path.Combine(DownloadsPath, "auto~lo~gge~~r".Replace("~", "")));
                    MSData.Instance.obfStr5.Add(Path.Combine(DownloadsPath, "av_~bl~o~ck~_re~m~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DownloadsPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !Program.WinPEMode && !Program.fullScan)
                    {
                        MSData.Instance.obfStr6.Add(DownloadsPath);
                    }
                }
            }

            if (!string.IsNullOrEmpty(Program.selectedPath) && Directory.Exists(Program.selectedPath) && !Program.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(Program.selectedPath);
            }

            MSData.Instance.obfStr6 = MSData.Instance.obfStr6.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            ScanDirectories(MSData.Instance.obfStr5, founded_suspLockedPaths, true);

            if (!Program.ScanOnly)
            {
                if (founded_suspLockedPaths.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

            }


            string baseDirectory = Program.drive_letter + @":\ProgramData";
            string pattern = @"^[a-zA-Z0-9_\-]+-[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
            Regex regex = new Regex(pattern);

            foreach (string directory in Directory.GetDirectories(baseDirectory))
            {
                string directoryName = Path.GetFileName(directory);

                if (regex.IsMatch(directoryName))
                {
                    foreach (string file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
                    {
                        if (FileChecker.CalculateMD5(file).Equals("0c0195c48b6b8582fa6f6373032118da"))
                        {
                            MSData.Instance.obfStr1.Add(directory);
                        }
                    }
                }
            }

            ScanDirectories(MSData.Instance.obfStr1, founded_mlwrDirs);
            if (!Program.ScanOnly)
            {
                if (founded_mlwrDirs.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            Program.LL.LogHeadMessage("_ScanFiles");

            string _baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).Replace("x:", $@"{Program.drive_letter}:");
            FindMlwrFiles(_baseDirectory);

            if (!Program.ScanOnly)
            {
                if (founded_mlwrFiles.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            if (!Program.WinPEMode)
            {
                ScanRegistry();
                ScanWMI();
                if (!Program.no_services)
                {
                    ScanServices();
                }

                switch (OSExtensions.GetBootMode())
                {
                    case BootMode.Normal:
                        Program.LL.LogHeadMessage("_ScanFirewall");
                        ScanFirewall();
                        if (!Program.no_scan_tasks)
                        {
                            Program.LL.LogHeadMessage("_ScanTasks");
                            try
                            {
                                ScanTaskScheduler();
                            }
                            catch (FileNotFoundException)
                            {
                                MessageBoxCustom.Show(Program.LL.GetLocalizedString("_ErrorNotFoundComponent"), Program._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            catch (Exception ex)
                            {
                                Program.LL.LogErrorMessage("_Error", ex);
                            }
                        }
                        break;
                    case BootMode.SafeMinimal:
                        Program.LL.LogStatusMessage("_SafeBootHint");
                        break;
                    case BootMode.SafeNetworking:
                        Program.LL.LogHeadMessage("_ScanFirewall");
                        ScanFirewall();
                        Program.LL.LogHeadMessage("_SafeBootNetworkingHint");

                        break;
                    default:
                        break;
                }
            }

            if (!Program.no_check_hosts)
            {
                CleanHosts();
            }


        }

        public void Clean()
        {

            if (mlwrPids.Count != 0)
            {
                Program.LL.LogHeadMessage("_Malici0usProcesses");
                Program.LL.LogCautionMessage("_MlwrProcessesCount", mlwrPids.Count.ToString());

                if (!Program.ScanOnly)
                {
                    Program.LL.LogHeadMessage("_TryCloseProcess");
                    ProcessManager.UnProtect(mlwrPids.ToArray());
                }

                foreach (var id in mlwrPids)
                {
                    string pname = string.Empty;
                    try
                    {
                        using (Process p = Process.GetProcessById(id))
                        {
                            pname = p.ProcessName;
                            int pid = p.Id;

                            if (!Program.ScanOnly)
                            {
                                p.Kill();
                            }

                            if (!Program.ScanOnly)
                            {
                                if (p.HasExited)
                                {
                                    Program.LL.LogSuccessMessage("_ProcessTerminated", $"{pname}, PID: {pid}");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{Program.LL.GetLocalizedString("_Just_Process")} {pname}", ScanActionType.Terminated));
                                }

                            }
                            else
                            {
                                Program.LL.LogCautionMessage("_Malici0usProcess", $"{pname} - PID: {pid}");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{Program.LL.GetLocalizedString("_Just_Process")} {pname} - PID: {pid}", ScanActionType.Active));

                            }
                        }

                    }
                    catch (InvalidOperationException)
                    {
                        Program.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {id}");
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                    catch (Exception e)
                    {
                        Program.LL.LogErrorMessage(Program.ScanOnly ? "_Error" : "_ErrorTerminateProcess", e);
                        Program.totalNeutralizedThreats--;
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{Program.LL.GetLocalizedString("_Just_Process")} {pname}", ScanActionType.Error));

                    }
                }
                if (Program.ScanOnly)
                {
                    LocalizedLogger.LogScanOnlyMode();
                }
            }

            Program.LL.LogHeadMessage("_RemovingKnownMlwrFiles");

            int deletedFilesCount = 0;



            foreach (string path in MSData.Instance.obfStr2)
            {
                if (File.Exists(path))
                {
                    Program.totalFoundThreats++;
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            if (UnlockObjectClass.KillAndDelete(path))
                            {
                                Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                deletedFilesCount++;
                                continue;
                            }
                            else
                            {
                                if (UnlockObjectClass.ResetObjectACL(Path.GetDirectoryName(path)))
                                {
                                    Program.LL.LogSuccessMessage("_UnlockSuccess", path);
                                }

                                if (UnlockObjectClass.KillAndDelete(path))
                                {
                                    Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                    deletedFilesCount++;
                                    continue;
                                }
                                else
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotRemove", new Exception(""), path, "_File");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error));
                                    Program.totalNeutralizedThreats--;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error));
                            Program.totalNeutralizedThreats--;
                        }
                    }
                    else
                    {
                        Program.LL.LogCautionMessage("_Malici0usFile", path);
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                    }

                }
            }

            if (!Program.ScanOnly)
            {
                if (deletedFilesCount == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
            else LocalizedLogger.LogScanOnlyMode();


            CleanFoundedMlwr();

            if (suspFls_path.Count > 0)
            {
                Program.LL.LogHeadMessage("_RemovingMLWRFiles");
                foreach (string path in suspFls_path)
                {
                    if (File.Exists(path))
                    {
                        Program.totalFoundThreats++;
                        if (!Program.ScanOnly)
                        {
                            UnlockObjectClass.ResetObjectACL(path);
                            try
                            {
                                File.SetAttributes(path, FileAttributes.Normal);
                                File.Delete(path);
                                Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));


                            }
                            catch (Exception)
                            {
                                Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                                Program.LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                                if (UnlockObjectClass.IsLockedObject(Path.GetDirectoryName(path)))
                                {
                                    if (UnlockObjectClass.ResetObjectACL(Path.GetDirectoryName(path)))
                                    {
                                        Program.LL.LogSuccessMessage("_UnlockSuccess", path);
                                    }
                                }
                                try
                                {

                                    UnlockObjectClass.KillAndDelete(path);
                                    if (!File.Exists(path))
                                    {
                                        Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error));
                                    Program.totalNeutralizedThreats--;
                                }
                            }
                        }
                        else
                        {
                            Program.LL.LogCautionMessage("_Malici0usFile", path);
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                        }
                    }
                }
            }

            if (!Program.ScanOnly)
            {
                Program.LL.LogHeadMessage("_CheckingTermService");
                ServiceHelper.CheckTermService();
            }

            if (founded_mlwrDirs.Count > 0)
            {
                Program.LL.LogHeadMessage("_RemovingMLWRPaths");

                foreach (string str in founded_mlwrDirs)
                {
                    if (!Program.ScanOnly)
                    {
                        if (Directory.Exists(str))
                        {
                            try
                            {
                                FileSystemManager.ResetAttributes(str);
                                Directory.Delete(str, true);
                                if (!Directory.Exists(str))
                                {
                                    Program.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));

                                }
                            }
                            catch (Exception)
                            {
                                foreach (var file in FileSystemManager.GetOpenFilesInDirectory(str))
                                {
                                    var pid = ProcessManager.GetProcessIdByFilePath(file);
                                    try
                                    {
                                        Process pr = Process.GetProcessById((int)pid);

                                        if (pr != null)
                                        {
                                            if (pr.Id != 0)
                                            {
                                                ProcessManager.UnProtect(new int[] { pr.Id });
                                                pr.Kill();
                                            }
                                        }
                                    }
                                    catch (ArgumentException)
                                    {
                                        continue;
                                    }
                                    catch (Exception) { }
                                }

                                try
                                {
                                    if (UnlockObjectClass.ResetObjectACL(str))
                                    {
                                        Directory.Delete(str, true);
                                        if (!Directory.Exists(str))
                                        {
                                            Program.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));
                                        }
                                    }
                                }
                                catch (Exception e)
                                {

                                    if (e.Message.EndsWith(".dll\".") || e.Message.EndsWith(".exe\"."))
                                    {
                                        Program.LL.LogErrorMessage("_ErrorCannotRemove", e, $"\"{str}\"", "_Directory");
                                        Program.totalNeutralizedThreats--;
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Error));

                                    }
                                    else
                                    {
                                        Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", $"\"{str}\"", e.Message);
                                        Program.totalFoundThreats--;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Program.LL.LogWarnMediumMessage("_MaliciousDir", str);
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Skipped));

                    }
                }
            }

            if (founded_suspLockedPaths.Count > 0)
            {
                UnlockFolders(founded_suspLockedPaths);
            }

            if (Program.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }

            if (!Program.WinPEMode)
            {
                Program.LL.LogHeadMessage("_CheckUserJohn");

                if (OSExtensions.CheckUserExists("J?ohn".Replace("?", "")))
                {
                    Program.totalFoundThreats++;
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            OSExtensions.DeleteUser("J?ohn".Replace("?", ""));
                            Thread.Sleep(100);
                            if (!OSExtensions.CheckUserExists("J?ohn".Replace("?", "")))
                            {
                                Program.LL.LogSuccessMessage("_Userprofile", "\"J?ohn\"", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, "Pro?file:J??oh?n".Replace("?", ""), ScanActionType.Deleted));

                            }
                            else
                            {
                                Program.LL.LogErrorMessage("_ErrorCannotRemove", new Exception(""), $"\"John\"", "_Userprofile");
                                scanResults.Add(new ScanResult(ScanObjectType.Unknown, "Pro?file:J??oh?n".Replace("?", ""), ScanActionType.Error));
                                Program.totalNeutralizedThreats--;
                            }

                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"J?ohn\"".Replace("?", ""), "_Userprofile");
                            Program.totalNeutralizedThreats--;
                        }
                    }
                    else
                    {
                        Program.LL.LogWarnMediumMessage("_MaliciousProfile", "J?ohn");
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, "Pro?file:J??oh?n".Replace("?", ""), ScanActionType.Skipped));
                        LocalizedLogger.LogScanOnlyMode();
                    }

                }
                else
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

            }

        }
        void UnlockFolders(HashSet<string> inputList)
        {
            int foldersDeleted = 0;
            foreach (string str in inputList)
            {
                try
                {
                    if (!Program.ScanOnly)
                    {
                        if (UnlockObjectClass.IsLockedObject(str))
                        {
                            UnlockObjectClass.ResetObjectACL(str);
                            if (FileSystemManager.IsDirectoryEmpty(str))
                            {
                                Directory.Delete(str, true);
                                if (!Directory.Exists(str))
                                {
                                    Program.LL.LogMessage("\t[_]", "_RemovedEmptyDir", $"\"{str}\"", ConsoleColor.White);
                                    foldersDeleted++;

                                }
                            }
                        }

                    }
                    else
                    {
                        Program.LL.LogWarnMessage("_LockedDir", $"\"{str}\"");
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    Program.LL.LogCautionMessage("_ErrorLockedByWD", str);
                }
                catch (Exception ex1)
                {
#if DEBUG
                    Logger.WriteLog($"\t[*] DeleteEmpyFolders exception: {ex1.Message}", ConsoleColor.Gray, false);
#endif
                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex1, str, "_Directory");
                }
            }

            if (!Program.ScanOnly)
            {
                if (foldersDeleted == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

        }
        void ScanDirectories(List<string> constDirsArray, HashSet<string> newList, bool checkAccessible = false)
        {
            foreach (string dir in constDirsArray)
            {
                if (Directory.Exists(dir))
                {
                    try
                    {

                        if (checkAccessible)
                        {
                            if (UnlockObjectClass.IsLockedObject(dir))
                            {
                                newList.Add(dir);
                                continue;
                            }
                            if (FileSystemManager.IsDirectoryEmpty(dir))
                            {
                                newList.Add(dir);
                            }
                        }
                        else
                        {
                            Program.totalFoundThreats++;
                            newList.Add(dir);
                        }
                    }
                    catch (SecurityException)
                    {
                        if (checkAccessible)
                        {
                            Program.totalFoundThreats++;
                        }
                        newList.Add(dir);
                    }

                }
            }
        }
        void ScanFirewall()
        {
            int firewall_items = 0;
            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);

                INetFwRules rules = fwPolicy2.Rules;
                foreach (string programPath in MSData.Instance.obfStr2)
                {
                    foreach (INetFwRule rule in rules)
                    {
                        if (rule.ApplicationName != null)
                        {
                            if (rule.ApplicationName.Equals(programPath, StringComparison.OrdinalIgnoreCase))
                            {
                                Program.LL.LogMessage("\t[.]", "_Name", rule.Name, ConsoleColor.White);
                                Program.LL.LogWarnMessage("_Path", rule.ApplicationName);

                                if (!Program.ScanOnly)
                                {
                                    string ruleName = rule.Name;
                                    rules.Remove(ruleName);
                                    firewall_items++;
                                    Program.LL.LogSuccessMessage("_Rule", ruleName, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Fi?re?wall:{ruleName}".Replace("?", ""), ScanActionType.Deleted));
                                }
                                else
                                {
                                    string ruleName = rule.Name;
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Fir?ewa?ll:{ruleName}".Replace("?", ""), ScanActionType.Skipped));
                                }

                                Logger.WriteLog($"------------------------------", ConsoleColor.White, true, true);
                            }
                        }

                    }

                }
                if (!Program.ScanOnly && firewall_items == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

                if (Program.ScanOnly)
                {
                    LocalizedLogger.LogScanOnlyMode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get firewall rules: {ex.Message}");
            }
        }

        void FindMlwrFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            var files = FileEnumerator.GetFiles(directoryPath, "*.bat");

            foreach (string file in files)
            {
                if (FileSystemManager.IsReparsePoint(Path.GetDirectoryName(file)) || !FileSystemManager.IsAccessibleFile(file))
                {
                    continue;
                }

                if (FileChecker.IsBatchFileBad(file))
                {
                    Program.LL.LogCautionMessage("_Malici0usFile", file);
                    Program.totalFoundThreats++;
                    founded_mlwrFiles.Add(file);

                }
                else
                {
                    Program.LL.LogMessage("[.]", "_File", file, ConsoleColor.White);
                }
            }

            foreach (string nearExeFile in FileEnumerator.GetFiles(directoryPath, "*.exe", 0, Program.maxSubfolders))
            {
                if (FileChecker.IsSfxArchive(nearExeFile))
                {
                    WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(nearExeFile);
                    if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.Error)
                    {
                        Program.totalFoundThreats++;
                        founded_mlwrFiles.Add(nearExeFile);
                    }

                }
            }



        }
        void CleanHosts()
        {
            Program.LL.LogHeadMessage("_ScanningHosts");

            List<string> linesToDelete = new List<string>();
            string hostsPath_full = $"{Program.drive_letter}{MSData.Instance.queries["h0sts"]}";
            string hostsPath_tmp = Path.Combine(Path.GetTempPath(), $"hosts{Utils.GetRndString(16)}");

            if (!File.Exists(hostsPath_full))
            {
                Program.LL.LogMessage("\t[?]", "_HostsFileMissing", "", ConsoleColor.Gray);
                string hostsdir = Path.GetDirectoryName(hostsPath_full);
                if (!Directory.Exists(hostsdir))
                {
                    Directory.CreateDirectory(hostsdir);
                }
                if (Program.WinPEMode && !Directory.Exists(hostsdir))
                {
                    Directory.CreateDirectory(hostsdir);
                }
                try
                {
                    File.Create(hostsPath_full).Close();
                    Thread.Sleep(100);
                    if (File.Exists(hostsPath_full))
                    {
                        Program.LL.LogSuccessMessage("_HostsFileCreated");
                    }
                }
                catch (Exception e)
                {
                    Program.LL.LogErrorMessage("_ErrorCleanHosts", e);
                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Inaccassible));
                }
                return;
            }
            else
            {
                try
                {
                    File.Copy(hostsPath_full, hostsPath_tmp, true);
                }
                catch (UnauthorizedAccessException)
                {
                    try
                    {
                        UnlockObjectClass.ResetObjectACL(hostsPath_full);
                        File.SetAttributes(hostsPath_full, FileAttributes.Normal);
                        File.Copy(hostsPath_full, hostsPath_tmp, true);
                    }
                    catch (Exception e)
                    {
                        Program.LL.LogErrorMessage("_ErrorCleanHosts", e);
                        scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Inaccassible));
                    }

                }
            }

            if (File.Exists(hostsPath_tmp))
            {
                File.SetAttributes(hostsPath_tmp, FileAttributes.Normal);

                List<string> lines = File.ReadLines(hostsPath_tmp)
                                  .Where(line => !string.IsNullOrWhiteSpace(line))
                                  .Distinct().ToList();
                int deletedLineCount = 0;
                bool isHostsInfected = false;

                try
                {
                    for (int i = lines.Count - 1; i >= 0; i--)
                    {
                        string line = lines[i];

                        if (line.StartsWith("#") || MSData.Instance.whiteListedWords.Any(word => line.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            continue;
                        }

                        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2) continue;

                        string ipAddress = parts[0];
                        string domain = parts[1].ToLower();
                        if (domain.StartsWith("www.")) domain = domain.Substring(4);

                        foreach (HashedString hLine in MSData.Instance.hStrings)
                        {
                            if (hLine.OriginalLength <= domain.Length)
                            {
                                string truncatedDomain = domain.Substring(domain.Length - hLine.OriginalLength);

                                if (Utils.StringMD5(truncatedDomain).Equals(hLine.Hash))
                                {
                                    if (!Program.ScanOnly)
                                    {
                                        if (!MSData.Instance.hStrings.Any(h => Utils.StringMD5(domain).Equals(h.Hash)))
                                        {
                                            linesToDelete.Add(line);
                                        }
                                        else
                                        {
                                            Program.LL.LogWarnMediumMessage("_MaliciousEntry", lines[i]);
                                            lines.RemoveAt(i);
                                            deletedLineCount++;
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        isHostsInfected = true; //for scan-only mode
                                        Program.LL.LogWarnMessage("_MaliciousEntry", line);
                                    }
                                }
                            }
                        }
                    }

                    if (linesToDelete.Count > 0)
                    {
                        if (!Program.ScanOnly)
                        {
                            HostsDeletionForm form = new HostsDeletionForm(linesToDelete)
                            {
                                TopMost = true
                            };

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                List<string> selectedLinesToDelete = form.GetSelectedLinesToDelete();

                                if (selectedLinesToDelete.Count != 0)
                                {
                                    foreach (var line in selectedLinesToDelete)
                                    {
                                        deletedLineCount++;
                                        Program.LL.LogWarnMessage("_SuspiciousEntry", line);
                                        lines.Remove(line);
                                    }

                                    File.WriteAllLines(hostsPath_tmp, lines);

                                }
                            }
                            else
                            {
                                linesToDelete.Clear();
                            }
                        }
                    }

                    if (deletedLineCount > 0 || isHostsInfected == true)
                    {
                        Program.totalFoundThreats++;
                        if (!Program.ScanOnly)
                        {
                            File.WriteAllLines(hostsPath_tmp, lines);
                            if (File.Exists(hostsPath_tmp))
                            {
                                UnlockObjectClass.ResetObjectACL(hostsPath_full);
                                File.SetAttributes(hostsPath_full, FileAttributes.Normal);
                                File.Copy(hostsPath_tmp, hostsPath_full, true);
                            }
                            Program.LL.LogSuccessMessage("_HostsFileRecovered", deletedLineCount.ToString());
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Cured));

                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Skipped));
                            LocalizedLogger.LogScanOnlyMode();
                        }

                    }
                    else if (!Program.ScanOnly)
                    {
                        LocalizedLogger.LogNoThreatsFound();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Program.totalNeutralizedThreats--;
                    string message = Program.LL.GetLocalizedString("_ErrorLockedFile").Replace("#file#", hostsPath_full);
                    Logger.WriteLog($"\t[!!!] {message}", Logger.caution);
                    scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Inaccassible));
                }
                catch (Exception e)
                {
                    Program.totalNeutralizedThreats--;
                    Program.LL.LogErrorMessage("_ErrorCleanHosts", e);
                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Error));
                }

                foreach (string tmpFile in Directory.GetFiles(Path.GetDirectoryName(hostsPath_tmp), "hosts*", SearchOption.TopDirectoryOnly))
                {
                    File.Delete(tmpFile);
                }
                MSData.Instance.hStrings.Clear();
            }
        }

        void ScanRegistry()
        {
            Program.LL.LogHeadMessage("_ScanRegistry");

            int affected_items = 0;

            #region DisallowRun
            Logger.WriteLog(@"[Reg] Dis?allo?wRun...".Replace("?", ""), ConsoleColor.DarkCyan);
            try
            {
                RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["ExplorerPolicies"], true);
                if (DisallowRunKey != null)
                {
                    if (DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                    {
                        Program.totalFoundThreats++;
                        Program.LL.LogWarnMessage("_SuspiciousRegKey", "D?isa?llo?wRun");

                        if (!Program.ScanOnly)
                        {
                            DisallowRunKey.DeleteValue("Dis?allo?wRun".Replace("?", ""));
                            if (!DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                            {
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", "Dis?allo?wRun");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, "Registry:Dis?allo?wRun".Replace("?", ""), ScanActionType.Deleted));

                                affected_items++;
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, "Registry:Dis?allo?wRun".Replace("?", ""), ScanActionType.Skipped));
                        }

                    }

                    if (!Program.ScanOnly)
                    {
                        RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["ExplorerDisallowRun"], true);
                        if (DisallowRunSub != null)
                        {
                            Program.totalFoundThreats++;
                            DisallowRunKey.DeleteSubKeyTree("Di?sall?owR?un".Replace("?", ""));
                            DisallowRunSub = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["ExplorerDisallowRun"], true);
                            if (DisallowRunSub == null)
                            {
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", "Dis?allo?wRun (hive)");

                                affected_items++;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\Explorer");
                scanResults.Add(new ScanResult(ScanObjectType.Malware, "Registry:Dis?allo?wRun".Replace("?", ""), ScanActionType.Error));

            }

            #endregion

            #region Appinit_dlls
            Logger.WriteLog(@"[Reg] AppInitDLL...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["WindowsNT_CurrentVersion_Windows"], true);
                if (appinit_key != null)
                {
                    string appInitDllsValue = appinit_key.GetValue("AppI?nit?_DLLs".Replace("?", "")).ToString();
                    if (!string.IsNullOrEmpty(appInitDllsValue))
                    {
                        if (appinit_key.GetValue("Loa??dApp??Init_DLLs".Replace("?", "")).ToString() == "1")
                        {
                            if (!appinit_key.GetValueNames().Contains("RequireSignedApp?Ini?t_D?LLs".Replace("?", "")))
                            {
                                Program.LL.LogWarnMessage("_AppInitNotEmpty");
                                Program.LL.LogMessage("\t\t[.]", "_File", appInitDllsValue, ConsoleColor.White, false);
                                Program.LL.LogCautionMessage("_SignedAppInitNotFound");
                                Program.totalFoundThreats++;

                                if (!Program.ScanOnly)
                                {
                                    appinit_key.SetValue("RequireSignedApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                    if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                    {
                                        Program.LL.LogSuccessMessage("_ValueWasCreated");

                                        affected_items++;
                                    }
                                }
                            }
                            else if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "0")
                            {
                                Program.LL.LogWarnMessage("_AppInitNotEmpty");
                                Program.LL.LogMessage("\t[.]", "_File", appInitDllsValue, ConsoleColor.White, false);
                                Program.LL.LogCautionMessage("_SignedAppInitValue");
                                Program.totalFoundThreats++;

                                if (!Program.ScanOnly)
                                {
                                    appinit_key.SetValue("Re?qu?ireSigne?dApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                    if (appinit_key.GetValue("Requi????reSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                    {
                                        Program.LL.LogSuccessMessage("_ValueSetTo1");

                                        affected_items++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region IFEO
            Logger.WriteLog(@"[Reg] IFEO...", ConsoleColor.DarkCyan);

            try
            {
                RegistryKey IFEOKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["IFEO"], true);

                if (IFEOKey != null)
                {
                    foreach (string subKeyName in IFEOKey.GetSubKeyNames())
                    {
                        try
                        {
                            using (RegistryKey subKey = IFEOKey.OpenSubKey(subKeyName, true))
                            {
                                if (subKey != null)
                                {
                                    object globalFlagValue = subKey.GetValue("GlobalFlag");
                                    if (globalFlagValue != null && globalFlagValue is int && (int)globalFlagValue == 0x200)
                                    {
                                        Program.totalFoundThreats++;

                                        if (!Program.ScanOnly)
                                        {
                                            subKey.DeleteValue("GlobalFlag");
                                            if (subKey.GetValue("GlobalFlag") == null)
                                            {
                                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"SilentExit flag: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}".Replace("?", ""), ScanActionType.Deleted));

                                                affected_items++;
                                            }
                                        }

                                    }


                                    object DebbuggerValue = subKey.GetValue("deb?ug?ger".Replace("?", ""));
                                    if (DebbuggerValue != null && DebbuggerValue is string @string)
                                    {
                                        string _dValue = @string;

                                        if (Utils.ShouldRemoveDebugger(_dValue))
                                        {
                                            Program.totalFoundThreats++;

                                            if (!Program.ScanOnly)
                                            {
                                                subKey.DeleteValue("deb?ug?ger".Replace("?", ""));
                                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Deleted));
                                            }
                                        }
                                    }

                                    object mmStackValue = subKey.GetValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                    if (mmStackValue != null && mmStackValue is int @int)
                                    {
                                        int _dvalue = @int;
                                        if (_dvalue > 32768)
                                        {
                                            if (!Program.ScanOnly)
                                            {
                                                subKey.DeleteValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Deleted));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (SecurityException se)
                        {
#if DEBUG
                            Console.WriteLine($"[DBG] IFEO {subKeyName} : {se.Message}");
#endif
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "IFEO");
            }

            #endregion

            #region IFEO_Wow6432
            Logger.WriteLog(@"[Reg] IFEO WOW6432...", ConsoleColor.DarkCyan);

            try
            {
                RegistryKey IFEOKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["Wow6432Node_IFEO"], true);

                if (IFEOKey != null)
                {
                    foreach (string subKeyName in IFEOKey.GetSubKeyNames())
                    {
                        try
                        {
                            RegistryKey subKey = IFEOKey.OpenSubKey(subKeyName, true);

                            if (subKey != null)
                            {
                                object globalFlagValue = subKey.GetValue("GlobalFlag");
                                if (globalFlagValue != null && globalFlagValue is int && (int)globalFlagValue == 0x200)
                                {
                                    Program.totalFoundThreats++;
                                    if (!Program.ScanOnly)
                                    {
                                        subKey.DeleteValue("GlobalFlag");
                                        if (subKey.GetValue("GlobalFlag") == null)
                                        {
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"SilentExit flag: {subKeyName}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}".Replace("?", ""), ScanActionType.Deleted));

                                            affected_items++;
                                        }
                                    }

                                }


                                object DebbuggerValue = subKey.GetValue("deb?ug?ger".Replace("?", ""));
                                if (DebbuggerValue != null && DebbuggerValue is string @string)
                                {
                                    string _dValue = @string;
                                    if (Utils.ShouldRemoveDebugger(_dValue))
                                    {
                                        Program.totalFoundThreats++;

                                        if (!Program.ScanOnly)
                                        {
                                            subKey.DeleteValue("deb?ug?ger".Replace("?", ""));
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Deleted));
                                        }
                                    }
                                }

                                object mmStackValue = subKey.GetValue("Min?im?umSta?ckC?om?mit?InB?yte?s".Replace("?", ""));
                                if (mmStackValue != null && mmStackValue is int @int)
                                {
                                    int _dvalue = @int;
                                    if (_dvalue > 32768)
                                    {
                                        if (!Program.ScanOnly)
                                        {
                                            subKey.DeleteValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Deleted));
                                        }
                                    }
                                }
                            }
                        }
                        catch (SecurityException se)
                        {
#if DEBUG
                            Console.WriteLine($"[DBG] IFEO {subKeyName} : {se.Message}");
#endif
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "IFEO");

            }
            #endregion

            #region SilentExitCheck
            Logger.WriteLog(@"[Reg] Silent_Exit_Process...", ConsoleColor.DarkCyan);

            try
            {
                RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["SilentProcessExit"], writable: true);

                if (baseKey != null)
                {
                    foreach (string subKeyName in baseKey.GetSubKeyNames())
                    {
                        try
                        {
                            using (RegistryKey subKey = baseKey.OpenSubKey(subKeyName))
                            {
                                if (subKey != null)
                                {
                                    object monitorProcessValue = subKey.GetValue("MonitorProcess");

                                    if (monitorProcessValue != null)
                                    {
                                        Program.totalFoundThreats++;
                                        baseKey.DeleteSubKey(subKeyName);
                                        Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"MonitorProcess: {subKeyName} -> {monitorProcessValue}");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"MonitorProcess: {subKeyName} -> {monitorProcessValue}", ScanActionType.Deleted));
                                    }
                                }
                            }
                        }
                        catch (SecurityException se)
                        {
#if DEBUG
                            Console.WriteLine($"[DBG] Silent_Exit_Process {subKeyName} : {se.Message}");
#endif
                            continue;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "Silent_Exit_process");
            }
            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["StartupRun"], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"[Reg] HKLM Autorun...", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();

                    foreach (string value in RunKeys)
                    {
                        string path = FileSystemManager.ResolveFilePathFromString(AutorunKey, value);
                        if (string.IsNullOrEmpty(path))
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Program.LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");
                        }

                        string AutorunKeyValue = AutorunKey.GetValue(value).ToString();
                        if (AutorunKeyValue.IndexOf("Re??a?l?te?kH?D\\t?ask".Replace("?", ""), StringComparison.InvariantCultureIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("Re?aI?te??kHD\\tas?k".Replace("?", ""), StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            Program.totalFoundThreats++;

                            string valuename = value;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", valuename);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKLM:Run: Rea?ltek H?D A?udio".Replace("?", ""), ScanActionType.Deleted));

                                affected_items++;
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", valuename);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKLM:Run: Rea?ltek H?D A?udio".Replace("?", ""), ScanActionType.Skipped));

                            }

                        }


                        if (AutorunKeyValue.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.totalFoundSuspiciousObjects++;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Skipped));

                            }
                        }

                        if ((AutorunKeyValue.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && (AutorunKeyValue.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0)))
                        {
                            Program.totalFoundThreats++;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                if (AutorunKey.GetValue(value) == null)
                                {
                                    Program.LL.LogSuccessMessage("_RegistryKeyRemoved", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Deleted));
                                    affected_items++;
                                }
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Skipped));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKLM\\...\\run");
            }

            #endregion

            #region HKCU
            Logger.WriteLog(@"[Reg] HKCU Autorun...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["StartupRun"], true);
                if (AutorunKey != null)
                {

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = FileSystemManager.ResolveFilePathFromString(AutorunKey, value);
                        string AutorunKeyValue = AutorunKey.GetValue(value).ToString();
                        if (string.IsNullOrEmpty(path))
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Program.LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKeyValue} | {value}");

                        }

                        if (AutorunKeyValue.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.totalFoundSuspiciousObjects++;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKCU:Run: {value} -> {AutorunKeyValue}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKCU:Run: {value} -> {AutorunKeyValue}", ScanActionType.Skipped));

                            }
                        }

                        if ((AutorunKeyValue.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && (AutorunKeyValue.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0)))
                        {
                            Program.totalFoundThreats++;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                if (AutorunKey.GetValue(value) == null)
                                {
                                    Program.LL.LogSuccessMessage("_RegistryKeyRemoved", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKCU:Run: {value} -> {AutorunKeyValue}", ScanActionType.Deleted));
                                    affected_items++;
                                }
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKCU:Run: {value} -> {AutorunKeyValue}", ScanActionType.Skipped));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\run");
            }


            Logger.WriteLog(@"[Reg] HKCU System policies...", ConsoleColor.DarkCyan);
            try
            {
                string regPath = MSData.Instance.queries["SystemPolicies"];
                string[] valueNames = { "Di?sable?TaskM?gr".Replace("?", ""), "Di??sab?leRe?gistr??yToo?ls".Replace("?", "") };

                using (RegistryKey systemKey = Registry.CurrentUser.OpenSubKey(regPath, true))
                {
                    if (systemKey != null)
                    {
                        foreach (string policiesVal in valueNames)
                        {
                            if (systemKey.GetValue(policiesVal) != null)
                            {
                                Program.totalFoundThreats++;
                                if (!Program.ScanOnly)
                                {
                                    systemKey.DeleteValue(policiesVal, false);
                                    if (systemKey.GetValue(policiesVal) == null)
                                    {
                                        Program.LL.LogSuccessMessage("_RegistryKeyRemoved", policiesVal);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKCU:SystemPolicies: {policiesVal}", ScanActionType.Deleted));
                                        affected_items++;
                                    }
                                    else
                                    {
                                        Program.totalNeutralizedThreats--;
                                        Program.LL.LogErrorMessage("_ErrorCannotRemove", null, policiesVal);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKCU:SystemPolicies: {policiesVal}", ScanActionType.Inaccassible));
                                    }

                                }
                                else
                                {
                                    Program.LL.LogWarnMediumMessage("_FoundMlwrKey", policiesVal);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"HKCU:SystemPolicies: {policiesVal}", ScanActionType.Skipped));
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\System");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("tek").Append("to").Append("nit");
            string subkeyNameTektonit = sb.ToString();

            try
            {

                Logger.WriteLog($"[Reg] {subkeyNameTektonit}...", ConsoleColor.DarkCyan);

                RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(@"Software", true);

                if (tektonit.GetSubKeyNames().Contains(subkeyNameTektonit))
                {
                    Program.LL.LogWarnMessage("_SuspiciousRegKey", subkeyNameTektonit);
                    Program.totalFoundThreats++;

                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            if (UnlockObjectClass.IsRegistryKeyBlocked(Path.Combine("Software", subkeyNameTektonit)))
                            {
                                UnlockObjectClass.UnblockRegistry(Path.Combine("Software", subkeyNameTektonit));
                            }
                        }
                        catch (SecurityException)
                        {
                            UnlockObjectClass.TakeownRegKey(Path.Combine(@"Software", subkeyNameTektonit));
                            UnlockObjectClass.ResetPermissionsToDefault(Path.Combine(@"Software", subkeyNameTektonit));
                            UnlockObjectClass.UnblockRegistry(Path.Combine("Software", subkeyNameTektonit));
                        }


                        if (tektonit.GetSubKeyNames().Contains(subkeyNameTektonit))
                        {
                            var tektonitSubKeys = tektonit.GetSubKeyNames();
                            if (tektonitSubKeys != null)
                            {
                                tektonit.DeleteSubKeyTree(subkeyNameTektonit);
                            }
                            else tektonit.DeleteSubKey(subkeyNameTektonit);

                            if (!tektonit.GetSubKeyNames().Contains(subkeyNameTektonit))
                            {
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", subkeyNameTektonit);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Registry:HKCU:{subkeyNameTektonit}".Replace("?", ""), ScanActionType.Deleted));

                                affected_items++;
                            }
                        }
                    }
                    else
                    {
                        Program.LL.LogWarnMediumMessage("_FoundMlwrKey", subkeyNameTektonit);
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Registry:HKCU:{subkeyNameTektonit}".Replace("?", ""), ScanActionType.Skipped));
                    }
                }
            }
            catch (Exception ex)
            {
                Program.totalNeutralizedThreats--;
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, $"HKCU\\...\\{subkeyNameTektonit}");
                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Registry:HKCU:{subkeyNameTektonit}".Replace("?", ""), ScanActionType.Error));

            }

            #endregion

            #region Applocker

            Logger.WriteLog(@"[Reg] Applocker...", ConsoleColor.DarkCyan);
            string registryPath = MSData.Instance.queries["appl0cker"];

            bool isContainsBadPolicies = false;

            List<string> allSubkeys = Program._utils.GetSubkeys(registryPath);

            if (allSubkeys.Count > 0)
            {
                using (RegistryKey parentKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, true))
                {
                    foreach (var subkeyName in allSubkeys)
                    {
                        if (MSData.Instance.badSubkeys.Contains(subkeyName, StringComparer.OrdinalIgnoreCase))
                        {
                            isContainsBadPolicies = true;
                            try
                            {
                                parentKey.DeleteSubKeyTree(subkeyName);
                                if (!Utils.RegistryKeyExists(registryPath))
                                {
                                    Program.LL.LogSuccessMessage("_RegistryKeyRemoved", subkeyName);
                                    affected_items++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, subkeyName);
                            }
                        }
                    }

                    if (isContainsBadPolicies)
                    {
                        Program.totalFoundThreats++;
                        scanResults.Add(new ScanResult(ScanObjectType.Infected, $"Reg?istry:Appl?ocker".Replace("?", ""), ScanActionType.Cured));
                    }
                }
            }

            #endregion

            #region WindowsDefender
            StringBuilder sbWD = new StringBuilder("Re").Append("gi").Append("st").Append("ry").Append(":W").Append("in").Append("De").Append("fe").Append("nd").Append(" Ex").Append("cl").Append("us").Append("io").Append("ns");
            Logger.WriteLog($"[Reg] {new StringBuilder("Wi").Append("nd").Append("ow").Append("s ").Append("De").Append("fe").Append("nd").Append("er")}...".Replace("~", ""), ConsoleColor.DarkCyan);

            try
            {
                string[] baseKeys =
                {
                    MSData.Instance.queries["WDExclusionsLocal"],
                    MSData.Instance.queries["WDExclusionsPolicies"]
                };

                string[] subKeys = { "Paths", "Processes", "Extensions" };

                foreach (string baseKey in baseKeys)
                {
                    RegistryKey winDefenderKey = baseKey == baseKeys[0] ? Registry.LocalMachine.OpenSubKey(baseKey) : Registry.LocalMachine.OpenSubKey(baseKey, true);
                    if (winDefenderKey == null) continue;

                    foreach (string subKey in subKeys)
                    {
                        string fullKeyPath = baseKey + "\\" + subKey;
                        RegistryKey key = baseKey == baseKeys[0] ? Registry.LocalMachine.OpenSubKey(fullKeyPath) : Registry.LocalMachine.OpenSubKey(fullKeyPath, true);
                        if (key == null) continue;

                        string[] valueNames = key.GetValueNames();
                        List<string> checkList;

                        if (subKey == "Processes")
                            checkList = MSData.Instance.obfStr4;
                        else if (subKey == "Extensions")
                            checkList = new List<string> { ".exe", ".tmp" };
                        else
                            checkList = MSData.Instance.obfStr3;

                        foreach (string valueName in valueNames)
                        {

                            if (checkList.Contains(valueName, StringComparer.OrdinalIgnoreCase))
                            {
                                Program.totalFoundThreats++;

                                try
                                {
                                    if (!Program.ScanOnly)
                                    {
                                        if (baseKey == MSData.Instance.queries["WDExclusionsLocal"])
                                        {
                                            Utils.RemoveDefenderExclusion(subKey, valueName);
                                            if (key.GetValue(valueName) == null)
                                            {
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, sbWD + " (Local) -> " + valueName, ScanActionType.Deleted));
                                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{valueName} (local)");
                                                affected_items++;
                                            }
                                            else
                                            {
                                                Program.totalNeutralizedThreats--;
                                                Program.LL.LogErrorMessage("_ErrorCannotRemove", new Exception($"{sbWD} is turned off possible"), $"{valueName} (local)");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, sbWD + " (Local) -> " + valueName, ScanActionType.Inaccassible));
                                            }
                                        }
                                        else
                                        {
                                            key.DeleteValue(valueName);
                                            if (key.GetValue(valueName) == null)
                                            {
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, sbWD + " (Policies) -> " + valueName, ScanActionType.Deleted));
                                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"{valueName} (policy)");
                                                affected_items++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Program.LL.LogWarnMediumMessage("_FoundMlwrKey", valueName);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, sbWD + " -> " + valueName, ScanActionType.Skipped));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Program.totalNeutralizedThreats--;
                                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, valueName);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, sbWD + " -> " + valueName, ScanActionType.Error));
                                }
                            }

                        }

                        key.Close();
                    }

                    winDefenderKey.Close();
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["Wow6432Node_StartupRun"], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"[Reg] Wow64Node Autorun...", ConsoleColor.DarkCyan);

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = FileSystemManager.ResolveFilePathFromString(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Program.LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "WOW6432?Node\\...\\run");
            }
            #endregion

            if (affected_items == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }

            if (Program.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }
        void ScanTaskScheduler()
        {
            string[] checkDirectories =
            {
                Environment.SystemDirectory, // System32
                new StringBuilder(Program.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("WO").Append("W6").Append("4").ToString(),
                new StringBuilder(Program.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("te").Append("m3").Append("2\\").Append("wb").Append("em").ToString(),
                MSData.Instance.queries["PowerShellPath"],
            };

            string[] badArgStrings =
            {
                new StringBuilder("--").Append("al").Append("go").ToString(),
                new StringBuilder("--").Append("co").Append("in").ToString(),
                new StringBuilder("--").Append("pa").Append("ss").Append(" x").ToString(),
                new StringBuilder("st").Append("ra").Append("tu").Append("m+").ToString(),
                new StringBuilder("r").Append("eg").Append(" co").Append("py").ToString(),
            };

            using (TaskService taskService = new TaskService())
            {
                if (taskService.AllTasks == null)
                {
                    Program.LL.LogErrorMessage("TaskService", new Exception("Failed to retrieve tasks."));
                    return;
                }

                var filteredTasks = taskService.AllTasks
                    .Where(task => task != null)
                    .OrderBy(task => task.Name)
                    .ToList();

                foreach (var task in filteredTasks)
                {
                    if (task.Name == null || task.Folder == null || task.Definition == null || task.Definition.Actions == null)
                    {
                        continue;
                    }

                    string taskName = task.Name;
                    string taskFolder = task.Folder.ToString();

                    foreach (ExecAction action in task.Definition.Actions.OfType<ExecAction>())
                    {
                        if (action.Path == null)
                        {
                            continue;
                        }

                        string arguments = action.Arguments ?? string.Empty;
                        string filePath = Environment.ExpandEnvironmentVariables(action.Path.Replace("\"", ""));
                        Program.LL.LogMessage("[#]", "_Scanning", $"{taskName} | {taskFolder}", ConsoleColor.White);

                        if (!Program.ScanOnly)
                        {
                            if (taskName.StartsWith("d?ia?le?r".Replace("?", "")))
                            {
                                Program.LL.LogCautionMessage("_MaliciousEntry", taskName);
                                Program.totalFoundThreats++;
                                try
                                {
                                    taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                    Program.totalNeutralizedThreats--;
                                }
                            }

                            if (!string.IsNullOrEmpty(arguments))
                            {
                                foreach (string arg in badArgStrings)
                                {
                                    if (arguments.IndexOf(arg, StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        Program.LL.LogCautionMessage("_MaliciousEntry", taskName);
                                        Program.totalFoundThreats++;

                                        try
                                        {
                                            taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                            if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                            {
                                                Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                                break;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                            Program.totalNeutralizedThreats--;
                                        }
                                    }
                                }

                                if (arguments.IndexOf("/c reg add ", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    Program.totalFoundThreats++;
                                    try
                                    {
                                        taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                        if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                        {
                                            Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.totalNeutralizedThreats--;
                                        Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                    }
                                }

                                if (arguments.IndexOf("-jar ", StringComparison.OrdinalIgnoreCase) >= 0 && (arguments.EndsWith(".txt\"", StringComparison.OrdinalIgnoreCase) || arguments.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)))
                                {
                                    Program.totalFoundThreats++;
                                    try
                                    {
                                        taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                        if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                        {
                                            Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.totalNeutralizedThreats--;
                                        Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                    }
                                }

                            }


                        }

                        if (!string.IsNullOrEmpty(filePath) && filePath.IndexOf(":\\", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (File.Exists(filePath))
                            {
                                ProcessFilePath(filePath, arguments, taskService, taskFolder, taskName);
                            }
                            else
                            {
                                Program.LL.LogWarnMessage("_FileIsNotFound", filePath);
                                if (Program.RemoveEmptyTasks)
                                {
                                    Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                }
                            }
                        }
                        else
                        {
                            bool fileFound = false;

                            foreach (string checkDir in checkDirectories)
                            {
                                string fullPath = Path.Combine(checkDir, filePath);
                                if (!fullPath.EndsWith(".exe"))
                                {
                                    fullPath += ".exe";
                                }

                                if (File.Exists(fullPath))
                                {
                                    ProcessFilePath(fullPath, arguments, taskService, taskFolder, taskName);
                                    fileFound = true;
                                    break;
                                }
                            }

                            if (!fileFound)
                            {
                                Program.LL.LogWarnMessage("_FileNotExistsSpec", filePath);
                                if (Program.RemoveEmptyTasks)
                                {
                                    Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(arguments))
                        {
                            FileSystemManager.ProcessFileFromArgs(checkDirectories, filePath, arguments);
                        }

                        if (!Program.RemoveEmptyTasks && Utils.IsTaskEmpty(task))
                        {
                            Program.LL.LogWarnMessage("_EmptyTask", taskName);

                            if (!Program.ScanOnly)
                            {
                                Program._utils.DeleteTask(taskService, taskFolder, taskName);
                            }
                        }
                    }
                }
            }
        }


        void ScanWMI()
        {
            Program.LL.LogHeadMessage(@"_WMIHead");

            ServiceHelper.CheckWMI(false);

            try
            {

                ManagementScope scope = new ManagementScope(@"\\.\root\subscription");
                scope.Connect(); // Connect to the namespace

                ObjectQuery query = new ObjectQuery("SELECT * FROM CommandLineEventConsumer");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection results = searcher.Get();

                if (results.Count > 0)
                {
                    Program.totalFoundThreats += results.Count;
                    string wmiObjName = "";
                    string wmiObjpath = "";
                    foreach (ManagementObject obj in results)
                    {
                        wmiObjName = (string)obj["Name"];
                        wmiObjpath = (string)obj["CommandLineTemplate"];
                        Program.LL.LogWarnMediumMessage("_WMIEvent", $"{wmiObjName} -> {wmiObjpath}");
                        obj.Delete();
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"WMIEvent: {wmiObjName} -> {wmiObjpath}", ScanActionType.Deleted));
                        Program.LL.LogSuccessMessage("_WMIEvent", $"{wmiObjName}", "_Deleted");

                    }
                }
                else
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex);
            }

        }

        void ProcessFilePath(string filePath, string arguments, TaskService taskService, string taskFolder, string taskName)
        {
            if (File.Exists(filePath))
            {
                Program.LL.LogMessage("\t[.]", "_Just_File", $"{filePath} {arguments}", ConsoleColor.Gray);

                try
                {

                    if (filePath.IndexOf("powershell", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (arguments.IndexOf(Bfs.Create("gKZ8iLuNHQyRVA79WjU7aoXiqb8RaBszXxkY+xqD1svbXNY1/TjBQs1rAuYNW8a1VfzC9TMVgNg3zIpuaOxzXGIQO+jQOj5W5HHHczm+CIu2x3Jy9gGCXjOu9NG0lF00", "WO7vfK83a1gysefdgmTKUwvH0alCzL+8xqJuf+A8uQI=", "sULHw8R24176yOiD2c9b3Q=="), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return;
                        }

                        if (arguments.Length >= 500 || arguments.Replace("'", "").IndexOf(" -e ", StringComparison.OrdinalIgnoreCase) >= 0 || arguments.Replace("'", "").IndexOf("-encodedcommand", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.LL.LogCautionMessage("_MaliciousEntry", taskName);

                            Program.totalFoundThreats++;
                            if (!Program.ScanOnly)
                            {
                                Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));

                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                            }
                            return;
                        }
                    }

                    if (filePath.IndexOf("msiexec", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        foreach (var argsPart in arguments.Split(' '))
                        {
                            if (argsPart.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            string msiFile = FileSystemManager.ResolveFilePathFromString(argsPart);
                            if (msiFile.IndexOf(":\\", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Program.LL.LogCautionMessage("_MaliciousEntry", taskName);
                                Program.totalFoundThreats++;

                                if (!Program.ScanOnly)
                                {
                                    Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                    Utils.AddToQuarantine(msiFile);

                                }
                                else
                                {
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                                }

                                return;
                            }
                        }

                    }

                    if (filePath.IndexOf(new StringBuilder("for").Append("files").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (arguments.Count(c => c == '^') == 2)
                        {
                            string wsfFile = arguments.Split('^')[1].Remove(0, 1);

                            if (wsfFile.Remove(0, 1).StartsWith(":\\"))
                            {
                                if (File.Exists(wsfFile))
                                {
                                    Program.LL.LogCautionMessage("_MaliciousEntry", taskName);
                                    Program.totalFoundThreats++;

                                    if (!Program.ScanOnly)
                                    {
                                        Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                        Utils.AddToQuarantine(wsfFile);

                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                                    }

                                    return;
                                }
                                else
                                {
                                    Program.LL.LogWarnMessage("_FileIsNotFound", wsfFile);
                                }
                            }

                        }
                    }

                    if (filePath.IndexOf(new StringBuilder("ws").Append("cri").Append("pt").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (!Program.ScanOnly)
                        {
                            try
                            {
                                Program.totalFoundThreats++;
                                taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                {
                                    Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                Program.totalNeutralizedThreats--;
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Error));
                                Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                        }

                    }

                    if (filePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && arguments.Equals("/LHS", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!Program.ScanOnly)
                        {
                            Program.LL.LogCautionMessage("_Malici0usFile", filePath);
                            if (founded_mlwrFiles.Add(filePath))
                            {
                                Program.totalFoundThreats++;
                                UnlockObjectClass.DisableExecute(filePath);
                            }
                            else Program.LL.LogSuccessMessage("_AlreadyProceeded");

                            try
                            {

                                taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                {
                                    Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                Program.totalNeutralizedThreats--;
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                        }
                    }

                    if (filePath.IndexOf("regasm", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (!Program.ScanOnly)
                        {
                            string dllPath = FileSystemManager.ExtractDllPath(arguments);
                            if (!string.IsNullOrEmpty(dllPath) && File.Exists(dllPath))
                            {
                                Program.LL.LogCautionMessage("_Malici0usFile", dllPath);
                                if (founded_mlwrFiles.Add(dllPath))
                                {
                                    Program.totalFoundThreats++;
                                    UnlockObjectClass.DisableExecute(dllPath);
                                }
                                else Program.LL.LogSuccessMessage("_AlreadyProceeded");
                            }

                            try
                            {
                                Program.LL.LogCautionMessage("_Malic1ousTask", taskName);
                                Program.totalFoundThreats++;
                                taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                {
                                    Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                Program.totalNeutralizedThreats--;
                                Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                        }
                    }

                    if (winTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success)
                    {
                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                        return;
                    }

                    if (filePath.StartsWith(Environment.GetEnvironmentVariable("AppData"), StringComparison.OrdinalIgnoreCase))
                    {
                        if (arguments.Equals(new StringBuilder("/v").Append("er").Append("ys").Append("il").Append("en").Append("t").ToString(), StringComparison.OrdinalIgnoreCase) || (FileChecker.IsExecutable(filePath) && string.IsNullOrEmpty(Path.GetExtension(filePath))))
                        {
                            if (!Program.ScanOnly)
                            {
                                Program.LL.LogCautionMessage("_Malici0usFile", filePath);
                                if (founded_mlwrFiles.Add(filePath))
                                {
                                    Program.totalFoundThreats++;
                                    UnlockObjectClass.DisableExecute(filePath);
                                }
                                else Program.LL.LogSuccessMessage("_AlreadyProceeded");

                                try
                                {

                                    taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                    }

                                    return;
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                    Program.totalNeutralizedThreats--;
                                }
                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                            }

                        }

                    }



                    long fileSize = new FileInfo(filePath).Length;
                    if (fileSize >= maxFileSize || FileChecker.IsJarFile(filePath) || (FileChecker.IsDotNetAssembly(filePath) && FileSystemManager.HasHiddenAttribute(filePath)))
                    {
                        if (!Program.ScanOnly)
                        {
                            Program.LL.LogCautionMessage("_Malici0usFile", filePath);
                            if (founded_mlwrFiles.Add(filePath))
                            {
                                Program.totalFoundThreats++;
                                UnlockObjectClass.DisableExecute(filePath);
                            }
                            else Program.LL.LogSuccessMessage("_AlreadyProceeded");

                            try
                            {
                                taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                {
                                    Program.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                Program.totalNeutralizedThreats--;
                                Program.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                        }
                    }

                    if (FileChecker.IsSfxArchive(filePath))
                    {
                        Program.LL.LogWarnMediumMessage("_sfxArchive", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            Program.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else Program.LL.LogSuccessMessage("_AlreadyProceeded");

                        return;
                    }

                    if (FileChecker.CheckSignature(filePath, MSData.Instance.signatures) || FileChecker.CheckDynamicSignature(filePath, 16, 100))
                    {
                        Program.LL.LogCautionMessage("_Found", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            Program.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else Program.LL.LogSuccessMessage("_AlreadyProceeded");

                        return;
                    }

                    int filesCount = 0;
                    foreach (string file in Directory.EnumerateFiles(Path.GetDirectoryName(filePath), "*.*", SearchOption.TopDirectoryOnly))
                    {
                        filesCount++;
                    }

                    if (filesCount.Equals(1) && FileSystemManager.HasHiddenAttribute(filePath) && fileSize > 1024 * 1024)
                    {
                        Program.LL.LogMessage("\t[!]", "_FileSize", $"{FileChecker.GetFileSize(fileSize)} | {Program.LL.GetLocalizedString("_FileAttributes")} \"{File.GetAttributes(filePath)}\"", Logger.warn);
                        Program.LL.LogCautionMessage("_Found", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            Program.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else Program.LL.LogSuccessMessage("_AlreadyProceeded");
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    Program.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
                }
                catch (Exception ex)
                {
                    Program.LL.LogErrorMessage("_Error", ex);
                }
            }
            else
            {
                Program.LL.LogWarnMessage("_FileIsNotFound", filePath);

                if (Program.RemoveEmptyTasks)
                {
                    Program._utils.DeleteTask(taskService, taskFolder, taskName);
                }
            }
        }

        public void ScanServices()
        {
            Program.LL.LogHeadMessage("_ScanServices");

            Dictionary<ServiceController, SuspiciousServiceInfo> suspiciousServiceList = new Dictionary<ServiceController, SuspiciousServiceInfo>();
            HashSet<string> suspiciousServiceDlls = new HashSet<string>() { };
            HashSet<string> trustedPaths = new HashSet<string>();
            string registryPath = new StringBuilder("SY").Append("ST").Append("EM").Append("\\C").Append("ur").Append("re").Append("nt").Append("Co").Append("nt").Append("ro").Append("lS").Append("et").Append("\\S").Append("er").Append("vi").Append("ce").Append("s").ToString();

            Regex nameRegex = new Regex(@"^[a-zA-Z]{8}$");
            Regex pathRegex = new Regex(@"^(\\\\\?\\)?[a-fA-F]:\\ProgramData\\[a-zA-Z]{12}\\[a-zA-Z]{12}\.exe$");


            // Get all services
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                string serviceName = service.ServiceName;
                SuspiciousServiceInfo svcinfo = new SuspiciousServiceInfo();

                try
                {
                    if (NativeServiceController.IsServiceMarkedToDelete(serviceName))
                    {
                        continue;
                    }

                    ServiceControllerStatus status = service.Status;

                    string servicePathWithArgs = NativeServiceController.GetServiceImagePath(serviceName);
                    string servicePath = FileSystemManager.ResolveFilePathFromString(servicePathWithArgs);

                    Program.LL.LogMessage("[.]", "_ServiceName", serviceName, ConsoleColor.White);
                    Program.LL.LogMessage("[.]", "_Just_Service", servicePathWithArgs, ConsoleColor.White);
                    Program.LL.LogMessage("[.]", "_State", status.ToString(), ConsoleColor.White);

                    if (File.Exists(servicePath))
                    {
                        FileInfo fi = new FileInfo(servicePath);
                        Program.LL.LogMessage("[.]", "_FileSize", FileChecker.GetFileSize(fi.Length), ConsoleColor.White);
                        if (!trustedPaths.Contains(servicePath))
                        {
                            WinVerifyTrustResult servicePathSignature = winTrust.VerifyEmbeddedSignature(Environment.ExpandEnvironmentVariables(servicePath));
                            if (servicePathSignature != WinVerifyTrustResult.Success && servicePathSignature != WinVerifyTrustResult.Error)
                            {
                                if (fi.Length >= maxFileSize || (nameRegex.IsMatch(serviceName) && pathRegex.IsMatch(servicePath)) || FileChecker.CheckSignature(servicePath, MSData.Instance.signatures) || FileChecker.CheckDynamicSignature(servicePath, 2096, startSequence, endSequence) || FileChecker.CheckDynamicSignature(servicePath, 16, 100))
                                {
                                    Program.LL.LogCautionMessage("_Found", $"{service.ServiceName} {servicePath}");

                                    svcinfo.FilePath = servicePath;
                                    svcinfo.IsMlwrSignature = true;

                                    suspiciousServiceList.Add(service, svcinfo);
                                    UnlockObjectClass.DisableExecute(servicePath);
                                }
                            }
                            else if (servicePathSignature == WinVerifyTrustResult.Success)
                            {
                                if (NativeServiceController.GetServiceStartType(serviceName) == NativeServiceController.ServiceStartMode.Disabled)
                                {
                                    Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                                    continue;
                                }

                                string serviceDir = Path.GetDirectoryName(servicePath);
                                if (Directory.Exists(serviceDir))
                                {
                                    foreach (string dll in Directory.GetFiles(serviceDir, "*.dll"))
                                    {
                                        string dllName = Path.GetFileName(dll);
                                        if (Array.Exists(MSData.Instance.sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                                        {
                                            WinVerifyTrustResult dllSignature = winTrust.VerifyEmbeddedSignature(dll);

                                            if (dllSignature != WinVerifyTrustResult.Success)
                                            {
                                                Program.LL.LogCautionMessage("_Found", Program.LL.GetLocalizedString("_ValidServiceDLLHijacking").Replace("#SERVICENAME#", service.ServiceName).Replace("#DLLNAME#", dll));

                                                svcinfo.FilePath = servicePath;
                                                svcinfo.HasHijackedDll = true;

                                                suspiciousServiceList.Add(service, svcinfo);
                                                suspiciousServiceDlls.Add(dll);
                                                UnlockObjectClass.DisableExecute(dll);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!svcinfo.HasHijackedDll)
                            {
                                trustedPaths.Add(servicePath);
                                Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                            }
                        }

                        using (RegistryKey servicesKey = Registry.LocalMachine.OpenSubKey(registryPath))
                        {
                            if (servicesKey != null)
                            {
                                using (RegistryKey serviceKey = servicesKey.OpenSubKey(serviceName + @"\Parameters"))
                                {
                                    if (serviceKey != null)
                                    {
                                        object serviceDllValue = serviceKey.GetValue("ServiceDll");
                                        if (serviceDllValue != null)
                                        {
                                            string serviceDll = Environment.ExpandEnvironmentVariables(serviceDllValue.ToString());
                                            Logger.WriteLog($"[.] ServiceDll: {serviceDll}", ConsoleColor.White);
                                            if (File.Exists(serviceDll))
                                            {
                                                WinVerifyTrustResult serviceDllSignature = winTrust.VerifyEmbeddedSignature(serviceDll);
                                                if (serviceDllSignature != WinVerifyTrustResult.Success && serviceDllSignature != WinVerifyTrustResult.SubjectCertExpired && serviceDllSignature != WinVerifyTrustResult.Error)
                                                {
                                                    Program.LL.LogCautionMessage("_Found", $"{service.ServiceName} {serviceDll}");

                                                    svcinfo.FilePath = servicePath;
                                                    svcinfo.HasHijackedDll = true;

                                                    suspiciousServiceList.Add(service, svcinfo);
                                                    suspiciousServiceDlls.Add(serviceDll);
                                                }
                                                else
                                                {
                                                    Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                                                }
                                            }
                                            else
                                            {
                                                Program.LL.LogWarnMessage("_FileIsNotFound", serviceDll);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Program.LL.LogWarnMessage("_ServiceFileIsNotFound");
                    }
                }
                catch (Exception ex)
                {
                    Program.LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                }

                Logger.WriteLog("------------", ConsoleColor.White, true, true);
            }

            if (!Program.ScanOnly)
            {
                if (suspiciousServiceList.Count >= 5)
                {
                    string suspiciousServicesNames = string.Join(Environment.NewLine, suspiciousServiceList.Select(s => s.Key.DisplayName));
                    string message = Program.LL.GetLocalizedString("_DisableServiceWarning");

                    DialogResult result = DialogResult.Cancel;
                    if (!Program.silent)
                    {
                        result = MessageBoxCustom.Show(message.Replace("#COUNT#", suspiciousServiceList.Count.ToString()), Program._title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }

                    if (result == DialogResult.Yes)
                    {
                        foreach (var suspiciousService in suspiciousServiceList)
                        {
                            DisableService(suspiciousService.Key, suspiciousService.Value, suspiciousServiceDlls);
                        }
                    }
                    else
                    {
                        foreach (var suspiciousService in suspiciousServiceList)
                        {
                            Program.LL.LogMessage("[i]", "_SkipServiceMessage", suspiciousService.Key.DisplayName, ConsoleColor.White);
                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{Program.LL.GetLocalizedString("_Just_Service")} { suspiciousService.Key.ServiceName }", ScanActionType.Skipped));
                        }
                    }
                }
                else
                {
                    foreach (var suspiciousService in suspiciousServiceList)
                    {
                        DisableService(suspiciousService.Key, suspiciousService.Value, suspiciousServiceDlls);
                    }
                }
            }
            else
            {
                LocalizedLogger.LogScanOnlyMode();
                foreach (var suspiciousService in suspiciousServiceList)
                {
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{Program.LL.GetLocalizedString("_Just_Service")} { suspiciousService.Key.ServiceName }", ScanActionType.Skipped));
                }
            }
        }

        void DisableService(ServiceController service, SuspiciousServiceInfo serviceInfo, HashSet<string> serviceDlls = null)
        {

            string serviceName = service.ServiceName;

            var startMode = NativeServiceController.GetServiceStartType(serviceName);
            ServiceControllerStatus status = service.Status;

            if (startMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                Program.totalFoundThreats++;
                try
                {
                    NativeServiceController.SetServiceStartType(service.ServiceName, NativeServiceController.ServiceStartMode.Disabled);
                    if (NativeServiceController.GetServiceStartType(service.ServiceName) == NativeServiceController.ServiceStartMode.Disabled)
                    {
                        Program.LL.LogSuccessMessage("_ServiceDisabled", serviceName);
                        scanResults.Add(new ScanResult(ScanObjectType.Suspicious, Program.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceInfo.FilePath, ScanActionType.Disabled));
                    }
                }
                catch (Win32Exception w32e)
                {
                    if (serviceDlls != null)
                    {
                        Program.totalNeutralizedThreats--;
                    }
                    Program.LL.LogErrorMessage("_ErrorCannotProceed", w32e, service.ServiceName, "_Service");
                }

            }

            if (status == ServiceControllerStatus.Running)
            {
                try
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                    Program.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                }
                catch (InvalidOperationException)
                {
                    try
                    {
                        int ServicePID = NativeServiceController.GetServiceId(service.ServiceName);
                        ProcessManager.UnProtect(new int[] { ServicePID });
                        Process serviceProcess = Process.GetProcessById(ServicePID);

                        string args = ProcessManager.GetProcessCommandLine(serviceProcess);
                        if (args != null && args.IndexOf($"-s {service.ServiceName}", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ProcessManager.UnProtect(new int[] { serviceProcess.Id });
                            serviceProcess.Kill();
                            status = ServiceControllerStatus.Stopped;
                            Program.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_ErrorCannotProceed", ex, service.ServiceName, "_Service");
                    }
                }
            }

            Thread.Sleep(2000);
            startMode = NativeServiceController.GetServiceStartType(service.ServiceName);
            status = service.Status;
            if (startMode != NativeServiceController.ServiceStartMode.Disabled || status != ServiceControllerStatus.Stopped)
            {
                //Program.totalNeutralizedThreats--;
                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{Program.LL.GetLocalizedString("_Just_Service")} { service.ServiceName }", ScanActionType.Error));
            }

            if (serviceDlls != null)
            {
                if (serviceDlls.Count != 0)
                {
                    foreach (string serviceDll in serviceDlls)
                    {
                        if (File.Exists(serviceDll))
                        {
                            Utils.AddToQuarantine(serviceDll);
                            if (!File.Exists(serviceDll))
                            {
                                Program.totalFoundThreats++;
                            }
                            else Program.totalNeutralizedThreats--;
                        }
                    }
                }
            }

            if (serviceInfo.IsMlwrSignature)
            {

                try
                {
                    try
                    {
                        if (File.Exists(serviceInfo.FilePath))
                        {
                            UnlockObjectClass.KillAndDelete(serviceInfo.FilePath);
                            if (!File.Exists(serviceInfo.FilePath))
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, serviceInfo.FilePath, ScanActionType.Deleted));
                            }
                        }
                    }
                    catch (IOException)
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, serviceInfo.FilePath, ScanActionType.Error));
                    }
                    catch (UnauthorizedAccessException)
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, serviceInfo.FilePath, ScanActionType.Error));
                    }


                    if (service != null)
                    {
                        ServiceHelper.Uninstall(serviceName);
                        Program.LL.LogSuccessMessage("_MaliciousService", serviceName, "_Deleted");
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceName, ScanActionType.Deleted));

                    }
                }
                catch (Win32Exception win32Ex)
                {
                    if (win32Ex.NativeErrorCode != 1072 && win32Ex.NativeErrorCode != 1060) // ERROR_SERVICE_MARKED_FOR_DELETE
                    {
                        Program.LL.LogErrorMessage("_ErrorCannotRemove", win32Ex, serviceName, "_Service");
                        Program.totalNeutralizedThreats--;
                    }
                }
                catch (Exception ex)
                {
                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, serviceName, "_Service");
                    Program.totalNeutralizedThreats--;
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceInfo.FilePath, ScanActionType.Error));

                }
            }
        }

        public void SignatureScan()
        {
            Program.LL.LogHeadMessage("_StartSignatureScan");
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

#if !DEBUG
            if (!Program.WinPEMode)
            {
                MSData.Instance.obfStr6.Add(Path.GetTempPath());
            }
#endif

            if (!string.IsNullOrEmpty(Program.selectedPath) && Directory.Exists(Program.selectedPath) && !Program.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(Program.selectedPath);
            }

            if (Program.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                var localDrives = allDrives.Where(drive => drive.DriveType == DriveType.Fixed && !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));
                foreach (var drive in localDrives)
                {
                    MSData.Instance.obfStr6.Add(drive.Name);
                }
            }



            MSData.Instance.obfStr6 = MSData.Instance.obfStr6.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            foreach (string path in MSData.Instance.obfStr6)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }


                foreach (var filepath in FileEnumerator.GetFiles(path, "*.exe", 0, Program.maxSubfolders))
                {
                    if (IsMalic1ousFile(FileSystemManager.GetUNCPath(filepath)))
                    {
                        UnlockObjectClass.DisableExecute(FileSystemManager.GetUNCPath(filepath));
                    }
                }

            }

            if (!Program.ScanOnly && founded_mlwrFiles.Count == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }
            else
            {
                if (!Program.ScanOnly)
                {
                    Program.LL.LogWarnMediumMessage("_FoundThreats", founded_mlwrFiles.Count.ToString());
                    Program.LL.LogHeadMessage("_RestartCleanup");
                    CleanFoundedMlwr();
                }
                else
                {
                    CleanFoundedMlwr();
                    LocalizedLogger.LogScanOnlyMode();
                }
            }

        }

        public void CleanFoundedMlwr()
        {
            if (founded_mlwrFiles.Count > 0)
            {
                Program.LL.LogHeadMessage("_RemovingFoundMlwrFiles");

                foreach (string path in founded_mlwrFiles)
                {
                    if (!Program.ScanOnly)
                    {
                        if (File.Exists(path))
                        {
                            Utils.AddToQuarantine(path);
                            if (!File.Exists(path))
                            {
                                continue;
                            }

                            try
                            {
                                UnlockObjectClass.ResetObjectACL(new FileInfo(path).DirectoryName);
                                File.SetAttributes(path, FileAttributes.Normal);

                                Utils.AddToQuarantine(path);
                                if (!File.Exists(path))
                                {
                                    Program.LL.LogSuccessMessage("_Malici0usFile", path, "_MovedToQuarantine");
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error));
                            }


                        }
                    }
                    else
                    {
                        if (File.Exists(path))
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                        }
                    }
                }
            }
            else
            {
                if (!Program.ScanOnly)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
        }

        public bool IsMalic1ousFile(string file, bool displayProgress = true)
        {

            if (!File.Exists(file) || file.Length > 240)
            {
                return false;
            }

            if (displayProgress)
            {
                LocalizedLogger.LogAnalyzingFile(file);
            }

            try
            {

                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Length > maxFileSize || fileInfo.Length < minFileSize)
                {
                    if (displayProgress)
                    {
                        LocalizedLogger.LogOK();
                    }
                    return false;
                }

                WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(file);
                if (trustResult == WinVerifyTrustResult.Success)
                {
                    if (displayProgress)
                    {
                        LocalizedLogger.LogOK();
                    }
                    return false;
                }

                if (FileChecker.IsSfxArchive(file))
                {
                    Program.LL.LogWarnMediumMessage("_sfxArchive", file);
                    Program.totalFoundSuspiciousObjects++;
                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, file, ScanActionType.Skipped));

                    return false;
                }


                bool sequenceFound = FileChecker.CheckSignature(file, MSData.Instance.signatures);

                if (sequenceFound)
                {
                    Program.LL.LogCautionMessage("_Found", file);
                    Program.totalFoundThreats++;
                    founded_mlwrFiles.Add(file);
                    return true;
                }

                bool computedSequence = FileChecker.CheckDynamicSignature(file, 16, 100);
                if (computedSequence)
                {
                    founded_mlwrFiles.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);

                    return true;
                }

                bool computedSequence2 = FileChecker.CheckDynamicSignature(file, 2096, startSequence, endSequence);
                if (computedSequence2)
                {
                    founded_mlwrFiles.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);
                    return true;
                }

                if (displayProgress)
                {
                    LocalizedLogger.LogOK();
                }
            }
            catch (DirectoryNotFoundException)
            {
                //nothing to do
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x8007016A)))
            {
                Program.LL.LogWarnMediumMessage("_ErrorFileOnlineOnly", file);
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                Program.LL.LogCautionMessage("_ErrorLockedByWD", file);
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorAnalyzingFile", ex, file);
            }
            return false;
        }

        internal static void SentLog()
        {
            if (OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) >= 0 || Program.bootMode == BootMode.SafeMinimal)
            {
                return;
            }

            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    return;
                }
            }

            TelegramAPI.UploadFile(Path.Combine(Logger.LogsFolder, Logger.logFileName), $"{DeviceIdProvider.GetDeviceId()}\nv{Program.CurrentVersion}");
        }
    }
}
