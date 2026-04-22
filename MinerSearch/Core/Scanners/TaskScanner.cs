using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MSearch.Core.Scanners
{
    public class TaskScanner : IThreatScanner
    {
        private readonly TaskParser _parser = new TaskParser();

        public IEnumerable<IThreatObject> Scan()
        {
            var allTasks = _parser.GetAllTasks();
            List<IThreatObject> threatTasks = new List<IThreatObject>();

            if (allTasks == null) return threatTasks;

            foreach (var task in allTasks.OrderBy(t => t.Name))
            {
                if (task.ExecActions.Count == 0) continue;

                // Берем первое действие для упрощения (или можно делать объект на каждое действие)
                var action = task.ExecActions[0];

                FileThreatObject linkedFile = null;
                if (!string.IsNullOrEmpty(action.Path))
                {
                    // Простая попытка резолва (без сложной логики парсинга аргументов)
                    string rawPath = Environment.ExpandEnvironmentVariables(action.Path.Replace("\"", ""));
                    string resolved = FileSystemManager.ResolveExecutablePath(rawPath);

                    if (File.Exists(resolved))
                    {
                        // Создаем объект фактов о файле
                        // WinTrust вызываем, чтобы передать статус подписи
                        var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(resolved, false);
                        FileInfo info = new FileInfo(resolved);
                        var versionInfo = FileVersionInfo.GetVersionInfo(resolved);
                        string originalFileName = versionInfo.OriginalFilename ?? string.Empty;
                        string fileDescription = versionInfo.FileDescription ?? string.Empty;

                        linkedFile = new FileThreatObject(resolved, Path.GetFileName(resolved), info.Length, originalFileName, fileDescription, FileChecker.CalculateSHA1(resolved), trust);
                    }
                }

                threatTasks.Add(new TaskThreatObject(task, linkedFile));
            }

            return threatTasks;
        }
    }
}
