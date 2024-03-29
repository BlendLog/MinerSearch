﻿
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
    internal enum BootMode
    {
        Normal = 0,
        SafeMinimal = 1,
        SafeNetworking = 2
    }

    internal class Utils
    {
        WinTrust winTrust = new WinTrust();

        static string query = Bfs.Create("AMtoxtzwhhVorHrU4S/HRE5WvrPtVVxIbAjhumQAeX5MPifo52GuIYkhKMLfYGT4XXdcWarMJmFH2FQffcEwMg==", "8ynS5yiEgcgdKBEK5RRgLNlNG3kYG6eE9zkKalcqn14=", "JfqpmZ9I1tTetkz9pwPKSg=="); //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
        static string BitVersionStr = Bfs.Create("ZhsF9lq3O+2hp0sSsvh97kbwK/KWx5RpfitoVCyIhfiu6Omu5UaX0s4AVSZymP/E", "K0HQJVQIXMo1jG7aK6lZlEly2XGdPofFVOMTqRdam50=", "9OTJDTgjwUfbShVAfuHi8Q=="); //HARDWARE\Description\System\CentralProcessor\0

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
                catch (Exception)
                {
                    continue;
                }

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
                new LocalizedLogger().LogErrorMessage("_Error", ex);

            }

        }

        internal void SuspendProcess(int pid)
        {
            LocalizedLogger LL = new LocalizedLogger();
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
                    LL.LogSuccessMessage("_ProcessSuspended", $"{process.ProcessName}, PID: {process.Id}");
                }
                else if (totalThreads > 0)
                {
                    LL.LogWarnMediumMessage("_ProcessSuspendedPartially", $"{process.ProcessName}.exe, PID: {process.Id}");
                }

                process.Close();

            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_Error", ex);
            }
        }

        internal string GetDownloadsPath()
        {
            IntPtr pathPtr = IntPtr.Zero;

            try
            {
                Native.SHGetKnownFolderPath(ref Native.FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_Error", ex);
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
                new LocalizedLogger().LogErrorMessage("_Error", ex);
                
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
                        line = ResolveEnvironmentVariables(line.Replace("\"", ""));
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
            return path.Contains("\\\u00A0\\\u00A0\\");
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
                new LocalizedLogger().LogErrorMessage("_Error", ex);
            }

            return "N/A";
        }

        internal static BootMode GetBootMode()
        {
            return (BootMode)Native.GetSystemMetrics(Native.SM_CLEANBOOT);
        }

        internal static string getBitVersion()
        {
            if (Registry.LocalMachine.OpenSubKey(BitVersionStr).GetValue("Id?enti?fier".Replace("?", "")).ToString().Contains("x86"))
            {
                return "x32";
            }
            else
            {
                return "x64";
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

        internal void CheckWMI()
        {
            LocalizedLogger LL = new LocalizedLogger();
            string serviceName = "wi~nm~gm~t".Replace("~", "");

            try
            {
                if (ServiceHelper.ServiceIsInstalled(serviceName))
                {
                    var serviceinfo = ServiceHelper.GetServiceInfo(serviceName);

                    if ((ServiceHelper.ServiceBootFlag)serviceinfo.StartType != ServiceHelper.ServiceBootFlag.AutoStart)
                    {
                        ServiceHelper.ChangeStartMode(serviceName, ServiceHelper.ServiceBootFlag.AutoStart);
                        LL.LogSuccessMessage("_CriticalServiceStartup");
                    }

                    if (ServiceHelper.GetServiceState(serviceName) != ServiceHelper.ServiceState.Running)
                    {
                        ServiceHelper.StartService(serviceName);
                        LL.LogSuccessMessage("_CriticalServiceRestart");
                    }
                }
                else
                {
                    LocalizedLogger.LogError_СriticalServiceNotInstalled();
                    Console.ReadLine();
                    Environment.Exit(-5);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[xxx] {serviceName}: {ex.Message}", ConsoleColor.DarkRed, false);
                Console.ReadLine();
                Environment.Exit(-5);
            }


        }

        internal void CheckTermService()
        {
            LocalizedLogger LL = new LocalizedLogger();

            string registryPath = Bfs.Create("dWUOPNJnJ6PWbI94u//tbgd2o7muMeChDiZr29LEQq00/vyxHEW0N4YMHV4qKc6zhUSNSEkkp7wz+6iyYsbFZw==", "ygWpRIRpjlo6I742UDP5TjLfbznEpVLwyV/GMZ78rfM=", "LpdrmeFR0+f+2LsRDnXGzw=="); //SYSTEM\CurrentControlSet\Services\TermService\Parameters

            string paramName = Bfs.Create("bG/mP5JC2OiXbf+pElWcaw==", "EZjCrhyK3G4iDS+g2Sv5m6Burr6DsM1HcJHwgHrcDJQ=", "uqrsF78VNxC8pJgYuHHxYw=="); //ServiceDll

            string desiredValue = Bfs.Create("FiaNtKNzmI2pgf9k/cH6tQKDy/Px1V1zmdmxxr29hnDBvw2HoCLoTZVgkw/0TlhE", "4CXNmoPVv3dSJbV+hU5xmU154DtUI04k7uQY66doRmo=", "aqUzX0is5HcVKAaAnyHfDA=="); //%SystemRoot%\System32\termsrv.dll

            using (var regkey = Registry.LocalMachine.OpenSubKey(registryPath, true))
            {
                string currentValue = (string)regkey.GetValue(paramName);
                if (currentValue != ResolveEnvironmentVariables(desiredValue))
                {
                    LL.LogWarnMessage("_TermServiceInvalidPath", currentValue);

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
                            LL.LogErrorMessage("_Error", ex);
                            return;
                        }

                        regkey.SetValue(paramName, desiredValue, RegistryValueKind.ExpandString);
                        currentValue = (string)regkey.GetValue(paramName);
                        if (currentValue == ResolveEnvironmentVariables(desiredValue))
                        {
                            LL.LogSuccessMessage("_TermServiceRestored");
                        }
                        else
                        {
                            LL.LogErrorMessage("_TermServiceFailedRestore", new Exception(""));
                        }
                    }
                    else
                    {
                        LocalizedLogger.LogScanOnlyMode();
                    }
                }
                else
                {
                    LocalizedLogger.LogNoThreatsFound();
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
                LocalizedLogger.LogStartupCount(1);
            }
            else
            {
                int newValue = (int)key.GetValue(valueName, 0) + 1;
                LocalizedLogger.LogStartupCount(newValue);
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

        internal void DeleteTask(TaskService taskService, string taskFolder, string taskName)
        {
            LocalizedLogger LL = new LocalizedLogger();
            try
            {
                taskService.GetFolder(taskFolder).DeleteTask(taskName);
                LL.LogSuccessMessage("_TaskDeleted", taskName);
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_ErrorTaskDeleteFail", ex, taskFolder + "\\" + taskName);
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
                        if (value.Equals(CodeLang[0]))
                        {
                            return "EN";
                        }
                        else if (value.Equals(CodeLang[1]))
                        {
                            return "RU";
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

        internal void InitPrivileges()
        {
            LocalizedLogger LL = new LocalizedLogger();
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
                                LL.LogErrorMessage("_Error", new Exception("Init Privileges"));
                            }
                        }
                        else
                        {
                            LL.LogErrorMessage("_Error", new Exception("Init Privileges"));

                        }
                    }
                    else
                    {
                        LL.LogErrorMessage("_Error", new Exception("Init Privileges"));

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

        internal void SaveRenamedFileData(object renamedFileInfo)
        {
            LocalizedLogger LL = new LocalizedLogger();
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
                LL.LogErrorMessage("_Error", e, "SaveRenamedFilesData");
            }
        }

        internal List<RenamedFileInfo> GetRenamedFilesData()
        {
            List<RenamedFileInfo> result = new List<RenamedFileInfo>();
            LocalizedLogger LL = new LocalizedLogger();

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
                LL.LogErrorMessage("_Error", e, "ReadRenamedFilesData");
            }

            return result;
        }

        internal void RemoveRenamedFilesData()
        {
            LocalizedLogger LL = new LocalizedLogger();
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\MinerSearch", true))
                {
                    key?.DeleteSubKeyTree("ProcessData", false);
                }
            }
            catch (Exception e)
            {
                LL.LogErrorMessage("_Error", e, "RemoveRenamedFilesData");
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

        internal void ProccedFileFromArgs(string fullpath, string arguments)
        {
            LocalizedLogger LL = new LocalizedLogger();
            string[] checkDirs =
            {
                Environment.SystemDirectory, // System32
                $@"{Program.drive_letter}:\Wind?ows\Sys?WOW?64".Replace("?", ""), // SysWow64
                $@"{Program.drive_letter}:\W?in?dow?s\Sys?tem?32\wbem".Replace("?",""), // Wbem
                Program.drive_letter + Bfs.Create("vURBGfwJ3kuF4Nf5qhE0WizxUO+lc9E9bNdtBqD+7fU2h6o6YLUh3g+LtEQR4mmI","YLztxAVwTZbtradu42S8/S8RsViXRanQICaZRT4WuF4=", "3gcV9/AzxICm4AmSwm78sA=="), //:\Windows\System32\WindowsPowerShell\v1.0
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

                            LL.LogMessage("[.]", "_Just_File", fullPathToFileFromArgs, ConsoleColor.Gray);
                            var trustResult = winTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                            if (trustResult != WinVerifyTrustResult.Success)
                            {
                                LL.LogWarnMessage("_InvalidCertificateSignature", rundll32Args);

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
                        LL.LogErrorMessage("_Error", ex);
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
                            LL.LogMessage("[.]", "_Just_File", fullPathToFileFromArgs, ConsoleColor.Gray);


                            var trustResult = winTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                            if (winTrust.VerifyEmbeddedSignature(pcaluaArgs) != WinVerifyTrustResult.Success)
                            {
                                LL.LogWarnMessage("_InvalidCertificateSignature", pcaluaArgs);

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
                        LL.LogErrorMessage("_Error", ex);
                    }


                }
            }
        }

        public static Mutex mutex = new Mutex(false, "{e8cc8d71-bdb3-42cf-bcc0-c6c5fd8cdc1a}");
        internal static bool IsOneAppCopy() => mutex.WaitOne(0, true);

        internal static bool RegistryKeyExists(string path)
        {
            return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(path) == null;
        }
        internal List<string> GetSubkeys(string parentKeyPath)
        {
            List<string> subkeys = new List<string>();

            try
            {
                using (RegistryKey parentKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(parentKeyPath))
                {
                    if (parentKey != null)
                    {
                        foreach (var subkeyName in parentKey.GetSubKeyNames())
                        {
                            subkeys.Add(subkeyName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_Error", ex, "GetSubkeys");
            }

            return subkeys;
        }

        internal static bool SwitchMouseSelection(bool enable = false)
        {
            IntPtr consoleHandle = Native.GetStdHandle(Native.STD_INPUT_HANDLE);
            if (Native.GetConsoleMode(consoleHandle, out uint mode))
            {
                if (!enable)
                {
                    mode &= ~Native.ENABLE_QUICK_EDIT_MODE;
                }
                else
                {
                    mode |= Native.ENABLE_QUICK_EDIT_MODE;
                }

                if (Native.SetConsoleMode(consoleHandle, mode))
                {
                    return true;
                }
            }
            return false;
        }

        internal static void AddToQuarantine(string sourceFilePath, string encryptedFilePath, byte[] key)
        {
            byte[] fileBytes = File.ReadAllBytes(sourceFilePath);

            for (int i = 0; i < fileBytes.Length; i++)
            {
                fileBytes[i] ^= key[i % key.Length];
            }

            byte[] markerBytes = Encoding.UTF8.GetBytes("!mschqur!");
            byte[] encryptedFileBytes = new byte[markerBytes.Length + fileBytes.Length];

            Array.Copy(markerBytes, 0, encryptedFileBytes, 0, markerBytes.Length);
            Array.Copy(fileBytes, 0, encryptedFileBytes, markerBytes.Length, fileBytes.Length);

            File.WriteAllBytes(encryptedFilePath, encryptedFileBytes);
        }

        internal static void RestoreFromQuarantine(string encryptedFilePath, string restoredFilePath, byte[] key)
        {
            byte[] encryptedFileBytes = File.ReadAllBytes(encryptedFilePath);

            string marker = "!mschqur!";
            byte[] markerBytes = Encoding.UTF8.GetBytes(marker);

            if (IsMarkerPresent(encryptedFileBytes, markerBytes))
            {
                byte[] originalFileBytes = new byte[encryptedFileBytes.Length - markerBytes.Length];
                Array.Copy(encryptedFileBytes, markerBytes.Length, originalFileBytes, 0, originalFileBytes.Length);

                for (int i = 0; i < originalFileBytes.Length; i++)
                {
                    originalFileBytes[i] ^= key[i % key.Length];
                }

                File.WriteAllBytes(restoredFilePath, originalFileBytes);


                LocalizedLogger.LogRestoredFile(restoredFilePath);
            }
            else
            {
                LocalizedLogger.LogInvalidFile(restoredFilePath);

            }

        }

        static bool IsMarkerPresent(byte[] source, byte[] marker)
        {
            if (source.Length < marker.Length)
            {
                return false;
            }

            for (int i = 0; i < marker.Length; i++)
            {
                if (source[i] != marker[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetProcessOwner(int processId)
        {
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("Select * From Win32_Process Where ProcessID = " + processId.ToString()).Get())
            {
                string[] args = new string[2]
                {
          string.Empty,
          string.Empty
                };
                if (Convert.ToInt32(managementObject.InvokeMethod("GetOwner", (object[])args)) == 0)
                    return args[1] + "\\" + args[0];
            }
            return "N/A";
        }

        internal void CreateSignatureRestrictedProcess(string path)
        {
            LocalizedLogger LL = new LocalizedLogger();
            var lpSize = IntPtr.Zero;
            var success = Native.InitializeProcThreadAttributeList(IntPtr.Zero, 2, 0, ref lpSize);
            var pInfo = new Native.PROCESS_INFORMATION();
            var siEx = new Native.STARTUPINFOEX();
            siEx.lpAttributeList = Marshal.AllocHGlobal(lpSize);
            success = Native.InitializeProcThreadAttributeList(siEx.lpAttributeList, 2, 0, ref lpSize);
            IntPtr lpMitigationPolicy = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteInt64(lpMitigationPolicy, Native.PROCESS_CREATION_MITIGATION_POLICY_BLOCK_NON_MICROSOFT_BINARIES_ALWAYS_ON);

            success = Native.UpdateProcThreadAttribute(
                siEx.lpAttributeList,
                0,
                (IntPtr)Native.PROC_THREAD_ATTRIBUTE_MITIGATION_POLICY,
                lpMitigationPolicy,
                (IntPtr)IntPtr.Size,
                IntPtr.Zero,
                IntPtr.Zero);
            if (!success)
            {
                LL.LogErrorMessage("_ErrorSetMitigationPolicy", new Exception("Mitigation Policy"));
            }
            var ps = new Native.SECURITY_ATTRIBUTES();
            var ts = new Native.SECURITY_ATTRIBUTES();
            ps.nLength = Marshal.SizeOf(ps);
            ts.nLength = Marshal.SizeOf(ts);
            bool ret = Native.CreateProcess(null, path, ref ps, ref ts, true, Native.EXTENDED_STARTUPINFO_PRESENT | Native.CREATE_NEW_CONSOLE, IntPtr.Zero, null, ref siEx, out pInfo);
            if (!ret)
            {
                LL.LogErrorMessage("_ProcessFailedExecute", new Exception("Failed Execute"));

            }
            Native.CloseHandle(pInfo.hProcess);
        }

        public static Icon BitmapToIcon(Bitmap bmp)
        {
            IntPtr hIcon = bmp.GetHicon();
            return Icon.FromHandle(hIcon);
        }

        public static void SetConsoleWindowIcon(Bitmap bitmap)
        {
            Icon icon = BitmapToIcon(bitmap);
            IntPtr mwHandle = Process.GetCurrentProcess().MainWindowHandle;
            Native.SendMessage(mwHandle, Native.WM_SETICON, IntPtr.Zero, icon.Handle);
        }

        public static Image GetSmallWindowIcon(IntPtr hWnd)
        {
            try
            {
                IntPtr hIcon = default;

                hIcon = Native.SendMessage(hWnd, Native.WM_GETICON, Native.ICON_SMALL2, IntPtr.Zero);

                if (hIcon == IntPtr.Zero)
                    hIcon = Native.GetClassLongPtr(hWnd, Native.GCL_HICON);

                if (hIcon == IntPtr.Zero)
                    hIcon = Native.LoadIcon(IntPtr.Zero, Native.IDI_APPLICATION);

                if (hIcon != IntPtr.Zero)
                    return new Bitmap(Icon.FromHandle(hIcon).ToBitmap(), 16, 16);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
