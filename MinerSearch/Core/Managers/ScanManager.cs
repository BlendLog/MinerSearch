using MSearch.Core.Scanners;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MSearch.Core.Managers
{
    public interface IThreatScanner
    {
        IEnumerable<IThreatObject> Scan(CancellationToken token);
    }

    public class ScanManager
    {
        private readonly IThreatScanner _rootkitScanner;
        private readonly ProcessScanner _processScanner;
        private readonly IEnumerable<IThreatScanner> _otherScanners;
        private readonly LaunchOptions _options;

        /// <summary>Another scanner of the general stage has started (for the sub-stage label in the UI).</summary>
        public event Action<IThreatScanner> ScannerStarted;

        public ScanManager(RootkitScanner rootkitScanner, ProcessScanner processScanner, IEnumerable<IThreatScanner> otherScanners)
        {
            _rootkitScanner = rootkitScanner;
            _processScanner = processScanner;
            _otherScanners = otherScanners;
            _options = LaunchOptions.GetInstance;
        }

        public IEnumerable<IThreatObject> ScanProcessesOnly(CancellationToken token)
        {
            if (_options.no_runtime) return new List<IThreatObject>();
            return _processScanner.Scan(token);
        }

        /// <summary>
        /// Scans rootkits; runs first, before any other scan.
        /// </summary>
        public IEnumerable<IThreatObject> ScanRootkitFirst(CancellationToken token)
        {
            if (_options.no_rootkit_check) return new List<IThreatObject>();
            return _rootkitScanner.Scan(token);
        }

        /// <summary>
        /// Scans everything else, yielding each scanner's findings as soon as they are collected.
        /// The workflow analyzes them between scanners, so the live counters grow during the stage.
        /// </summary>
        public IEnumerable<IThreatObject> ScanEverythingElse(CancellationToken token)
        {
            foreach (var scanner in _otherScanners)
            {
                token.ThrowIfCancellationRequested();

                if (scanner is RegistryScanner && _options.no_scan_registry) continue;

                if (scanner is TaskScanner && _options.no_scan_tasks) continue;
                
                if (scanner is ServiceScanner && _options.no_services) continue;

                if (scanner is FirewallScanner && _options.no_firewall) continue;

                if (scanner is HostsThreatScanner && _options.no_check_hosts) continue;

                if (scanner is WmiScanner && _options.no_scan_wmi) continue;

                if (scanner is UserProfileScanner && _options.no_scan_users) continue;

                if (scanner is FileSystemThreatScanner && _options.no_scan_filesystem) continue;

                if (scanner is ShellStartupScanner && _options.no_shell_startup) continue;

                Action<IThreatScanner> started = ScannerStarted;
                if (started != null)
                    started(scanner);

                foreach (IThreatObject threat in scanner.Scan(token))
                {
                    token.ThrowIfCancellationRequested();
                    yield return threat;
                }
            }
        }
    }
}
