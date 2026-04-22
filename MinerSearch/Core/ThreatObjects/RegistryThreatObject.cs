using Microsoft.Win32;
using System.Text;

namespace MSearch.Core.ThreatObjects
{
    public enum RegistryNodeType
    {
        Key,
        Value
    }

    public sealed class RegistryThreatObject : ThreatObject
    {
        public string Hive { get; }
        public string KeyPath { get; }
        public RegistryNodeType NodeType { get; }
        public string ValueName { get; }
        public string ValueData { get; }
        public RegistryValueKind ValueKind { get; }
        public bool IsAccessDenied { get; }
        public FileThreatObject LinkedFile { get; }
        public string SectionName { get; set; }


        public bool ActionDelete { get; internal set; }
        public bool ActionSetData { get; internal set; }
        public string TargetData { get; internal set; }
        public RegistryValueKind TargetKind { get; internal set; }
        public bool ActionSetSibling { get; internal set; }
        public string SiblingName { get; internal set; }
        public string SiblingData { get; internal set; }
        public RegistryValueKind SiblingKind { get; internal set; }
        public bool ActionUnlockFirst { get; internal set; }
        public bool ActionDeleteParentKey { get; internal set; }
        public bool ActionRemoveDefenderExclusion { get; internal set; }

        public RegistryThreatObject(
            string hive,
            string keyPath,
            RegistryNodeType nodeType,
            string valueName,
            string valueData,
            RegistryValueKind valueKind,
            bool accessDenied,
            FileThreatObject linkedFile = null) : base(ThreatObjectKind.RegistryObject, $@"{hive}\{keyPath}\{valueName}")
        {
            Hive = hive;
            KeyPath = keyPath;
            NodeType = nodeType;
            ValueName = valueName ?? string.Empty;
            ValueData = valueData ?? string.Empty;
            ValueKind = valueKind;
            IsAccessDenied = accessDenied;
            LinkedFile = linkedFile;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb
                .Append($"Hive: {Hive}\n")
                .Append($"KeyPath: {KeyPath}\n")
                .Append($"NodeType: {NodeType}\n")
                .Append($"ValueName: {ValueName}\n")
                .Append($"ValueData: {ValueData}\n")
                .Append($"ValueKind: {ValueKind}\n")
                .Append($"IsAccessDenied: {IsAccessDenied}\n");

            if (LinkedFile != null)
            {
                sb.Append($"LinkedFile: {LinkedFile.FilePath}");
            }

            return sb.ToString();
        }
    }
}
