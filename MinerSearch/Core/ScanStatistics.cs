namespace MSearch.Core
{
    /// <summary>
    /// Final counters of one scan run.
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
