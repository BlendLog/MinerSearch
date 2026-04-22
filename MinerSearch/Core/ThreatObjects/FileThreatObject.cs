using MSearch.Core.ThreatAnalyzers;

namespace MSearch.Core.ThreatObjects
{
    public sealed class FileThreatObject : ThreatObject
    {
        public readonly long MAX_FILE_SIZE = 200 * 1024 * 1024;
        public readonly long MIN_FILE_SIZE = 2112;

        public string FilePath { get; }
        public string FileName { get; }
        public long FileSize { get; }
        public string FileNameOriginal { get; }
        public string FileDescription { get; }
        public string Hash { get; }
        public WinVerifyTrustResult TrustResult { get; internal set; }

        // Специфичные признаки файла
        public bool IsValidSignature => TrustResult == WinVerifyTrustResult.Success;
        public FileContentAnalysisResult AnalysisResult { get; internal set; }
        public bool IsAlreadyAnalyzed { get; internal set; }
        public bool IsSuspiciousPath { get; internal set; }
        public bool IsFileTooLarge { get; internal set; }
        public bool ShouldDisableExecute { get; internal set; }
        
        /// <summary>
        /// Тег источника обнаружения (заполняется сканером). 
        /// Примеры: "obfStr2", "bloated", "bad_bat", "unsigned_sfx", "signature", "suspicious_path"
        /// Анализатор использует этот тег для принятия решений.
        /// </summary>
        public string SourceTag { get; internal set; }

        // Решения (заполняет Анализатор)
        public bool ShouldDeleteFile { get; internal set; }
        public bool ShouldMoveFileToQuarantine { get; internal set; }

        public FileThreatObject(
            string filePath,
            string fileName,
            long fileSize,
            string originalName,
            string description,
            string hash,
            WinVerifyTrustResult trustResult)
            : base(ThreatObjectKind.File, hash)
        {
            FilePath = filePath;
            FileName = fileName;
            FileSize = fileSize;
            FileNameOriginal = originalName;
            FileDescription = description;
            Hash = hash;
            TrustResult = trustResult;
        }
    }
}
