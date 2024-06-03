using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace MSearch
{
    public partial class License : Form
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }

            base.WndProc(ref m);
        }

        public License()
        {
            InitializeComponent();
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Accept_btn_Click(object sender, EventArgs e)
        {
            const string registryKeyPath = @"Software\M1nerSearch";
            const string valueName = "acceptedEula";
            Registry.CurrentUser.CreateSubKey(registryKeyPath).SetValue(valueName, 1);
            Close();
        }
    }
}
