using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using Win32Wrapper;

namespace MSearch.Core.Scanners
{
    public class ServiceScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {

            var results = new List<IThreatObject>();
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services.OrderBy(_svc => _svc.DisplayName))
            {
                string serviceName = service.ServiceName;

                try
                {
                    if (NativeServiceController.IsServiceMarkedToDelete(serviceName))
                    {
                        service.Dispose();
                        continue;
                    }

                    ServiceControllerStatus status = service.Status;
                    string servicePathWithArgs = NativeServiceController.GetServiceImagePath(serviceName);
                    string servicePath = FileSystemManager.ExtractExecutableFromCommand(servicePathWithArgs);
                    NativeServiceController.ServiceStartMode startMode = NativeServiceController.GetServiceStartType(serviceName);

                    // Создаём FileThreatObject для основного exe сервиса
                    FileThreatObject linkedFile = null;
                    if (File.Exists(servicePath))
                    {
                        var trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(Environment.ExpandEnvironmentVariables(servicePath));
                        long fileSize = 0;
                        string originalName = "";
                        string description = "";
                        string hash = "";

                        try { fileSize = new FileInfo(servicePath).Length; } catch { }
                        try
                        {
                            var fvi = FileVersionInfo.GetVersionInfo(servicePath);
                            originalName = fvi.OriginalFilename ?? "";
                            description = fvi.FileDescription ?? "";
                        } catch { }
                        try { hash = FileChecker.CalculateSHA1(servicePath); } catch { }

                        linkedFile = new FileThreatObject(servicePath, Path.GetFileName(servicePath), fileSize, originalName, description, hash, trustResult);
                    }

                    var serviceThreat = new ServiceThreatObject(
                        serviceName,
                        service.DisplayName,
                        servicePath,
                        servicePathWithArgs,
                        status,
                        startMode)
                    {
                        LinkedServiceFile = linkedFile
                    };

                    results.Add(serviceThreat);

                    // Если есть связанный файл — добавляем отдельно для FileSystemThreatHandler
                    if (linkedFile != null)
                    {
                        results.Add(linkedFile);
                    }
                }
                catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", serviceName);
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                }
                finally
                {
                    service.Dispose();
                }
            }

            return results;
        }
    }
}
