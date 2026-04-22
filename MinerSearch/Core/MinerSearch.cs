using MSearch.Core;
using netlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MSearch
{
    public interface IScanState
    {
        void AddScanResult(ScanResult result);
        void IncrementFoundThreats();
        void IncrementFoundSuspicious();
    }

    public sealed class MinerSearchScanState : IScanState
    {
        public List<ScanResult> scanResults = new List<ScanResult>() { };
        public void AddScanResult(ScanResult result)
        {
            scanResults.Add(result);
        }
        public void IncrementFoundThreats()
        {
            AppConfig.GetInstance.totalFoundThreats++;
        }

        public void IncrementFoundSuspicious()
        {
            AppConfig.GetInstance.totalFoundSuspiciousObjects++;
        }

        public List<ScanResult> GetScanResults()
        {
            return scanResults;
        }


    }

    public class MinerSearch
    {
        internal static void SentLog()
        {
            if (OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) >= 0 || AppConfig.GetInstance.bootMode == BootMode.SafeMinimal)
            {
                return;
            }

            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    return;
                }
            }

            string DeviceId = DeviceIdProvider.GetDeviceId();
            LogSender.UploadFile(Path.Combine(Logger.LogsFolder, Logger.logFileName), Convert.ToBase64String(Guid.Parse(DeviceId).ToByteArray()), $"{DeviceId}" +
                $"\nv{AppConfig.GetInstance.CurrentVersion}" +
                $"\nRuns: {AppConfig.GetInstance.RunCount}, Threats: {AppConfig.GetInstance.totalFoundThreats}, Cured: {AppConfig.GetInstance.totalFoundThreats + AppConfig.GetInstance.totalNeutralizedThreats}");
        }
    }
}
