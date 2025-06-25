using MSearch.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch
{
    public partial class MessageBoxCustom : FormShadow
    {
        Dictionary<string, DialogResult> buttonMapping;

        public MessageBoxCustom(string text, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            InitializeComponent();
            labelMessage.Text = text;
            top.Text = title;
            tableLayoutPanel1.BackColor = top.BackColor;
            pictureBox1.Image = GetMessageBoxIcon(icon);
            GenerateButtons(buttons);

            if (pictureBox1.Size.Width == 1)
            {
                tableLayoutPanel2.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[0].Width = 0;
                tableLayoutPanel2.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[1].Width = 100;
            }
        }

        public MessageBoxCustom(string text, string title, Color titleColor, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            InitializeComponent();
            labelMessage.Text = text;
            top.Text = title;
            top.BackColor = titleColor;
            tableLayoutPanel1.BackColor = titleColor;
            CloseBtn.BackColor = top.BackColor;
            pictureBox1.Image = GetMessageBoxIcon(icon);
            GenerateButtons(buttons);

            if (pictureBox1.Size.Width == 1)
            {
                tableLayoutPanel2.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[0].Width = 0;
                tableLayoutPanel2.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[1].Width = 100;
            }
        }

        private Image GetMessageBoxIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.None:
                    return new Bitmap(1, 1);
                case MessageBoxIcon.Question:
                    return SystemIcons.Question.ToBitmap();
                case MessageBoxIcon.Exclamation:
                    return SystemIcons.Exclamation.ToBitmap();
                case MessageBoxIcon.Error:
                    return SystemIcons.Error.ToBitmap();
                case MessageBoxIcon.Information:
                    return SystemIcons.Information.ToBitmap();
            }
            return new Bitmap(1, 1);
        }

        private void GenerateButtons(MessageBoxButtons buttons)
        {
            buttonMapping = new Dictionary<string, DialogResult>();
            string[] buttonTexts = GetLocalizedButtonTexts(buttons);

            int buttonWidth = 84;
            int buttonHeight = 35;
            int spacing = 10;
            int totalWidth = (buttonWidth + spacing) * buttonTexts.Length - spacing;
            int startX = (panelButtons.ClientSize.Width - totalWidth) / 2;

            for (int i = 0; i < buttonTexts.Length; i++)
            {
                Button btn = new Button
                {
                    Text = buttonTexts[i],
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(startX + i * (buttonWidth + spacing), 7),
                    BackColor = Color.Gainsboro,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Anchor = AnchorStyles.Top
                };
                btn.FlatAppearance.BorderColor = Color.Black;
                btn.FlatAppearance.MouseDownBackColor = Color.Navy;
                btn.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;

                btn.Click += (sender, e) =>
                {
                    this.DialogResult = buttonMapping[btn.Text];
                    this.Close();
                };

                panelButtons.Controls.Add(btn);
            }
        }

        private string[] GetLocalizedButtonTexts(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    buttonMapping["OK"] = DialogResult.OK;
                    return new[] { "OK" };
                case MessageBoxButtons.OKCancel:
                    buttonMapping["OK"] = DialogResult.OK;
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_CancelBtn")] = DialogResult.Cancel;
                    return new[]
                    {
                        AppConfig.Instance.LL.GetLocalizedString("OK"),
                        AppConfig.Instance.LL.GetLocalizedString("_CancelBtn")
                    };
                case MessageBoxButtons.YesNo:
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_YesBtn")] = DialogResult.Yes;
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_NoBtn")] = DialogResult.No;
                    return new[]
                    {
                        AppConfig.Instance.LL.GetLocalizedString("_YesBtn"),
                        AppConfig.Instance.LL.GetLocalizedString("_NoBtn")
                    };
                case MessageBoxButtons.YesNoCancel:
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_YesBtn")] = DialogResult.Yes;
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_NoBtn")] = DialogResult.No;
                    buttonMapping[AppConfig.Instance.LL.GetLocalizedString("_CancelBtn")] = DialogResult.Cancel;
                    return new[]
                    {
                        AppConfig.Instance.LL.GetLocalizedString("_YesBtn"),
                        AppConfig.Instance.LL.GetLocalizedString("_NoBtn"),
                        AppConfig.Instance.LL.GetLocalizedString("_CancelBtn")
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null);
            }
        }

        public static DialogResult Show(string message)
        {
            using (var box = new MessageBoxCustom(message))
            {
                return box.ShowDialog();
            }
        }

        public static DialogResult Show(string message, string title)
        {
            using (var box = new MessageBoxCustom(message, title))
            {
                return box.ShowDialog();
            }
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons)
        {
            using (var box = new MessageBoxCustom(message, title, buttons))
            {
                return box.ShowDialog();
            }
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var box = new MessageBoxCustom(message, title, buttons, icon))
            {
                return box.ShowDialog();
            }
        }

        public static DialogResult Show(string message, string title, Color color)
        {
            using (var box = new MessageBoxCustom(message, title, color))
            {
                return box.ShowDialog();
            }
        }

        public static DialogResult Show(string message, string title, Color color, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var box = new MessageBoxCustom(message, title, color, buttons, icon))
            {
                return box.ShowDialog();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Close();
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void labelMessage_SizeChanged(object sender, EventArgs e)
        {
            foreach (char c in labelMessage.Text)
            {
                if (c.Equals('\n'))
                {
                    this.Height += labelMessage.Height / 25;
                    tableLayoutPanel2.Height += labelMessage.Height / 25;
                }
            }
        }
    }
}
