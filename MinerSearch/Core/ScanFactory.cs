using DBase;
using MSearch.Core.Handlers;
using MSearch.Core.Managers;
using MSearch.Core.Scanners;
using MSearch.Core.ThreatAnalyzers;
using MSearch.Core.ThreatHandlers;
using System;
using System.IO;

namespace MSearch.Core
{
    /// <summary>
    /// Создание инфраструктуры сканирования и подготовка окружения.
    /// </summary>
    public static class ScanFactory
    {
        /// <summary>
        /// Разовая подготовка окружения перед сканированием:
        /// сигнатуры, привилегии, специальные каталоги.
        /// </summary>
        public static void PrepareEnvironment(LaunchOptions options)
        {
            FileChecker.RestoreSignatures(MSData.GetInstance.signatures);
            new PrivilegeManager(options).Enable();
            PrepareSpecialDirectories();
        }

        /// <summary>
        /// Создаёт все сканеры, анализаторы и менеджеры для сканирования.
        /// </summary>
        public static ScanInfrastructure CreateInfrastructure()
        {
            MinerSearchScanState state = new MinerSearchScanState();
            SignatureFileAnalyzer signatureFileAnalyzer = new SignatureFileAnalyzer();

            // RootkitScanner runs first — before any scanning
            ScanManager scanManager = new ScanManager(
                new RootkitScanner(),
                new ProcessScanner(),
                new IThreatScanner[] {
                    new FileSystemThreatScanner(),
                    new ShellStartupScanner(),
                    new RegistryScanner(),
                    new ServiceScanner(),
                    new TaskScanner(),
                    new FirewallScanner(),
                    new WmiScanner(),
                    new HostsThreatScanner(),
                    new UserProfileScanner(),
                });

            ThreatManager threatManager = new ThreatManager(new IThreatAnalyzer[] {
                new RootkitThreatAnalyzer(),
                new ProcessThreatAnalyzer(signatureFileAnalyzer),
                new FileSystemThreatAnalyzer(signatureFileAnalyzer),
                new ShellStartupThreatAnalyzer(signatureFileAnalyzer),
                new RegistryThreatAnalyzer(signatureFileAnalyzer),
                new ServiceThreatAnalyzer(signatureFileAnalyzer),
                new TaskThreatAnalyzer(signatureFileAnalyzer),
                new FirewallThreatAnalyzer(),
                new DirectoryThreatAnalyzer(),
                new WmiThreatAnalyzer(),
                new HostsThreatAnalyzer(),
                new UserProfileThreatAnalyzer(),
            });

            CleanManager cleanManager = new CleanManager(new IThreatHandler[] {
                new RootkitThreatHandler(),
                new ProcessThreatHandler(),
                new RegistryThreatHandler(),
                new ServiceThreatHandler(),
                new TaskThreatHandler(),
                new ShellStartupThreatHandler(),
                new FirewallThreatHandler(),
                new DirectoryThreatHandler(),
                new FileThreatHandler(),
                new WmiThreatHandler(),
                new HostsThreatHandler(),
                new UserProfileThreatHandler(),
            }, state);

            return new ScanInfrastructure
            {
                State = state,
                SignatureAnalyzer = signatureFileAnalyzer,
                ScanManager = scanManager,
                ThreatManager = threatManager,
                CleanManager = cleanManager
            };
        }

        private static void PrepareSpecialDirectories()
        {
            if (AppConfig.GetInstance.WinPEMode || AppConfig.GetInstance.RunAsSystem)
                return;

            string tempPath = Environment.GetEnvironmentVariable("TEMP");
            if (string.IsNullOrEmpty(tempPath))
                tempPath = Environment.GetEnvironmentVariable("TMP");
            if (string.IsNullOrEmpty(tempPath))
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                if (!string.IsNullOrEmpty(userProfile))
                    tempPath = Path.Combine(userProfile, "AppData", "Local", "Temp");
            }

            MSData.GetInstance.PrepareSpecialDirectories(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileSystemManager.GetDownloadsPath(),
                tempPath);
        }
    }
}
