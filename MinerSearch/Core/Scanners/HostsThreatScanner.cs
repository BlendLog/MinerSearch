using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// SRP: Только сбор значимых строк файла hosts (IP + домены).
    /// </summary>
    public class HostsThreatScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            string hostsPath_full = $"{AppConfig.GetInstance.drive_letter}{MSData.GetInstance.queries["h0sts"]}";

            if (!File.Exists(hostsPath_full))
                return new List<IThreatObject>();

            try
            {
                List<HostsEntry> entries = new List<HostsEntry>();
                List<string> lines = File.ReadLines(hostsPath_full)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Distinct()
                    .ToList();

                foreach (string line in lines)
                {
                    // BOM первой строки + ведущие пробелы: комментарий может быть с отступом
                    string trimmed = line.TrimStart('\uFEFF', ' ', '\t');
                    if (trimmed.Length == 0)
                        continue;
                    if (trimmed[0] == '#')
                        continue;

                    // Отрезать инлайн-комментарий "IP host # comment"
                    int hashIndex = trimmed.IndexOf('#');
                    string content = hashIndex >= 0 ? trimmed.Substring(0, hashIndex) : trimmed;

                    string[] parts = content.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2)
                        continue;

                    string ipAddress = parts[0].Trim();

                    // IPv6 пока не обрабатываем
                    if (ipAddress.IndexOf(':') >= 0)
                        continue;

                    List<string> domains = new List<string>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string domain = parts[i].Trim().ToLower();
                        if (domain.Length == 0 || domain[0] == '#')
                            break;
                        domains.Add(domain);
                    }

                    if (domains.Count == 0)
                        continue;

                    entries.Add(new HostsEntry(line, ipAddress, domains));
                }

                if (entries.Count > 0)
                {
                    return new List<IThreatObject> { new HostsThreatObject(hostsPath_full, entries) };
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, hostsPath_full, "_ErrorReadHosts");
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, hostsPath_full, "_ErrorReadHosts");
            }

            return new List<IThreatObject>();
        }
    }
}
