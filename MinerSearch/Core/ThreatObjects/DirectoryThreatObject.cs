namespace MSearch.Core.ThreatObjects
{
    public sealed class DirectoryThreatObject : ThreatObject
    {
        public string DirectoryPath { get; }
        public string DirectoryName { get; }
        
        /// <summary>
        /// Detection source tag (set by the scanner).
        /// Examples: "obfStr1" (known malicious), "locked" (blocked by malware).
        /// The analyzer uses this tag to make decisions.
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
