using MSearch.Core;

namespace MSearch
{
    public enum QuarantineItemType
    {
        File,
        Task,
        Service,
        Registry
    }

    public class QuarantineItem
    {
        public string OriginalPath { get; set; }
        public string FileSize { get; set; }
        public string FileHash { get; set; }
        public QuarantineItemType ItemType { get; set; } = QuarantineItemType.File;

        public static string GetLocalizedTypeName(QuarantineItemType type)
        {
            switch (type)
            {
                case QuarantineItemType.Service:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ObjectType_Service");
                case QuarantineItemType.Task:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ObjectType_ScheduledTask");
                case QuarantineItemType.Registry:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ObjectType_RegistryObject");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ObjectType_File");
            }
        }
    }
}
