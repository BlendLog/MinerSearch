using Microsoft.Win32;
using MSearch.Core;
using System;
using System.Windows.Forms;

namespace MSearch.UI
{
    public partial class FormSettings : FormShadow
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            TranslateForm();
            LoadRegistryValues();
        }

        private void TranslateForm()
        {
            top.Text = AppConfig.GetInstance.LL.GetLocalizedString("_FormSettings_Title");
            Label_allowStatistics.Text = AppConfig.GetInstance.LL.GetLocalizedString("_LabelAllowStatistics");
            Label_autoApplyThreatAction.Text = AppConfig.GetInstance.LL.GetLocalizedString("_label_autoApplyThreatAction");
        }

        private void LoadRegistryValues()
        {
            UpdateAllowStatisticsToggle(AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.StatisticsValueName);
            UpdateAutoAcceptDecisionsToggle(AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.AutoAcceptUnknownValueName);
        }

        void UpdateAllowStatisticsToggle(string registryPath, string valueName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
            {
                if (key != null)
                {
                    object regValue = key.GetValue(valueName);

                    if (regValue != null)
                    {
                        ts_AllowCollectStatistics.CheckedChanged -= ts_AllowCollectStatistics_CheckedChanged;
                        ts_AllowCollectStatistics.Checked = (int)regValue == 1;
                        ts_AllowCollectStatistics.CheckedChanged += ts_AllowCollectStatistics_CheckedChanged;
                    }
                }
            }
        }

        void UpdateAutoAcceptDecisionsToggle(string registryPath, string valueName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath, true))
            {
                if (key != null)
                {
                    object regValue = key.GetValue(valueName);

                    if (regValue != null)
                    {
                        ts_AutoApplyThreatActions.CheckedChanged -= ts_AutoApplyThreatActions_CheckedChanged;
                        ts_AutoApplyThreatActions.Checked = (int)regValue == 1;
                        ts_AutoApplyThreatActions.CheckedChanged += ts_AutoApplyThreatActions_CheckedChanged;
                    }
                }
            }
        }

        void SaveAllowStatisticsToggle()
        {
            var registryPath = AppConfig.GetInstance.RegistryPathMain;

            using (var key = Registry.CurrentUser.OpenSubKey(registryPath, true)
                        ?? Registry.CurrentUser.CreateSubKey(registryPath))
            {
                if (key != null)
                {
                    key.SetValue(AppConfig.GetInstance.StatisticsValueName,
                        ts_AllowCollectStatistics.Checked ? 1 : 0, RegistryValueKind.DWord);
                    key.Close();
                }
            }
        }

        void SaveAutoAcceptDecisionsToggle()
        {
            var registryPath = AppConfig.GetInstance.RegistryPathMain;

            using (var key = Registry.LocalMachine.OpenSubKey(registryPath, true)
                        ?? Registry.LocalMachine.CreateSubKey(registryPath))
            {
                if (key != null)
                {
                    key.SetValue(AppConfig.GetInstance.AutoAcceptUnknownValueName,
                        ts_AutoApplyThreatActions.Checked ? 1 : 0, RegistryValueKind.DWord);
                    key.Close();
                }
            }
        }

        private void ts_AllowCollectStatistics_CheckedChanged(object sender, EventArgs e)
        {
            SaveAllowStatisticsToggle();
        }

        private void ts_AutoApplyThreatActions_CheckedChanged(object sender, EventArgs e)
        {
            SaveAutoAcceptDecisionsToggle();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            SaveAllowStatisticsToggle();
            SaveAutoAcceptDecisionsToggle();
            Close();
        }
    }
}
