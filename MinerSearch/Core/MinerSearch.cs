using DBase;
using Microsoft.Win32;
using MSearch.Core;
using MSearch.Infrastructure;
using MSearch.UI;
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
            7001,
            7777,
            9980,
            9999,
            10191,
            10343,
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
            AppConfig.Instance.LL.LogHeadMessage("_ChekingR00tkit");

            Native.R77_PROCESS[] r77Processes = new Native.R77_PROCESS[Native.MaxProcesses];
            uint processCount = Native.MaxProcesses;

            RkRemover remover = new RkRemover();
            remover.GetR77Processes(ref r77Processes, ref processCount);
            if (processCount > 0)
            {
                LocalizedLogger.LogR00TkitPresent();
                AppConfig.Instance.totalFoundThreats++;

                remover.DetachAllInjProcs(r77Processes, processCount);
                remover.TerminateR77Service(-1);
                remover.RemoveR77Config();

                remover.GetR77Processes(ref r77Processes, ref processCount);
                if (processCount == 0)
                {
                    AppConfig.Instance.LL.LogSuccessMessage("_SuccessR00tkitNeutralized");
                }


                foreach (Process process in ProcessManager.SafeGetProcesses())
                {
                    if (!process.ProcessName.StartsWith(new StringBuilder("di").Append("al").Append("er").ToString())) continue;
                    try
                    {
                        ProcessManager.SuspendProcess(process);
                        mlwrPids.Add(process.Id);
                        AppConfig.Instance.totalFoundThreats++;
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
            AppConfig.Instance.LL.LogHeadMessage("_ScanProcesses");

            string myExePath = FileSystemManager.NormalizeExtendedPath(Assembly.GetExecutingAssembly().Location);

            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<Process> procs = ProcessManager.SafeGetProcesses();

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
                string args = null;
                bool isMaliciousProcess = false;
                bool isBadArgsPatternPresent = false;
                bool isFileTooBig = false;

                try
                {
                    string processPath = FileSystemManager.NormalizeExtendedPath(Path.GetFullPath(p.MainModule.FileName));
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
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                                    suspFls_path.Add(p.MainModule.FileName);
                                    riskLevel += 2;
                                }
                            }

                            if (originalFileName != null)
                            {
                                if (originalFileName.IndexOf(new StringBuilder("Spot").Append("ifySta").Append("rtupTas").Append("k.exe").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                                    riskLevel += 3;
                                    founded_mlwrFiles.Add(p.MainModule.FileName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", processPath);
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
                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                    }


                    if (modCount > 2)
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_GPULibsUsage", $"{processName}.exe, PID: {processId}");
                        riskLevel += 1;

                    }

                    if (AppConfig.Instance.bootMode != BootMode.SafeMinimal)
                    {
                        try
                        {
                            int remoteport = ProcessManager.GetPortByProcessId(p.Id);
                            if (remoteport != -1 && remoteport != 0)
                            {
                                if (_PortList.Contains(remoteport))
                                {
                                    AppConfig.Instance.LL.LogWarnMessage("_BlacklistedPort", $"{remoteport} - {processName}");
                                    riskLevel += 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                        }
                    }

                    if (!string.IsNullOrEmpty(args))
                    {
                        foreach (int port in _PortList)
                        {
                            if (args.IndexOf($":{port}", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                riskLevel += 1;
                                AppConfig.Instance.LL.LogWarnMessage("_BlacklistedPortCMD", $"{port} : {processName}.exe");
                            }
                        }

                        if (MSData.Instance.badArgStrings.Any(s => args.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            riskLevel += 3;
                            isBadArgsPatternPresent = true;
                            AppConfig.Instance.LL.LogWarnMediumMessage("_PresentInCmdArgs", args);
                        }


                        if (args.IndexOf("-systemcheck", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            riskLevel += 2;
                            AppConfig.Instance.LL.LogWarnMessage("_FakeSystemTask");

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
                                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                                continue;

                            }

                        }

                        if ((processName.Equals(MSData.Instance.SysFileName[3], StringComparison.OrdinalIgnoreCase) && args.IndexOf($"\\??\\{AppConfig.Instance.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) == -1))
                        {
                            riskLevel += 3;
                            if (args.IndexOf($"\\\\?\\{AppConfig.Instance.drive_letter}:\\", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                riskLevel--;
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                            }
                        }
                        if (processName.Equals(MSData.Instance.SysFileName[4], StringComparison.OrdinalIgnoreCase) && (args.IndexOf($"{MSData.Instance.SysFileName[4]}.exe -k dcomlaunch", StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            foreach (ProcessModule pMod in p.Modules)
                            {
                                WinVerifyTrustResult pModSignature = winTrust.VerifyEmbeddedSignature(pMod.FileName);
                                if (pModSignature != WinVerifyTrustResult.Success && pModSignature != WinVerifyTrustResult.Error)
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_ServiceDcomAbusing", pMod.FileName + $" | PID: {p.Id}");
                                }
                            }
                        }

                        if (processName.Equals(MSData.Instance.SysFileName[32], StringComparison.OrdinalIgnoreCase) && args.IndexOf("#system32", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals(MSData.Instance.SysFileName[31], StringComparison.OrdinalIgnoreCase) && !p.HasExited && (DateTime.Now - p.StartTime).TotalSeconds >= 60)
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName.Equals("explorer", StringComparison.OrdinalIgnoreCase) && args.IndexOf($@"{AppConfig.Instance.drive_letter}:\Windows\Explorer.exe", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            riskLevel++;
                        }

                    }

                    bool isSuspiciousPath = false;
                    string fullPath = p.MainModule.FileName;
                    string appData = Environment.GetEnvironmentVariable("AppData") ?? "";
                    if (!isValidProcess && fullPath.StartsWith(appData, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(Path.GetExtension(fullPath)))
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        isSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    if (processName.Equals(MSData.Instance.SysFileName[28], StringComparison.OrdinalIgnoreCase) && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        isSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    try
                    {
                        fileSize = new FileInfo(processPath).Length;
                    }
                    catch (Exception ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                    }

                    for (int i = 0; i < MSData.Instance.SysFileName.Length; i++)
                    {

                        if (processName.Equals(MSData.Instance.SysFileName[i], StringComparison.OrdinalIgnoreCase))
                        {

                            if (fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\system32", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\system32\\wbem", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\syswow64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\winsxs\\amd64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\winsxs\\x86", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\microsoft.net\\framework64", StringComparison.OrdinalIgnoreCase) == -1
                                && fullPath.IndexOf($"{AppConfig.Instance.drive_letter}:\\windows\\microsoft.net\\framework", StringComparison.OrdinalIgnoreCase) == -1)
                            {

                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }

                            if (fileSize >= MSData.Instance.constantFileSize[i] * 3 && !isValidProcess)
                            {
                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousFileSize", FileChecker.GetFileSize(fileSize));
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
                        if (processName.Equals(MSData.Instance.SysFileName[17], StringComparison.OrdinalIgnoreCase) && (ProcessManager.IsDotnetProcess(p) || p.TotalProcessorTime > new TimeSpan(0, 0, 30)))
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                            riskLevel += 3;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                        continue;
                    }


                    if (processName.Equals("rundll", StringComparison.OrdinalIgnoreCase) || processName.Equals("system", StringComparison.OrdinalIgnoreCase) || processName.Equals("wi?ns?er?v".Replace("?", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");

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


                        if (ProcessManager.IsSystemProcess(p.Id) && !AppConfig.Instance.RunAsSystem)
                        {
                            riskLevel += 3;
                        }

                        if (ProcessManager.IsDotnetProcess(p))
                        {
                            riskLevel += 1;
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

                        if (ProcessManager.IsDotnetProcess(p))
                        {
                            riskLevel += 1;
                        }
                    }

                    if (p.TotalProcessorTime > new TimeSpan(0, 1, 0) || (p.PagedMemorySize64 / (1024 * 1024) >= 2048))
                    {

                        if (File.Exists(processPath))
                        {
                            if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(processPath), false))
                            {
                                riskLevel += 3;
                                isMaliciousProcess = true;
                            }

                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", processPath);
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

                    if (MSData.Instance.obfStr2.Any(s => FileSystemManager.NormalizeExtendedPath(s).Equals(processPath, StringComparison.OrdinalIgnoreCase)))
                    {
                        riskLevel += 3;
                        isMaliciousProcess = true;
                    }

                    if (isValidProcess && ProcessManager.IsProcessHollowed(p))
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                        riskLevel += 3;
                    }

                    if (riskLevel >= 3)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_ProcessFound", ProcessManager.GetLocalizedRiskLevel(riskLevel));
                        AppConfig.Instance.totalFoundThreats++;
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            ProcessManager.SuspendProcess(p);
                        }



                        if (isSuspiciousPath || isMaliciousProcess)
                        {
                            if (!AppConfig.Instance.ScanOnly)
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
                            if (!AppConfig.Instance.ScanOnly)
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

                    DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorScanProcesses"), AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }

                if ((riskLevel == 0 || !isMaliciousProcess) && AppConfig.Instance.verbose)
                {
                    LocalizedLogger.LogOK();
                }
            }

            procs.Clear();

        }

        public void StaticScan()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanDirectories");

            if (AppConfig.Instance.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(AppConfig.Instance.drive_letter + ":\\");
            }

            if (!AppConfig.Instance.WinPEMode)
            {

                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (DesktopPath != null && !AppConfig.Instance.RunAsSystem)
                {
                    MSData.Instance.obfStr5.Add(Path.Combine(DesktopPath, "aut~olo~~gger".Replace("~", "")));
                    MSData.Instance.obfStr5.Add(Path.Combine(DesktopPath, "av~_bl~~ock~_rem~~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DesktopPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !AppConfig.Instance.WinPEMode && !AppConfig.Instance.fullScan)
                    {
                        MSData.Instance.obfStr6.Add(DesktopPath);
                    }
                }

                string DownloadsPath = FileSystemManager.GetDownloadsPath();
                if (DownloadsPath != null && !AppConfig.Instance.RunAsSystem)
                {
                    MSData.Instance.obfStr5.Add(Path.Combine(DownloadsPath, "auto~lo~gge~~r".Replace("~", "")));
                    MSData.Instance.obfStr5.Add(Path.Combine(DownloadsPath, "av_~bl~o~ck~_re~m~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DownloadsPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && !AppConfig.Instance.WinPEMode && !AppConfig.Instance.fullScan)
                    {
                        MSData.Instance.obfStr6.Add(DownloadsPath);
                    }
                }
            }

            if (!string.IsNullOrEmpty(AppConfig.Instance.selectedPath) && Directory.Exists(AppConfig.Instance.selectedPath) && !AppConfig.Instance.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(AppConfig.Instance.selectedPath);
            }

            MSData.Instance.obfStr6 = MSData.Instance.obfStr6.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            ScanDirectories(MSData.Instance.obfStr5, founded_suspLockedPaths, true);

            if (!AppConfig.Instance.ScanOnly)
            {
                if (founded_suspLockedPaths.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

            }


            string baseDirectory = AppConfig.Instance.drive_letter + @":\ProgramData";
            string pattern = @"^[a-zA-Z0-9_\-]+-[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
            Regex regex = new Regex(pattern);

            foreach (string directory in Directory.EnumerateDirectories(baseDirectory))
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

            string windir = AppConfig.Instance.drive_letter + @":\Windows";
            foreach (string file in Directory.EnumerateFiles(windir, "*.exe", SearchOption.TopDirectoryOnly))
            {
                if (FileChecker.IsFileSizeBloated(file))
                {
                    MSData.Instance.obfStr2.Add(file);
                }
            }

            ScanDirectories(MSData.Instance.obfStr1, founded_mlwrDirs);
            if (!AppConfig.Instance.ScanOnly)
            {
                if (founded_mlwrDirs.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            AppConfig.Instance.LL.LogHeadMessage("_ScanFiles");

            string _baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).Replace("x:", $@"{AppConfig.Instance.drive_letter}:");
            FindMlwrFiles(_baseDirectory);

            if (!AppConfig.Instance.ScanOnly)
            {
                if (founded_mlwrFiles.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            if (!AppConfig.Instance.WinPEMode)
            {
                ScanShellStartup();
                ScanRegistry();
                ScanWMI();
                if (!AppConfig.Instance.no_services)
                {
                    ScanServices();
                }
                if (!AppConfig.Instance.no_scan_tasks)
                {
                    try
                    {
                        ScanTaskScheduler();
                    }
                    catch (Exception ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                    }
                }
                switch (OSExtensions.GetBootMode())
                {
                    case BootMode.SafeMinimal:
                        AppConfig.Instance.LL.LogStatusMessage("_SafeBootHint");
                        break;
                    case BootMode.SafeNetworking:
                        if (!AppConfig.Instance.no_firewall)
                        {
                            AppConfig.Instance.LL.LogHeadMessage("_ScanFirewall");
                            try
                            {
                                ScanFirewall();
                            }
                            catch (FileNotFoundException notfoundEx)
                            {
                                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n{notfoundEx.FileName}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        break;
                    default:
                        if (!AppConfig.Instance.no_firewall)
                        {
                            AppConfig.Instance.LL.LogHeadMessage("_ScanFirewall");
                            try
                            {
                                ScanFirewall();
                            }
                            catch (FileNotFoundException notfoundEx)
                            {
                                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n{notfoundEx.FileName}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                }
            }

            if (!AppConfig.Instance.no_check_hosts)
            {
                CleanHosts();
            }


        }

        public void Clean()
        {

            if (mlwrPids.Count != 0)
            {
                AppConfig.Instance.LL.LogHeadMessage("_Malici0usProcesses");
                AppConfig.Instance.LL.LogCautionMessage("_MlwrProcessesCount", mlwrPids.Count.ToString());

                if (!AppConfig.Instance.ScanOnly)
                {
                    AppConfig.Instance.LL.LogHeadMessage("_TryCloseProcess");
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

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                p.Kill();
                            }

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                if (p.HasExited)
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_ProcessTerminated", $"{pname}, PID: {pid}");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Process")} {pname}", ScanActionType.Terminated));
                                }

                            }
                            else
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usProcess", $"{pname} - PID: {pid}");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Process")} {pname} - PID: {pid}", ScanActionType.Active));

                            }
                        }

                    }
                    catch (InvalidOperationException ioe) when (ioe.HResult.Equals(unchecked((int)0x80070057)))
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {id}");
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                    catch (Exception e)
                    {
                        AppConfig.Instance.LL.LogErrorMessage(AppConfig.Instance.ScanOnly ? "_Error" : "_ErrorTerminateProcess", e);
                        AppConfig.Instance.totalNeutralizedThreats--;
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Process")} {pname}", ScanActionType.Error, e.Message));

                    }
                }
                if (AppConfig.Instance.ScanOnly)
                {
                    LocalizedLogger.LogScanOnlyMode();
                }
            }

            AppConfig.Instance.LL.LogHeadMessage("_RemovingKnownMlwrFiles");

            int deletedFilesCount = 0;



            foreach (string path in MSData.Instance.obfStr2)
            {
                if (File.Exists(path))
                {
                    AppConfig.Instance.totalFoundThreats++;
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        try
                        {
                            if (UnlockObjectClass.KillAndDelete(path))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                deletedFilesCount++;
                                continue;
                            }
                            else
                            {
                                if (UnlockObjectClass.ResetObjectACL(Path.GetDirectoryName(path)))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_UnlockSuccess", path);
                                }

                                if (UnlockObjectClass.KillAndDelete(path))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                    deletedFilesCount++;
                                    continue;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error, ex.Message));
                            AppConfig.Instance.totalNeutralizedThreats--;
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                    }

                }
            }

            if (!AppConfig.Instance.ScanOnly)
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
                AppConfig.Instance.LL.LogHeadMessage("_RemovingMLWRFiles");
                foreach (string path in suspFls_path)
                {
                    if (File.Exists(path))
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            UnlockObjectClass.ResetObjectACL(path);
                            try
                            {
                                File.SetAttributes(path, FileAttributes.Normal);
                                File.Delete(path);
                                AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));


                            }
                            catch (Exception)
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                                AppConfig.Instance.LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                                if (UnlockObjectClass.IsLockedObject(Path.GetDirectoryName(path)))
                                {
                                    if (UnlockObjectClass.ResetObjectACL(Path.GetDirectoryName(path)))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_UnlockSuccess", path);
                                    }
                                }
                                try
                                {

                                    UnlockObjectClass.KillAndDelete(path);
                                    if (!File.Exists(path))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error, ex.Message));
                                    AppConfig.Instance.totalNeutralizedThreats--;
                                }
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                        }
                    }
                }
            }

            if (!AppConfig.Instance.ScanOnly)
            {
                AppConfig.Instance.LL.LogHeadMessage("_CheckingTermService");
                ServiceHelper.CheckTermService();
            }

            if (founded_mlwrDirs.Count > 0)
            {
                AppConfig.Instance.LL.LogHeadMessage("_RemovingMLWRPaths");

                foreach (string str in founded_mlwrDirs)
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        if (Directory.Exists(str))
                        {
                            try
                            {
                                FileSystemManager.ResetAttributes(str);
                                Directory.Delete(str, true);
                                if (!Directory.Exists(str))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));

                                }
                            }
                            catch (Exception)
                            {
                                foreach (string file in FileSystemManager.GetOpenFilesInDirectory(str))
                                {
                                    uint pid = ProcessManager.GetProcessIdByFilePath(file);
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
                                            AppConfig.Instance.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));
                                        }
                                    }
                                }
                                catch (Exception e)
                                {

                                    if (e.Message.EndsWith(".dll\".") || e.Message.EndsWith(".exe\"."))
                                    {
                                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", e, $"\"{str}\"", "_Directory");
                                        AppConfig.Instance.totalNeutralizedThreats--;
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Error, e.Message));

                                    }
                                    else
                                    {
                                        AppConfig.Instance.LL.LogWarnMediumMessage("_ErrorCannotRemove", $"\"{str}\"", e.Message);
                                        AppConfig.Instance.totalFoundThreats--;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMediumMessage("_MaliciousDir", str);
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Skipped));

                    }
                }
            }

            if (founded_suspLockedPaths.Count > 0)
            {
                UnlockFolders(founded_suspLockedPaths);
            }

            if (AppConfig.Instance.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }

            if (!AppConfig.Instance.WinPEMode)
            {
                AppConfig.Instance.LL.LogHeadMessage("_CheckUserJohn");
                const string userName = "John";

                bool userPresent = OSExtensions.CheckUserExists(userName);
                bool userHidden = OSExtensions.IsUserHidden(userName);

                if (userPresent)
                {
                    if (OSExtensions.IsUserHidden(userName))
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            try
                            {
                                OSExtensions.DeleteUser(userName);
                                Thread.Sleep(100);
                                if (!OSExtensions.CheckUserExists(userName))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Userprofile", "\"John\"", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, AppConfig.Instance.LL.GetLocalizedString("_MaliciousProfile") + $" {userName}", ScanActionType.Deleted));
                                }

                            }
                            catch (Exception ex)
                            {
                                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"John\"", "_Userprofile");
                                scanResults.Add(new ScanResult(ScanObjectType.Unknown, AppConfig.Instance.LL.GetLocalizedString("_MaliciousProfile") + $" {userName}", ScanActionType.Error, ex.Message));
                                AppConfig.Instance.totalNeutralizedThreats--;
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_MaliciousProfile", userName);
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, AppConfig.Instance.LL.GetLocalizedString("_MaliciousProfile") + $" {userName}", ScanActionType.Skipped));
                            LocalizedLogger.LogScanOnlyMode();
                        }

                    }
                }

                if (!userPresent || !userHidden)
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
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        if (UnlockObjectClass.IsLockedObject(str))
                        {
                            UnlockObjectClass.ResetObjectACL(str);
                            if (FileSystemManager.IsDirectoryEmpty(str))
                            {
                                Directory.Delete(str, true);
                                if (!Directory.Exists(str))
                                {
                                    AppConfig.Instance.LL.LogMessage("\t[_]", "_RemovedEmptyDir", $"\"{str}\"", ConsoleColor.White);
                                    foldersDeleted++;

                                }
                            }
                        }

                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_LockedDir", $"\"{str}\"");
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", str);
                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, str, ScanActionType.LockedByAntivirus));
                }
                catch (Exception ex1)
                {
#if DEBUG
                    Logger.WriteLog($"\t[*] DeleteEmpyFolders exception: {ex1.Message}", ConsoleColor.Gray, false);
#endif
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex1, str, "_Directory");
                }
            }

            if (!AppConfig.Instance.ScanOnly)
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
                            AppConfig.Instance.totalFoundThreats++;
                            newList.Add(dir);
                        }
                    }
                    catch (SecurityException)
                    {
                        if (checkAccessible)
                        {
                            AppConfig.Instance.totalFoundThreats++;
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
                                AppConfig.Instance.LL.LogMessage("\t[.]", "_Name", rule.Name, ConsoleColor.White);
                                AppConfig.Instance.LL.LogWarnMessage("_Path", rule.ApplicationName);

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    string ruleName = rule.Name;
                                    rules.Remove(ruleName);
                                    firewall_items++;
                                    AppConfig.Instance.LL.LogSuccessMessage("_Rule", ruleName, "_Deleted");
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
                if (!AppConfig.Instance.ScanOnly && firewall_items == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

                if (AppConfig.Instance.ScanOnly)
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

            IEnumerable<string> files = FileEnumerator.GetFiles(directoryPath, "*.bat");

            foreach (string file in files)
            {
                if (FileSystemManager.IsReparsePoint(Path.GetDirectoryName(file)) || !FileSystemManager.IsAccessibleFile(file))
                {
                    continue;
                }

                if (FileChecker.IsBatchFileBad(file))
                {
                    AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", file);
                    AppConfig.Instance.totalFoundThreats++;
                    founded_mlwrFiles.Add(file);

                }
                else
                {
                    AppConfig.Instance.LL.LogMessage("[.]", "_File", file, ConsoleColor.White);
                }
            }

            foreach (string nearExeFile in FileEnumerator.GetFiles(directoryPath, "*.exe", 0, AppConfig.Instance.maxSubfolders))
            {
                if (FileChecker.IsSfxArchive(nearExeFile))
                {
                    WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(nearExeFile);
                    if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.Error)
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        founded_mlwrFiles.Add(nearExeFile);
                    }

                }
            }



        }
        void CleanHosts()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanningHosts");

            List<string> linesToDelete = new List<string>();
            string hostsPath_full = $"{AppConfig.Instance.drive_letter}{MSData.Instance.queries["h0sts"]}";
            string hostsPath_tmp = Path.Combine(Path.GetTempPath(), $"hosts{Utils.GetRndString(16)}");

            if (!File.Exists(hostsPath_full))
            {
                AppConfig.Instance.LL.LogMessage("\t[?]", "_HostsFileMissing", "", ConsoleColor.Gray);
                string hostsdir = Path.GetDirectoryName(hostsPath_full);
                if (!Directory.Exists(hostsdir))
                {
                    Directory.CreateDirectory(hostsdir);
                }
                if (AppConfig.Instance.WinPEMode && !Directory.Exists(hostsdir))
                {
                    Directory.CreateDirectory(hostsdir);
                }
                try
                {
                    File.Create(hostsPath_full).Close();
                    Thread.Sleep(100);
                    if (File.Exists(hostsPath_full))
                    {
                        AppConfig.Instance.LL.LogSuccessMessage("_HostsFileCreated");
                    }
                }
                catch (Exception e)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCleanHosts", e);
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
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCleanHosts", e);
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

                        if (line.StartsWith("#"))
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
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        if (!MSData.Instance.hStrings.Any(h => Utils.StringMD5(domain).Equals(h.Hash)))
                                        {
                                            linesToDelete.Add(line);
                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogWarnMediumMessage("_MaliciousEntry", lines[i]);
                                            lines.RemoveAt(i);
                                            deletedLineCount++;
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        isHostsInfected = true; //for scan-only mode
                                        AppConfig.Instance.LL.LogWarnMessage("_MaliciousEntry", line);
                                    }
                                }
                            }
                        }
                    }

                    if (linesToDelete.Count > 0)
                    {
                        if (!AppConfig.Instance.ScanOnly || !AppConfig.Instance.console_mode)
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
                                    foreach (string line in selectedLinesToDelete)
                                    {
                                        deletedLineCount++;
                                        AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", line);
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
                        AppConfig.Instance.totalFoundThreats++;
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            File.WriteAllLines(hostsPath_tmp, lines);
                            if (File.Exists(hostsPath_tmp))
                            {
                                UnlockObjectClass.ResetObjectACL(hostsPath_full);
                                File.SetAttributes(hostsPath_full, FileAttributes.Normal);
                                File.Copy(hostsPath_tmp, hostsPath_full, true);
                            }
                            AppConfig.Instance.LL.LogSuccessMessage("_HostsFileRecovered", deletedLineCount.ToString());
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Cured));

                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Skipped));
                            LocalizedLogger.LogScanOnlyMode();
                        }

                    }
                    else if (!AppConfig.Instance.ScanOnly)
                    {
                        LocalizedLogger.LogNoThreatsFound();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    AppConfig.Instance.totalNeutralizedThreats--;
                    string message = AppConfig.Instance.LL.GetLocalizedString("_ErrorLockedFile").Replace("#file#", hostsPath_full);
                    Logger.WriteLog($"\t[!!!] {message}", Logger.caution);
                    scanResults.Add(new ScanResult(ScanObjectType.Infected, hostsPath_full, ScanActionType.Inaccassible));
                }
                catch (Exception e)
                {
                    AppConfig.Instance.totalNeutralizedThreats--;
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCleanHosts", e);
                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Error, e.Message));
                }

                foreach (string tmpFile in Directory.EnumerateFiles(Path.GetDirectoryName(hostsPath_tmp), "hosts*", SearchOption.TopDirectoryOnly))
                {
                    File.Delete(tmpFile);
                }
                MSData.Instance.hStrings.Clear();
            }
        }

        void ScanShellStartup()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanStarupFolder");
            int affected_items = 0;

            string[] wellKnownStartupFolders =
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup),
            };

            foreach (string wkFolder in wellKnownStartupFolders)
            {
                if (string.IsNullOrEmpty(wkFolder)) continue;

                foreach (string file in FileEnumerator.GetFiles(wkFolder, "*.*"))
                {
                    string uncfile = FileSystemManager.NormalizeExtendedPath(file);
                    bool hasInvisibleChars = FileSystemManager.IsOnlyInvisibleCharacters(Path.GetFileNameWithoutExtension(uncfile));

                    if (File.Exists(uncfile) && Path.GetExtension(uncfile) != ".ini")
                    {
                        if (FileChecker.IsShortcut(uncfile))
                        {
                            ShortcutResolver.ShortcutInfo shortcutInfo = ShortcutResolver.GetShortcutInfo(uncfile);
                            AppConfig.Instance.LL.LogMessage("[.]", "_Just_File", $"{Path.GetFileName(uncfile)} --> {shortcutInfo.TargetPath} {shortcutInfo.Arguments}", ConsoleColor.White);

                            if (File.Exists(shortcutInfo.TargetPath))

                            {
                                bool isValid = winTrust.VerifyEmbeddedSignature(shortcutInfo.TargetPath, true) == WinVerifyTrustResult.Success;
                                if (!isValid)
                                {
                                    if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(shortcutInfo.TargetPath), false, false))
                                    {
                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(shortcutInfo.TargetPath));
                                            affected_items++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", shortcutInfo.TargetPath);
                            }


                            if ((shortcutInfo.TargetPath.EndsWith("cmd.exe", StringComparison.OrdinalIgnoreCase) && shortcutInfo.Arguments.StartsWith("/c ", StringComparison.OrdinalIgnoreCase)) || hasInvisibleChars)
                            {

                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", uncfile);
                                AppConfig.Instance.totalFoundThreats++;
                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (!founded_mlwrFiles.Add(uncfile))
                                    {
                                        affected_items++;
                                        AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                                    }
                                }
                                else
                                {
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, uncfile, ScanActionType.Skipped));
                                }
                            }

                            foreach (string arg in MSData.Instance.badArgStrings)
                            {
                                if (shortcutInfo.Arguments.IndexOf(arg, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", uncfile);
                                    AppConfig.Instance.totalFoundThreats++;
                                    affected_items++;

                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        if (!founded_mlwrFiles.Add(uncfile))
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                                        }
                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, uncfile, ScanActionType.Skipped));
                                        break;
                                    }
                                }
                            }
                        }

                        WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(uncfile, true);
                        if (hasInvisibleChars || FileChecker.IsSfxArchive(uncfile) || FileSystemManager.HasHiddenAttribute(uncfile) || (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown && FileChecker.IsDotNetAssembly(uncfile) && FileChecker.CalculateShannonEntropy(File.ReadAllBytes(uncfile)) > 7.6))
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", uncfile);
                            AppConfig.Instance.totalFoundThreats++;
                            affected_items++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                if (!founded_mlwrFiles.Add(uncfile))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                                }
                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, uncfile, ScanActionType.Skipped));
                                break;
                            }

                        }

                        if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(uncfile), false, false))
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", uncfile);
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(uncfile));
                            }
                        }
                    }
                }
            }



            if (affected_items == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }

            if (AppConfig.Instance.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }

        void ScanRegistry()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanRegistry");

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
                        AppConfig.Instance.totalFoundThreats++;
                        AppConfig.Instance.LL.LogWarnMessage("_SuspiciousRegKey", "D?isa?llo?wRun");

                        if (!AppConfig.Instance.ScanOnly)
                        {
                            DisallowRunKey.DeleteValue("Dis?allo?wRun".Replace("?", ""));
                            if (!DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", "Dis?allo?wRun");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, Path.Combine("HKEY_CURRENT_USER", MSData.Instance.queries["ExplorerPolicies"]) + "|DisallowRun", ScanActionType.Deleted));

                                affected_items++;
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, Path.Combine("HKEY_CURRENT_USER", MSData.Instance.queries["ExplorerPolicies"]) + "|DisallowRun", ScanActionType.Skipped));
                        }

                    }

                    if (!AppConfig.Instance.ScanOnly)
                    {
                        RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["ExplorerDisallowRun"], true);
                        if (DisallowRunSub != null)
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            DisallowRunKey.DeleteSubKeyTree("Di?sall?owR?un".Replace("?", ""));
                            DisallowRunSub = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["ExplorerDisallowRun"], true);
                            if (DisallowRunSub == null)
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryKeyRemoved", "Dis?allo?wRun");

                                affected_items++;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "HKCU\\...\\Explorer");
                scanResults.Add(new ScanResult(ScanObjectType.Malware, Path.Combine("HKEY_CURRENT_USER", MSData.Instance.queries["ExplorerPolicies"]) + "|DisallowRun", ScanActionType.Error, ex.Message));

            }

            #endregion

            #region Appinit_dlls
            Logger.WriteLog(@"[Reg] AppInitDLL...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["WindowsNT_CurrentVersion_Windows"], true);
                if (appinit_key != null)
                {
                    object appInitDllsRawValue = appinit_key.GetValue("AppI?nit?_DLLs".Replace("?", ""));
                    if (appInitDllsRawValue != null)
                    {
                        string appInitDllsValue = appInitDllsRawValue.ToString();
                        if (!string.IsNullOrEmpty(appInitDllsValue))
                        {
                            string RequireSignedValueFullPath = Path.Combine("HKEY_LOCAL_MACHINE", MSData.Instance.queries["WindowsNT_CurrentVersion_Windows"]) + "|RequireSignedAppInit_DLLs";
                            if (appinit_key.GetValue("Loa?dAp?p??Init_DLLs".Replace("?", "")).ToString() == "1")
                            {
                                if (!appinit_key.GetValueNames().Contains("RequireSignedApp?Ini?t_D?LLs".Replace("?", "")))
                                {
                                    AppConfig.Instance.LL.LogWarnMessage("_AppInitNotEmpty");
                                    AppConfig.Instance.LL.LogMessage("\t\t[.]", "_File", appInitDllsValue, ConsoleColor.White, false);
                                    AppConfig.Instance.LL.LogCautionMessage("_SignedAppInitNotFound");
                                    AppConfig.Instance.totalFoundThreats++;

                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        appinit_key.SetValue("RequireSignedApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                        if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_ValueWasCreated");
                                            scanResults.Add(new ScanResult(ScanObjectType.Infected, RequireSignedValueFullPath, ScanActionType.Cured));
                                            affected_items++;
                                        }
                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Infected, RequireSignedValueFullPath, ScanActionType.Skipped));
                                    }
                                }
                                else if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "0")
                                {
                                    AppConfig.Instance.LL.LogWarnMessage("_AppInitNotEmpty");
                                    AppConfig.Instance.LL.LogMessage("\t[.]", "_File", appInitDllsValue, ConsoleColor.White, false);
                                    AppConfig.Instance.LL.LogCautionMessage("_SignedAppInitValue");
                                    AppConfig.Instance.totalFoundThreats++;

                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        appinit_key.SetValue("Re?qu?ireSigne?dApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                        if (appinit_key.GetValue("Requi????reSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_ValueSetTo1");
                                            scanResults.Add(new ScanResult(ScanObjectType.Infected, RequireSignedValueFullPath, ScanActionType.Cured));
                                            affected_items++;
                                        }
                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Infected, RequireSignedValueFullPath, ScanActionType.Skipped));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region IFEO
            Logger.WriteLog(@"[Reg] IFEO...", ConsoleColor.DarkCyan);

            RegistryKey IFEOKey = null;

            try
            {
                IFEOKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["IFEO"], true);

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
                                        AppConfig.Instance.totalFoundThreats++;
                                        string silentExitLocalized = AppConfig.Instance.LL.GetLocalizedString("_NoteSilentProcessExitFlag");

                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            subKey.DeleteValue("GlobalFlag");
                                            if (subKey.GetValue("GlobalFlag") == null)
                                            {
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"SilentExit flag: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}", ScanActionType.Deleted, silentExitLocalized));

                                                affected_items++;
                                            }
                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"SilentExit flag: {subKeyName}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}", ScanActionType.Skipped, silentExitLocalized));
                                        }
                                    }


                                    object DebbuggerValue = subKey.GetValue("deb?ug?ger".Replace("?", ""));
                                    if (DebbuggerValue != null && DebbuggerValue is string @string)
                                    {
                                        string _dValue = @string;
                                        AppConfig.Instance.LL.LogMessage("[.]", "_DebuggerString", $"{subKeyName}: {_dValue}", ConsoleColor.Gray);
                                        if (IfeoDbgHelper.ShouldRemoveDbg(_dValue))
                                        {
                                            AppConfig.Instance.totalFoundThreats++;

                                            if (!AppConfig.Instance.ScanOnly)
                                            {
                                                subKey.DeleteValue("deb?ug?ger".Replace("?", ""));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Deleted));
                                            }
                                            else
                                            {
                                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Skipped));
                                            }
                                        }
                                    }

                                    object mmStackValue = subKey.GetValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                    if (mmStackValue != null && mmStackValue is int @int)
                                    {
                                        int _dvalue = @int;
                                        if (_dvalue > 32768)
                                        {
                                            if (!AppConfig.Instance.ScanOnly)
                                            {
                                                subKey.DeleteValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Deleted));
                                            }
                                            else
                                            {
                                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Skipped));
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "IFEO");
            }
            finally
            {
                if (IFEOKey != null)
                {
                    IFEOKey.Dispose();
                }
            }

            #endregion

            #region IFEO_Wow6432
            Logger.WriteLog(@"[Reg] IFEO WOW6432...", ConsoleColor.DarkCyan);

            RegistryKey IFEOKeyWow6432 = null;

            try
            {
                IFEOKeyWow6432 = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["Wow6432Node_IFEO"], true);

                if (IFEOKeyWow6432 != null)
                {
                    foreach (string subKeyName in IFEOKeyWow6432.GetSubKeyNames())
                    {
                        try
                        {
                            using (RegistryKey subKey = IFEOKeyWow6432.OpenSubKey(subKeyName, true))
                            {
                                if (subKey != null)
                                {
                                    object globalFlagValue = subKey.GetValue("GlobalFlag");
                                    if (globalFlagValue != null && globalFlagValue is int && (int)globalFlagValue == 0x200)
                                    {
                                        AppConfig.Instance.totalFoundThreats++;
                                        string silentExitLocalized = AppConfig.Instance.LL.GetLocalizedString("_NoteSilentProcessExitFlag");

                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            subKey.DeleteValue("GlobalFlag");
                                            if (subKey.GetValue("GlobalFlag") == null)
                                            {
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"SilentExit flag: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}".Replace("?", ""), ScanActionType.Deleted, silentExitLocalized));

                                                affected_items++;
                                            }
                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"SilentExit flag: {subKeyName}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"SilentExit flag: {subKeyName}".Replace("?", ""), ScanActionType.Skipped, silentExitLocalized));
                                        }
                                    }


                                    object DebbuggerValue = subKey.GetValue("deb?ug?ger".Replace("?", ""));
                                    if (DebbuggerValue != null && DebbuggerValue is string @string)
                                    {
                                        string _dValue = @string;
                                        AppConfig.Instance.LL.LogMessage("[.]", "_DebuggerString", $"{subKeyName}: {_dValue}", ConsoleColor.Gray);
                                        if (IfeoDbgHelper.ShouldRemoveDbg(_dValue))
                                        {
                                            AppConfig.Instance.totalFoundThreats++;

                                            if (!AppConfig.Instance.ScanOnly)
                                            {
                                                subKey.DeleteValue("deb?ug?ger".Replace("?", ""));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Deleted));
                                            }
                                            else
                                            {
                                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{"deb?ug?ger".Replace("?", "")}: {subKeyName} {_dValue}", ScanActionType.Skipped));
                                            }
                                        }
                                    }

                                    object mmStackValue = subKey.GetValue("Min?imumSta?ckC?om?mit?InB?ytes".Replace("?", ""));
                                    if (mmStackValue != null && mmStackValue is int @int)
                                    {
                                        int _dvalue = @int;
                                        if (_dvalue > 32768)
                                        {
                                            string invalidValueLocalized = AppConfig.Instance.LL.GetLocalizedString("_NoteInvalidRegistryValue");

                                            if (!AppConfig.Instance.ScanOnly)
                                            {
                                                subKey.DeleteValue("Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", ""));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Deleted, invalidValueLocalized));
                                            }
                                            else
                                            {
                                                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousEntry", $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{"Min?imu?mSt?ack?Com?mit?InB?yte?s".Replace("?", "")}: {subKeyName}", ScanActionType.Skipped, invalidValueLocalized));
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "IFEO");

            }
            finally
            {
                if (IFEOKeyWow6432 != null)
                {
                    IFEOKeyWow6432.Dispose();
                }
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
                                        AppConfig.Instance.totalFoundThreats++;

                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            baseKey.DeleteSubKey(subKeyName);
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"MonitorProcess: {subKeyName} -> {monitorProcessValue}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"MonitorProcess: {subKeyName} -> {monitorProcessValue}", ScanActionType.Deleted));
                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", $"MonitorProcess: {subKeyName} -> {monitorProcessValue}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"MonitorProcess: {subKeyName} -> {monitorProcessValue}", ScanActionType.Skipped));
                                        }
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "Silent_Exit_process");
            }
            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKeyHKLM = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["StartupRun"], true);
                if (AutorunKeyHKLM != null)
                {
                    Logger.WriteLog(@"[Reg] HKLM Autorun...", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKeyHKLM.GetValueNames().ToList();
                    string HKLMRunKeyFullPath = Path.Combine("HKEY_LOCAL_MACHINE", MSData.Instance.queries["StartupRun"]);

                    foreach (string value in RunKeys)
                    {
                        string path = "";
                        string AutorunKeyValue = AutorunKeyHKLM.GetValue(value).ToString();
                        try
                        {
                            path = FileSystemManager.ExtractExecutableFromCommand(AutorunKeyHKLM.GetValue(value) as string);
                        }
                        catch (ArgumentException)
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_InvalidPathCharactersFound", AutorunKeyValue + $" -> \"{value}\"");
                        }

                        if (string.IsNullOrEmpty(path))
                            continue;

                        AppConfig.Instance.LL.LogMessage("\t[.]", "_Just_File", $"{value} {AutorunKeyHKLM.GetValue(value)}", ConsoleColor.Gray);

                        if (AutorunKeyValue.IndexOf("Re??a?l?te?kH?D\\t?ask".Replace("?", ""), StringComparison.InvariantCultureIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("Re?aI?te??kHD\\tas?k".Replace("?", ""), StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            AppConfig.Instance.totalFoundThreats++;

                            string valuename = value;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                AutorunKeyHKLM.DeleteValue(value);
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", valuename);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{valuename}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", valuename);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{valuename}", ScanActionType.Skipped));

                            }

                        }


                        if (AutorunKeyValue.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            AppConfig.Instance.totalFoundSuspiciousObjects++;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                AutorunKeyHKLM.DeleteValue(value);
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));

                            }
                        }

                        if ((AutorunKeyValue.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && (AutorunKeyValue.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0)))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                if (AutorunKeyHKLM.GetValue(value) != null)
                                {
                                    AutorunKeyHKLM.DeleteValue(value);
                                    if (AutorunKeyHKLM.GetValue(value) == null)
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                        affected_items++;
                                    }
                                }
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));

                            }
                        }

                        if (File.Exists(path))
                        {
                            if (FileSystemManager.IsAppExecutionAlias(path))
                            {
                                continue;
                            }

                            WinVerifyTrustResult trustResult = WinVerifyTrustResult.ActionUnknown;
                            trustResult = winTrust.VerifyEmbeddedSignature(path, true);

                            long fileSize = new FileInfo(path).Length;
                            bool IsFileTooBig = fileSize >= maxFileSize;
                            bool IsHidden = FileSystemManager.HasHiddenAttribute(path);
                            bool IsUnsigned = (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown);
                            bool IsDotnet = FileChecker.IsDotNetAssembly(path);
                            bool HasSuspicousPath = path.Contains("Temp") || path.Contains("ProgramData") || path.Contains("AppData");

                            bool hasGuidIdentifier = false;

                            string filePathDir = Path.GetDirectoryName(path);
                            if (!string.IsNullOrEmpty(filePathDir) && Directory.Exists(filePathDir))
                            {
                                hasGuidIdentifier = Directory.EnumerateFiles(filePathDir, "*", SearchOption.TopDirectoryOnly).Select(Path.GetFileNameWithoutExtension).Any(n => n != null && Guid.TryParse(n, out _));
                            }

                            bool hasMultiExe = FileSystemManager.HasMultipleExeExtensions(path);
                            bool isUpx = FileChecker.IsUpxPacked(path);

                            bool IsHignEntropy = FileChecker.CalculateShannonEntropy(File.ReadAllBytes(path)) > 7.6;

                            if ((IsFileTooBig && IsUnsigned) || FileChecker.IsSfxArchive(path) || IsHidden || (IsUnsigned && IsDotnet && IsHignEntropy && HasSuspicousPath))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (AutorunKeyHKLM.GetValue(value) != null)
                                    {
                                        AutorunKeyHKLM.DeleteValue(value);
                                        if (AutorunKeyHKLM.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                            continue;
                                        }
                                    }

                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }


                            }

                            if ((IsUnsigned && isUpx) && (IsHidden || hasGuidIdentifier || hasMultiExe))
                            {
                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                    AppConfig.Instance.totalFoundThreats++;
                                    affected_items++;

                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (AutorunKeyHKLM.GetValue(value) != null)
                                    {
                                        AutorunKeyHKLM.DeleteValue(value);
                                        if (AutorunKeyHKLM.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }
                            }

                            if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(path), false))
                            {
                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));


                                    if (AutorunKeyHKLM.GetValue(value) != null)
                                    {
                                        AutorunKeyHKLM.DeleteValue(value);
                                        if (AutorunKeyHKLM.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                        }
                                    }

                                    affected_items++;
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }
                            }

                            if (trustResult == WinVerifyTrustResult.Success)
                            {
                                string autorunValueDir = Path.GetDirectoryName(path);
                                if (Directory.Exists(autorunValueDir))
                                {
                                    foreach (string dll in Directory.EnumerateFiles(autorunValueDir, "*.dll"))
                                    {
                                        string dllName = Path.GetFileName(dll);
                                        if (Array.Exists(MSData.Instance.sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
                                        {
                                            WinVerifyTrustResult dllSignature = winTrust.VerifyEmbeddedSignature(dll);

                                            if (dllSignature != WinVerifyTrustResult.Success)
                                            {
                                                AppConfig.Instance.LL.LogCautionMessage("_Found", AppConfig.Instance.LL.GetLocalizedString("_ValidFileDllHijacking").Replace("#FILENAME#", path).Replace("#DLLNAME#", dll));
                                                AppConfig.Instance.totalFoundThreats++;

                                                if (!AppConfig.Instance.ScanOnly)
                                                {
                                                    UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));
                                                    founded_mlwrFiles.Add(path);
                                                    UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(dll));
                                                    founded_mlwrFiles.Add(dll);

                                                    if (AutorunKeyHKLM.GetValue(value) != null)
                                                    {
                                                        AutorunKeyHKLM.DeleteValue(value);
                                                        if (AutorunKeyHKLM.GetValue(value) == null)
                                                        {
                                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                                            affected_items++;
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{HKLMRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKeyHKLM.GetValue(value)} | {value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "HKLM\\...\\run");

            }

            Logger.WriteLog(@"[Reg] HKLM System settings...", ConsoleColor.DarkCyan);
            try
            {
                string regPath = MSData.Instance.queries["WindowsNT_CurrentVersion_Winlogon"];

                using (RegistryKey WinLogonKey = Registry.LocalMachine.OpenSubKey(regPath, true))
                {
                    if (WinLogonKey != null)
                    {
                        object UserinitValue = WinLogonKey.GetValue("Userinit");
                        if (UserinitValue != null)
                        {
                            string UserinitData = UserinitValue.ToString();
                            if (!string.IsNullOrEmpty(UserinitData))
                            {
                                string defaultData = $@"{AppConfig.Instance.drive_letter}:\windows\system32\userinit.exe,";
                                if (!UserinitData.Equals(defaultData, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    AppConfig.Instance.totalFoundThreats++;
                                    affected_items++;

                                    AppConfig.Instance.LL.LogWarnMediumMessage("_WarnUserinitHijacked", UserinitData);
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        try
                                        {
                                            WinLogonKey.SetValue("Userinit", defaultData);
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRestoredDefault");
                                            scanResults.Add(new ScanResult(ScanObjectType.Unsafe, Path.Combine("HKEY_LOCAL_MACHINE", regPath, "Userinit"), ScanActionType.Cured));
                                        }
                                        catch (Exception ex)
                                        {
                                            AppConfig.Instance.totalFoundThreats--;
                                            AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, "HKLM\\...\\Userinit");
                                        }
                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Unsafe, Path.Combine("HKEY_LOCAL_MACHINE", regPath, "Userinit"), ScanActionType.Skipped));
                                    }
                                }
                            }
                        }

                        object ShellValue = WinLogonKey.GetValue("Shell");
                        if (ShellValue != null)
                        {
                            string ShellData = ShellValue.ToString();
                            if (!string.IsNullOrEmpty(ShellData))
                            {
                                string defaultDataVariant1 = "explorer.exe";
                                string defaultDataVariant2 = $"{AppConfig.Instance.drive_letter}:\\Windows\\explorer.exe";
                                if (!ShellData.Equals(defaultDataVariant1, StringComparison.InvariantCultureIgnoreCase) && !ShellData.Equals(defaultDataVariant2, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    AppConfig.Instance.totalFoundThreats++;
                                    affected_items++;

                                    AppConfig.Instance.LL.LogWarnMediumMessage("_WarnShellValueHijacked", ShellData);
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        try
                                        {
                                            WinLogonKey.SetValue("Shell", defaultDataVariant1);
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRestoredDefault");
                                            scanResults.Add(new ScanResult(ScanObjectType.Unsafe, Path.Combine("HKEY_LOCAL_MACHINE", regPath, "Shell"), ScanActionType.Cured));
                                        }
                                        catch (Exception ex)
                                        {
                                            AppConfig.Instance.totalFoundThreats--;
                                            AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, "HKLM\\...\\Shell");
                                        }
                                    }
                                    else
                                    {
                                        scanResults.Add(new ScanResult(ScanObjectType.Unsafe, Path.Combine("HKEY_LOCAL_MACHINE", regPath, "Shell"), ScanActionType.Skipped));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKLM\\...\\Winlogon");
            }

            #endregion

            #region HKCU
            Logger.WriteLog(@"[Reg] HKCU Autorun...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["StartupRun"], true);
                string RunKeyFullPath = Path.Combine("HKEY_CURRENT_USER", MSData.Instance.queries["StartupRun"]);

                if (AutorunKey != null)
                {

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = "";
                        string AutorunKeyValue = AutorunKey.GetValue(value).ToString();

                        try
                        {
                            path = FileSystemManager.ExtractExecutableFromCommand(AutorunKey.GetValue(value) as string);
                        }
                        catch (ArgumentException)
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_InvalidPathCharactersFound", AutorunKeyValue + $" -> \"{value}\"");
                        }
                        if (string.IsNullOrEmpty(path))
                            continue;

                        AppConfig.Instance.LL.LogMessage("\t[.]", "_Just_File", $"{value} {AutorunKeyValue}", ConsoleColor.Gray);

                        if (AutorunKeyValue.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            AppConfig.Instance.totalFoundSuspiciousObjects++;
                            affected_items++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, RunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, RunKeyFullPath + $"|{value}", ScanActionType.Skipped));

                            }
                        }

                        if ((AutorunKeyValue.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && (AutorunKeyValue.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0)))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            affected_items++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                if (AutorunKey.GetValue(value) == null)
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                                }
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Skipped));

                            }
                        }


                        if (File.Exists(path))
                        {
                            if (FileSystemManager.IsAppExecutionAlias(path))
                            {
                                continue;
                            }

                            WinVerifyTrustResult trustResult = WinVerifyTrustResult.ActionUnknown;
                            trustResult = winTrust.VerifyEmbeddedSignature(path, true);

                            long fileSize = new FileInfo(path).Length;
                            bool IsFileTooBig = fileSize >= maxFileSize;
                            bool IsHidden = FileSystemManager.HasHiddenAttribute(path);
                            bool IsUnsigned = (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown);
                            bool IsDotnet = FileChecker.IsDotNetAssembly(path);
                            bool HasSuspicousPath = path.Contains("Temp") || path.Contains("ProgramData") || path.Contains("AppData");

                            bool hasGuidIdentifier = false;

                            string filePathDir = Path.GetDirectoryName(path);
                            if (!string.IsNullOrEmpty(filePathDir) && Directory.Exists(filePathDir))
                            {
                                hasGuidIdentifier = Directory.EnumerateFiles(filePathDir, "*", SearchOption.TopDirectoryOnly).Select(Path.GetFileNameWithoutExtension).Any(n => n != null && Guid.TryParse(n, out _));
                            }

                            bool hasMultiExe = FileSystemManager.HasMultipleExeExtensions(path);
                            bool isUpx = FileChecker.IsUpxPacked(path);

                            bool IsHignEntropy = FileChecker.CalculateShannonEntropy(File.ReadAllBytes(path)) > 7.6;

                            if ((IsFileTooBig && IsUnsigned) || FileChecker.IsSfxArchive(path) || IsHidden || (IsUnsigned && IsDotnet && IsHignEntropy && HasSuspicousPath))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (AutorunKey.GetValue(value) != null)
                                    {
                                        AutorunKey.DeleteValue(value);
                                        if (AutorunKey.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                                        }
                                    }

                                    continue;
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Skipped));
                                }


                            }

                            if ((IsUnsigned && isUpx) && (IsHidden || hasGuidIdentifier || hasMultiExe))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (AutorunKey.GetValue(value) != null)
                                    {
                                        AutorunKey.DeleteValue(value);
                                        if (AutorunKey.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RunKeyFullPath}|{value}", ScanActionType.Deleted));
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }
                            }

                            if (trustResult == WinVerifyTrustResult.Success)
                            {
                                string autorunValueDir = Path.GetDirectoryName(path);
                                if (Directory.Exists(autorunValueDir))
                                {
                                    foreach (string dll in from string dll in Directory.EnumerateFiles(autorunValueDir, "*.dll")
                                                           let dllName = Path.GetFileName(dll)
                                                           where Array.Exists(MSData.Instance.sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                                                           let dllSignature = winTrust.VerifyEmbeddedSignature(dll)
                                                           where dllSignature != WinVerifyTrustResult.Success
                                                           select dll)
                                    {
                                        AppConfig.Instance.LL.LogCautionMessage("_Found", AppConfig.Instance.LL.GetLocalizedString("_ValidFileDllHijacking").Replace("#FILENAME#", path).Replace("#DLLNAME#", dll));
                                        AppConfig.Instance.totalFoundThreats++;
                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));
                                            founded_mlwrFiles.Add(path);
                                            UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(dll));
                                            founded_mlwrFiles.Add(dll);

                                            AutorunKey.DeleteValue(value);
                                            if (AutorunKey.GetValue(value) == null)
                                            {
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                                                affected_items++;
                                            }

                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Skipped));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(path), false))
                                {
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));

                                        AutorunKey.DeleteValue(value);
                                        if (AutorunKey.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                                        }

                                        affected_items++;
                                    }
                                    else
                                    {
                                        AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, RunKeyFullPath + $"|{value}", ScanActionType.Skipped));
                                    }
                                }
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "HKCU\\...\\run");
            }


            Logger.WriteLog(@"[Reg] HKCU System policies...", ConsoleColor.DarkCyan);
            try
            {
                string regPath = MSData.Instance.queries["SystemPolicies"];
                string regPathFull = Path.Combine("HKEY_CURRENT_USER", regPath);
                string[] valueNames = { "Di?sable?TaskM?gr".Replace("?", ""), "Di??sab?leRe?gistr??yToo?ls".Replace("?", "") };


                using (RegistryKey systemKey = Registry.CurrentUser.OpenSubKey(regPath, true))
                {
                    if (systemKey != null)
                    {
                        for (int v = 0; v < valueNames.Length; v++)
                        {
                            string policiesVal = valueNames[v];
                            if (systemKey.GetValue(policiesVal) != null)
                            {
                                AppConfig.Instance.totalFoundThreats++;
                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    systemKey.DeleteValue(policiesVal, false);
                                    if (systemKey.GetValue(policiesVal) == null)
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", policiesVal);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{regPathFull}|{policiesVal}", ScanActionType.Deleted, v == 0 ? AppConfig.Instance.LL.GetLocalizedString("_Note_TaskManager") : AppConfig.Instance.LL.GetLocalizedString("_Note_RegTools")));
                                        affected_items++;
                                    }
                                    else
                                    {
                                        AppConfig.Instance.totalNeutralizedThreats--;
                                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", null, policiesVal);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{regPathFull}|{policiesVal}", ScanActionType.Inaccassible, v == 0 ? AppConfig.Instance.LL.GetLocalizedString("_Note_TaskManager") : AppConfig.Instance.LL.GetLocalizedString("_Note_RegTools")));
                                    }

                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", policiesVal);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{regPathFull}|{policiesVal}", ScanActionType.Skipped, v == 0 ? AppConfig.Instance.LL.GetLocalizedString("_Note_TaskManager") : AppConfig.Instance.LL.GetLocalizedString("_Note_RegTools")));
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "HKCU\\...\\System");
            }


            StringBuilder sb = new StringBuilder();
            sb.Append("tek").Append("to").Append("nit");
            string subkeyNameTektonit = sb.ToString();
            bool isMaliciousSubkey = false;

            try
            {

                Logger.WriteLog($"[Reg] {subkeyNameTektonit}...", ConsoleColor.DarkCyan);

                using (RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(MSData.Instance.queries["Tekt0nitParameters"], true))
                {
                    if (tektonit != null)
                    {
                        object rfusClient = tektonit.GetValue("FUSClientPath");
                        if (rfusClient != null)
                        {
                            string clientPath = rfusClient.ToString();
                            bool fileSigned = false;
                            bool isValidPath = true;
                            bool hasUnofficialParams = false;

                            if (File.Exists(clientPath))
                            {
                                WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(clientPath);
                                if (trustResult == WinVerifyTrustResult.Success)
                                {
                                    fileSigned = true;
                                }
                            }

                            if (clientPath.IndexOf("programdata", StringComparison.OrdinalIgnoreCase) >= 0 || clientPath.IndexOf("appdata", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                isValidPath = false;
                            }

                            foreach (var valueName in tektonit.GetValueNames())
                            {
                                if (Utils.ContainsNonAscii(valueName))
                                {
                                    hasUnofficialParams = true;
                                    break;
                                }
                            }

                            if (!isValidPath || hasUnofficialParams || !fileSigned)
                            {
                                isMaliciousSubkey = true;
                            }
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                AppConfig.Instance.LL.LogWarnMessage("_SuspiciousRegKey", subkeyNameTektonit);
                isMaliciousSubkey = true;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.totalNeutralizedThreats--;
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", ex, $"HKCU\\...\\{subkeyNameTektonit}");
                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_RegistryKey")} {subkeyNameTektonit}".Replace("?", ""), ScanActionType.Error, ex.Message));

            }

            if (isMaliciousSubkey)
            {
                AppConfig.Instance.totalFoundThreats++;

                if (!AppConfig.Instance.ScanOnly)
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

                    using (RegistryKey baseKey = Registry.CurrentUser.OpenSubKey("Software", true))
                    {
                        if (baseKey == null)
                            return;

                        using (RegistryKey target = baseKey.OpenSubKey(subkeyNameTektonit))
                        {
                            if (target == null)
                                return;

                            if (target.SubKeyCount > 0)
                                baseKey.DeleteSubKeyTree(subkeyNameTektonit);
                            else
                                baseKey.DeleteSubKey(subkeyNameTektonit);
                        }

                        RegistryKey deletedTarget = baseKey.OpenSubKey(subkeyNameTektonit);
                        if (deletedTarget == null)
                        {
                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", subkeyNameTektonit);
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_RegistryKey")} {subkeyNameTektonit}".Replace("?", ""), ScanActionType.Deleted));

                            affected_items++;
                        }

                    }
                }
                else
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", subkeyNameTektonit);
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_RegistryKey")} {subkeyNameTektonit}".Replace("?", ""), ScanActionType.Skipped));
                }
            }

            #endregion

            #region Applocker

            Logger.WriteLog(@"[Reg] Applocker...", ConsoleColor.DarkCyan);
            string registryPath = MSData.Instance.queries["appl0cker"];

            bool isContainsBadPolicies = false;

            List<string> allSubkeys = AppConfig.Instance._utils.GetSubkeys(registryPath);

            if (allSubkeys.Count > 0)
            {
                using (RegistryKey parentKey = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    foreach (string subkeyName in allSubkeys)
                    {
                        if (MSData.Instance.badSubkeys.Contains(subkeyName, StringComparer.OrdinalIgnoreCase))
                        {
                            isContainsBadPolicies = true;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                try
                                {
                                    parentKey.DeleteSubKeyTree(subkeyName);
                                    if (!Utils.RegistryKeyExists(registryPath))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", subkeyName);
                                        affected_items++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", ex, subkeyName);
                                }
                            }
                        }
                    }

                    if (isContainsBadPolicies)
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, Path.Combine("HKEY_LOCAL_MACHINE", registryPath), ScanActionType.Cured, AppConfig.Instance.LL.GetLocalizedString("_Note_ApplockerHasBadSettings")));
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_WarnApplockerHaveChanges");
                            scanResults.Add(new ScanResult(ScanObjectType.Infected, Path.Combine("HKEY_LOCAL_MACHINE", registryPath), ScanActionType.Skipped, AppConfig.Instance.LL.GetLocalizedString("_Note_ApplockerHasBadSettings")));
                        }
                    }
                }
            }

            #endregion

            #region WindowsDefender
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
                    RegistryKey winDefenderKey;
                    try
                    {
                        winDefenderKey = baseKey == baseKeys[0] ? Registry.LocalMachine.OpenSubKey(baseKey) : Registry.LocalMachine.OpenSubKey(baseKey, true);
                        if (winDefenderKey == null) continue;
                    }
                    catch (SecurityException uax)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", uax, baseKey);
                        continue;
                    }

                    foreach (string subKey in subKeys)
                    {
                        string fullKeyPath = baseKey + "\\" + subKey;
                        RegistryKey key;
                        try
                        {
                            key = baseKey == baseKeys[0] ? Registry.LocalMachine.OpenSubKey(fullKeyPath) : Registry.LocalMachine.OpenSubKey(fullKeyPath, true);
                            if (key == null) continue;
                        }
                        catch (SecurityException uax)
                        {
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", uax, subKey);
                            continue;
                        }


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
                                try
                                {
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        if (baseKey == MSData.Instance.queries["WDExclusionsLocal"])
                                        {

                                            Utils.RemoveDefenderExclusion(subKey, valueName);
                                            if (key.GetValue(valueName) == null)
                                            {
                                                scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"{AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?","")} (Local) -> " + valueName, ScanActionType.Deleted));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{valueName} (local)");
                                                affected_items++;
                                                AppConfig.Instance.totalFoundThreats++;
                                            }
                                            else
                                            {
                                                AppConfig.Instance.LL.LogWarnMediumMessage("_ErrorCannotRemove", $"{valueName} (local)");
                                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?", "")} (Local) -> " + valueName, ScanActionType.Inaccassible, AppConfig.Instance.LL.GetLocalizedString("_ErrorTurnedOffDefender").Replace("?","")));
                                                AppConfig.Instance.totalFoundSuspiciousObjects++;
                                            }
                                        }
                                        else
                                        {
                                            key.DeleteValue(valueName);
                                            if (key.GetValue(valueName) == null)
                                            {
                                                scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"{AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?", "")} (Policies) -> " + valueName, ScanActionType.Deleted));
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", $"{valueName} (policy)");
                                                affected_items++;
                                                AppConfig.Instance.totalFoundThreats++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AppConfig.Instance.totalFoundThreats++;
                                        AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", valueName);
                                        scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"{AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?", "")} -> " + valueName, ScanActionType.Skipped));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, valueName, AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?", ""));
                                    scanResults.Add(new ScanResult(ScanObjectType.Unsafe, $"{AppConfig.Instance.LL.GetLocalizedString("_UnwantedExclusion").Replace("?", "")} -> " + valueName, ScanActionType.Error, ex.Message));
                                }
                            }

                        }

                        key.Close();
                    }

                    if (winDefenderKey != null) winDefenderKey.Close();
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey WOW64AutorunKey = Registry.LocalMachine.OpenSubKey(MSData.Instance.queries["Wow6432Node_StartupRun"], true);
                string RegRunKeyFullPath = Path.Combine("HKEY_LOCAL_MACHINE", MSData.Instance.queries["Wow6432Node_StartupRun"]);
                if (WOW64AutorunKey != null)
                {
                    Logger.WriteLog(@"[Reg] Wow64Node Autorun...", ConsoleColor.DarkCyan);

                    List<string> RunKeys = WOW64AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = "";
                        string AutorunKeyValue = WOW64AutorunKey.GetValue(value).ToString();

                        try
                        {
                            path = FileSystemManager.ExtractExecutableFromCommand(WOW64AutorunKey.GetValue(value) as string);
                        }
                        catch (ArgumentException)
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_InvalidPathCharactersFound", AutorunKeyValue + $" -> \"{value}\"");
                        }
                        if (string.IsNullOrEmpty(path))
                            continue;

                        AppConfig.Instance.LL.LogMessage("\t[.]", "_Just_File", $"{value} {AutorunKeyValue}", ConsoleColor.Gray);

                        if (AutorunKeyValue.IndexOf("explorer.exe ", StringComparison.OrdinalIgnoreCase) >= 0 || AutorunKeyValue.IndexOf("cmd.exe /c ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            AppConfig.Instance.totalFoundSuspiciousObjects++;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                WOW64AutorunKey.DeleteValue(value);
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                affected_items++;
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));

                            }
                        }

                        if ((AutorunKeyValue.IndexOf(" /c cd ", StringComparison.OrdinalIgnoreCase) >= 0 && (AutorunKeyValue.IndexOf(" && ", StringComparison.OrdinalIgnoreCase) >= 0)))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                WOW64AutorunKey.DeleteValue(value);
                                if (WOW64AutorunKey.GetValue(value) == null)
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                    affected_items++;
                                }
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));

                            }
                        }

                        if (File.Exists(path))
                        {
                            if (FileSystemManager.IsAppExecutionAlias(path))
                            {
                                continue;
                            }

                            WinVerifyTrustResult trustResult = WinVerifyTrustResult.ActionUnknown;
                            trustResult = winTrust.VerifyEmbeddedSignature(path, true);

                            long fileSize = new FileInfo(path).Length;
                            bool IsFileTooBig = fileSize >= maxFileSize;
                            bool IsHidden = FileSystemManager.HasHiddenAttribute(path);
                            bool IsUnsigned = (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown);
                            bool IsDotnet = FileChecker.IsDotNetAssembly(path);
                            bool HasSuspicousPath = path.Contains("Temp") || path.Contains("ProgramData") || path.Contains("AppData");

                            bool hasGuidIdentifier = false;

                            string filePathDir = Path.GetDirectoryName(path);
                            if (!string.IsNullOrEmpty(filePathDir) && Directory.Exists(filePathDir))
                            {
                                hasGuidIdentifier = Directory.EnumerateFiles(filePathDir, "*", SearchOption.TopDirectoryOnly).Select(Path.GetFileNameWithoutExtension).Any(n => n != null && Guid.TryParse(n, out _));
                            }

                            bool hasMultiExe = FileSystemManager.HasMultipleExeExtensions(path);
                            bool isUpx = FileChecker.IsUpxPacked(path);

                            bool IsHignEntropy = FileChecker.CalculateShannonEntropy(File.ReadAllBytes(path)) > 7.6;

                            if ((IsFileTooBig && IsUnsigned) || FileChecker.IsSfxArchive(path) || IsHidden || (IsUnsigned && IsDotnet && IsHignEntropy && HasSuspicousPath))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (WOW64AutorunKey.GetValue(value) != null)
                                    {
                                        WOW64AutorunKey.DeleteValue(value);
                                        if (WOW64AutorunKey.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, RegRunKeyFullPath + $"|{value}", ScanActionType.Deleted));
                                        }
                                    }

                                    continue;
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, RegRunKeyFullPath + $"|{value}", ScanActionType.Skipped));
                                }


                            }

                            if ((IsUnsigned && isUpx) && (IsHidden || hasGuidIdentifier || hasMultiExe))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (WOW64AutorunKey.GetValue(value) != null)
                                    {
                                        WOW64AutorunKey.DeleteValue(value);
                                        if (WOW64AutorunKey.GetValue(value) == null)
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }
                            }

                            if ((fileSize >= maxFileSize && trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown) || FileChecker.IsSfxArchive(path) || FileSystemManager.HasHiddenAttribute(path) || ((trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.ActionUnknown) && FileChecker.IsDotNetAssembly(path) && FileChecker.CalculateShannonEntropy(File.ReadAllBytes(path)) > 7.6))
                            {
                                AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", path);
                                AppConfig.Instance.totalFoundThreats++;
                                affected_items++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(path))
                                    {
                                        UnlockObjectClass.DisableExecute(path);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    WOW64AutorunKey.DeleteValue(value);
                                    if (WOW64AutorunKey.GetValue(value) == null)
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                    }

                                    continue;
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }


                            }

                            if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(path), false))
                            {
                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));

                                    WOW64AutorunKey.DeleteValue(value);
                                    if (WOW64AutorunKey.GetValue(value) == null)
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                    }

                                    affected_items++;
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                }
                            }

                            if (trustResult == WinVerifyTrustResult.Success)
                            {
                                string autorunValueDir = Path.GetDirectoryName(path);
                                if (Directory.Exists(autorunValueDir))
                                {
                                    foreach (string dll in from string dll in Directory.EnumerateFiles(autorunValueDir, "*.dll")
                                                           let dllName = Path.GetFileName(dll)
                                                           where Array.Exists(MSData.Instance.sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                                                           let dllSignature = winTrust.VerifyEmbeddedSignature(dll)
                                                           where dllSignature != WinVerifyTrustResult.Success
                                                           select dll)
                                    {
                                        AppConfig.Instance.LL.LogCautionMessage("_Found", AppConfig.Instance.LL.GetLocalizedString("_ValidFileDllHijacking").Replace("#FILENAME#", path).Replace("#DLLNAME#", dll));
                                        AppConfig.Instance.totalFoundThreats++;
                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(path));
                                            founded_mlwrFiles.Add(path);
                                            UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(dll));
                                            founded_mlwrFiles.Add(dll);

                                            WOW64AutorunKey.DeleteValue(value);
                                            if (WOW64AutorunKey.GetValue(value) == null)
                                            {
                                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryValueRemoved", value);
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Deleted));
                                                affected_items++;
                                            }

                                        }
                                        else
                                        {
                                            AppConfig.Instance.LL.LogWarnMediumMessage("_FoundMlwrKey", value);
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{RegRunKeyFullPath}|{value}", ScanActionType.Skipped));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", $"{WOW64AutorunKey.GetValue(value)} | {value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotOpen", ex, "WOW6432?Node\\...\\run");
            }
            #endregion

            if (affected_items == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }

            if (AppConfig.Instance.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }

        void ScanTaskScheduler()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanTasks");

            string[] checkDirectories =
            {
                    Environment.SystemDirectory,
                    new StringBuilder(AppConfig.Instance.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("WO").Append("W6").Append("4").ToString(),
                    new StringBuilder(AppConfig.Instance.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("te").Append("m3").Append("2\\").Append("wb").Append("em").ToString(),
                    MSData.Instance.queries["PowerShellPath"],
                };

            SafeModeTaskParser parser = new SafeModeTaskParser();
            List<ScheduledTaskInfo> allTasks = parser.GetAllTasks();

            if (allTasks == null || allTasks.Count == 0)
            {
                AppConfig.Instance.LL.LogErrorMessage("SafeModeTaskParser");
                return;
            }

            foreach (ScheduledTaskInfo task in allTasks.OrderBy(t => t.Name))
            {
                if (task.ExecActions.Count == 0) continue;

                string taskName = task.Name;
                string taskFolder = task.Path;
                bool breakOuterLoop = false; // exit flag if the task was deleted

                foreach (ExecActionInfo action in task.ExecActions)
                {
                    if (string.IsNullOrEmpty(action.Path)) continue;

                    string arguments = action.Arguments ?? string.Empty;
                    AppConfig.Instance.LL.LogMessage("[#]", "_Scanning", $"{taskName} | {taskFolder}", ConsoleColor.White);

                    if (!AppConfig.Instance.ScanOnly)
                    {
                        bool suspiciousFound = false;

                        if (taskName.StartsWith("d?ia?le?r".Replace("?", ""))) suspiciousFound = true;
                        if (!suspiciousFound)
                        {
                            foreach (string arg in MSData.Instance.badArgStrings)
                            {
                                if (arguments.IndexOf(arg, StringComparison.OrdinalIgnoreCase) >= 0) { suspiciousFound = true; break; }
                            }
                        }
                        if (!suspiciousFound && arguments.IndexOf("/c reg add ", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            suspiciousFound = true;
                        }

                        if (!suspiciousFound && arguments.IndexOf("-jar ", StringComparison.OrdinalIgnoreCase) >= 0 && arguments.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            suspiciousFound = true;
                        }

                        if (suspiciousFound)
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", taskName);
                            AppConfig.Instance.totalFoundThreats++;
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {taskFolder}\\{taskName}", ScanActionType.Error));
                                AppConfig.Instance.totalNeutralizedThreats--;
                            }
                            breakOuterLoop = true;
                            break;
                        }
                    }

                    string filePath = Environment.ExpandEnvironmentVariables(action.Path.Replace("\"", ""));
                    string resolvedPath = FileSystemManager.ResolveExecutablePath(filePath);

                    if (resolvedPath != null && File.Exists(resolvedPath))
                    {
                        ProcessFilePath(resolvedPath, arguments, parser, task);
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", filePath);
                        if (AppConfig.Instance.RemoveEmptyTasks)
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_EmptyTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Unknown, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {taskFolder}\\{taskName}", ScanActionType.Deleted, AppConfig.Instance.LL.GetLocalizedString("_EmptyTask")));
                                AppConfig.Instance.hasEmptyTasks = true;
                            }
                            breakOuterLoop = true;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(arguments))
                    {
                        if (FileSystemManager.ProcessFileFromArgs(checkDirectories, filePath, arguments))
                        {
                            if (!AppConfig.Instance.ScanOnly)
                            {


                                if (parser.DeleteTaskDirectly(task))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{taskFolder}\\{taskName}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                }
                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {taskFolder}\\{taskName}", ScanActionType.Skipped));
                            }
                        }
                    }
                }
                if (breakOuterLoop) continue;
            }
        }

        void ScanWMI()
        {
            AppConfig.Instance.LL.LogHeadMessage(@"_WMIHead");

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
                    AppConfig.Instance.totalFoundThreats += results.Count;
                    string wmiObjName = "";
                    string wmiObjpath = "";
                    foreach (ManagementObject obj in results)
                    {
                        wmiObjName = (string)obj["Name"];
                        wmiObjpath = (string)obj["CommandLineTemplate"];
                        AppConfig.Instance.LL.LogWarnMediumMessage("_WMIEvent", $"{wmiObjName} -> {wmiObjpath}");
                        obj.Delete();
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"WMIEvent: {wmiObjName} -> {wmiObjpath}", ScanActionType.Deleted));
                        AppConfig.Instance.LL.LogSuccessMessage("_WMIEvent", $"{wmiObjName}", "_Deleted");

                    }
                }
                else
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }

        }

        void ProcessFilePath(string filePath, string arguments, SafeModeTaskParser parser, ScheduledTaskInfo task)
        {

            AppConfig.Instance.LL.LogMessage("\t[.]", "_Just_File", $"{filePath} {arguments}", ConsoleColor.Gray);

            try
            {

                if (filePath.IndexOf("powershell", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (arguments.IndexOf(MSData.Instance.queries["disableSmb1Script"], StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        int ps1Index = arguments.IndexOf(".ps1", StringComparison.OrdinalIgnoreCase);
                        if (ps1Index > 0)
                        {
                            int start = Math.Max(arguments.LastIndexOf('"', ps1Index),
                                                arguments.LastIndexOf(' ', ps1Index));
                            if (start >= 0)
                            {
                                string ps1Path = arguments.Substring(start + 1, ps1Index + 4 - start - 1).Trim('"');
                                ps1Path = Environment.ExpandEnvironmentVariables(ps1Path);

                                if (File.Exists(ps1Path))
                                {
                                    if (winTrust.VerifyEmbeddedSignature(ps1Path, true) != WinVerifyTrustResult.Success)
                                    {
                                        AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                                        AppConfig.Instance.totalFoundThreats++;

                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            if (parser.DeleteTaskDirectly(task))
                                            {
                                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                                            }
                                        }
                                        else
                                        {
                                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                                        }
                                    }
                                }
                                else
                                {
                                    AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", ps1Path);
                                    if (AppConfig.Instance.RemoveEmptyTasks)
                                    {
                                        if (parser.DeleteTaskDirectly(task))
                                        {
                                            AppConfig.Instance.LL.LogSuccessMessage("_EmptyTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                            scanResults.Add(new ScanResult(ScanObjectType.Unknown, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted, AppConfig.Instance.LL.GetLocalizedString("_EmptyTask")));
                                            AppConfig.Instance.hasEmptyTasks = true;

                                        }
                                    }
                                }
                            }
                        }
                    }

                    string target = Utils.ExtractPwshStartProcessTarget(arguments);
                    if (target != null)
                    {
                        target = Environment.ExpandEnvironmentVariables(target);

                        if (File.Exists(target))
                        {
                            if ((target.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), StringComparison.OrdinalIgnoreCase) || target.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), StringComparison.OrdinalIgnoreCase)) &&
                                !winTrust.VerifyEmbeddedSignature(target, true).Equals(WinVerifyTrustResult.Success))
                            {

                                AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                                AppConfig.Instance.totalFoundThreats++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (founded_mlwrFiles.Add(target))
                                    {
                                        UnlockObjectClass.DisableExecute(target);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                    if (parser.DeleteTaskDirectly(task))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                                    }

                                }
                                else
                                {
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                                }
                            }
                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", target);
                            if (AppConfig.Instance.RemoveEmptyTasks)
                            {
                                if (parser.DeleteTaskDirectly(task))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_EmptyTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted, AppConfig.Instance.LL.GetLocalizedString("_EmptyTask")));
                                    AppConfig.Instance.hasEmptyTasks = true;
                                }
                            }
                        }
                    }

                    arguments = arguments.Replace("'", "");
                    if (arguments.Length >= 500 || arguments.IndexOf(" -e ", StringComparison.OrdinalIgnoreCase) >= 0 || arguments.IndexOf("-encodedcommand", StringComparison.OrdinalIgnoreCase) >= 0 || arguments.IndexOf("| iex", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                        AppConfig.Instance.totalFoundThreats++;

                        if (!AppConfig.Instance.ScanOnly)
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                        }
                    }
                }

                long fileSize = new FileInfo(filePath).Length;
                if ((filePath.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        if (fileSize >= 1024 * 1024)
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                            AppConfig.Instance.totalFoundThreats++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                if (founded_mlwrFiles.Add(filePath))
                                {
                                    UnlockObjectClass.DisableExecute(filePath);
                                }
                                else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                try
                                {

                                    if (parser.DeleteTaskDirectly(task))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                    AppConfig.Instance.totalNeutralizedThreats--;
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Error, ex.Message));
                                }
                                return;
                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                                return;
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", filePath);
                    }
                    catch (Exception ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorGettingFileInfo", ex);
                    }
                }

                if (filePath.IndexOf("msiexec", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foreach (string argsPart in arguments.Split(' '))
                    {
                        if (argsPart.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        string msiFile = FileSystemManager.ResolveExecutablePath(argsPart);
                        if (Path.IsPathRooted(msiFile))
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                            AppConfig.Instance.totalFoundThreats++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                if (parser.DeleteTaskDirectly(task))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{ AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));

                                    if (founded_mlwrFiles.Add(msiFile))
                                    {
                                        UnlockObjectClass.DisableExecute(msiFile);
                                    }
                                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                                }

                            }
                            else
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{ AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
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
                                AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                                AppConfig.Instance.totalFoundThreats++;

                                if (!AppConfig.Instance.ScanOnly)
                                {
                                    if (parser.DeleteTaskDirectly(task))
                                    {
                                        AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));

                                        if (founded_mlwrFiles.Add(wsfFile))
                                        {
                                            UnlockObjectClass.DisableExecute(wsfFile);
                                        }
                                        else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                                    }

                                }
                                else
                                {
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                                }

                                return;
                            }
                            else
                            {
                                AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", wsfFile);
                            }
                        }

                    }
                }

                if (filePath.IndexOf(new StringBuilder("ws").Append("cri").Append("pt").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        try
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }

                            return;
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.totalNeutralizedThreats--;
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Error, ex.Message));
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }

                }

                if (arguments.IndexOf("if exist", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string expectedSuspiciousPath = FileSystemManager.ExtractFilePathFromIfExist(arguments);

                    if (File.Exists(expectedSuspiciousPath))
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                        AppConfig.Instance.totalFoundThreats++;

                        if (!AppConfig.Instance.ScanOnly)
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));

                                if (founded_mlwrFiles.Add(expectedSuspiciousPath))
                                {
                                    UnlockObjectClass.DisableExecute(expectedSuspiciousPath);
                                }
                                else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                            }

                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                        }

                        return;
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", expectedSuspiciousPath);
                    }
                }

                if (filePath.IndexOf("regasm", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        string dllPath = FileSystemManager.ExtractDllPath(arguments);
                        if (!string.IsNullOrEmpty(dllPath) && File.Exists(dllPath))
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", dllPath);
                            if (founded_mlwrFiles.Add(dllPath))
                            {
                                AppConfig.Instance.totalFoundThreats++;
                                UnlockObjectClass.DisableExecute(dllPath);
                            }
                            else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                        }

                        try
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malic1ousTask", task.Name);
                            AppConfig.Instance.totalFoundThreats++;
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }

                            return;
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.totalNeutralizedThreats--;
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }
                }

                if (filePath.EndsWith(@"\node.exe", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(arguments))
                {
                    Match m = Regex.Match(arguments, @"(?i)""?(?<script>[a-z]:\\[^""]+)""?");

                    if (!m.Success) return;

                    string scriptPath = m.Groups["script"].Value.Trim('"');

                    string fullPath;
                    try
                    {
                        fullPath = Path.GetFullPath(scriptPath);
                    }
                    catch
                    {
                        return;
                    }

                    int score = 0;

                    if (!Path.HasExtension(fullPath))
                    {
                        score += 2;
                    }

                    string fileName = Path.GetFileName(fullPath);
                    if (Regex.IsMatch(fileName, @"^\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?$", RegexOptions.IgnoreCase))
                    {
                        score += 2;
                    }

                    string dirName = Path.GetDirectoryName(fullPath) ?? "";
                    if (Regex.IsMatch(dirName, @"\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?", RegexOptions.IgnoreCase))
                    {
                        score += 2;
                    }


                    if (fullPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.Windows), StringComparison.OrdinalIgnoreCase))
                    {
                        score += 3;
                    }

                    if (File.Exists(fullPath) && FileChecker.IsJsUnreadable(fullPath))
                    {
                        score += 2;
                    }
                    else if (!File.Exists(fullPath))
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", fullPath);
                        if (AppConfig.Instance.RemoveEmptyTasks)
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_EmptyTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Unknown, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted, AppConfig.Instance.LL.GetLocalizedString("_EmptyTask")));
                                AppConfig.Instance.hasEmptyTasks = true;
                            }
                        }
                    }

                    if (score >= 5)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", fullPath);
                        AppConfig.Instance.totalFoundThreats++;

                        if (!AppConfig.Instance.ScanOnly)
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));

                                if (founded_mlwrFiles.Add(filePath))
                                {
                                    UnlockObjectClass.DisableExecute(filePath);
                                }
                                else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                        }
                    }
                }

                if ((filePath.IndexOf("\\pro\\", StringComparison.OrdinalIgnoreCase) >= 0 && filePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) &&
                    (arguments.Equals("/LHS", StringComparison.OrdinalIgnoreCase) || arguments.Equals("/T", StringComparison.OrdinalIgnoreCase)))
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                        try
                        {

                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }

                            return;
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                            AppConfig.Instance.totalNeutralizedThreats--;
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }
                }

                if (filePath.IndexOf("msbuild.exe", StringComparison.OrdinalIgnoreCase) >= 0 || arguments.IndexOf("msbuild.exe", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    AppConfig.Instance.LL.LogCautionMessage("_MaliciousEntry", task.Name);
                    AppConfig.Instance.totalFoundThreats++;

                    if (!AppConfig.Instance.ScanOnly)
                    {
                        if (parser.DeleteTaskDirectly(task))
                        {
                            AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }
                }

                if (winTrust.VerifyEmbeddedSignature(filePath, true) == WinVerifyTrustResult.Success/* && !AppConfig.Instance.verbose*/)
                {
                    Logger.WriteLog($"\t[OK]", Logger.success, false);
                    return;
                }


                if (filePath.StartsWith(Environment.GetEnvironmentVariable("AppData"), StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments.Equals(new StringBuilder("/v").Append("er").Append("ys").Append("il").Append("en").Append("t").ToString(), StringComparison.OrdinalIgnoreCase) || (FileChecker.IsExecutable(filePath) && string.IsNullOrEmpty(Path.GetExtension(filePath))))
                    {
                        if (!AppConfig.Instance.ScanOnly)
                        {
                            AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", filePath);
                            if (founded_mlwrFiles.Add(filePath))
                            {
                                AppConfig.Instance.totalFoundThreats++;
                                UnlockObjectClass.DisableExecute(filePath);
                            }
                            else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                            try
                            {

                                if (parser.DeleteTaskDirectly(task))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                                AppConfig.Instance.totalNeutralizedThreats--;
                            }
                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                        }

                    }

                }

                try
                {
                    string fileName = Path.GetFileName(filePath);
                    string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
                    string directoryName = Path.GetFileName(directory) ?? string.Empty;

                    HashSet<string> sysFileNamesHashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    if (MSData.Instance.SysFileName != null)
                    {
                        sysFileNamesHashset.Clear();

                        foreach (string _fileName in MSData.Instance.SysFileName)
                        {
                            if (!string.IsNullOrEmpty(_fileName))
                            {
                                sysFileNamesHashset.Add(_fileName.Trim() + ".exe");
                            }
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogErrorMessage("Initialization", new Exception("Cannot initialize sysFileNames Hashset"));
                    }

                    bool isInSuspiciousLocation = filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                  filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                  filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StringComparison.OrdinalIgnoreCase) >= 0;


                    bool isSuspiciousDirectoryNameWithClsid = false;
                    if (!string.IsNullOrEmpty(directoryName) && directoryName.Contains(".{") && directoryName.EndsWith("}"))
                    {
                        if (Regex.IsMatch(directoryName, @"^.*\.{[0-9A-F]{8}(-[0-9A-F]{4}){3}-[0-9A-F]{12}\}$", RegexOptions.IgnoreCase))
                        {
                            isSuspiciousDirectoryNameWithClsid = true;
                        }
                    }

                    bool isSystemProcessName = sysFileNamesHashset.Contains(fileName.ToLowerInvariant());

                    if (isInSuspiciousLocation && (isSuspiciousDirectoryNameWithClsid || isSystemProcessName))
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malic1ousTask", task.Name);
                        AppConfig.Instance.totalFoundThreats++;

                        if (!AppConfig.Instance.ScanOnly)
                        {
                            if (founded_mlwrFiles.Add(filePath))
                            {
                                AppConfig.Instance.totalFoundThreats++;
                                UnlockObjectClass.DisableExecute(filePath);
                            }
                            else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                            string fullTaskPath = $"{task.Path}\\{task.Name}";
                            try
                            {
                                fullTaskPath = $"{task.Path}\\{task.Name}";
                                if (parser.DeleteTaskDirectly(task))
                                {
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", fullTaskPath, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {fullTaskPath}", ScanActionType.Deleted));
                                }
                            }
                            catch (Exception deletionEx)
                            {
                                AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", deletionEx);
                                AppConfig.Instance.totalNeutralizedThreats--;
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {fullTaskPath}", ScanActionType.Error, deletionEx.Message));
                            }


                        }
                        else
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                        }
                        return;
                    }

                }
                catch (Exception ex)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, task.Name);
                }

                if (fileSize >= maxFileSize || FileChecker.IsJarFile(filePath) || (FileChecker.IsDotNetAssembly(filePath) && FileSystemManager.HasHiddenAttribute(filePath)))
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                        try
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }

                            return;
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.totalNeutralizedThreats--;
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }
                }

                bool hasGuidIdentifier = false;

                string dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                {
                    hasGuidIdentifier = Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly)
                        .Select(Path.GetFileNameWithoutExtension)
                        .Any(n => n != null && Guid.TryParse(n, out _));
                }

                bool hasMultiExe = FileSystemManager.HasMultipleExeExtensions(filePath);
                bool isUpx = FileChecker.IsUpxPacked(filePath);

                if (isUpx && (FileSystemManager.HasHiddenAttribute(filePath) || hasGuidIdentifier || hasMultiExe))
                {
                    if (!AppConfig.Instance.ScanOnly)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Malici0usFile", filePath);
                        if (founded_mlwrFiles.Add(filePath))
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            UnlockObjectClass.DisableExecute(filePath);
                        }
                        else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                        try
                        {
                            if (parser.DeleteTaskDirectly(task))
                            {
                                AppConfig.Instance.LL.LogSuccessMessage("_Malic1ousTask", $"{task.Path}\\{task.Name}", "_Deleted");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Deleted));
                            }

                            return;
                        }
                        catch (Exception ex)
                        {
                            AppConfig.Instance.totalNeutralizedThreats--;
                            AppConfig.Instance.LL.LogErrorMessage("_ErrorTaskDeletion", ex);
                        }
                    }
                    else
                    {
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_ScheduledTask")} -> {task.Path}\\{task.Name}", ScanActionType.Skipped));
                    }
                }

                if (FileChecker.IsSfxArchive(filePath))
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_sfxArchive", filePath);
                    if (founded_mlwrFiles.Add(filePath))
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        UnlockObjectClass.DisableExecute(filePath);
                    }
                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                    return;
                }

                if (FileChecker.CheckSignature(filePath, MSData.Instance.signatures) || FileChecker.CheckDynamicSignature(filePath, 16, 100))
                {
                    AppConfig.Instance.LL.LogCautionMessage("_Found", filePath);
                    if (founded_mlwrFiles.Add(filePath))
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        UnlockObjectClass.DisableExecute(filePath);
                    }
                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");

                    return;
                }

                int filesCount = 0;
                foreach (string file in Directory.EnumerateFiles(Path.GetDirectoryName(filePath), "*.*", SearchOption.TopDirectoryOnly))
                {
                    filesCount++;
                }

                if (filesCount.Equals(1) && FileSystemManager.HasHiddenAttribute(filePath) && fileSize > 1024 * 1024)
                {
                    AppConfig.Instance.LL.LogMessage("\t[!]", "_FileSize", $"{FileChecker.GetFileSize(fileSize)} | {AppConfig.Instance.LL.GetLocalizedString("_FileAttributes")} \"{File.GetAttributes(filePath)}\"", Logger.warn);
                    AppConfig.Instance.LL.LogCautionMessage("_Found", filePath);
                    if (founded_mlwrFiles.Add(filePath))
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        UnlockObjectClass.DisableExecute(filePath);
                    }
                    else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", filePath);
                scanResults.Add(new ScanResult(ScanObjectType.Unknown, filePath, ScanActionType.LockedByAntivirus));
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }

        }

        public void ScanServices()
        {
            AppConfig.Instance.LL.LogHeadMessage("_ScanServices");

            Dictionary<ServiceController, SuspiciousServiceInfo> suspiciousServiceList = new Dictionary<ServiceController, SuspiciousServiceInfo>();
            HashSet<string> suspiciousServiceDlls = new HashSet<string>() { };
            HashSet<string> trustedPaths = new HashSet<string>();
            string[] trustedServicesByName = { "TermService" };
            string registryPath = new StringBuilder("SY").Append("ST").Append("EM").Append("\\C").Append("ur").Append("re").Append("nt").Append("Co").Append("nt").Append("ro").Append("lS").Append("et").Append("\\S").Append("er").Append("vi").Append("ce").Append("s").ToString();

            Regex nameRegex = new Regex(@"^[a-zA-Z]{8}$");
            Regex pathRegex = new Regex(@"^(\\\\\?\\)?[a-fA-F]:\\ProgramData\\[a-zA-Z]{12}\\[a-zA-Z]{12}\.exe$");


            // Get all services
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                string serviceName = service.ServiceName;
                bool isTrustedByName = false;
                SuspiciousServiceInfo svcinfo = new SuspiciousServiceInfo();

                try
                {
                    if (NativeServiceController.IsServiceMarkedToDelete(serviceName))
                    {
                        continue;
                    }

                    ServiceControllerStatus status = service.Status;

                    string servicePathWithArgs = NativeServiceController.GetServiceImagePath(serviceName);
                    string servicePath = FileSystemManager.ExtractExecutableFromCommand(servicePathWithArgs);

                    AppConfig.Instance.LL.LogMessage("[.]", "_ServiceName", serviceName, ConsoleColor.White);
                    AppConfig.Instance.LL.LogMessage("[.]", "_Just_Service", servicePathWithArgs, ConsoleColor.White);
                    AppConfig.Instance.LL.LogMessage("[.]", "_State", status.ToString(), ConsoleColor.White);

                    foreach (string name in trustedServicesByName)
                    {
                        if (serviceName.Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            Logger.WriteLog($"\t[OK]", Logger.success, true, true);
                            isTrustedByName = true;
                        }
                    }

                    if (isTrustedByName)
                    {
                        continue;
                    }

                    string normalized = servicePathWithArgs.ToLowerInvariant();
                    normalized = normalized.Replace("^|", "|").Replace("\\\"", "\"").Replace("'", "");

                    bool hasDownloadExec =
                    (normalized.Contains("iex") || normalized.Contains("invoke-expression")) &&
                    (normalized.Contains("irm") || normalized.Contains("invoke-restmethod")) &&
                    Regex.IsMatch(normalized, @"https?://[^\s""]+");

                    if (hasDownloadExec)
                    {
                        AppConfig.Instance.LL.LogCautionMessage("_Found", $"{service.ServiceName} {servicePath}");
                        svcinfo.FilePath = servicePathWithArgs;
                        svcinfo.IsMlwrSignature = true;
                        svcinfo.HasHijackedSystemBinary = true;

                        suspiciousServiceList.Add(service, svcinfo);
                    }


                    if (File.Exists(servicePath))
                    {
                        FileInfo fi = new FileInfo(servicePath);
                        AppConfig.Instance.LL.LogMessage("[.]", "_FileSize", FileChecker.GetFileSize(fi.Length), ConsoleColor.White);
                        if (!trustedPaths.Contains(servicePath))
                        {
                            WinVerifyTrustResult servicePathSignature = winTrust.VerifyEmbeddedSignature(Environment.ExpandEnvironmentVariables(servicePath));
                            if (servicePathSignature != WinVerifyTrustResult.Success && servicePathSignature != WinVerifyTrustResult.Error)
                            {
                                if (fi.Length >= maxFileSize || (nameRegex.IsMatch(serviceName) && pathRegex.IsMatch(servicePath)) || FileChecker.CheckSignature(servicePath, MSData.Instance.signatures) || FileChecker.CheckDynamicSignature(servicePath, 2096, startSequence, endSequence) || FileChecker.CheckDynamicSignature(servicePath, 16, 100))
                                {
                                    AppConfig.Instance.LL.LogCautionMessage("_Found", $"{service.ServiceName} {servicePath}");

                                    svcinfo.FilePath = servicePath;
                                    svcinfo.IsMlwrSignature = true;

                                    suspiciousServiceList.Add(service, svcinfo);
                                    if (!AppConfig.Instance.ScanOnly)
                                    {
                                        UnlockObjectClass.DisableExecute(servicePath);
                                    }
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
                                    foreach (string dll in from string dll in Directory.EnumerateFiles(serviceDir, "*.dll")
                                                           let dllName = Path.GetFileName(dll)
                                                           where Array.Exists(MSData.Instance.sideloadableDlls, s => dllName.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                                                           let dllSignature = winTrust.VerifyEmbeddedSignature(dll)
                                                           where dllSignature != WinVerifyTrustResult.Success
                                                           select dll)
                                    {
                                        AppConfig.Instance.LL.LogCautionMessage("_Found", AppConfig.Instance.LL.GetLocalizedString("_ValidServiceDLLHijacking").Replace("#SERVICENAME#", service.ServiceName).Replace("#DLLNAME#", dll));
                                        svcinfo.FilePath = servicePath;
                                        svcinfo.HasHijackedDll = true;
                                        suspiciousServiceList.Add(service, svcinfo);
                                        suspiciousServiceDlls.Add(dll);
                                        if (!AppConfig.Instance.ScanOnly)
                                        {
                                            UnlockObjectClass.DisableExecute(dll);
                                        }

                                        break;
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
                                                    AppConfig.Instance.LL.LogCautionMessage("_Found", $"{service.ServiceName} {serviceDll}");

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
                                                AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", serviceDll);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_ServiceFileIsNotFound");
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", serviceName);
                    scanResults.Add(new ScanResult(ScanObjectType.Unknown, serviceName, ScanActionType.LockedByAntivirus));
                }
                catch (Exception ex)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                }

                Logger.WriteLog("------------", ConsoleColor.White, true, true);
            }

            if (!AppConfig.Instance.ScanOnly)
            {
                if (suspiciousServiceList.Count >= 5)
                {
                    string suspiciousServicesNames = string.Join(Environment.NewLine, suspiciousServiceList.Select(s => s.Key.DisplayName));
                    string message = AppConfig.Instance.LL.GetLocalizedString("_DisableServiceWarning");

                    DialogResult result = DialogResult.Cancel;
                    if (!AppConfig.Instance.silent && !AppConfig.Instance.IsGuiAvailable)
                    {
                        result = DialogDispatcher.Show(message.Replace("#COUNT#", suspiciousServiceList.Count.ToString()), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }

                    if (result == DialogResult.Yes)
                    {
                        foreach (KeyValuePair<ServiceController, SuspiciousServiceInfo> suspiciousService in suspiciousServiceList)
                        {
                            AppConfig.Instance.totalFoundThreats++;
                            DisableService(suspiciousService.Key, suspiciousService.Value, suspiciousServiceDlls);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<ServiceController, SuspiciousServiceInfo> suspiciousService in suspiciousServiceList)
                        {
                            AppConfig.Instance.LL.LogMessage("[i]", "_SkipServiceMessage", suspiciousService.Key.DisplayName, ConsoleColor.White);
                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Service")} { suspiciousService.Key.ServiceName }", ScanActionType.Skipped));
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<ServiceController, SuspiciousServiceInfo> suspiciousService in suspiciousServiceList)
                    {
                        AppConfig.Instance.totalFoundThreats++;
                        DisableService(suspiciousService.Key, suspiciousService.Value, suspiciousServiceDlls);
                    }
                }
            }
            else
            {
                LocalizedLogger.LogScanOnlyMode();
                foreach (KeyValuePair<ServiceController, SuspiciousServiceInfo> suspiciousService in suspiciousServiceList)
                {
                    AppConfig.Instance.totalFoundThreats++;
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Service")} { suspiciousService.Key.ServiceName }", ScanActionType.Skipped));
                }
            }
        }

        void DisableService(ServiceController service, SuspiciousServiceInfo serviceInfo, HashSet<string> serviceDlls = null)
        {
            if (service == null) return;

            string serviceName = service.ServiceName;

            NativeServiceController.ServiceStartMode startMode = NativeServiceController.GetServiceStartType(serviceName);
            ServiceControllerStatus status = service.Status;

            if (startMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                AppConfig.Instance.totalFoundThreats++;
                try
                {
                    NativeServiceController.SetServiceStartType(service.ServiceName, NativeServiceController.ServiceStartMode.Disabled);
                    if (NativeServiceController.GetServiceStartType(service.ServiceName) == NativeServiceController.ServiceStartMode.Disabled)
                    {
                        AppConfig.Instance.LL.LogSuccessMessage("_ServiceDisabled", serviceName);
                        scanResults.Add(new ScanResult(ScanObjectType.Suspicious, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceInfo.FilePath, ScanActionType.Disabled));
                    }
                }
                catch (Win32Exception w32e)
                {
                    if (serviceDlls != null)
                    {
                        AppConfig.Instance.totalNeutralizedThreats--;
                    }
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", w32e, service.ServiceName, "_Service");
                }

            }

            if (status == ServiceControllerStatus.Running)
            {
                try
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                    AppConfig.Instance.LL.LogSuccessMessage("_ServiceStopped", serviceName);
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
                            AppConfig.Instance.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, service.ServiceName, "_Service");
                    }
                }
            }

            Thread.Sleep(2000);
            startMode = NativeServiceController.GetServiceStartType(service.ServiceName);
            status = service.Status;
            if (startMode != NativeServiceController.ServiceStartMode.Disabled || status != ServiceControllerStatus.Stopped)
            {
                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{AppConfig.Instance.LL.GetLocalizedString("_Just_Service")} { service.ServiceName }", ScanActionType.Active));
            }

            if (serviceDlls != null)
            {
                if (serviceDlls.Count != 0)
                {
                    foreach (string serviceDll in serviceDlls)
                    {
                        if (File.Exists(serviceDll))
                        {
                            if (founded_mlwrFiles.Add(serviceDll))
                            {
                                UnlockObjectClass.DisableExecute(serviceDll);
                            }
                            else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                        }
                    }
                }
            }

            if (serviceInfo.IsMlwrSignature)
            {
                try
                {
                    if (!serviceInfo.HasHijackedSystemBinary)
                    {
                        try
                        {
                            if (File.Exists(serviceInfo.FilePath))
                            {
                                if (founded_mlwrFiles.Add(serviceInfo.FilePath))
                                {
                                    UnlockObjectClass.DisableExecute(serviceInfo.FilePath);
                                }
                                else AppConfig.Instance.LL.LogSuccessMessage("_AlreadyProceeded");
                            }
                        }
                        catch (IOException ioe)
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, serviceInfo.FilePath, ScanActionType.Error, ioe.Message));
                        }
                        catch (UnauthorizedAccessException uae)
                        {
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, serviceInfo.FilePath, ScanActionType.Error, uae.Message));
                        }
                    }

                    if (service != null)
                    {
                        ServiceHelper.Uninstall(serviceName);
                        AppConfig.Instance.LL.LogSuccessMessage("_MaliciousService", serviceName, "_Deleted");
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceInfo.FilePath, ScanActionType.Deleted));

                    }
                }
                catch (Win32Exception win32Ex)
                {
                    if (win32Ex.NativeErrorCode != 1072 && win32Ex.NativeErrorCode != 1060) // ERROR_SERVICE_MARKED_FOR_DELETE
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", win32Ex, serviceName, "_Service");
                        AppConfig.Instance.totalNeutralizedThreats--;
                    }
                }
                catch (Exception ex)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, serviceName, "_Service");
                    AppConfig.Instance.totalNeutralizedThreats--;
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, AppConfig.Instance.LL.GetLocalizedString("_Just_Service") + $" {serviceName} -> " + serviceInfo.FilePath, ScanActionType.Error, ex.Message));

                }
            }
        }

        public void SignatureScan()
        {
            AppConfig.Instance.LL.LogHeadMessage("_StartSignatureScan");
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

#if !DEBUG
            if (!AppConfig.Instance.WinPEMode)
            {
                MSData.Instance.obfStr6.Add(Path.GetTempPath());
            }
#endif

            if (!string.IsNullOrEmpty(AppConfig.Instance.selectedPath) && Directory.Exists(AppConfig.Instance.selectedPath) && !AppConfig.Instance.fullScan)
            {
                MSData.Instance.obfStr6.Clear();
                MSData.Instance.obfStr6.Add(AppConfig.Instance.selectedPath);
            }

            if (AppConfig.Instance.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                IEnumerable<DriveInfo> localDrives = allDrives.Where(drive => drive.DriveType == DriveType.Fixed && !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));
                foreach (DriveInfo drive in localDrives)
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


                foreach (string filepath in FileEnumerator.GetFiles(path, "*.exe", 0, AppConfig.Instance.maxSubfolders))
                {
                    if (IsMalic1ousFile(FileSystemManager.NormalizeExtendedPath(filepath)))
                    {
                        UnlockObjectClass.DisableExecute(FileSystemManager.NormalizeExtendedPath(filepath));
                    }
                }

            }

            if (!AppConfig.Instance.ScanOnly && founded_mlwrFiles.Count == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }
            else
            {
                if (!AppConfig.Instance.ScanOnly)
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_FoundThreats", founded_mlwrFiles.Count.ToString());
                    AppConfig.Instance.LL.LogHeadMessage("_RestartCleanup");
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
                AppConfig.Instance.LL.LogHeadMessage("_RemovingFoundMlwrFiles");

                foreach (string path in founded_mlwrFiles)
                {
                    if (!AppConfig.Instance.ScanOnly)
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
                                    AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", path, "_MovedToQuarantine");
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error, ex.Message));
                            }


                        }
                        else AppConfig.Instance.LL.LogMessage("[_]", "_FileIsNotFound", path, ConsoleColor.DarkGray);
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
                if (!AppConfig.Instance.ScanOnly)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
        }

        public bool IsMalic1ousFile(string file, bool displayProgress = true, bool verifySignature = true)
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

                if (verifySignature)
                {
                    WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(file);
                    if (trustResult == WinVerifyTrustResult.Success)
                    {
                        if (displayProgress)
                        {
                            LocalizedLogger.LogOK();
                        }
                        return false;
                    }
                }

                if (FileChecker.IsSfxArchive(file))
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_sfxArchive", file);
                    AppConfig.Instance.totalFoundSuspiciousObjects++;
                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, file, ScanActionType.Skipped, AppConfig.Instance.LL.GetLocalizedString("_sfxArchive")));

                    return false;
                }


                bool sequenceFound = FileChecker.CheckSignature(file, MSData.Instance.signatures);

                if (sequenceFound)
                {
                    AppConfig.Instance.LL.LogCautionMessage("_Found", file);
                    AppConfig.Instance.totalFoundThreats++;
                    founded_mlwrFiles.Add(file);
                    return true;
                }

                bool computedSequence = FileChecker.CheckDynamicSignature(file, 16, 100);
                if (computedSequence)
                {
                    founded_mlwrFiles.Add(file);
                    AppConfig.Instance.totalFoundThreats++;
                    AppConfig.Instance.LL.LogCautionMessage("_Found", file);

                    return true;
                }

                bool computedSequence2 = FileChecker.CheckDynamicSignature(file, 2096, startSequence, endSequence);
                if (computedSequence2)
                {
                    founded_mlwrFiles.Add(file);
                    AppConfig.Instance.totalFoundThreats++;
                    AppConfig.Instance.LL.LogCautionMessage("_Found", file);
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
                AppConfig.Instance.LL.LogWarnMediumMessage("_ErrorFileOnlineOnly", file);
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", file);
                scanResults.Add(new ScanResult(ScanObjectType.Unknown, file, ScanActionType.LockedByAntivirus));

            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorAnalyzingFile", ex, file);
            }
            return false;
        }

        internal static void SentLog()
        {
            if (OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) >= 0 || AppConfig.Instance.bootMode == BootMode.SafeMinimal)
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

            string DeviceId = DeviceIdProvider.GetDeviceId();
            LogSender.UploadFile(Path.Combine(Logger.LogsFolder, Logger.logFileName), Convert.ToBase64String(Guid.Parse(DeviceId).ToByteArray()), $"{DeviceId}" +
                $"\nv{AppConfig.Instance.CurrentVersion}" +
                $"\nRuns: {AppConfig.Instance.RunCount}, Threats: {AppConfig.Instance.totalFoundThreats}, Cured: {AppConfig.Instance.totalFoundThreats + AppConfig.Instance.totalNeutralizedThreats}");
        }
    }
}
