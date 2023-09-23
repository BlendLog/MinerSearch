﻿using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;

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

        List<string> SuspiciousLockedPaths = new List<string>
        {
            $@"{Program.drive_letter}:\ProgramData\360safe",
            $@"{Program.drive_letter}:\ProgramData\AVAST Software",
            $@"{Program.drive_letter}:\ProgramData\Avira",
            $@"{Program.drive_letter}:\ProgramData\BookManager",
            $@"{Program.drive_letter}:\ProgramData\Doctor Web",
            $@"{Program.drive_letter}:\ProgramData\ESET",
            $@"{Program.drive_letter}:\ProgramData\Evernote",
            $@"{Program.drive_letter}:\ProgramData\FingerPrint",
            $@"{Program.drive_letter}:\ProgramData\Kaspersky Lab",
            $@"{Program.drive_letter}:\ProgramData\Kaspersky Lab Setup Files",
            $@"{Program.drive_letter}:\ProgramData\MB3Install",
            $@"{Program.drive_letter}:\ProgramData\Malwarebytes",
            $@"{Program.drive_letter}:\ProgramData\McAfee",
            $@"{Program.drive_letter}:\ProgramData\Norton",
            $@"{Program.drive_letter}:\ProgramData\grizzly",
            $@"{Program.drive_letter}:\Program Files (x86)\Microsoft JDX",
            $@"{Program.drive_letter}:\Program Files (x86)\360",
            $@"{Program.drive_letter}:\Program Files (x86)\SpyHunter",
            $@"{Program.drive_letter}:\Program Files (x86)\AVAST Software",
            $@"{Program.drive_letter}:\Program Files (x86)\AVG",
            $@"{Program.drive_letter}:\Program Files (x86)\Kaspersky Lab",
            $@"{Program.drive_letter}:\Program Files (x86)\Cezurity",
            $@"{Program.drive_letter}:\Program Files (x86)\GRIZZLY Antivirus",
            $@"{Program.drive_letter}:\Program Files (x86)\Panda Security",
            $@"{Program.drive_letter}:\Program Files (x86)\IObit\Advanced SystemCare",
            $@"{Program.drive_letter}:\Program Files (x86)\IObit\IObit Malware Fighter",
            $@"{Program.drive_letter}:\Program Files (x86)\IObit",
            $@"{Program.drive_letter}:\Program Files (x86)\Moo0",
            $@"{Program.drive_letter}:\Program Files (x86)\SpeedFan",
            $@"{Program.drive_letter}:\Program Files\AVAST Software",
            $@"{Program.drive_letter}:\Program Files\AVG",
            $@"{Program.drive_letter}:\Program Files\Bitdefender Agent",
            $@"{Program.drive_letter}:\Program Files\ByteFence",
            $@"{Program.drive_letter}:\Program Files\COMODO",
            $@"{Program.drive_letter}:\Program Files\Cezurity",
            $@"{Program.drive_letter}:\Program Files\Common Files\AV",
            $@"{Program.drive_letter}:\Program Files\Common Files\Doctor Web",
            $@"{Program.drive_letter}:\Program Files\Common Files\McAfee",
            $@"{Program.drive_letter}:\Program Files\DrWeb",
            $@"{Program.drive_letter}:\Program Files\ESET",
            $@"{Program.drive_letter}:\Program Files\Enigma Software Group",
            $@"{Program.drive_letter}:\Program Files\EnigmaSoft",
            $@"{Program.drive_letter}:\Program Files\Kaspersky Lab",
            $@"{Program.drive_letter}:\Program Files\Loaris Trojan Remover",
            $@"{Program.drive_letter}:\Program Files\Malwarebytes",
            $@"{Program.drive_letter}:\Program Files\Process Lasso",
            $@"{Program.drive_letter}:\Program Files\Rainmeter",
            $@"{Program.drive_letter}:\Program Files\Ravantivirus",
            $@"{Program.drive_letter}:\Program Files\SpyHunter",
            $@"{Program.drive_letter}:\Program Files\Process Hacker 2",
            $@"{Program.drive_letter}:\Program Files\RogueKiller",
            $@"{Program.drive_letter}:\Program Files\SUPERAntiSpyware",
            $@"{Program.drive_letter}:\Program Files\HitmanPro",
            $@"{Program.drive_letter}:\Program Files\RDP Wrapper",
            $@"{Program.drive_letter}:\AdwCleaner",
            $@"{Program.drive_letter}:\KVRT_Data",
            $@"{Program.drive_letter}:\KVRT2020_Data",
            $@"{Program.drive_letter}:\FRST"
        };

        List<string> malware_paths = new List<string>
        {
            $@"{Program.drive_letter}:\ProgramData\Install",
            $@"{Program.drive_letter}:\ProgramData\Microsoft\Check",
            $@"{Program.drive_letter}:\ProgramData\Microsoft\Intel",
            $@"{Program.drive_letter}:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64",
            $@"{Program.drive_letter}:\ProgramData\Microsoft\temp",
            $@"{Program.drive_letter}:\ProgramData\PuzzleMedia",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD",
            $@"{Program.drive_letter}:\ProgramData\RobotDemo",
            $@"{Program.drive_letter}:\ProgramData\RunDLL",
            $@"{Program.drive_letter}:\ProgramData\Setup",
            $@"{Program.drive_letter}:\ProgramData\System32",
            $@"{Program.drive_letter}:\ProgramData\WavePad",
            $@"{Program.drive_letter}:\ProgramData\Windows Tasks Service",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask",
            $@"{Program.drive_letter}:\Program Files\Transmission",
            $@"{Program.drive_letter}:\Program Files\Google\Libs",
            $@"{Program.drive_letter}:\Program Files (x86)\Transmission",
            $@"{Program.drive_letter}:\Windows\Fonts\Mysql",
            $@"{Program.drive_letter}:\Program Files\Internet Explorer\bin",
            $@"{Program.drive_letter}:\ProgramData\princeton-produce",
            $@"{Program.drive_letter}:\ProgramData\Timeupper"
        };

        List<string> WD_exclusion_paths = new List<string>()
        {
            $@"{Program.drive_letter}:\Program Files\RDP Wrapper",
            $@"{Program.drive_letter}:\ProgramData",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\Windows Tasks Service\winserv.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AMD.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AppModule.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\audiodg.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\MicrosoftHost.exe",
            $@"{Program.drive_letter}:\Windows\System32",
            $@"{Program.drive_letter}:\Windows\SysWOW64\unsecapp.exe"
        };

        List<string> WD_exclusion_processes = new List<string>()
        {
            $@"{Program.drive_letter}:\ProgramData\RDPWinst.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\Windows Tasks Service\winserv.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AMD.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AppModule.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\audiodg.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\MicrosoftHost.exe",
            $@"{Program.drive_letter}:\Windows\SysWOW64\unsecapp.exe"
        };

        List<string> suspiciousFiles_path = new List<string>();

        List<string> known_malware_files = new List<string>()
        {
            $@"{Program.drive_letter}:\ProgramData\Microsoft\win.exe",
            $@"{Program.drive_letter}:\Program Files\Google\Chrome\updater.exe",
            $@"{Program.drive_letter}:\ProgramData\RDPWinst.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD\taskhost.exe",
            $@"{Program.drive_letter}:\ProgramData\RealtekHD\taskhostw.exe",
            $@"{Program.drive_letter}:\ProgramData\Windows Tasks Service\winserv.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AMD.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\AppModule.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\audiodg.exe",
            $@"{Program.drive_letter}:\ProgramData\WindowsTask\MicrosoftHost.exe",
            $@"{Program.drive_letter}:\Windows\SysWOW64\unsecapp.exe",
            $@"{Program.drive_letter}:\ProgramData\Timeupper\HVPIO.exe"
        };

        List<string> previousMalwareFilePath = new List<string>();
        
        List<byte[]> signatures = new List<byte[]>
                {
                    new byte[] {0x67, 0x33, 0x71, 0x70, 0x70, 0x6D },
                    new byte[] {0x33, 0x6E, 0x6A, 0x6F, 0x66, 0x73, 0x74 },
                    new byte[] {0x6F, 0x6A, 0x64, 0x66, 0x69, 0x62, 0x74, 0x69 },
                    new byte[] {0x75, 0x66, 0x6C, 0x75, 0x70, 0x6F, 0x6A, 0x75 },
                    new byte[] {0x2F, 0x75, 0x69, 0x66, 0x6E, 0x6A, 0x65, 0x62 },
                    new byte[] {0x74, 0x75, 0x73, 0x62, 0x75, 0x76, 0x6E, 0x2C },
                    new byte[] {0x60, 0x73, 0x62, 0x6F, 0x65, 0x70, 0x6E, 0x79, 0x60 },
                    new byte[] {0x46, 0x75, 0x66, 0x73, 0x6F, 0x62, 0x6D, 0x63, 0x6D, 0x76, 0x66 },
                    new byte[] {0x67, 0x6D, 0x7A, 0x71, 0x70, 0x70, 0x6D, 0x2F, 0x70, 0x73, 0x68 },
                    new byte[] {0x6F, 0x62, 0x6F, 0x70, 0x71, 0x70, 0x70, 0x6D, 0x2F, 0x70, 0x73, 0x68 },
                    new byte[] {0x54, 0x69, 0x66, 0x6D, 0x6D, 0x64, 0x70, 0x65, 0x66, 0x47, 0x6A, 0x6D, 0x66 },
                    new byte[] {0x42, 0x6D, 0x68, 0x70, 0x73, 0x6A, 0x75, 0x69, 0x6E, 0x41, 0x79, 0x6E, 0x73, 0x6A, 0x68 },
                    new byte[] {0x45, 0x70, 0x76, 0x63, 0x6D, 0x66, 0x51, 0x76, 0x6D, 0x74, 0x62, 0x73, 0x51, 0x73, 0x66, 0x74, 0x66, 0x6F, 0x75 }
                };

        public List<string> founded_malware_files = new List<string>();

        public List<string> paths_to_scan = new List<string>()
        {
            $@"{Program.drive_letter}:\ProgramData",
            $@"{Program.drive_letter}:\Program Files",
            $@"{Program.drive_letter}:\Program Files (x86)",
            $@"{Program.drive_letter}:\Windows"
        };

        string[] SystemFileNames = new[]
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
            "unsecapp",
            "ngen",
            "dialer"
        };

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
            40960 //dialer
        };
        long maxFileSize = 100 * 1024 * 1024;

        public List<int> malware_pids = new List<int>();
        public List<string> founded_suspiciousLockedPaths = new List<string>();
        public List<string> founded_malwarePaths = new List<string>();
        public bool RatProcessExists = false;
        public string WindowsVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductName").ToString();
        string quarantineFolder = Path.Combine(Environment.CurrentDirectory, "minersearch_quarantine");

        public void Scan()
        {

            string processName = "";
            string fullPath = "";
            string args = "";
            int riskLevel = 0;
            long fileSize = 0;
            bool isValidProcess;
            Stopwatch startTime = Stopwatch.StartNew();
            List<utils.Connection> cons = new List<utils.Connection>();
            List<Process> procs = utils.GetProcesses();

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


                if (WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Success)
                {
                    riskLevel += 1;
                    isValidProcess = false;
                }
                else
                {
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
                            riskLevel += 2;
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

                cons = utils.GetConnections();

                IEnumerable<utils.Connection> tiedConnections = cons.Where(x => x.ProcessId == p.Id);
                IEnumerable<utils.Connection> badPorts = tiedConnections.Where(x => _PortList.Any(y => y == x.RemotePort));
                foreach (utils.Connection conn in badPorts)
                {
                    Logger.WriteLog("\t[!] " + conn, Logger.warn);
                    if (processName == "notepad")
                    {
                        riskLevel += 2;
                    }
                    riskLevel += 1;
                }

                try
                {
                    args = utils.GetCommandLine(p).ToLower();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"[x] Error get cmd args on {p.MainModule.FileName}\n{ex.Message}", Logger.error);
                    args = null;
                }
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
                        riskLevel += 2;
                    }
                    if (processName == SystemFileNames[4] && !args.Contains($"{SystemFileNames[4]}.exe -k"))
                    {
                        Logger.WriteLog($"\t[!!!] Process injection. Process ID: {p.Id}", Logger.caution);
                        riskLevel += 3;
                    }
                    if (processName == SystemFileNames[5])
                    {
                        int argsLen = args.Length;
                        bool isFakeDwm = false;


                        if ((WindowsVersion.ToLower().Contains("windows 7") && argsLen > 29) || (WindowsVersion.Contains("8 ") && argsLen > 10) || !WindowsVersion.ToLower().Contains("windows 7") && !WindowsVersion.Contains("8 ") && args.Length > 9)
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
                            riskLevel += 2;
                        }


                        if (fileSize >= constantFileSize[i] * 3 && !isValidProcess)
                        {
                            Logger.WriteLog($"\t[!] Suspicious file size: {utils.Sizer(fileSize)}", Logger.warn);
                            riskLevel += 1;
                        }

                    }

                }

                if (processName == "unsecapp" && !p.MainModule.FileName.ToLower().Contains(@"c:\windows\system32\wbem"))
                {
                    Logger.WriteLog($"\t[!!] Watchdog process. Process ID: {p.Id}", Logger.cautionLow);
                    isSuspiciousPath = true;
                    riskLevel += 3;
                }

                if (processName == "rundll" || processName == "system" || processName == "winserv")
                {
                    Logger.WriteLog($"\t[!!] RAT process: {fullPath} Process ID: {p.Id}", Logger.caution);
                    isSuspiciousPath = true;
                    RatProcessExists = true;
                    riskLevel += 3;
                }

                if (processName == "explorer")
                {
                    int ParentProcessId = utils.GetParentProcessId(p.Id);
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

                    utils.SuspendProcess(p.Id);

                    if (isSuspiciousPath)
                    {
                        try
                        {
                            string rnd = utils.GetRndString();
                            string NewFilePath = Path.Combine(Path.GetDirectoryName(p.MainModule.FileName), $"{Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe");
                            File.Move(p.MainModule.FileName, NewFilePath); //Rename malicious file
                            Logger.WriteLog($"\t[+] File renamed to {Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe", Logger.success);
                            suspiciousFiles_path.Add(NewFilePath);
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLog($"\t[x] Cannot rename file: {e.Message}", Logger.error);
                        }
                    }

                    malware_pids.Add(p.Id);
                }
                p.Close();

            }

            startTime.Stop();
            TimeSpan resultTime = startTime.Elapsed;
            string elapsedTime = $"{resultTime.Hours:00}:{resultTime.Minutes:00}:{resultTime.Seconds:00}.{resultTime.Milliseconds:000}";
            Console.WriteLine($"\nScan elapsed time: {elapsedTime}");


        }
        public void StaticScan()
        {
            if (!Program.WinPEMode)
            {
                SuspiciousLockedPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "autologger"));
                SuspiciousLockedPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av_block_remover"));
                SuspiciousLockedPaths.Add(Path.Combine(utils.GetDownloadsPath(), "autologger"));
                SuspiciousLockedPaths.Add(Path.Combine(utils.GetDownloadsPath(), "av_block_remover"));
            }


            Logger.WriteLog("\t\tScanning directories...", Logger.head, false);
            ScanDirectories(SuspiciousLockedPaths, founded_suspiciousLockedPaths);
            if (founded_suspiciousLockedPaths.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
            ScanDirectories(malware_paths, founded_malwarePaths);
            if (founded_malwarePaths.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            Logger.WriteLog("\t\tScanning files...", Logger.head, false);

            string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft").Replace("C:", $@"{Program.drive_letter}:");
            FindMalwareFiles(baseDirectory);

            if (founded_malware_files.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            if (!Program.WinPEMode)
            {
                ScanRegistry();

                int BootMode = WinApi.GetSystemMetrics(WinApi.SM_CLEANBOOT);

                switch (BootMode)
                {
                    case 0:
                        Logger.WriteLog("\t\tScanning firewall...", Logger.head, false);
                        ScanFirewall();
                        Logger.WriteLog($"\t\tScanning Tasks...", Logger.head, false);
                        ScanTaskScheduler();
                        break;
                    case 1:
                        Logger.WriteLog("\t[#] Safe boot: no scan tasks and firewall rules", ConsoleColor.Blue);
                        break;
                    case 2:
                        Logger.WriteLog("\t\tScanning firewall...", Logger.head, false);
                        ScanFirewall();
                        Logger.WriteLog("\t[#] Safe boot networking: no scan tasks", ConsoleColor.Blue);
                        break;
                    default:
                        break;
                }

            }
            CleanHosts();

            List<string> filesToLock = new List<string>()
            {
                $@"{Program.drive_letter}:\ProgramData\ReaItekHD",
                $@"{Program.drive_letter}:\ProgramData\Windows Tasks Service",
                $@"{Program.drive_letter}:\ProgramData\WindowsTask",
                $@"{Program.drive_letter}:\ProgramData\System32"
            };


            foreach (string objectTolock in filesToLock)
            {
                if (Directory.Exists(objectTolock))
                {
                    continue;
                }
                if (!File.Exists(objectTolock))
                {
                    try
                    {
                        File.WriteAllText(objectTolock, "not-a-virus");
                        Thread.Sleep(100);
                        LockFile(objectTolock);
                        Logger.WriteLog($"\t[D] Created protected file {objectTolock}", Logger.head);
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                }
            }
        }

        public void SignatureScan()
        {
            if (!Program.WinPEMode)
            {
                paths_to_scan.Add(Path.GetTempPath());
                paths_to_scan.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }

            signatures = utils.RestoreSignatures(signatures);

            foreach (string path in paths_to_scan)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }


                List<string> executableFiles = utils.GetFiles(path, "*.exe", 0, Program.maxSubfolders);
                foreach (var file in executableFiles)
                {

                    Console.WriteLine($"Analyzing {file}...");
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        if (fileInfo.Length > maxFileSize)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t[OK]");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        WinVerifyTrustResult trustResult = WinTrust.VerifyEmbeddedSignature(file);
                        if (trustResult == WinVerifyTrustResult.Success)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t[OK]");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        bool sequenceFound = utils.BinarySearchByteSequenceInFile(file, signatures);

                        if (sequenceFound)
                        {
                            Logger.WriteLog($"\tFOUND: {file}", ConsoleColor.Magenta);

                            founded_malware_files.Add(file);
                            previousMalwareFilePath.Add(file);
                            continue;
                        }

                        bool computedSequence = utils.CheckByteSequenceOccurrences(file, 16, 100);
                        if (computedSequence)
                        {

                            founded_malware_files.Add(file);
                            previousMalwareFilePath.Add(file);
                            Logger.WriteLog($"\tFOUND: {file}", ConsoleColor.Magenta);
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\t[OK]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Error analyzing file {file}\n{ex.Message}", Logger.error);
                    }
                }
                executableFiles.Clear();
            }
            signatures.Clear();

            if (founded_malware_files.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
            else
            {
                Logger.WriteLog($"\t[!!] Founded threats: {founded_malware_files.Count}", Logger.cautionLow, false);
                Logger.WriteLog($"\t[#] Restart cleanup...", ConsoleColor.Blue, false);
                CleanFoundedMalware();
            }
        }

        public void CleanFoundedMalware()
        {
            if (founded_malware_files.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving founded malicious files...", Logger.head, false);

                if (!Directory.Exists(quarantineFolder))
                {
                    Directory.CreateDirectory(quarantineFolder);
                }

                string prevMalwarePathsLog = Path.Combine(quarantineFolder, $"previousMalwarePaths_{utils.GetRndString()}.txt");

                File.WriteAllLines(prevMalwarePathsLog, previousMalwareFilePath);

                foreach (string path in founded_malware_files)
                {
                    if (File.Exists(path))
                    {
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
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
                                File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Logger.WriteLog($"\t[x] cannot delete file {path}\n{ex.Message}\n{ex.StackTrace}", Logger.error);
                                Logger.WriteLog($"\t[.] Trying to find blocking process...", ConsoleColor.White);

#else
                                Logger.WriteLog($"\t[x] malware_files: cannot delete file {path} | {ex.Message}", Logger.error);
                                Logger.WriteLog($"\t[.] Trying to find blocking process...", ConsoleColor.White);

#endif
                                try
                                {
                                    try
                                    {
                                        uint processId = utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                            }
                                        }
                                        Logger.WriteLog("Blocking process has been terminated", Logger.success);
                                    }
                                    catch (Exception) { }

                                    File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
                                    File.Delete(path);
                                    if (!File.Exists(path))
                                    {
                                        Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                                    }
                                }
                                catch (Exception)
                                {
                                    Logger.WriteLog($"\t[x] Failed to delete file: {path}\n{ex.Message}", Logger.error);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }

        public void Clean()
        {
            if (malware_pids.Count != 0)
            {
                Logger.WriteLog("\t\tTrying to close processes...", Logger.head, false);

                utils.UnProtect(malware_pids.ToArray());

                foreach (var id in malware_pids)
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
                                Logger.WriteLog($"\t[+] Malicious process {pname} - pid:{pid} successfully closed", Logger.success);
                            }
                        }

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

            Logger.WriteLog("\t\tRemoving known malware files...", Logger.head, false);
            int deletedFilesCount = 0;

            foreach (string path in known_malware_files)
            {
                if (File.Exists(path))
                {
                    UnlockFile(path);
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Delete(path);
                        Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                        deletedFilesCount++;
                    }
                    catch (Exception)
                    {
                        Logger.WriteLog($"\t[!!] Cannot delete file {path}", Logger.cautionLow);
                        Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                        UnlockDirectory(Path.GetDirectoryName(path));
                        try
                        {
                            Logger.WriteLog($"\t[+] Unlock success", Logger.success);

                            try
                            {
                                uint processId = utils.GetProcessIdByFilePath(path);

                                if (processId != 0)
                                {
                                    Process process = Process.GetProcessById((int)processId);
                                    if (!process.HasExited)
                                    {
                                        process.Kill();
                                        Logger.WriteLog($"\t[+] Blocking process {processId} has been closed", Logger.success);
                                    }
                                }
                            }
                            catch (Exception) { }

                            Thread.Sleep(100);
                            File.Delete(path);
                            if (!File.Exists(path))
                            {
                                Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                                deletedFilesCount++;
                            }

                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            Logger.WriteLog($"\t[x] known_malware_files: cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                            Logger.WriteLog($"\t[x] known_malware_files: cannot delete file {path} | {ex.Message}", Logger.error);
#endif
                        }
                    }
                }
            }

            CleanFoundedMalware();

            if (suspiciousFiles_path.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving malicious files...", Logger.head, false);
                foreach (string path in suspiciousFiles_path)
                {
                    if (File.Exists(path))
                    {
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                            Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                        }
                        catch (Exception)
                        {
                            Logger.WriteLog($"\t[!!] suspiciousFiles_path: Cannot delete file {path}", Logger.cautionLow);
                            Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                            UnlockDirectory(Path.GetDirectoryName(path));
                            try
                            {
                                Logger.WriteLog($"\t[+] Unlock success", Logger.success);
                                try
                                {
                                    uint processId = utils.GetProcessIdByFilePath(path);

                                    if (processId != 0)
                                    {
                                        Process process = Process.GetProcessById((int)processId);
                                        if (!process.HasExited)
                                        {
                                            process.Kill();
                                            Logger.WriteLog($"\t[+] Blocking process {processId} has been closed", Logger.success);
                                        }
                                    }
                                }
                                catch (Exception) { }
                                Thread.Sleep(100);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Logger.WriteLog($"\t[+] Malicious file {path} deleted", Logger.success);
                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Logger.WriteLog($"\t[x] suspiciousFiles: cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                                Logger.WriteLog($"\t[x] suspiciousFiles: cannot delete file {path} | {ex.Message}", Logger.error);
#endif
                            }
                        }
                    }
                }
            }

            if (deletedFilesCount == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            if (founded_malwarePaths.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving malware paths...", Logger.head, false);
                foreach (string str in founded_malwarePaths)
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

            if (founded_suspiciousLockedPaths.Count > 0)
            {
                DeleteEmptyFolders(founded_suspiciousLockedPaths);
            }

            if (!Program.WinPEMode)
            {
                Logger.WriteLog("\t\tChecking user John...", Logger.head, false);
                if (utils.CheckUserExists("john"))
                {
                    if (Environment.UserName.ToLower() == "john")
                    {
                        Logger.WriteLog($"\t[#] Current user - john. Removing is not required", ConsoleColor.Blue);
                    }
                    else
                    {
                        try
                        {
                            utils.DeleteUser("john");
                            Thread.Sleep(100);
                            if (!utils.CheckUserExists("john"))
                            {
                                Logger.WriteLog("\t[+] Successfull deleted userprofile \"John\"", Logger.success);
                            }
                            else
                                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog($"\t[x] Cannot delete user \"John\":\n{ex.Message}", Logger.error);
                        }
                    }


                }
                else
                    Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

        }
        void DeleteEmptyFolders(List<string> inputList)
        {
            int foldersDeleted = 0;
            foreach (string str in inputList)
            {
                try
                {
                    if (utils.IsDirectoryEmpty(str))
                    {
                        UnlockDirectory(str);
                        Directory.Delete(str, true);
                        if (!Directory.Exists(str))
                        {
                            Logger.WriteLog($"\t[_] Removed empty dir '{str}'", ConsoleColor.White);
                            foldersDeleted++;
                        }
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        UnlockDirectory(str);
                        if (utils.IsDirectoryEmpty(str))
                        {
                            Directory.Delete(str);
                            if (!Directory.Exists(str))
                            {
                                Logger.WriteLog($"\t[_] Removed empty dir '{str}'", ConsoleColor.White);
                                foldersDeleted++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Cannot remove dir {str}\n{ex.Message}", Logger.error);
                    }

                }
            }

            if (foldersDeleted == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }
        void ScanDirectories(List<string> constDirsArray, List<string> newList)
        {
            foreach (string dir in constDirsArray)
            {
                if (Directory.Exists(dir))
                {
                    newList.Add(dir);
                }
            }
        }
        void ScanFirewall()
        {
            int affected_items = 0;
            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);

                INetFwRules rules = fwPolicy2.Rules;

                foreach (string programPath in known_malware_files)
                {
                    foreach (INetFwRule rule in rules)
                    {
                        if (rule.ApplicationName != null)
                        {
                            if (rule.ApplicationName.ToLower() == programPath.ToLower())
                            {
                                Logger.WriteLog($"[.] Name: {rule.Name}", ConsoleColor.White);
                                Logger.WriteLog($"\t[!] Path: {rule.ApplicationName}", Logger.warn);

                                rules.Remove(rule.Name);
                                affected_items++;
                                Logger.WriteLog($"\t[+] Rule {rule.Name} has been removed", Logger.success);
                                Logger.WriteLog($"------------------------------", ConsoleColor.White);
                            }
                        }

                    }

                }
                if (affected_items == 0)
                {
                    Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get firewall rules: {ex.Message}");
            }
        }
        void FindMalwareFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            try
            {
                IEnumerable<string> files = Directory.GetFiles(directoryPath, "*.bat", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    founded_malware_files.Add(file);
                    foreach (var nearExeFile in Directory.GetFiles(Path.GetDirectoryName(file), "*.exe", SearchOption.TopDirectoryOnly))
                    {
                        founded_malware_files.Add(nearExeFile);
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
        void CleanHosts()
        {
            Logger.WriteLog("\t\tScanning hosts file...", Logger.head, false);

            RegistryKey hostsDir = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters");
            if (hostsDir != null)
            {
                string hostsPath = hostsDir.GetValue("DataBasePath").ToString();
                if (hostsPath.StartsWith("%"))
                {
                    hostsPath = utils.ResolveEnvironmentVariables(hostsPath);
                }

                string hostsPath_full = hostsPath + "\\hosts";

                if (Program.WinPEMode)
                {
                    hostsPath_full.Replace("C:", $"{Program.drive_letter}:");
                }

                if (!Program.WinPEMode && !File.Exists(hostsPath_full))
                {
                    Logger.WriteLog($"\t[?] Hosts file is missing", ConsoleColor.Gray);
                    File.Create(hostsPath_full).Close();
                    Thread.Sleep(100);
                    if (File.Exists(hostsPath_full))
                    {
                        Logger.WriteLog($"\t[+] New hosts file has been created", Logger.success);
                    }
                    return;
                }


                try
                {
                    UnlockFile(hostsPath_full);
                    File.SetAttributes(hostsPath_full, FileAttributes.Normal);
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error CleanHosts: {ex.Message}", Logger.error);
                    return;
                }

                List<string> knownStrings = new List<string>
                {
                    Encoding.UTF8.GetString(Convert.FromBase64String("Y29kZWxvYWQuZ2l0aHViLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c3VwcG9ydC5rYXNwZXJza3kucnU=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("a2FzcGVyc2t5LnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dmlydXNpbmZvLmluZm8=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("Zm9ydW0ua2FzcGVyc2t5Y2x1Yi5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("Y3liZXJmb3J1bS5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdC1maWxlLnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("MzYwdG90YWxzZWN1cml0eS5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("Y2V6dXJpdHkuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmRyb3Bib3guY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("MTkzLjIyOC41NC4yMw==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c3BlYy1rb21wLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZXNldC51YQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cmVnaXN0LnNhZmV6b25lLmNj")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cHJvZ3JhbWtpLm5ldA==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c2FmZXpvbmUuY2M=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmVzZXRub2QzMi5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmNvbXNzLnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("Zm9ydW0ub3N6b25lLm5ldA==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("YmxvZy1wYy5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c2VjdXJyaXR5LnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("bm9ydG9uLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dmVsbGlzYS5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZG93bmxvYWQtc29mdHdhcmUucnU=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZHJ3ZWItY3VyZWl0LnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdHBhY2tldC5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3Lmthc3BlcnNreS5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmF2YXN0LnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmF2YXN0LnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("emlsbHlhLnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c2FmZXpvbmUudWE=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dm1zLmRyd2ViLnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmRyd2ViLnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZnJlZS5kcndlYi5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("YmlibHByb2cub3JnLnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZnJlZS1zb2Z0d2FyZS5jb20udWE=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZnJlZS5kYXRhcHJvdGVjdGlvbi5jb20udWE=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmRyd2ViLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LnNvZnRwb3J0YWwuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3Lm5hc2huZXQudWE=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdGxpc3QuY29tLnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("aXQtZG9jLmluZm8=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZXNldG5vZDMyLnJ1")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("YmxvZy1icmlkZ2UucnU=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cmVtb250a2EucHJv")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c2VjdXJvcy5vcmcudWE=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cGMtaGVscHAuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdGRyb2lkLm5ldA==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("bWFsd2FyZWJ5dGVzLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cnUudmVzc29mdC5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("QWxwaW5lRmlsZS5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("bWFsd2FyZWJ5dGVzLWFudGktbWFsd2FyZS5ydS51cHRvZG93bi5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("UHJvZ3JhbURvd25sb2FkRnJlZS5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZG93bmxvYWQuY25ldC5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdC5teWRpdi5uZXQ=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c3B5d2FyZS1ydS5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cmVtb250Y29tcGEucnU=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmhpdG1hbnByby5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("aGl0bWFuLXByby5ydS51cHRvZG93bi5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmJsZWVwaW5nY29tcHV0ZXIuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdC5vc3pvbmUubmV0")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("a3J1dG9yLm9yZw==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmdyZWF0aXMuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dW5oYWNrbWUucnUudXB0b2Rvd24uY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cHJvZ3JhbXkuY29tLnVh")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("cnNsb2FkLm5ldA==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("c29mdG9iYXNlLmNvbQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("d3d3LmJlc3BsYXRub3Byb2dyYW1teS5ydQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dW5oYWNrbWUuZW4uc29mdG9uaWMuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dW5oYWNrbWUuY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dW5oYWNrbWUucnU=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("bm5tLWNsdWIubmFtZQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("dmdyb20uY29t")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("eWFkaS5zdQ==")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("ZXNldC5jb20=")),
                    Encoding.UTF8.GetString(Convert.FromBase64String("bXl3b3QuY29t"))
                };

                int originalLineCount = 0;
                int deletedLineCount = 0;

                try
                {
                    List<string> lines = File.ReadAllLines(hostsPath_full).ToList();
                    originalLineCount = lines.Count;

                    lines = lines.Where(line =>
                    {
                        if (knownStrings.Any(known => line.Contains(known)))
                        {
                            deletedLineCount++;
                            return false;
                        }
                        return true;
                    }).ToList();

                    File.WriteAllLines(hostsPath_full, lines);

                    if (deletedLineCount > 0)
                    {
                        string logMessage = $"Hosts file has been recovered. Affected strings {deletedLineCount}";
                        Logger.WriteLog(logMessage, Logger.success);
                    }
                    else
                        Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);

                }
                catch (Exception e)
                {
                    Logger.WriteLog("Error read/write: " + e.Message, Logger.error);
                }
            }
        }
        void ScanRegistry()
        {
            Logger.WriteLog("\t\tScanning registry...", Logger.head, false);
            int affected_items = 0;

            #region DisallowRun
            RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            if (DisallowRunKey != null)
            {
                if (DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                {
                    Logger.WriteLog("\t[!] Suspicious registry key: DisallowRun - restricts the launch of the specified applications", Logger.warn);
                    DisallowRunKey.DeleteValue("DisallowRun");
                    if (!DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                    {
                        Logger.WriteLog("\t[+] DisallowRun key successfully deleted", Logger.success);
                        affected_items++;
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
                        affected_items++;
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
                                affected_items++;
                            }
                        }
                        else if (appinit_key.GetValue("RequireSignedAppInit_DLLs").ToString() == "0")
                        {
                            Logger.WriteLog("\t[!] AppInit_DLLs is not empty", Logger.warn);
                            Logger.WriteLog("\t[!!!] RequireSignedAppInit_DLLs key set is 0", Logger.caution);
                            appinit_key.SetValue("RequireSignedAppInit_DLLs", 1, RegistryValueKind.DWord);
                            if (appinit_key.GetValue("RequireSignedAppInit_DLLs").ToString() == "1")
                            {
                                Logger.WriteLog("\t[+] The value was set to 1", Logger.success);
                                affected_items++;
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
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }

                        if (AutorunKey.GetValue(value).ToString() == $@"{Program.drive_letter}:\ProgramData\ReaItekHD\taskhostw.exe")
                        {
                            AutorunKey.DeleteValue(value);
                            Logger.WriteLog("\t[+] Removed malicious autorun key RealtekHD", Logger.success);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKLM\\...\\run: {ex.Message}", Logger.error);
            }

            #region WindowsDefender

            Logger.WriteLog(@"HKLM\Software\Policies\Microsoft\Windows Defender\Exclusions", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey WindowsDefender = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows Defender\Exclusions", true);
                if (WindowsDefender != null)
                {

                    foreach (string path in WD_exclusion_paths)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows Defender\Exclusions\Paths", true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
                                    {
                                        key.DeleteValue(valueName);
                                        Logger.WriteLog($"\t[+] Removed {valueName} exclusion", Logger.success);
                                        affected_items++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"[x] Cannot {valueName} exclusion | {ex.Message}", Logger.error);
                                }

                            }

                            key.Close();
                        }
                    }

                    foreach (string process in WD_exclusion_processes)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows Defender\Exclusions\Processes", true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(process, StringComparison.OrdinalIgnoreCase))
                                    {
                                        key.DeleteValue(valueName);
                                        Logger.WriteLog($"\t[+] Removed {valueName} exclusion", Logger.success);
                                        affected_items++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"[x] Cannot {valueName} exclusion | {ex.Message}", Logger.error);
                                }

                            }

                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKLM\\...\\Windows Defender\\Exclusions: {ex.Message}", Logger.error);
            }

            #endregion
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
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
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
                        tektonit.DeleteSubKeyTree("tektonit");
                        if (!tektonit.GetSubKeyNames().Contains("tektonit"))
                        {
                            Logger.WriteLog("\t[+] tektonit key successfully deleted", Logger.success);
                            affected_items++;
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
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
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

            if (affected_items == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }
        void ScanTaskScheduler()
        {
            TaskService taskService = new TaskService();
            IEnumerable<Task> Tasks = taskService.AllTasks;
            foreach (Task task in Tasks.OrderBy(t => t.Name).ToList())
            {
                if (task != null)
                {
                    foreach (ExecAction action in task.Definition.Actions.OfType<ExecAction>())
                    {

                        string filePath = utils.ResolveEnvironmentVariables(action.Path.Replace("\"", ""));

                        Logger.WriteLog($"[#] Scan: {task.Name} | {task.Folder}", ConsoleColor.White);

                        if (filePath.Contains(":\\"))
                        {
                            if (!File.Exists(filePath))
                            {
                                Logger.WriteLog($"\t[!] File is not exists: {filePath}", Logger.warn);

                                if (Program.RemoveEmptyTasks)
                                {
                                    string taskName = task.Name;
                                    string taskFolder = task.Folder.ToString();
                                    try
                                    {
                                        taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                        if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                        {
                                            Logger.WriteLog($"\t[+] Empty Task {taskName} was deleted", Logger.success);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.WriteLog($"\t[x] Cannot delete task {taskFolder}\\{taskName} | {ex.Message}", Logger.error);
                                    }

                                }

                            }
                            else
                            {
                                Logger.WriteLog($"\t[.] File: {filePath}", ConsoleColor.Gray);

                                try
                                {
                                    if (WinTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success || new FileInfo(filePath).Length > maxFileSize)
                                    {
                                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                                        
                                        continue;
                                    }

                                    if (utils.BinarySearchByteSequenceInFile(filePath, signatures) || (utils.CheckByteSequenceOccurrences(filePath, 16, 100)))
                                    {
                                        Logger.WriteLog($"FOUND: {filePath}", Logger.caution);
                                        founded_malware_files.Add(filePath);
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"\t[x] TaskScheduler scan error: {ex.Message}", Logger.error);
                                }
                            }
                        }
                        else
                        {
                            string system32Path = Path.Combine(Environment.SystemDirectory, filePath);

                            if (!system32Path.EndsWith(".exe"))
                            {
                                system32Path += ".exe";
                            }


                            if (!File.Exists(system32Path))
                            {
                                Logger.WriteLog($"\t[!] File is not exists: {system32Path}", Logger.warn);

                                if (Program.RemoveEmptyTasks)
                                {
                                    string taskName = task.Name;
                                    string taskFolder = task.Folder.ToString();

                                    try
                                    {
                                        taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                        if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                        {
                                            Logger.WriteLog($"\t[+] Empty Task {taskName} was deleted", Logger.success);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.WriteLog($"\t[x] Cannot delete task {taskFolder}\\{taskName} | {ex.Message}", Logger.error);
                                    }
                                }
                            }
                            else
                            {
                                try

                                {
                                    Logger.WriteLog($"\t[.] File: {system32Path}", ConsoleColor.Gray);
                                    if (WinTrust.VerifyEmbeddedSignature(system32Path) == WinVerifyTrustResult.Success || new FileInfo(system32Path).Length > maxFileSize)
                                    {
                                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                                        continue;
                                    }

                                    if (utils.BinarySearchByteSequenceInFile(system32Path, signatures) || (utils.CheckByteSequenceOccurrences(system32Path, 16, 100)))
                                    {
                                        Logger.WriteLog($"FOUND: {system32Path}", Logger.caution);
                                        founded_malware_files.Add(system32Path);
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"\t[x] TaskScheduler scan error: {ex.Message}", Logger.error);
                                }
                            }

                            if (utils.getBitVersion().Contains("x64"))
                            {
                                string sysWow64path = Path.Combine($@"{Program.drive_letter}:\Windows\SysWOW64", filePath);

                                if (!sysWow64path.EndsWith(".exe"))
                                {
                                    sysWow64path += ".exe";
                                }

                                if (!File.Exists(sysWow64path))
                                {
                                    Logger.WriteLog($"\t[!] File is not exists: {sysWow64path}", Logger.warn);

                                    if (Program.RemoveEmptyTasks)
                                    {
                                        string taskName = task.Name;
                                        string taskFolder = task.Folder.ToString();

                                        try
                                        {
                                            taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                            if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                            {
                                                Logger.WriteLog($"\t[+] Empty Task {taskName} was deleted", Logger.success);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.WriteLog($"\t[x] Cannot delete task {taskFolder}\\{taskName} | {ex.Message}", Logger.error);
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        Logger.WriteLog($"\t[.] File: {sysWow64path}", ConsoleColor.Gray);
                                        if (WinTrust.VerifyEmbeddedSignature(sysWow64path) == WinVerifyTrustResult.Success || new FileInfo(sysWow64path).Length > maxFileSize)
                                        {
                                            Logger.WriteLog($"\t[OK]", Logger.success, false);
                                            continue;
                                        }

                                        if (utils.BinarySearchByteSequenceInFile(sysWow64path, signatures) || (utils.CheckByteSequenceOccurrences(sysWow64path, 16, 100)))
                                        {
                                            Logger.WriteLog($"FOUND: {sysWow64path}", Logger.caution);
                                            founded_malware_files.Add(sysWow64path);
                                            continue;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.WriteLog($"\t[x] TaskScheduler scan error: {ex.Message}", Logger.error);
                                    }
                                }
                            }

                        }

                        if (!Program.RemoveEmptyTasks)
                        {
                            if ((uint)task.LastTaskResult == (0x80070002))
                            {
                                Logger.WriteLog($"\t[!] Empty task {task.Name}", Logger.warn);

                                string taskName = task.Name;
                                string taskFolder = task.Folder.ToString();
                                try
                                {
                                    taskService.GetFolder(task.Folder.ToString()).DeleteTask(task.Name);
                                    if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                                    {
                                        Logger.WriteLog($"\t[+] Task {taskName} has been deleted", Logger.success);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"\t[x] Cannot delete task {taskFolder}\\{taskName} | {ex.Message}", Logger.error);
                                }
                            }
                        }
                    }
                }
            }
            taskService.Dispose();
        }
        void UnlockDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            try
            {
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                DirectorySecurity directorySecurity = new DirectorySecurity();
                directorySecurity.SetOwner(currentUserIdentity);

                directorySecurity.SetAccessRuleProtection(true, false);

                AuthorizationRuleCollection accessRules = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        directorySecurity.RemoveAccessRuleSpecific(fileRule);
                    }
                }

                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(currentUserRule);

                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(administratorsRule);

                SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule usersRule = new FileSystemAccessRule(
                    usersGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(usersRule);

                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(systemRule);

                Directory.SetAccessControl(directoryPath, directorySecurity);


            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }
        }
        void UnlockFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                // Получаем текущего пользователя
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                // Создаем объект FileSecurity и устанавливаем владельца
                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(currentUserIdentity);

                // Удаляем унаследованные права доступа
                fileSecurity.SetAccessRuleProtection(true, false);

                // Удаляем все права доступа Запрет
                AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        fileSecurity.RemoveAccessRuleSpecific(fileRule);
                    }
                }

                // Добавляем права доступа для текущего пользователя
                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(currentUserRule);

                // Добавляем права доступа для группы администраторов
                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(administratorsRule);

                // Добавляем права доступа для группы пользователей
                SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule usersRule = new FileSystemAccessRule(
                    usersGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(usersRule);

                // Добавляем права доступа для системы
                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(systemRule);

                // Применяем настройки к файлу
                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }
        }
        void LockFile(string filePath)
        {
            try
            {
                File.SetAttributes(filePath, FileAttributes.Hidden | FileAttributes.System);
                // Получаем текущего пользователя
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                // Создаем объект FileSecurity и устанавливаем владельца
                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(currentUserIdentity);

                // Удаляем унаследованные права доступа
                fileSecurity.SetAccessRuleProtection(true, false);

                // Добавляем права доступа для текущего пользователя
                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(currentUserRule);

                // Добавляем права доступа для группы администраторов
                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(administratorsRule);


                // Добавляем права доступа для системы
                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(systemRule);

                // Применяем настройки к файлу
                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }
        }

    }
}
