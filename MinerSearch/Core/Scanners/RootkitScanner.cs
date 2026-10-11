using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Win32Wrapper;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// Сканер руткитов R77 — работает мгновенно, до основного сканирования.
    /// Проверка только текущего процесса на наличие R77-подписи.
    /// При заражении — детачинг себя.
    /// </summary>
    public sealed class RootkitScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            AppConfig.GetInstance.LL.LogHeadMessage("_ChekingR00tkit");

            bool isInfected = false;

            // Проверяем, заражён ли текущий процесс R77
            CheckCurrentProcess(ref isInfected);

            var rootkitThreat = new RootkitThreatObject(isInfected);

            yield return rootkitThreat;
        }

        /// <summary>
        /// Проверяет, заражён ли текущий процесс R77, и если да — детачит его.
        /// </summary>
        private void CheckCurrentProcess(ref bool isInfected)
        {
            try
            {
                using (var currentProcess = Process.GetCurrentProcess())
                {
                    IntPtr[] modules = new IntPtr[Native.MaxModules];
                    uint moduleCount = 0;
                    byte[] moduleBytes = new byte[512];

                    if (Native.EnumProcessModulesEx(
                        currentProcess.Handle,
                        modules,
                        (uint)modules.Length * (uint)IntPtr.Size,
                        out moduleCount,
                        0x03 /* LIST_MODULES_ALL */))
                    {
                        moduleCount /= (uint)IntPtr.Size;

                        for (uint j = 0; j < moduleCount; j++)
                        {
                            if (Native.ReadProcessMemory(
                                currentProcess.Handle,
                                modules[j],
                                moduleBytes,
                                moduleBytes.Length,
                                IntPtr.Zero))
                            {
                                // r77 >= 1.8.0 — 0x28, r77 <= 1.7.x — legacy 0x40
                                int headerOffset = Native.R77_HEADER_OFFSET;
                                ushort signature = BitConverter.ToUInt16(moduleBytes, headerOffset);
                                if (signature != Native.R77_SIGNATURE &&
                                    signature != Native.R77_SERVICE_SIGNATURE &&
                                    signature != Native.R77_HELPER_SIGNATURE)
                                {
                                    headerOffset = Native.R77_HEADER_OFFSET_LEGACY;
                                    signature = BitConverter.ToUInt16(moduleBytes, headerOffset);
                                }

                                if (signature == Native.R77_SIGNATURE ||
                                    signature == Native.R77_SERVICE_SIGNATURE ||
                                    signature == Native.R77_HELPER_SIGNATURE)
                                {
                                    LocalizedLogger.LogR00TkitPresent();
                                    isInfected = true;

                                    DetachR77Processes(
                                        new[] { currentProcess.Id },
                                        new[] { (uint)signature },
                                        new[] { signature == Native.R77_SIGNATURE ? BitConverter.ToUInt64(moduleBytes, headerOffset + 2) : 0UL });
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Детачит инъекции R77 в процессах (версия с detachAddresses).
        /// </summary>
        private void DetachR77Processes(int[] processIds, uint[] signatures, ulong[] detachAddresses)
        {
            for (int i = 0; i < processIds.Length; i++)
            {
                if (signatures[i] == Native.R77_SIGNATURE && detachAddresses[i] != 0)
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
                            IntPtr thread = IntPtr.Zero;
                            int status = Native.NtCreateThreadEx(
                                out thread,
                                Native.THREAD_QUERY_INFORMATION,
                                IntPtr.Zero,
                                process,
                                new IntPtr((long)detachAddresses[i]),
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
    }
}
