using MSearch.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// About page.
    /// </summary>
    public partial class AboutPage : UserControl
    {
        private string _deviceId;

        public AboutPage()
        {
            InitializeComponent();

            panelCard.Resize += delegate { UpdateDisclaimerWidth(); };

            if (!IsInDesignMode)
            {
                labelAppName.Text = AppConfig.GetInstance._title;
                labelVersion.Text = L("_About_Version").Replace("#VERSION#", AppConfig.GetInstance.CurrentVersion);
                _deviceId = DeviceIdProvider.GetDeviceId();
                linkDeviceId.Text = _deviceId;
                UpdateDisclaimerWidth();
                ApplyLanguage();
            }
        }

        /// <summary>
        /// Keeps the disclaimer text inside the card at any window width.
        /// </summary>
        private void UpdateDisclaimerWidth()
        {
            int width = Math.Max(200, panelCard.ClientSize.Width - 48);
            labelDisclaimer.MaximumSize = new Size(width, 0);
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
                Clipboard.SetText(_deviceId);
                linkDeviceId.LinkVisited = true;
            }
            catch { }
        }

        private void linkTelegram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://t.me/MinerSearch_blog");
        }

        private static void OpenUrl(string url)
        {
            Utils.OpenUrlInBrowser(url);
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_About");
            labelVersion.Text = L("_About_Version").Replace("#VERSION#", AppConfig.GetInstance.CurrentVersion);
            labelDeviceIdCaption.Text = L("_About_DeviceId");
            labelDisclaimer.Text = L("_About_Disclaimer");
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
