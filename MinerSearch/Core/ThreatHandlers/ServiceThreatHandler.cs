using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Win32Wrapper;

namespace MSearch.Core.Handlers
{
    internal sealed class ServiceThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Service;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            // Только фаза Finalize
            if (phase != CleanupPhase.Finalize) return ApplyResult.NotApplicable;

            var svc = decision.Target as ServiceThreatObject;
            if (svc == null) return ApplyResult.NotApplicable;

            // ScanOnly — не выполняем действия
            if (LaunchOptions.GetInstance.ScanOnly) return ApplyResult.Skipped;

            if (!svc.ShouldDisableService && !svc.ShouldDeleteService)
                return ApplyResult.Skipped;

            ServiceController service = null;
            try
            {
                service = new ServiceController(svc.ServiceName);

                if (svc.ShouldDeleteService) return HandleDelete(service, svc, decision);
                if (svc.ShouldRestoreService) return HandleRestore(service, svc, decision);

                return HandleDisableOnly(service, svc, decision);
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ex, svc.ServiceName, "_Service");
                return ApplyResult.Error;
            }
            finally
            {
                service?.Dispose();
            }
        }

        ApplyResult HandleRestore(ServiceController service, ServiceThreatObject svc, ThreatDecision decision)
        {
            // Специальная обработка для TermService - восстановление вместо удаления
            if (svc.ServiceName.Equals("TermService", StringComparison.OrdinalIgnoreCase))
            {
                return HandleRestoreTermService(service, svc, decision);
            }
            return ApplyResult.NotApplicable;
        }

        private ApplyResult HandleDelete(ServiceController service, ServiceThreatObject svc, ThreatDecision decision)
        {
            string serviceName = svc.ServiceName;

            // 1. Отключаем сервис
            if (svc.StartMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                try
                {
                    NativeServiceController.SetServiceStartType(serviceName, NativeServiceController.ServiceStartMode.Disabled);
                }
                catch (Win32Exception w32e)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", w32e, serviceName, "_Service");
                    return ApplyResult.Failed;
                }
            }

            // 2. Останавливаем если работает
            if (svc.Status == ServiceControllerStatus.Running && svc.ShouldStopService)
            {
                try
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                }
                catch (InvalidOperationException)
                {
                    // Пробуем убить процесс сервиса
                    try
                    {
                        int servicePid = NativeServiceController.GetServiceId(serviceName);
                        if (servicePid != 0)
                        {
                            ProcessManager.UnProtect(new int[] { servicePid });
                            using (Process proc = Process.GetProcessById(servicePid))
                            {
                                string args = ProcessManager.GetProcessCommandLine(proc);
                                if (args != null && args.IndexOf($"-s {serviceName}", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    ProcessManager.UnProtect(new int[] { proc.Id });
                                    proc.Kill();
                                    AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                    }
                }
            }

            Thread.Sleep(2000);

            // 3. Проверяем результат
            var newStartMode = NativeServiceController.GetServiceStartType(serviceName);
            if (newStartMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", null, serviceName, "_Service");
                return ApplyResult.Failed;
            }

            // 4. Удаляем сервис
            try
            {
                ServiceHelper.Uninstall(serviceName);
                AppConfig.GetInstance.LL.LogSuccessMessage("_MaliciousService", serviceName, "_Deleted");
            }
            catch (Win32Exception win32Ex) when (win32Ex.NativeErrorCode == 1072 || win32Ex.NativeErrorCode == 1060)
            {
                // ERROR_SERVICE_MARKED_FOR_DELETE или ERROR_SERVICE_DOES_NOT_EXIST — это ОК
            }
            catch (Win32Exception win32Ex)
            {
                decision.ApplyErrorMessage = win32Ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", win32Ex, serviceName, "_Service");
                return ApplyResult.Failed;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, serviceName, "_Service");
                return ApplyResult.Error;
            }

            return ApplyResult.Success;
        }

        private ApplyResult HandleRestoreTermService(ServiceController service, ServiceThreatObject svc, ThreatDecision decision)
        {
            string serviceName = svc.ServiceName;
            string registryPath = @"SYSTEM\CurrentControlSet\Services\TermService\Parameters";
            string originalDll = @"%SystemRoot%\System32\termsrv.dll";

            try
            {
                // 1. Восстанавливаем путь к оригинальной библиотеке
                using (var key = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    if (key != null)
                    {
                        key.SetValue("ServiceDll", originalDll, RegistryValueKind.ExpandString);
                        AppConfig.GetInstance.LL.LogSuccessMessage("_TermServiceRestored");
                    }
                    else
                    {
                        decision.ApplyErrorMessage = "Не удалось открыть раздел реестра TermService\\Parameters";
                        AppConfig.GetInstance.LL.LogErrorMessage("_ErrorRestoreTermService", null, "_Service");
                        return ApplyResult.Error;
                    }
                }

                // 2. Перезапускаем службу
                try
                {
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                    }

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceRestarted", serviceName);
                }
                catch (Exception ex)
                {
                    decision.ApplyErrorMessage = $"Служба восстановлена, но перезапуск не удался: {ex.Message}";
                    AppConfig.GetInstance.LL.LogWarnMessage("_ServiceRestartFailed", $"{serviceName}: {ex.Message}");
                    // Считаем успешным, так как путь восстановлен
                }

                // 3. Помечаем как вылеченную угрозу
                decision.ObjectType = ScanObjectType.Infected;
                decision.ActionType = ScanActionType.Cured;

                return ApplyResult.Success;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ObjectType = ScanObjectType.Infected;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorRestoreTermService", ex, "_Service");
                return ApplyResult.Error;
            }
        }

        private ApplyResult HandleDisableOnly(ServiceController service, ServiceThreatObject svc, ThreatDecision decision)
        {
            string serviceName = svc.ServiceName;

            if (svc.StartMode != NativeServiceController.ServiceStartMode.Disabled)
            {
                try
                {
                    NativeServiceController.SetServiceStartType(serviceName, NativeServiceController.ServiceStartMode.Disabled);

                    if (NativeServiceController.GetServiceStartType(serviceName) == NativeServiceController.ServiceStartMode.Disabled)
                    {
                        AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceDisabled", serviceName);
                        return ApplyResult.Success;
                    }
                }
                catch (Win32Exception w32e)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", w32e, serviceName, "_Service");
                    return ApplyResult.Failed;
                }
            }

            // Останавливаем если работает
            if (svc.Status == ServiceControllerStatus.Running && svc.ShouldStopService)
            {
                try
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                    AppConfig.GetInstance.LL.LogSuccessMessage("_ServiceStopped", serviceName);
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_ErrorCannotProceed", $"{serviceName}: {ex.Message}");
                }
            }

            return ApplyResult.Success;
        }
    }
}
