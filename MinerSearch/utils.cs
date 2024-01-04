//#define BETA

using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MinerSearch
{
    internal static class utils
    {
        static string query = Bfs.Create("OduMCaT1Jwq6AToZkbfR8OuO70dOPgupqQrYRVwVdT+QSo3fVJe7ICQcMs3keFaX27tGy3ge0/8r8PelNnhUcA==",
            new byte[] { 0x93, 0x9e, 0x9e, 0x5c, 0xfd, 0xb5, 0xcf, 0xca, 0xb5, 0x67, 0xe9, 0x72, 0x3e, 0x60, 0x40, 0xd5, 0x5c, 0x48, 0x84, 0x91, 0x8c, 0xad, 0x60, 0x06, 0x48, 0x15, 0x0b, 0x53, 0x9e, 0x7f, 0xef, 0x74 },
            new byte[] { 0x6f, 0x4f, 0xeb, 0x53, 0x35, 0x5f, 0x75, 0x3f, 0x90, 0xb4, 0xc1, 0xb0, 0x8f, 0xaa, 0x92, 0x95 }); //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
        static string BitVersionStr = Bfs.Create("DJDSmyQtQkO8ateFOpqOUbwnOJ/9ggu/qaSL4ts1BPzAQSgu3+nOOD+qmVS9mGbW",
            new byte[] { 0x93, 0x49, 0x84, 0x8b, 0xcf, 0x42, 0xd2, 0x6a, 0x78, 0x74, 0x8f, 0xb4, 0x40, 0x37, 0xb9, 0xb7, 0x98, 0x92, 0x3d, 0x17, 0x0c, 0xab, 0x50, 0x71, 0x25, 0x38, 0xea, 0xf7, 0x41, 0x65, 0x2a, 0x92 },
            new byte[] { 0x19, 0x95, 0xc4, 0x31, 0x89, 0x8e, 0x99, 0x4a, 0x14, 0xc5, 0x01, 0xca, 0x1a, 0x87, 0x3f, 0x5f }); //HARDWARE\Description\System\CentralProcessor\0

        [Serializable]
        public class RenamedFileInfo
        {
            public int _ProcessId { get; set; }
            public string _NewFilePath { get; set; }
        }

        internal static string GetCommandLine(Process process)
        {
            string cmdLine = null;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query + process.Id))
            {
                ManagementObjectCollection.ManagementObjectEnumerator matchEnum = searcher.Get().GetEnumerator();
                if (matchEnum.MoveNext())
                {
                    cmdLine = matchEnum.Current["CommandLine"]?.ToString();
                }
            }
            return cmdLine;
        }

        internal static string Sizer(long CountBytes)
        {
            string[] type = { "B", "KB", "MB", "GB" };
            if (CountBytes == 0)
                return $"0 {type[0]}";

            double bytes = Math.Abs(CountBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(CountBytes) * num} {type[place]}";
        }

        internal static List<Process> GetProcesses()
        {
            List<Process> procs = new List<Process>();
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    ProcessModule t = p.Modules[0];
                }
#if BETA
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t [x] Error for get process {p.ProcessName} | {ex.Message}", ConsoleColor.Red, false);
                }
#else
                catch (Exception)
                {
                    continue;
                }
#endif
                procs.Add(p);
            }
            return procs;
        }

        internal static int GetPortByProcessId(int pid)
        {
            int buffSize = 0;
            Native.GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, 2, Native.TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0);
            IntPtr tcpTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                Native.GetExtendedTcpTable(tcpTable, ref buffSize, true, 2, Native.TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0);

                int rowNumber = Marshal.ReadInt32(tcpTable);
                IntPtr rowPtr = (IntPtr)(tcpTable.ToInt64() + 4);

                for (int i = 0; i < rowNumber; i++)
                {
                    Native.MIB_TCPROW_OWNER_PID row = (Native.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(Native.MIB_TCPROW_OWNER_PID));
                    if (row.owningPid == pid)
                    {
                        return BitConverter.ToUInt16(new byte[2] { row.remotePort[1], row.remotePort[0] }, 0);
                    }
                    rowPtr = (IntPtr)(rowPtr.ToInt64() + Marshal.SizeOf(row));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTable);
            }

            return -1;
        }

        internal static uint GetProcessIdByFilePath(string filePath)
        {
            Native.PROCESSENTRY32 processEntry = new Native.PROCESSENTRY32();
            processEntry.dwSize = (uint)Marshal.SizeOf(typeof(Native.PROCESSENTRY32));

            IntPtr snapshotHandle = Native.CreateToolhelp32Snapshot(Native.TH32CS_SNAPPROCESS, 0);

            if (Native.Process32First(snapshotHandle, ref processEntry))
            {
                do
                {
                    Process process = Process.GetProcessById((int)processEntry.th32ProcessID);

                    try
                    {
                        foreach (ProcessModule module in process.Modules)
                        {
                            if (module.FileName.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                            {
                                Native.CloseHandle(snapshotHandle);
                                return processEntry.th32ProcessID;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore any exceptions caused by accessing the process modules.
                    }
                } while (Native.Process32Next(snapshotHandle, ref processEntry));
            }

            Native.CloseHandle(snapshotHandle);
            return 0;
        }

        internal static int GetParentProcessId(int processId)
        {
            int parentProcessId = 0;
            IntPtr hProcess = Native.OpenProcess(Native.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

            if (hProcess != IntPtr.Zero)
            {
                Native.PROCESS_BASIC_INFORMATION pbi = new Native.PROCESS_BASIC_INFORMATION();

                int status = Native.NtQueryInformationProcess(hProcess, 0, ref pbi, Marshal.SizeOf(pbi), out int returnLength);

                if (status == Native.STATUS_SUCCESS)
                {
                    parentProcessId = pbi.InheritedFromUniqueProcessId.ToInt32();
                }

                Native.CloseHandle(hProcess);
            }

            return parentProcessId;
        }

        internal static void UnProtect(int[] pids)
        {
            Process.EnterDebugMode();
            try
            {

                foreach (int pid in pids)
                {
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;

                    IntPtr handle = Native.OpenProcess(0x001F0FFF, false, pid);
                    Native.NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
                    Native.CloseHandle(handle);
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

        internal static void SuspendProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                ProcessThreadCollection Threads = process.Threads;
                int totalThreads = Threads.Count;

                foreach (ProcessThread pT in Threads)
                {

                    IntPtr pOpenThread = Native.OpenThread(Native.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        continue;
                    }
                    else
                    {
                        Native.SuspendThread(pOpenThread);
                        Native.CloseHandle(pOpenThread);
                    }

                }

                foreach (ProcessThread pT in Threads)
                {
                    if (pT.ThreadState == System.Diagnostics.ThreadState.Wait)
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

                process.Close();

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

        internal static string GetDownloadsPath()
        {
            IntPtr pathPtr = IntPtr.Zero;

            try
            {
                Native.SHGetKnownFolderPath(ref Native.FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Error GetDownloadsPath: {ex.Message}", Logger.error);
                return "";
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }

        internal static string ResolveEnvironmentVariables(string path)
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

        internal static string ResolveFilePathFromString(RegistryKey key, string keyName)
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
                            value = utils.ResolveEnvironmentVariables(value.Replace("\"", ""));
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

        internal static string ResolveFilePathFromString(string line)
        {
            try
            {

                if (line != null)
                {
                    if (line == "")
                    {
                        return "";
                    }

                    if (line.StartsWith("\"") && line.EndsWith("\"") || line.StartsWith("\"%") || line.StartsWith("%"))
                    {
                        line = utils.ResolveEnvironmentVariables(line.Replace("\"", ""));
                    }

                    if (line.Contains(":\\"))
                    {
                        int index = line.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                        if (index > 0)
                        {
                            string executablePath = line.Substring(0, index + 4);
                            return executablePath.Replace("\"", "");
                        }
                    }
                    else if (!line.Contains(":\\") && line.ToLower().EndsWith(".exe"))
                    {
                        if (File.Exists(Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", line)))
                        {
                            return Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", line);
                        }
                        else if (File.Exists(Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SysWOW64", line)))
                        {
                            return Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SysWOW64", line);
                        }
                    }
                    return line;
                }

                return "";
            }
            catch (Exception)
            {
                return "";
            }



        }

        internal static string GetRndString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(8);
        }

        internal static string GetRndString(int len)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(len);
        }

        internal static bool IsDirectoryEmpty(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] subdirectories = Directory.GetDirectories(path);

            if (files.Length > 0 || subdirectories.Length > 0)
                return false;

            foreach (string subdirectory in subdirectories)
            {
                if (!IsDirectoryEmpty(subdirectory))
                    return false;
            }

            return true;
        }

        internal static bool CheckSignature(string filePath, List<byte[]> targetSequences)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            const int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int fileIndex = 0;
            bool found = false;

            while (fileIndex < fileBytes.Length)
            {
                int bytesToRead = Math.Min(bufferSize, fileBytes.Length - fileIndex);
                Array.Copy(fileBytes, fileIndex, buffer, 0, bytesToRead);

                fileIndex += bytesToRead;

                for (int i = 0; i <= bytesToRead - targetSequences[0].Length; i++)
                {
                    foreach (var targetSequence in targetSequences)
                    {
                        bool sequenceFound = true;
                        for (int j = 0; j < targetSequence.Length; j++)
                        {
                            if (i + j >= bytesToRead || buffer[i + j] != targetSequence[j])
                            {
                                sequenceFound = false;
                                break;
                            }
                        }

                        if (sequenceFound)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }

                if (found)
                    break;
            }

            return found;
        }

        internal static List<string> GetFiles(string path, string pattern, int currentDepth = 0, int maxDepth = 2)
        {
            var files = new List<string>();

            try
            {
                if (currentDepth <= maxDepth)
                {
                    files.AddRange(Directory.EnumerateFiles(path, pattern, SearchOption.TopDirectoryOnly));
                    foreach (var directory in Directory.GetDirectories(path))
                    {
                        if (!directory.Contains(":\\Windows\\WinSxS") &&
                            !directory.Contains(":\\$") &&
                            !directory.Contains("minersearch_quarantine") &&
                            !directory.Contains(@":\ProgramData\Microsoft\Windows\Containers\BaseImages") &&
                            !directory.Contains(@"AppData\Local\Microsoft\WindowsApps"))
                        {
                            files.AddRange(GetFiles(directory, pattern, currentDepth + 1, maxDepth));
                        }
                    }
                }
            }
            catch (Exception) { }

            return files;
        }

        internal static bool IsSpecificPath(string path)
        {
            if (path.Contains("\u00a0"))
            {
                return true;
            }
            return false;
        }

        public static string GetWindowsVersion()
        {
            try
            {
                Native.OSVERSIONINFOEX osInfo = new Native.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(Native.OSVERSIONINFOEX))
                };

                if (Native.GetVersionEx(ref osInfo))
                {
                    foreach (Native.BuildNumber version in Enum.GetValues(typeof(Native.BuildNumber)))
                    {
                        if (osInfo.dwBuildNumber == (uint)version)
                        {
                            string WinVerion = $"{version.ToString().Replace('_', ' ')} {osInfo.dwBuildNumber}";
                            return WinVerion;
                        }
                    }

                    return "N/A";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }

            return "N/A";
        }

        internal static string getBitVersion()
        {
            if (Registry.LocalMachine.OpenSubKey(BitVersionStr).GetValue("Id?enti?fier".Replace("?", "")).ToString().Contains("x86"))
            {
                return "x86";
            }
            else
            {
                return "x86-64";
            }
        }

        internal static List<byte[]> RestoreSignatures(List<byte[]> signatures)
        {
            foreach (var sig in signatures)
            {
                for (int i = 0; i < sig.Length; i++)
                {
                    if (sig[i] / 2 == 0)
                    {
                        sig[i] += 1;
                    }
                    else
                    {
                        sig[i] -= 1;
                    }

                }
            }
            return signatures;
        }

        internal static bool CheckUserExists(string username)
        {
            Process net = Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd",
                Arguments = "/c net user",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.GetEncoding(866),
                StandardErrorEncoding = Encoding.GetEncoding(866)
            });

            string strOut = net.StandardOutput.ReadToEnd();
#if DEBUG
            string strErr = net.StandardError.ReadToEnd();
            int exitCode = net.ExitCode;

            Logger.WriteLog("\t[.] strOutput: " + strOut, ConsoleColor.DarkGray, false);
            Logger.WriteLog("\t[.] strError: " + strErr, ConsoleColor.DarkGray, false);
            Logger.WriteLog("\t[.] exit code: " + exitCode, ConsoleColor.DarkGray, false);
#endif
            return strOut.ToLower().Contains(username);

        }

        internal static void DeleteUser(string username)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "net",
                Arguments = $"user /delete {username}",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        internal static void CheckWMI()
        {
            string serviceName = "wi~nm~gm~t".Replace("~", "");

            try
            {
                if (ServiceHelper.ServiceIsInstalled(serviceName))
                {
                    var serviceinfo = ServiceHelper.GetServiceInfo(serviceName);

                    if ((ServiceHelper.ServiceBootFlag)serviceinfo.StartType != ServiceHelper.ServiceBootFlag.AutoStart)
                    {
                        ServiceHelper.ChangeStartMode(serviceName, ServiceHelper.ServiceBootFlag.AutoStart);
                        Logger.WriteLog($"\t[+] ~Sta~rt~up ty~pe of th~e critic~~al se~rvic~e h~as ~be~en rest~~ored".Replace("~", ""), Logger.success, false);
                    }

                    if (ServiceHelper.GetServiceState(serviceName) != ServiceHelper.ServiceState.Running)
                    {
                        ServiceHelper.StartService(serviceName);
                        Logger.WriteLog("\t[+] Crit~~ical service~ has be~en res~tar~ted".Replace("~", ""), Logger.success);
                    }
                }
                else
                {
                    Logger.WriteLog($"\t[xxx] {serviceName} service is not installed!", Logger.error);
                    Console.ReadLine();
                    Environment.Exit(-5);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[xxx] Error {serviceName}: {ex.Message}", ConsoleColor.DarkRed, false);
                Console.ReadLine();
                Environment.Exit(-5);
            }


        }

        internal static void CheckTermService()
        {
            string registryPath = Bfs.Create("3EmNXFx8rJJq7PbN/xLQ75rsj9VBP8uxvFQ5+cC3mcJNoCLR/OXzyCMlt6ETbUNADbanQ1X1I9pqbBAHOXufTg==",
            new byte[] { 0x43, 0x27, 0x70, 0x0b, 0xeb, 0xaf, 0xc1, 0xcd, 0xf7, 0x25, 0xdc, 0x62, 0x6a, 0x72, 0x78, 0x47, 0x5b, 0xa5, 0xdb, 0x49, 0x17, 0x52, 0x47, 0xdc, 0x92, 0x76, 0xa2, 0x39, 0x0c, 0x3e, 0xc4, 0xa1 },
            new byte[] { 0x91, 0xe8, 0xe8, 0x9b, 0xc3, 0xf4, 0x08, 0x73, 0xb9, 0xd9, 0x5e, 0xc6, 0xe0, 0xf4, 0xd5, 0xcb }); //SYSTEM\CurrentControlSet\Services\TermService\Parameters

            string paramName = Bfs.Create("z9KbnV9u8F5mkfUu2J3uOQ==",
            new byte[] { 0x16, 0x13, 0xd6, 0xe9, 0xf3, 0x11, 0x8d, 0x7f, 0x78, 0x62, 0xc4, 0xf4, 0xbc, 0x7a, 0x86, 0x48, 0x8b, 0x71, 0x2b, 0x9e, 0x2a, 0xe3, 0x08, 0x8a, 0xe6, 0xad, 0xa1, 0x80, 0x3b, 0x87, 0x7f, 0xb9 },
            new byte[] { 0x0f, 0xf7, 0x3c, 0x48, 0xb9, 0xca, 0x6d, 0xa2, 0x01, 0x8c, 0xc2, 0x72, 0xb6, 0xfa, 0x54, 0xd3 }); //ServiceDll

            string desiredValue = Bfs.Create("bmOebxzJe9mFF/zd+ykUeZGxU+CV2xfx0vRUigwXBoo6pFaMGKgRiSyjh9PNQZZ5",
            new byte[] { 0x62, 0xf2, 0x17, 0x00, 0x4d, 0x83, 0xb5, 0x12, 0x8a, 0x30, 0x21, 0x69, 0x2e, 0x4c, 0x93, 0x0e, 0x5b, 0x64, 0x15, 0x45, 0xe2, 0xf5, 0x39, 0xa4, 0xef, 0xb8, 0xae, 0x67, 0x4a, 0x13, 0xef, 0x5f },
            new byte[] { 0x29, 0xf1, 0x2f, 0xe0, 0x97, 0x25, 0x55, 0x21, 0x23, 0x2d, 0x69, 0x7e, 0x5a, 0xd4, 0x34, 0x12 }); //%SystemRoot%\System32\termsrv.dll

            using (var regkey = Registry.LocalMachine.OpenSubKey(registryPath, true))
            {
                string currentValue = (string)regkey.GetValue(paramName);
                if (currentValue != ResolveEnvironmentVariables(desiredValue))
                {
                    Logger.WriteLog($"\t[!] TermService: non original library path {currentValue}", Logger.warn);
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            string termsrv = "TermService";
                            string UmRdpSrv = "UmRdpService";

                            var UmRdpSrvInfo = ServiceHelper.GetServiceInfo(UmRdpSrv);
                            var termSrvInfo = ServiceHelper.GetServiceInfo(termsrv);

                            if (ServiceHelper.GetServiceState(UmRdpSrv) == ServiceHelper.ServiceState.Running)
                            {
                                ServiceHelper.StopService(UmRdpSrv);
                            }

                            if ((ServiceHelper.ServiceBootFlag)UmRdpSrvInfo.StartType != ServiceHelper.ServiceBootFlag.DemandStart)
                            {
                                ServiceHelper.ChangeStartMode(UmRdpSrv, ServiceHelper.ServiceBootFlag.DemandStart);
                            }

                            if (ServiceHelper.GetServiceState(termsrv) == ServiceHelper.ServiceState.Running)
                            {
                                ServiceHelper.StopService(termsrv);
                            }

                            if ((ServiceHelper.ServiceBootFlag)termSrvInfo.StartType != ServiceHelper.ServiceBootFlag.DemandStart)
                            {
                                ServiceHelper.ChangeStartMode(termsrv, ServiceHelper.ServiceBootFlag.DemandStart);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
                            return;
                        }

                        regkey.SetValue(paramName, desiredValue, RegistryValueKind.ExpandString);
                        currentValue = (string)regkey.GetValue(paramName);
                        if (currentValue == ResolveEnvironmentVariables(desiredValue))
                        {
                            Logger.WriteLog("\t[+] TermService successfully restored", Logger.success);
                        }
                        else
                        {
                            Logger.WriteLog("\t[x] Failed to restore TermService", Logger.error);
                        }
                    }
                    else
                    {
                        Logger.WriteLog("[#] Scan only mode", ConsoleColor.Blue);
                    }
                }
                else
                {
                    Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
                }
            }


        }

        internal static bool CheckDynamicSignature(string filePath, int sequenceLength, int minOccurrences)
        {
            byte[] allBytes = File.ReadAllBytes(filePath);

            Dictionary<string, int> sequenceCounts = new Dictionary<string, int>();

            for (int i = 0; i <= allBytes.Length - sequenceLength; i += sequenceLength)
            {
                byte[] sequenceBytes = new byte[sequenceLength];
                Array.Copy(allBytes, i, sequenceBytes, 0, sequenceLength);

                if (!ContainsOnlyZeros(sequenceBytes))
                {
                    string sequenceHash = BitConverter.ToString(sequenceBytes);

                    if (sequenceCounts.TryGetValue(sequenceHash, out int count))
                    {
                        sequenceCounts[sequenceHash] = count + 1;
                    }
                    else
                    {
                        sequenceCounts[sequenceHash] = 1;
                    }
                }


            }

            if (sequenceCounts.Count <= 0)
            {
                return false;
            }

            int maxOccurrences = sequenceCounts.Values.Max();

            if (maxOccurrences < minOccurrences)
            {
                sequenceCounts.Clear();
                return false;
            }

            string mostCommonSequence = sequenceCounts.FirstOrDefault(x => x.Value == maxOccurrences).Key;
            double shannonEntropy = ShannonEntropy(mostCommonSequence);
            int bytesSum = SequenceSum(mostCommonSequence);

            sequenceCounts.Clear();
            return maxOccurrences >= minOccurrences && shannonEntropy >= 2.5 && shannonEntropy <= 2.9 &&
                   IsInRange(mostCommonSequence, 0x61, 0x7A) && bytesSum >= 1750 && bytesSum <= 1800;

        }

        static bool ContainsOnlyZeros(byte[] sequenceBytes)
        {
            foreach (byte b in sequenceBytes)
            {
                if (b != 0)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsInRange(string strBytes, byte min, byte max)
        {
            string[] bytesFromString = strBytes.Split('-');
            foreach (string byteStr in bytesFromString)
            {
                byte byteValue = Convert.ToByte(byteStr, 16);
                if (byteValue < min || byteValue > max)
                {
                    return false;
                }
            }
            return true;
        }

        static int SequenceSum(string strBytes)
        {
            string[] bytesFromString = strBytes.Split('-');
            int sum = 0;
            foreach (string byteStr in bytesFromString)
            {
                sum += Convert.ToByte(byteStr, 16);
            }
            return sum;
        }

        static double ShannonEntropy(string message)
        {

            double sumnation = 0;
            foreach (var s in message.ToList().Distinct())
            {
                var temp = (message.Count(x => x == s) / (double)(message.Length));
                sumnation += temp * Math.Log(temp, 2.0f);
            }
            return -1.0f * sumnation;
        }

        internal static string CalculateMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    StringBuilder sb = new StringBuilder();

                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                    }

                    return sb.ToString();
                }
            }
        }

        internal static bool IsDigit(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        internal static void CheckStartupCount()
        {
            const string registryKeyPath = @"Software\MinerSearch";
            const string valueName = "runcount";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath, true);
            if (key == null)
            {
                Registry.CurrentUser.CreateSubKey(registryKeyPath).SetValue(valueName, 1);
                Logger.WriteLog("\t\tStartup count: 1", ConsoleColor.White, false);
            }
            else
            {
                int newValue = (int)key.GetValue(valueName, 0) + 1;
                Logger.WriteLog($"\t\tStartup count: {newValue}", ConsoleColor.White, false);
                key.SetValue(valueName, newValue);
            }
        }

        internal static string StringMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        internal static void DeleteTask(TaskService taskService, string taskFolder, string taskName)
        {
            try
            {
                taskService.GetFolder(taskFolder).DeleteTask(taskName);
                Logger.WriteLog($"\t[+] Task {taskName} was deleted", Logger.success);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Cannot delete task {taskFolder}\\{taskName} | {ex.Message}", Logger.error);
            }
        }

        internal static bool IsTaskEmpty(Task task)
        {
            try
            {
                return (uint)task.LastTaskResult == 0x80070002;
            }
            catch
            {
                return false;
            }
        }

        internal static string GetSystemLanguage()
        {

            string registryKeyPath = @"SY~STE~M\Curre~ntCont~rolSet\Co~ntro~l\Nls\Lan~guag~e".Replace("~", "");
            string registryValueName = "Ins~tall~La~ngu~age".Replace("~", "");

            string[] CodeLang = new string[]
            {
                "0409", //English
                "0419", //Русский
            };

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKeyPath))
            {
                if (key != null)
                {
                    var value = key.GetValue(registryValueName) as string;
                    if (value != null)
                    {
                        if (value.Equals(CodeLang[1]))
                        {
                            return "RU";
                        }
                        else if (value.Equals(CodeLang[0]))
                        {
                            return "EN";
                        }

                    }
                    else
                    {
                        return "EN";
                    }
                }
                else
                {
                    return "EN";
                }
            }
            return "EN";
        }

        internal static void InitPrivileges()
        {

            IntPtr token;
            if (Native.OpenProcessToken(Process.GetCurrentProcess().Handle, Native.TOKEN_ADJUST_PRIVILEGES | Native.TOKEN_QUERY, out token))
            {
                try
                {
                    var seSecurityLuid = new Native.LUID();
                    if (Native.LookupPrivilegeValue(null, Native.SE_SECURITY_NAME, out seSecurityLuid))
                    {
                        var seTakeOwnershipLuid = new Native.LUID();
                        if (Native.LookupPrivilegeValue(null, Native.SE_TAKE_OWNERSHIP_NAME, out seTakeOwnershipLuid))
                        {
                            var tokenPrivileges = new Native.TOKEN_PRIVILEGES
                            {
                                PrivilegeCount = 2,
                                Privileges = new Native.LUID_AND_ATTRIBUTES[2]
                                {
                        new Native.LUID_AND_ATTRIBUTES { Luid = seSecurityLuid, Attributes = Native.SE_PRIVILEGE_ENABLED },
                        new Native.LUID_AND_ATTRIBUTES { Luid = seTakeOwnershipLuid, Attributes = Native.SE_PRIVILEGE_ENABLED }
                                }
                            };
                            if (!Native.AdjustTokenPrivileges(token, false, ref tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
                            {
                                Logger.WriteLog("[x] Erro~r InitP~rivil~eges both SeS~ecur~ityPriv~ilege and Se~Tak~eOw~ners~hipPriv~ilege: ".Replace("~", "") + Marshal.GetLastWin32Error(), Logger.error, false);
                            }
                        }
                        else
                        {
                            Logger.WriteLog("[x] Error to lookup Se~Take~Own~ersh~ipPri~vil~ege: ".Replace("~", "") + Marshal.GetLastWin32Error(), Logger.error, false);
                        }
                    }
                    else
                    {
                        Logger.WriteLog("[x] Error to lookup S~eSe~curity~Pr~ivi~lege: ".Replace("~", "") + Marshal.GetLastWin32Error(), Logger.error, false);
                    }
                }
                finally
                {
                    Native.CloseHandle(token);
                }
            }
            else
            {
                Console.WriteLine("Fai~led t~o g~et proc~ess han~dle wit~h ~error~ cod~e: ".Replace("~", "") + Marshal.GetLastWin32Error());
            }
        }

        internal static bool IsStartedFromArchive()
        {
            string currentPath = Process.GetCurrentProcess().MainModule.FileName;

            if (currentPath.ToLower().Contains(@"appdata\local\temp"))
            {
                return true;
            }
            return false;
        }

        internal static string GetServiceImagePath(string serviceName)
        {
            using (var sc = new System.Management.ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                return sc["PathName"].ToString();
            }
        }

        internal static void SetServiceStartType(string serviceName, ServiceStartMode startMode)
        {
            using (var service = new System.Management.ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                var inParams = service.GetMethodParameters("ChangeStartMode");
                inParams["StartMode"] = startMode.ToString();
                service.InvokeMethod("ChangeStartMode", inParams, null);
            }
        }

        internal static ServiceStartMode GetServiceStartType(string serviceName)
        {
            using (var service = new System.Management.ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                return (ServiceStartMode)Enum.Parse(typeof(ServiceStartMode), service["StartMode"].ToString().Replace("Auto", "Automatic"));
            }
        }

        internal static byte[] SerializeObject(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        internal static object DeserializeObject(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(memoryStream);
            }
        }

        internal static void SaveRenamedFileData(object renamedFileInfo)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\MinerSearch\\ProcessData"))
                {
                    byte[] serializedData = SerializeObject(renamedFileInfo);
                    key.SetValue(GetRndString(), serializedData, RegistryValueKind.Binary);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog($"\t[x] Failed to save renamed files info to registry: {e.Message}", Logger.error);
            }
        }

        internal static List<RenamedFileInfo> GetRenamedFilesData()
        {
            List<RenamedFileInfo> result = new List<RenamedFileInfo>();

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\MinerSearch\\ProcessData"))
                {
                    if (key != null)
                    {
                        string[] valueNames = key.GetValueNames();

                        foreach (var valueName in valueNames)
                        {
                            byte[] serializedData = key.GetValue(valueName) as byte[];

                            if (serializedData != null)
                            {
                                RenamedFileInfo fileInfo = (RenamedFileInfo)DeserializeObject(serializedData);
                                result.Add(fileInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog($"\t[x] Failed to read renamed files info from registry: {e.Message}", Logger.error);
            }

            return result;
        }

        internal static void RemoveRenamedFilesData()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\MinerSearch", true))
                {
                    key?.DeleteSubKeyTree("ProcessData", false);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog($"\t[x] Failed to remove renamed files info from registry: {e.Message}", Logger.error);
            }
        }

        internal static bool IsSfxArchive(string path)
        {
            byte[] archiveSignature = { 0x01, 0x53, 0x62, 0x73, 0x22, 0x1B, 0x08, 0x02, 0x01 };

            for (int i = 0; i < archiveSignature.Length; i++)
            {
                archiveSignature[i] -= (byte)1;
            }

            try
            {
                byte[] signatureBytes = new byte[3];
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(signatureBytes, 0, 3);
                }

                if (signatureBytes[0] == 0x4D && signatureBytes[1] == 0x5A && signatureBytes[2] == 0x90)
                {
                    FileInfo fileInfo = new FileInfo(path);
                    long fileLength = fileInfo.Length;
                    byte[] fileBytes = File.ReadAllBytes(path);

                    for (int i = 0; i <= fileLength - archiveSignature.Length; i++)
                    {
                        bool match = true;
                        for (int j = 0; j < archiveSignature.Length; j++)
                        {
                            if (fileBytes[i + j] != archiveSignature[j])
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                            return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        internal static bool IsAccessibleFile(string path)
        {
            try
            {
                using (File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static void ProccedFileFromArgs(string fullpath, string arguments)
        {
            string[] checkDirs =
            {
                Environment.SystemDirectory, // System32
                $@"{Program.drive_letter}:\Wind?ows\Sys?WOW?64".Replace("?", ""), // SysWow64
                $@"{Program.drive_letter}:\W?in?dow?s\Sys?tem?32\wbem".Replace("?",""), // Wbem
                Program.drive_letter + Bfs.Create("vBeCJGSga8uIxsPe+vdoXlkiNkY7KZ+G6TAI4UcNOCkm7Ptqf319nxyC5G887AJD",
                    new byte[] {0xe6,0x40,0xfb,0xf4,0x38,0xa8,0xe5,0xf1,0x56,0xfa,0x42,0xfa,0xcb,0x6c,0x56,0x59,0xe9,0xf4,0x02,0xf4,0xd2,0x60,0x29,0x6b,0x6c,0x84,0x39,0x1f,0x01,0xf5,0x25,0x5b},
                    new byte[] {0x82,0xb9,0x69,0x48,0x69,0xac,0x53,0x6e,0x55,0x7c,0xd2,0xa7,0x41,0x37,0x2f,0x0a}), // PowerShell
            };


            if (fullpath.ToLower().Contains("rundll32"))
            {
                string rundll32Args = ResolveFilePathFromString(arguments).Split(',')[0];

                foreach (string checkDir in checkDirs)
                {
                    string fullPathToFileFromArgs = Path.Combine(checkDir, rundll32Args);
                    if (!fullPathToFileFromArgs.EndsWith(".dll"))
                    {
                        fullPathToFileFromArgs += ".dll";
                    }
                    try
                    {
                        if (File.Exists(fullPathToFileFromArgs))
                        {
                            IntPtr ptr = IntPtr.Zero;

                            Logger.WriteLog($"\t[.] File: {fullPathToFileFromArgs}", ConsoleColor.Gray);
                            var trustResult = WinTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                            if (trustResult != WinVerifyTrustResult.Success)
                            {
                                Logger.WriteLog($"\t[!] Invalid signature file {rundll32Args}", Logger.warn);
                                return;
                            }
                            else if (trustResult == WinVerifyTrustResult.Success)
                            {
                                Logger.WriteLog($"\t[OK]", Logger.success, false);
                            }
                            return;

                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Error {ex.Message}", Logger.error);
                    }
                }


            }

            if (fullpath.ToLower().Contains("pcalua"))
            {
                string pcaluaArgs = ResolveFilePathFromString(arguments.Replace("-a ", ""));

                foreach (string checkDir in checkDirs)
                {
                    string fullPathToFileFromArgs = Path.Combine(checkDir, pcaluaArgs);
                    if (!fullPathToFileFromArgs.EndsWith(".exe"))
                    {
                        fullPathToFileFromArgs += ".exe";
                    }

                    try
                    {
                        if (File.Exists(fullPathToFileFromArgs))
                        {
                            Logger.WriteLog($"\t[.] File: {fullPathToFileFromArgs}", ConsoleColor.Gray);

                            var trustResult = WinTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                            if (WinTrust.VerifyEmbeddedSignature(pcaluaArgs) != WinVerifyTrustResult.Success)
                            {
                                Logger.WriteLog($"\t[!] Invalid signature file {pcaluaArgs}", Logger.warn);
                                return;
                            }
                            else if (trustResult == WinVerifyTrustResult.Success)
                            {
                                Logger.WriteLog($"\t[OK]", Logger.success, false);
                            }
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Error {ex.Message}", Logger.error);
                    }


                }
            }
        }

    }
}
