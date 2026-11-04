using DBase;
using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Win32Wrapper;
using Native = Win32Wrapper.Native;

namespace MSearch.Core.Handlers
{
    /// <summary>
    /// Rootkit threat handler.
    /// Performs final cleanup: terminates remaining R77 processes
    /// and removes the registry configuration.
    /// </summary>
    internal sealed class RootkitThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Other;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var rootkit = decision.Target as RootkitThreatObject;
            if (rootkit == null) return ApplyResult.NotApplicable;

            if (rootkit.WasNeutralized)
            {
                return ApplyResult.NotApplicable;
            }

            if (phase == CleanupPhase.SuspendOnly)
            {
                return ApplyResult.Skipped;
            }

            if (rootkit.ConfigRemoved)
            {
                decision.ActionType = ScanActionType.Deleted;
                return ApplyResult.Success;
            }

            if (LaunchOptions.GetInstance.ScanOnly)
            {
                decision.ActionType = ScanActionType.Skipped;
                return ApplyResult.Skipped;
            }

            bool success = false;
            bool dialerTerminated = false;

            try
            {
                // Удаляем Windows-службы руткита: иначе после перезагрузки стагер поднимет её заново
                bool serviceRemoved = RemoveR77Services();

                // Graceful-detach core-инжектов и сервиса (сервис r77 1.8.x живёт внутри winlogon.exe)
                DetachR77Processes(rootkit.R77Processes);

                dialerTerminated = TerminateDialerProcesses(rootkit.DialerPids, token);

                success = RemoveR77Config() || serviceRemoved;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorNeutralizeRootkit", ex);
                return ApplyResult.Error;
            }

            if (dialerTerminated || success)
            {
                decision.ActionType = dialerTerminated ? ScanActionType.Terminated : ScanActionType.Deleted;
                return ApplyResult.Success;
            }
            else
            {
                decision.ActionType = ScanActionType.Error;
                return ApplyResult.Failed;
            }
        }

        /// <summary>
        /// Graceful detach of R77 injections: core (R77_SIGNATURE) and service (R77_SERVICE_SIGNATURE).
        /// </summary>
        private void DetachR77Processes(Native.R77_PROCESS[] r77Processes)
        {
            if (r77Processes == null)
                return;

            foreach (Native.R77_PROCESS r77 in r77Processes)
            {
                // Сервис (host — winlogon.exe) снимается тем же detach-адресом, что и core.
                bool detachable = r77.Signature == (int)Native.R77_SIGNATURE ||
                                  r77.Signature == (int)Native.R77_SERVICE_SIGNATURE;

                if (detachable && r77.DetachAddress != 0)
                {
                    IntPtr process = IntPtr.Zero;
                    try
                    {
                        process = OpenProcessForDetach((int)r77.ProcessId);

                        if (process != IntPtr.Zero)
                        {
                            IntPtr thread = IntPtr.Zero;
                            int status = Native.NtCreateThreadEx(
                                out thread,
                                Native.THREAD_QUERY_INFORMATION,
                                IntPtr.Zero,
                                process,
                                new IntPtr((long)r77.DetachAddress),
                                IntPtr.Zero,
                                false,
                                0,
                                0,
                                0,
                                IntPtr.Zero);

                            if (status >= 0 && thread != IntPtr.Zero)
                            {
                                Native.CloseHandle(thread);
                            }
                        }
                    }
                    catch (Exception) { }
                    finally
                    {
                        if (process != IntPtr.Zero)
                        {
                            Native.CloseHandle(process);
                        }
                    }
                }
            }
        }

        private static IntPtr OpenProcessForDetach(int pid)
        {
            IntPtr process = Native.OpenProcess(
                Native.PROCESS_CREATE_THREAD |
                Native.PROCESS_VM_OPERATION |
                Native.PROCESS_VM_WRITE |
                Native.PROCESS_QUERY_INFORMATION,
                false,
                pid);

            if (process == IntPtr.Zero)
            {
                process = Native.OpenProcess(0x001F0FFF /* PROCESS_ALL_ACCESS */, false, pid);
            }

            return process;
        }

        /// <summary>
        /// Terminates dialer processes that were suspended in RootkitScanner.
        /// </summary>
        /// <returns>true if at least one dialer process was terminated successfully.</returns>
        private bool TerminateDialerProcesses(int[] dialerPids, CancellationToken token)
        {
            if (dialerPids == null || dialerPids.Length == 0)
                return false;

            bool anyTerminated = false;

            foreach (int pid in dialerPids.Distinct())
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    UnProtect(pid);

                    using (var p = Process.GetProcessById(pid))
                    {
                        string pname = p.ProcessName;

                        if (!p.HasExited)
                        {
                            p.Kill();
                            while (!p.WaitForExit(200))
                                token.ThrowIfCancellationRequested();

                            if (p.HasExited)
                            {
                                AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessTerminated", $"{pname} - PID: {pid}");
                                anyTerminated = true;
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (ArgumentException)
                {
                    // Процесс уже завершился
                }
                catch (Win32Exception)
                {
                    // Нет доступа или процесс завершился
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorTerminateProcess", ex);
                }
            }

            return anyTerminated;
        }

        /// <summary>
        /// Removes process kill protection (same as ProcessThreatHandler.UnProtect).
        /// </summary>
        private void UnProtect(int pid)
        {
            int _pid = 0;
            int isCritical = 0;
            int BreakOnTermination = 0x1D;

            if (pid == 0 || pid == -1)
                return;

            try
            {
                _pid = pid;
                IntPtr handle = Native.OpenProcess(0x001F0FFF, false, pid);
                Native.NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
                Native.CloseHandle(handle);
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

        /// <summary>
        /// Removes the rootkit's Windows services.
        /// </summary>
        private static bool RemoveR77Services()
        {
            string[] serviceNames = { MSKeys.R77_SERVICE_NAME, MSKeys.DIALER_SERVICE_NAME };
            bool removed = false;

            foreach (string serviceName in serviceNames)
            {
                try
                {
                    if (ServiceHelper.TryDelete(serviceName))
                        removed = true;
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_ErrorCannotRemove", $"{serviceName}: {ex.Message}");
                }
            }

            return removed;
        }

        /// <summary>
        /// Removes the rootkit configuration from the registry.
        /// </summary>
        private bool RemoveR77Config()
        {
            bool success = false;
            string[] patterns = { "dll32", "dll64", "stager" };

            try
            {
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
                                    AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);
                                    baseKey.DeleteValue(valueName);
                                    success = true;
                                }
                            }
                        }
                    }
                }


                using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true))
                {
                    if (baseKey != null)
                    {
                        foreach (string subKeyPath in MSData.GetInstance.r77SubKeys)
                        {
                            if (baseKey.OpenSubKey(subKeyPath) != null)
                            {
                                baseKey.DeleteSubKeyTree(subKeyPath);
                                AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryKeyRemoved", subKeyPath);
                                success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            return success;
        }
    }
}
