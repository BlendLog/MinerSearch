using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSearch
{
    public partial class Finish : Form
    {
        public Finish(int _totalThreats, int _neutralizedThreats, string _elapsedTime)
        {
            InitializeComponent();
            LBL_threatsCount.Text = _totalThreats.ToString();
            LBL_curedCount.Text = _neutralizedThreats.ToString();
            LBL_scanElapsedTime.Text = _elapsedTime;
        }

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

        void OpenExternalLink(string link)
        {
            if (link.StartsWith("http"))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "explorer",
                    Arguments = $"\"{link}\""
                });
            }
        }

        private void pb_QR_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://boosty.to/blendlog/donate");
        }

        private void pb_telegram_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_blog");
        }

        private void pb_M1nerSearch_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_chat");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void Finish_Load(object sender, EventArgs e)
        {
            TopMost = true;
            TranslateForm();
            MessageBox.Show(Program.LL.GetLocalizedString("_End"), Utils.GetRndString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Text = Utils.GetRndString();

        }

        void TranslateForm()
        {
            if (Program.ActiveLanguage != "RU")
            {
                LBL_yoomoney.Visible = false;
            }

            if (Program.totalFoundThreats > 0)
            {
                LBL_totalThreats.ForeColor = System.Drawing.Color.DarkRed;
                LBL_threatsCount.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                LBL_totalThreats.ForeColor = System.Drawing.Color.DarkGreen;
                LBL_threatsCount.ForeColor = System.Drawing.Color.DarkGreen;
            }
            
            if (Program.ScanOnly || Program.totalFoundThreats == 0)
            {
                LBL_neutralizedThreats.Visible = false;
                LBL_curedCount.Visible = false;
            }



            LBL_ScanComplete.Text = Program.LL.GetLocalizedString("_End");
            LBL_totalThreats.Text = Program.LL.GetLocalizedString("_TotalThreatsFound");
            LBL_neutralizedThreats.Text = Program.LL.GetLocalizedString("_TotalNeutralizedThreats");
            LBL_JoinTelegram.Text = Program.LL.GetLocalizedString("_JoinToTelegram");
            LBL_ScanTime.Text = Program.LL.GetLocalizedString("_Elapse");
            LBL_Support.Text = Program.LL.GetLocalizedString("_LabelSupport");
        }

        private void pb_QR_MouseEnter(object sender, EventArgs e)
        {
            pb_QR.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pb_QR_MouseLeave(object sender, EventArgs e)
        {
            pb_QR.BorderStyle = BorderStyle.None;
        }

        private void pb_telegram_MouseEnter(object sender, EventArgs e)
        {
            pb_telegram.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pb_telegram_MouseLeave(object sender, EventArgs e)
        {
            pb_telegram.BorderStyle = BorderStyle.None;
        }

        private void pb_M1nerSearch_MouseLeave(object sender, EventArgs e)
        {
            pb_M1nerSearch.BorderStyle = BorderStyle.None;
        }

        private void pb_M1nerSearch_MouseEnter(object sender, EventArgs e)
        {
            pb_M1nerSearch.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LBL_link1_MouseEnter(object sender, EventArgs e)
        {
            LBL_link1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link1_MouseLeave(object sender, EventArgs e)
        {
            LBL_link1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link2_MouseEnter(object sender, EventArgs e)
        {
            LBL_link2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link2_MouseLeave(object sender, EventArgs e)
        {
            LBL_link2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link1_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_blog");
        }

        private void LBL_link2_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_chat");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://boosty.to/BlendLog/donate");
        }
    }
}
