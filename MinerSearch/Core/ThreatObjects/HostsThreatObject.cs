using MSearch.Core.ThreatDecisions;
using System.Collections.Generic;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// SRP: one meaningful hosts-file line (not a comment and not empty).
    /// Raw scanner data: the IP as written plus normalized domains. Contains no verdict.
    /// </summary>
    public sealed class HostsEntry
    {
        public string RawLine { get; }
        public string Ip { get; }
        public List<string> Domains { get; }

        public HostsEntry(string rawLine, string ip, List<string> domains)
        {
            RawLine = rawLine;
            Ip = ip;
            Domains = domains ?? new List<string>();
        }
    }

    /// <summary>
    /// SRP: represents an infected hosts file.
    /// Holds the file path, raw scanner entries and the analyzer verdict
    /// (Blocked — exact blackhole, Redirect — external IP for an AV domain).
    /// </summary>
    public sealed class HostsThreatObject : ThreatObject
    {
        public string HostsFilePath { get; }

        /// <summary>
        /// All meaningful file lines (collected by the scanner, evaluated by the analyzer).
        /// </summary>
        public List<HostsEntry> Entries { get; }

        /// <summary>
        /// Blackhole entries: an AV domain resolved to a blocking address.
        /// </summary>
        public List<string> BlockedLines { get; }

        public int BlockedLinesCount => BlockedLines != null ? BlockedLines.Count : 0;

        /// <summary>
        /// Redirect entries: an AV domain resolved to another external IP. Left alone —
        /// only a neutral info log in the analyzer; they are not cleaned.
        /// </summary>
        public List<string> RedirectLines { get; }

        public int RedirectLinesCount => RedirectLines != null ? RedirectLines.Count : 0;

        /// <summary>
        /// Compatibility: malicious lines are blackhole entries only.
        /// </summary>
        public List<string> InfectedLines
        {
            get
            {
                List<string> all = new List<string>();
                if (BlockedLines != null) all.AddRange(BlockedLines);
                return all;
            }
        }

        public int InfectedLinesCount => BlockedLinesCount;

        /// <summary>
        /// Flag: add the hosts file to quarantine.
        /// </summary>
        public bool ShouldQuarantineFile { get; internal set; }

        /// <summary>
        /// Flag: remove blackhole entries.
        /// </summary>
        public bool ShouldRemoveBlockedLines { get; internal set; }

        /// <summary>
        /// Compatibility: remove malicious lines (= blackhole entries).
        /// </summary>
        public bool ShouldRemoveInfectedLines
        {
            get { return ShouldRemoveBlockedLines; }
            internal set { ShouldRemoveBlockedLines = value; }
        }

        public HostsThreatObject(string hostsFilePath, List<HostsEntry> entries)
            : base(ThreatObjectKind.Hosts, hostsFilePath)
        {
            HostsFilePath = hostsFilePath;
            Entries = entries ?? new List<HostsEntry>();
            BlockedLines = new List<string>();
            RedirectLines = new List<string>();
            ShouldQuarantineFile = true;
            ShouldRemoveBlockedLines = true;
        }

        public HostsThreatObject(string hostsFilePath, List<string> infectedLines)
            : base(ThreatObjectKind.Hosts, hostsFilePath)
        {
            HostsFilePath = hostsFilePath;
            Entries = new List<HostsEntry>();
            BlockedLines = infectedLines ?? new List<string>();
            RedirectLines = new List<string>();
            ShouldQuarantineFile = true;
            ShouldRemoveBlockedLines = true;
        }
    }
}
