using System.ServiceProcess;
using Win32Wrapper;

namespace MSearch.Core.ThreatObjects
{
    public sealed class ServiceThreatObject : ThreatObject
    {
        public string ServiceName { get; }
        public string ServicePath { get; }
        public string ServicePathWithArgs { get; }
        public bool HasInSafeMode { get; }    // Сервис зарегистрирован в SafeBoot\Minimal или SafeBoot\Network

        public ServiceControllerStatus Status { get; }
        public NativeServiceController.ServiceStartMode StartMode { get; }

        // Связанные файлы (отдельные FileThreatObject для FileSystemThreatHandler)
        public FileThreatObject LinkedServiceFile { get; set; }
        public FileThreatObject LinkedServiceDll { get; set; }

        // Решения (заполняет анализатор)
        public bool ShouldStopService { get; internal set; }
        public bool ShouldDisableService { get; internal set; }
        public bool ShouldDeleteService { get; internal set; }
        public bool ShouldRestoreService { get; internal set; } //special for TermService (full restore)
        public bool ShouldRestoreServiceDll { get; internal set; } // restore ServiceDll to original termsrv.dll
        public bool ShouldResetSddl { get; internal set; } // Сбросить SDDL службы к стандартному значению при очистке
        public bool ShouldRemoveFromSafeMode { get; internal set; } // Требуется удалить запись из SafeBoot
        public bool SCMUnavailable { get; internal set; }  // SCM недоступен (SDDL) — статус не удалось получить

        public ServiceThreatObject(string serviceName, string servicePath, string servicePathWithArgs, ServiceControllerStatus status, bool hasInSafeMode, NativeServiceController.ServiceStartMode startMode) : base(ThreatObjectKind.Service, serviceName)
        {
            ServiceName = serviceName;
            ServicePath = servicePath;
            ServicePathWithArgs = servicePathWithArgs;
            Status = status;
            HasInSafeMode = hasInSafeMode;
            StartMode = startMode;
        }
    }
}
