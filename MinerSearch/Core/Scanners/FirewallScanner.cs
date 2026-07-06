using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// Сканирует ВСЕ правила брандмауэра Windows.
    /// Фильтрация и маркировка — задача анализатора.
    /// </summary>
    public class FirewallScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            List<IThreatObject> results = new List<IThreatObject>();

            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                if (typeFWPolicy2 == null) return results;

                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);
                INetFwRules rules = fwPolicy2.Rules;

                foreach (INetFwRule rule in rules)
                {
                    if (string.IsNullOrEmpty(rule.ApplicationName) || rule.ApplicationName.Equals("system", StringComparison.OrdinalIgnoreCase)) continue;                    

                    var direction = rule.Direction;
                    var action = rule.Action;
                    string protocol = rule.Protocol.ToString();

                    results.Add(new FirewallRuleThreatObject(
                        rule.Name,
                        rule.ApplicationName,
                        direction,
                        action,
                        protocol));
                }
            }
            catch (Exception)
            {
                // Игнорируем ошибки доступа к Firewall API
            }

            return results;
        }
    }
}
