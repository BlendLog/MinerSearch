namespace MSearch.Core.ThreatObjects
{
    public sealed class DirectoryThreatObject : ThreatObject
    {
        public string DirectoryPath { get; }
        public string DirectoryName { get; }
        
        /// <summary>
        /// Тег источника обнаружения (заполняется сканером).
        /// Примеры: "obfStr1" (известный вредоносный), "locked" (заблокирован вирусом)
        /// Анализатор использует этот тег для принятия решений.
        /// </summary>
        public string SourceTag { get; }

        // Решения (заполняет Анализатор)
        public bool ShouldDeleteDirectory { get; internal set; }
        public bool ShouldUnlockAndDelete { get; internal set; }

        public DirectoryThreatObject(
            string directoryPath,
            string directoryName,
            string sourceTag)
            : base(ThreatObjectKind.Directory, directoryPath)
        {
            DirectoryPath = directoryPath;
            DirectoryName = directoryName;
            SourceTag = sourceTag;
        }
    }
}
