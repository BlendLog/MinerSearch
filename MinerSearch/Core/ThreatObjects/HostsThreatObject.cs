using MSearch.Core.ThreatDecisions;
using System.Collections.Generic;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// SRP: Одна значимая (не комментарий/не пустая) строка файла hosts.
    /// Сырьё от сканера: IP как в файле + нормализованные домены. Вердикт не содержит.
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
    /// SRP: Представляет заражённый файл hosts.
    /// Хранит путь к файлу, сырые записи от сканера и вердикт анализатора
    /// (Blocked — точная глушилка, Redirect — внешний IP на AV-домен).
    /// </summary>
    public sealed class HostsThreatObject : ThreatObject
    {
        public string HostsFilePath { get; }

        /// <summary>
        /// Все значимые строки файла (собирает сканер, оценивает анализатор).
        /// </summary>
        public List<HostsEntry> Entries { get; }

        /// <summary>
        /// Строки-глушилки: AV-домен на 127/8, 0/8, 8.8.8.8, 8.8.4.4, 255.255.255.255.
        /// </summary>
        public List<string> BlockedLines { get; }

        public int BlockedLinesCount => BlockedLines != null ? BlockedLines.Count : 0;

        /// <summary>
        /// Строки-редиректы: AV-домен на прочий внешний IP. Не трогаем —
        /// только нейтральный инфо-лог в анализаторе, в очистку не идут.
        /// </summary>
        public List<string> RedirectLines { get; }

        public int RedirectLinesCount => RedirectLines != null ? RedirectLines.Count : 0;

        /// <summary>
        /// Совместимость: вредоносные строки = только глушилки.
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
        /// Флаг: добавить файл hosts в карантин.
        /// </summary>
        public bool ShouldQuarantineFile { get; internal set; }

        /// <summary>
        /// Флаг: удалить строки-глушилки.
        /// </summary>
        public bool ShouldRemoveBlockedLines { get; internal set; }

        /// <summary>
        /// Совместимость: удалить вредоносные строки (= глушилки).
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
