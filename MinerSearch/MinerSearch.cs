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
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        };

        readonly string[] _nvdlls = new[]
        {
            "nvcompiler.dll",
            "nvopencl.dll",
            "nvfatbinaryLoader.dll",
            "nvapi64.dll",
            "OpenCL.dll",
        };

        List<string> suspFls_path = new List<string>();

        byte[] startSequence = { 0xFF, 0xC7, 0x05, 0xC5 };
        byte[] endSequence = { 0xE8, 0x54, 0xFF, 0xFF, 0xFF };

        public List<string> founded_mlwrPths = new List<string>();

        readonly long[] constantFileSize = new long[]
        {
            634880, //audiodg
            98304, //taskhostw
            69632, //taskhost
            862208, //conhost
            55320, //svchost
            94720, //dwm
            71680, //rundll32
            906752, //winlogon
            17600, //csrss
            714856, //services
            60544, //lsass
            21312, //dllhost
            155976, //smss
            420472, //wininit
            3235192, //vbc
            57344, //unsecapp
            174552, //ngen
            40960, //dialer
            12800, //tcpsvcs
            17408, //print
            20480, //find
            61440, //winver
            24576, //ping
            28672, //fc
            12288, //help
            28672, //sort
            20480, //label
            106496, //runtimebroker
            245760 //compattelrunner
         };
        long maxFileSize = 100 * 1024 * 1024;
        long minFileSize = 2112;


        public List<int> mlwrPids = new List<int>();
        public List<string> founded_suspLckPths = new List<string>();
        public List<string> founded_mlwrPathes = new List<string>();
        public static string quarantineFolder = Path.Combine(Environment.CurrentDirectory, "mi?ne?rs?eаrch_quarаntine".Replace("?", ""));

        public static List<ScanResult> scanResults = new List<ScanResult>() { };

        WinTrust winTrust = new WinTrust();
        public MSData msData = new MSData();

        public void DetectRk()
        {
            Program.LL.LogHeadMessage("_ChekingR00tkit");

            Native.R77_PROCESS[] r77Processes = new Native.R77_PROCESS[Native.MaxProcesses];
            uint processCount = Native.MaxProcesses;

            Program._utils.GetR77Processes(ref r77Processes, ref processCount);
            if (processCount > 0)
            {
                LocalizedLogger.LogR00TkitPresent();
                Program.totalFoundThreats++;

                Program._utils.DetachAllInjectedProcesses(r77Processes, processCount);
                Program._utils.TerminateR77Service(-1);
                Program._utils.RemoveR77Config();

                Program._utils.GetR77Processes(ref r77Processes, ref processCount);
                if (processCount == 0)
                {
                    Program.LL.LogSuccessMessage("_SuccessR00tkitNeutralized");
                }

                foreach (Process process in Utils.GetProcesses())
                {
                    if (!process.ProcessName.StartsWith("d?i?al?e??r?".Replace("?", ""))) continue;
                    Program._utils.SuspendProcess(process.Id);
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

            string myExePath = Application.ExecutablePath;

            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<Process> procs = Utils.GetProcesses();


            List<Utils.RenamedFileInfo> renamedFilesInfo = Program._utils.GetRenamedFilesData();

            if (renamedFilesInfo.Count > 0)
            {
                foreach (var rfi in renamedFilesInfo)
                {
                    suspFls_path.Add(rfi._NewFilePath);
                    mlwrPids.Add(rfi._ProcessId);
                }
            }

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
                try
                {


                    string processPath = p.MainModule.FileName;
                    if (processPath.Equals(myExePath))
                    {
                        continue;
                    }
                    if (!p.HasExited)
                    {
                        processName = p.ProcessName.ToLower();
                        processId = p.Id;
                        LocalizedLogger.LogScanning(processName);
                    }
                    else
                    {
                        processId = -1;
                        continue;
                    }

                    if (renamedFilesInfo.Any(fileInfo => fileInfo._ProcessId == p.Id))
                    {
                        processId = -1;
                        Program.LL.LogSuccessMessage("_AlreadyProceeded");
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
                            fileSize = new FileInfo(processPath).Length;
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_Error", ex);
                        }

                        try
                        {
                            string fileDescription = p.MainModule.FileVersionInfo.FileDescription;
                            if (fileDescription != null)
                            {
                                if (fileDescription.Contains("svhost"))
                                {
                                    Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                                    suspFls_path.Add(p.MainModule.FileName);
                                    riskLevel += 2;
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

                    if (processName.Contains("helper") && !isValidProcess)
                    {
                        riskLevel += 1;
                    }

                    int modCount = 0;
                    try
                    {
                        foreach (ProcessModule pMod in p.Modules)
                        {
                            modCount += _nvdlls.Where(name => pMod.ModuleName.ToLower().Contains(name.ToLower())).Count();
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
                            int remoteport = Utils.GetPortByProcessId(p.Id);
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

                    string args = null;

                    try
                    {
                        args = Utils.GetCommandLine(p).ToLower();
                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                        args = null;
                    }

                    if (processName.ToLower() == msData.SysFileName[19])
                    {
                        Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                        riskLevel += 3;
                    }

                    if (args != null)
                    {
                        foreach (int port in _PortList)
                        {
                            if (args.Contains(port.ToString()))
                            {
                                riskLevel += 1;
                                Program.LL.LogWarnMessage("_BlacklistedPortCMD", $"{port} : {processName}.exe");
                            }
                        }
                        if (args.Contains("str???at??um".Replace("?", "")))
                        {
                            riskLevel += 3;
                            Program.LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "st??ra??tum");
                        }
                        if (args.Contains("na??nop??ool?".Replace("?", "")))
                        {
                            riskLevel += 3;
                            Program.LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "nano?po?ol??");

                        }
                        if (args.Contains("p?ool.".Replace("?", "")))
                        {
                            riskLevel += 3;
                            Program.LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "po?ol??.");

                        }

                        if (args.Contains("-systemcheck"))
                        {
                            riskLevel += 2;
                            Program.LL.LogWarnMessage("_FakeSystemTask");

                            try
                            {
                                if (p.MainModule.FileName.ToLower().Contains("appdata") && p.MainModule.FileName.ToLower().Contains("windows"))
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

                        if ((processName == msData.SysFileName[3] && !args.Contains($"\\??\\{Program.drive_letter.ToLower()}:\\")))
                        {
                            Program.LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                            riskLevel += 3;
                        }
                        if (processName == msData.SysFileName[4] && !args.Contains($"{msData.SysFileName[4]}.exe -k"))
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }
                        if (processName == msData.SysFileName[4] && (args.ToLower().Contains($"{msData.SysFileName[4]}.exe -k dcomlaunch")))
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
                        if (processName == msData.SysFileName[5] && !Program.RunAsSystem)
                        {
                            //int argsLen = args.Length;
                            bool isFakeDwm = false;

                            if (!Utils.GetProcessOwner(p.Id).StartsWith("Window Manager\\DWM-") && !Utils.GetWindowsVersion().Contains("Windows 7"))
                            {
                                isFakeDwm = true;
                            }

                            int dwmParentPID = Utils.GetParentProcessId(p.Id);
                            if (dwmParentPID != 0)
                            {
                                try
                                {
                                    Process dwmParent = Process.GetProcessById(dwmParentPID);
                                    if (dwmParent.HasExited || dwmParent.ProcessName != msData.SysFileName[7] && !Utils.GetWindowsVersion().Contains("Windows 7"))
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

                            try
                            {
                                if (Utils.IsSystemProcess(p.Id))
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
                        if (processName == msData.SysFileName[17] && args.Contains("\\dia?ler.exe ".Replace("?", "")))
                        {
                            Program.LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }

                        if (processName == "explorer" && !args.ToLower().Contains($@"{Program.drive_letter.ToLower()}:\windows\explorer.exe"))
                        {
                            riskLevel++;
                        }

                    }

                    bool isSuspiciousPath = false;
                    string fullPath = p.MainModule.FileName.ToLower();

                    if (processName == msData.SysFileName[28] && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\system32"))
                    {
                        Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                        isSuspiciousPath = true;
                        riskLevel += 2;
                    }

                    for (int i = 0; i < msData.SysFileName.Length; i++)
                    {

                        if (processName == msData.SysFileName[i])
                        {

                            if (!fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\system32")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\syswow64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\winsxs\\amd64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\microsoft.net\\framework64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\microsoft.net\\framework"))
                            {

                                Program.LL.LogWarnMessage("_SuspiciousPath", fullPath);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }

                            if (fileSize >= constantFileSize[i] * 3 && !isValidProcess)
                            {
                                Program.LL.LogWarnMessage("_SuspiciousFileSize", Utils.Sizer(fileSize));
                                riskLevel += 1;
                            }

                        }

                    }

                    try
                    {
                        if (processName == "un?sec?app".Replace("?", "") && !p.MainModule.FileName.ToLower().Contains(@":\w?in?do?ws\s?yst?em3?2\wb?em".Replace("?", "")))
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


                    if (processName == "rundll" || processName == "system" || processName == "wi?ns?er?v".Replace("?", ""))
                    {
                        Program.LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");

                        isSuspiciousPath = true;
                        riskLevel += 3;
                    }

                    if (processName == "explorer")
                    {

                        try
                        {
                            int ParentProcessId = Utils.GetParentProcessId(processId);
                            if (ParentProcessId != 0)
                            {
                                Process ParentProcess = Process.GetProcessById(ParentProcessId);
                                if (ParentProcess.ProcessName.ToLower() == "explorer")
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
                            continue;
                        }
                        catch (ArgumentException)
                        {
                            continue;
                        }


                        if (Utils.IsSystemProcess(p.Id) && !Program.RunAsSystem)
                        {
                            riskLevel += 2;
                        }
                    }

                    if (p.TotalProcessorTime > new TimeSpan(0, 1, 0))
                    {

                        if (File.Exists(processPath))
                        {
                            AnalyzeFile(Utils.GetLongPath(processPath), false);
                        }
                        else
                        {
                            Program.LL.LogWarnMessage("_FileIsNotFound", processPath);
                        }
                    }


                    if (riskLevel >= 3)
                    {
                        Program.LL.LogCautionMessage("_ProcessFound", riskLevel.ToString());
                        Program._utils.SuspendProcess(processId);
                        Program.totalFoundThreats++;

                        if (isSuspiciousPath)
                        {
                            if (!Program.ScanOnly)
                            {
                                try
                                {
                                    string rnd = Utils.GetRndString();
                                    string NewFilePath = Path.Combine(Path.GetDirectoryName(p.MainModule.FileName), $"{Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe");
                                    File.Move(p.MainModule.FileName, NewFilePath); //Rename malicious file
                                    Program.LL.LogSuccessMessage("_FileRenamed", $"{Path.GetFileNameWithoutExtension(NewFilePath)}.exe");
                                    Program._utils.SaveRenamedFileData(new Utils.RenamedFileInfo()
                                    {
                                        _ProcessId = p.Id,
                                        _NewFilePath = NewFilePath
                                    });

                                    suspFls_path.Add(NewFilePath);
                                }
                                catch (ArgumentException)
                                {
                                    continue;
                                }
                                catch (Exception e)
                                {
                                    Program.totalNeutralizedThreats--;
                                    Program.LL.LogErrorMessage("_Error", e);
                                }
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

                    MessageBox.Show(Program.LL.GetLocalizedString("_ErrorScanProcesses"), Utils.GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }

            procs.Clear();
            if (renamedFilesInfo.Count > 0)
            {
                renamedFilesInfo.Clear();
            }
            Program._utils.RemoveRenamedFilesData();


        }
        public void StaticScan()
        {
            Program.LL.LogHeadMessage("_ScanDirectories");

            if (!Directory.Exists(quarantineFolder))
            {
                try
                {
                    Directory.CreateDirectory(quarantineFolder);
                }
                catch (SecurityException)
                {
                    quarantineFolder = Path.Combine(Environment.CurrentDirectory, $"mi?ner?se?а?rch_quarаntine_{Utils.GetRndString()}".Replace("?", ""));
                    Directory.CreateDirectory(quarantineFolder);
                }
            }

            List<string> LockedFolders = msData.obfStr5;
            if (!Program.WinPEMode)
            {
                LockedFolders.Add(Path.Combine(Environment.GetEnvironmentVariable("Appdata"), "sys~~fil~es".Replace("~", "")));
                LockedFolders.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "aut~olo~~gger".Replace("~", "")));
                LockedFolders.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av~_bl~~ock~_rem~~over".Replace("~", "")));

                string DownloadsPath = Program._utils.GetDownloadsPath();
                if (DownloadsPath != null)
                {
                    LockedFolders.Add(Path.Combine(DownloadsPath, "aut~olo~gger".Replace("~", "")));
                    LockedFolders.Add(Path.Combine(DownloadsPath, "av~_bl~ock~_rem~over".Replace("~", "")));
                    if (!Path.GetPathRoot(DownloadsPath).Equals("C:\\") && !Program.WinPEMode)
                    {
                        msData.obfStr6.Add(DownloadsPath);
                    }
                }

            }

            ScanDirectories(LockedFolders, founded_suspLckPths, true);

            if (!Program.ScanOnly)
            {
                if (founded_suspLckPths.Count == 0)
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
                        if (Utils.CalculateMD5(file).Equals("0c0195c48b6b8582fa6f6373032118da"))
                        {
                            msData.obfStr1.Add(directory);
                        }
                    }
                }
            }


            ScanDirectories(msData.obfStr1, founded_mlwrPathes);
            if (!Program.ScanOnly)
            {
                if (founded_mlwrPathes.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

            //#if DEBUG
            Program.LL.LogHeadMessage("_ScanFiles");

            string _baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).ToLower().Replace("x:", $@"{Program.drive_letter}:");
            FindMlwrFiles(_baseDirectory);

            if (!Program.ScanOnly)
            {
                if (founded_mlwrPths.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
            //#endif

            if (!Program.WinPEMode)
            {
                ScanRegistry();
                ScanWMI();
                if (!Program.no_services)
                {
                    ScanServices();
                }

                switch (Utils.GetBootMode())
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
                                MessageBox.Show(Program.LL.GetLocalizedString("_ErrorNotFoundComponent"), Utils.GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (Program.ScanOnly)
                {
                    foreach (var id in mlwrPids)
                    {
                        try
                        {
                            using (Process p = Process.GetProcessById(id))
                            {
                                string pname = p.ProcessName;
                                int pid = p.Id;

                                if (!p.HasExited)
                                {
                                    Program.LL.LogCautionMessage("_Malici0usProcess", $"{pname} - PID: {pid}");
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
                            Program.LL.LogErrorMessage("_ErrorTerminateProcess", e);
                        }
                    }
                    LocalizedLogger.LogScanOnlyMode();

                }
                else
                {
                    Program.LL.LogHeadMessage("_TryCloseProcess");

                    Utils.UnProtect(mlwrPids.ToArray());

                    foreach (var id in mlwrPids)
                    {
                        try
                        {
                            using (Process p = Process.GetProcessById(id))
                            {
                                string pname = p.ProcessName;
                                int pid = p.Id;

                                p.Kill();

                                if (p.HasExited)
                                {
                                    Program.LL.LogSuccessMessage("_ProcessTerminated", $"{pname}, PID: {pid}");

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
                            Program.LL.LogErrorMessage("_ErrorTerminateProcess", e);
                        }
                    }
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
                        if (UnlockObjectClass.IsLockedObject(path))
                        {
                            UnlockObjectClass.UnlockFile(path);
                        }
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                            Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));

                            deletedFilesCount++;
                        }
                        catch (Exception)
                        {
                            Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                            Program.LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);
                            if (UnlockObjectClass.IsLockedObject(Path.GetDirectoryName(path)))
                            {
                                Utils.ForceUnlockDirectory(Path.GetDirectoryName(path));
                            }
                            try
                            {
                                Program.LL.LogSuccessMessage("_UnlockSuccess");

                                try
                                {
                                    uint processId = Utils.GetProcessIdByFilePath(path);

                                    if (processId != 0)
                                    {
                                        Process process = Process.GetProcessById((int)processId);
                                        if (!process.HasExited)
                                        {
                                            process.Kill();
                                            Program.LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");
                                        }
                                    }
                                }
                                catch (Exception) { }

                                Thread.Sleep(100);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Deleted));

                                    deletedFilesCount++;
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
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Skipped));
                        Program.LL.LogCautionMessage("_Malici0usFile", path);
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


            if (!Program.ScanOnly)
            {
                CleanFoundedMlwr();
            }

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
                            UnlockObjectClass.UnlockFile(path);
                            try
                            {
                                File.SetAttributes(path, FileAttributes.Normal);
                                File.Delete(path);
                                Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");


                            }
                            catch (Exception)
                            {
                                Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                                Program.LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                                if (UnlockObjectClass.IsLockedObject(Path.GetDirectoryName(path)))
                                {
                                    Utils.ForceUnlockDirectory(Path.GetDirectoryName(path));
                                }
                                try
                                {
                                    Program.LL.LogSuccessMessage("_UnlockSuccess");
                                    try
                                    {
                                        uint processId = Utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                                Program.LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");

                                            }
                                        }
                                    }
                                    catch (Exception) { }
                                    Thread.Sleep(100);
                                    File.Delete(path);
                                    if (!File.Exists(path))
                                    {
                                        Program.LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");


                                    }

                                }
                                catch (Exception ex)
                                {

                                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                    Program.totalNeutralizedThreats--;
                                }
                            }
                        }
                        else
                        {
                            Program.LL.LogCautionMessage("_Malici0usFile", path);
                        }
                    }
                }
            }

            if (!Program.ScanOnly)
            {
                Program.LL.LogHeadMessage("_CheckingTermService");
                Program._utils.CheckTermService();
            }

            if (founded_mlwrPathes.Count > 0)
            {
                Program.LL.LogHeadMessage("_RemovingMLWRPaths");

                foreach (string str in founded_mlwrPathes)
                {
                    if (!Program.ScanOnly)
                    {
                        if (Directory.Exists(str))
                        {
                            try
                            {
                                Directory.Delete(str, true);
                                if (!Directory.Exists(str))
                                {
                                    Program.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));

                                }
                            }
                            catch (Exception ex)
                            {
                                List<string> openFiles = Utils.GetOpenFilesInDirectory(str);

                                foreach (var file in openFiles)
                                {
                                    var pid = Utils.GetProcessIdByFilePath(file);
                                    try
                                    {
                                        Process pr = Process.GetProcessById((int)pid);

                                        if (pr != null)
                                        {
                                            if (pr.Id != 0)
                                            {
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


                                if (!Utils.ForceDeleteDirectory(str))
                                {
                                    Program.LL.LogSuccessMessage("_Directory", str, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Deleted));

                                }
                                else
                                {
                                    if (ex.Message.EndsWith(".dll\".") || ex.Message.EndsWith(".exe\"."))
                                    {
                                        Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"{str}\"", "_Directory");
                                        Program.totalNeutralizedThreats--;
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, str, ScanActionType.Error));

                                    }
                                    else
                                    {
                                        Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", $"\"{str}\"", ex.Message);
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

            if (founded_suspLckPths.Count > 0)
            {
                UnlockFolders(founded_suspLckPths);
            }

            if (!Program.WinPEMode)
            {
                Program.LL.LogHeadMessage("_CheckUserJohn");

                if (Utils.CheckUserExists("J?ohn".Replace("?", "")))
                {
                    Program.totalFoundThreats++;
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            Utils.DeleteUser("J?ohn".Replace("?", ""));
                            Thread.Sleep(100);
                            if (!Utils.CheckUserExists("J?ohn".Replace("?", "")))
                            {
                                Program.LL.LogSuccessMessage("_Userprofile", "\"J?ohn\"", "_Deleted");

                            }
                            else
                            {
                                Program.LL.LogErrorMessage("_ErrorCannotRemove", new Exception(""), $"\"John\"", "_Userprofile");
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
                        LocalizedLogger.LogScanOnlyMode();
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

        }
        void UnlockFolders(List<string> inputList)
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
                            Utils.ForceUnlockDirectory(str);
                            if (Utils.IsDirectoryEmpty(str))
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
        void ScanDirectories(List<string> constDirsArray, List<string> newList, bool checkAccessible = false)
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
                            if (Utils.IsDirectoryEmpty(dir))
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
                            if (rule.ApplicationName.ToLower() == programPath.ToLower())
                            {
                                Program.LL.LogMessage("\t[.]", "_Name", rule.Name, ConsoleColor.White);
                                Program.LL.LogWarnMessage("_Path", rule.ApplicationName);

                                if (!Program.ScanOnly)
                                {
                                    string ruleName = rule.Name;
                                    rules.Remove(ruleName);
                                    firewall_items++;
                                    Program.LL.LogSuccessMessage("_Rule", ruleName, "_Deleted");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Firewall:{ruleName}", ScanActionType.Deleted));
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

            try
            {

                var files = Utils.GetFiles(directoryPath, "*.bat");

                foreach (string file in files)
                {
                    if (Utils.IsReparsePoint(Path.GetDirectoryName(file)) || !Utils.IsAccessibleFile(file))
                    {
                        continue;
                    }

                    if (Program._utils.IsBatchFileBad(file))
                    {
                        Program.LL.LogCautionMessage("_Malici0usFile", file);
                        Program.totalFoundThreats++;
                        founded_mlwrPths.Add(file);

                    }
                    else
                    {
                        Program.LL.LogMessage("[.]", "_File", file, ConsoleColor.White);
                    }
                }

                foreach (string nearExeFile in Utils.GetFiles(directoryPath, "*.exe", 0, Program.maxSubfolders))
                {
                    if (Utils.IsSfxArchive(nearExeFile))
                    {
                        WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(nearExeFile);
                        if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.Error)
                        {
                            Program.totalFoundThreats++;
                            founded_mlwrPths.Add(nearExeFile);
                        }

                    }
                }
            }
            catch (Exception) { }


        }
        void CleanHosts()
        {
            Program.LL.LogHeadMessage("_ScanningHosts");

            List<string> linesToDelete = new List<string>();
            string hostsPath_full = $"{Program.drive_letter}:\\Windows\\System32\\drivers\\etc\\hosts";

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


            try
            {

                UnlockObjectClass.UnlockFile(hostsPath_full);
                File.SetAttributes(hostsPath_full, FileAttributes.Normal);

                List<string> lines = File.ReadAllLines(hostsPath_full).ToList();
                int deletedLineCount = 0;

                for (int i = lines.Count - 1; i >= 0; i--)
                {
                    string line = lines[i];

                    if (line.StartsWith("#") || msData.whitelistedWords.Any(word => line.Contains(word)))
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
                                        Program.totalFoundThreats++;
                                        Program.LL.LogSuccessMessage("_MaliciousEntry", lines[i], "_Deleted");
                                        lines.RemoveAt(i);
                                        deletedLineCount++;
                                        break;
                                    }

                                }
                                else
                                {
                                    Program.LL.LogWarnMessage("_SuspiciousEntry", line);
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
                                    Program.LL.LogSuccessMessage("_SuspiciousEntry", line, "_Deleted");
                                    lines.Remove(line);
                                }

                                File.WriteAllLines(hostsPath_full, lines);
                                Program.LL.LogSuccessMessage("_HostsFileRecovered", deletedLineCount.ToString());
                            }
                        }
                        else
                        {
                            linesToDelete.Clear();
                        }
                    }
                }

                if (deletedLineCount > 0)
                {
                    if (!Program.ScanOnly)
                    {
                        File.WriteAllLines(hostsPath_full, lines);
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
                string message = Program.LL.GetLocalizedString("_ErrorLockedFile").Replace("#file#", hostsPath_full);
                Logger.WriteLog($"\t[!!] {message}", Logger.warnMedium);
                scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Error));

            }
            catch (Exception e)
            {
                Program.LL.LogErrorMessage("_ErrorCleanHosts", e);
                scanResults.Add(new ScanResult(ScanObjectType.Unknown, hostsPath_full, ScanActionType.Error));

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


                                object DebbuggerValue = subKey.GetValue("debugger");
                                if (DebbuggerValue != null && DebbuggerValue is string @string)
                                {
                                    string _dValue = @string;
                                    if (!Utils.ResolveEnvironmentVariables(_dValue).ToLower().EndsWith("systray.exe"))
                                    {
                                        Program.totalFoundThreats++;

                                        if (!Program.ScanOnly)
                                        {
                                            subKey.DeleteValue("Debugger");
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"Debugger: {subKeyName} {_dValue}");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Debugger: {subKeyName} {_dValue}".Replace("?", ""), ScanActionType.Deleted));


                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SecurityException) { }
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


                            object DebbuggerValue = subKey.GetValue("debugger");
                            if (DebbuggerValue != null && DebbuggerValue is string @string)
                            {
                                string _dValue = @string;
                                if (!Utils.ResolveEnvironmentVariables(_dValue).ToLower().EndsWith("systray.exe"))
                                {
                                    Program.totalFoundThreats++;
                                    if (!Program.ScanOnly)
                                    {
                                        subKey.DeleteValue("Debugger");
                                        Program.LL.LogSuccessMessage("_RegistryKeyRemoved", $"Debugger: {subKeyName} {_dValue}");
                                        scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Debugger: {subKeyName} {_dValue}".Replace("?", ""), ScanActionType.Deleted));

                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (SecurityException) { }
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
                }
            }
            catch (SecurityException) { }
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
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
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


                        if (AutorunKey.GetValue(value).ToString() == $@"{Program.drive_letter}:\Pro?gra?mDa?ta\Re?aItek?HD\task?host?w.e?x?e".Replace("?", ""))
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
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
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
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\run");
            }

            try
            {
                Logger.WriteLog(@"[Reg] te!kt!on!it...".Replace("!", ""), ConsoleColor.DarkCyan);

                RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(@"Software", true);
                string subKeyName = "tek?toni?t".Replace("?", "");

                if (tektonit.GetSubKeyNames().Contains(subKeyName))
                {
                    Program.LL.LogWarnMessage("_SuspiciousRegKey", "te?kto?nit");
                    Program.totalFoundThreats++;

                    if (!Program.ScanOnly)
                    {
                        if (tektonit.GetSubKeyNames().Contains(subKeyName))
                        {
                            tektonit.DeleteSubKeyTree(subKeyName);
                            if (!tektonit.GetSubKeyNames().Contains(subKeyName))
                            {
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", "tek?t?onit");
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Registry:HKCU:tekt??o?n?it".Replace("?", ""), ScanActionType.Deleted));

                                affected_items++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\t?e??k??ton?i?t");
                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"Registry:HKCU:tekt??o?n?it".Replace("?", ""), ScanActionType.Error));

                Program.totalNeutralizedThreats--;
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

            Logger.WriteLog(@"[Reg] Wind~ows De~fe~nder...".Replace("~", ""), ConsoleColor.DarkCyan);
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
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Registry:WinDefend Policy -> {valueName}", ScanActionType.Deleted));
                                            Program.LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);
                                            affected_items++;
                                        }
                                        else
                                        {
                                            Program.LL.LogWarnMediumMessage("_FoundMlwrKey", valueName);
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Registry:WinDefend Policy -> {valueName}", ScanActionType.Skipped));

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
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Registry:WinDefend Policy -> {valueName}", ScanActionType.Deleted));

                                            affected_items++;
                                        }
                                        else
                                        {
                                            Program.LL.LogWarnMediumMessage("_MaliciousEntry", $"{valueName} (WinD?efen?der)");
                                            scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Registry:WinDefend Policy -> {valueName}", ScanActionType.Skipped));

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotOpen", ex, valueName, "_Exclusion");
                                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"Registry:WinDefend Policy -> {valueName}", ScanActionType.Error));

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
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
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
                $@"{Program.drive_letter}:\Wind?ows\Sys?WOW?64".Replace("?", ""), // SysWow64
                $@"{Program.drive_letter}:\W?in?dow?s\Sys?tem?32\wbem".Replace("?",""), // Wbem
                msData.queries[9], // PowerShell
            };

            string[] badArgStrings =
            {
                "--??al??g??o".Replace("?",""),
                "-?-c?oi?n".Replace("?",""),
                "-?-?p?ass?? x".Replace("?",""),
                "s?tr?at?um+".Replace("?","")
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
                        Program.LL.LogWarnMessage("Task", $"Skipping task with missing properties: {task?.Name ?? "Unknown"}.");
                        continue;
                    }

                    string taskName = task.Name;
                    string taskFolder = task.Folder.ToString();

                    foreach (ExecAction action in task.Definition.Actions.OfType<ExecAction>())
                    {
                        if (action.Path == null)
                        {
                            Program.LL.LogWarnMessage("ActionPath", "Skipping action with null path.");
                            continue;
                        }

                        string arguments = action.Arguments ?? string.Empty; // Если null, подставляем пустую строку
                        string filePath = Utils.ResolveEnvironmentVariables(action.Path.Replace("\"", ""));
                        Program.LL.LogMessage("[#]", "_Scanning", $"{taskName} | {taskFolder}", ConsoleColor.White);

                        if (!Program.ScanOnly)
                        {
                            if (taskName.StartsWith("d?ia?le?r".Replace("?", "")))
                            {
                                Program.LL.LogCautionMessage("_MaliciousEntry", taskName);

                                try
                                {
                                    taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Program.LL.LogSuccessMessage("_MaliciousEntry", $"{taskFolder}\\{taskName}", "_Deleted");
                                        Logger.WriteLog($"\t[+] M@alic@iou@s task {taskName} was deleted".Replace("@", ""), Logger.success);
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.LL.LogErrorMessage("TaskDeletion", ex);
                                }
                            }

                            if (!string.IsNullOrEmpty(arguments))
                            {
                                foreach (string arg in badArgStrings)
                                {
                                    if (arguments.Contains(arg))
                                    {
                                        Program.LL.LogCautionMessage("_MaliciousEntry", taskName);

                                        try
                                        {
                                            taskService.GetFolder(taskFolder)?.DeleteTask(taskName);
                                            if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                            {
                                                Program.LL.LogSuccessMessage("_MaliciousEntry", $"{taskFolder}\\{taskName}", "_Deleted");
                                                Logger.WriteLog($"\t[+] M@alic@iou@s task {taskName} was deleted".Replace("@", ""), Logger.success);
                                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));
                                                break;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Program.LL.LogErrorMessage("TaskDeletion", ex);
                                        }
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(filePath) && filePath.Contains(":\\"))
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

                        Program._utils.ProccedFileFromArgs(checkDirectories, filePath, arguments);

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

                    if (filePath.ToLower().Contains("powershell"))
                    {
                        if (arguments.Contains(Bfs.Create("CXeqnCj42yBfKQxCMVMryRLAKhJaTEwZgqc1rA2AGXxCh5ihx0KFxJP9vffxjXt/EV77lUvxxVA4mRmrhDW5Rr9G0Cxey+RKyZz8HiPO71Q=", "92S1XJVCCrCdqlvfqA5XmsLVck5kW/sl2TDdm8A30c8=", "MrF7kqW0hN4j8eCjbYdWRg==")))
                        {
                            return;
                        }

                        if (arguments.ToLower().Replace("'", "").Contains("-e") || arguments.ToLower().Replace("'", "").Contains("-encodedcommand"))
                        {
                            Program.LL.LogCautionMessage("_MaliciousEntry", taskName);

                            Program.totalFoundThreats++;
                            if (!Program.ScanOnly)
                            {
                                Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));

                            }
                            return;
                        }
                    }

                    if (filePath.ToLower().Contains("msiexec"))
                    {
                        foreach (var argsPart in arguments.Split(' '))
                        {
                            if (argsPart.ToLower().StartsWith("/"))
                            {
                                continue;
                            }

                            string msiFile = Utils.ResolveFilePathFromString(argsPart);
                            if (msiFile.Contains(":\\"))
                            {
                                Program.LL.LogCautionMessage("_MaliciousEntry", taskName);
                                Program.totalFoundThreats++;

                                if (!Program.ScanOnly)
                                {
                                    Program._utils.DeleteTask(taskService, taskFolder, taskName);
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, $"TaskScheduler -> {taskFolder}\\{taskName}", ScanActionType.Deleted));


                                    byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                                    Utils.AddToQuarantine(msiFile, Path.Combine(quarantineFolder, Path.GetFileName(msiFile) + $"_{Utils.CalculateMD5(msiFile)}.bak"), qkey);

                                }

                                return;
                            }
                        }

                    }

                    if (winTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success || new FileInfo(filePath).Length > maxFileSize)
                    {
                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                        return;
                    }

                    if (Utils.IsSfxArchive(filePath))
                    {
                        Program.LL.LogWarnMediumMessage("_sfxArchive", filePath);
                        Program.totalFoundThreats++;
                        founded_mlwrPths.Add(filePath);
                        return;
                    }

                    if (Utils.CheckSignature(filePath, msData.signatures) || Utils.CheckDynamicSignature(filePath, 16, 100))
                    {
                        Program.LL.LogCautionMessage("_Found", filePath);
                        Program.totalFoundThreats++;
                        founded_mlwrPths.Add(filePath);
                        return;
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

            List<ServiceController> suspiciousServiceList = new List<ServiceController>();
            List<string> suspiciousServiceDlls = new List<string>() { };

            // Get all services
            ServiceController[] services = ServiceController.GetServices();

            HashSet<string> trustedPaths = new HashSet<string>();
            string registryPath = @"SYSTEM\CurrentControlSet\Services";
            string ValidServicePath = "";
            bool isMlwrSignature = false;


            foreach (ServiceController service in services)
            {
                string serviceName = service.ServiceName;
                isMlwrSignature = false;

                try
                {
                    ServiceControllerStatus status = service.Status;

                    string servicePathWithArgs = Utils.GetServiceImagePath(serviceName);
                    string servicePath = Utils.ResolveFilePathFromString(servicePathWithArgs).ToLower();

                    Program.LL.LogMessage("[.]", "_ServiceName", serviceName, ConsoleColor.White);
                    Program.LL.LogMessage("[.]", "_Just_Service", servicePathWithArgs, ConsoleColor.White);
                    Program.LL.LogMessage("[.]", "_State", status.ToString(), ConsoleColor.White);

                    if (File.Exists(servicePath))
                    {
                        ValidServicePath = servicePath;
                        FileInfo fi = new FileInfo(ValidServicePath);
                        Logger.WriteLog($"[.] File Size: {Utils.Sizer(fi.Length)}", ConsoleColor.White);
                        if (!trustedPaths.Contains(servicePath))
                        {
                            WinVerifyTrustResult servicePathSignature = winTrust.VerifyEmbeddedSignature(Utils.ResolveEnvironmentVariables(servicePath));
                            if (servicePathSignature != WinVerifyTrustResult.Success && servicePathSignature != WinVerifyTrustResult.Error)
                            {
                                if (fi.Length >= maxFileSize || Utils.CheckSignature(servicePath, msData.signatures) || Utils.CheckDynamicSignature(servicePath, 2096, startSequence, endSequence) || Utils.CheckDynamicSignature(servicePath, 16, 100))
                                {
                                    Program.LL.LogCautionMessage("_Found", $"{service.ServiceName} {servicePath}");
                                    isMlwrSignature = true;
                                    suspiciousServiceList.Add(service);
                                }
                            }
                            else
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
                                            string serviceDll = Utils.ResolveEnvironmentVariables(serviceDllValue.ToString());
                                            Logger.WriteLog($"[.] ServiceDll: {serviceDll}", ConsoleColor.White);
                                            if (File.Exists(serviceDll))
                                            {
                                                WinVerifyTrustResult serviceDllSignature = winTrust.VerifyEmbeddedSignature(serviceDll);
                                                if (serviceDllSignature != WinVerifyTrustResult.Success && serviceDllSignature != WinVerifyTrustResult.SubjectCertExpired && serviceDllSignature != WinVerifyTrustResult.Error)
                                                {
                                                    Program.LL.LogCautionMessage("_Found", $"{service.ServiceName} {serviceDll}");
                                                    suspiciousServiceList.Add(service);
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
                if (suspiciousServiceList.Count > 5)
                {
                    string suspiciousServicesNames = string.Join(Environment.NewLine, suspiciousServiceList.Select(s => s.DisplayName));
                    string message = Program.LL.GetLocalizedString("_DisableServiceWarning");

                    DialogResult result = MessageBox.Show(message.Replace("#InvalidServices#", suspiciousServicesNames), Utils.GetRndString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        foreach (var suspiciousService in suspiciousServiceList)
                        {
                            DisableService(suspiciousService, ValidServicePath, isMlwrSignature, suspiciousServiceDlls);
                        }
                    }
                    else
                    {
                        foreach (var suspiciousService in suspiciousServiceList)
                        {
                            Program.LL.LogMessage("[i]", "_SkipServiceMessage", suspiciousService.DisplayName, ConsoleColor.White);
                        }
                    }
                }
                else
                {
                    foreach (var suspiciousService in suspiciousServiceList)
                    {
                        DisableService(suspiciousService, ValidServicePath, isMlwrSignature, suspiciousServiceDlls);
                    }
                }
            }
            else
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }

        private void DisableService(ServiceController service, string servicePath, bool isMlwrSigChk, List<string> serviceDlls = null)
        {
            string serviceName = service.ServiceName;

            ServiceStartMode startMode = Utils.GetServiceStartType(serviceName);
            ServiceControllerStatus status = service.Status;

            if (startMode != ServiceStartMode.Disabled)
            {
                Program.totalFoundThreats++;
                Utils.SetServiceStartType(service.ServiceName, ServiceStartMode.Disabled);
                Program.LL.LogSuccessMessage("_ServiceDisabled", serviceName);

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
                        int ServicePID = Utils.GetServiceId(service.ServiceName);
                        Utils.UnProtect(new int[] { ServicePID });
                        Process serviceProcess = Process.GetProcessById(ServicePID);

                        string args = Utils.GetCommandLine(serviceProcess);
                        if (args != null && args.Contains($"-s {service.ServiceName}"))
                        {
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
            startMode = Utils.GetServiceStartType(service.ServiceName);
            status = service.Status;
            if (startMode != ServiceStartMode.Disabled || status != ServiceControllerStatus.Stopped)
            {
                Program.totalNeutralizedThreats--;
                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{Program.LL.GetLocalizedString("_Just_Service")} { service.ServiceName }", ScanActionType.Error));
            }

            if (serviceDlls != null)
            {
                if (serviceDlls.Count != 0)
                {
                    byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                    foreach (string serviceDll in serviceDlls)
                    {
                        if (File.Exists(serviceDll))
                        {
                            Utils.AddToQuarantine(serviceDll, Path.Combine(quarantineFolder, Path.GetFileName(serviceDll) + $"_{Utils.CalculateMD5(servicePath)}.bak"), qkey); //MD5 Hash from original file
                            if (!File.Exists(serviceDll))
                            {
                                scanResults.Add(new ScanResult(ScanObjectType.Suspicious, $"{Program.LL.GetLocalizedString("_Just_Service")} {service.ServiceName} -> {serviceDll}", ScanActionType.Quarantine));
                            }
                        }
                    }
                }

            }

            if (isMlwrSigChk)
            {

                if (File.Exists(servicePath))
                {
                    byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                    Utils.AddToQuarantine(servicePath, Path.Combine(quarantineFolder, Path.GetFileName(servicePath) + $"_{Utils.CalculateMD5(servicePath)}.bak"), qkey); //MD5 Hash from original file
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + servicePath, ScanActionType.Quarantine));

                }

                try
                {
                    if (service != null)
                    {
                        ServiceHelper.Uninstall(service.ServiceName);
                        Program.LL.LogSuccessMessage("_MaliciousService", service.ServiceName, "_Deleted");
                        scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + " -> " + service.ServiceName, ScanActionType.Deleted));

                    }
                }
                catch (Win32Exception win32Ex)
                {
                    if (win32Ex.NativeErrorCode != 1072 && win32Ex.NativeErrorCode != 1060) // ERROR_SERVICE_MARKED_FOR_DELETE
                    {
                        Program.LL.LogErrorMessage("_ErrorCannotRemove", win32Ex, service.ServiceName, "_Service");
                        Program.totalNeutralizedThreats--;
                    }
                }
                catch (Exception ex)
                {
                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, service.ServiceName, "_Service");
                    Program.totalNeutralizedThreats--;
                    scanResults.Add(new ScanResult(ScanObjectType.Malware, Program.LL.GetLocalizedString("_Just_Service") + " -> " + servicePath, ScanActionType.Error));

                }
            }
        }

        public void SignatureScan()
        {
            Program.LL.LogHeadMessage("_StartSignatureScan");

            if (Program.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                var localDrives = allDrives.Where(drive => drive.DriveType == DriveType.Fixed && !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));
                foreach (var drive in localDrives)
                {
                    msData.obfStr6.Add(drive.Name);
                }
            }

#if !DEBUG
            if (!Program.WinPEMode)
            {
                msData.obfStr6.Add(Path.GetTempPath());
                msData.obfStr6.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }
#endif
            foreach (string path in msData.obfStr6)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }

                List<string> executableFiles = Utils.GetFiles(path, "*.exe", 0, Program.maxSubfolders);
                foreach (var filepath in executableFiles)
                {
                    AnalyzeFile(Utils.GetLongPath(filepath));
                }
                executableFiles.Clear();
            }

            if (!Program.ScanOnly && founded_mlwrPths.Count == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }
            else
            {
                if (!Program.ScanOnly)
                {
                    Program.LL.LogWarnMediumMessage("_FoundThreats", founded_mlwrPths.Count.ToString());
                    Program.LL.LogHeadMessage("_RestartCleanup");
                    CleanFoundedMlwr();
                }
                else LocalizedLogger.LogScanOnlyMode();
            }
        }

        public void CleanFoundedMlwr()
        {
            if (founded_mlwrPths.Count > 0)
            {
                Program.LL.LogHeadMessage("_RemovingFoundMlwrFiles");

                foreach (string path in founded_mlwrPths)
                {
                    if (File.Exists(path))
                    {
                        byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                        if (UnlockObjectClass.IsLockedObject(path))
                        {
                            UnlockObjectClass.UnlockFile(path);
                        }
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey); //MD5 Hash from original file
                            scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Quarantine));

                        }
                        catch (Exception)
                        {
                            Program.LL.LogWarnMediumMessage("_ErrorCannotRemove", path);
                            Program.LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                            if (UnlockObjectClass.IsLockedObject(new FileInfo(path).DirectoryName))
                            {
                                UnlockObjectClass.UnlockFile(new FileInfo(path).DirectoryName);
                            }
                            try
                            {
                                Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey);
                                if (!File.Exists(path))
                                {
                                    Program.LL.LogSuccessMessage("_Malici0usFile", path, "_MovedToQuarantine");
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Quarantine));

                                }

                            }
                            catch (Exception ex)
                            {
                                Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                Program.LL.LogMessage("\t[.]", "_FindBlockingProcess", "", ConsoleColor.White);

                                try
                                {
                                    try
                                    {
                                        uint processId = Utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                                Program.LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");

                                            }
                                        }
                                    }
                                    catch (Exception) { }

                                    Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey);
                                    if (!File.Exists(path))
                                    {
                                        Program.LL.LogSuccessMessage("_Malici0usFile", path, "_MovedToQuarantine");
                                        scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Quarantine));

                                    }
                                }
                                catch (Exception ex1)
                                {
                                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex1, path, "_File");
                                    Program.totalNeutralizedThreats--;
                                    scanResults.Add(new ScanResult(ScanObjectType.Malware, path, ScanActionType.Error));

                                }
                            }
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

        public void AnalyzeFile(string file, bool verboseLog = true)
        {

            if (!File.Exists(file) || file.Length > 240)
            {
                return;
            }

            if (verboseLog)
            {
                LocalizedLogger.LogAnalyzingFile(file);
            }

            try
            {

                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Length > maxFileSize || fileInfo.Length < minFileSize)
                {
                    if (verboseLog)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\t[OK]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return;
                }

                WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(file);
                if (trustResult == WinVerifyTrustResult.Success)
                {
                    if (verboseLog)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\t[OK]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return;
                }

                if (Utils.IsSfxArchive(file))
                {
                    Program.LL.LogWarnMediumMessage("_sfxArchive", file);
                    Program.totalFoundSuspiciousObjects++;
                    scanResults.Add(new ScanResult(ScanObjectType.Suspicious, file, ScanActionType.Skipped));

                    return;
                }

                bool sequenceFound = Utils.CheckSignature(file, msData.signatures);

                if (sequenceFound)
                {
                    Program.LL.LogCautionMessage("_Found", file);
                    Program.totalFoundThreats++;
                    founded_mlwrPths.Add(file);
                    return;
                }

#if !DEBUG
                bool computedSequence = Utils.CheckDynamicSignature(file, 16, 100);

                if (computedSequence)
                {
                    founded_mlwrPths.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);

                    return;
                }

                bool computedSequence2 = Utils.CheckDynamicSignature(file, 2096, startSequence, endSequence);
                if (computedSequence2)
                {
                    founded_mlwrPths.Add(file);
                    Program.totalFoundThreats++;
                    Program.LL.LogCautionMessage("_Found", file);
                    return;
                }

#endif
                if (verboseLog)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\t[OK]");
                    Console.ForegroundColor = ConsoleColor.White;
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
        }

        internal static void SentLog()
        {
            if (Utils.GetWindowsVersion().Contains("Windows 7") || Program.bootMode == BootMode.SafeMinimal)
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

            TelegramAPI.UploadFile(Path.Combine(Logger.LogsFolder, Logger.logFileName), Utils.GetDeviceId());
        }
    }
}
