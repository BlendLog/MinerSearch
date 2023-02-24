using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.Win32;

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

        readonly string[] SuspiciousProgramData_path = new[]
        {
            @"C:\ProgramData\RealtekHD",
            @"C:\ProgramData\ReaItekHD",
            @"C:\ProgramData\WindowsTask",
            @"C:\ProgramData\Windows Tasks Service",
            @"C:\ProgramData\Setup",
            @"C:\ProgramData\RunDLL",
            @"C:\ProgramData\System32",
            @"C:\ProgramData\MB3Install",
            @"C:\ProgramData\Malwarebytes",
            @"C:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64",
            @"C:\ProgramData\360safe",
            @"C:\ProgramData\Norton",
            @"C:\ProgramData\AVAST Software",
            @"C:\ProgramData\Kaspersky Lab Setup Files",
            @"C:\ProgramData\Kaspersky Lab",
            @"C:\ProgramData\Doctor Web",
            @"C:\ProgramData\Install",
            @"C:\ProgramData\grizzly",
            @"C:\ProgramData\McAfee",
            @"C:\ProgramData\Avira",
            @"C:\ProgramData\Evernote",
            @"C:\ProgramData\WavePad",
            @"C:\ProgramData\RobotDemo",
            @"C:\ProgramData\PuzzleMedia",
            @"C:\ProgramData\ESET",
            @"C:\ProgramData\FingerPrint",
            @"C:\ProgramData\BookManager",
            @"C:\ProgramData\versionApp"
        };

        string[] SuspiciousProgramFilesX86_path = new[]
        {
            @"C:\Program Files (x86)\RDP Wrapper",
            @"C:\Program Files (x86)\Microsoft JDX",
            @"C:\Program Files (x86)\360",
            @"C:\Program Files (x86)\SpyHunter",
            @"C:\Program Files (x86)\AVAST Software",
            @"C:\Program Files (x86)\AVG",
            @"C:\Program Files (x86)\Kaspersky Lab",
            @"C:\Program Files (x86)\Cezurity",
            @"C:\Program Files (x86)\GRIZZLY Antivirus",
            @"C:\Program Files (x86)\Panda Security",
            @"C:\Program Files (x86)\IObit",
            @"C:\Program Files (x86)\IObit\Advanced SystemCare",
            @"C:\Program Files (x86)\IObit\IObit Malware Fighter",
            @"C:\Program Files (x86)\Transmission"

        };

        string[] SuspiciousProgramFiles_path = new[]
        {
            @"C:\Program Files\RDP Wrapper",
            @"C:\Program Files\Internet Explorer",
            @"C:\Program Files\ByteFence",
            @"C:\Program Files\Malwarebytes",
            @"C:\Program Files\COMODO",
            @"C:\Program Files\Enigma Software Group",
            @"C:\Program Files\SpyHunter",
            @"C:\Program Files\AVAST Software",
            @"C:\Program Files\AVG",
            @"C:\Program Files\Kaspersky Lab",
            @"C:\Program Files\Bitdefender Agent",
            @"C:\Program Files\DrWeb",
            @"C:\Program Files\Common Files\Doctor Web",
            @"C:\Program Files\Cezurity",
            @"C:\Program Files\Common Files\McAfee",
            @"C:\Program Files\Rainmeter",
            @"C:\Program Files\Loaris Trojan Remover",
            @"C:\Program Files\ESET",
            @"C:\Program Files\Process Lasso",
            @"C:\Program Files\Ravantivirus"
        };

        readonly string[] SuspiciousRoot_path = new[]
        {
            @"C:\AdwCleaner",
            @"C:\KVRT_Data",
            @"C:\KVRT2020_Data",
            @"C:\FRST"
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
            "vbc"

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
            3235192 //vbc
        };

        public List<int> malware_pids = new List<int>();
        public List<string> malware_dirs = new List<string>();
        public bool CleanupHosts = false;
        public int suspiciousObj_count = 0;

        public void Scan()
        {
            string processName = "";
            string fullPath = "";
            string args = "";
            long fileSize = 0;
            double riskLevel = 0;
            Stopwatch startTime = Stopwatch.StartNew();
            List<Connection> cons = new List<Connection>();
            List<Process> proc = GetProcesses();

            foreach (Process p in proc)
            {
                if (!p.HasExited)
                {
                    processName = p.ProcessName.ToLower();
                    Logger.Log($"Scanning: {processName}.exe");
                }
                else continue;
                riskLevel = 0;

                fullPath = $@"""{p.MainModule.FileName}""".ToLower();
                fileSize = new FileInfo(p.MainModule.FileName).Length;


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

                            Logger.LogWarn($"\tSuspicious path: {fullPath}");
                            riskLevel += 2.5;
                        }

                        if (fileSize >= constantFileSize[i] * 2)
                        {
                            Logger.LogWarn($"\tSuspicious file size: {Sizer(fileSize)}");
                            riskLevel += 0.5;
                        }

                    }

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
                    Logger.LogWarn("\tToo much GPU libs usage: " + p.ProcessName + ".exe, Process ID: " + p.Id);
                    riskLevel += 0.5;

                }

                cons = GetConnections();
                IEnumerable<Connection> tiedConnections = cons.Where(x => x.ProcessId == p.Id);
                IEnumerable<Connection> badPorts = tiedConnections.Where(x => _PortList.Any(y => y == x.RemotePort));
                foreach (Connection conn in badPorts)
                {
                    Logger.LogWarn("\t" + conn);
                    if (processName == "notepad")
                    {
                        riskLevel += 1.5;
                    }
                    riskLevel += 1;
                }


                args = GetCommandLine(p);
                if (args != null)
                {
                    foreach (int port in _PortList)
                    {
                        bool portActive = badPorts.Any(x => x.RemotePort == port);
                        if (portActive && args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.LogWarn($"\tActive blacklisted port {port} in CMD ARGS");
                        }
                        else if (args.Contains(port.ToString()))
                        {
                            riskLevel += 0.5;
                            Logger.LogWarn($"\tBlacklisted port {port} in CMD ARGS");
                        }
                    }
                    if (args.Contains("stratum"))
                    {
                        riskLevel += 2.5;
                        Logger.LogWarn("\tPresent \"stratum\" in cmd args.");
                    }
                    if (args.Contains("nanopool") || args.Contains("pool."))
                    {
                        riskLevel += 2.5;
                        Logger.LogWarn("\tPresent \"pool\" in cmd args.");
                    }

                    if (args.ToLower().Contains("-systemcheck"))
                    {
                        riskLevel += 2;
                        Logger.LogWarn("\tProbably fake system task");
                        if (fullPath.Contains("appdata") && fullPath.Contains("windows"))
                        {

                            riskLevel += 0.5;
                            suspiciousFiles_path.Add(fullPath);
                        }

                    }
                    if (processName == "helper" || processName == "helper32" || processName == "helper64")
                    {
                        riskLevel += 0.5;
                    }

                    if (processName == SystemFileNames[3] && !args.ToLower().Contains("\\??\\c:\\"))
                    {
                        Logger.LogWarn($"\tProbably watchdog process. Process ID: {p.Id}");
                        riskLevel += 2.5;
                    }
                    if (processName == SystemFileNames[4] && !args.ToLower().Contains($"{SystemFileNames[4]}.exe -k"))
                    {
                        Logger.LogWarn($"\tProbably process injection. Process ID: {p.Id}");
                        riskLevel += 2.5;
                    }
                    if (processName == SystemFileNames[5] && args.ToLower() != "\"dwm.exe\"")
                    {
                        Logger.LogWarn($"\tProbably process injection. Process ID: {p.Id}");
                        riskLevel += 2;
                    }
                }

                if (processName == "rundll" || processName == "system" || processName == "winserv")
                {
                    Logger.LogWarn($"\tProbably RAT process: {fullPath} Process ID: {p.Id}");
                    suspiciousFiles_path.Add(fullPath);
                    riskLevel += 3;
                }

                if (p.MainModule.FileVersionInfo.FileDescription != null)
                {
                    if (p.MainModule.FileVersionInfo.FileDescription.ToLower().Contains("svhost"))
                    {
                        Logger.LogWarn($"\tProbably RAT process: {fullPath} Process ID: {p.Id}");
                        suspiciousFiles_path.Add(fullPath);
                        riskLevel += 3;
                    }
                }
                if (riskLevel >= 2.5)
                {
                    Logger.LogCaution("\tMalicious process found! Risk level: " + riskLevel);
                    try
                    {
                        SuspendProcess(p.Id);
                        Logger.LogSuccess($"\tProcess {p.ProcessName}.exe - PID: {p.Id} has been suspended successfully");
                        malware_pids.Add(p.Id);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError("\tFailed to suspend process: " + fullPath + $"{e}\n");
                    }
                }

            }
            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Console.WriteLine($"\nScan elapsed time: {elapsedTime}");
        }
        public void StaticScan()
        {

            Logger.LogHeader("Scanning directories...");
            string[] homedrive = Directory.GetDirectories(@"C:\", "*", SearchOption.TopDirectoryOnly);
            ScanDirectories(homedrive, SuspiciousRoot_path);

            string[] ProgramFiles = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "*", SearchOption.TopDirectoryOnly);
            ScanDirectories(ProgramFiles, SuspiciousProgramFiles_path);

            string[] ProgramFilesX86 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "*", SearchOption.TopDirectoryOnly);
            ScanDirectories(ProgramFilesX86, SuspiciousProgramFilesX86_path);

            string[] programdata = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "*", SearchOption.TopDirectoryOnly);
            ScanDirectories(programdata, SuspiciousProgramData_path);


            foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "*.exe", SearchOption.AllDirectories))
            {
                suspiciousFiles_path.Add(Path.GetFullPath(file));
            }

            foreach (string path in suspiciousFiles_path)
            {
                Logger.LogWarn($"\tSuspicious file {path}");
            }

            Logger.LogHeader("Scanning registry...");
            ScanRegistry();
            Logger.LogHeader("Scanning hosts file...");
            ScanHosts();
            suspiciousObj_count += malware_dirs.Count + suspiciousFiles_path.Count;


        }
        public void Clean()
        {

            if (malware_pids.Count != 0)
            {
                Logger.LogHeader("Trying to close processes...");

                UnProtect(malware_pids.ToArray());
                foreach (var id in malware_pids)
                {
                    Process p = Process.GetProcessById(id);
                    try
                    {
                        if (!p.HasExited)
                        {
                            string pname = p.ProcessName;
                            int pid = p.Id;
                            p.Kill();

                            if (p.HasExited)
                                Logger.LogSuccess($"Malicious process {pname} - pid:{pid} successfully closed");
                        }
                        else
                        {
                            Logger.LogCaution($"Malicious process has exited");
                            continue;
                        }

                    }
                    catch (Exception)
                    {
                        Logger.LogError($"Failed to kill malicious process! ProcessID: {p.Id}");
                        throw;
                    }
                }
            }

            if (suspiciousFiles_path.Count > 0)
            {
                Logger.LogHeader("Removing malicious files...");

                foreach (string path in suspiciousFiles_path)
                {
                    if (File.Exists(path))
                    {
                        DeleteLockedFile(path);
                        if (!File.Exists(path))
                        {
                            Logger.LogSuccess($"Malicious file {path} deleted");
                        }
                        else Logger.LogError($"Failed to delete {path}");
                    }
                }
            }

            if (malware_dirs.Count != 0)
            {
                Logger.LogHeader("Removing malicious dirs...");

                foreach (string str in malware_dirs)
                {
                    TakeownDelete(str);
                    if (!Directory.Exists(str))
                    {
                        Logger.LogSuccess($"Directory {str} successfull deleted");
                    }
                    else
                    {
                        Logger.LogError($"Failed to delete directory: {str}");
                        Logger.LogCaution($"The directory may be occupied by a malicious process");
                    }
                }
            }
        }

        void ScanDirectories(string[] dirsArray, string[] constDirsArray)
        {

            for (int j = 0; j < constDirsArray.Length; j++)
            {
                string dir = constDirsArray[j].ToLower();
                for (int i = 0; i < dirsArray.Length; i++)
                {
                    string scanDir = dirsArray[i].ToLower();
                    if ($"\"{scanDir}\"" == $"\"{dir}\"")
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(scanDir);
                        if (dinfo.Attributes == (FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System) || dinfo.Attributes == (FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System | FileAttributes.NotContentIndexed))
                        {
                            Logger.LogWarn($"Suspicious hidden directory: \"{scanDir}\"");
                            malware_dirs.Add(scanDir);
                        }
                    }
                }
            }
        }
        void ScanHosts()
        {
            RegistryKey hostsDir = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters");
            int hostsRiskLevel = 0;
            if (hostsDir != null)
            {
                string hostsPath = hostsDir.GetValue("DataBasePath").ToString();
                FileInfo hostsinfo = new FileInfo(hostsPath + "\\hosts");
                if (hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System | FileAttributes.NotContentIndexed) || hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System) || hostsinfo.Attributes == (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive))
                {
                    Logger.LogWarn($"\tHas \"hidden\" attribute");
                    hostsRiskLevel += 2;
                }
                if (hostsinfo.Length > 1024)
                {
                    Logger.LogWarn($"\tSuspicious file size: {Sizer(hostsinfo.Length)}");
                    hostsRiskLevel++;
                }
                if (hostsRiskLevel >= 1)
                {

                    Logger.LogCaution($"\tInfected hosts file. Risk level {hostsRiskLevel}");
                    string answer;
                answerLabel:
                    Console.WriteLine("\nThe hosts file will be completely overwritten. Continue? [enter \"yes\" to confirm | \"no\" - skip cleaning]:");
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "yes":
                            suspiciousObj_count++;
                            CleanupHosts = true;
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "cmd",
                                Arguments = $@"/c attrib -s -h ""{hostsPath + "\\hosts"}"" && ren ""{hostsPath + "\\hosts"}"" hosts.infected && echo #MinerSearch by BlendLog > ""{hostsPath + "\\hosts"}"" && exit",
                                UseShellExecute = false,
                                CreateNoWindow = true
                            }).WaitForExit();

                            hostsinfo = new FileInfo(hostsPath + "\\hosts");
                            if (hostsinfo.Length <= 1024)
                            {
                                if (hostsRiskLevel >= 1)
                                {
                                    Logger.LogSuccess("The hosts file has been successfully cleaned");
                                }
                            }
                            else Logger.LogError("Failed to clear hosts file");
                            break;
                        case "no":
                            break;
                        default:
                            goto answerLabel;
                    }

                }
            }
        }
        void ScanRegistry()
        {
            RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            if (DisallowRunKey != null)
            {
                if (DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                {
                    Logger.LogWarn("Suspicious registry key: DisallowRun - restricts the launch of the specified applications");
                    suspiciousObj_count++;
                    DisallowRunKey.DeleteValue("DisallowRun");
                    if (!DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                    {
                        Logger.LogSuccess("DisallowRun key successfully deleted");
                    }
                }
                RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", true);
                if (DisallowRunSub != null)
                {
                    DisallowRunKey.DeleteSubKeyTree("DisallowRun");
                    DisallowRunSub = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", true);
                    if (DisallowRunSub == null)
                    {
                        Logger.LogSuccess("DisallowRun hive successfully deleted");
                    }
                }
            }

            RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (AutorunKey != null)
            {
                List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                foreach (string value in RunKeys)
                {
                    if (AutorunKey.GetValue(value).ToString().ToLower() == @"c:\programdata\realtekhd\taskhostw.exe" || AutorunKey.GetValue(value).ToString().ToLower() == @"c:\programdata\reaitekhd\taskhostw.exe")
                    {
                        Logger.LogWarn($"Suspicious registry key {value}");
                        suspiciousObj_count++;
                        AutorunKey.DeleteValue(value);
                        if (AutorunKey.GetValue(value) == null)
                        {
                            Logger.LogSuccess($"Successfull deleted registry key {value}");
                        }
                    }
                }
            }
        }
        void TakeownDelete(string dir)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/c takeown /f ""{dir}"" && icacls ""{dir}"" /remove:d *S-1-5-18 && icacls ""{dir}"" /remove:d %username% && rd /s /q ""{dir}"" && exit",
                UseShellExecute = false,
                CreateNoWindow = true
            }).WaitForExit();
        }
        void DeleteLockedFile(string filePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/c takeown /f ""{filePath}"" && icacls ""{filePath}"" /remove:d *S-1-5-18 && icacls ""{filePath}"" /remove:d %username% && del ""{filePath}"" && exit",
                UseShellExecute = false,
                CreateNoWindow = true
            }).WaitForExit();
        }

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);


        private static void SuspendProcess(int pid)
        {
            Process process = Process.GetProcessById(pid); // throws exception if process does not exist

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        private static void UnProtect(int[] pids)
        {
            try
            {
                Process.EnterDebugMode();

                foreach (int pid in pids)
                {
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;
                    NtSetInformationProcess(OpenProcess(0x001F0FFF, false, pid), BreakOnTermination, ref isCritical, sizeof(int));
                }
            }
            catch (Exception) { throw; }
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
                        Logger.LogError("Failed to read TCP connections.");
                        return null;
                    }

                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        if (String.IsNullOrEmpty(row)) continue;

                        if (row.Contains("0.0.0.0") || row.Contains("127.0.0.1") || row.Contains("[::")) continue;
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
                Logger.LogError(ex.Message);
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
