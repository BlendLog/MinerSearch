
using DBase;
using Microsoft.Win32;
using MSearch.Core;
using MSearch.UI;
using netlib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MSearch
{
    public enum BootMode
    {
        Normal = 0,
        SafeMinimal = 1,
        SafeNetworking = 2
    }

    internal class SuspiciousServiceInfo
    {
        public string FilePath { get; set; } = "";
        public bool IsMlwrSignature { get; set; } = false;
        public bool HasHijackedDll { get; set; } = false;
    }

    public static class ExeptionHandler
    {
        public static void HookExeption(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            DialogDispatcher.Show(e.Message + "\n" + e.StackTrace, AppConfig.Instance.LL.GetLocalizedString("_ExeptionTitle"), Color.Salmon, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class Utils
    {

        public static Mutex mutex = new Mutex(false, "9c5d03a2-b3e5-4b28-a1f6-eafd9b0ed091");
        public static Mutex rebootMtx = new Mutex(false, "dcd2f5a3-55f7-4903-b4ff-303879f87aab");

        internal static string GetRndString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(8);
        }

        internal static string GetRndString(int len)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(len);
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
                AppConfig.Instance.RunCount = 1;
            }
            else
            {
                int newValue = (int)key.GetValue(valueName, 0) + 1;
                LocalizedLogger.LogStartupCount(newValue);
                key.SetValue(valueName, newValue);
                AppConfig.Instance.RunCount = newValue;
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

        internal static bool ShouldRemoveDebugger(string debuggerPath)
        {
            if (string.IsNullOrWhiteSpace(debuggerPath))
                return true;

            if (Path.IsPathRooted(debuggerPath))
            {
                string debuggerPathNormal = debuggerPath.Replace("\"", "");
                if (File.Exists(debuggerPathNormal))
                {
                    if (new WinTrust().VerifyEmbeddedSignature(debuggerPathNormal) == WinVerifyTrustResult.Success)
                    {
                        return false;
                    }
                    return true;
                }
            }

            if (debuggerPath.Equals("/") || debuggerPath.Equals("*")) return false;

            string resolvedPath = FileSystemManager.ResolveExecutablePath(debuggerPath);
            if (resolvedPath == null)
            {
                AppConfig.Instance.LL.LogWarnMessage("_Error", $"Unable to resolve path from Debugger: {debuggerPath}");
                return false;
            }
            resolvedPath = resolvedPath.Trim();

            string[] allowedRedir = { "taskkill.exe", "dllhost.exe", "svchost.exe", "systray.exe" };

            if (!File.Exists(resolvedPath)) return true;

            string system32Path = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32");
            string syswow64Path = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SysWOW64");
            bool isInSystemFolder = resolvedPath.StartsWith(system32Path, StringComparison.OrdinalIgnoreCase) || resolvedPath.StartsWith(syswow64Path, StringComparison.OrdinalIgnoreCase);

            if (isInSystemFolder)
            {
                foreach (string allowed in allowedRedir)
                {
                    if (resolvedPath.EndsWith("\\" + allowed, StringComparison.OrdinalIgnoreCase))
                        return false;
                }
            }

            return true;
        }

        internal static bool IsOneAppCopy() => mutex.WaitOne(0, true);
        internal static bool IsRebootMtx() => rebootMtx.WaitOne(0, true);

        internal static bool RegistryKeyExists(string path)
        {
            return Registry.LocalMachine.OpenSubKey(path) == null;
        }

        internal static void DebugLog(string message)
        {
            var stackTrace = new StackTrace(true);
            var frame = stackTrace.GetFrame(1);
            var lineNumber = frame.GetFileLineNumber();
            var fileName = frame.GetFileName();

            Console.WriteLine($"[DBG] FileName:{Path.GetFileName(fileName)} | Line:{lineNumber} | {message}");
        }

        internal static void RemoveDefenderExclusion(string type, string value)
        {

            string exclusionType = "";

            if (type == "Paths")
                exclusionType = "Path";
            else if (type == "Processes")
                exclusionType = "Process";
            else if (type == "Extensions")
                exclusionType = "Extension";

            if (exclusionType == "") return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "powershell.exe";
            psi.Arguments = "-Exe~cuti~onPo~~licy By~pa~ss -c \"Re~mo~ve~-~M~p~Prefe~~rence -Ex~~clus~ion".Replace("~", "") + exclusionType + " '" + value + "'\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
            }

        }

        internal List<string> GetSubkeys(string parentKeyPath)
        {
            List<string> subkeys = new List<string>();

            try
            {
                using (RegistryKey parentKey = Registry.LocalMachine.OpenSubKey(parentKeyPath))
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, "GetSubkeys");
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

        internal static void AddToQuarantine(string sourceFilePath, string note = "")
        {
            if (!File.Exists(sourceFilePath))
                return;

            try
            {
                long fileSize = new FileInfo(sourceFilePath).Length;
                if (fileSize >= 200 * 1024 * 1024)
                {
                    UnlockObjectClass.KillAndDelete(sourceFilePath);
                    if (!File.Exists(sourceFilePath))
                    {
                        AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", sourceFilePath, "_Deleted");
                        MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Deleted, note));
                        return;
                    }
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", sourceFilePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, sourceFilePath, ScanActionType.LockedByAntivirus, note));

            }
            catch (Exception ex1)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotRemove", ex1, sourceFilePath, "_File");
                AppConfig.Instance.totalNeutralizedThreats--;
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Error, note));
                return;
            }

            try
            {
                const string quarantineKeyPath = @"Software\M1nerSearch\Quarantine";
                const string quarantineKeyPathMain = @"Software\M1nerSearch";

                if (UnlockObjectClass.IsRegistryKeyBlocked(quarantineKeyPathMain))
                {
                    UnlockObjectClass.UnblockRegistry(quarantineKeyPathMain);
                }

                string fileHash;
                using (var md5 = MD5.Create())
                {
                    byte[] hashBytes = md5.ComputeHash(File.ReadAllBytes(sourceFilePath));
                    fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }

                const int blockSize = 1024 * 512;
                using (var baseKey = Registry.LocalMachine.CreateSubKey(quarantineKeyPath))
                {
                    if (baseKey == null)
                        return;

                    using (var subKey = baseKey.CreateSubKey(fileHash))
                    {
                        if (subKey == null)
                            throw new InvalidOperationException($"Unable to create subkey: {fileHash}");

                        subKey.SetValue("OriginalPath", sourceFilePath, RegistryValueKind.String);

                        using (var fileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            long fileSize = fileStream.Length;
                            int totalParts = (int)Math.Ceiling((double)fileSize / blockSize);
                            subKey.SetValue("TotalParts", totalParts, RegistryValueKind.DWord);

                            byte[] buffer = new byte[blockSize];
                            for (int i = 0; i < totalParts; i++)
                            {
                                int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                                if (bytesRead > 0)
                                {
                                    byte[] actualData = buffer;
                                    if (bytesRead < blockSize)
                                    {
                                        actualData = new byte[bytesRead];
                                        Array.Copy(buffer, actualData, bytesRead);
                                    }

                                    subKey.SetValue($"FileData_Part{i}", actualData, RegistryValueKind.Binary);
                                }
                            }
                        }
                    }
                }

                UnlockObjectClass.KillAndDelete(sourceFilePath);
                if (!File.Exists(sourceFilePath))
                {
                    AppConfig.Instance.LL.LogSuccessMessage("_Malici0usFile", sourceFilePath, "_MovedToQuarantine");
                    MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Quarantine, note));
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", sourceFilePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, sourceFilePath, ScanActionType.LockedByAntivirus, note));

            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80070020)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByAnotherProcess", sourceFilePath);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Error, e.Message));
                AppConfig.Instance.totalNeutralizedThreats--;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, sourceFilePath, "_File");
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Error, ex.Message));
                AppConfig.Instance.totalNeutralizedThreats--;
            }
        }

        internal static void CheckLatestReleaseVersion()
        {
            AppConfig.Instance.LL.LogHeadMessage("_LogCheckingUpdates");
            try
            {
                if (Internet.IsOK())
                {
                    string latest = GithubAPI.GetLatestVersion().Trim();
                    string current = AppConfig.Instance.CurrentVersion.Replace(".", "");

                    if (!IsLatestVersion(current, latest.StartsWith("v") ? latest.Substring(1).Replace(".", "") : latest.Replace(".", "")))
                    {
                        if (AppConfig.Instance.IsGuiAvailable)
                        {
                            var message = DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_MessageNewVersion").Replace("#LATEST#", latest), latest, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (message == DialogResult.Yes)
                            {
                                Process.Start("explorer", "https://github.com/BlendLog/MinerSearch/releases/latest");
                                Environment.Exit(0);
                            }
                            else
                            {
                                Logger.WriteLog(AppConfig.Instance.LL.GetLocalizedString("_Version") + " " + AppConfig.Instance.CurrentVersion + " " + AppConfig.Instance.LL.GetLocalizedString("_PrefixNewVersionAvailable").Replace("#LATEST#", latest), Logger.warnMedium);
                            }
                        }
                        else
                        {
                            Logger.WriteLog(AppConfig.Instance.LL.GetLocalizedString("_Version") + " " + AppConfig.Instance.CurrentVersion + " " + AppConfig.Instance.LL.GetLocalizedString("_PrefixNewVersionAvailable").Replace("#LATEST#", latest), Logger.warnMedium);
                        }

                    }
                    else
                    {
                        Logger.WriteLog("\t\t" + AppConfig.Instance.LL.GetLocalizedString("_LogLastVersion"), Logger.success, false);
                    }
                }
                else
                {
                    Logger.WriteLog("\t\t[!] " + AppConfig.Instance.LL.GetLocalizedString("_ErrorCannotCheckUpdates") + " | " + AppConfig.Instance.LL.GetLocalizedString("_NoInternetConnection"), false, false, true);
                }
            }
            catch (FileNotFoundException notfoundEx)
            {
                DialogDispatcher.Show(AppConfig.Instance.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}", AppConfig.Instance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotCheckUpdates", ex);
            }
        }

        static bool IsLatestVersion(string currentVersion, string latestVersion)
        {
            if (currentVersion.Length > latestVersion.Length)
            {
                latestVersion += "0";
            }
            else if (currentVersion.Length < latestVersion.Length)
            {
                currentVersion += "0";
            }

            double currentVersionAsDouble = double.Parse(currentVersion + "0", CultureInfo.InvariantCulture);
            double latestVersionAsDouble = double.Parse(latestVersion + "0", CultureInfo.InvariantCulture);

            return currentVersionAsDouble >= latestVersionAsDouble;
        }


    }

    public class ProcessManager
    {
        static readonly int MAX_PATH_BUFFER = 4096;
        static readonly string[] netAssemblyNames = new string[] { "mscorlib.dll", "mscorlib.ni.dll", "mscoree.dll", "mscoreei.dll", "clrjit.dll" };

        // Helper method to convert byte array to structure
        static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        internal static List<Process> SafeGetProcesses()
        {
            List<Process> procs = new List<Process>();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == 0 || p.Id == 4) continue;
                try
                {
                    ProcessModule t = p.Modules[0];
                }
                catch (System.ComponentModel.Win32Exception w32e)
                {
#if DEBUG
                    Console.WriteLine($"[DBG] {p.ProcessName}: {w32e.Message}");
#endif
                    continue;
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine($"[DBG] {e.Message}");
#endif
                    continue;
                }

                procs.Add(p);
            }
            return procs;
        }

        //based on https://github.com/dotnet-campus/dotnetCampus.Win32ProcessCommandViewer/blob/master/src/dotnetCampus.Win32ProcessCommandViewer/AppConfig.Instance.cs
        internal static string GetProcessCommandLine(Process process)
        {
            var pid = process.Id;

            var pbi = new Native.PROCESS_BASIC_INFORMATION();

            IntPtr proc = Native.OpenProcess(
                Native.PROCESS_QUERY_INFORMATION | Native.PROCESS_VM_READ,
                false,
                pid
            );

            if (proc == IntPtr.Zero)
            {
#if DEBUG

                Console.WriteLine($"[DBG] Failed to open process. Error: {Marshal.GetLastWin32Error()}");
#endif
                return "";
            }

            try
            {
                int status = Native.NtQueryInformationProcess(
                    proc,
                    Native.ProcessBasicInformation,
                    ref pbi,
                    Marshal.SizeOf(pbi),
                    out int returnLength
                );

                if (status != 0)
                {
#if DEBUG
                    Console.WriteLine($"[DBG] NtQueryInformationProcess failed. Status: 0x{status:X}");
#endif
                    return "";
                }

                var buff = new byte[IntPtr.Size];
                IntPtr rtlUserProcParamsOffset = (IntPtr.Size == 4) ? new IntPtr(0x10) : new IntPtr(0x20);

                IntPtr rtlUserProcParamsAddress = IntPtr.Add(pbi.PebBaseAddress, rtlUserProcParamsOffset.ToInt32());

                if (!Native.ReadProcessMemory(proc, rtlUserProcParamsAddress, buff, IntPtr.Size, out _))
                {
#if DEBUG
                    Console.WriteLine($"[DBG] Failed to read RTL_USER_PROCESS_PARAMETERS address. Error: {Marshal.GetLastWin32Error()}");
#endif
                    return "";
                }

                IntPtr rtlUserProcParamsPointer = (IntPtr.Size == 4) ? (IntPtr)BitConverter.ToInt32(buff, 0) : (IntPtr)BitConverter.ToInt64(buff, 0);

                IntPtr commandLineOffset = (IntPtr.Size == 4) ? new IntPtr(0x40) : new IntPtr(0x70);
                IntPtr commandLineAddress = IntPtr.Add(rtlUserProcParamsPointer, commandLineOffset.ToInt32());

                var commandLineStruct = new byte[Marshal.SizeOf(typeof(Native.UNICODE_STRING))];

                if (!Native.ReadProcessMemory(proc, commandLineAddress, commandLineStruct, commandLineStruct.Length, out _))
                {
#if DEBUG
                    Console.WriteLine($"[DBG] Failed to read UNICODE_STRING structure. Error: {Marshal.GetLastWin32Error()}");
#endif
                    return "";
                }

                var ucsData = ByteArrayToStructure<Native.UNICODE_STRING>(commandLineStruct);

                var commandLineBuffer = new byte[ucsData.Length];
                if (!Native.ReadProcessMemory(proc, ucsData.Buffer, commandLineBuffer, ucsData.Length, out _))
                {
#if DEBUG
                    Console.WriteLine($"[DBG] Failed to read command line string. Error: {Marshal.GetLastWin32Error()}");
#endif
                    return "";
                }

                return Encoding.Unicode.GetString(commandLineBuffer);
            }
            catch (Win32Exception w32e)
            {
#if DEBUG
                Console.WriteLine($"[DBG] Win32Exception: {w32e.HResult} {w32e.Message}");
#endif
            }
            finally
            {
                Native.CloseHandle(proc);
            }

            return "";
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

        internal static string GetLocalizedRiskLevel(int riskLevel)
        {

            if (riskLevel == 3) return $"{AppConfig.Instance.LL.GetLocalizedString("_ProcessRiskLevel_Medium")} ({riskLevel})";
            if (riskLevel >= 4 && riskLevel <= 5) return $"{AppConfig.Instance.LL.GetLocalizedString("_ProcessRiskLevel_High")} ({riskLevel})";
            if (riskLevel >= 6 && riskLevel < 10) return $"{AppConfig.Instance.LL.GetLocalizedString("_ProcessRiskLevel_VeryHigh")} ({riskLevel})";

            return $"{AppConfig.Instance.LL.GetLocalizedString("_ProcessRiskLevel_ExtremelyHigh")} ({riskLevel})";
        }

        internal static void UnProtect(int[] pids)
        {
            int _pid = 0;

            if (pids.Length == 0)
            {
                return;
            }

            try
            {

                foreach (int pid in pids)
                {
                    _pid = pid;
                    int isCritical = 0;
                    int BreakOnTermination = 0x1D;

                    IntPtr handle = Native.OpenProcess(0x001F0FFF, false, pid);
                    Native.NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
                    Native.CloseHandle(handle);
                }
            }
            catch (InvalidOperationException)
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {_pid}");
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.Instance.LL.LogWarnMessage("_ProcessNotRunning", _pid.ToString());
            }
            catch (System.ComponentModel.Win32Exception) { }
            catch (Exception e)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorTerminateProcess", e);
            }

        }

        internal static void SuspendProcess(Process process)
        {
            try
            {
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
                    AppConfig.Instance.LL.LogSuccessMessage("_ProcessSuspended", $"{process.ProcessName}, PID: {process.Id}");
                }
                else if (totalThreads > 0)
                {
                    AppConfig.Instance.LL.LogWarnMediumMessage("_ProcessSuspendedPartially", $"{process.ProcessName}.exe, PID: {process.Id}");
                }

                process.Close();

            }
            catch (Exception)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("SuspendProcess()"));
            }
        }

        internal static WindowsIdentity GetCurrentProcessOwner()
        {
            return WindowsIdentity.GetCurrent();
        }

        internal static string GetProcessOwner(int processId)
        {

            IntPtr tokenHandle = IntPtr.Zero;
            try
            {
                using (Process process = Process.GetProcessById(processId))
                {

                    if (Native.OpenProcessToken(process.Handle, Native.TOKEN_QUERY, out tokenHandle))
                    {
                        WindowsIdentity identity = new WindowsIdentity(tokenHandle);
                        return identity.Name;
                    }
                    else
                    {
                        throw new Exception("Unable open process token");
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }
            finally
            {
                if (tokenHandle != IntPtr.Zero)
                {
                    Native.CloseHandle(tokenHandle);
                }
            }
            return "";
        }

        static Icon BitmapToIcon(Bitmap bmp)
        {
            IntPtr hIcon = bmp.GetHicon();
            return Icon.FromHandle(hIcon);
        }

        internal static void SetConsoleWindowIcon(Bitmap bitmap, IntPtr mwHandle)
        {
            Icon icon = BitmapToIcon(bitmap);
            Native.SendMessage(mwHandle, Native.WM_SETICON, IntPtr.Zero, icon.Handle);
        }

        internal static void SetSmallWindowIconRandomHash(IntPtr mHandle)
        {
            var bitmap = (Bitmap)GetSmallWindowIcon(mHandle);
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                bitmap.SetPixel(rnd.Next(0, 16), rnd.Next(0, 16), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            }

            SetConsoleWindowIcon(bitmap, mHandle);
        }


        internal static Image GetSmallWindowIcon(IntPtr hWnd)
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

        internal static bool IsSystemProcess(int processId)
        {

            IntPtr tokenHandle;
            try
            {
                using (Process process = Process.GetProcessById(processId))
                {

                    if (Native.OpenProcessToken(process.Handle, Native.TOKEN_QUERY, out tokenHandle))
                    {
                        WindowsIdentity identity = new WindowsIdentity(tokenHandle);
#if DEBUG
                        Console.WriteLine("\t[DBG] Process owner {0}: {1}", processId, identity.Name);
#endif
                        Native.CloseHandle(tokenHandle);
                        return identity.IsSystem;
                    }
                    else
                    {
                        throw new Exception("Unable open process token");
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }
            return false;
        }

        internal static bool IsTrustedProcess(MSData msdata, string originalFileName, bool isValidSig)
        {
            try
            {
                if (msdata == null || string.IsNullOrEmpty(originalFileName))
                    return false;

                if (!isValidSig)
                    return false;

                return msdata.trustedProcesses.Contains(originalFileName, StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }


        internal static bool IsPwshAsParentProcess(int pid)
        {
            try
            {
                int targetPid = Process.GetProcessById(pid).Id;
                int desiredPid = GetParentProcessId(targetPid);
                if (desiredPid != 0)
                {
                    return Process.GetProcessById(desiredPid).ProcessName.Equals("powershell", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool IsDotnetProcess(Process proc)
        {
            if (proc == null || proc.Id == 0 || proc.Id == 4) return false;
            try { if (proc.MainModule == null) return false; } catch { return false; }

            IntPtr hProcess = IntPtr.Zero;
            try
            {
                hProcess = Native.OpenProcess(Native.PROCESS_QUERY_INFORMATION | Native.PROCESS_VM_READ, false, proc.Id);
                if (hProcess == IntPtr.Zero) return false;

                if (!Native.EnumProcessModulesEx(hProcess, null, 0, out uint cbNeeded, 3 /* LIST_MODULES_ALL */)) return false;
                if (cbNeeded == 0) return false;

                IntPtr[] moduleHandles = new IntPtr[cbNeeded / IntPtr.Size];
                if (!Native.EnumProcessModulesEx(hProcess, moduleHandles, cbNeeded, out cbNeeded, 3)) return false;

                foreach (IntPtr hModule in moduleHandles)
                {
                    if (hModule == IntPtr.Zero) continue;

                    var pathFromLoader = new StringBuilder(MAX_PATH_BUFFER);

                    uint loaderPathLength = Native.GetModuleFileNameEx(hProcess, hModule, pathFromLoader, MAX_PATH_BUFFER);
                    string moduleName = pathFromLoader.ToString();
                    if (loaderPathLength > 0 && moduleName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    {
                        if (netAssemblyNames.Any(s => s.Equals(Path.GetFileName(moduleName), StringComparison.OrdinalIgnoreCase)))
                        {
                            if (AppConfig.Instance.verbose)
                            {
                                AppConfig.Instance.LL.LogMessage("\t[i]", "_IsDotnetAssembly", $"{proc.ProcessName} | {proc.Id}", ConsoleColor.DarkGreen, false);
                            }
                            return true;
                        }
                    }

                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (hProcess != IntPtr.Zero) Native.CloseHandle(hProcess);
            }
        }

        public static bool IsProcessHollowed(Process proc)
        {
            if (proc == null || proc.Id == 0 || proc.Id == 4) return false;
            try { if (proc.MainModule == null) return false; } catch { return false; }

            IntPtr hProcess = IntPtr.Zero;
            try
            {
                hProcess = Native.OpenProcess(Native.PROCESS_QUERY_INFORMATION | Native.PROCESS_VM_READ, false, proc.Id);
                if (hProcess == IntPtr.Zero) return false;

                if (!Native.EnumProcessModulesEx(hProcess, null, 0, out uint cbNeeded, 3 /* LIST_MODULES_ALL */)) return false;
                if (cbNeeded == 0) return false;

                IntPtr[] moduleHandles = new IntPtr[cbNeeded / IntPtr.Size];
                if (!Native.EnumProcessModulesEx(hProcess, moduleHandles, cbNeeded, out cbNeeded, 3)) return false;

                foreach (IntPtr hModule in moduleHandles)
                {
                    if (hModule == IntPtr.Zero) continue;

                    var pathFromLoader = new StringBuilder(MAX_PATH_BUFFER);

                    uint loaderPathLength = Native.GetModuleFileNameEx(hProcess, hModule, pathFromLoader, MAX_PATH_BUFFER);
                    if (loaderPathLength > 0 && pathFromLoader.ToString().EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        var pathFromMemoryMap = new StringBuilder(MAX_PATH_BUFFER);
                        uint memoryPathLength = Native.GetMappedFileName(hProcess, hModule, pathFromMemoryMap, MAX_PATH_BUFFER);

                        if (memoryPathLength == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (hProcess != IntPtr.Zero) Native.CloseHandle(hProcess);
            }
        }

        internal static void InitPrivileges()
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("OpenProcessToken: Current process"));
                return;
            }

            for (int i = 0; i < privilegeNames.Length; i++)
            {
                if (!Native.LookupPrivilegeValue(null, privilegeNames[i], out Native.LUID luid))
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("LookupPrivilegeValue()"));
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
                int error = Marshal.GetLastWin32Error();
                AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception($"AdjustTokenPrivileges failed with error code: {error}"));
                return;
            }

            Native.CloseHandle(hToken);
        }

        internal static bool HasDebugPrivilege()
        {
            IntPtr hToken;
            if (!Native.OpenProcessToken(Native.GetCurrentProcess(), Native.TOKEN_QUERY, out hToken))
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("OpenProcessToken: Current process"));
                return false;
            }

            try
            {
                if (!Native.LookupPrivilegeValue(null, "SeDebugPrivilege", out Native.LUID luid))
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("LookupPrivilegeValue: SeDebugPrivilege"));
                    return false;
                }

                Native.PRIVILEGE_SET privilegeSet = new Native.PRIVILEGE_SET
                {
                    PrivilegeCount = 1,
                    Control = 1,
                    Privilege = new Native.LUID_AND_ATTRIBUTES[1]
                };
                privilegeSet.Privilege[0].Luid = luid;
                privilegeSet.Privilege[0].Attributes = Native.SE_PRIVILEGE_ENABLED;

                if (!Native.PrivilegeCheck(hToken, ref privilegeSet, out bool hasPrivilege))
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", new Exception("PrivilegeCheck"));
                    return false;
                }

                return hasPrivilege;
            }
            finally
            {
                Native.CloseHandle(hToken);
            }
        }

        internal static bool IsStartedFromArchive()
        {
            string currentPath = Process.GetCurrentProcess().MainModule.FileName;
            return currentPath.IndexOf(@"appdata\local\temp", StringComparison.OrdinalIgnoreCase) >= 0;
        }


    }

    public class RkRemover
    {
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
            string[] patterns = { "dll32", "dll64", "sta&ger&".Replace("&", "") };

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
                                AppConfig.Instance.LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);
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
                    AppConfig.Instance.LL.LogSuccessMessage("_RegistryKeyRemoved", subKeyPath);
                }
            }

        }
    }

    public class FileChecker
    {
        static string batchSig = MSData.Instance.queries["Defender_AddExclusionPath"];

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

        internal static bool CheckSignature(string filePath, List<byte[]> targetSequences)
        {
            const int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            double entropy;
            bool found = false;
            long globalOffset = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            {
                byte[] entropyCalcBuffer = new byte[Math.Min(bufferSize, fs.Length)];
                fs.Read(entropyCalcBuffer, 0, entropyCalcBuffer.Length);
                entropy = CalculateShannonEntropy(entropyCalcBuffer);
                fs.Seek(0, SeekOrigin.Begin);

                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i <= bytesRead - targetSequences[0].Length; i++)
                    {
                        for (int index = 0; index < targetSequences.Count; index++)
                        {
                            byte[] targetSequence = targetSequences[index];
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
                                long absoluteOffset = globalOffset + i;
                                found = EvaluateConditions(index, absoluteOffset, entropy);

#if DEBUG
                                if (found)
                                {
                                    string tmp = "";
                                    foreach (var c in targetSequence)
                                    {
                                        tmp += (char)c;
                                    }
                                    Console.WriteLine($"FOUND by: {tmp}, Offset: {absoluteOffset}, Entropy: {entropy}");
                                }
#endif
                                if (found)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    globalOffset += bytesRead;
                }
            }

            return false;
        }

        internal static bool CheckDynamicSignature(string filePath, int sequenceLength, int minOccurrences)
        {
            byte[] allBytes = File.ReadAllBytes(filePath);
            int length = allBytes.Length;
            if (length < sequenceLength)
                return false;

            var sequenceCounts = new Dictionary<ulong, int>(10000);
            ulong maxKey = 0;
            int maxCount = 0;

            for (int i = 0; i <= length - sequenceLength; i += sequenceLength)
            {
                // make 16 byte as 2 x ulong (128 bit)
                ulong hash = ComputeSimpleHash(allBytes, i, sequenceLength);
                if (hash == 0)
                    continue;

                if (sequenceCounts.TryGetValue(hash, out int count))
                {
                    count++;
                    sequenceCounts[hash] = count;
                }
                else
                {
                    sequenceCounts[hash] = 1;
                    count = 1;
                }

                if (count > maxCount)
                {
                    maxCount = count;
                    maxKey = hash;
                }

                if (sequenceCounts.Count > 10000)
                {
                    sequenceCounts.Clear();
                    sequenceCounts[maxKey] = maxCount;
                }
            }

            if (maxCount < minOccurrences)
                return false;

            // restore sequence
            byte[] mostCommonSequence = FindMostCommonSequence(allBytes, sequenceLength, maxKey);

            if (mostCommonSequence == null)
                return false;

            double entropy = ShannonEntropy(mostCommonSequence);
            int sum = SequenceSum(mostCommonSequence);


            bool isEntropy = entropy >= 3.0 && entropy <= 3.75;
            bool isRage = IsInRange(mostCommonSequence);
            bool isSum = sum >= 1750 && sum <= 2000;

            bool result = isEntropy && isRage && isSum;


#if DEBUG
            Console.Write($"[DEBUG] Occurrences: {maxCount}");

            Console.Write(", Entropy: ");
            Console.ForegroundColor = isEntropy ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write($"{entropy:F4}");
            Console.ResetColor();

            Console.Write(", ByteSum: ");
            Console.ForegroundColor = isSum ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(sum);
            Console.ResetColor();

            Console.Write(", Sequence: ");
            Console.ForegroundColor = isRage ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(BitConverter.ToString(mostCommonSequence));
            Console.ResetColor();

            Console.WriteLine("");
#endif

            return result;
        }


        internal static bool CheckDynamicSignature(string filePath, int offset, byte[] startSequence, byte[] endSequence)
        {
            const int sequenceLength = 16;

            if (startSequence.Length > sequenceLength || endSequence.Length > sequenceLength)
            {
                throw new ArgumentException("Start or end sequence is longer than the expected sequence length.");
            }

            byte[] sequenceInRange = new byte[sequenceLength];

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (fs.Length < offset + sequenceLength)
                {
                    return false;
                }

                fs.Seek(offset, SeekOrigin.Begin);
                int readBytes = fs.Read(sequenceInRange, 0, sequenceLength);

                if (readBytes < sequenceLength)
                {
                    return false;
                }
            }

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
            for (int i = 0; i < endSequence.Length; i++)
            {
                if (sequenceInRange[sequenceLength - endSequence.Length + i] != endSequence[i])
                {
                    isEndSequence = false;
                    break;
                }
            }

            return isStartSequence && isEndSequence;
        }

        private static bool EvaluateConditions(int index, long globalOffset, double entropy)
        {
            if (index == 3 && (globalOffset > 544 || globalOffset < 544 || globalOffset == 544) && entropy < 7.5)
            {
                return false;
            }

            if (index == 3 && globalOffset > 544 && entropy >= 7.5)
            {
                return false;
            }

            if (index == 7 && globalOffset > 4096)
            {
                return false;
            }

            if ((index == 0 || index == 1 || index == 2) && globalOffset >= 4096)
            {
                return false;
            }

            if (((index == 0 || index == 1 || index == 2) && entropy >= 7.5) || (index == 3 && globalOffset <= 544 && entropy >= 7.5))
            {
                return true;
            }

            return true;
        }

        static ulong ComputeSimpleHash(byte[] data, int offset, int length)
        {
            if (length < 8 || offset + length > data.Length)
                return 0;

            ulong result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = (result << 8) | data[offset + i];
            }

            // skips zeros
            for (int i = 0; i < length; i++)
            {
                if (data[offset + i] != 0)
                    return result;
            }
            return 0;
        }

        static byte[] FindMostCommonSequence(byte[] data, int length, ulong hash)
        {
            for (int i = 0; i <= data.Length - length; i += length)
            {
                ulong candidateHash = ComputeSimpleHash(data, i, length);
                if (candidateHash == hash)
                {
                    byte[] result = new byte[length];
                    Buffer.BlockCopy(data, i, result, 0, length);
                    return result;
                }
            }
            return null;
        }
        static bool IsInRange(byte[] sequence, int threshold = 12)
        {
            int count = 0;
            for (int i = 0; i < threshold; i++)
            {
                if (sequence[i] >= 0x61 && sequence[i] <= 0x7A)
                    count++;
            }
            return count >= threshold;
        }

        static double ShannonEntropy(byte[] data)
        {
            if (data.Length == 0) return 0;
            int[] freq = new int[256];
            foreach (byte b in data)
                freq[b]++;

            double entropy = 0.0;
            int len = data.Length;

            for (int i = 0; i < 256; i++)
            {
                if (freq[i] == 0) continue;
                double p = (double)freq[i] / len;
                entropy -= p * Math.Log(p, 2);
            }

            return entropy;
        }

        static int SequenceSum(byte[] data)
        {
            int sum = 0;
            foreach (byte b in data)
                sum += b;
            return sum;
        }

        internal static double CalculateShannonEntropy(byte[] fileBytes)
        {
            if (fileBytes.Length == 0)
            {
                return 0;
            }

            int[] byteFrequencies = new int[256];

            foreach (byte b in fileBytes)
            {
                byteFrequencies[b]++;
            }

            double entropy = 0.0;
            int totalBytes = fileBytes.Length;

            for (int i = 0; i < 256; i++)
            {
                if (byteFrequencies[i] > 0)
                {
                    double probability = (double)byteFrequencies[i] / totalBytes;
                    entropy -= probability * Log2(probability);
                }
            }

            return entropy;
        }

        static double Log2(double x)
        {
            return Math.Log(x) / Math.Log(2);
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

        internal static bool IsSfxArchive(string path)
        {
            byte[] archiveSignature = { 0x01, 0x01, 0x01, 0x53, 0x62, 0x73, 0x22, 0x1B, 0x08, 0x02, 0x01 };

            for (int i = 0; i < archiveSignature.Length; i++)
            {
                archiveSignature[i] -= (byte)1;
            }

            try
            {
                if (IsExecutable(path))
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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, path);
                return false;
            }
        }

        internal static bool IsJarFile(string path)
        {

            // JAR files are ZIP archives with a specific signature at the beginning
            byte[] jarSignature = { 0x51, 0x4C, 0x04, 0x05 }; // PK\003\004

            for (int i = 0; i < jarSignature.Length; i++)
            {
                jarSignature[i] -= (byte)1;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(path);
                long fileLength = fileInfo.Length;
                byte[] fileBytes = File.ReadAllBytes(path);

                if (fileLength >= jarSignature.Length)
                {
                    bool match = true;
                    for (int j = 0; j < jarSignature.Length; j++)
                    {
                        if (fileBytes[j] != jarSignature[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    return match;
                }
                return false;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, path);
                return false;
            }
        }


        internal static bool IsExecutable(string filepath)
        {
            try
            {
                byte[] signatureBytes = new byte[3];
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(signatureBytes, 0, 3);
                }

                if (signatureBytes[0] == 0x4D && signatureBytes[1] == 0x5A && signatureBytes[2] == 0x90)
                {
                    return true;
                }
                return false;
            }
            catch (IOException) { return false; }

            catch (UnauthorizedAccessException) { return false; }


        }

        internal static bool IsBatchFileBad(string filePath)
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
                AppConfig.Instance.LL.LogErrorMessage("_ErrorAnalyzingFile", ex, filePath);
            }

            return false;
        }

        internal static bool IsDotNetAssembly(string filePath)
        {
            try
            {
                var an = AssemblyName.GetAssemblyName(filePath);
                return true;
            }
            catch (BadImageFormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool IsShortcut(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            long fileLength = fileInfo.Length;
            if (fileLength > 4096) return false;


            byte[] shortcutSignature = { 0x4D, 0x01, 0x01, 0x01, 0x02, 0x15 };

            for (int i = 0; i < shortcutSignature.Length; i++)
            {
                shortcutSignature[i] -= (byte)1;
            }

            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);

                if (fileLength >= filePath.Length)
                {
                    bool match = true;
                    for (int j = 0; j < shortcutSignature.Length; j++)
                    {
                        if (fileBytes[j] != shortcutSignature[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    return match;
                }
                return false;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, filePath);
                return false;
            }
        }

        internal static string GetFileSize(long CountBytes)
        {
            string[] type = { "B", "KB", "MB", "GB" };
            if (CountBytes == 0)
                return $"0 {type[0]}";

            double bytes = Math.Abs(CountBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(CountBytes) * num} {type[place]}";
        }

        internal static bool IsFileSizeBloated(string filePath, long minFileSizeMb = 100, double threshold = 0.95)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }

                var fileInfo = new FileInfo(filePath);
                long minFileSizeBytes = minFileSizeMb * 1024 * 1024;

                if (fileInfo.Length < minFileSizeBytes)
                {
                    return false;
                }

                const int sampleSize = 8192; // chunk sample
                byte[] buffer = new byte[sampleSize];

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    long[] samplePositions =
                    {
                        0,
                        fileInfo.Length / 2 - (sampleSize / 2),
                        fileInfo.Length - sampleSize
                    };

                    foreach (long position in samplePositions)
                    {
                        if (position < 0) continue;

                        fs.Position = position;
                        int bytesRead = fs.Read(buffer, 0, sampleSize);
                        if (bytesRead == 0) continue;

                        byte repeatedByte = buffer[0];
                        int count = 0;
                        for (int i = 0; i < bytesRead; i++)
                        {
                            if (buffer[i] == repeatedByte)
                            {
                                count++;
                            }
                        }

                        if ((double)count / bytesRead >= threshold)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ex, filePath, "_File");
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_GenericError", ex, $"IsFileSizeBloated check on {filePath}");
            }

            return false;
        }
    }

    public class FileEnumerator
    {
        internal static readonly Regex ProgramDataLayersRegEx = new Regex(
    @"^(\\\\\?\\)?[a-fA-F]:\\ProgramData\\Microsoft\\Windows\\Containers\\Layers\\[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}",
    RegexOptions.Compiled | RegexOptions.IgnoreCase);

        internal static IEnumerable<string> GetFiles(string path, string pattern, int currentDepth = 0, int maxDepth = 3)
        {
            if (currentDepth > maxDepth)
            {
                yield break;
            }

            foreach (var file in SafeEnumerateFiles(path, pattern))
            {
                yield return file;
            }

            foreach (var directory in SafeEnumerateDirectories(path))
            {
                if (ShouldSkipDirectory(directory))
                {
                    continue;
                }

                foreach (var file in GetFiles(directory, pattern, currentDepth + 1, maxDepth))
                {
                    yield return file;
                }
            }
        }

        static IEnumerable<string> SafeEnumerateFiles(string path, string pattern)
        {
            var result = new List<string>();

            try
            {
                foreach (var file in Directory.EnumerateFiles(FileSystemManager.GetUNCPath(path), pattern, SearchOption.TopDirectoryOnly))
                {
                    result.Add(file);
                }
            }
            catch (UnauthorizedAccessException uax)
            {
                if (AppConfig.Instance.verbose)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", uax, path, "_File");
                }
            }
            catch (Win32Exception) { AppConfig.Instance.LL.LogWarnMessage("_Error", path); }
            catch (IOException ioex) { AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ioex, path, "_File"); }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", path);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, path, ScanActionType.LockedByAntivirus));

            }
            catch (Exception ex)
            {
                if (AppConfig.Instance.verbose)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", ex, path, "_File");
                }
            }

            return result;
        }

        static IEnumerable<string> SafeEnumerateDirectories(string path)
        {
            var result = new List<string>();

            try
            {
                foreach (var dir in Directory.EnumerateDirectories(FileSystemManager.GetUNCPath(path)))
                {
                    result.Add(dir);
                }
            }
            catch (UnauthorizedAccessException uax)
            {
                if (AppConfig.Instance.verbose)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", uax, path, "_Directory");
                }
            }
            catch (Win32Exception) { AppConfig.Instance.LL.LogWarnMessage("_Error", path); }
            catch (IOException ioex) { AppConfig.Instance.LL.LogErrorMessage("_ErrorCannotProceed", ioex, path, "_Directory"); }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", path);
                MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, path, ScanActionType.LockedByAntivirus));

            }
            catch (Exception ex)
            {
                if (AppConfig.Instance.verbose)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", ex, path, "_Directory");
                }
            }

            return result;
        }

        static bool ShouldSkipDirectory(string directory)
        {
            if (FileSystemManager.IsReparsePoint(directory))
            {
                return true;
            }

            if (!AppConfig.Instance.RunAsSystem)
            {
                if (directory.IndexOf(":\\Windows\\WinSxS", StringComparison.OrdinalIgnoreCase) != -1 ||
                    directory.IndexOf(":\\$", StringComparison.OrdinalIgnoreCase) != -1 ||
                    directory.IndexOf(@":\programdata\microsoft\Windows\Containers\BaseImages", StringComparison.OrdinalIgnoreCase) != -1 ||
                    ProgramDataLayersRegEx.IsMatch(directory) ||
                    directory.IndexOf(@"AppData\Local\Microsoft\WindowsApps", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class FileSystemManager
    {
        private static readonly Regex IfExistPathRegex = new Regex(@"if\s+exist\s+(?:""|\^"")(?<filepath>[A-Z]:\\.*?\.(?:dll|wsf))(?:""|\^"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

        internal static bool IsFileExistsLongPath(string path)
        {
            string formattedPath = path.StartsWith(@"\\?\") ? path : @"\\?\" + path;

            Native.WIN32_FILE_ATTRIBUTE_DATA fileData;
            bool fileExists = Native.GetFileAttributesEx(formattedPath, Native.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out fileData);
            return fileExists && (fileData.dwFileAttributes & Native.FILE_ATTRIBUTE_DIRECTORY) == 0;
        }

        internal static bool IsReparsePoint(string path)
        {
            return (Native.GetFileAttributes(path) & (uint)Native.FILE_ATTRIBUTE.REPARSE_POINT) == (uint)Native.FILE_ATTRIBUTE.REPARSE_POINT;
        }

        internal static bool IsOnlyInvisibleCharacters(string input)
        {
            return Regex.IsMatch(input, @"^[\u200B\u200C\u200E\u202F\u00A0]*$");
        }

        internal static bool HasHiddenAttribute(string path)
        {
            FileAttributes attributes = File.GetAttributes(path);
            FileAttributes targetAttributes = FileAttributes.Hidden;
            return (attributes & targetAttributes) == targetAttributes;
        }

        internal static void ResetAttributes(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    if (!File.Exists(path))
                    {
                        return;
                    }

                    File.SetAttributes(path, FileAttributes.Normal);
                    return;
                }

                File.SetAttributes(path, FileAttributes.Normal);

                foreach (string entry in Directory.GetFileSystemEntries(path))
                {
                    File.SetAttributes(entry, FileAttributes.Normal);
                }
            }
            catch (Exception) { };

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
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
                return "";
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }

        public static string GetUNCPath(string path)
        {
            // Check if the path already contains the long path prefix
            if (path.StartsWith(@"\\?\"))
            {
                return path;
            }

            // Prepend the long path prefix
            return @"\\?\" + path;
        }

        public static string ExtractExecutableFromCommand(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                return null;

            string line = Environment.ExpandEnvironmentVariables(commandLine.Trim());
            line = line.Replace('/', Path.DirectorySeparatorChar);

            if (line.StartsWith("\""))
            {
                int closingQuoteIndex = line.IndexOf('\"', 1);
                if (closingQuoteIndex > 0)
                {
                    string pathInQuotes = line.Substring(1, closingQuoteIndex - 1);
                    return ResolveExecutablePath(pathInQuotes);
                }
            }

            var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) return null;

            var pathBuilder = new StringBuilder();
            string lastFoundPath = null;

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i > 0) pathBuilder.Append(" ");
                pathBuilder.Append(tokens[i]);

                string currentPathCandidate = pathBuilder.ToString();

                if (File.Exists(currentPathCandidate))
                {
                    lastFoundPath = currentPathCandidate;
                }
            }

            if (lastFoundPath != null)
            {
                return Path.GetFullPath(lastFoundPath);
            }

            return ResolveExecutablePath(tokens[0]);
        }

        public static string ExtractFilePathFromIfExist(string arguments)
        {
            Match match = IfExistPathRegex.Match(arguments);

            if (match.Success)
            {
                return match.Groups["filepath"].Value;
            }

            return null;
        }

        internal static string ResolveExecutablePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            string expandedPath = Environment.ExpandEnvironmentVariables(path.Trim('\"'));

            if (expandedPath.Contains('/'))
            {
                expandedPath = expandedPath.Replace('/', Path.DirectorySeparatorChar);
            }

            if (Path.IsPathRooted(expandedPath))
            {
                return Path.GetFullPath(expandedPath);
            }

            var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(';');
            if (pathDirs != null)
            {
                var pathexts = Environment.GetEnvironmentVariable("PATHEXT")?.Split(';') ?? new[] { ".COM", ".EXE", ".BAT", ".CMD", ".MSI", ".VBS" };

                foreach (var dir in pathDirs)
                {
                    if (string.IsNullOrWhiteSpace(dir)) continue;


                    string combinedPath = Path.Combine(dir.Trim(), expandedPath);

                    if (File.Exists(combinedPath))
                        return combinedPath;

                    if (!Path.HasExtension(combinedPath))
                    {
                        foreach (var ext in pathexts)
                        {
                            string pathWithExt = combinedPath + ext;
                            if (File.Exists(pathWithExt))
                                return pathWithExt;
                        }
                    }

                }
            }

            return null;
        }

        internal static string ExtractDllPath(string commandLine)
        {
            int firstQuote = commandLine.IndexOf('"');
            int lastQuote = commandLine.LastIndexOf('"');

            if (firstQuote != -1 && lastQuote > firstQuote)
            {
                return commandLine.Substring(firstQuote + 1, lastQuote - firstQuote - 1);
            }

            string[] parts = commandLine.Split(' ');
            foreach (string part in parts)
            {
                if (part.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    return part;
            }
            return string.Empty;
        }

        internal static List<string> SplitCommandLineArguments(string commandLine)
        {
            var args = new List<string>();
            var matches = Regex.Matches(commandLine, @"""[^""]+""|\S+");
            foreach (Match match in matches)
            {
                args.Add(match.Value.Trim('"'));
            }
            return args;
        }

        internal static string NormalizePath(string rawPath)
        {
            if (string.IsNullOrEmpty(rawPath))
            {
                return rawPath;
            }

            int driveLetterPos = rawPath.IndexOf(":\\");
            if (driveLetterPos > 0)
            {
                return rawPath.Substring(driveLetterPos - 1);
            }

            return rawPath;
        }

        internal static bool ProcessFileFromArgs(string[] checkDirs, string fullpath, string arguments)
        {
            WinTrust winTrust = new WinTrust();

            if (fullpath.IndexOf("rundll32", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                List<string> argsList = SplitCommandLineArguments(arguments);
                string dllPathCandidate = null;

                foreach (string arg in argsList)
                {
                    if (!arg.StartsWith("/"))
                    {
                        dllPathCandidate = arg;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(dllPathCandidate))
                {
                    return false;
                }

                string dllPath = dllPathCandidate.Split(',')[0];

                string expandedDllPath = Environment.ExpandEnvironmentVariables(dllPath);

                string resolvedDllPath = ResolveExecutablePath(expandedDllPath);

                string filePathFromArgs_tmp = "";
                try
                {
                    if (File.Exists(resolvedDllPath))
                    {
                        filePathFromArgs_tmp = resolvedDllPath;
                        AppConfig.Instance.LL.LogMessage("[.]", "_Just_File", resolvedDllPath, ConsoleColor.Gray);
                        var trustResult = winTrust.VerifyEmbeddedSignature(resolvedDllPath, true);
                        if (trustResult != WinVerifyTrustResult.Success)
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_InvalidCertificateSignature", arguments);
                            AppConfig.Instance.totalFoundThreats++;
                            if (!AppConfig.Instance.ScanOnly)
                            {
                                Utils.AddToQuarantine(resolvedDllPath, AppConfig.Instance.LL.GetLocalizedString("_Rundll32Abuse"));
                                if (!File.Exists(resolvedDllPath))
                                {
                                    return true;
                                }
                            }
                            else return true; //Found suspicious dll. Nothing to do
                        }
                        else if (trustResult == WinVerifyTrustResult.Success)
                        {
                            Logger.WriteLog($"\t[OK]", Logger.success, false);
                        }
                    }
                    else
                    {
                        AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", resolvedDllPath);
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    AppConfig.Instance.LL.LogCautionMessage("_ErrorLockedByWD", filePathFromArgs_tmp);
                    MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, filePathFromArgs_tmp, ScanActionType.LockedByAntivirus));

                }
                catch (Exception e)
                {
                    AppConfig.Instance.LL.LogErrorMessage("_Error", e);
                }

            }

            if (fullpath.IndexOf("pcalua", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Match matchRegex = Regex.Match(arguments, @"-a\s+(?:""(?<filepath>[^""]+)""|(?<filepath>\S+))", RegexOptions.IgnoreCase);
                string fileFromArgs = "";


                if (matchRegex.Success)
                {
                    fileFromArgs = matchRegex.Groups["filepath"].Value;
                }


                if (string.IsNullOrEmpty(fileFromArgs))
                {
                    return false;
                }

                string finalPath = "";

                if (Path.IsPathRooted(fileFromArgs))
                {
                    finalPath = fileFromArgs;
                }
                else
                {
                    foreach (string checkDir in checkDirs)
                    {
                        string combinedPath = Path.Combine(checkDir, fileFromArgs);
                        if (File.Exists(combinedPath))
                        {
                            finalPath = combinedPath;
                            break;
                        }
                    }
                }


                if (!string.IsNullOrEmpty(finalPath) && string.IsNullOrEmpty(Path.GetExtension(finalPath)))
                {
                    if (File.Exists(finalPath + ".exe"))
                    {
                        finalPath += ".exe";
                    }
                }

                if (File.Exists(finalPath))
                {
                    AppConfig.Instance.LL.LogMessage("[.]", "_Just_File", finalPath, ConsoleColor.Gray);
                    try
                    {
                        var trustResult = winTrust.VerifyEmbeddedSignature(finalPath, true);
                        if (trustResult != WinVerifyTrustResult.Success)
                        {
                            AppConfig.Instance.LL.LogWarnMediumMessage("_InvalidCertificateSignature", finalPath);
                            AppConfig.Instance.LL.LogWarnMediumMessage("_PcaluaAbuse", finalPath);
                            AppConfig.Instance.totalFoundSuspiciousObjects++;
                            MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Suspicious, finalPath, ScanActionType.Skipped, AppConfig.Instance.LL.GetLocalizedString("_PcaluaAbuse")));
                        }
                        else
                        {
                            Logger.WriteLog($"\t[OK]", Logger.success, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppConfig.Instance.LL.LogErrorMessage("_Error", ex, $"pcalua processing: {finalPath}");
                    }
                }
                else
                {
                    AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", fileFromArgs);
                }
            }

            if (fullpath.IndexOf("regsvr32", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                List<string> argsList = SplitCommandLineArguments(arguments);

                if (argsList.Count > 0)
                {
                    string potentialPath = argsList.Last();

                    string normalizedPath = NormalizePath(potentialPath).Replace("\\\\", "\\");

                    if (normalizedPath.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) ||
                        normalizedPath.EndsWith(".p12", StringComparison.OrdinalIgnoreCase))
                    {
                        AppConfig.Instance.LL.LogWarnMediumMessage("_SuspiciousRegsvr32", normalizedPath);

                        if (File.Exists(normalizedPath))
                        {
                            AppConfig.Instance.totalFoundThreats++;

                            if (!AppConfig.Instance.ScanOnly)
                            {
                                Utils.AddToQuarantine(normalizedPath);
                                if (!File.Exists(normalizedPath))
                                {
                                    return true;
                                }
                            }
                            else return true; //found, but nothing to do while scan only

                        }
                        else
                        {
                            AppConfig.Instance.LL.LogWarnMessage("_FileIsNotFound", normalizedPath);
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        internal static List<string> GetOpenFilesInDirectory(string directoryPath)
        {
            var openFiles = new List<string>();
            int handleInfoSize = 0x10000;
            IntPtr handleInfoPtr = Marshal.AllocHGlobal(handleInfoSize);
            int returnLength = 0;

            while (Native.NtQuerySystemInformation(Native.SystemHandleInformation, handleInfoPtr, handleInfoSize, ref returnLength) == unchecked((int)0xc0000004))
            {
                handleInfoSize = returnLength;
                Marshal.FreeHGlobal(handleInfoPtr);
                handleInfoPtr = Marshal.AllocHGlobal(handleInfoSize);
            }

            int handleCount = Marshal.ReadInt32(handleInfoPtr);
            IntPtr handlePtr = IntPtr.Add(handleInfoPtr, 4);

            for (int i = 0; i < handleCount; i++)
            {
                Native.SYSTEM_HANDLE_INFORMATION handleInfo = Marshal.PtrToStructure<Native.SYSTEM_HANDLE_INFORMATION>(handlePtr);
                handlePtr = IntPtr.Add(handlePtr, Marshal.SizeOf<Native.SYSTEM_HANDLE_INFORMATION>());

                IntPtr processHandle = Native.OpenProcess(Native.PROCESS_DUP_HANDLE, false, handleInfo.ProcessId);
                if (processHandle == IntPtr.Zero)
                    continue;

                if (Native.DuplicateHandle(processHandle, new IntPtr(handleInfo.Handle), Process.GetCurrentProcess().Handle, out IntPtr targetHandle, 0, false, Native.DUPLICATE_SAME_ACCESS))
                {
                    IntPtr objectNamePtr = Marshal.AllocHGlobal(0x1000);
                    returnLength = 0;

                    if (Native.NtQueryObject(targetHandle, Native.ObjectNameInformation, objectNamePtr, 0x1000, ref returnLength) == 0)
                    {
                        string objectName = Marshal.PtrToStringUni(IntPtr.Add(objectNamePtr, 2));
                        if (objectName != null && objectName.StartsWith(@"\"))
                        {
                            try
                            {
                                string filePath = Path.GetFullPath(objectName);
                                if (filePath.StartsWith(directoryPath, StringComparison.OrdinalIgnoreCase))
                                {
                                    openFiles.Add(filePath);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    Marshal.FreeHGlobal(objectNamePtr);
                    Native.CloseHandle(targetHandle);
                }

                Native.CloseHandle(processHandle);
            }

            Marshal.FreeHGlobal(handleInfoPtr);
            return openFiles;
        }

    }

    public class OSExtensions
    {
        internal static bool IsWinPEEnv()
        {
            return
                Path.GetPathRoot(Assembly.GetExecutingAssembly().Location).StartsWith("X:") &&
                ProcessManager.GetCurrentProcessOwner().IsSystem &&
                Convert.ToInt64(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")?.GetValue("InstallDate") ?? -1) == 0;
        }

        internal static bool IsDotNetInstalled()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    int releaseKey = (int)ndpKey.GetValue("Release");
                    return releaseKey >= 460798;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string GetAccountNameFromSid(IntPtr pSid)
        {
            StringBuilder name = new StringBuilder();
            StringBuilder domainName = new StringBuilder();
            int nameLen = 0;
            int domainNameLen = 0;
            int sidType;

            Native.LookupAccountSid(null, pSid, name, ref nameLen, domainName, ref domainNameLen, out sidType);

            name = new StringBuilder(nameLen);
            domainName = new StringBuilder(domainNameLen);

            if (Native.LookupAccountSid(null, pSid, name, ref nameLen, domainName, ref domainNameLen, out sidType))
            {
                return name.ToString();
            }

            return null;
        }

        public static string GetWindowsVersion()
        {
            Version osVersion = Environment.OSVersion.Version;

            if (Enum.IsDefined(typeof(Native.BuildNumber), (uint)osVersion.Build))
            {
                return Enum.GetName(typeof(Native.BuildNumber), (uint)osVersion.Build)?.Replace('_', ' ');
            }

            return $"Undefined Windows version ({osVersion})";
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

        internal static bool IsUserHidden(string username)
        {
            const string userListPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\SpecialAccounts\UserList";

            try
            {

                using (RegistryKey userListKey = Registry.LocalMachine.OpenSubKey(userListPath, false))
                {
                    if (userListKey == null)
                    {
                        return false;
                    }

                    object value = userListKey.GetValue(username);

                    if (value == null)
                    {
                        return false;
                    }

                    if (value is int intValue && intValue == 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_ErrorRegistryCheck", ex, username);
                return false;
            }

            return false;
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
            const int NERR_Success = 0;

            int result = Native.NetUserDel(null, userName);
            if (result != NERR_Success)
            {
                throw new InvalidOperationException($"HRESULT: 0x{result:X16}");
            }
        }
    }

    public static class LanguageManager
    {

        public const string CfgFile = "language.cfg";
        const string DefaultLanguage = "EN";

        static string GetSystemLanguage()
        {

            string registryKeyPath = Bfs.Create("AUrBULu43F4kUPGfSCDNmIWMNWZCRFxYftK6U7OHT4q7+IIDoMdj0L2QVmGMZiLf", "zhByStdA7bNgVXWsNH5xgRI6vkqGkHn/PRjdpwBLB54=", "DU6KMnjTbANMufY77YMC+g=="); //SYSTEM\CurrentControlSet\Control\Nls\Language
            string registryValueName = "Install~Language".Replace("~", "");

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
                            return DefaultLanguage;
                        }
                        else if (value.Equals(CodeLang[1]))
                        {
                            return "RU";
                        }

                    }
                    else
                    {
                        return DefaultLanguage;
                    }
                }
                else
                {
                    return DefaultLanguage;
                }
            }
            return DefaultLanguage;
        }

        public static string LoadLanguageSetting()
        {
            try
            {
                if (File.Exists(CfgFile))
                {
                    string languageCode = File.ReadAllText(CfgFile).Trim();
                    return IsLanguageSupported(languageCode) ? languageCode : DefaultLanguage;
                }
                else return GetSystemLanguage();
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Language config error: {ex.Message}", Logger.error, false);
            }
            return DefaultLanguage;
        }

        public static void SaveLanguageSetting(string languageCode)
        {
            try
            {
                if (IsLanguageSupported(languageCode))
                {
                    if (!Directory.Exists(CfgFile))
                    {
                        File.WriteAllText(CfgFile, languageCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error saving language: {ex.Message}", Logger.error, false);
            }
        }

        static bool IsLanguageSupported(string languageCode)
        {
            return languageCode == "RU" || languageCode == "EN";
        }
    }

    public static class DeviceIdProvider
    {
        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool GetVolumeInformation(
            string lpRootPathName,
            StringBuilder lpVolumeNameBuffer,
            int nVolumeNameSize,
            out uint lpVolumeSerialNumber,
            out uint lpMaximumComponentLength,
            out uint lpFileSystemFlags,
            StringBuilder lpFileSystemNameBuffer,
            int nFileSystemNameSize);

        [StructLayout(LayoutKind.Sequential)]
        struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            public ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        public static string GetDeviceId()
        {
            string path = Path.Combine(Registry.LocalMachine.Name, @"SOFTWARE\Microsoft\SQMClient");
            var machineIdValue = Registry.GetValue(path, "MachineId", null);

            if (machineIdValue != null)
            {
                string str = machineIdValue.ToString();
                if (Guid.TryParse(str, out Guid existingGuid) && existingGuid != Guid.Empty)
                {
                    return existingGuid.ToString();
                }
            }

            string cpuId = GetCpuId();
            string winVer = Environment.OSVersion.VersionString;
            string diskSerial = GetSystemDriveSerial();

            string combined = cpuId + winVer + diskSerial;
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(combined));
                Guid newGuid = new Guid(hash);
                return newGuid.ToString();
            }
        }

        static string GetCpuId()
        {
            SYSTEM_INFO sysInfo;
            GetSystemInfo(out sysInfo);

            return $"{sysInfo.processorType}-{sysInfo.processorLevel}-{sysInfo.processorRevision}";
        }

        static string GetSystemDriveSerial()
        {
            uint serialNumber, maxComponentLen, fileSystemFlags;
            var volumeName = new StringBuilder(261);
            var fileSystemName = new StringBuilder(261);

            string rootPath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            if (GetVolumeInformation(rootPath, volumeName, volumeName.Capacity, out serialNumber, out maxComponentLen, out fileSystemFlags, fileSystemName, fileSystemName.Capacity))
            {
                return serialNumber.ToString("X8");
            }

            return "00000000";
        }
    }

    public static class ShortcutResolver
    {
        public static ShortcutInfo GetShortcutInfo(string lnkPath)
        {
            if (string.IsNullOrWhiteSpace(lnkPath))
                throw new ArgumentException("Path to the shortcut file cannot be empty", nameof(lnkPath));

            if (!System.IO.File.Exists(lnkPath))
                throw new FileNotFoundException("File is not found", lnkPath);

            var shellLink = (IShellLink)new ShellLink();
            ((IPersistFile)shellLink).Load(lnkPath, 0);

            var pathBuilder = new StringBuilder(260);
            WIN32_FIND_DATAW data = new WIN32_FIND_DATAW();
            int hr1 = shellLink.GetPath(pathBuilder, pathBuilder.Capacity, out data, 0);
            if (hr1 != 0)
                Marshal.ThrowExceptionForHR(hr1);

            var argsBuilder = new StringBuilder(1024);
            int hr2 = shellLink.GetArguments(argsBuilder, argsBuilder.Capacity);
            if (hr2 != 0)
                Marshal.ThrowExceptionForHR(hr2);

            return new ShortcutInfo
            {
                TargetPath = pathBuilder.ToString(),
                Arguments = argsBuilder.ToString()
            };
        }

        public class ShortcutInfo
        {
            public string TargetPath { get; set; }
            public string Arguments { get; set; }

            public override string ToString()
            {
                return $"Target: {TargetPath}\nArgs: {Arguments}";
            }
        }

        [ComImport]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellLink
        {
            int GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                        int cchMaxPath,
                        out WIN32_FIND_DATAW pfd,
                        uint fFlags);

            int GetIDList(out IntPtr ppidl);
            int SetIDList(IntPtr pidl);
            int GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            int SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            int GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            int SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            int GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            int SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            int GetHotkey(out short pwHotkey);
            int SetHotkey(short wHotkey);
            int GetShowCmd(out int piShowCmd);
            int SetShowCmd(int iShowCmd);
            int GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            int SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            int SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
            int Resolve(IntPtr hwnd, uint fFlags);
            int SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [ComImport]
        [Guid("0000010b-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IPersistFile
        {
            int GetClassID(out Guid pClassID);
            int IsDirty();
            int Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
            int Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, bool fRemember);
            int SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
            int GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
        }

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink
        {
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
    }
}
