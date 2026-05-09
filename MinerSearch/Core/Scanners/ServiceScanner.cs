using Microsoft.Win32;
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

            // Единый проход: собираем все службы из реестра + опрашиваем статус точечно
            var allRegistryServices = ServiceHelper.GetAllServiceNamesFromRegistry();

            foreach (string serviceName in allRegistryServices.OrderBy(name => name))
            {
                try
                {
                    // Читаем статические данные из реестра
                    string servicePathWithArgs = ServiceHelper.GetImagePathFromRegistryDirect(serviceName);

                    if (string.IsNullOrEmpty(servicePathWithArgs)) continue;
                    if (servicePathWithArgs.EndsWith(".sys"))
                    {
#if DEBUG
                        Console.WriteLine($"\t[DBG] {serviceName} is kernel driver. Skipping.");
#endif
                        continue;
                    }


                    string servicePath = FileSystemManager.ExtractExecutableFromCommand(servicePathWithArgs);
                    NativeServiceController.ServiceStartMode startMode = ServiceHelper.GetStartTypeFromRegistryDirect(serviceName);


                    // Пробуем получить статус через SCManager (живой опрос)
                    ServiceControllerStatus status = ServiceControllerStatus.Stopped;
                    bool scmUnavailable = false;
                    try
                    {
                        var sc = new ServiceController(serviceName);
                        status = sc.Status;
                        sc.Dispose();
                    }
                    catch
                    {
                        // SCManager недоступен — помечаем флаг
#if DEBUG
                        Console.WriteLine($"\t[DBG] \"{serviceName}\"SCM Unavailable");
#endif
                        scmUnavailable = true;
                    }

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
                        }
                        catch { }
                        try { hash = FileChecker.CalculateSHA1(servicePath); } catch { }

                        linkedFile = new FileThreatObject(servicePath, Path.GetFileName(servicePath), fileSize, originalName, description, hash, trustResult);
                    }

                    // Проверяем SafeBoot\Minimal и SafeBoot\Network
                    bool hasInMinimal = false;
                    bool hasInNetwork = false;
                    try
                    {
                        using (var minimalKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\SafeBoot\Minimal"))
                        {
                            if (minimalKey != null)
                            {
                                hasInMinimal = minimalKey.GetSubKeyNames().Any(name => string.Equals(name, serviceName, StringComparison.OrdinalIgnoreCase));
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        using (var networkKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\SafeBoot\Network"))
                        {
                            if (networkKey != null)
                            {
                                hasInNetwork = networkKey.GetSubKeyNames().Any(name => string.Equals(name, serviceName, StringComparison.OrdinalIgnoreCase));
                            }
                        }
                    }
                    catch { }

                    bool inSafeMode = hasInMinimal || hasInNetwork;

                    var serviceThreat = new ServiceThreatObject(
                        serviceName,
                        servicePath,
                        servicePathWithArgs,
                        status,
                        inSafeMode,
                        startMode)
                    {
                        LinkedServiceFile = linkedFile,
                        SCMUnavailable = scmUnavailable
                    };

                    results.Add(serviceThreat);

                    // Если есть связанный файл — добавляем отдельно для FileSystemThreatHandler
                    if (linkedFile != null)
                    {
                        results.Add(linkedFile);
                    }
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                }
            }

            return results;
        }
    }
}
