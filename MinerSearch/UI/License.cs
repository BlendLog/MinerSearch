using Microsoft.Win32;
using MSearch.Core;
using MSearch.Properties;
using System;
using System.Windows.Forms;

namespace MSearch
{
    public partial class License : FormShadow
    {
        /// <summary>EULA viewing from settings: buttons do not exit the application.</summary>
        public bool ViewMode { get; set; }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            // В режиме принятия EULA окно нельзя закрыть системной командой.
            if (!ViewMode && m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }

            base.WndProc(ref m);
        }

        public License()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!ViewMode)
                return;

            LoadContent();

            Accept_btn.Text = L("_CloseBtn");
            Exit_btn.Visible = false;
            CancelButton = Accept_btn;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Текст только для чтения: курсор в начале документа выглядит как артефакт.
            ActiveControl = null;
        }

        /// <summary>
        /// Fills the window with the EULA text and button captions for the current language.
        /// </summary>
        public void LoadContent()
        {
            bool russian = AppConfig.GetInstance.ActiveLanguage == "RU";

            Label_LicenseCaption.Text = russian ? Resources._LicenseCaption_RU : Resources._LicenseCaption_EN;
            richTextBox1.Rtf = russian ? Resources._License_RU : Resources._License_EN;
            Accept_btn.Text = russian ? Resources._AcceptBtn_RU : Resources._AcceptBtn_EN;
            Exit_btn.Text = russian ? Resources._ExitBtn_RU : Resources._ExitBtn_EN;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            if (ViewMode)
            {
                Close();
                return;
            }

            Environment.Exit(0);
        }

        private void Accept_btn_Click(object sender, EventArgs e)
        {
            if (ViewMode)
            {
                Close();
                return;
            }

            const string registryKeyPath = @"Software\M1nerSearch";
            const string valueName = "acceptedEula";
            Registry.CurrentUser.CreateSubKey(registryKeyPath).SetValue(valueName, 1);
            Close();
        }

        private void Label_LicenseCaption_MouseDown(object sender, MouseEventArgs e)
        {
            Label_LicenseCaption.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ViewMode)
            {
                Close();
                return;
            }

            Environment.Exit(0);
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
