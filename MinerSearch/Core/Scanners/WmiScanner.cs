using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Management;

namespace MSearch.Core.Scanners
{
    public class WmiScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            var results = new List<IThreatObject>();

            try
            {
                AppConfig.GetInstance.LL.LogHeadMessage("_WMIHead");
                Utils.CheckWMI(false);

                ManagementScope scope = new ManagementScope(MSData.GetInstance.consts[MSKeys.WmiNamespace]);
                scope.Connect();

                ObjectQuery query = new ObjectQuery(MSData.GetInstance.consts[MSKeys.WmiQuery]);
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
                                MSData.GetInstance.consts[MSKeys.WmiConsumerClass]);

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
