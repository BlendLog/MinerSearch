using Microsoft.Win32;
using MSearch.Properties;
using MSearch.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MSearch.Core
{
    /// <summary>
    /// Результаты проверки окружения приложения.
    /// </summary>
    public class EnvironmentResult
    {
        public bool DotNetInstalled { get; set; }
        public bool CanRunSingleInstance { get; set; }
        public bool IsRebootRequired { get; set; }
        public bool IsWinPE { get; set; }
        public bool IsStartedFromArchive { get; set; }
        public bool IsSystemProcess { get; set; }
    }

    /// <summary>
    /// Проверяет окружение и взаимодействует с пользователем при необходимости.
    /// Вынесено из Program.Init() для соблюдения SRP и тестируемости.
    /// </summary>
    public static class EnvironmentChecker
    {
        #region Основные проверки

        /// <summary>
        /// Выполняет все проверки окружения и возвращает результат.
        /// </summary>
        public static EnvironmentResult Check()
        {
            return new EnvironmentResult
            {
                DotNetInstalled = OSExtensions.IsDotNetInstalled(),
                CanRunSingleInstance = Utils.IsOneAppCopy(),
                IsRebootRequired = Utils.IsRebootMtx(),
                IsWinPE = OSExtensions.IsWinPEEnv(),
                IsStartedFromArchive = ProcessManager.IsStartedFromArchive(),
                IsSystemProcess = ProcessManager.GetCurrentProcessOwner().IsSystem
            };
        }

        #endregion

        #region EULA и предупреждения

        /// <summary>
        /// Обрабатывает принятие EULA: диалоговое окно или консольный режим.
        /// Возвращает false, если пользователь отменил действие.
        /// </summary>
        public static bool PromptEula(AppConfig config)
        {
            const string registryKeyPath = @"Software\M1nerSearch";
            const string registryValueName = "acceptedEula";

            // Проверка Windows 7
            if ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1))
            {
                DialogDispatcher.Show(config.LL.GetLocalizedString("_WarnOutdatedOS"), config._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                var eulaValue = key.GetValue(registryValueName);
                if (eulaValue == null || (eulaValue != null && (int)eulaValue != 1))
                {
                    // EULA ещё не принят — показываем диалог или консольный запрос
                    return ProcessEulaWithoutAccept(key, registryValueName);
                }

                return true;
            }
        }

        /// <summary>
        /// Показывает предупреждение о запуске от имени SYSTEM.
        /// </summary>
        public static DialogResult PromptRunAsSystemWarning(AppConfig config, bool isGuiAvailable)
        {
            if (!isGuiAvailable) return DialogResult.Yes;

            var msg = DialogDispatcher.Show(
                config.LL.GetLocalizedString("_MessageRunAsSystemWarn"),
                config._title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            return msg;
        }

        /// <summary>
        /// Запрашивает букву диска в режиме WinPE (через консоль).
        /// Вместо goto используется цикл while.
        /// </summary>
        public static string PromptWinPEDriveLetter(AppConfig config)
        {
            string driveLetter;

            while (true)
            {
                LocalizedLogger.LogSpecifyDrive();
                driveLetter = Console.ReadLine();

                if (driveLetter.Length > 1 || FileChecker.IsDigit(driveLetter))
                {
                    LocalizedLogger.LogIncorrectDrive(driveLetter);
                    continue;
                }

                if (!Directory.Exists(driveLetter + ":\\ "))
                {
                    LocalizedLogger.LogIncorrectDrive(driveLetter);
                    continue;
                }

                return driveLetter.ToUpperInvariant();
            }
        }

        #endregion

        #region Внутренние методы

        /// <summary>
        /// Обрабатывает принятие EULA пользователем (форма или консоль).
        /// </summary>
        private static bool ProcessEulaWithoutAccept(RegistryKey key, string registryValueName)
        {
            var config = AppConfig.GetInstance;

            if (config.IsGuiAvailable)
            {
                try
                {
                    using (License licenseForm = new License())
                    {
                        if (config.ActiveLanguage == "EN")
                        {
                            licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_EN;
                            licenseForm.richTextBox1.Rtf = Resources._License_EN;
                            licenseForm.Accept_btn.Text = Resources._accept_en;
                            licenseForm.Exit_btn.Text = Resources._exit_EN;
                        }
                        else if (config.ActiveLanguage == "RU")
                        {
                            licenseForm.Label_LicenseCaption.Text = Resources._LicenseCaption_RU;
                            licenseForm.richTextBox1.Rtf = Resources._License_RU;
                            licenseForm.Accept_btn.Text = Resources._accept_ru;
                            licenseForm.Exit_btn.Text = Resources._exit_RU;
                        }

                        licenseForm.ShowDialog();
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    DialogDispatcher.Show(
                        config.LL.GetLocalizedString("_Error") + $"\n\n{ioe.Message}",
                        config._title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                catch (FileNotFoundException notfoundEx)
                {
                    DialogDispatcher.Show(
                        config.LL.GetLocalizedString("_ErrorNotFoundComponent") + $"\n\n{notfoundEx.FileName}",
                        config._title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
#if DEBUG
                Logger.WriteLog("[DBG] UserInteractive false, Console mode...", ConsoleColor.White);
#endif

                DialogResult agreementResult = DialogDispatcher.Show(
                    config.LL.GetLocalizedString("_License_console"),
                    config._title,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (agreementResult == DialogResult.Yes)
                {
                    key.SetValue(registryValueName, 1);
                    return true;
                }
            }

            // Если не консоль и форма закрыта без принятия — проверяем, был ли принят
            if (!config.IsGuiAvailable || !Environment.UserInteractive)
            {
                var newValue = key.GetValue(registryValueName);
                return newValue != null && (int)newValue == 1;
            }

            // Для GUI: проверяем после ShowDialog
            var finalValue = key.GetValue(registryValueName);
            return finalValue != null && (int)finalValue == 1;
        }

        #endregion
    }
}
