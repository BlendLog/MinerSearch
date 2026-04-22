using MSearch.Core.ThreatDecisions;
using System.Collections.Generic;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// SRP: Представляет заражённый файл hosts.
    /// Хранит путь к файлу и список вредоносных строк.
    /// </summary>
    public sealed class HostsThreatObject : ThreatObject
    {
        public string HostsFilePath { get; }
        public List<string> InfectedLines { get; }
        public int InfectedLinesCount => InfectedLines?.Count ?? 0;

        /// <summary>
        /// Флаг: добавить файл hosts в карантин.
        /// </summary>
        public bool ShouldQuarantineFile { get; internal set; }

        /// <summary>
        /// Флаг: удалить вредоносные строки из hosts.
        /// </summary>
        public bool ShouldRemoveInfectedLines { get; internal set; }

        public HostsThreatObject(string hostsFilePath, List<string> infectedLines)
            : base(ThreatObjectKind.Hosts, hostsFilePath)
        {
            HostsFilePath = hostsFilePath;
            InfectedLines = infectedLines ?? new List<string>();
            ShouldQuarantineFile = true;
            ShouldRemoveInfectedLines = true;
        }
    }
}
