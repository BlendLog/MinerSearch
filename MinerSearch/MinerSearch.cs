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

        internal MSData msData = new MSData();
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
                    ProcessManager.SuspendProcess(process);
                    mlwrPids.Add(process.Id);
                    Program.totalFoundThreats++;
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

            string myExePath = FileSystemManager.GetLongPath(Assembly.GetExecutingAssembly().Location);

            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<Process> procs = ProcessManager.SafeGetProcesses();

            if (!Program.RunAsSystem)
            {
                msData.obfStr2.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "clienthelper-updater", "installer.exe"));
                msData.obfStr2.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "torrentpro-updater", "installer.exe"));
                msData.obfStr2.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "Programs", "Common", "OneDriveCloud", "ta?sk?hos?tw.e?xe".Replace("?", "")));
                msData.obfStr2.Add(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Microsoft", "U?pda?teTa?skMa?nage?r.e?xe".Replace("?", "")));
                msData.obfStr2.Add(Path.Combine(Environment.GetEnvironmentVariable("temp"), "btma" + "insvc.exe"));
            }

            HashSet<string> badStringInArgs = new HashSet<string>()
            {
               new StringBuilder("st").Append("ra").Append("tu").Append("m").ToString(),
               new StringBuilder("na").Append("no").Append("po").Append("ol").ToString(),
               new StringBuilder("po").Append("ol").Append(".").ToString(),
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
                    string processPath = FileSystemManager.GetLongPath(Path.GetFullPath(p.MainModule.FileName));
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


                            //if (ProcessManager.IsTrustedProcess(msData, originalFileName, isValidProcess))
                            //{
                            //    continue;
                            //}


                            if (fileDescription != null)
                            {
                                if (fileDescription.IndexOf("svhost", StringComparison.OrdinalIgnoreCase) >= 0)
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

                    if (processName.Equals(msData.SysFileName[19], StringComparison.OrdinalIgnoreCase))
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

                        if ((processName.Equals(msData.SysFileName[3], StringComparison.OrdinalIgnoreCase) && args.IndexOf($"\\??\\{Program.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) == -1))
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
                        if (processName.Equals(msData.SysFileName[4], StringComparison.OrdinalIgnoreCase) && args.IndexOf($"{msData.SysFileName[4]}.exe -k", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }
                        if (processName.Equals(msData.SysFileName[4], StringComparison.OrdinalIgnoreCase) && (args.IndexOf($"{msData.SysFileName[4]}.exe -k dcomlaunch", StringComparison.OrdinalIgnoreCase) >= 0))
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
                        if (processName.Equals(msData.SysFileName[5], StringComparison.OrdinalIgnoreCase) && !Program.RunAsSystem)
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
                                        if (dwmParent.HasExited || !dwmParent.ProcessName.Equals(msData.SysFileName[7], StringComparison.OrdinalIgnoreCase))
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
                                        if (dwmParent.HasExited || !dwmParent.ProcessName.Equals(msData.SysFileName[4], StringComparison.OrdinalIgnoreCase))
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
                        if (processName.Equals(msData.SysFileName[17], StringComparison.OrdinalIgnoreCase) && args.IndexOf("\\dia?ler.exe ".Replace("?", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals(msData.SysFileName[32], StringComparison.OrdinalIgnoreCase) && args.IndexOf("#system32", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals(msData.SysFileName[31], StringComparison.OrdinalIgnoreCase) && !p.HasExited ? (DateTime.Now - p.StartTime).TotalSeconds >= 60 : false)
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

                    if (processName.Equals(msData.SysFileName[28], StringComparison.OrdinalIgnoreCase) && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1)
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

                    for (int i = 0; i < msData.SysFileName.Length; i++)
                    {

                        if (processName.Equals(msData.SysFileName[i], StringComparison.OrdinalIgnoreCase))
                        {

                            if (fullPath.IndexOf($"{Program.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\syswow64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\winsxs\\amd64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\microsoft.net\\framework64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{Program.drive_letter}:\\windows\\microsoft.net\\framework", StringComparison.OrdinalIgnoreCase) == -1)
                            {

                                Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }

                            if (fileSize >= msData.constantFileSize[i] * 3 && !isValidProcess)
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
                        if (processName.Equals(msData.SysFileName[17], StringComparison.OrdinalIgnoreCase) && p.MainModule.FileName.IndexOf(@":\w?in?do?ws\s?yst?em3?2\wb?em".Replace("?", ""), StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            Program.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");

                            isSuspiciousPath = true;
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

                    if (p.TotalProcessorTime > new TimeSpan(0, 1, 0) || (p.PagedMemorySize64 / 1024 * 1024) >= 2048)
                    {

                        if (File.Exists(processPath))
                        {
                            if (IsMalic1ousFile(FileSystemManager.GetLongPath(processPath), false))
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

                    if (msData.obfStr2.Any(s => FileSystemManager.GetLongPath(s).Equals(processPath, StringComparison.OrdinalIgnoreCase)))
                    {
                        riskLevel += 3;
                        isMaliciousProcess = true;
                    }

                    if (riskLevel >= 3)
                    {
                        Program.LL.LogCautionMessage("_ProcessFound", riskLevel.ToString());
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
                msData.obfStr6.Clear();
                msData.obfStr6.Add(Program.drive_letter + ":\\");
            }

            if (!Program.WinPEMode)
            {

                if (!Program.RunAsSystem)
                {
                    msData.obfStr1.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "clienthelper-updater"));
                    msData.obfStr1.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "torrentpro-updater"));
                    msData.obfStr1.Add(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "Programs", "Common", "OneDriveCloud"));
                }


                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (DesktopPath != null && !Program.RunAsSystem)
                {
                    msData.obfStr5.Add(Path.Combine(DesktopPath, "aut~olo~~gger".Replace("~", "")));
                    msData.obfStr5.Add(Path.Combine(DesktopPath, "av~_bl~~ock~_rem~~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DesktopPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !Program.WinPEMode && !Program.fullScan)
                    {
                        msData.obfStr6.Add(DesktopPath);
                    }
                }
                //#if !DEBUG
                string DownloadsPath = FileSystemManager.GetDownloadsPath();
                if (DownloadsPath != null && !Program.RunAsSystem)
                {
                    msData.obfStr5.Add(Path.Combine(DownloadsPath, "auto~lo~gge~~r".Replace("~", "")));
                    msData.obfStr5.Add(Path.Combine(DownloadsPath, "av_~bl~o~ck~_re~m~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DownloadsPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !Program.WinPEMode && !Program.fullScan)
                    {
                        msData.obfStr6.Add(DownloadsPath);
                    }
                }
                //#endif
            }

            if (!string.IsNullOrEmpty(Program.selectedPath) && Directory.Exists(Program.selectedPath) && !Program.fullScan)
            {
                msData.obfStr6.Clear();
                msData.obfStr6.Add(Program.selectedPath);
            }

            msData.obfStr6 = msData.obfStr6.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            ScanDirectories(msData.obfStr5, founded_suspLockedPaths, true);

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
                            msData.obfStr1.Add(directory);
                        }
                    }
                }
            }

            msData.obfStr1.Add(Path.Combine(Environment.GetEnvironmentVariable("appdata"), "sys?fi?les".Replace("?", "")));

            ScanDirectories(msData.obfStr1, founded_mlwrDirs);
            if (!Program.ScanOnly)
            {
                if (founded_mlwrDirs.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            //#if DEBUG
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
            //#if !DEBUG
            CleanHosts();
            //#endif
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



            foreach (string path in msData.obfStr2)
            {
                if (File.Exists(path))
                {
                    Program.totalFoundThreats++;
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            UnlockObjectClass.KillAndDelete(path);
                            if (!File.Exists(path))
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
                                    Program.LL.LogSuccessMessage("_UnlockSuccess");
                                }
                                UnlockObjectClass.KillAndDelete(path);
                                if (!File.Exists(path))
                                {
                                    Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                    deletedFilesCount++;
                                    continue;
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
                                    UnlockObjectClass.ResetObjectACL(Path.GetDirectoryName(path));
                                    Program.LL.LogSuccessMessage("_UnlockSuccess");
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
                foreach (string programPath in msData.obfStr2)
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

                                Logger.WriteLog($"------------------------------", ConsoleColor.White);
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
            string hostsPath_full = $"{Program.drive_letter}:\\Windows\\System32\\drivers\\etc\\hosts";
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

                File.Create(hostsPath_full).Close();
                Thread.Sleep(100);
                if (File.Exists(hostsPath_full))
                {
                    Program.LL.LogSuccessMessage("_HostsFileCreated");
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

                        if (line.StartsWith("#") || msData.whiteListedWords.Any(word => line.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            continue;
                        }

                        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2) continue;

                        string ipAddress = parts[0];
                        string domain = parts[1];

                        foreach (HashedString hLine in msData.hStrings)
                        {
                            if (hLine.OriginalLength <= domain.Length)
                            {
                                string truncatedDomain = domain.Substring(domain.Length - hLine.OriginalLength);

                                if (Utils.StringMD5(truncatedDomain).Equals(hLine.Hash))
                                {
                                    if (!Program.ScanOnly)
                                    {
                                        if (!msData.hStrings.Any(h => Utils.StringMD5(domain).Equals(h.Hash)))
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
                msData.hStrings.Clear();
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
                RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(msData.queries[1], true);
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
                        RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(msData.queries[2], true);
                        if (DisallowRunSub != null)
                        {
                            Program.totalFoundThreats++;
                            DisallowRunKey.DeleteSubKeyTree("Di?sall?owR?un".Replace("?", ""));
                            DisallowRunSub = Registry.CurrentUser.OpenSubKey(msData.queries[2], true);
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
                RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(msData.queries[3], true);
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
                RegistryKey IFEOKey = Registry.LocalMachine.OpenSubKey(msData.queries[15], true);

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
                RegistryKey IFEOKey = Registry.LocalMachine.OpenSubKey(msData.queries[16], true);

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
                RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(msData.queries[17], writable: true);

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
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(msData.queries[4], true);
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
                        if (AutorunKeyValue == $@"{Program.drive_letter}:\Pro?gra?mDa?ta\Re?aItek?HD\task?host?w.e?x?e".Replace("?", ""))
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
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(msData.queries[4], true);
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
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                Program.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"HKLM:Run: {value} -> {AutorunKeyValue}", ScanActionType.Skipped));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\run");
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
                        if (UnlockObjectClass.IsRegistryKeyBlocked(Path.Combine("Software", subkeyNameTektonit)))
                        {
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
            string registryPath = @"SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe";
            List<string> badSubkeys = new List<string>()
            {
                "046f9638-b658-43ee-97f8-e15031db0b6f",
                "0cfc12f8-7909-4835-90dd-68d33e7f0f10",
                "10635fa4-7a5b-425d-838b-689f9b246807",
                "17034547-0c43-4381-b97a-ce8a2d5e96f8",
                "36bced03-d5ef-47fa-a598-a6693a3bc59f",
                "3fb8bf6b-9eed-456b-94e4-00022745779e",
                "443594ac-609b-4dd7-816d-f4f1e3efc726",
                "489640ba-736f-4381-9b78-b11b5fa07fea",
                "5766b2e3-7cad-4f73-9c67-762db4f8d63a",
                "5c158d85-7483-455d-8f96-a1888217e308",
                "6a0278ea-9b21-4c53-a18c-a0e6411ea624",
                "701deaa1-2dad-4f95-a15a-1aa778b4b812",
                "71e498b6-68f4-4c4c-9831-b37fa2483e24",
                "72b5c9be-1cf7-43eb-af80-63feaf6bb690",
                "7b63de66-5456-46bc-9a2a-2fe7a84cd763",
                "7fde4b58-4627-49c7-baef-4a881d3ef94c",
                "808be0f0-b8ab-46c7-a3a0-bdeb742ccde9",
                "839d18ed-9e08-492b-bfca-4a53c1e7c8c4",
                "85a18717-d5f9-4f3b-89b4-1ed4f02b1eeb",
                "8c9ead7d-b294-4159-9607-9b9b7766f860",
                "8e27ae66-7447-4de5-8759-475393f09764",
                "93b1f30a-51e3-4582-a3e0-582d1ba1987d",
                "97e69d73-af4e-4d3b-93c0-de2d00492518",
                "9cfdfc36-6bd5-4b9c-baf1-56ba7df44ec6",
                "a395fe35-b771-44e1-b640-8877314b2643",
                "a439a434-146a-4c9f-8743-051f522f36bb",
                "af801e3f-3fa4-4910-b559-b9c956783ee5",
                "b1a2abe0-68e5-4632-866f-2c6215dec459",
                "baac2a1e-8890-4bad-998a-c11534e1b44d",
                "bae342c0-8b15-4823-80a8-fe5067a75f90",
                "be235b32-21ab-4dd8-bc6e-61649ec11f3d",
                "c1abb5ee-85f8-47dd-b567-cfbe3ea51516",
                "c2d49146-e267-4fe6-9867-b2d42fdf52e2",
                "c888e849-8015-4f41-b2a2-d18e4c6bf02c",
                "ca90426a-78be-4a8b-af20-d13452175d73",
                "cb5f59ee-d2be-4d9d-99dc-7657843cece2",
                "d16c6ab4-3721-4e52-9902-64e76212094c",
                "d8ee32c1-472b-41dd-a204-b198cb1ae9b8",
                "ea9fa9c5-2743-44a1-99ed-d9ac26a135e7",
                "ec544bd8-4a5d-4ae7-8c5c-044f4b6d60fb",
                "ec77c5b9-3955-44f4-804b-c678504c16b6",
                "f025c3b3-d9d1-4c09-be3b-bfc05fdbe243",
                "f2be1651-b3c6-477d-a183-8f2946538210",
                "f9729781-9d66-46b8-8553-f0099fd924d3",
                "f9b3908f-4f58-45ec-a9a8-c1b88e9dbe98",
                "d8e659be-d4a5-4cd6-bf96-c92736039685",
                "e8a3f75c-ee02-4c96-958e-7e31352c196c",
                "eedeed7f-e2e7-4181-8050-4a4f90361328",
                "adb6a6f1-9af9-496f-b8d4-ba695911f83a"
            };

            bool isContainsBadPolicies = false;

            List<string> allSubkeys = Program._utils.GetSubkeys(registryPath);

            if (allSubkeys.Count > 0)
            {
                using (RegistryKey parentKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, true))
                {
                    foreach (var subkeyName in allSubkeys)
                    {
                        if (badSubkeys.Contains(subkeyName, StringComparer.OrdinalIgnoreCase))
                        {
                            isContainsBadPolicies = true;
                            Program.totalFoundThreats++;
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
                        scanResults.Add(new ScanResult(ScanObjectType.Infected, $"Registry:Applocker".Replace("?", ""), ScanActionType.Deleted));
                    }
                }
            }

            #endregion

            #region WindowsDefender
            StringBuilder sbWD = new StringBuilder("Re").Append("gi").Append("st").Append("ry").Append(":W").Append("in").Append("De").Append("fe").Append("nd").Append(" P").Append("ol").Append("ic").Append("y");
            Logger.WriteLog($"[Reg] {new StringBuilder("Wi").Append("nd").Append("ow").Append("s ").Append("De").Append("fe").Append("nd").Append("er")}...".Replace("~", ""), ConsoleColor.DarkCyan);
            try
            {
                RegistryKey winDfndr = Registry.LocalMachine.OpenSubKey(msData.queries[5], true);
                if (winDfndr != null)
                {
                    foreach (string path in msData.obfStr3)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(msData.queries[6], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Program.totalFoundThreats++;

                                        if (!Program.ScanOnly)
                                        {
                                            key.DeleteValue(valueName);
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{sbWD} -> {valueName}".Replace("?", ""), ScanActionType.Deleted));
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);
                                            affected_items++;
                                        }
                                        else
                                        {
                                            Program.LL.LogWarnMediumMessage("_FoundMlwrKey", valueName);
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{sbWD} -> {valueName}".Replace("?", ""), ScanActionType.Skipped));

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, valueName);
                                }

                            }

                            key.Close();
                        }
                    }

                    foreach (string process in msData.obfStr4)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(msData.queries[7], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(process, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Program.totalFoundThreats++;
                                        if (!Program.ScanOnly)
                                        {
                                            key.DeleteValue(valueName);
                                            Program.LL.LogSuccessMessage("_Exclusion", valueName, "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{sbWD} -> {valueName}".Replace("?", ""), ScanActionType.Deleted));

                                            affected_items++;
                                        }
                                        else
                                        {
                                            Program.LL.LogWarnMediumMessage("_MaliciousEntry", $"{valueName} (WinD?efen?der)");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{sbWD} -> {valueName}".Replace("?", ""), ScanActionType.Skipped));

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, valueName, "_Exclusion");
                                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{sbWD} -> {valueName}".Replace("?", ""), ScanActionType.Error));

                                }

                            }

                            key.Close();
                        }
                    }
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
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(msData.queries[8], true);
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
                msData.queries[9], // PowerShell
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

                        FileSystemManager.ProcessFileFromArgs(checkDirectories, filePath, arguments);

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

                        if (arguments.Replace("'", "").IndexOf(" -e ", StringComparison.OrdinalIgnoreCase) >= 0 || arguments.Replace("'", "").IndexOf("-encodedcommand", StringComparison.OrdinalIgnoreCase) >= 0)
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

                    if (filePath.IndexOf(new StringBuilder("ws").Append("cri").Append("pt").ToString(), StringComparison.OrdinalIgnoreCase) >= 0 && arguments.EndsWith(".wsf\"", StringComparison.OrdinalIgnoreCase))
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

                    if (filePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && arguments.Equals("/LHS", StringComparison.OrdinalIgnoreCase))
                    {
                        if (File.Exists(filePath))
                        {
                            Program.totalFoundThreats++;
                            Utils.AddToQuarantine(filePath);
                        }

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

                    if (winTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success)
                    {
                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                        return;
                    }

                    long fileSize = new FileInfo(filePath).Length;
                    if (fileSize >= maxFileSize)
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

                    if (FileChecker.CheckSignature(filePath, msData.signatures) || FileChecker.CheckDynamicSignature(filePath, 16, 100))
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

                    if (filesCount.Equals(1) && FileSystemManager.HasSystemHiddenAttribute(filePath) && fileSize > 1024 * 1024)
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

            Dictionary<ServiceController, bool> suspiciousServiceList = new Dictionary<ServiceController, bool>();
            HashSet<string> suspiciousServiceDlls = new HashSet<string>() { };

            // Get all services
            ServiceController[] services = ServiceController.GetServices();
            string[] sideloadableDlls = { "SbieDll.dll", "MSASN1.dll" };

            HashSet<string> trustedPaths = new HashSet<string>();
            string registryPath = new StringBuilder("SY").Append("ST").Append("EM").Append("\\C").Append("ur").Append("re").Append("nt").Append("Co").Append("nt").Append("ro").Append("lS").Append("et").Append("\\S").Append("er").Append("vi").Append("ce").Append("s").ToString();
            string ValidServicePath = "";
            bool isMlwrSignature = false;
            bool isLoadDLL = false;


            foreach (ServiceController service in services)
            {
                string serviceName = service.ServiceName;
                isMlwrSignature = false;
                isLoadDLL = false;

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
                        ValidServicePath = servicePath;
                        FileInfo fi = new FileInfo(ValidServicePath);
                        Program.LL.LogMessage("[.]", "_FileSize", FileChecker.GetFileSize(fi.Length), ConsoleColor.White);
                        if (!trustedPaths.Contains(servicePath))
                        {
                            WinVerifyTrustResult servicePathSignature = winTrust.VerifyEmbeddedSignature(Environment.ExpandEnvironmentVariables(servicePath));
                            if (servicePathSignature != WinVerifyTrustResult.Success && servicePathSignature != WinVerifyTrustResult.Error)
                            {
                                if (fi.Length >= maxFileSize || FileChecker.CheckSignature(servicePath, msData.signatures) || FileChecker.CheckDynamicSignature(servicePath, 2096, startSequence, endSequence) || FileChecker.CheckDynamicSignature(servicePath, 16, 100))
                                {
                                    Program.LL.LogCautionMessage("_Found", $"{service.ServiceName} {servicePath}");
                                    isMlwrSignature = true;
                                    suspiciousServiceList.Add(service, isMlwrSignature);
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
                                        if (Array.Exists(sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                                        {
                                            WinVerifyTrustResult dllSignature = winTrust.VerifyEmbeddedSignature(dll);

                                            if (dllSignature != WinVerifyTrustResult.Success)
                                            {
                                                Program.LL.LogCautionMessage("_Found", Program.LL.GetLocalizedString("_ValidServiceDLLHijacking").Replace("#SERVICENAME#", service.ServiceName).Replace("#DLLNAME#", dll));
                                                suspiciousServiceList.Add(service, isMlwrSignature);
                                                isLoadDLL = true;
                                                UnlockObjectClass.DisableExecute(dll);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!isLoadDLL)
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
                                                    suspiciousServiceList.Add(service, isMlwrSignature);
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

                Logger.WriteLog("------------", ConsoleColor.White);
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
                            DisableService(suspiciousService.Key, ValidServicePath, suspiciousService.Value, suspiciousServiceDlls);
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
                        DisableService(suspiciousService.Key, ValidServicePath, suspiciousService.Value, suspiciousServiceDlls);
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

        private void DisableService(ServiceController service, string servicePath, bool isMlwrSigChk, HashSet<string> serviceDlls = null)
        {

            string serviceName = service.ServiceName;

            var startMode = NativeServiceController.GetServiceStartType(serviceName);
            ServiceControllerStatus status = service.Status;

            if (startMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                Program.totalFoundThreats++;
                NativeServiceController.SetServiceStartType(service.ServiceName, NativeServiceController.ServiceStartMode.Disabled);
                if (NativeServiceController.GetServiceStartType(service.ServiceName) == NativeServiceController.ServiceStartMode.Disabled)
                {
                    Program.LL.LogSuccessMessage("_ServiceDisabled", serviceName);
                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, Program.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + servicePath, ScanActionType.Disabled));
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
                Program.totalNeutralizedThreats--;
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
                        }
                    }
                }
            }

            if (isMlwrSigChk)
            {

                try
                {
                    try
                    {
                        if (File.Exists(servicePath))
                        {
                            if (new FileInfo(servicePath).Length >= maxFileSize)
                            {
                                UnlockObjectClass.KillAndDelete(servicePath);
                                if (!File.Exists(servicePath))
                                {
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, servicePath, ScanActionType.Deleted));
                                }
                            }
                        }
                    }
                    catch (IOException)
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, servicePath, ScanActionType.Error));
                    }
                    catch (UnauthorizedAccessException)
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, servicePath, ScanActionType.Error));
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
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + servicePath, ScanActionType.Error));

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
                msData.obfStr6.Add(Path.GetTempPath());
            }
#endif

            if (!string.IsNullOrEmpty(Program.selectedPath) && Directory.Exists(Program.selectedPath) && !Program.fullScan)
            {
                msData.obfStr6.Clear();
                msData.obfStr6.Add(Program.selectedPath);
            }

            if (Program.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                var localDrives = allDrives.Where(drive => drive.DriveType == DriveType.Fixed && !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));
                foreach (var drive in localDrives)
                {
                    msData.obfStr6.Add(drive.Name);
                }
            }



            msData.obfStr6 = msData.obfStr6.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            //foreach (string path in new List<string> { @"D:\share\_evil\" })
            foreach (string path in msData.obfStr6)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }


                foreach (var filepath in FileEnumerator.GetFiles(path, "*.exe", 0, Program.maxSubfolders))
                {
                    if (IsMalic1ousFile(FileSystemManager.GetLongPath(filepath)))
                    {
                        UnlockObjectClass.DisableExecute(FileSystemManager.GetLongPath(filepath));
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

            if (displayProgress) //for SignatureScan
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

                //#if !DEBUG

                bool sequenceFound = FileChecker.CheckSignature(file, msData.signatures);

                if (sequenceFound)
                {
                    Program.LL.LogCautionMessage("_Found", file);
                    Program.totalFoundThreats++;
                    founded_mlwrFiles.Add(file);
                    return true;
                }
                //#endif

                bool computedSequence = FileChecker.CheckDynamicSignature(file, 16, 100);
                if (computedSequence)
                {
                    founded_mlwrFiles.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);

                    return true;
                }
                //#if !DEBUG

                bool computedSequence2 = FileChecker.CheckDynamicSignature(file, 2096, startSequence, endSequence);
                if (computedSequence2)
                {
                    founded_mlwrFiles.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);
                    return true;
                }
                //#endif

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

            TelegramAPI.UploadFile(Path.Combine(Logger.LogsFolder, Logger.logFileName), OSExtensions.GetDeviceId());
        }
    }
}
