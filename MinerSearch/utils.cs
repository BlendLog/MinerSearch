
using DBase;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MSearch
{
    internal enum BootMode
    {
        Normal = 0,
        SafeMinimal = 1,
        SafeNetworking = 2
    }

    public static class ExeptionHandler
    {
        public static void HookExeption(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.Message + "\n" + e.StackTrace, Program.LL.GetLocalizedString("_ExeptionTitle"));
        }
    }

    public class Utils
    {
        WinTrust winTrust = new WinTrust();
        static MSData msData = new MSData();

        static string batchSig = msData.queries[12]; //Add-MpPreference -ExclusionPath

        [Serializable]
        public class RenamedFileInfo
        {
            public int _ProcessId { get; set; }
            public string _NewFilePath { get; set; }
        }

        internal static string GetCommandLine(Process process)
        {

            string cmdLine = null;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(msData.queries[10] + process.Id))
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
                    Program.LL.LogSuccessMessage("_ProcessSuspended", $"{process.ProcessName}, PID: {process.Id}");
                }
                else if (totalThreads > 0)
                {
                    Program.LL.LogWarnMediumMessage("_ProcessSuspendedPartially", $"{process.ProcessName}.exe, PID: {process.Id}");
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

                    if (line.Contains($"{Program.drive_letter}:/"))
                    {
                        line = line.Replace("/", "\\");
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

            var files = new DirectoryInfo(path).GetFiles("*", SearchOption.TopDirectoryOnly);
            var subdirectories = new DirectoryInfo(path).GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
                return false;

            foreach (var subdirectory in subdirectories)
            {
                if (!IsDirectoryEmpty(subdirectory.FullName))
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
                    foreach (var directory in Directory.EnumerateDirectories(path))
                    {
                        if (!IsReparsePoint(directory) &&
                            !directory.Contains(":\\Windows\\WinSxS") &&
                            !directory.Contains(":\\$") &&
                            !directory.Contains("mi?ner?se?arch_quarantine".Replace("?", "")) &&
                            !directory.Contains(@":\programdata\microsoft\Windows\Containers\BaseImages") &&
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

        internal static bool IsReparsePoint(string path)
        {
            return (Native.GetFileAttributes(path) & Native.FILE_ATTRIBUTE_REPARSE_POINT) == Native.FILE_ATTRIBUTE_REPARSE_POINT;
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
                            string WinVerion = $"{version.ToString().Replace('_', ' ')} {osInfo.dwBuildNumber} {GetPlatform()}";
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

        internal static string GetPlatform()
        {
            Native.SYSTEM_INFO lpSystemInfo = new Native.SYSTEM_INFO();
            Native.GetNativeSystemInfo(ref lpSystemInfo);
            switch (lpSystemInfo.wProcessorArchitecture)
            {
                case 0:
                    return "(x86)";

                case 9:
                    return "(x64)";
            }
            return "";
        }

        internal static List<byte[]> RestoreSignatures(List<byte[]> signatures)
        {
            foreach (var sig in signatures)
            {
                for (int i = 0; i < sig.Length; i++)
                {
                    if (i / 2 == 0)
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
#if DEBUG
            var users = GetUsers();
            foreach (var user in users)
            {
                Logger.WriteLog(user, ConsoleColor.DarkGray, false);
            }
#endif
            return GetUsers().Contains(username);
        }

        internal static List<string> GetUsers()
        {
            const int MAX_PREFERRED_LENGTH = -1;
            const int NERR_Success = 0;
            IntPtr bufPtr;
            int entriesRead, totalEntries;
            int resumeHandle = 0;

            List<string> users = new List<string>();

            int result = Native.NetUserEnum(null, 0, 2, out bufPtr, MAX_PREFERRED_LENGTH, out entriesRead, out totalEntries, ref resumeHandle);

            if (result == NERR_Success)
            {
                IntPtr current = bufPtr;

                for (int i = 0; i < entriesRead; i++)
                {
                    Native.USER_INFO_0 userInfo = (Native.USER_INFO_0)Marshal.PtrToStructure(current, typeof(Native.USER_INFO_0));
                    users.Add(userInfo.Username);
                    current += Marshal.SizeOf(typeof(Native.USER_INFO_0));
                }

                Native.NetApiBufferFree(bufPtr);
            }

            return users;
        }

        internal static void DeleteUser(string userName)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Machine);
            UserPrincipal usrp = new UserPrincipal(ctx);
            usrp.Name = userName;
            PrincipalSearcher ps_usr = new PrincipalSearcher(usrp);
            var user = ps_usr.FindOne();
            user.Delete();
        }

        internal void CheckWMI()
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
                        Program.LL.LogSuccessMessage("_CriticalServiceStartup");
                    }

                    if (ServiceHelper.GetServiceState(serviceName) != ServiceHelper.ServiceState.Running)
                    {
                        ServiceHelper.StartService(serviceName);
                        Program.LL.LogSuccessMessage("_CriticalServiceRestart");
                    }

                    try
                    {
                        GetProcessOwner(Process.GetProcessesByName("explorer")[0].Id);
                        GetServiceImagePath("Dhcp");
                    }
                    catch (ManagementException me)
                    {
                        if (me.ErrorCode == ManagementStatus.InvalidClass)
                        {
                            Utils.RestoreWMICorruption();
                        }
                        throw me;
                    }

                }
                else
                {
                    LocalizedLogger.LogError_СriticalServiceNotInstalled();
                    Console.ReadLine();
                    Environment.Exit(-5);
                }

            }
            catch (MissingMethodException)
            {
                if (!Utils.IsDotNetInstalled())
                {
                    MessageBox.Show(Program.LL.GetLocalizedString("_ErrorNoDotNet"), GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[xxx] {serviceName}: {ex.Message}", ConsoleColor.DarkRed, false);
                Console.ReadLine();
                Environment.Exit(-5);
            }


        }

        internal static void RestoreWMICorruption()
        {
            if (Program.RestoredWMI)
            {
                throw new Exception("WMI_Corruption");
            }

            Program.LL.LogMessage("\t\t[xxx]", "_WMICorruption", "", ConsoleColor.Red, false);

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Program.LL.LogHeadMessage("_WMIRecompilation");

            string wbemPath = Path.Combine(Environment.SystemDirectory, "Wbem");

            List<string> filteredMof = Directory
                    .EnumerateFiles(wbemPath, "*.*", SearchOption.AllDirectories)
                    .Where(file => new FileInfo(file).Extension.Equals(".mof") || new FileInfo(file).Extension.Equals(".mfl"))
                    .ToList();

            foreach (var file in filteredMof)
            {
                if (File.Exists(file))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "mofcomp.exe",
                        Arguments = $"\"{file}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }).WaitForExit();
                }
            }

            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
            Program.LL.LogHeadMessage("_WMIRegister");

            foreach (var file in Directory.GetFiles(wbemPath, "*.dll", SearchOption.AllDirectories))
            {
                if (File.Exists(file))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "regsvr32.exe",
                        Arguments = $"-s \"{file}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }).WaitForExit();
                }
            }


            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
            Program.LL.LogHeadMessage("_WMIRestartService");

            ServiceController service = new ServiceController("winmgmt");
            if (service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            Thread.Sleep(3000);
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running);


            Logger.WriteLog($"\t\t[OK]", Logger.success, false, true);
            Program.LL.LogHeadMessage("_WMIRestartApplication");

            Thread.Sleep(2000);
            mutex.ReleaseMutex();
            Process.Start(Application.ExecutablePath, "-R");
            Environment.Exit(0);
        }

        internal void CheckTermService()
        {
            string registryPath = msData.queries[13]; //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            string desiredValue = msData.queries[14]; //%SystemRoot%\System32\termsrv.dll

            string paramName = "Ser/vice/Dll".Replace("/", "");

            using (var regkey = Registry.LocalMachine.OpenSubKey(registryPath, true))
            {
                if (regkey != null)
                {
                    string currentValue = (string)regkey.GetValue(paramName);
                    if (currentValue != null)
                    {
                        if (currentValue != ResolveEnvironmentVariables(desiredValue))
                        {
                            Program.LL.LogWarnMessage("_TermServiceInvalidPath", currentValue);
                            Program.totalFoundThreats++;


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
                                    Program.LL.LogErrorMessage("_Error", ex);
                                    return;
                                }

                                regkey.SetValue(paramName, desiredValue, RegistryValueKind.ExpandString);
                                currentValue = (string)regkey.GetValue(paramName);
                                if (currentValue == ResolveEnvironmentVariables(desiredValue))
                                {
                                    Program.LL.LogSuccessMessage("_TermServiceRestored");

                                }
                                else
                                {
                                    Program.LL.LogErrorMessage("_TermServiceFailedRestore", new Exception(""));
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
                else
                {
                    Program.LL.LogWarnMediumMessage("_ServiceNotInstalled", "T?ermS?ervi?ce".Replace("?", ""));
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

        internal static bool CheckDynamicSignature(string filePath, int offset, byte[] startSequence, byte[] endSequence)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                byte[] sequenceInRange = new byte[16];
                Array.Copy(buffer, offset, sequenceInRange, 0, sequenceInRange.Length);

                bool isStartSequence = true;
                for (int i = 0; i < startSequence.Length; i++)
                {
                    if (sequenceInRange[i] != startSequence[i])
                    {
                        isStartSequence = false;
                        break;
                    }
                }

                bool isEndSequence = true;
                for (int i = endSequence.Length - 1; i >= 0; i--)
                {
                    if (sequenceInRange[sequenceInRange.Length - endSequence.Length + i] != endSequence[i])
                    {
                        isEndSequence = false;
                        break;
                    }
                }


                return isStartSequence && isEndSequence;
            }
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
                try
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
                catch (IOException)
                {
                    return "N/A";
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
            const string registryKeyPath = @"Software\M1nerSearch";
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
            try
            {
                taskService.GetFolder(taskFolder).DeleteTask(taskName);
                Program.LL.LogSuccessMessage("_TaskDeleted", taskName);
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_ErrorTaskDeleteFail", ex, taskFolder + "\\" + taskName);
            }
        }

        public static string GetDeviceId()
        {
            string path = Path.Combine(Registry.LocalMachine.Name, @"SOFTWARE\Microsoft\SQMClient");
            var machineIdValue = Registry.GetValue(path, "MachineId", null);
            if (machineIdValue == null)
            {
                return "N/A";
            }

            Guid MachineId = new Guid((string)machineIdValue);
            return MachineId.ToString();
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

            string registryKeyPath = @"SYS~TEM~\Cu~rrentC~ontrol~Set\Co~n~tro~l\Nls\La~ngu~ag~e".Replace("~", "");
            string registryValueName = "Ins~tall~~Language".Replace("~", "");

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
            IntPtr hToken;
            Native.TOKEN_PRIVILEGES tkpPrivileges = new Native.TOKEN_PRIVILEGES
            {
                PrivilegeCount = 5,
                Privileges = new Native.LUID_AND_ATTRIBUTES[5]
            };

            string[] privilegeNames = new string[]
            {
            "SeDebugPrivilege",
            "SeBackupPrivilege",
            "SeRestorePrivilege",
            "SeTakeOwnershipPrivilege",
            "SeSecurityPrivilege"
            };

            if (!Native.OpenProcessToken(Native.GetCurrentProcess(), Native.TOKEN_ADJUST_PRIVILEGES | Native.TOKEN_QUERY, out hToken))
            {
                Program.LL.LogErrorMessage("_Error", new Exception("OpenProcessToken: Current process"));
                return;
            }

            for (int i = 0; i < privilegeNames.Length; i++)
            {
                if (!Native.LookupPrivilegeValue(null, privilegeNames[i], out Native.LUID luid))
                {
                    Program.LL.LogErrorMessage("_Error", new Exception("LookupPrivilegeValue()"));
                    Native.CloseHandle(hToken);
                    return;
                }
                else
                {
                    tkpPrivileges.Privileges[i].Luid = luid;
                    tkpPrivileges.Privileges[i].Attributes = Native.SE_PRIVILEGE_ENABLED;
                }
            }

            if (!Native.AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
            {
                Program.LL.LogErrorMessage("_Error", new Exception("AdjustTokenPrivileges: Privileges are not available"));
            }

            Native.CloseHandle(hToken);
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

            using (var sc = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                string sPath = sc["PathName"]?.ToString();

                if (sPath == null || sPath.Equals(""))
                {
                    return "";
                }
                return sPath;
            }
        }

        internal static void SetServiceStartType(string serviceName, ServiceStartMode startMode)
        {
            using (var service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
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
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\M1nerSearch\\ProcessData"))
                {
                    byte[] serializedData = SerializeObject(renamedFileInfo);
                    key.SetValue(GetRndString(), serializedData, RegistryValueKind.Binary);
                }
            }
            catch (Exception e)
            {
                Program.LL.LogErrorMessage("_Error", e, "SaveRenamedFilesData");
            }
        }

        internal List<RenamedFileInfo> GetRenamedFilesData()
        {
            List<RenamedFileInfo> result = new List<RenamedFileInfo>();
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\M1nerSearch\\ProcessData"))
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
                Program.LL.LogErrorMessage("_Error", e, "ReadRenamedFilesData");
            }

            return result;
        }

        internal void RemoveRenamedFilesData()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\M1nerSearch", true))
                {
                    key?.DeleteSubKeyTree("ProcessData", false);
                }
            }
            catch (Exception e)
            {
                Program.LL.LogErrorMessage("_Error", e, "RemoveRenamedFilesData");
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

        internal bool IsBatchFileBad(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.IndexOf(batchSig, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LocalizedLogger().LogErrorMessage("_ErrorAnalyzingFile", ex, filePath);
            }

            return false;
        }

        internal void ProccedFileFromArgs(string fullpath, string arguments)
        {
            string[] checkDirs =
            {
                Environment.SystemDirectory, // System32
                $@"{Program.drive_letter}:\Wind?ows\Sys?WOW?64".Replace("?", ""), // SysWow64
                $@"{Program.drive_letter}:\W?in?dow?s\Sys?tem?32\wbem".Replace("?",""), // Wbem
                msData.queries[9],
            };


            if (fullpath.ToLower().Contains("rundll32"))
            {
                string rundll32Args = ResolveFilePathFromString(arguments).Split(',')[0];

                string fullPathToFileFromArgs = "";
                if (!rundll32Args.EndsWith("\""))
                {
                    foreach (string checkDir in checkDirs)
                    {
                        fullPathToFileFromArgs = Path.Combine(checkDir, rundll32Args);
                        if (!fullPathToFileFromArgs.EndsWith(".dll"))
                        {
                            fullPathToFileFromArgs += ".dll";
                        }
                    }
                }
                else
                {
                    fullPathToFileFromArgs = rundll32Args.Replace("\"", "");
                }


                try
                {
                    if (File.Exists(fullPathToFileFromArgs))
                    {
                        IntPtr ptr = IntPtr.Zero;

                        Program.LL.LogMessage("[.]", "_Just_File", fullPathToFileFromArgs, ConsoleColor.Gray);
                        var trustResult = winTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                        if (trustResult != WinVerifyTrustResult.Success)
                        {
                            Program.LL.LogWarnMediumMessage("_InvalidCertificateSignature", rundll32Args);
                            AddToQuarantine(fullPathToFileFromArgs, Path.Combine(MinerSearch.quarantineFolder, Path.GetFileName(fullPathToFileFromArgs) + $"_{Utils.CalculateMD5(fullPathToFileFromArgs)}.bak"), Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", "")));
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
                    Program.LL.LogErrorMessage("_Error", ex);
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
                            Program.LL.LogMessage("[.]", "_Just_File", fullPathToFileFromArgs, ConsoleColor.Gray);


                            var trustResult = winTrust.VerifyEmbeddedSignature(fullPathToFileFromArgs);
                            if (winTrust.VerifyEmbeddedSignature(pcaluaArgs) != WinVerifyTrustResult.Success)
                            {
                                Program.LL.LogWarnMediumMessage("_InvalidCertificateSignature", pcaluaArgs);
                                AddToQuarantine(fullPathToFileFromArgs, Path.Combine(MinerSearch.quarantineFolder, Path.GetFileName(fullPathToFileFromArgs) + $"_{Utils.CalculateMD5(fullPathToFileFromArgs)}.bak"), Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", "")));
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
                        Program.LL.LogErrorMessage("_Error", ex);
                    }


                }
            }
        }

        public static Mutex mutex = new Mutex(false, "9c5d03a2-b3e5-4b28-a1f6-eafd9b0ed091");
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
            if (File.Exists(sourceFilePath))
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

                try
                {
                    File.Delete(sourceFilePath);
                    if (!File.Exists(sourceFilePath))
                    {
                        Program.LL.LogSuccessMessage("_Malici0usFile", sourceFilePath, "_MovedToQuarantine");
                    }
                }
                catch (Exception ex)
                {
                    Program.LL.LogErrorMessage("_ErrorCannotRemove", ex, sourceFilePath, "_File");
                }
            }
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


            foreach (ManagementObject managementObject in new ManagementObjectSearcher(msData.queries[11] + processId.ToString()).Get())
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

        internal static bool IsDotNetInstalled()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    int releaseKey = (int)ndpKey.GetValue("Release");
                    return releaseKey >= 378389;
                }
                else
                {
                    return false;
                }
            }
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

        public static string GetLongPath(string path)
        {
            // Check if the path already contains the long path prefix
            if (path.StartsWith(@"\\?\"))
            {
                return path;
            }

            // Prepend the long path prefix
            return @"\\?\" + path;
        }
        public static bool ForceDeleteDirectory(string path)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c takeown /f \"\\\\?\\{path}\" /R /A & icacls \"\\\\?\\{path}\" /remove:d /T & icacls \"\\\\?\\{path}\" /grant *S-1-5-32-544:F & rd /s /q \"\\\\?\\{path}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processStartInfo))
            {
                process.WaitForExit();
            }

            return Directory.Exists(path);
        }

        internal bool GetR77Processes(ref Native.R77_PROCESS[] r77Processes, ref uint count)
        {
            bool result = true;
            uint actualCount = 0;

            uint[] processes = new uint[Native.MaxProcesses];
            int processCount = 0;
            IntPtr[] modules = new IntPtr[Native.MaxModules];
            uint moduleCount = 0;
            byte[] moduleBytes = new byte[512];

            if (Native.EnumProcesses(processes, processes.Length * sizeof(uint), out processCount))
            {
                processCount /= sizeof(uint);

                for (int i = 0; i < processCount; i++)
                {
                    IntPtr process = Native.OpenProcess(0x0410 /* PROCESS_QUERY_INFORMATION | PROCESS_VM_READ */, false, (int)processes[i]);
                    if (process != IntPtr.Zero)
                    {
                        if (Native.EnumProcessModulesEx(process, modules, (uint)modules.Length * (uint)IntPtr.Size, out moduleCount, 0x03 /* LIST_MODULES_ALL */))
                        {
                            moduleCount /= (uint)IntPtr.Size;

                            for (uint j = 0; j < moduleCount; j++)
                            {
                                if (Native.ReadProcessMemory(process, modules[j], moduleBytes, moduleBytes.Length, IntPtr.Zero))
                                {
                                    ushort signature = BitConverter.ToUInt16(moduleBytes, 0x40);
                                    if (signature == Native.R77_SIGNATURE || signature == Native.R77_SERVICE_SIGNATURE || signature == Native.R77_HELPER_SIGNATURE)
                                    {
                                        if (actualCount < count)
                                        {
                                            r77Processes[actualCount].ProcessId = processes[i];
                                            r77Processes[actualCount].Signature = signature;
                                            r77Processes[actualCount++].DetachAddress = (signature == Native.R77_SIGNATURE) ? BitConverter.ToUInt64(moduleBytes, 0x40 + 2) : 0;
                                        }
                                        else
                                        {
                                            result = false;
                                        }

                                        break;
                                    }
                                }
                            }
                        }

                        Native.CloseHandle(process);
                    }
                }
            }

            count = actualCount;
            return result;
        }

        internal bool DetachInjectedProcess(ref Native.R77_PROCESS r77Process)
        {
            bool result = false;
            IntPtr process = IntPtr.Zero;
            if (r77Process.Signature == Native.R77_SIGNATURE)
            {
                process = Native.OpenProcess(0x1F0FFF /* PROCESS_ALL_ACCESS */, false, (int)r77Process.ProcessId);
                if (process != IntPtr.Zero)
                {
                    IntPtr thread = IntPtr.Zero;
                    int status = Native.NtCreateThreadEx(
                        out thread,
                        0x1FFFFF, /* Desired Access */
                        IntPtr.Zero, /* Object Attributes */
                        process, /* Process Handle */
                        new IntPtr((long)r77Process.DetachAddress), /* Start Address */
                        IntPtr.Zero, /* Parameter */
                        false, /* Create Suspended */
                        0, /* Stack Zero Bits */
                        0, /* Size Of Stack */
                        0, /* Maximum Stack Size */
                        IntPtr.Zero /* Attribute List */
                    );

                    if (status >= 0 && thread != IntPtr.Zero)
                    {
#if DEBUG
                        Console.WriteLine($"\tDetach process pid: {r77Process.ProcessId}");
#endif 
                        result = true;
                        Native.CloseHandle(thread);
                    }

                    Native.CloseHandle(process);
                }
            }

            return result;
        }

        internal void DetachAllInjectedProcesses(Native.R77_PROCESS[] r77Processes, uint r77ProcessCount)
        {
            for (uint i = 0; i < r77ProcessCount; i++)
            {
                DetachInjectedProcess(ref r77Processes[i]);
            }
        }

        internal void TerminateR77Service(int excludedProcessId)
        {
            Native.R77_PROCESS[] r77Processes = new Native.R77_PROCESS[Native.MaxProcesses];
            uint r77ProcessCount = Native.MaxProcesses;
            if (GetR77Processes(ref r77Processes, ref r77ProcessCount))
            {
                for (int i = 0; i < r77ProcessCount; i++)
                {
                    if (r77Processes[i].Signature == Native.R77_SERVICE_SIGNATURE && r77Processes[i].ProcessId != excludedProcessId)
                    {
                        IntPtr process = Native.OpenProcess(0x0001, false, (int)r77Processes[i].ProcessId);
                        if (process != IntPtr.Zero)
                        {
                            Native.NtTerminateProcess(process, 0);
                            Native.CloseHandle(process);
                        }
                    }
                }
            }
        }

        internal void RemoveR77Config()
        {
            string[] patterns = { "dll32", "dll64", "stager" };

            using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true))
            {
                if (baseKey != null)
                {
                    foreach (string valueName in baseKey.GetValueNames())
                    {
                        foreach (string pattern in patterns)
                        {
                            if (valueName.EndsWith(pattern, StringComparison.OrdinalIgnoreCase))
                            {
                                Program.LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);
                                baseKey.DeleteValue(valueName);
                            }
                        }
                    }
                }
            }


            string subKeyPath = @"dia?ler?con?fig".Replace("?", "");

            using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true))
            {
                if (baseKey != null && baseKey.OpenSubKey(subKeyPath) != null)
                {
                    baseKey.DeleteSubKeyTree(subKeyPath);
                    Program.LL.LogSuccessMessage("_RegistryKeyRemoved", subKeyPath);
                }
            }

        }

        //public static string GetFileImpHash(string path) //too slow
        //{
        //    return new ImportHash(new PeNet.PeFile(File.ReadAllBytes(path)).ImportedFunctions).ImpHash;
        //}

        internal static int GetServiceId(string serviceName)
        {
            ManagementObject service = new ManagementObject(@"Win32_service.Name='" + serviceName + "'");
            object serviceObject = service.GetPropertyValue("ProcessId");
            return (int)(uint)serviceObject;
        }
    }
}
