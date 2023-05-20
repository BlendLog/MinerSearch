using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace MinerSearch
{
    public class MinerSearch
    {
        int[] _PortList = new[]
        {
            9999,
            14444,
            14433,
            6666,
            16666,
            6633,
            16633,
            4444,
            14444,
            3333,
            13333,
            7777,
            5555,
            9980
        };

        readonly string[] _Nvidia = new[]
        {
            "nvcompiler.dll",
            "nvopencl.dll",
            "nvfatbinaryLoader.dll",
            "nvapi64.dll",
            "OpenCL.dll"
        };

        public List<string> SuspiciousLockedPaths = new List<string>
        {
            @"C:\ProgramData\360safe",
            @"C:\ProgramData\AVAST Software",
            @"C:\ProgramData\Avira",
            @"C:\ProgramData\BookManager",
            @"C:\ProgramData\Doctor Web",
            @"C:\ProgramData\ESET",
            @"C:\ProgramData\Evernote",
            @"C:\ProgramData\FingerPrint",
            @"C:\ProgramData\Install",
            @"C:\ProgramData\Kaspersky Lab",
            @"C:\ProgramData\Kaspersky Lab Setup Files",
            @"C:\ProgramData\MB3Install",
            @"C:\ProgramData\Malwarebytes",
            @"C:\ProgramData\McAfee",
            @"C:\ProgramData\Microsoft\Check",
            @"C:\ProgramData\Microsoft\Intel",
            @"C:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64",
            @"C:\ProgramData\Microsoft\temp",
            @"C:\ProgramData\Norton",
            @"C:\ProgramData\PuzzleMedia",
            @"C:\ProgramData\RealtekHD",
            @"C:\ProgramData\ReaItekHD",
            @"C:\ProgramData\RobotDemo",
            @"C:\ProgramData\RunDLL",
            @"C:\ProgramData\Setup",
            @"C:\ProgramData\System32",
            @"C:\ProgramData\WavePad",
            @"C:\ProgramData\Windows Tasks Service",
            @"C:\ProgramData\WindowsTask",
            @"C:\ProgramData\grizzly",
            @"C:\Program Files (x86)\Microsoft JDX",
            @"C:\Program Files (x86)\360",
            @"C:\Program Files (x86)\SpyHunter",
            @"C:\Program Files (x86)\AVAST Software",
            @"C:\Program Files (x86)\AVG",
            @"C:\Program Files (x86)\Kaspersky Lab",
            @"C:\Program Files (x86)\Cezurity",
            @"C:\Program Files (x86)\GRIZZLY Antivirus",
            @"C:\Program Files (x86)\Panda Security",
            @"C:\Program Files (x86)\IObit\Advanced SystemCare",
            @"C:\Program Files (x86)\IObit\IObit Malware Fighter",
            @"C:\Program Files (x86)\IObit",
            @"C:\Program Files (x86)\Transmission",
            @"C:\Program Files\AVAST Software",
            @"C:\Program Files\AVG",
            @"C:\Program Files\Bitdefender Agent",
            @"C:\Program Files\ByteFence",
            @"C:\Program Files\COMODO",
            @"C:\Program Files\Cezurity",
            @"C:\Program Files\Common Files\AV",
            @"C:\Program Files\Common Files\Doctor Web",
            @"C:\Program Files\Common Files\McAfee",
            @"C:\Program Files\DrWeb",
            @"C:\Program Files\ESET",
            @"C:\Program Files\Enigma Software Group",
            @"C:\Program Files\Internet Explorer\bin",
            @"C:\Program Files\Kaspersky Lab",
            @"C:\Program Files\Loaris Trojan Remover",
            @"C:\Program Files\Malwarebytes",
            @"C:\Program Files\Process Lasso",
            @"C:\Program Files\Rainmeter",
            @"C:\Program Files\Ravantivirus",
            @"C:\Program Files\SpyHunter",
            @"C:\AdwCleaner",
            @"C:\KVRT_Data",
            @"C:\KVRT2020_Data",
            @"C:\FRST",
            @"C:\Windows\Fonts\Mysql"
        };

        List<string> suspiciousFiles_path = new List<string>();

        private readonly string[] SystemFileNames = new[]
        {
            "audiodg",
            "taskhostw",
            "taskhost",
            "conhost",
            "svchost",
            "dwm",
            "rundll32",
            "winlogon",
            "csrss",
            "services",
            "lsass",
            "dllhost",
            "smss",
            "wininit",
            "vbc",
            "unsecapp"
        };

        public List<string> malware_files = new List<string>()
        {
            @"C:\ProgramData\Microsoft\win.exe"
        };

        private readonly long[] constantFileSize = new long[]
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
            57344 //unsecapp
        };

        public List<int> malware_pids = new List<int>();
        public List<string> malware_dirs = new List<string>();
        public bool CleanupHosts = false;
        public int suspiciousObj_count = 0;
        public bool RatProcessExists = false;
        public string WindowsVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductName").ToString();

        public void Scan()
        {
            string processName = "";
            string fullPath = "";
            string args = "";
            int riskLevel = 0;
            long fileSize = 0;
            bool isValidProcess;
            Stopwatch startTime = Stopwatch.StartNew();
            List<Connection> cons = new List<Connection>();
            List<Process> procs = GetProcesses();

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {

                if (!p.HasExited)
                {
                    processName = p.ProcessName.ToLower();
                    Logger.WriteLog($"Scanning: {processName}.exe", ConsoleColor.White);
                }
                else
                    continue;


                riskLevel = 0;
                isValidProcess = false;



                if (WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Error)
                {
                    if (WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Success)
                    {
                        riskLevel += 1;
                        isValidProcess = false;
                    }
                    else
                        isValidProcess = true;
                }


                fullPath = p.MainModule.FileName.ToLower();
                try
                {
                    fileSize = new FileInfo(p.MainModule.FileName).Length;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error get file size: {ex.Message}", Logger.error);
                }


                if (processName.Contains("helper") && !isValidProcess)
                {
                    riskLevel += 1;
                }

                try
                {
                    if (p.MainModule.FileVersionInfo.FileDescription != null)
                    {
                        if (p.MainModule.FileVersionInfo.FileDescription.ToLower().Contains("svhost"))
                        {
                            Logger.WriteLog($"\t[!] Probably RAT process: {fullPath} Process ID: {p.Id}", Logger.warn);
                            suspiciousFiles_path.Add(fullPath);
                            riskLevel += 3;
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.WriteLog($"\t[x] Error get file description: {fullPath} | Process ID: {p.Id}", Logger.error);
                }

                int modCount = 0;
                foreach (ProcessModule pMod in p.Modules)
                {
                    foreach (string name in _Nvidia)
                        if (pMod.ModuleName.ToLower().Equals(name.ToLower()))
                            modCount++;
                }

                if (modCount > 2)
                {
                    Logger.WriteLog("\t[!] Too much GPU libs usage: " + p.ProcessName + ".exe, Process ID: " + p.Id, Logger.warn);
                    riskLevel += 1;

                }

                cons = GetConnections();
                IEnumerable<Connection> tiedConnections = cons.Where(x => x.ProcessId == p.Id);
                IEnumerable<Connection> badPorts = tiedConnections.Where(x => _PortList.Any(y => y == x.RemotePort));
                foreach (Connection conn in badPorts)
                {
                    Logger.WriteLog("\t[!] " + conn, Logger.warn);
                    if (processName == "notepad")
                    {
                        riskLevel += 2;
                    }
                    riskLevel += 1;
                }

                args = GetCommandLine(p).ToLower();
                if (args != null)
                {
                    foreach (int port in _PortList)
                    {
                        bool portActive = badPorts.Any(x => x.RemotePort == port);
                        if (portActive && args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.WriteLog($"\t[!] {processName}.exe: Active blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                        else if (args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.WriteLog($"\t[!] {processName}.exe: Blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                    }
                    if (args.Contains("stratum"))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"stratum\" in cmd args.", Logger.warn);
                    }
                    if (args.Contains("nanopool") || args.Contains("pool."))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"pool\" in cmd args.", Logger.warn);
                    }

                    if (args.Contains("-systemcheck"))
                    {
                        riskLevel += 4;
                        Logger.WriteLog("\t[!] Probably fake system task", Logger.warn);
                        if (fullPath.Contains("appdata") && fullPath.Contains("windows"))
                        {
                            riskLevel += 1;
                            suspiciousFiles_path.Add(fullPath);
                        }

                    }

                    if ((processName == SystemFileNames[3] && !args.Contains("\\??\\c:\\")))
                    {
                        Logger.WriteLog($"\t[!] Probably watchdog process. Process ID: {p.Id}", Logger.warn);
                        riskLevel += 3;
                    }
                    if (processName == SystemFileNames[4] && !args.Contains($"{SystemFileNames[4]}.exe -k"))
                    {
                        Logger.WriteLog($"\t[!] Probably process injection. Process ID: {p.Id}", Logger.warn);
                        riskLevel += 3;
                    }
                    if (processName == SystemFileNames[5])
                    {
                        int argsLen = args.Length;
                        bool isFakeDwm = false;
                        if (WindowsVersion.ToLower().Contains("windows 7") && argsLen > 29 || !WindowsVersion.ToLower().Contains("windows 7") && argsLen > 9)
                        {
                            isFakeDwm = true;
                        }

                        if (isFakeDwm)
                        {
                            Logger.WriteLog($"\t[!] Probably process injection. Process ID: {p.Id}", Logger.warn);
                            riskLevel += 2;
                        }
                    }

                }



                bool isSuspiciousPath = false;
                for (int i = 0; i < SystemFileNames.Length; i++)
                {

                    if (processName == SystemFileNames[i])
                    {

                        if (!fullPath.Contains("c:\\windows\\system32")
                            && !fullPath.Contains("c:\\windows\\syswow64")
                            && !fullPath.Contains("c:\\windows\\winsxs\\amd64")
                            && !fullPath.Contains("c:\\windows\\microsoft.net\\framework64")
                            && !fullPath.Contains("c:\\windows\\microsoft.net\\framework"))
                        {

                            Logger.WriteLog($"\t[!] Suspicious path: {fullPath}", Logger.warn);
                            isSuspiciousPath = true;
                            riskLevel += 3;
                        }


                        if (fileSize >= constantFileSize[i] * 3 && !isValidProcess)
                        {
                            Logger.WriteLog($"\t[!] Suspicious file size: {Sizer(fileSize)}", Logger.warn);
                            riskLevel += 1;
                        }

                    }

                }

                if (processName == "unsecapp" && !p.MainModule.FileName.ToLower().Contains(@"c:\windows\system32\wbem"))
                {
                    Logger.WriteLog($"\t[!] Probably watchdog process. Process ID: {p.Id}", Logger.warn);
                    isSuspiciousPath = true;
                    riskLevel += 3;
                }

                if (processName == "rundll" || processName == "system" || processName == "winserv")
                {
                    Logger.WriteLog($"\t[!] Probably RAT process: {fullPath} Process ID: {p.Id}", Logger.warn);
                    isSuspiciousPath = true;
                    RatProcessExists = true;
                    riskLevel += 6;
                }

                if (processName == "explorer")
                {
                    int ParentProcessId = GetParentProcessId(p.Id);
                    if (ParentProcessId != 0)
                    {
                        try
                        {
                            Process ParentProcess = Process.GetProcessById(ParentProcessId);
                            if (ParentProcess.ProcessName.ToLower() == "explorer")
                            {
                                riskLevel += 2;
                            }
                        }
                        catch { }

                    }
                }


                if (riskLevel >= 3)
                {
                    Logger.WriteLog("\t[!!!] Malicious process found! Risk level: " + riskLevel, Logger.caution);
                    SuspendProcess(p.Id);

                    if (isSuspiciousPath)
                    {
                        try
                        {
                            string rnd = GetHash();
                            string NewFilePath = Path.Combine(Path.GetDirectoryName(p.MainModule.FileName), $"{Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe");
                            File.Move(p.MainModule.FileName, NewFilePath); //Rename malicious file
                            Logger.WriteLog($"\t[+] File renamed to {Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe", Logger.success);
                            suspiciousFiles_path.Add(NewFilePath);
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLog($"\t[x] Cannot rename file: {e.Message}", Logger.error);
                            suspiciousFiles_path.Add(p.MainModule.FileName);
                        }

                    }
                    malware_pids.Add(p.Id);


                }

            }

            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Console.WriteLine($"\nScan elapsed time: {elapsedTime}");
        }

        public void StaticScan()
        {

            Logger.WriteLog("Scanning directories...", Logger.head);

            ScanDirectories(SuspiciousLockedPaths);

            foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "*.exe", SearchOption.AllDirectories))
            {
                suspiciousFiles_path.Add(Path.GetFullPath(file));
            }

            string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft");
            FindMalwareFiles(baseDirectory);



            foreach (string path in suspiciousFiles_path)
            {
                Logger.WriteLog($"\t[!] Suspicious file {path}", Logger.warn);
            }

            Logger.WriteLog("Scanning registry...", Logger.head);
            ScanRegistry();
            Logger.WriteLog("Scanning hosts file...", Logger.head);
            ScanHosts();
            Logger.WriteLog($"Scanning Tasks...", Logger.head);
            try
            {
                ScanTaskScheduler();
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Scan Task error: {ex.Message}", Logger.error);
            }
            suspiciousObj_count += malware_dirs.Count + suspiciousFiles_path.Count;
        }
        public void Clean()
        {

            if (malware_pids.Count != 0)
            {
                Logger.WriteLog("Trying to close processes...", Logger.head);

                UnProtect(malware_pids.ToArray());

                foreach (var id in malware_pids)
                {

                    try
                    {
                        Process p = Process.GetProcessById(id);
                        string pname = p.ProcessName;
                        int pid = p.Id;

                        p.Kill();

                        if (p.HasExited)
                            Logger.WriteLog($"\t[+] Malicious process {pname} - pid:{pid} successfully closed", Logger.success);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Failed to kill malicious process! {ex.Message}", Logger.error);
                        continue;
                    }
                }

                if (RatProcessExists)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c sc stop TermService && exit",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }).WaitForExit();
                }
            }

            if (suspiciousFiles_path.Count > 0)
            {
                Logger.WriteLog("Removing malicious files...", Logger.head);

                foreach (string path in suspiciousFiles_path)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            UnlockFile(path);
                            File.Delete(path);
                            Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                        }
                        catch (Exception)
                        {
                            Logger.WriteLog($"\t[!!] Cannot delete file {path}", Logger.cautionLow);
                            Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                            UnlockDirectory(new FileInfo(path).DirectoryName);
                            try
                            {

                                UnlockFile(path);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                                Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message}", Logger.error);
#endif
                            }
                        }
                    }
                }
            }

            if (malware_files.Count > 0)
            {
                Logger.WriteLog("Removing malicious files...", Logger.head);

                foreach (string path in malware_files)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            UnlockFile(path);
                            File.Delete(path);
                            Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                                Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message}", Logger.error);
#endif
                        }
                    }
                }
            }

            if (malware_dirs.Count != 0)
            {
                Logger.WriteLog("Removing malicious dirs...", Logger.head);

                foreach (string str in malware_dirs)
                {

                    UnlockDirectory(str);
                    try
                    {
                        Directory.Delete(str, true);
                        if (!Directory.Exists(str))
                        {
                            Logger.WriteLog($"\t[+] Directory {str} successfull deleted", Logger.success);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Logger.WriteLog($"\t[x] Failed to delete directory \"{str}\" | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                        Logger.WriteLog($"\t[x] Failed to delete directory \"{str}\" | {ex.Message}", Logger.error);
#endif
                    }


                }
            }


        }

        void ScanDirectories(List<string> constDirsArray)
        {
            SuspiciousLockedPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "autologger"));
            SuspiciousLockedPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av_block_remover"));
            foreach (string dir in constDirsArray)
            {
                if (Directory.Exists(dir))
                {
                    Logger.WriteLog($"\t[!!] Directory: \"{dir}\"", Logger.cautionLow);
                    malware_dirs.Add(dir);
                }
            }
        }

        void FindMalwareFiles(string directoryPath)
        {
            try
            {
                IEnumerable<string> files = Directory.GetFiles(directoryPath, "*.bat");

                foreach (string file in files)
                {
                    malware_files.Add(file);
                    foreach (var nearExeFile in Directory.GetFiles(Path.GetDirectoryName(file), "*.exe"))
                    {
                        malware_files.Add(nearExeFile);
                    }
                }

                IEnumerable<string> directories = Directory.EnumerateDirectories(directoryPath);
                foreach (string directory in directories)
                {
                    FindMalwareFiles(directory);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        void ScanHosts()
        {

            RegistryKey hostsDir = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters");
            int hostsRiskLevel = 0;
            if (hostsDir != null)
            {
                string hostsPath = hostsDir.GetValue("DataBasePath").ToString();
                if (hostsPath.StartsWith("%"))
                {
                    hostsPath = ResolveEnvironmentVariables(hostsPath);
                }
                if (!File.Exists(hostsPath + "\\hosts"))
                {
                    Logger.WriteLog("[!] Hosts file is missing", Logger.warn);
                    return;
                }
                FileInfo hostsinfo = new FileInfo(hostsPath + "\\hosts");
                if (hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System | FileAttributes.NotContentIndexed) || hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System) || hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive))
                {
                    Logger.WriteLog($"\t[!] Has \"hidden\" attribute", Logger.warn);
                    hostsRiskLevel += 2;
                }
                if (hostsinfo.Length > 1024)
                {
                    Logger.WriteLog($"\t[!] Suspicious file size: {Sizer(hostsinfo.Length)}", Logger.warn);
                    hostsRiskLevel++;
                }
                if (hostsRiskLevel >= 1)
                {

                    Logger.WriteLog($"\t[!!] Infected hosts file. Risk level {hostsRiskLevel}", Logger.cautionLow);
                    string answer;
                answerLabel:
                    Console.WriteLine("\nThe hosts file will be completely overwritten. Continue? [enter \"y\" to confirm | \"n\" - skip cleaning]:");
                    answer = Console.ReadLine().ToLower().Trim();
                    switch (answer)
                    {
                        case "y":
                            suspiciousObj_count++;
                            CleanupHosts = true;
                            if (File.Exists(Path.Combine(hostsPath, "hosts.infected")))
                            {
                                File.Delete(Path.Combine(hostsPath, "hosts.infected"));
                            }

                            try
                            {
                                File.SetAttributes(Path.Combine(hostsPath, "hosts"), FileAttributes.Normal);
                                File.Move(Path.Combine(hostsPath, "hosts"), Path.Combine(hostsPath, "hosts.infected"));
                                File.Create(Path.Combine(hostsPath, "hosts"));
                            }

                            catch (Exception ex)
                            {
                                Logger.WriteLog($"\t[x] Error cleanup hosts file: {ex.Message}", Logger.error);
                            }

                            hostsinfo = new FileInfo(Path.Combine(hostsPath, "hosts"));
                            if (hostsinfo.Length <= 1024)
                            {
                                if (hostsRiskLevel >= 1)
                                {
                                    Logger.WriteLog("\t[+] The hosts file has been successfully cleaned", Logger.success);
                                }
                            }
                            else
                                Logger.WriteLog("\t[x] Failed to clear hosts file", Logger.error);
                            break;
                        case "n":
                            break;
                        default:
                            goto answerLabel;
                    }

                }
            }
        }
        void ScanRegistry()
        {


            #region DisallowRun
            RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            if (DisallowRunKey != null)
            {
                if (DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                {
                    Logger.WriteLog("\t[!] Suspicious registry key: DisallowRun - restricts the launch of the specified applications", Logger.warn);
                    suspiciousObj_count++;
                    DisallowRunKey.DeleteValue("DisallowRun");
                    if (!DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                    {
                        Logger.WriteLog("\t[+] DisallowRun key successfully deleted", Logger.success);
                    }
                }
                RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", true);
                if (DisallowRunSub != null)
                {
                    DisallowRunKey.DeleteSubKeyTree("DisallowRun");
                    DisallowRunSub = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", true);
                    if (DisallowRunSub == null)
                    {
                        Logger.WriteLog("\t[+] DisallowRun hive successfully deleted", Logger.success);
                    }
                }
            }
            #endregion


            #region Appinit_dlls
            RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", true);
            if (appinit_key != null)
            {
                if (!String.IsNullOrEmpty(appinit_key.GetValue("AppInit_DLLs").ToString()))
                {
                    if (appinit_key.GetValue("LoadAppInit_DLLs").ToString() == "1")
                    {
                        if (!appinit_key.GetValueNames().Contains("RequireSignedAppInit_DLLs"))
                        {
                            Logger.WriteLog("\t[!] AppInit_DLLs is not empty", Logger.warn);
                            Logger.WriteLog("\t[!!!] RequireSignedAppInit_DLLs key is not found", Logger.caution);
                            appinit_key.SetValue("RequireSignedAppInit_DLLs", 1, RegistryValueKind.DWord);
                            if (appinit_key.GetValue("RequireSignedAppInit_DLLs").ToString() == "1")
                            {
                                Logger.WriteLog("\t[+] The value was created and set to 1", Logger.success);
                            }
                            suspiciousObj_count += 2;
                        }
                        else if (appinit_key.GetValue("RequireSignedAppInit_DLLs").ToString() == "0")
                        {
                            Logger.WriteLog("\t[!] AppInit_DLLs is not empty", Logger.warn);
                            Logger.WriteLog("\t[!!!] RequireSignedAppInit_DLLs key set is 0", Logger.caution);
                            suspiciousObj_count += 2;
                            appinit_key.SetValue("RequireSignedAppInit_DLLs", 1, RegistryValueKind.DWord);
                            if (appinit_key.GetValue("RequireSignedAppInit_DLLs").ToString() == "1")
                            {
                                Logger.WriteLog("\t[+] The value was set to 1", Logger.success);
                            }
                        }
                    }
                }
            }
            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"HKLM\Software\Microsoft\Windows\CurrentVersion\Run", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();

                    foreach (string value in RunKeys)
                    {
                        string path = GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Error)
                            {
                                WinTrust.VerifyEmbeddedSignature(path);
                            }

                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKLM\\...\\run: {ex.Message}", Logger.error);
            }

            //TODO: delete malicious win defender exclusions
            #endregion

            #region HKCU
            try
            {
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Run", ConsoleColor.DarkCyan);

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Error)
                            {
                                WinTrust.VerifyEmbeddedSignature(path);
                            }
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\run: {ex.Message}", Logger.error);
            }

            try
            {
                RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(@"Software", true);
                if (tektonit != null)
                {
                    Logger.WriteLog(@"HKCU\Software", ConsoleColor.DarkCyan);
                    if (tektonit.GetSubKeyNames().Contains("tektonit"))
                    {
                        Logger.WriteLog("\t[!] Suspicious registry key: tektonit", Logger.warn);
                        suspiciousObj_count++;
                        tektonit.DeleteSubKeyTree("tektonit");
                        if (!tektonit.GetSubKeyNames().Contains("tektonit"))
                        {
                            Logger.WriteLog("\t[+] tektonit key successfully deleted", Logger.success);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\tektonit: {ex.Message}", Logger.error);
            }
            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Error)
                            {
                                WinTrust.VerifyEmbeddedSignature(path);
                            }
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open WOW6432Node\\...\\run: {ex.Message}", Logger.error);
            }
            #endregion
        }

        void ScanTaskScheduler()
        {
            TaskService taskService = new TaskService();
            IEnumerable<Task> Tasks = taskService.AllTasks;
            HashSet<string> scannedFilePaths = new HashSet<string>();
            foreach (Task task in Tasks.OrderBy(t => t.Name).ToList())
            {
                if (task != null)
                {
                    foreach (ExecAction action in task.Definition.Actions.OfType<ExecAction>())
                    {
                        string filePath = ResolveEnvironmentVariables(action.Path.Replace("\"", ""));
                        if (scannedFilePaths.Contains(filePath))
                        {
                            continue;
                        }
                        else
                        {
                            scannedFilePaths.Add(filePath);
                        }

                        Logger.WriteLog($"Scan: {task.Name}", ConsoleColor.White);
                        Logger.WriteLog($"Location: {task.Folder}", ConsoleColor.White);

                        if (filePath.Contains(":\\"))
                        {
                            if (!File.Exists(filePath))
                            {
                                Logger.WriteLog($"\t[!] File is not exists: {filePath}", Logger.warn);

                                if (Program.RemoveEmptyTasks)
                                {
                                    string taskName = task.Name;
                                    string taskFolder = task.Folder.ToString();

                                    taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Logger.WriteLog($"\t[+] Empty Task {taskName} was deleted", Logger.success);
                                    }
                                }

                            }
                            else
                                Logger.WriteLog($"\t[#] File path: {filePath}", ConsoleColor.Blue);

                        }
                        else
                        {
                            string system32Path = Path.Combine(Environment.SystemDirectory, filePath);

                            if (!File.Exists(system32Path))
                            {
                                Logger.WriteLog($"\t[!] File is not exists: {system32Path}", Logger.warn);

                                if (Program.RemoveEmptyTasks)
                                {
                                    string taskName = task.Name;
                                    string taskFolder = task.Folder.ToString();

                                    taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Logger.WriteLog($"\t[+] Empty Task {taskName} was deleted", Logger.success);
                                    }
                                }
                            }
                            else
                                Logger.WriteLog($"\t[#] File path: {system32Path}", ConsoleColor.Blue);
                        }
                    }
                }
            }
        }
        void UnlockDirectory(string dir)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                DirectorySecurity directorySecurity = new DirectorySecurity();
                directorySecurity.SetOwner(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
                directoryInfo.SetAccessControl(directorySecurity);
                directorySecurity = directoryInfo.GetAccessControl();
                AuthorizationRuleCollection acl = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (FileSystemAccessRule ace in acl)
                {
                    if (ace.AccessControlType == AccessControlType.Deny || (ace.FileSystemRights != FileSystemRights.FullControl && !ace.IsInherited))
                    {
                        directorySecurity.RemoveAccessRule(ace);
                    }

                }
                FileSystemAccessRule userRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.ReadAndExecute, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit | InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                FileSystemAccessRule adminRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit | InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                FileSystemAccessRule systemRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit | InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                FileSystemAccessRule authRight = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit | InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
                directorySecurity.AddAccessRule(userRights);
                directorySecurity.AddAccessRule(adminRights);
                directorySecurity.AddAccessRule(systemRights);
                directorySecurity.AddAccessRule(authRight);
                directoryInfo.SetAccessControl(directorySecurity);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error to unlock directory: {ex.Message}", Logger.error);
            }
        }
        void UnlockFile(string filePath)
        {
            try
            {
                FileInfo directoryInfo = new FileInfo(filePath);
                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(WindowsIdentity.GetCurrent().User);
                File.SetAccessControl(filePath, fileSecurity);
                fileSecurity = File.GetAccessControl(filePath);
                AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if (rule.AccessControlType == AccessControlType.Deny || (rule.FileSystemRights != FileSystemRights.FullControl && !rule.IsInherited))
                    {
                        fileSecurity.RemoveAccessRule(rule);
                    }
                }
                FileSystemAccessRule userRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.FullControl, AccessControlType.Allow);
                FileSystemAccessRule adminRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, AccessControlType.Allow);
                FileSystemAccessRule systemRights = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, AccessControlType.Allow);
                FileSystemAccessRule authRight = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null), FileSystemRights.FullControl, AccessControlType.Allow);
                fileSecurity.AddAccessRule(userRights);
                fileSecurity.AddAccessRule(adminRights);
                fileSecurity.AddAccessRule(systemRights);
                fileSecurity.AddAccessRule(authRight);
                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (Exception ex)
            {
#if DEBUG
                Logger.WriteLog($"\t[x] Error to unlock file: {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                Logger.WriteLog($"\t[x] Error to unlock file: {ex.Message}", Logger.error);
#endif
            }
        }

        string GetHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(8);
        }

        string GetFilePathFromRegistry(RegistryKey key, string keyName)
        {
            try
            {
                string value;

                if (key != null)
                {
                    object keyValue = key.GetValue(keyName);
                    if (keyValue != null)
                    {
                        value = keyValue.ToString();

                        if (value == "")
                        {
                            return "";
                        }

                        if (value.StartsWith("\"") && value.EndsWith("\"") || value.StartsWith("\"%") || value.StartsWith("%"))
                        {
                            value = ResolveEnvironmentVariables(value.Replace("\"", ""));
                        }

                        if (value.Contains(":\\"))
                        {
                            int index = value.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                            if (index > 0)
                            {
                                string executablePath = value.Substring(0, index + 4);
                                return executablePath.Replace("\"", "");
                            }
                        }
                        else if (!value.Contains(":\\") && value.ToLower().EndsWith(".exe"))
                        {
                            if (File.Exists(Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", value)))
                            {
                                return Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", value);
                            }
                            else if (File.Exists(Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SysWOW64", value)))
                            {
                                return Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SysWOW64", value);
                            }
                        }
                        return value;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
#if DEBUG
                Logger.WriteLog($"\t[x] Error GetFilePathFromRegistry: {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                Logger.WriteLog($"\t[x] Error GetFilePathFromRegistry: {ex.Message}", Logger.error);
#endif
                return "";
            }



        }
        string ResolveEnvironmentVariables(string path)
        {
            char separator = Path.DirectorySeparatorChar;

            string[] parts = path.Split(separator);
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (part.StartsWith("%") && part.EndsWith("%"))
                {
                    string variableName = part.Substring(1, part.Length - 2);
                    string variableValue = Environment.GetEnvironmentVariable(variableName);

                    if (variableValue != null)
                    {
                        parts[i] = variableValue;
                    }
                }
            }

            return string.Join(separator.ToString(), parts);
        }

        private static void SuspendProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                ProcessThreadCollection Threads = process.Threads;
                int totalThreads = Threads.Count;

                foreach (ProcessThread pT in Threads)
                {

                    IntPtr pOpenThread = WinApi.OpenThread(WinApi.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        continue;
                    }

                    WinApi.SuspendThread(pOpenThread);
                    WinApi.CloseHandle(pOpenThread);
                }

                foreach (ProcessThread pT in Threads)
                {
                    if (pT.ThreadState == ThreadState.Wait)
                    {
                        if (pT.WaitReason != ThreadWaitReason.Executive)
                            totalThreads -= 1;
                    }
                }


                if (totalThreads == 0)
                {
                    Logger.WriteLog($"\t[+] Process {process.ProcessName}.exe - PID: {process.Id} has been suspended", Logger.success);
                }
                else if (totalThreads > 0)
                {
                    Logger.WriteLog($"\t[!!] Not all threads are suspended in {process.ProcessName}.exe - PID: {process.Id}", Logger.cautionLow);
                }

            }
            catch (Exception ex)
            {

#if DEBUG
                Logger.WriteLog($"[x] Error to suspend process: {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                Logger.WriteLog($"[x] Error to suspend process: {ex.Message}", Logger.error);
#endif
            }

        }

        private static void UnProtect(int[] pids)
        {
            try
            {
                Process.EnterDebugMode();

                foreach (int pid in pids)
                {
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;
                    WinApi.NtSetInformationProcess(WinApi.OpenProcess(0x001F0FFF, false, pid), BreakOnTermination, ref isCritical, sizeof(int));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Logger.WriteLog($"\t[x] Error for unprotect process: {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                Logger.WriteLog($"\t[x] Error for unprotect process: {ex.Message}", Logger.error);
#endif
            }

        }

        public static int GetParentProcessId(int processId)
        {
            int parentProcessId = 0;
            IntPtr hProcess = WinApi.OpenProcess(WinApi.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

            if (hProcess != IntPtr.Zero)
            {
                WinApi.PROCESS_BASIC_INFORMATION pbi = new WinApi.PROCESS_BASIC_INFORMATION();

                int status = WinApi.NtQueryInformationProcess(hProcess, 0, ref pbi, Marshal.SizeOf(pbi), out int returnLength);

                if (status == WinApi.STATUS_SUCCESS)
                {
                    parentProcessId = pbi.InheritedFromUniqueProcessId.ToInt32();
                }

                WinApi.CloseHandle(hProcess);
            }

            return parentProcessId;
        }

        private List<Process> GetProcesses()
        {
            List<Process> procs = new List<Process>();
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    ProcessModule t = p.Modules[0];
                }
                catch (Exception)
                {
                    continue;
                }
                procs.Add(p);
            }
            return procs;
        }

        string Sizer(long CountBytes)
        {
            string[] type = { "B", "KB", "MB", "GB" };
            if (CountBytes == 0)
                return $"0 {type[0]}";

            double bytes = Math.Abs(CountBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(CountBytes) * num} {type[place]}";
        }

        #region " TCP Connections "

        private List<Connection> GetConnections()
        {
            List<Connection> Connections = new List<Connection>();

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo
                    {
                        Arguments = "-a -n -o",
                        FileName = "netstat.exe",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        Logger.WriteLog("\t[x] Failed to read TCP connections.", Logger.error);
                        return null;
                    }

                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        if (String.IsNullOrEmpty(row))
                            continue;

                        if (row.Contains("0.0.0.0") || row.Contains("127.0.0.1") || row.Contains("[::") || row.Contains("::"))
                            continue;
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && tokens[1].Equals("TCP"))
                        {
                            string t = tokens[3].Split(':')[1];
                            int remotePort = Int32.Parse(t);
                            Connections.Add(new Connection()
                            {
                                ProcessId = Int32.Parse(tokens[5]),
                                RemotePort = remotePort,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Logger.WriteLog($"\t[x] Error for GetConnection: {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                Logger.WriteLog($"\t[x] Error for GetConnection: {ex.Message}", Logger.error);
#endif
            }
            return Connections;
        }

        public class Connection
        {
            public int RemotePort { get; set; }
            public int ProcessId { get; set; }

            public override string ToString()
            {
                return "TCP Connection - Process ID: " + ProcessId + ", Port: " + RemotePort;
            }
        }
        #endregion

        #region " Commandline Args "

        private string GetCommandLine(Process process)
        {
            string cmdLine = null;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            {
                ManagementObjectCollection.ManagementObjectEnumerator matchEnum = searcher.Get().GetEnumerator();
                if (matchEnum.MoveNext())
                {
                    cmdLine = matchEnum.Current["CommandLine"]?.ToString();
                }
            }
            return cmdLine;
        }

        #endregion
    }
}
