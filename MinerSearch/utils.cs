using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MinerSearch
{
    public static class utils
    {
        public class Connection
        {
            public int RemotePort { get; set; }
            public int ProcessId { get; set; }

            public override string ToString()
            {
                return "TCP Connection - Process ID: " + ProcessId + ", Port: " + RemotePort;
            }
        }

        public static string GetCommandLine(Process process)
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

        public static List<Connection> GetConnections()
        {
            List<Connection> Connections = new List<Connection>();

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo
                    {
                        StandardErrorEncoding = Encoding.GetEncoding("CP866"),
                        Arguments = "-a -n -o",
                        FileName = "netstat.exe",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,

                    };

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        Logger.WriteLog($"\t[x] Failed to read TCP connections\n{stdError.ReadToEnd()}", Logger.error);
                        return Connections;
                    }

                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        if (String.IsNullOrEmpty(row))
                            continue;

                        if (row.Contains("0.0.0.0") || row.Contains("127.0.0.1") || row.StartsWith("[::") || row.Contains("::"))
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
                    stdOutput.Close();
                    stdError.Close();
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

        public static string Sizer(long CountBytes)
        {
            string[] type = { "B", "KB", "MB", "GB" };
            if (CountBytes == 0)
                return $"0 {type[0]}";

            double bytes = Math.Abs(CountBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(CountBytes) * num} {type[place]}";
        }

        public static List<Process> GetProcesses()
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

        public static uint GetProcessIdByFilePath(string filePath)
        {
            WinApi.PROCESSENTRY32 processEntry = new WinApi.PROCESSENTRY32();
            processEntry.dwSize = (uint)Marshal.SizeOf(typeof(WinApi.PROCESSENTRY32));

            IntPtr snapshotHandle = WinApi.CreateToolhelp32Snapshot(WinApi.TH32CS_SNAPPROCESS, 0);

            if (WinApi.Process32First(snapshotHandle, ref processEntry))
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
                                WinApi.CloseHandle(snapshotHandle);
                                return processEntry.th32ProcessID;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore any exceptions caused by accessing the process modules.
                    }
                } while (WinApi.Process32Next(snapshotHandle, ref processEntry));
            }

            WinApi.CloseHandle(snapshotHandle);
            return 0;
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

        public static void UnProtect(int[] pids)
        {
            Process.EnterDebugMode();
            try
            {

                foreach (int pid in pids)
                {
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;

                    IntPtr handle = WinApi.OpenProcess(0x001F0FFF, false, pid);
                    WinApi.NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
                    WinApi.CloseHandle(handle);
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

        public static void SuspendProcess(int pid)
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
                    else
                    {
                        WinApi.SuspendThread(pOpenThread);
                        WinApi.CloseHandle(pOpenThread);
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

        public static string GetDownloadsPath()
        {
            IntPtr pathPtr = IntPtr.Zero;

            try
            {
                WinApi.SHGetKnownFolderPath(ref WinApi.FolderDownloads, 0, IntPtr.Zero, out pathPtr);
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

        public static string ResolveEnvironmentVariables(string path)
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

        public static string GetFilePathFromRegistry(RegistryKey key, string keyName)
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

        public static string GetHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(8);
        }

        public static bool IsDirectoryEmpty(string path)
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

        public static bool BinarySearchByteSequenceInFile(string file, byte[] fileBytes, List<byte[]> targetSequences)
        {
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
                            if (buffer[i + j] != targetSequence[j])
                            {
                                sequenceFound = false;
                                break;
                            }
                        }

                        if (sequenceFound)
                        {
                            Logger.WriteLog($"Signature match: {file}", Logger.caution);
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

        public static List<string> GetFiles(string path, string pattern, int currentDepth = 0, int maxDepth = 2)
        {
            var files = new List<string>();

            try
            {
                if (currentDepth <= maxDepth)
                {
                    files.AddRange(Directory.EnumerateFiles(path, pattern, SearchOption.TopDirectoryOnly));
                    foreach (var directory in Directory.GetDirectories(path))
                    {
                        if (!directory.StartsWith("C:\\Windows\\WinSxS"))
                        {
                            files.AddRange(GetFiles(directory, pattern, currentDepth + 1, maxDepth));
                        }
                    }
                }
            }
            catch (Exception) { }

            return files;
        }

        public static string GetWindowsVersion()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    string productName = key.GetValue("ProductName") as string;
                    string displayName = key.GetValue("DisplayVersion") as string;
                    if (!string.IsNullOrEmpty(displayName))
                    {
                        return $"{productName} {displayName}";
                    }
                    else
                    {
                        return $"{productName}";
                    }
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public static string getBitVersion()
        {
            if (Registry.LocalMachine.OpenSubKey(@"HARDWARE\Description\System\CentralProcessor\0").GetValue("Identifier").ToString().Contains("x86"))
            {
                return "(32 Bit)";
            }
            else
            {
                return "(64 Bit)";
            }
        }

        public static List<byte[]> RestoreSignatures(List<byte[]> signatures)
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

        public static bool CheckUserExists(string username)
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

        public static void DeleteUser(string username)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "net",
                Arguments = $"user /delete {username}",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        public static void CheckWMI()
        {
            string serviceName = "winmgmt";

            try
            {
                IntPtr scm = WinApi.OpenSCManager(null, null, WinApi.SC_MANAGER_CONNECT | WinApi.SC_MANAGER_CREATE_SERVICE);
                if (scm != IntPtr.Zero)
                {
                    IntPtr service = WinApi.OpenService(scm, serviceName, WinApi.SERVICE_QUERY_CONFIG | WinApi.SERVICE_CHANGE_CONFIG | WinApi.SERVICE_START);
                    if (service != IntPtr.Zero)
                    {
                        WinApi.QueryServiceConfig(service, IntPtr.Zero, 0, out int bytesNeeded);

                        IntPtr ptr = Marshal.AllocHGlobal(bytesNeeded);
                        if (WinApi.QueryServiceConfig(service, ptr, bytesNeeded, out bytesNeeded))
                        {
                            WinApi.SERVICE_CONFIG serviceConfig = (WinApi.SERVICE_CONFIG)Marshal.PtrToStructure(ptr, typeof(WinApi.SERVICE_CONFIG));

                            if (serviceConfig.dwStartType != WinApi.SERVICE_AUTO_START)
                            {
                                if (WinApi.ChangeServiceConfig(service, WinApi.SERVICE_NO_CHANGE, WinApi.SERVICE_AUTO_START, WinApi.SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null))
                                {
                                    Logger.WriteLog($"\t[+] Startup type of the critical service has been restored", Logger.success, false);
                                }
                                else
                                {
                                    Logger.WriteLog($"\t[x] Error change startup type for WMI service", Logger.error);
                                }
                            }

                            if (serviceConfig.dwCurrentState != WinApi.SERVICE_RUNNING)
                            {
                                WinApi.StartService(service, 0, null);
                                Thread.Sleep(999);
                                if (serviceConfig.dwCurrentState == WinApi.SERVICE_RUNNING)
                                {
                                    Logger.WriteLog("\t[+] Critical service has been restarted", Logger.success);
                                }
                            }
                        }

                        Marshal.FreeHGlobal(ptr);
                        WinApi.CloseServiceHandle(service);
                    }
                    else
                    {
                        Logger.WriteLog($"\t[xxx] WMI Service is not found!", ConsoleColor.DarkRed, false);
                    }

                    WinApi.CloseServiceHandle(scm);
                    Application.Exit();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadLine();
            }


        }
    }
}
