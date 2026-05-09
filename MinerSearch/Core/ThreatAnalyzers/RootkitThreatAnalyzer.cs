using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using Win32Wrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Native = Win32Wrapper.Native;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// Анализатор для руткит-угроз.
    /// Если RootkitThreatObject.IsRootkitPresent == true, сканирует все процессы
    /// на наличие R77-подписей и заполняет ProcessIds, Signatures, DialerPids, DetachAddresses.
    /// </summary>
    internal sealed class RootkitThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Other;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var rootkit = threat as RootkitThreatObject;
            if (rootkit == null) yield break;

            // Если текущий процесс не заражён — сканировать нечего
            if (!rootkit.IsRootkitPresent)
            {
                LocalizedLogger.LogNoThreatsFound();
                yield break;
            }

            // Руткит — всегда критическая угроза (riskLevel = 10)
            int riskLevel = 10;
            ScanObjectType scanType = ScanObjectType.Rootkit;

            yield return new ThreatDecision(threat, riskLevel, scanType);

            // Сканируем все процессы на R77-подписи
            ScanProcesses(rootkit);
        }

        /// <summary>
        /// Сканирует все процессы на наличие R77-подписей, заполняет RootkitThreatObject.
        /// </summary>
        private void ScanProcesses(RootkitThreatObject rootkit)
        {
            List<Native.R77_PROCESS> r77Processes = new List<Native.R77_PROCESS>();
            List<int> dialerPids = new List<int>();

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
                                            Native.R77_PROCESS r77Proc = new Native.R77_PROCESS
                                            {
                                                Signature = (int)signature,
                                                ProcessId = (uint)processes[i],
                                                DetachAddress = 0
                                            };

                                            if (signature == Native.R77_SIGNATURE)
                                            {
                                                r77Proc.DetachAddress = BitConverter.ToUInt64(moduleBytes, 0x40 + 2);
                                            }

                                            r77Processes.Add(r77Proc);
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

            // Находим и приостанавливаем dialer-процессы
            string dialerPattern = "di" + "al" + "er";
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

            // Заполняем RootkitThreatObject
            rootkit.R77Processes = r77Processes.Distinct().ToArray();
            rootkit.DialerPids = dialerPids.Distinct().ToArray();
        }
    }
}
