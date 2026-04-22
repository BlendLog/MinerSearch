using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using Win32Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

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

            // Если руткит уже был нейтрализован — выходим
            if (rootkit.WasNeutralized)
            {
                return ApplyResult.NotApplicable;
            }

            if (phase == CleanupPhase.SuspendOnly)
            {
                // На этой фазе ничего не делаем — руткит обрабатывается мгновенно в сканере
                return ApplyResult.Skipped;
            }

            // Finalize — финальная проверка и очистка
            if (rootkit.ConfigRemoved)
            {
                return ApplyResult.Success;
            }

            if (LaunchOptions.GetInstance.ScanOnly)
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usRootkit", "R77 configuration detected");
                return ApplyResult.Skipped;
            }

            // Финальная нейтрализация
            bool success = false;

            try
            {
                // 1. Терминация оставшихся R77-процессов
                TerminateRemainingR77Services(rootkit.ProcessIds, rootkit.Signatures);

                // 2. Удаление конфигурации из реестра
                success = RemoveR77Config();
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorNeutralizeRootkit", ex);
                return ApplyResult.Error;
            }

            return success ? ApplyResult.Success : ApplyResult.Failed;
        }

        /// <summary>
        /// Завершает оставшиеся процессы R77-сервиса.
        /// </summary>
        private void TerminateRemainingR77Services(int[] processIds, uint[] signatures)
        {
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
                                    if (signature == Native.R77_SERVICE_SIGNATURE)
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
