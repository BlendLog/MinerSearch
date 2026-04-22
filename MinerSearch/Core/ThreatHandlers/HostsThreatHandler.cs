using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using MSearch.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Win32Wrapper;

namespace MSearch.Core.ThreatHandlers
{
    /// <summary>
    /// SRP: Обрабатывает заражённый файл hosts.
    /// - Копирует заражённый файл в карантин
    /// - Удаляет вредоносные строки
    /// - Записывает очищенный файл обратно
    /// </summary>
    internal sealed class HostsThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Hosts;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var hostsThreat = decision.Target as HostsThreatObject;
            if (hostsThreat == null)
                return ApplyResult.NotApplicable;

            string hostsPath = hostsThreat.HostsFilePath;

#if DEBUG
            Console.WriteLine($"[DBG HostsThreatHandler] Phase={phase}, Path={hostsPath}, InfectedLines={hostsThreat.InfectedLinesCount}");
#endif

            if (phase != CleanupPhase.Finalize)
                return ApplyResult.NotApplicable;

            if (!File.Exists(hostsPath))
            {
                AppConfig.GetInstance.LL.LogMessage("\t[?]", "_HostsFileMissing", "", ConsoleColor.Gray);
                string hostsdir = Path.GetDirectoryName(hostsThreat.HostsFilePath);
                if (!Directory.Exists(hostsdir) || (AppConfig.GetInstance.WinPEMode && !Directory.Exists(hostsdir)))
                {
                    Directory.CreateDirectory(hostsdir);
                }
                try
                {
                    File.Create(hostsThreat.HostsFilePath).Close();
                    if (File.Exists(hostsThreat.HostsFilePath))
                    {
                        AppConfig.GetInstance.LL.LogSuccessMessage("_HostsFileCreated");
                    }
                }
                catch (Exception e)
                {
                    decision.ApplyErrorMessage = e.Message;
                    AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCleanHosts", e);
                    return ApplyResult.Error;

                }
                return ApplyResult.NotApplicable;
            }

            try
            {
                if (hostsThreat.ShouldQuarantineFile)
                {
                    Utils.AddToQuarantine(hostsThreat.HostsFilePath, "", false);
                }

                // 2. Удаляем вредоносные строки
                if (hostsThreat.ShouldRemoveInfectedLines && hostsThreat.InfectedLinesCount > 0)
                {
                    // Снять атрибут ReadOnly
                    File.SetAttributes(hostsPath, FileAttributes.Normal);
                    
                    // Сбросить ACL
                    UnlockObjectClass.ResetObjectACL(hostsPath);
                    
                    // Создать временную копию в %Temp% со случайным именем
                    string tempPath = Path.Combine(Path.GetTempPath(), 
                        "hosts_" + Guid.NewGuid().ToString("N").Substring(0, 8) + ".tmp");
                    
                    try
                    {
                        File.Copy(hostsPath, tempPath, true);
                        
                        // Работаем с копией (очистка вредоносных записей)
                        List<string> allLines = File.ReadLines(tempPath).ToList();
                        HashSet<string> infectedSet = new HashSet<string>(hostsThreat.InfectedLines, StringComparer.Ordinal);

                        foreach (var line in infectedSet)
                        {
                            AppConfig.GetInstance.LL.LogWarnMessage("_MaliciousEntry", line);
                        }

                        List<string> cleanedLines = new List<string>();
                        foreach (string line in allLines)
                        {
                            if (infectedSet.Contains(line))
                                continue;
                            cleanedLines.Add(line);
                        }

                        File.WriteAllLines(tempPath, cleanedLines);
                        
                        // Восстановить атрибуты и ACL оригинала
                        File.SetAttributes(hostsPath, FileAttributes.Normal);
                        UnlockObjectClass.ResetObjectACL(hostsPath);
                        
                        // Заменить оригинал на обработанный
                        File.Copy(tempPath, hostsPath, true);
                        NativeFileOperations.DeleteFileWithRetry(tempPath);
                        
                        return ApplyResult.Success;
                    }
                    catch
                    {
                        // Очистить временный файл при ошибке
                        if (File.Exists(tempPath))
                        {
                            try { NativeFileOperations.DeleteFileWithRetry(tempPath); } catch { }
                        }
                        throw;
                    }
                }

                return ApplyResult.Success;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Антивирус/HIPS блокирует доступ
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Inaccessible;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorCannotRemove", ex, hostsPath, "_File");
                
                return ApplyResult.Error;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, hostsPath, "_ErrorCleanHosts");
                return ApplyResult.Error;
            }
        }
    }
}
