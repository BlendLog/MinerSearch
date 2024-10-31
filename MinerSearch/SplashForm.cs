using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSearch
{
    public partial class SplashForm : FormShadow
    {
        string sber = "";
        string yoomoney = "";
        string usdt = "";

        public SplashForm()
        {
            InitializeComponent();
            sber = textBox1.Text;
            yoomoney = textBox2.Text;
            usdt = textBox4.Text;
        }

        void TranslateForm()
        {
            Label_supportCaption.Text = Program.LL.GetLocalizedString("_LabelSupportCaption");
            Label_supportText.Text = Program.LL.GetLocalizedString("_LabelSupportText");
            Label_supportClickHint.Text = Program.LL.GetLocalizedString("_LabelSupportClickHint");
            Label_supportGroupCaption.Text = Program.LL.GetLocalizedString("_LabelSupportGroupCaption");
            Label_BlogNews.Text = Program.LL.GetLocalizedString("_LabelBlogNews");
            Label_chatHelp.Text = Program.LL.GetLocalizedString("_LabelChatHelp");
            Exit_btn.Text = Program.LL.GetLocalizedString("_exit");
        }

        void OnMouseClick(TextBox control)
        {
            Clipboard.SetText(control.Text);
            control.TextAlign = HorizontalAlignment.Center;
            control.Text = Program.LL.GetLocalizedString("_EventCopyText");
        }

        void OnMouseLeave(string oldValue, TextBox control)
        {
            control.TextAlign = HorizontalAlignment.Left;
            control.Text = oldValue;
        }

        void OpenExternalLink(string link)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "explorer",
                Arguments = link,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            TranslateForm();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(textBox1);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(sber, textBox1);
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(textBox2);
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(yoomoney, textBox2);
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(textBox4);
        }

        private void textBox4_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(usdt, textBox4);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLink(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLink(linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLink(linkLabel3.Text);
        }
    }
}
