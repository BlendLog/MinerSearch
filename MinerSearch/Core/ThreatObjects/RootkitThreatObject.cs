using MSearch.Core;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// Data object for a detected R77 rootkit.
    /// IsRootkitPresent is set by the scanner when the current process is infected.
    /// R77Processes and DialerPids are filled by the analyzer.
    /// WasNeutralized and ConfigRemoved are filled by the handler.
    /// </summary>
    public sealed class RootkitThreatObject : ThreatObject
    {
        /// <summary>
        /// true if the current process is infected with R77 (set by the scanner).
        /// </summary>
        public bool IsRootkitPresent { get; }

        /// <summary>
        /// List of processes with rootkit signatures (R77/R77_SERVICE/R77_HELPER).
        /// Filled by the analyzer.
        /// </summary>
        public Win32Wrapper.Native.R77_PROCESS[] R77Processes { get; internal set; }

        /// <summary>
        /// PIDs of dialer processes that were suspended.
        /// Filled by the analyzer.
        /// </summary>
        public int[] DialerPids { get; internal set; }

        /// <summary>
        /// true if all R77 processes were neutralized successfully.
        /// Filled by the handler.
        /// </summary>
        public bool WasNeutralized { get; internal set; }

        /// <summary>
        /// true if the rootkit configuration was removed from the registry.
        /// Filled by the handler.
        /// </summary>
        public bool ConfigRemoved { get; internal set; }

        public RootkitThreatObject(bool isRootkitPresent)
            : base(ThreatObjectKind.Other, "Rootkit")
        {
            IsRootkitPresent = isRootkitPresent;
        }
    }
}
