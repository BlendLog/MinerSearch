using MSearch.Core.Scanners;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.Managers
{
    public interface IThreatScanner
    {
        IEnumerable<IThreatObject> Scan();
    }

    public class ScanManager
    {
        private readonly IThreatScanner _rootkitScanner;
        private readonly ProcessScanner _processScanner;
        private readonly IEnumerable<IThreatScanner> _otherScanners;
        private readonly LaunchOptions _options;

        public ScanManager(RootkitScanner rootkitScanner, ProcessScanner processScanner, IEnumerable<IThreatScanner> otherScanners)
        {
            _rootkitScanner = rootkitScanner;
            _processScanner = processScanner;
            _otherScanners = otherScanners;
            _options = LaunchOptions.GetInstance;
        }

        public IEnumerable<IThreatObject> ScanProcessesOnly()
        {
            if (_options.no_runtime) return new List<IThreatObject>();
            return _processScanner.Scan();
        }

        /// <summary>
        /// Сканнирует руткиты — выполняется первым, до любого другого сканирования.
        /// </summary>
        public IEnumerable<IThreatObject> ScanRootkitFirst()
        {
            if (_options.no_rootkit_check) return new List<IThreatObject>();
            return _rootkitScanner.Scan();
        }

        public IEnumerable<IThreatObject> ScanEverythingElse()
        {
            List<IThreatObject> allThreats = new List<IThreatObject>();

            foreach (var scanner in _otherScanners)
            {
                if (scanner is RegistryScanner && _options.no_scan_registry) continue;

                if (scanner is TaskScanner && _options.no_scan_tasks) continue;
                
                if (scanner is ServiceScanner && _options.no_services) continue;

                if (scanner is FirewallScanner && _options.no_firewall) continue;

                if (scanner is HostsThreatScanner && _options.no_check_hosts) continue;

                if (scanner is WmiScanner && _options.no_scan_wmi) continue;

                if (scanner is UserProfileScanner && _options.no_scan_users) continue;

                allThreats.AddRange(scanner.Scan());
            }

            return allThreats;
        }
    }
}
