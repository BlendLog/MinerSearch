using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Management;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// SRP: Сканирует WMI Event Consumers (механизм персистентности вредоносного ПО).
    /// Ищет CommandLineEventConsumer в root\subscription.
    /// </summary>
    public class WmiScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            var results = new List<IThreatObject>();

            try
            {
                ManagementScope scope = new ManagementScope(@"\\.\root\subscription");
                scope.Connect();

                ObjectQuery query = new ObjectQuery("SELECT * FROM CommandLineEventConsumer");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (ManagementObjectCollection resultsCollection = searcher.Get())
                    {
                        foreach (ManagementObject obj in resultsCollection)
                        {
                            string name = obj["Name"] as string ?? "";
                            string commandLine = obj["CommandLineTemplate"] as string ?? "";

                            var wmiThreat = new WmiSubscriptionThreatObject(
                                name,
                                commandLine,
                                "CommandLineEventConsumer");

                            results.Add(wmiThreat);
                        }
                    }
                }
            }
            catch (ManagementException mex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", mex);
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }

            return results;
        }
    }
}
