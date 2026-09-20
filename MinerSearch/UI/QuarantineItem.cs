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
    }
}
