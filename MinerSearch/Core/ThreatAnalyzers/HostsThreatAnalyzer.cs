using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MSearch.Core.ThreatAnalyzers
{
    /// <summary>
    /// SRP: Анализирует записи hosts, принимает решения.
    /// Угроза — только глушилки (127/8, 0/8, 8.8.8.8, 8.8.4.4, 255.255.255.255):
    /// ScanObjectType.Infected. Внешний IP на AV-домен (Redirect) не трогаем —
    /// только нейтральная строка в лог, без решения и без очистки.
    /// НЕ увеличивает счётчики — это делает CleanManager.
    /// </summary>
    public sealed class HostsThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Hosts;

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();
        LaunchOptions _options = LaunchOptions.GetInstance;

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            var hostsThreat = threat as HostsThreatObject;
            if (hostsThreat == null)
                yield break;

            // Заголовок логируем один раз
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanHosts");
                        _headerLogged = true;
                    }
                }
            }

            if (hostsThreat.Entries == null || hostsThreat.Entries.Count == 0) yield break;

            // Вердикт: матчим домены по базе, делим строки по IP
            foreach (HostsEntry entry in hostsThreat.Entries)
            {
                if (entry == null || entry.Domains == null || entry.Domains.Count == 0)
                    continue;

                bool isAvEntry = false;
                foreach (string domain in entry.Domains)
                {
                    if (IsAvDomain(domain))
                    {
                        isAvEntry = true;
                        break;
                    }
                }

                if (!isAvEntry)
                    continue;

                if (IsBlackholeIp(entry.Ip))
                {
                    if (!hostsThreat.BlockedLines.Contains(entry.RawLine))
                        hostsThreat.BlockedLines.Add(entry.RawLine);
                }
                else
                {
                    if (!hostsThreat.RedirectLines.Contains(entry.RawLine))
                        hostsThreat.RedirectLines.Add(entry.RawLine);
                }
            }

            if (hostsThreat.BlockedLinesCount == 0 && hostsThreat.RedirectLinesCount == 0) yield break;

            if (hostsThreat.BlockedLinesCount > 0)
                AppConfig.GetInstance.LL.LogCautionMessage("_InfectedHosts", hostsThreat.HostsFilePath);

            // Redirect не трогаем: только нейтральные строки в лог, без решения и очистки
            foreach (string redirectLine in hostsThreat.RedirectLines)
            {
                AppConfig.GetInstance.LL.LogMessage("[i]", "_SuspiciousHostsEntry", redirectLine, ConsoleColor.Gray);
            }

            if (hostsThreat.BlockedLinesCount == 0) yield break;

            if (!_options.ScanOnly)
            {
                hostsThreat.ShouldQuarantineFile = true;
                hostsThreat.ShouldRemoveBlockedLines = true;
                AppConfig.GetInstance.LL.LogSuccessMessage("_FileCopy_MarkedToMoveQuarantine", hostsThreat.HostsFilePath);
            }

            // Одно решение на файл: только глушилки (Infected, risk 3)
            yield return new ThreatDecision(hostsThreat, riskLevel: 3, ScanObjectType.Infected);
        }

        /// <summary>
        /// Сверка домена с MSData.hStrings через MD5-суффикс.
        /// </summary>
        internal static bool IsAvDomain(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                return false;

            string d = domain.ToLower();
            if (d.StartsWith("www."))
                d = d.Substring(4);

            foreach (HashedString hLine in MSData.GetInstance.hStrings)
            {
                if (hLine.OriginalLength > d.Length)
                    continue;

                string tail = d.Substring(d.Length - hLine.OriginalLength);
                if (!Utils.StringMD5(tail).Equals(hLine.Hash, StringComparison.OrdinalIgnoreCase))
                    continue;

                // Граница домена
                if (tail.Length == d.Length || d[d.Length - tail.Length - 1] == '.')
                    return true;
            }

            return false;
        }

        internal static bool IsBlackholeIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return false;

            IPAddress addr;
            if (!IPAddress.TryParse(ip.Trim(), out addr))
                return false;

            if (addr.AddressFamily != AddressFamily.InterNetwork)
                return false;

            byte[] b = addr.GetAddressBytes();
            if (b[0] == 127)
                return true;
            if (b[0] == 0)
                return true;

            string normalized = addr.ToString();
            if (normalized.Equals("8.8.8.8", StringComparison.Ordinal) ||
                normalized.Equals("8.8.4.4", StringComparison.Ordinal) ||
                normalized.Equals("9.9.9.9", StringComparison.Ordinal) ||
                normalized.Equals("255.255.255.255", StringComparison.Ordinal))
                return true;

            return false;
        }
    }
}
