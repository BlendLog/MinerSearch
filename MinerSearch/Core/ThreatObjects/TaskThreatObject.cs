using System;
using System.Collections.Generic;

namespace MSearch.Core.ThreatObjects
{
    public class ScheduledTaskInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid Guid { get; set; }
        public string XmlPath { get; set; }

        public bool IsEnabled { get; set; }
        public List<ExecActionInfo> ExecActions { get; set; } = new List<ExecActionInfo>();
    }

    public class ExecActionInfo
    {
        public string Path { get; set; }
        public string Arguments { get; set; }
    }

    public sealed class TaskThreatObject : ThreatObject
    {
        // Факты (Payload)
        public ScheduledTaskInfo Info { get; }
        public FileThreatObject LinkedFile { get; }


        // Решения (заполняет Анализатор)
        public FileThreatObject LinkedFileFromArgs { get; internal set; }
        public bool ActionDeleteTask { get; internal set; }
        public bool ActionDeleteFile { get; internal set; }
        public bool ActionDeleteAdditionalFile { get; internal set; }
        public string DetectionReasonRes { get; internal set; } // Чтобы знать, почему удаляем (Missing file / Malicious arg)

        public TaskThreatObject(ScheduledTaskInfo info, FileThreatObject linkedFile) : base(ThreatObjectKind.ScheduledTask, $"{info.Path}\\{info.Name}")
        {
            Info = info;
            LinkedFile = linkedFile;
        }
    }
}
