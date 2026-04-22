using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// SRP: Только обнаружение вредоносных записей в файле hosts.
    /// НЕ принимает решений об удалении — это делает анализатор.
    /// Возвращает HostsThreatObject с заражёнными строками.
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
                List<string> infectedLines = new List<string>();
                List<string> lines = File.ReadLines(hostsPath_full)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Distinct()
                    .ToList();

                foreach (string line in lines)
                {
                    if (line.StartsWith("#"))
                        continue;

                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2)
                        continue;

                    string ipAddress = parts[0];
                    string domain = parts[1].ToLower();
                    if (domain.StartsWith("www."))
                        domain = domain.Substring(4);

                    // Сравниваем через MD5 хэш домена с MSData.hStrings (обфусцированные строки)
                    foreach (HashedString hLine in MSData.GetInstance.hStrings)
                    {
                        if (hLine.OriginalLength <= domain.Length)
                        {
                            string truncatedDomain = domain.Substring(domain.Length - hLine.OriginalLength);
                            if (Utils.StringMD5(truncatedDomain).Equals(hLine.Hash))
                            {
                                infectedLines.Add(line);
                                break;
                            }
                        }
                    }
                }

                if (infectedLines.Count > 0)
                {
                    return new List<IThreatObject> { new HostsThreatObject(hostsPath_full, infectedLines) };
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
