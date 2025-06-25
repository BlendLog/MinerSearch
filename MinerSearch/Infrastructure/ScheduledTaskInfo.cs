using System;
using System.Collections.Generic;

namespace MSearch.Infrastructure
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
}