using System;
using System.Diagnostics;

namespace MSearch.Core.ThreatObjects
{
    public sealed class ProcessThreatObject : ThreatObject
    {
        public int ProcessId { get; }
        public string ProcessName { get; }
        public string ProcessArgs { get; }
        public int ProcessRemotePort { get; }
        public TimeSpan CpuTime { get; }
        public DateTime StartTime { get; }
        public long UsedMemorySize { get; }
        public ProcessModuleCollection ProcessModules { get; }
        public bool IsDotnetProcess { get; }
        public bool HasSystemPrivilege { get; }

        //Специфичные флаги процесса
        public int GPULibsCount { get; internal set; }
        public bool IsBadArgsPatternPresent { get; internal set; }
        public bool ShouldSuspend { get; internal set; }
        public bool ShouldTerminate { get; internal set; }
        public bool IsProcessHollowed { get; internal set; }
        public bool WasSuspended { get; internal set; }

        //Связанный файл процесса
        public FileThreatObject FileProcess { get; }

        public ProcessThreatObject(
            int processId,
            string processName,
            string args,
            TimeSpan cpuTime,
            DateTime startTime,
            long memorySize,
            int remotePort,
            ProcessModuleCollection modulesCollection,
            bool isDotnet,
            bool hasSystemPrivilege,
            FileThreatObject fileProcess)
            : base(ThreatObjectKind.Process, processId.ToString())
        {
            ProcessId = processId;
            ProcessName = processName;
            ProcessArgs = args;
            ProcessRemotePort = remotePort;
            CpuTime = cpuTime;
            StartTime = startTime;
            UsedMemorySize = memorySize;
            ProcessModules = modulesCollection;
            IsDotnetProcess = isDotnet;
            HasSystemPrivilege = hasSystemPrivilege;
            FileProcess = fileProcess;
        }
    }
}
