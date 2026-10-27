using MSearch.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Страница «О программе».
    /// </summary>
    public partial class AboutPage : UserControl
    {
        public AboutPage()
        {
            InitializeComponent();

            if (!IsInDesignMode)
            {
                labelAppName.Text = AppConfig.GetInstance._title;
                labelVersion.Text = L("_About_Version").Replace("#VERSION#", AppConfig.GetInstance.CurrentVersion);
                linkDeviceId.Text = DeviceIdProvider.GetDeviceId();
                ApplyLanguage();
            }
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void panelCard_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelCard.Width - 1, panelCard.Height - 1);
            }
        }

        private void linkDeviceId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetText(linkDeviceId.Text);
                linkDeviceId.Text = L("_EventCopyText");
            }
            catch { }
        }

        private void linkTelegram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://t.me/MinerSearch_blog");
        }

        private void buttonDonate_Click(object sender, EventArgs e)
        {
            OpenUrl("https://blendlog.github.io/donate.html");
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_About");
            labelVersion.Text = L("_About_Version").Replace("#VERSION#", AppConfig.GetInstance.CurrentVersion);
            labelDeviceIdCaption.Text = L("_About_DeviceId");
            buttonDonate.Text = L("_DonateBtn");
            labelDisclaimer.Text = L("_About_Disclaimer");
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
