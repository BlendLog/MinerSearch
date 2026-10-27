using Microsoft.Win32;
using MSearch.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Страница настроек: язык, статистика, авто-принятие неизвестных, EULA.
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public event EventHandler LanguageChanged;

        private bool _loading;

        public SettingsPage()
        {
            InitializeComponent();
            PositionControls();

            if (!IsInDesignMode)
                ApplyLanguage();
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void PositionControls()
        {
            PositionControl(panelLanguageControl, comboLanguage);
            PositionControl(panelStatisticsControl, toggleStatistics);
            PositionControl(panelAutoAcceptControl, toggleAutoAccept);
            PositionControl(panelEulaControl, buttonEula);
        }

        private static void PositionControl(Panel panel, Control control)
        {
            if (panel == null || control == null)
                return;

            control.Location = new Point(
                Math.Max(0, panel.ClientSize.Width - control.Width),
                Math.Max(0, (panel.ClientSize.Height - control.Height) / 2));
        }

        private void ControlPanel_Resize(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null)
                return;

            if (panel == panelLanguageControl)
                PositionControl(panel, comboLanguage);
            else if (panel == panelStatisticsControl)
                PositionControl(panel, toggleStatistics);
            else if (panel == panelAutoAcceptControl)
                PositionControl(panel, toggleAutoAccept);
            else if (panel == panelEulaControl)
                PositionControl(panel, buttonEula);
        }

        private void panelCard_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelCard.Width - 1, panelCard.Height - 1);
            }
        }

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageBox_SelectedIndexChanged(sender, e);
        }

        private void toggleStatistics_CheckedChanged(object sender, EventArgs e)
        {
            StatisticsToggle_CheckedChanged(sender, e);
        }

        private void toggleAutoAccept_CheckedChanged(object sender, EventArgs e)
        {
            AutoAcceptToggle_CheckedChanged(sender, e);
        }

        private void buttonEula_Click(object sender, EventArgs e)
        {
            using (License license = new License { ViewMode = true })
            {
                license.ShowDialog(this);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsInDesignMode)
                return;

            LoadValues();
        }

        private void LoadValues()
        {
            _loading = true;
            try
            {
                comboLanguage.SelectedIndex = AppConfig.GetInstance.ActiveLanguage == "RU" ? 0 : 1;
                toggleStatistics.Checked = ReadDword(Registry.CurrentUser, AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.StatisticsValueName) == 1;
                toggleAutoAccept.Checked = ReadDword(Registry.LocalMachine, AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.AutoAcceptUnknownValueName) == 1;
            }
            finally
            {
                _loading = false;
            }
        }

        private static int ReadDword(RegistryKey hive, string path, string valueName)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path))
                {
                    if (key == null)
                        return -1;

                    object value = key.GetValue(valueName);
                    if (value == null)
                        return -1;

                    return (int)value;
                }
            }
            catch
            {
                return -1;
            }
        }

        private static void WriteDword(RegistryKey hive, string path, string valueName, int value)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path, true) ?? hive.CreateSubKey(path))
                {
                    if (key != null)
                        key.SetValue(valueName, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            string language = comboLanguage.SelectedIndex == 0 ? "RU" : "EN";
            if (AppConfig.GetInstance.ActiveLanguage == language)
                return;

            AppConfig.GetInstance.ActiveLanguage = language;
            try
            {
                LanguageManager.SaveLanguageSetting(language);
            }
            catch { }

            EventHandler handler = LanguageChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void StatisticsToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            WriteDword(Registry.CurrentUser, AppConfig.GetInstance.RegistryPathMain,
                AppConfig.GetInstance.StatisticsValueName, toggleStatistics.Checked ? 1 : 0);
        }

        private void AutoAcceptToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            WriteDword(Registry.LocalMachine, AppConfig.GetInstance.RegistryPathMain,
                AppConfig.GetInstance.AutoAcceptUnknownValueName, toggleAutoAccept.Checked ? 1 : 0);
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_FormSettings_Title");
            labelLanguage.Text = L("_Settings_Language");
            labelStatistics.Text = L("_Settings_Statistics");
            labelStatisticsHint.Text = L("_Settings_StatisticsHint");
            labelAutoAccept.Text = L("_Settings_AutoAccept");
            labelAutoAcceptHint.Text = L("_Settings_AutoAcceptHint");
            labelEula.Text = L("_Settings_Eula");
            buttonEula.Text = L("_Settings_EulaOpen");
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
