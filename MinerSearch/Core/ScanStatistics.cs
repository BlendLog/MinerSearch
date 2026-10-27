namespace MSearch.Core
{
    /// <summary>
    /// Итоговые счётчики одного сканирования.
    /// </summary>
    public sealed class ScanStatistics
    {
        public int Found { get; set; }
        public int Neutralized { get; set; }
        public int LockedByAv { get; set; }
        public int Suspicious { get; set; }
        public int Skipped { get; set; }
        public string ElapsedTime { get; set; }
    }
}
