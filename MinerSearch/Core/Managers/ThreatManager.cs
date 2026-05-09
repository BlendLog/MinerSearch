using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSearch.Core.Managers
{
    public sealed class ThreatManager
    {
        private readonly Dictionary<ThreatObjectKind, IThreatAnalyzer> _analyzers;
        private readonly Dictionary<string, ThreatDecision> _aggregatedDecisions;

        private static bool _johnPatternsFound = false;

        // Приоритет действий: Deleted > Quarantine > Cured > Skipped
        private static readonly Dictionary<ScanActionType, int> _actionPriority;

        static ThreatManager()
        {
            _actionPriority = new Dictionary<ScanActionType, int>();
            _actionPriority.Add(ScanActionType.Deleted, 1);
            _actionPriority.Add(ScanActionType.Quarantine, 2);
            _actionPriority.Add(ScanActionType.Cured, 3);
            _actionPriority.Add(ScanActionType.Skipped, 4);
        }

        public ThreatManager(IEnumerable<IThreatAnalyzer> analyzers)
        {
            _analyzers = analyzers.ToDictionary(a => a.Kind);
            _aggregatedDecisions = new Dictionary<string, ThreatDecision>(StringComparer.OrdinalIgnoreCase);
        }

        public static void ResetJohnPatternsFlag()
        {
            _johnPatternsFound = false;
        }

        public static bool IsJohnPatternsFound()
        {
            return _johnPatternsFound;
        }

        public IEnumerable<ThreatDecision> Analyze(IEnumerable<IThreatObject> threats)
        {
            _aggregatedDecisions.Clear();
            ResetJohnPatternsFlag();

            foreach (var threat in threats)
            {
                if (threat == null) continue;
                
                // Сбор JohnPatterns из каждого ThreatObject
                CheckForJohnPatterns(threat);
                
                if (_analyzers.TryGetValue(threat.Kind, out var analyzer))
                {
                    var decisions = analyzer.Analyze(threat);
                    if (decisions != null)
                    {
                        foreach (var decision in decisions)
                        {
                            if (decision != null)
                            {
                                _AggregateDecision(decision);
                            }
                        }
                    }
                }
            }

            return _aggregatedDecisions.Values;
        }

        private void CheckForJohnPatterns(IThreatObject target)
        {
            if (MSData.GetInstance.JohnPatterns == null || MSData.GetInstance.JohnPatterns.Count == 0)
                return;

            string[] texts = ExtractTexts(target);
            foreach (string text in texts)
            {
                if (string.IsNullOrEmpty(text)) continue;
                foreach (string pattern in MSData.GetInstance.JohnPatterns)
                {
                    if (text.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _johnPatternsFound = true;
                        return;
                    }
                }
            }
        }

        private string[] ExtractTexts(IThreatObject target)
        {
            List<string> texts = new List<string>();

            switch (target.Kind)
            {
                case ThreatObjectKind.File:
                    var file = target as FileThreatObject;
                    if (file?.FilePath != null)
                        texts.Add(file.FilePath);
                    break;

                case ThreatObjectKind.RegistryObject:
                    var reg = target as RegistryThreatObject;
                    if (reg?.ValueData != null)
                        texts.Add(reg.ValueData);
                    break;

                case ThreatObjectKind.ScheduledTask:
                    var task = target as TaskThreatObject;
                    if (task?.Info != null)
                    {
                        if (task.Info.Path != null) texts.Add(task.Info.Path);
                        foreach (var action in task.Info.ExecActions)
                        {
                            if (action?.Path != null) texts.Add(action.Path);
                            if (action?.Arguments != null) texts.Add(action.Arguments);
                        }
                    }
                    break;

                case ThreatObjectKind.Process:
                    var proc = target as ProcessThreatObject;
                    if (proc?.ProcessName != null) texts.Add(proc.ProcessName);
                    if (proc?.ProcessArgs != null) texts.Add(proc.ProcessArgs);
                    break;

                case ThreatObjectKind.Directory:
                    var dir = target as DirectoryThreatObject;
                    if (dir?.DirectoryPath != null) texts.Add(dir.DirectoryPath);
                    if (dir?.SourceTag != null) texts.Add(dir.SourceTag);
                    break;
            }

            return texts.ToArray();
        }

        private void _AggregateDecision(ThreatDecision decision)
        {
            string id = decision.Target.Id;

            if (!_aggregatedDecisions.TryGetValue(id, out var existing))
            {
                _aggregatedDecisions[id] = decision;
                return;
            }

            // Конфликт — выбираем приоритетное действие
            if (ActionPriority(decision.ActionType) < ActionPriority(existing.ActionType))
            {
                _aggregatedDecisions[id] = decision;
            }
        }

        private static int ActionPriority(ScanActionType action)
        {
            return _actionPriority.TryGetValue(action, out int priority) ? priority : 99;
        }
    }
}
