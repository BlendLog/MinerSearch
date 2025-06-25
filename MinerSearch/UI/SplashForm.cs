using MSearch.Core;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSearch
{
    public partial class SplashForm : FormShadow
    {
        string sber = "";
        string yoomoney = "";
        string ton = "";
        string usdt = "";
        bool IsOpenedByButton;


        public SplashForm(bool _value = false)
        {
            InitializeComponent();
            sber = textBox1.Text;
            yoomoney = textBox2.Text;
            ton = textBox3.Text;
            usdt = textBox4.Text;
            IsOpenedByButton = _value;
        }

        void TranslateForm()
        {
            Label_supportCaption.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelSupportCaption");
            Label_supportText.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelSupportText");
            Label_supportClickHint.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelSupportClickHint");
            Label_supportGroupCaption.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelSupportGroupCaption");
            Label_BlogNews.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelBlogNews");
            Label_chatHelp.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelChatHelp");
            Exit_btn.Text = AppConfig.Instance.LL.GetLocalizedString("_exit");
        }

        void OnMouseClick(TextBox control)
        {
            Clipboard.SetText(control.Text);
            control.TextAlign = HorizontalAlignment.Center;
            control.Text = AppConfig.Instance.LL.GetLocalizedString("_EventCopyText");
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
            OnExit();
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            OnExit();
        }


        void OnExit()
        {
            if (IsOpenedByButton)
            {
                FinishEx finishEx = Owner as FinishEx;
                finishEx.Opacity = 1;
                finishEx.Enabled = true;
                Close();
            }
            else
            {
                Environment.Exit(0);
            }
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

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(textBox3);
        }

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(ton, textBox3);
        }
    }
}
