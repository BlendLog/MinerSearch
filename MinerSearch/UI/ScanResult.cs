using MSearch.Core;
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
        Suspended,
        Error,
        Inaccessible,
        Disabled,
        LockedByAntivirus
    }

    public enum ScanObjectType
    {
        Malware,
        Unsafe,
        Suspicious,
        Infected,
        Unknown,
        Rootkit
    }

    public class ScanResult
    {
        public string Type { get; }   
        public string Path { get;  }  
        public string Action { get; } 
        public string Note { get; } 

        public ScanObjectType RawType { get; } 
        public ScanActionType RawAction { get; }

        public ScanResult(ScanObjectType _type, string _Path, ScanActionType actionType, string note = null)
        {
            Type = GetLocalizedType(_type);
            Action = GetLocalizedAction(actionType);
            Path = _Path;
            Note = note;
            RawType = _type;
            RawAction = actionType;
        }

        static string GetLocalizedType(ScanObjectType rawType)
        {
            switch (rawType)
            {
                case ScanObjectType.Malware:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Malware");
                case ScanObjectType.Unsafe:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Unsafe");
                case ScanObjectType.Suspicious:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Suspicious");
                case ScanObjectType.Infected:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Infected");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Unknown");

            }
        }

        static string GetLocalizedAction(ScanActionType action)
        {
            switch (action)
            {
                case ScanActionType.Cured:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Cured");
                case ScanActionType.Quarantine:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Quarantine");
                case ScanActionType.Deleted:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Deleted");
                case ScanActionType.Error:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Error");
                case ScanActionType.Active:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Active");
                case ScanActionType.Terminated:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Terminated");
                case ScanActionType.Inaccessible:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Inaccessible");
                case ScanActionType.Disabled:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Disabled");
                case ScanActionType.LockedByAntivirus:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_LockedByAV");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Skipped");
            }
        }
    }
}
