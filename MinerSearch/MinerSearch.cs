using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
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
            double riskLevel = 0;
            long fileSize = 0;
            object lockObject = new object();
            Stopwatch startTime = Stopwatch.StartNew();
            List<Connection> cons = new List<Connection>();
            List<Process> proc = GetProcesses();
            foreach (Process p in proc)
            {
                if (!p.HasExited)
                {
                    processName = p.ProcessName.ToLower();
                    Logger.WriteLog($"Scanning: {processName}.exe", ConsoleColor.White);
                }
                else continue;

                riskLevel = 0;
                if (WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Success && WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.FileNotSigned)
                {
                    Logger.WriteLog($"\t[!!!] Don't have valid signature in file: {p.MainModule.FileName}", Logger.caution);
                    riskLevel += 0.5;
                }

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

                            Logger.WriteLog($"\t[!] Suspicious path: {fullPath}", Logger.warn);
                            riskLevel += 2.5;
                        }

                        if (fileSize >= constantFileSize[i] * 2)
                        {
                            Logger.WriteLog($"\t[!] Suspicious file size: {Sizer(fileSize)}", Logger.warn);
                            riskLevel += 0.5;
                        }

                    }

                }

                if (processName.Contains("helper"))
                {
                    riskLevel += 0.5;
                }

                if (processName == "rundll" || processName == "system" || processName == "winserv")
                {
                    Logger.WriteLog($"\t[!] Probably RAT process: {fullPath} Process ID: {p.Id}", Logger.warn);
                    suspiciousFiles_path.Add(fullPath);
                    riskLevel += 3;
                }


                if (p.MainModule.FileVersionInfo.FileDescription != null)
                {
                    if (p.MainModule.FileVersionInfo.FileDescription.ToLower().Contains("svhost"))
                    {
                        Logger.WriteLog($"\t[!] Probably RAT process: {fullPath} Process ID: {p.Id}", Logger.warn);
                        suspiciousFiles_path.Add(fullPath);
                        riskLevel += 3;
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
                    Logger.WriteLog("\t[!] Too much GPU libs usage: " + p.ProcessName + ".exe, Process ID: " + p.Id, Logger.warn);
                    riskLevel += 0.5;

                }

                cons = GetConnections();
                IEnumerable<Connection> tiedConnections = cons.Where(x => x.ProcessId == p.Id);
                IEnumerable<Connection> badPorts = tiedConnections.Where(x => _PortList.Any(y => y == x.RemotePort));
                foreach (Connection conn in badPorts)
                {
                    Logger.WriteLog("\t[!] " + conn, Logger.warn);
                    if (processName == "notepad")
                    {
                        riskLevel += 1.5;
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
                            Logger.WriteLog($"\t[!] Active blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                        else if (args.Contains(port.ToString()))
                        {
                            riskLevel += 0.5;
                            Logger.WriteLog($"\t[!] Blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                    }
                    if (args.Contains("stratum"))
                    {
                        riskLevel += 2.5;
                        Logger.WriteLog("\t[!] Present \"stratum\" in cmd args.", Logger.warn);
                    }
                    if (args.Contains("nanopool") || args.Contains("pool."))
                    {
                        riskLevel += 2.5;
                        Logger.WriteLog("\t[!] Present \"pool\" in cmd args.", Logger.warn);
                    }

                    if (args.Contains("-systemcheck"))
                    {
                        riskLevel += 2;
                        Logger.WriteLog("\t[!] Probably fake system task", Logger.warn);
                        if (fullPath.Contains("appdata") && fullPath.Contains("windows"))
                        {

                            riskLevel += 0.5;
                            suspiciousFiles_path.Add(fullPath);
                        }

                    }

                    if (processName == SystemFileNames[3] && !args.Contains("\\??\\c:\\"))
                    {
                        Logger.WriteLog($"\t[!] Probably watchdog process. Process ID: {p.Id}", Logger.warn);
                        riskLevel += 2.5;
                    }
                    if (processName == SystemFileNames[4] && !args.Contains($"{SystemFileNames[4]}.exe -k"))
                    {
                        Logger.WriteLog($"\t[!] Probably process injection. Process ID: {p.Id}", Logger.warn);
                        riskLevel += 2.5;
                    }
                    if (processName == SystemFileNames[5] && args != "\"dwm.exe\"")
                    {
                        Logger.WriteLog($"\t[!] Probably process injection. Process ID: {p.Id}", Logger.warn);
                        riskLevel += 2;
                    }
                }

                if (riskLevel >= 2.5)
                {
                    Logger.WriteLog("\t[!!!] Malicious process found! Risk level: " + riskLevel, Logger.caution);
                    try
                    {
                        SuspendProcess(p.Id);
                        Logger.WriteLog($"\t[+] Process {p.ProcessName}.exe - PID: {p.Id} has been suspended", Logger.success);
                        malware_pids.Add(p.Id);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLog("\t[x] Failed to suspend process: " + fullPath + $"{e}\n", Logger.error);
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

            Logger.WriteLog("Scanning directories...", Logger.head);
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
                Logger.WriteLog($"\t[!] Suspicious file {path}", Logger.warn);
            }

            Logger.WriteLog("Scanning registry...", Logger.head);
            ScanRegistry();
            Logger.WriteLog("Scanning hosts file...", Logger.head);
            ScanHosts();
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
                    Process p = Process.GetProcessById(id);
                    try
                    {
                        if (!p.HasExited)
                        {
                            string pname = p.ProcessName;
                            int pid = p.Id;
                            p.Kill();

                            if (p.HasExited)
                                Logger.WriteLog($"\t[+] Malicious process {pname} - pid:{pid} successfully closed", Logger.success);
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!!!] Malicious process has exited", Logger.caution);
                            continue;
                        }

                    }
                    catch (Exception)
                    {
                        Logger.WriteLog($"\t[x] Failed to kill malicious process! ProcessID: {p.Id}", Logger.error);
                        throw;
                    }
                }
            }

            if (suspiciousFiles_path.Count > 0)
            {
                Logger.WriteLog("Removing malicious files...", Logger.head);

                foreach (string path in suspiciousFiles_path)
                {
                    if (File.Exists(path))
                    {
                        DeleteLockedFile(path);
                        if (!File.Exists(path))
                        {
                            Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                        }
                        else Logger.WriteLog($"\t[x] Failed to delete {path}", Logger.error);
                    }
                }
            }

            if (malware_dirs.Count != 0)
            {
                Logger.WriteLog("Removing malicious dirs...", Logger.head);

                foreach (string str in malware_dirs)
                {
                    TakeownDelete(str);
                    if (!Directory.Exists(str))
                    {
                        Logger.WriteLog($"\t[+] Directory {str} successfull deleted", Logger.success);
                    }
                    else
                    {
                        Logger.WriteLog($"\t[x] Failed to delete directory: {str}", Logger.error);
                        Logger.WriteLog($"\t[!!!] The directory may be occupied by a malicious process", Logger.caution);
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
                            Logger.WriteLog($"\t[!] Suspicious hidden directory: \"{scanDir}\"", Logger.warn);
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

                    Logger.WriteLog($"\t[!!!] Infected hosts file. Risk level {hostsRiskLevel}", Logger.caution);
                    string answer;
                answerLabel:
                    Console.WriteLine("\nThe hosts file will be completely overwritten. Continue? [enter \"yes\" to confirm | \"no\" - skip cleaning]:");
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "yes":
                            suspiciousObj_count++;
                            CleanupHosts = true;
                            if (File.Exists(hostsPath + "\\hosts.infected"))
                            {
                                File.Delete(hostsPath + "\\hosts.infected");
                            }
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "cmd",
                                Arguments = $@"/c attrib -s -h ""{hostsPath + "\\hosts"}"" && ren ""{hostsPath + "\\hosts"}"" hosts.infected && echo # > ""{hostsPath + "\\hosts"}"" && exit",
                                UseShellExecute = false,
                                CreateNoWindow = true
                            }).WaitForExit();

                            hostsinfo = new FileInfo(hostsPath + "\\hosts");
                            if (hostsinfo.Length <= 1024)
                            {
                                if (hostsRiskLevel >= 1)
                                {
                                    Logger.WriteLog("\t[+] The hosts file has been successfully cleaned", Logger.success);
                                }
                            }
                            else Logger.WriteLog("\t[x] Failed to clear hosts file", Logger.error);
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

            RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (AutorunKey != null)
            {
                List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                foreach (string value in RunKeys)
                {
                    string path = GetRegistryValue(AutorunKey, value);
                    if (File.Exists(path))
                    {
                        if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Success && WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!!!] Don't have valid signature in file: {path} in key {value}", Logger.caution);
                        }
                        else if (WinTrust.VerifyEmbeddedSignature(path) == WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!] File is not signed: {path} in key {value}", Logger.warn);
                        }
                    }
                    else
                    {
                        Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                    }
                }
            }

            #endregion

            #region HKCU

            AutorunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (AutorunKey != null)
            {
                List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                foreach (string value in RunKeys)
                {
                    string path = GetRegistryValue(AutorunKey, value);
                    if (File.Exists(path))
                    {
                        if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Success && WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!!!] Don't have valid signature in file: {path} in key {value}", Logger.caution);
                        }
                        else if (WinTrust.VerifyEmbeddedSignature(path) == WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!] File is not signed: {path} in key {value}", Logger.warn);
                        }
                    }
                    else
                    {
                        Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                    }
                }
            }

            #endregion

            #region WOW6432Node

            AutorunKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", true);
            if (AutorunKey != null)
            {
                List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                foreach (string value in RunKeys)
                {
                    string path = GetRegistryValue(AutorunKey, value);
                    if (File.Exists(path))
                    {
                        if (WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.Success && WinTrust.VerifyEmbeddedSignature(path) != WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!!!] Don't have valid signature in file: {path} in key {value}", Logger.caution);
                        }
                        else if (WinTrust.VerifyEmbeddedSignature(path) == WinVerifyTrustResult.FileNotSigned)
                        {
                            Logger.WriteLog($"\t[!] File is not signed: {path} in key {value}", Logger.warn);
                        }
                    }
                    else
                    {
                        Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                    }
                }
            }


            #endregion
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
        string GetRegistryValue(RegistryKey key, string keyName)
        {
            string value;
            string substring = "";

            if (key != null)
            {
                object keyValue = key.GetValue(keyName);
                if (keyValue != null)
                {
                    value = keyValue.ToString();

                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        return value.Replace("\"", "");
                    }

                    #region Processing strings with quotes, spaces, environment variables, not including arguments
                    int spaceIndex = -1;
                    int slashIndex = -1;
                    //search for slash char
                    for (int i = value.Length - 1; i > 0; i--) //Read to Left
                    {
                        if (value[i] == '\\')
                        {
                            slashIndex = i;
                            break;
                        }
                    }
                    //search for the first space char starting from slash index
                    for (int i = slashIndex; i < value.Length; i++) //Read to Right
                    {
                        if (value[i] == ' ')
                        {
                            spaceIndex = i;
                            break;
                        }
                    }

                    if (spaceIndex == -1)
                    {
                        return value;
                    }

                    //Reading entire string before founded space index
                    StringBuilder sb = new StringBuilder("");
                    for (int i = 0; i < spaceIndex + 1; i++)
                    {
                        sb.Append(value[i]);
                    }

                    //Delete redudant quotes
                    substring = sb.ToString().Replace("\"", "");


                    if (substring.StartsWith("%"))
                    {
                        substring = Environment.GetEnvironmentVariable(substring);
                    }
                    #endregion
                }
            }

            return substring;
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

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

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
                        Logger.WriteLog("\t[x] Failed to read TCP connections.", Logger.error);
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
                Logger.WriteLog("\t[x] " + ex.Message, Logger.error);
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
