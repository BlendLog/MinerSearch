using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;

namespace MSearch.Core.Scanners
{
    public class WmiScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan(CancellationToken token)
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
                            token.ThrowIfCancellationRequested();

                            string name = obj["Name"] as string ?? "";
                            string commandLine = obj["CommandLineTemplate"] as string ?? "";

                            ScanActivity.Report(string.IsNullOrEmpty(commandLine) ? name : commandLine);

                            var wmiThreat = new WmiSubscriptionThreatObject(
                                name,
                                commandLine,
                                MSData.GetInstance.consts[MSKeys.WmiConsumerClass]);

                            results.Add(wmiThreat);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
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
