using DBase;
using MSearch.Core.Managers;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

namespace MSearch.Core.Scanners
{
    /// <summary>
    /// SRP: Только обнаружение файлов и каталогов. НЕ принимает решений об удалении.
    /// Возвращает найденные объекты с метаданными (SourceTag) для анализатора.
    /// </summary>
    public class FileSystemThreatScanner : IThreatScanner
    {
        public IEnumerable<IThreatObject> Scan()
        {
            var results = new List<IThreatObject>();

            // Подготовка специфичных каталогов (Desktop, Downloads)
            PrepareSpecialDirectories();

            // 1. Известные вредоносные каталоги (obfStr1)
            ScanKnownDirectories(results);

            // 2. Заблокированные/пустые каталоги (obfStr5)
            ScanLockedDirectories(results);

            // 3. Известные вредоносные файлы (obfStr2)
            ScanKnownFiles(results);

            // 4. Каталоги по паттерну (GUID в ProgramData)
            ScanPatternBasedDirectories(results);

            // 5. Bloated файлы в Windows
            ScanBloatedFiles(results);

            // 6. Файлы в CommonApplicationData (bat + SFX)
            ScanCommonAppDataFiles(results);

            return results;
        }

        /// <summary>
        /// Подготавливает специфичные каталоги на рабочем столе и в загрузках.
        /// Это каталоги типа "autologger", "av_block_remover" которые должны удаляться.
        /// </summary>
        private void PrepareSpecialDirectories()
        {
            if (AppConfig.GetInstance.WinPEMode || AppConfig.GetInstance.RunAsSystem)
                return;

            string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DesktopPath != null)
            {
                MSData.GetInstance.obfStr5.Add(Path.Combine(DesktopPath, "aut~olo~~gger".Replace("~", "")));
                MSData.GetInstance.obfStr5.Add(Path.Combine(DesktopPath, "av~_bl~~ock~_rem~~over".Replace("~", "")));
                
                if (!Path.GetPathRoot(DesktopPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) && 
                    !AppConfig.GetInstance.WinPEMode && !LaunchOptions.GetInstance.fullScan)
                {
                    MSData.GetInstance.obfStr6.Add(DesktopPath);
                }
            }

            string DownloadsPath = FileSystemManager.GetDownloadsPath();
            if (DownloadsPath != null)
            {
                MSData.GetInstance.obfStr5.Add(Path.Combine(DownloadsPath, "auto~lo~gge~~r".Replace("~", "")));
                MSData.GetInstance.obfStr5.Add(Path.Combine(DownloadsPath, "av_~bl~o~ck~_re~m~over".Replace("~", "")));

                if (!Path.GetPathRoot(DownloadsPath).Equals("C:\\", StringComparison.OrdinalIgnoreCase) &&
                    !AppConfig.GetInstance.WinPEMode && !LaunchOptions.GetInstance.fullScan)
                {
                    MSData.GetInstance.obfStr6.Add(DownloadsPath);
                }
            }
        }

        private void ScanKnownDirectories(List<IThreatObject> results)
        {
            foreach (string dir in MSData.GetInstance.obfStr1)
            {
                if (Directory.Exists(dir))
                {
                    // SourceTag = "obfStr1" сообщает анализатору что это известный вредоносный каталог
                    var dirThreat = new DirectoryThreatObject(dir, Path.GetFileName(dir), sourceTag: "obfStr1");
                    results.Add(dirThreat);
                }
            }
        }

        private void ScanLockedDirectories(List<IThreatObject> results)
        {
            foreach (string dir in MSData.GetInstance.obfStr5)
            {
                if (Directory.Exists(dir))
                {
                    try
                    {
                        bool isLocked = UnlockObjectClass.IsLockedObject(dir);
                        bool isEmpty = FileSystemManager.IsDirectoryEmpty(dir);

                        // Удаляем каталог если он заблокирован ИЛИ пуст
                        if (isLocked || isEmpty)
                        {
                            // SourceTag = "locked" для заблокированных, "empty" для пустых
                            string tag = isLocked ? "locked" : "empty";
                            var dirThreat = new DirectoryThreatObject(dir, Path.GetFileName(dir), sourceTag: tag);
                            results.Add(dirThreat);
                        }
                    }
                    catch (SecurityException)
                    {
                        var dirThreat = new DirectoryThreatObject(dir, Path.GetFileName(dir), sourceTag: "locked");
                        results.Add(dirThreat);
                    }
                }
            }
        }

        private void ScanKnownFiles(List<IThreatObject> results)
        {
            foreach (string filePath in MSData.GetInstance.obfStr2)
            {
                if (File.Exists(filePath))
                {
                    long fileSize = 0;
                    string originalName = "";
                    string description = "";
                    string hash = "";
                    WinVerifyTrustResult trustResult = WinVerifyTrustResult.Error;

                    try { fileSize = new FileInfo(filePath).Length; } catch { }
                    try
                    {
                        var fvi = FileVersionInfo.GetVersionInfo(filePath);
                        originalName = fvi.OriginalFilename ?? "";
                        description = fvi.FileDescription ?? "";
                    } catch { }
                    try { hash = FileChecker.CalculateSHA1(filePath); } catch { }

                    // SourceTag = "obfStr2" сообщает анализатору что это известный вредоносный файл
                    var fileThreat = new FileThreatObject(filePath, Path.GetFileName(filePath), fileSize, originalName, description, hash, trustResult)
                    {
                        SourceTag = "obfStr2"
                    };

                    results.Add(fileThreat);
                }
            }
        }

        private void ScanPatternBasedDirectories(List<IThreatObject> results)
        {
            string baseDirectory = AppConfig.GetInstance.drive_letter + @":\ProgramData";
            
            if (!Directory.Exists(baseDirectory))
                return;

            string pattern = @"^[a-zA-Z0-9_\-]+-[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
            Regex regex = new Regex(pattern);

            foreach (string directory in Directory.EnumerateDirectories(baseDirectory))
            {
                string directoryName = Path.GetFileName(directory);

                if (regex.IsMatch(directoryName))
                {
                    // Проверяем есть ли файлы с известным malicious MD5
                    bool hasMaliciousFile = false;
                    try
                    {
                        foreach (string file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
                        {
                            if (FileChecker.CalculateMD5(file).Equals("0c0195c48b6b8582fa6f6373032118da"))
                            {
                                hasMaliciousFile = true;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (hasMaliciousFile)
                    {
                        MSData.GetInstance.obfStr1.Add(directory); // Добавляем в базу
                        
                        // SourceTag = "obfStr1" — теперь это известный вредоносный каталог
                        var dirThreat = new DirectoryThreatObject(directory, directoryName, sourceTag: "obfStr1");
                        results.Add(dirThreat);
                    }
                }
            }
        }

        private void ScanBloatedFiles(List<IThreatObject> results)
        {
            string windir = AppConfig.GetInstance.drive_letter + @":\Windows";
            
            if (!Directory.Exists(windir))
                return;

            foreach (string file in Directory.EnumerateFiles(windir, "*.exe", SearchOption.TopDirectoryOnly))
            {
                if (FileChecker.IsFileSizeBloated(file))
                {
                    MSData.GetInstance.obfStr2.Add(file); // Добавляем в базу

                    long fileSize = 0;
                    string originalName = "";
                    string description = "";
                    string hash = "";
                    WinVerifyTrustResult trustResult = WinVerifyTrustResult.Error;

                    try { fileSize = new FileInfo(file).Length; } catch { }
                    try
                    {
                        var fvi = FileVersionInfo.GetVersionInfo(file);
                        originalName = fvi.OriginalFilename ?? "";
                        description = fvi.FileDescription ?? "";
                    } catch { }
                    try { hash = FileChecker.CalculateSHA1(file); } catch { }

                    // SourceTag = "bloated" — анализатор решит что с этим делать
                    var fileThreat = new FileThreatObject(file, Path.GetFileName(file), fileSize, originalName, description, hash, trustResult)
                    {
                        SourceTag = "bloated"
                    };

                    results.Add(fileThreat);
                }
            }
        }

        private void ScanCommonAppDataFiles(List<IThreatObject> results)
        {
            string _baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
                .Replace("x:", $@"{AppConfig.GetInstance.drive_letter}:");

            if (!Directory.Exists(_baseDirectory))
                return;

            // Сканируем .bat файлы
            try
            {
                IEnumerable<string> batFiles = FileEnumerator.GetFiles(_baseDirectory, "*.bat");

                foreach (string file in batFiles)
                {
                    if (FileSystemManager.IsReparsePoint(Path.GetDirectoryName(file)) || !FileSystemManager.IsAccessibleFile(file))
                    {
                        continue;
                    }

                    if (FileChecker.IsBatchFileBad(file))
                    {
                        long fileSize = 0;
                        try { fileSize = new FileInfo(file).Length; } catch { }

                        // SourceTag = "bad_bat" — подозрительный bat файл
                        var fileThreat = new FileThreatObject(file, Path.GetFileName(file), fileSize, "", "", "", WinVerifyTrustResult.Error)
                        {
                            SourceTag = "bad_bat"
                        };

                        results.Add(fileThreat);
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }

            // Сканируем SFX архивы
            try
            {
                foreach (string exeFile in FileEnumerator.GetFiles(_baseDirectory, "*.exe", 0, LaunchOptions.GetInstance.maxSubfolders))
                {
                    if (FileChecker.IsSfxArchive(exeFile))
                    {
                        WinVerifyTrustResult trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(exeFile);
                        
                        if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.Error)
                        {
                            long fileSize = 0;
                            string originalName = "";
                            string description = "";
                            string hash = "";

                            try { fileSize = new FileInfo(exeFile).Length; } catch { }
                            try
                            {
                                var fvi = FileVersionInfo.GetVersionInfo(exeFile);
                                originalName = fvi.OriginalFilename ?? "";
                                description = fvi.FileDescription ?? "";
                            } catch { }
                            try { hash = FileChecker.CalculateSHA1(exeFile); } catch { }

                            // SourceTag = "unsigned_sfx" — неподписанный SFX архив
                            var fileThreat = new FileThreatObject(exeFile, Path.GetFileName(exeFile), fileSize, originalName, description, hash, trustResult)
                            {
                                SourceTag = "unsigned_sfx"
                            };

                            results.Add(fileThreat);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }
    }
}
