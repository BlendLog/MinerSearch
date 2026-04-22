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

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// Сканер руткитов R77 — работает мгновенно, до основного сканирования.
    /// Адаптирован из старого метода MinerSearch.DetectRk() и класса RkRemover.
    /// </summary>
    public sealed class RootkitScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            AppConfig.GetInstance.LL.LogHeadMessage("_ChekingR00tkit");

            // 1. Получаем процессы с руткит-подписями R77
            uint[] processes = new uint[Native.MaxProcesses];
            int processCount = 0;
            IntPtr[] modules = new IntPtr[Native.MaxModules];
            uint moduleCount = 0;
            byte[] moduleBytes = new byte[512];

            List<int> r77ProcessIds = new List<int>();
            List<uint> r77Signatures = new List<uint>();

            if (Native.EnumProcesses(processes, processes.Length * sizeof(uint), out processCount))
            {
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
                                        if (signature == Native.R77_SIGNATURE || 
                                            signature == Native.R77_SERVICE_SIGNATURE || 
                                            signature == Native.R77_HELPER_SIGNATURE)
                                        {
                                            r77ProcessIds.Add((int)processes[i]);
                                            r77Signatures.Add((uint)signature);
                                            break;
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

            // Если руткит не найден — выходим
            if (r77ProcessIds.Count == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
                yield break;
            }

            // 2. Нейтрализация руткита
            bool allNeutralized = false;
            bool configRemoved = false;

            // 2.1. Детачинг инжектированных процессов
            DetachR77Processes(r77ProcessIds.ToArray(), r77Signatures.ToArray());

            // 2.2. Терминация R77-сервиса
            TerminateR77Service(r77ProcessIds, r77Signatures);

            // 2.3. Проверка, что все процессы нейтрализованы
            allNeutralized = CheckR77ProcessesNeutralized();

            if (allNeutralized)
            {
                AppConfig.GetInstance.LL.LogSuccessMessage("_SuccessR00TkitNeutralized");
            }

            // 2.4. Удаление конфигурации руткита из реестра
            configRemoved = RemoveR77Config();

            // 3. Находим и приостанавливаем процессы dialer
            string dialerPattern = "di" + "al" + "er";
            List<int> dialerPids = new List<int>();

            foreach (Process process in ProcessManager.SafeGetProcesses())
            {
                if (!process.ProcessName.StartsWith(dialerPattern, StringComparison.OrdinalIgnoreCase)) continue;
                try
                {
                    ProcessManager.SuspendProcess(process.Id);
                    dialerPids.Add(process.Id);
                    AppConfig.GetInstance.totalFoundThreats++;
                }
                catch (Exception) { }
            }

            // 4. Создаём объект угрозы
            int[] processIdsArray = r77ProcessIds.Distinct().ToArray();
            uint[] signaturesArray = r77Signatures.Distinct().ToArray();
            int[] dialerPidsArray = dialerPids.Distinct().ToArray();

            var rootkitThreat = new RootkitThreatObject(
                processIdsArray,
                signaturesArray,
                dialerPidsArray,
                allNeutralized,
                configRemoved);

            AppConfig.GetInstance.totalFoundThreats += processIdsArray.Length;

            yield return rootkitThreat;
        }

        /// <summary>
        /// Детачит все инъекции R77 в процессах.
        /// </summary>
        private void DetachR77Processes(int[] processIds, uint[] signatures)
        {
            for (int i = 0; i < processIds.Length; i++)
            {
                if (signatures[i] == Native.R77_SIGNATURE)
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
                            processIds[i]);

                        if (process != IntPtr.Zero)
                        {
                            // Получаем адрес детача из модуля
                            byte[] moduleBytes = new byte[512];
                            IntPtr[] modules = new IntPtr[Native.MaxModules];
                            uint moduleCount = 0;

                            if (Native.EnumProcessModulesEx(process, modules, (uint)modules.Length * (uint)IntPtr.Size, out moduleCount, 0x03))
                            {
                                moduleCount /= (uint)IntPtr.Size;
                                if (Native.ReadProcessMemory(process, modules[0], moduleBytes, moduleBytes.Length, IntPtr.Zero))
                                {
                                    ulong detachAddress = BitConverter.ToUInt64(moduleBytes, 0x40 + 2);

                                    if (detachAddress != 0)
                                    {
                                        IntPtr thread = IntPtr.Zero;
                                        int status = Native.NtCreateThreadEx(
                                            out thread,
                                            Native.THREAD_QUERY_INFORMATION,
                                            IntPtr.Zero,
                                            process,
                                            new IntPtr((long)detachAddress),
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
        /// Завершает процессы R77-сервиса.
        /// </summary>
        private void TerminateR77Service(List<int> processIds, List<uint> signatures)
        {
            for (int i = 0; i < processIds.Count; i++)
            {
                if (signatures[i] == Native.R77_SERVICE_SIGNATURE)
                {
                    int pid = processIds[i];
                    int isCritical = 0;

                    IntPtr process = Native.OpenProcess(0x0001 | 0x0200, false, pid);
                    if (process != IntPtr.Zero)
                    {
                        try
                        {
                            Native.NtSetInformationProcess(process, 0x1D, ref isCritical, sizeof(int));

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
                        catch (Exception) { }
                        finally
                        {
                            Native.CloseHandle(process);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет, что все процессы R77 были нейтрализованы.
        /// </summary>
        private bool CheckR77ProcessesNeutralized()
        {
            uint[] processes = new uint[Native.MaxProcesses];
            int processCount = 0;
            IntPtr[] modules = new IntPtr[Native.MaxModules];
            uint moduleCount = 0;
            byte[] moduleBytes = new byte[512];

            if (!Native.EnumProcesses(processes, processes.Length * sizeof(uint), out processCount))
            {
                return true;
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
                                    if (signature == Native.R77_SIGNATURE || 
                                        signature == Native.R77_SERVICE_SIGNATURE || 
                                        signature == Native.R77_HELPER_SIGNATURE)
                                    {
                                        Native.CloseHandle(process);
                                        return false;
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

            return true;
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
