using DBase;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MSearch;

namespace MSearch.Core.ThreatAnalyzers
{

    public interface IFileContentAnalyzer
    {
        FileContentAnalysisResult Analyze(FileThreatObject fileThreat, bool displayProgress);
        void AnalyzeFiles(IEnumerable<FileThreatObject> fileThreats);
        FileContentAnalysisResult AnalyzeAndDisable(FileThreatObject fileThreat, bool displayProgress);
    }

    public sealed class FileContentAnalysisResult
    {
        public bool IsMalicious { get; private set; }
        public bool IsSuspicious { get; private set; }
        public bool IsLockedByAntivirus { get; private set; }
        public bool ErrorResult { get; private set; }

        public FileContentAnalysisResult(
            bool isMalicious,
            bool isSuspicious,
            bool isLockedByAntivirus,
            bool error)
        {
            IsMalicious = isMalicious;
            IsSuspicious = isSuspicious;
            IsLockedByAntivirus = isLockedByAntivirus;
            ErrorResult = error;
        }

        public static FileContentAnalysisResult Clean()
        {
            return new FileContentAnalysisResult(false, false, false, false);
        }

        public static FileContentAnalysisResult Malicious()
        {
            return new FileContentAnalysisResult(true, false, false, false);
        }

        public static FileContentAnalysisResult Suspicious()
        {
            return new FileContentAnalysisResult(false, true, false, false);
        }

        public static FileContentAnalysisResult LockedByAv()
        {
            return new FileContentAnalysisResult(false, false, true, false);
        }

        public static FileContentAnalysisResult Error()
        {
            return new FileContentAnalysisResult(false, false, false, true);
        }
    }

    public class SignatureFileAnalyzer : IFileContentAnalyzer
    {
        readonly byte[] startBadSequence = { 0xFF, 0xC7, 0x05, 0xC5 };
        readonly byte[] endBadSequence = { 0xE8, 0x54, 0xFF, 0xFF, 0xFF };

        public FileContentAnalysisResult Analyze(FileThreatObject fileThreat, bool displayProgress = true)
        {
            if (fileThreat.IsAlreadyAnalyzed)
            {
                return fileThreat.AnalysisResult ?? FileContentAnalysisResult.Clean();
            }

            if (fileThreat.FilePath.Length > 240)
            {
                return FileContentAnalysisResult.Clean();
            }

            if (displayProgress)
            {
                LocalizedLogger.LogAnalyzingFile(fileThreat.FilePath);
            }

            try
            {

                if (fileThreat.FileSize > fileThreat.MAX_FILE_SIZE || fileThreat.FileSize < fileThreat.MIN_FILE_SIZE)
                {
                    if (displayProgress)
                    {
                        LocalizedLogger.LogOK();
                    }
                    return FileContentAnalysisResult.Clean();
                }


                if (fileThreat.TrustResult == WinVerifyTrustResult.Success)
                {
                    if (displayProgress)
                    {
                        LocalizedLogger.LogOK();
                    }
                    return FileContentAnalysisResult.Clean();
                }


                if (FileChecker.IsSfxArchive(fileThreat.FilePath))
                {
                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_sfxArchive", fileThreat.FilePath);
                    return FileContentAnalysisResult.Suspicious();
                }


                bool sequenceFound = FileChecker.CheckSignature(fileThreat.FilePath, MSData.GetInstance.signatures);

                if (sequenceFound)
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_Found", fileThreat.FilePath);
                    fileThreat.IsAlreadyAnalyzed = true;
                    return FileContentAnalysisResult.Malicious();
                }


                bool computedSequence = FileChecker.CheckDynamicSignature(fileThreat.FilePath, 16, 100);
                if (computedSequence)
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_Found", fileThreat.FilePath);
                    fileThreat.IsAlreadyAnalyzed = true;
                    return FileContentAnalysisResult.Malicious();
                }

                bool computedSequence2 = FileChecker.CheckDynamicSignature(fileThreat.FilePath, 2096, startBadSequence, endBadSequence);
                if (computedSequence2)
                {
                    AppConfig.GetInstance.LL.LogCautionMessage("_Found", fileThreat.FilePath);
                    fileThreat.IsAlreadyAnalyzed = true;
                    return FileContentAnalysisResult.Malicious();
                }

                if (displayProgress)
                {
                    LocalizedLogger.LogOK();
                }
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x8007016A)))
            {
                AppConfig.GetInstance.LL.LogWarnMediumMessage("_ErrorFileOnlineOnly", fileThreat.FilePath);
                return FileContentAnalysisResult.Error();

            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x800700E1)))
            {
                AppConfig.GetInstance.LL.LogCautionMessage("_ErrorLockedByWD", fileThreat.FilePath);
                return FileContentAnalysisResult.LockedByAv();
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorAnalyzingFile", ex, fileThreat.FilePath);
                return FileContentAnalysisResult.Error();
            }
            
            fileThreat.IsAlreadyAnalyzed = true;
            return FileContentAnalysisResult.Clean();
        }

        public FileContentAnalysisResult AnalyzeAndDisable(FileThreatObject fileThreat, bool displayProgress = true)
        {
            var result = Analyze(fileThreat, displayProgress);
            
            // Немедленная блокировка выполнения для вредоносных файлов
            if (result.IsMalicious && !fileThreat.IsValidSignature)
            {
                try
                {
                    UnlockObjectClass.DisableExecute(fileThreat.FilePath);
#if DEBUG
                    Console.WriteLine($"[DBG] DisableExecute applied: {fileThreat.FilePath}");
#endif
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogWarnMessage("_WarnCannotDisableExecute", 
                        $"{fileThreat.FilePath}: {ex.Message}");
                }
            }
            
            return result;
        }

        public void AnalyzeFiles(IEnumerable<FileThreatObject> fileThreats)
        {
            var fileList = fileThreats.ToList();
            int total = fileList.Count;
            int current = 0;
            int analyzedCount = 0;

            Console.WriteLine($"  [{total}] файлов найдено. Начинаю анализ...");
            
            AppConfig.GetInstance.LL.LogHeadMessage("_StartSignatureScan");

            foreach (var fileThreat in fileList)
            {
                current++;
                
                if (fileThreat.IsAlreadyAnalyzed)
                {
                    if (LaunchOptions.GetInstance.verbose)
                    {
                        LocalizedLogger.LogSignatureScanSkipped(current, total, fileThreat.FilePath);
                    }
                    continue;
                }
                
                analyzedCount++;
                LocalizedLogger.LogSignatureScanProgress(analyzedCount, total, fileThreat.FilePath);

                try
                {
                    fileThreat.TrustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(fileThreat.FilePath);
                }
                catch
                {
                    fileThreat.TrustResult = WinVerifyTrustResult.Error;
                }

                var result = AnalyzeAndDisable(fileThreat, displayProgress: false);
                fileThreat.AnalysisResult = result;
            }

            Console.WriteLine();
            string completeMessage = AppConfig.GetInstance.LL.GetLocalizedString("_SignatureScanComplete");
            Logger.WriteLog($"[#] {completeMessage}", ConsoleColor.White, true);
        }
    }
}
