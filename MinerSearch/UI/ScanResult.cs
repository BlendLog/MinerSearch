using DBase;
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
        public string Class { get; }

        /// <summary>Уникальный Id ThreatObject для корректного подсчёта уникальных угроз</summary>
        public string ThreatObjectId { get; }

        public ScanObjectType RawType { get; }
        public ScanActionType RawAction { get; }
        public ThreatObjectKind RawClass { get; }

        public ScanResult(ScanObjectType _type, string _Path, ScanActionType actionType, string note = null, string threatObjectId = null, ThreatObjectKind @class = default)
        {
            Type = GetLocalizedType(_type);
            Action = GetLocalizedAction(actionType);
            Path = _Path;
            Note = note;
            ThreatObjectId = threatObjectId;
            Class = GetLocalizedClass(@class);
            RawType = _type;
            RawAction = actionType;
            RawClass = @class;
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

        static string GetLocalizedClass(ThreatObjectKind kind)
        {
            switch (kind)
            {
                case ThreatObjectKind.Process:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Process");
                case ThreatObjectKind.RegistryObject:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_RegistryObject");
                case ThreatObjectKind.File:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_File");
                case ThreatObjectKind.Directory:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Directory");
                case ThreatObjectKind.FirewallRule:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_FirewallRule");
                case ThreatObjectKind.UserProfile:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_UserProfile");
                case ThreatObjectKind.WmiSubscription:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_WmiSubscription");
                case ThreatObjectKind.ScheduledTask:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_ScheduledTask");
                case ThreatObjectKind.Service:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Service");
                case ThreatObjectKind.ShellStartupFile:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_ShellStartupFile");
                case ThreatObjectKind.Hosts:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Hosts");
                case ThreatObjectKind.Other:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Other");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Unknown");
            }
        }
    }
}
