using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSearch
{
    public enum ScanActionType
    {
        Cured,
        Deleted,
        Quarantine,
        Skipped,
        Active,
        Terminated,
        Error,
        Inaccassible
    }

    public enum ScanObjectType
    {
        Malware,
        Suspicious,
        Infected,
        Unknown
    }

    public class ScanResult
    {
        public string Type { get; }   
        public string Path { get;  }  
        public string Action { get; } 

        public ScanObjectType RawType { get; } 
        public ScanActionType RawAction { get; }

        public ScanResult(ScanObjectType _type, string _Path, ScanActionType actionType)
        {
            Type = GetLocalizedType(_type);
            Action = GetLocalizedAction(actionType);
            Path = _Path;
            RawType = _type;
            RawAction = actionType;
        }

        static string GetLocalizedType(ScanObjectType rawType)
        {
            switch (rawType)
            {
                case ScanObjectType.Malware:
                    return Program.LL.GetLocalizedString("_ThreatType_Malware");
                case ScanObjectType.Suspicious:
                    return Program.LL.GetLocalizedString("_ThreatType_Suspicious");
                case ScanObjectType.Infected:
                    return Program.LL.GetLocalizedString("_ThreatType_Infected");
                default:
                    return Program.LL.GetLocalizedString("_ThreatType_Unknown");

            }
        }

        static string GetLocalizedAction(ScanActionType action)
        {
            switch (action)
            {
                case ScanActionType.Cured:
                    return Program.LL.GetLocalizedString("_ActionType_Cured");
                case ScanActionType.Quarantine:
                    return Program.LL.GetLocalizedString("_ActionType_Quarantine");
                case ScanActionType.Deleted:
                    return Program.LL.GetLocalizedString("_ActionType_Deleted");
                case ScanActionType.Error:
                    return Program.LL.GetLocalizedString("_ActionType_Error");
                case ScanActionType.Active:
                    return Program.LL.GetLocalizedString("_ActionType_Active");
                case ScanActionType.Terminated:
                    return Program.LL.GetLocalizedString("_ActionType_Terminated");
                case ScanActionType.Inaccassible:
                    return Program.LL.GetLocalizedString("_ActionType_Inaccessible");
                default:
                    return Program.LL.GetLocalizedString("_ActionType_Skipped");
            }
        }
    }
}
