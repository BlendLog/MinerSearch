using Microsoft.Win32;
using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Win32Wrapper;
using Native = Win32Wrapper.Native;

namespace MSearch.Core.Handlers
{
    /// <summary>
    /// Обработчик для руткит-угроз.
    /// Выполняет финальную очистку: завершает оставшиеся процессы R77,
    /// удаляет конфигурацию из реестра.
    /// </summary>
    internal sealed class RootkitThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Other;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
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
                // 1. Детачим инжектированные процессы (R77_SIGNATURE)
                DetachR77Processes(rootkit.R77Processes);

                // 2. Терминируем dialer процессы
                dialerTerminated = TerminateDialerProcesses(rootkit.DialerPids);

                // 3. Терминируем R77-сервисы и хелперы
                TerminateRemainingR77Services(rootkit.R77Processes);

                // 4. Удаляем конфигурацию
                success = RemoveR77Config();
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
        /// Детачит инжектированные процессы R77 (R77_SIGNATURE).
        /// </summary>
        private void DetachR77Processes(Native.R77_PROCESS[] r77Processes)
        {
            if (r77Processes == null)
                return;

            foreach (Native.R77_PROCESS r77 in r77Processes)
            {
                if (r77.Signature == (int)Native.R77_SIGNATURE && r77.DetachAddress != 0)
                {
                    IntPtr process = IntPtr.Zero;
                    try
                    {
                        process = Native.OpenProcess(
                            Native.PROCESS_CREATE_THREAD |
                            Native.PROCESS_VM_OPERATION |
                            Native.PROCESS_VM_WRITE |
                            Native.PROCESS_QUERY_INFORMATION,
                            false,
                            (int)r77.ProcessId);

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

        /// <summary>
        /// Завершает dialer процессы, которые были приостановлены в RootkitScanner.
        /// </summary>
        /// <returns>true если хотя бы один dialer процесс был завершён успешно.</returns>
        private bool TerminateDialerProcesses(int[] dialerPids)
        {
            if (dialerPids == null || dialerPids.Length == 0)
                return false;

            bool anyTerminated = false;

            foreach (int pid in dialerPids.Distinct())
            {
                try
                {
                    // Снимаем защиту от завершения
                    UnProtect(pid);

                    // Завершаем процесс
                    using (var p = Process.GetProcessById(pid))
                    {
                        string pname = p.ProcessName;

                        if (!p.HasExited)
                        {
                            p.Kill();
                            p.WaitForExit();

                            if (p.HasExited)
                            {
                                AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessTerminated", $"{pname} - PID: {pid}");
                                anyTerminated = true;
                            }
                        }
                    }
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
        /// Снимает защиту процесса от завершения (аналогично ProcessThreatHandler.UnProtect).
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
        /// Завершает оставшиеся процессы R77-сервиса и хелперов.
        /// R77_SIGNATURE (0x7277) — НЕ терминирует (только детачинг).
        /// </summary>
        private void TerminateRemainingR77Services(Native.R77_PROCESS[] r77Processes)
        {
            if (r77Processes == null)
                return;

            // Получаем текущий список процессов с R77-подписью
            uint[] processes = new uint[Native.MaxProcesses];
            int processCount = 0;
            IntPtr[] modules = new IntPtr[Native.MaxModules];
            uint moduleCount = 0;
            byte[] moduleBytes = new byte[512];

            if (!Native.EnumProcesses(processes, processes.Length * sizeof(uint), out processCount))
            {
                return;
            }

            processCount /= sizeof(uint);

            for (int i = 0; i < processCount; i++)
            {
                IntPtr process = Native.OpenProcess(0x0410, false, (int)processes[i]);
                if (process != IntPtr.Zero)
                {
                    try
                    {
                        if (Native.EnumProcessModulesEx(process, modules, (uint)modules.Length * (uint)IntPtr.Size, out moduleCount, 0x03))
                        {
                            moduleCount /= (uint)IntPtr.Size;

                            for (uint j = 0; j < moduleCount; j++)
                            {
                                if (Native.ReadProcessMemory(process, modules[j], moduleBytes, moduleBytes.Length, IntPtr.Zero))
                                {
                                    ushort signature = BitConverter.ToUInt16(moduleBytes, 0x40);
                                    if (signature == Native.R77_SERVICE_SIGNATURE || signature == Native.R77_HELPER_SIGNATURE)
                                    {
                                        int pid = (int)processes[i];
                                        int isCritical = 0;

                                        IntPtr procHandle = Native.OpenProcess(0x0001 | 0x0200, false, pid);
                                        if (procHandle != IntPtr.Zero)
                                        {
                                            try
                                            {
                                                Native.NtSetInformationProcess(procHandle, 0x1D, ref isCritical, sizeof(int));

                                                try
                                                {
                                                    using (var p = Process.GetProcessById(pid))
                                                    {
                                                        if (!p.HasExited)
                                                        {
                                                            p.Kill();
                                                        }
                                                    }
                                                }
                                                catch (ArgumentException) { }
                                                catch (InvalidOperationException) { }
                                                catch (Win32Exception) { }
                                            }
                                            finally
                                            {
                                                Native.CloseHandle(procHandle);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        Native.CloseHandle(process);
                    }
                }
            }
        }

        /// <summary>
        /// Удаляет конфигурацию руткита из реестра.
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

                string subKeyPath = @"dialerconfig";

                using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true))
                {
                    if (baseKey != null && baseKey.OpenSubKey(subKeyPath) != null)
                    {
                        baseKey.DeleteSubKeyTree(subKeyPath);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_RegistryKeyRemoved", subKeyPath);
                        success = true;
                    }
                }
            }
            catch (Exception) { }

            return success;
        }
    }
}
