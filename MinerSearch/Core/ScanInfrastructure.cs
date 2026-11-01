using MSearch.Core.Managers;
using MSearch.Core.ThreatAnalyzers;

namespace MSearch.Core
{
    /// <summary>
    /// Set of scanners, analyzers and managers created for one scan run.
    /// </summary>
    public sealed class ScanInfrastructure
    {
        public MinerSearchScanState State { get; set; }
        public SignatureFileAnalyzer SignatureAnalyzer { get; set; }
        public ScanManager ScanManager { get; set; }
        public ThreatManager ThreatManager { get; set; }
        public CleanManager CleanManager { get; set; }
    }
}
