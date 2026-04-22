using System.ServiceProcess;
using Win32Wrapper;

namespace MSearch.Core.ThreatObjects
{
    public sealed class ServiceThreatObject : ThreatObject
    {
        public string ServiceName { get; }
        public string DisplayName { get; }
        public string ServicePath { get; }
        public string ServicePathWithArgs { get; }
        public ServiceControllerStatus Status { get; }
        public NativeServiceController.ServiceStartMode StartMode { get; }

        // Связанные файлы (отдельные FileThreatObject для FileSystemThreatHandler)
        public FileThreatObject LinkedServiceFile { get; set; }
        public FileThreatObject LinkedServiceDll { get; set; }

        // Решения (заполняет анализатор)
        public bool ShouldStopService { get; internal set; }
        public bool ShouldDisableService { get; internal set; }
        public bool ShouldDeleteService { get; internal set; }
        public bool ShouldRestoreService { get; internal set; } //special for TermService

        public ServiceThreatObject(
            string serviceName,
            string displayName,
            string servicePath,
            string servicePathWithArgs,
            ServiceControllerStatus status,
            NativeServiceController.ServiceStartMode startMode)
            : base(ThreatObjectKind.Service, serviceName)
        {
            ServiceName = serviceName;
            DisplayName = displayName;
            ServicePath = servicePath;
            ServicePathWithArgs = servicePathWithArgs;
            Status = status;
            StartMode = startMode;
        }
    }
}
