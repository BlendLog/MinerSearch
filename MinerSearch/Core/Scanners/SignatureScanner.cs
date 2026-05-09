using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSearch.Core.Scanners
{
    public class SignatureScanner : IThreatScanner
    {
        private const long MAX_FILE_SIZE = 200 * 1024 * 1024;
        private const long MIN_FILE_SIZE = 2112;
        private const int MAX_PATH_LENGTH = 240;

        private ConcurrentBag<FileThreatObject> _collectedFiles = new ConcurrentBag<FileThreatObject>();

        public async Task CollectFilesAsync()
        {
            AppConfig.GetInstance.LL.LogHeadMessage("_PreparingFileAnalysis");

            _collectedFiles = new ConcurrentBag<FileThreatObject>();

            List<string> scanPaths = PrepareScanPaths();
            if (scanPaths.Count == 0)
                return;

            scanPaths = scanPaths.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            await Task.Run(() =>
            {
                Parallel.ForEach(scanPaths, path =>
                {
                    if (!Directory.Exists(path))
                        return;

                    try
                    {
                        foreach (string filePath in FileEnumerator.GetFiles(path, "*.exe", 0, LaunchOptions.GetInstance.maxSubfolders))
                        {
                            var fileThreat = CreateFileThreatObject(filePath);
                            if (fileThreat != null)
                            {
                                _collectedFiles.Add(fileThreat);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (LaunchOptions.GetInstance.verbose)
                        {
                            AppConfig.GetInstance.LL.LogErrorMessage("_ErrorScanningDirectory", ex, path);
                        }
                    }
                });
            });

            // Удаляем дубликаты по пути
            var uniqueFiles = _collectedFiles
                .GroupBy(f => f.FilePath, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();

            _collectedFiles = new ConcurrentBag<FileThreatObject>();
            foreach (var file in uniqueFiles)
            {
                _collectedFiles.Add(file);
            }
        }

        public IEnumerable<FileThreatObject> GetFiles()
        {
            return _collectedFiles;
        }

        [Obsolete("Use CollectFilesAsync() + GetFiles() instead")]
        public IEnumerable<IThreatObject> Scan()
        {
            CollectFilesAsync().Wait();
            return _collectedFiles.Cast<IThreatObject>();
        }

        private List<string> PrepareScanPaths()
        {
            var paths = new List<string>();

            if (LaunchOptions.GetInstance.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                IEnumerable<DriveInfo> localDrives = allDrives.Where(drive =>
                    drive.DriveType == DriveType.Fixed &&
                    !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));

                foreach (DriveInfo drive in localDrives)
                {
                    paths.Add(drive.Name);
                }
                return paths;
            }

            if (!string.IsNullOrEmpty(LaunchOptions.GetInstance.selectedPath) &&
                Directory.Exists(LaunchOptions.GetInstance.selectedPath))
            {
                paths.Add(LaunchOptions.GetInstance.selectedPath);
                return paths;
            }

            return MSData.GetInstance.obfStr6;
        }

        private FileThreatObject CreateFileThreatObject(string filePath)
        {
            string normalizedPath = FileSystemManager.NormalizeExtendedPath(filePath);

            if (normalizedPath.Length > MAX_PATH_LENGTH)
                return null;

            long fileSize;
            try { fileSize = new FileInfo(normalizedPath).Length; }
            catch { return null; }

            if (fileSize > MAX_FILE_SIZE || fileSize < MIN_FILE_SIZE)
                return null;

            return new FileThreatObject(
                normalizedPath,
                Path.GetFileName(normalizedPath),
                fileSize,
                string.Empty,
                string.Empty,
                string.Empty,
                WinVerifyTrustResult.Error)
            {
                SourceTag = "signature_scan"
            };
        }
    }
}
