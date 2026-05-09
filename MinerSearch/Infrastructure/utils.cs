
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
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Win32Wrapper;
using static Win32Wrapper.ServiceHelper;

namespace MSearch
{
    public enum BootMode
    {
        Normal = 0,
        SafeMinimal = 1,
        SafeNetworking = 2
    }

    public static class ExeptionHandler
    {
        public static void HookExeption(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args?.ExceptionObject;
            DialogDispatcher.Show(e.Message + "\n" + e.StackTrace, AppConfig.GetInstance.LL.GetLocalizedString("_ExeptionTitle"), Color.Salmon, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class Utils
    {

        public static Mutex mutex;
        public static Mutex rebootMtx;
        static string _versionEndpoint = "";

        internal static string GetDeviceIdLockName()
        {
            string deviceId = DeviceIdProvider.GetDeviceId();
            return $"{deviceId}-instance-lock";
        }

        internal static string GetDeviceIdRebootName()
        {
            string deviceId = DeviceIdProvider.GetDeviceId();
            return $"{deviceId}-reboot-lock";
        }


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
            string KeyPath = AppConfig.GetInstance.RegistryPathMain;
            string valueName = AppConfig.GetInstance.LaunchCountValueName;


            const string migratedFlag = "migrated";
            int runCount = 0;

            try
            {
                using (RegistryKey newKey = Registry.LocalMachine.CreateSubKey(KeyPath, true))
                {
                    object migrated = newKey.GetValue(migratedFlag, 0);
                    bool alreadyMigrated = (migrated is int && (int)migrated == 1);

                    if (!alreadyMigrated)
                    {
                        using (RegistryKey oldKey = Registry.CurrentUser.OpenSubKey(KeyPath, true))
                        {
                            if (oldKey != null)
                            {
                                object oldValue = oldKey.GetValue(valueName, 0);
                                int oldCount = (oldValue is int) ? (int)oldValue : 0;
                                runCount = oldCount + 1;

                                newKey.SetValue(valueName, runCount, RegistryValueKind.DWord);
                                newKey.SetValue(migratedFlag, 1, RegistryValueKind.DWord);

                                oldKey.DeleteValue(valueName, false);

                                LocalizedLogger.LogStartupCount(runCount);
                                AppConfig.GetInstance.RunCount = runCount;
                                return;
                            }
                        }
                    }

                    object currentValue = newKey.GetValue(valueName, 0);
                    int currentCount = (currentValue is int) ? (int)currentValue : 0;
                    runCount = currentCount + 1;
                    newKey.SetValue(valueName, runCount, RegistryValueKind.DWord);
                    newKey.SetValue(migratedFlag, 1, RegistryValueKind.DWord);
                }
            }
            catch (SecurityException)
            {
                runCount = IncrementFallbackHKCU();
            }
            catch (UnauthorizedAccessException)
            {
                runCount = IncrementFallbackHKCU();
            }

            LocalizedLogger.LogStartupCount(runCount);
            AppConfig.GetInstance.RunCount = runCount;
        }

        static int IncrementFallbackHKCU()
        {
            string keyPath = AppConfig.GetInstance.RegistryPathMain;
            string valueName = AppConfig.GetInstance.LaunchCountValueName;

            int runCount;
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath, true))
            {
                object value = key.GetValue(valueName, 0);
                int current = (value is int) ? (int)value : 0;
                runCount = current + 1;
                key.SetValue(valueName, runCount, RegistryValueKind.DWord);
            }
            return runCount;
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

        internal static bool ContainsNonAscii(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            foreach (char c in s)
                if (c > 0x7F)
                    return true;

            return false;
        }

        internal static bool IsOneAppCopy() => mutex.WaitOne(0, true);
        internal static bool IsRebootMtx()
        {
            try
            {
                bool acquired = rebootMtx.WaitOne(0, true);
                if (acquired)
                {
                    rebootMtx.ReleaseMutex();
                }
                return acquired;
            }
            catch
            {
                return false;
            }
        }

        //internal static void DebugLog(string message)
        //{
        //    var stackTrace = new StackTrace(true);
        //    var frame = stackTrace.GetFrame(1);
        //    var lineNumber = frame.GetFileLineNumber();
        //    var fileName = frame.GetFileName();

        //    Console.WriteLine($"[DBG] FileName:{Path.GetFileName(fileName)} | Line:{lineNumber} | {message}");
        //}

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

        internal static void AddToQuarantine(string sourceFilePath, string note = "", bool deleteFromSource = true)
        {
            if (!File.Exists(sourceFilePath))
                return;

            try
            {

                if (UnlockObjectClass.IsRegistryKeyBlocked(AppConfig.GetInstance.RegistryPathMain))
                {
                    UnlockObjectClass.UnblockRegistry(AppConfig.GetInstance.RegistryPathMain);
                }

                string fileHash = FileChecker.CalculateMD5(sourceFilePath);

                const int blockSize = 1024 * 512;
                using (var baseKey = Registry.LocalMachine.CreateSubKey(AppConfig.GetInstance.QuarantineKeyPath))
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

                if (deleteFromSource)
                {
                    UnlockObjectClass.KillAndDelete(sourceFilePath);
                }

                if (!File.Exists(sourceFilePath))
                {
                    AppConfig.GetInstance.LL.LogSuccessMessage("_Malici0usFile", sourceFilePath, "_MovedToQuarantine");
                    //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Quarantine, note.Replace("?", "")));
                }
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", sourceFilePath);
                //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, sourceFilePath, ScanActionType.LockedByAntivirus, note.Replace("?", "")));

            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80070020)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByAnotherProcess", sourceFilePath);
                //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Error, e.Message));
                //AppConfig.GetInstance.totalNeutralizedThreats--;
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, sourceFilePath, "_File");
                //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Malware, sourceFilePath, ScanActionType.Error, ex.Message));
                //AppConfig.GetInstance.totalNeutralizedThreats--;
            }
        }

        internal static void CheckLatestReleaseVersion()
        {
            AppConfig.GetInstance.LL.LogHeadMessage("_LogCheckingUpdates");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                // Fallback: если primary timeout — сразу secondary без ping
                try
                {
                    _versionEndpoint = "primary";
                    IsNewVersionAvailable();
                }
                catch (OperationCanceledException)
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_EndpointTimeout", "primary");
                    _versionEndpoint = "secondary";
                    IsNewVersionAvailable();
                }
                catch (Exception ex)
                {
                    // Primary failed — try secondary directly
                    AppConfig.GetInstance.LL.LogWarnMessage("_EndpointError", ex.Message);
                    _versionEndpoint = "secondary";
                    IsNewVersionAvailable();
                }

                if (IsNewVersionAvailable() == 1)
                    Environment.Exit(0);
            }
            catch (Exception ex) when (stopwatch.Elapsed > TimeSpan.FromSeconds(10))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_OverallTimeout", "10s");
            }
            catch (FileNotFoundException)
            {
                DialogDispatcher.Show(
                    AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNotFoundComponent"),
                    AppConfig.GetInstance._title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                if (ex is AggregateException agg)
                {
                    var flattened = agg.Flatten();
                    bool isNetError = flattened.InnerException is System.Net.Sockets.SocketException
                        || flattened.InnerException is System.Net.Http.HttpRequestException;
                    if (isNetError)
                        AppConfig.GetInstance.LL.LogWarnMessage("_NoInternetForUpdates");
                    else
                        AppConfig.GetInstance.LL.LogWarnMessage("_UpdateCheckError", flattened.InnerException.Message);
                }
                else
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_UpdateCheckError", ex.Message);
                }
            }
        }

        static int IsNewVersionAvailable()
        {
            string latest = UpdateChecker.GetLatestVersion(_versionEndpoint).Trim();
            string current = AppConfig.GetInstance.CurrentVersion.Replace(".", "");

            if (!UpdateChecker.IsLatestVersion(current, latest.StartsWith("v") ? latest.Substring(1).Replace(".", "") : latest.Replace(".", "")))
            {
                if (AppConfig.GetInstance.IsGuiAvailable)
                {
                    var message = DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_MessageNewVersion").Replace("#LATEST#", latest), latest, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (message == DialogResult.Yes)
                    {
                        Process.Start("explorer", "https://github.com/BlendLog/MinerSearch/releases/latest");
                        return 1;
                    }
                    else
                    {
                        Logger.WriteLog(AppConfig.GetInstance.LL.GetLocalizedString("_Version") + " " + AppConfig.GetInstance.CurrentVersion + " " + AppConfig.GetInstance.LL.GetLocalizedString("_PrefixNewVersionAvailable").Replace("#LATEST#", latest), Logger.warnMedium);
                    }
                }
                else
                {
                    Logger.WriteLog(AppConfig.GetInstance.LL.GetLocalizedString("_Version") + " " + AppConfig.GetInstance.CurrentVersion + " " + AppConfig.GetInstance.LL.GetLocalizedString("_PrefixNewVersionAvailable").Replace("#LATEST#", latest), Logger.warnMedium);
                }
                return 2;

            }
            else
            {
                Logger.WriteLog("\t\t" + AppConfig.GetInstance.LL.GetLocalizedString("_LogLastVersion"), Logger.success, false);
            }
            return 0;
        }

        public static void CheckWMI(bool restartCheck)
        {
            string serviceName = "wi~nm~gm~t".Replace("~", "");
            try
            {
                if (ServiceIsInstalled(serviceName))
                {
                    var serviceinfo = GetServiceInfo(serviceName);

                    if ((ServiceBootFlag)serviceinfo.StartType != ServiceBootFlag.AutoStart)
                    {
                        ChangeStartMode(serviceName, ServiceBootFlag.AutoStart);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_CriticalServiceStartup");
                    }

                    if (GetServiceState(serviceName) != ServiceState.Running)
                    {
                        StartService(serviceName);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_CriticalServiceRestart");
                    }

                    try
                    {
                        WmiTestQuery("Dhcp");
                    }
                    catch (ManagementException me)
                    {
                        if (me.ErrorCode == ManagementStatus.InvalidClass || me.ErrorCode == ManagementStatus.ProviderLoadFailure || me.ErrorCode == ManagementStatus.ProviderFailure)
                        {
                            RestoreWMICorruption();
                            if (!restartCheck)
                            {
                                CheckWMI(true);
                            }
                        }
                    }

                }
                else
                {
                    LocalizedLogger.LogError_СriticalServiceNotInstalled();
                }

            }
            catch (MissingMethodException)
            {
                if (OSExtensions.IsDotNetInstalled())
                {
                    DialogDispatcher.Show(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorNoDotNet"), AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[xxx] {serviceName}: {ex.Message}", ConsoleColor.DarkRed, false);
            }


        }

        static void RestoreWMICorruption()
        {
            if (LaunchOptions.GetInstance.RestoredWMI)
            {
                throw new Exception("WMI_Corruption");
            }

            AppConfig.GetInstance.LL.LogMessage("\t\t[xxx]", "_WMICorruption", "", ConsoleColor.Red, false);

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            AppConfig.GetInstance.LL.LogHeadMessage("_WMIRecompilation");

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
            AppConfig.GetInstance.LL.LogHeadMessage("_WMIRegister");

            foreach (var file in Directory.EnumerateFiles(wbemPath, "*.dll", SearchOption.AllDirectories))
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
            AppConfig.GetInstance.LL.LogHeadMessage("_WMIRestartService");

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
        }

        public static string WmiTestQuery(string serviceName)
        {

            using (var service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
            {
                service.Get();
                string sPath = service["PathName"]?.ToString();

                return string.IsNullOrEmpty(sPath) ? "" : sPath;
            }
        }
    }

    public class IfeoDbgHelper
    {
        internal static bool ShouldRemoveDbg(string debugger)
        {
            if (string.IsNullOrWhiteSpace(debugger))
                return true;

            debugger = debugger.Trim();


            if (debugger == "*" || debugger == "/" || debugger.Equals("nul", StringComparison.OrdinalIgnoreCase))
                return false;

            string exe;
            string args;

            ExtractExeAndArgs(debugger, out exe, out args);
            if (exe == null)
                return true;

            exe = exe.ToLowerInvariant();

            if (IsShell(exe))
            {
                return AnalyzeShellPayload(exe, args);
            }

            if (exe == "rundll32" || exe == "rundll32.exe")
            {
                string dllPath = ExtractRundllDll(args);
                if (dllPath == null)
                    return true;

                string resolvedDll = FileSystemManager.ResolveExecutablePath(dllPath);
                return !IsAllowedPayload(resolvedDll, allowSystemOnly: true);
            }

            string resolvedExe = FileSystemManager.ResolveExecutablePath(exe);
            return !IsAllowedPayload(resolvedExe, allowSystemOnly: false);
        }

        static void ExtractExeAndArgs(string cmd, out string exe, out string args)
        {
            exe = null;
            args = null;

            if (cmd[0] == '"')
            {
                int end = cmd.IndexOf('"', 1);
                if (end <= 1) return;

                exe = cmd.Substring(1, end - 1);
                args = cmd.Substring(end + 1).Trim();
            }
            else
            {
                int space = cmd.IndexOf(' ');
                if (space < 0)
                {
                    exe = cmd;
                    return;
                }

                exe = cmd.Substring(0, space);
                args = cmd.Substring(space + 1).Trim();
            }
        }

        static bool AnalyzeShellPayload(string shellExe, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
                return true;

            if (shellExe == "cmd" || shellExe == "cmd.exe")
            {
                string payload = ExtractCmdStartPayload(args);
                if (payload == null)
                    return true;

                string resolved = FileSystemManager.ResolveExecutablePath(payload);
                return !IsAllowedPayload(resolved, allowSystemOnly: false);
            }

            return true;
        }

        static string ExtractCmdStartPayload(string args)
        {
            int idx = args.IndexOf("start ", StringComparison.OrdinalIgnoreCase);
            if (idx < 0)
                return null;

            string rest = args.Substring(idx + 6).Trim();
            ExtractExeAndArgs(rest, out string exe, out _);
            return exe;
        }

        static string ExtractRundllDll(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
                return null;

            args = args.Trim();

            if (args[0] == '"')
            {
                int end = args.IndexOf('"', 1);
                if (end <= 1) return null;
                return args.Substring(1, end - 1);
            }

            int comma = args.IndexOf(',');
            if (comma <= 0)
                return null;

            return args.Substring(0, comma).Trim();
        }

        static bool IsAllowedPayload(string path, bool allowSystemOnly)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (!File.Exists(path))
                return false;

            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string system32 = Path.Combine(windir, "System32");
            string syswow64 = Path.Combine(windir, "SysWOW64");

            bool isSystem =
                path.StartsWith(system32, StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith(syswow64, StringComparison.OrdinalIgnoreCase);

            if (allowSystemOnly && !isSystem)
                return false;

            return WinTrust.GetInstance.VerifyEmbeddedSignature(path, true) == WinVerifyTrustResult.Success;
        }

        static bool IsShell(string exe)
        {
            foreach (string pattern in MSData.GetInstance.shellPatterns)
            {
                if (exe.Equals(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
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

        //based on https://github.com/dotnet-campus/dotnetCampus.Win32ProcessCommandViewer/blob/master/src/dotnetCampus.Win32ProcessCommandViewer/AppConfig.GetInstance.cs
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

            if (riskLevel == 3) return $"{AppConfig.GetInstance.LL.GetLocalizedString("_ProcessRiskLevel_Medium")} ({riskLevel})";
            if (riskLevel >= 4 && riskLevel <= 5) return $"{AppConfig.GetInstance.LL.GetLocalizedString("_ProcessRiskLevel_High")} ({riskLevel})";
            if (riskLevel >= 6 && riskLevel < 10) return $"{AppConfig.GetInstance.LL.GetLocalizedString("_ProcessRiskLevel_VeryHigh")} ({riskLevel})";

            return $"{AppConfig.GetInstance.LL.GetLocalizedString("_ProcessRiskLevel_ExtremelyHigh")} ({riskLevel})";
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
            catch (InvalidOperationException ioe) when (ioe.HResult.Equals(unchecked((int)0x80070057)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {_pid}");
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning", _pid.ToString());
            }
            catch (System.ComponentModel.Win32Exception) { }
            catch (Exception e)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorTerminateProcess", e);
            }

        }

        internal static void SuspendProcess(int PID)
        {
            try
            {
                Process process = Process.GetProcessById(PID);

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
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessSuspended", $"{process.ProcessName}, PID: {process.Id}");
                }
                else if (totalThreads > 0)
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_ProcessSuspendedPartially", $"{process.ProcessName}.exe, PID: {process.Id}");
                }

                process.Close();

            }
            catch (Exception)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", new Exception("SuspendProcess()"));
            }
        }

        internal static WindowsIdentity GetCurrentProcessOwner()
        {
            return WindowsIdentity.GetCurrent();
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
            Bitmap bitmap = (Bitmap)GetSmallWindowIcon(mHandle);

            if (bitmap == null)
            {
                return;
            }

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

            IntPtr tokenHandle = IntPtr.Zero;
            WindowsIdentity identity = null;
            try
            {
                using (Process process = Process.GetProcessById(processId))
                {

                    if (Native.OpenProcessToken(process.Handle, Native.TOKEN_QUERY, out tokenHandle))
                    {
                        identity = new WindowsIdentity(tokenHandle);
#if DEBUG
                        //Console.WriteLine("\t[DBG] Process owner {0}: {1}", processId, identity.Name);
#endif
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
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
            finally
            {
                if (tokenHandle != IntPtr.Zero) Native.CloseHandle(tokenHandle);
                if (identity != null) identity.Dispose();
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
                            if (LaunchOptions.GetInstance.verbose)
                            {
                                AppConfig.GetInstance.LL.LogMessage("\t[i]", "_IsDotnetAssembly", $"{proc.ProcessName} | {proc.Id}", ConsoleColor.DarkGreen, false);
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

        public static bool IsProcessHollowed(int pid)
        {
            if (pid == 0 || pid == 4) return false;

            IntPtr hProcess = IntPtr.Zero;
            try
            {
                hProcess = Native.OpenProcess(Native.PROCESS_QUERY_INFORMATION | Native.PROCESS_VM_READ, false, pid);
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

        internal static bool IsStartedFromArchive()
        {
            string currentPath = Process.GetCurrentProcess().MainModule.FileName;
            return currentPath.IndexOf(@"appdata\local\temp", StringComparison.OrdinalIgnoreCase) >= 0;
        }


    }

    public class FileChecker
    {
        static string batchSig = MSData.GetInstance.queries["Defender_AddExclusionPath"];

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
            long globalOffset = 0;

            double entropy;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            {
                byte[] entropyCalcBuffer = new byte[Math.Min(bufferSize, fs.Length)];
                fs.Read(entropyCalcBuffer, 0, entropyCalcBuffer.Length);
                entropy = CalculateShannonEntropy(entropyCalcBuffer);
                fs.Seek(0, SeekOrigin.Begin);

                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    foreach (var seq in targetSequences)
                    {
                        int offset = BoyerMooreSearch(buffer, bytesRead, seq);
                        if (offset >= 0)
                        {
                            long absOffset = globalOffset + offset;
                            int index = targetSequences.IndexOf(seq);
                            if (EvaluateConditions(index, absOffset, entropy))
                            {
#if DEBUG
                                string tmp = "";
                                foreach (var c in targetSequences[index])
                                {
                                    tmp += (char)c;
                                }
                                Console.WriteLine($"[DBG] FOUND by: {tmp}, Offset: {absOffset}, Entropy: {entropy}");
#endif
                                return true;
                            }
                        }
                    }
                    globalOffset += bytesRead;
                }
            }
            return false;
        }

        static int BoyerMooreSearch(byte[] buffer, int length, byte[] pattern)
        {
            int[] badChar = new int[256];
            for (int i = 0; i < 256; i++) badChar[i] = pattern.Length;
            for (int i = 0; i < pattern.Length - 1; i++)
                badChar[pattern[i]] = pattern.Length - i - 1;

            int index = pattern.Length - 1;
            while (index < length)
            {
                int j = pattern.Length - 1;
                int i = index;
                while (j >= 0 && buffer[i] == pattern[j])
                {
                    i--; j--;
                }

                if (j < 0) return i + 1;

                index += badChar[buffer[index]];
            }
            return -1;
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

            double entropy = CalculateShannonEntropy(mostCommonSequence);
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

        static bool EvaluateConditions(int index, long globalOffset, double entropy)
        {
            if (index == 3)
                return (entropy >= 7.5) && (globalOffset <= 544);

            if (index == 7)
                return globalOffset <= 4096;

            if (index >= 0 && index <= 2)
                return globalOffset < 4096;

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

        internal static double CalculateShannonEntropy(byte[] data)
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

        internal static double CalculateShannonEntropy(string s)
        {
            var freq = s.GroupBy(c => c).Select(g => (double)g.Count() / s.Length);
            return -freq.Sum(p => p * Math.Log(p, 2));
        }

        static int SequenceSum(byte[] data)
        {
            int sum = 0;
            foreach (byte b in data)
                sum += b;
            return sum;
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

        internal static string CalculateSHA1(string filePath)
        {
            using (var sha1 = SHA1.Create())
            {
                try
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        byte[] hashBytes = sha1.ComputeHash(stream);
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

        #region SFX-Detector
        internal static bool IsSfxArchive(string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    long off = GetPeTailOffset(br);
                    if (off <= 0 || off >= fs.Length)
                        return false;

                    fs.Seek(off, SeekOrigin.Begin);
                    long avail = fs.Length - off;

                    return IsRar(br) || Is7z(br, avail);
                }
            }
            catch
            {
                return false;
            }
        }

        static long GetPeTailOffset(BinaryReader br)
        {
            if (br.ReadUInt16() != 0x5A4D)
                return -1;

            br.BaseStream.Seek(0x3C, SeekOrigin.Begin);
            int peOffset = br.ReadInt32();

            br.BaseStream.Seek(peOffset + 6, SeekOrigin.Begin);
            ushort sections = br.ReadUInt16();

            br.BaseStream.Seek(peOffset + 20, SeekOrigin.Begin);
            ushort optSize = br.ReadUInt16();

            long secTable = peOffset + 24 + optSize;
            br.BaseStream.Seek(secTable, SeekOrigin.Begin);

            long end = 0;

            for (int i = 0; i < sections; i++)
            {
                br.BaseStream.Seek(16, SeekOrigin.Current);
                uint size = br.ReadUInt32();
                uint ptr = br.ReadUInt32();
                br.BaseStream.Seek(16, SeekOrigin.Current);

                if (size > 0 && ptr + size > end)
                    end = ptr + size;
            }

            return end;
        }


        static bool IsRar(BinaryReader br)
        {
            byte[] sig = br.ReadBytes(8);

            return
                sig.Length >= 7 &&
                sig[0] == 0x52 && sig[1] == 0x61 && sig[2] == 0x72 &&
                sig[3] == 0x21 && sig[4] == 0x1A && sig[5] == 0x07 &&
                (sig[6] == 0x00 || sig[6] == 0x01);
        }


        static bool Is7z(BinaryReader br, long available)
        {
            if (available < 32)
                return false;

            byte[] sig = br.ReadBytes(6);
            if (!(sig[0] == 0x37 && sig[1] == 0x7A && sig[2] == 0xBC &&
                  sig[3] == 0xAF && sig[4] == 0x27 && sig[5] == 0x1C))
                return false;

            br.BaseStream.Seek(12, SeekOrigin.Current);
            ulong off = br.ReadUInt64();
            ulong size = br.ReadUInt64();

            return off + size + 32 <= (ulong)available;
        }
        #endregion

        internal static bool IsJarFile(string path)
        {
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
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, path);
                return false;
            }
        }

        internal static bool IsJsUnreadable(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length < (1024 * 1024))
            {

                string text;
                try
                {
                    text = File.ReadAllText(path);
                }
                catch
                {
                    return false;
                }

                if (text.Length < 1024)
                    return false;

                int longLines = text.Split('\n').Count(l => l.Length > 300);
                int escapeCount = Regex.Matches(text, @"\\x[0-9A-Fa-f]{2}|\\u[0-9A-Fa-f]{4}").Count;
                int evalCount = Regex.Matches(text, @"\beval\s*\(|\bFunction\s*\(").Count;

                double entropy = CalculateShannonEntropy(text);

                return
                    entropy > 4.9 &&
                    (longLines >= 3 || escapeCount >= 20 || evalCount > 0);
            }
            return false;
        }


        internal static bool IsUpxPacked(string filePath)
        {
            const int MaxCheckBytes = 1024;
            byte[] buffer = new byte[MaxCheckBytes];

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    int bytesRead = fs.Read(buffer, 0, MaxCheckBytes);
                    for (int i = 0; i < bytesRead - 2; i++)
                    {
                        if (buffer[i] == (byte)'U' &&
                            buffer[i + 1] == (byte)'P' &&
                            buffer[i + 2] == (byte)'X' &&
                            buffer[i + 3] == (byte)'!')
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
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
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorAnalyzingFile", ex, filePath);
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
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, filePath);
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
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ex, filePath, "_File");
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_GenericError", ex, $"IsFileSizeBloated check on {filePath}");
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
                foreach (var file in Directory.EnumerateFiles(FileSystemManager.NormalizeExtendedPath(path), pattern, SearchOption.TopDirectoryOnly))
                {
                    result.Add(file);
                }
            }
            catch (UnauthorizedAccessException uax)
            {
                if (LaunchOptions.GetInstance.verbose)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", uax, path, "_File");
                }
            }
            catch (Win32Exception) { AppConfig.GetInstance.LL.LogWarnMessage("_Error", path); }
            catch (IOException ioex) { AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ioex, path, "_File"); }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", path);
                //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, path, ScanActionType.LockedByAntivirus));

            }
            catch (Exception ex)
            {
                if (LaunchOptions.GetInstance.verbose)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, path, "_File");
                }
            }

            return result;
        }

        static IEnumerable<string> SafeEnumerateDirectories(string path)
        {
            var result = new List<string>();

            try
            {
                foreach (var dir in Directory.EnumerateDirectories(FileSystemManager.NormalizeExtendedPath(path)))
                {
                    result.Add(dir);
                }
            }
            catch (UnauthorizedAccessException uax)
            {
                if (LaunchOptions.GetInstance.verbose)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", uax, path, "_Directory");
                }
            }
            catch (Win32Exception) { AppConfig.GetInstance.LL.LogWarnMessage("_Error", path); }
            catch (IOException ioex) { AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ioex, path, "_Directory"); }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", path);
                //MinerSearch.scanResults.Add(new ScanResult(ScanObjectType.Unknown, path, ScanActionType.LockedByAntivirus));

            }
            catch (Exception ex)
            {
                if (LaunchOptions.GetInstance.verbose)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, path, "_Directory");
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

            if (!AppConfig.GetInstance.RunAsSystem)
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
        static readonly Regex IfExistPathRegex = new Regex(@"if\s+exist\s+(?:""|\^"")(?<filepath>[A-Z]:\\.*?\.(?:dll|wsf))(?:""|\^"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

        internal static uint GetReparseTag(string path)
        {
            IntPtr handle = Native.CreateFile(
                path,
                Native.GENERIC_READ,
                Native.FILE_SHARE_READ |
                Native.FILE_SHARE_WRITE |
                Native.FILE_SHARE_DELETE,
                IntPtr.Zero,
                Native.OPEN_EXISTING,
                Native.FILE_FLAG_OPEN_REPARSE_POINT |
                Native.FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);

            if (handle == IntPtr.Zero || handle == new IntPtr(-1))
                return 0;

            try
            {
                byte[] buffer = new byte[Native.MAXIMUM_REPARSE_DATA_BUFFER_SIZE];
                uint bytesReturned;

                bool ok = Native.DeviceIoControl(
                    handle,
                    Native.FSCTL_GET_REPARSE_POINT,
                    IntPtr.Zero,
                    0,
                    buffer,
                    (uint)buffer.Length,
                    out bytesReturned,
                    IntPtr.Zero);

                if (!ok || bytesReturned < 8)
                    return 0;

                return BitConverter.ToUInt32(buffer, 0);
            }
            finally
            {
                Native.CloseHandle(handle);
            }
        }

        internal static bool IsAppExecutionAlias(string path)
        {
            if (!IsReparsePoint(path))
                return false;

            uint tag = GetReparseTag(path);
            return tag == Native.IO_REPARSE_TAG_APPEXECLINK;
        }

        internal static bool IsOnlyInvisibleCharacters(string input)
        {
            // Расширенный набор: zero-width chars, bidi controls, word joiners, narrow spaces, BOM
            return Regex.IsMatch(input, @"^[\u200B\u200C\u200D\u200E\u200F\u202A\u202B\u202C\u202D\u202E\u202F\u2060\u2061\u2062\u2063\u2064\uFEFF\u00A0\u2009\u200A\u205F\u2006\u2007\u2008\u0020\p{Cf}]*$");
        }

        internal static bool HasHiddenAttribute(string path)
        {
            FileAttributes attributes = File.GetAttributes(path);
            FileAttributes targetAttributes = FileAttributes.Hidden;
            return (attributes & targetAttributes) == targetAttributes;
        }

        internal static bool HasMultipleExeExtensions(string filePath)
        {
            string name = Path.GetFileName(filePath);

            int count = 0;
            while (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                count++;
                name = name.Substring(0, name.Length - 4);
            }

            return count >= 2;
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
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                return "";
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }

        public static string NormalizeExtendedPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException();
            }

            if (path.StartsWith(@"\Device\", StringComparison.OrdinalIgnoreCase))
            {
                return path;
            }

            if (path.StartsWith(@"\\?\"))
            {
                return path;
            }

            if (path.StartsWith(@"\\"))
            {
                return @"\\?\UNC\" + path.Substring(2);
            }

            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(path);
            }

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

        public static string ConvertWellKnowSIDToGroupName(string GroupSid)
        {
            IntPtr pSid;
            if (!Native.ConvertStringSidToSid(GroupSid, out pSid))
            {
                return null;
            }

            string groupName = GetAccountNameFromSid(pSid);
            if (groupName != null)
            {
                return groupName;
            }

            Native.LocalFree(pSid);
            return null;

        }

        internal static string GetUACState()
        {
            var enableLua = Registry.LocalMachine.OpenSubKey(MSData.GetInstance.queries["SystemPolicies"])?.GetValue("EnableLUA");
            if (enableLua == null)
                return AppConfig.GetInstance.LL.GetLocalizedString("_UACUnknown");
            return (int)enableLua == 0 ? 
                AppConfig.GetInstance.LL.GetLocalizedString("_UACDisabled") : 
                AppConfig.GetInstance.LL.GetLocalizedString("_UACEnabled");
        }
    }

    public static class LanguageManager
    {

        public const string CfgFile = "language.cfg";
        const string DefaultLanguage = "EN";

        static string GetSystemLanguage()
        {

            string registryKeyPath = @"SYSTEM\Current?Control?Set\Con?trol\N?ls\Lang?uage".Replace("?", "");
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
