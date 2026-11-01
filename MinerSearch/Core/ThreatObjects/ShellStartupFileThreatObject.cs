namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// Represents a file from the Shell startup folder (User Startup + Common Startup).
    /// Can be a regular file (.exe, .bat, .vbs, etc.) or a shortcut (.lnk).
    /// </summary>
    public sealed class ShellStartupFileThreatObject : ThreatObject
    {
        public string FilePath { get; }
        public string FileName { get; }
        public string Extension { get; }

        // Специфичные для ярлыков
        public bool IsShortcut => Extension == ".lnk";
        public string ShortcutTargetPath { get; }
        public string ShortcutTargetArgs { get; }

        public FileThreatObject ShortcutTargetFile { get; set; }

        // Признаки подозрительности (для обычных файлов)
        public bool HasInvisibleChars { get; }
        public bool IsSfx { get; }
        public bool HasHiddenAttr { get; }
        public bool IsDotNetHighEntropy { get; }

        // Флаги действий (заполняет Анализатор)
        public bool ShouldDeleteFile { get; internal set; }
        public bool ShouldMoveToQuarantine { get; internal set; }
        public bool ShouldDisableShortcutTarget { get; internal set; }

        public ShellStartupFileThreatObject(
            string filePath,
            string fileName,
            string extension,
            string shortcutTargetPath,
            string shortcutTargetArgs,
            bool hasInvisibleChars,
            bool isSfx,
            bool hasHiddenAttr,
            bool isDotNetHighEntropy,
            FileThreatObject shortcutTargetFile = null)
            : base(ThreatObjectKind.ShellStartupFile, filePath)
        {
            FilePath = filePath;
            FileName = fileName;
            Extension = extension;
            ShortcutTargetPath = shortcutTargetPath;
            ShortcutTargetArgs = shortcutTargetArgs;
            HasInvisibleChars = hasInvisibleChars;
            IsSfx = isSfx;
            HasHiddenAttr = hasHiddenAttr;
            IsDotNetHighEntropy = isDotNetHighEntropy;
            ShortcutTargetFile = shortcutTargetFile;
        }
    }
}
