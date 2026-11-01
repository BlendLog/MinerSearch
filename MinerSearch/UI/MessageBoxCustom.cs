using MSearch.Core;
using MSearch.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch
{
    public partial class MessageBoxCustom : FormShadow
    {
        private const int ButtonHeight = 38;
        private const int ButtonSpacing = 10;
        private const int ButtonMinWidth = 92;
        private const int MinClientHeight = 150;

        public MessageBoxCustom(string text, string title = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
            : this(text, title, Color.Empty, buttons, icon)
        {
        }

        public MessageBoxCustom(string text, string title, Color titleColor, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            InitializeComponent();

            labelMessage.Text = text;
            top.Text = title;

            ApplyAccentColor(titleColor);
            ApplyIcon(icon);
            GenerateButtons(buttons);
        }

        /// <summary>
        /// Colored accent on the left of the header (for errors). Empty color — normal look.
        /// </summary>
        private void ApplyAccentColor(Color color)
        {
            accentStrip.BackColor = color == Color.Empty ? Color.White : color;
        }

        private void ApplyIcon(MessageBoxIcon icon)
        {
            bool hasIcon = icon != MessageBoxIcon.None;
            pictureBox1.Visible = hasIcon;
            pictureBox1.Image = hasIcon ? GetMessageBoxIcon(icon) : null;

            tableLayoutPanel2.ColumnStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanel2.ColumnStyles[0].Width = hasIcon ? 64F : 8F;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConstrainMessageWidth();
            AdjustHeightForMessage();
        }

        /// <summary>
        /// Limits the text width so long lines wrap with a margin from the edge.
        /// </summary>
        private void ConstrainMessageWidth()
        {
            int iconWidth = (int)tableLayoutPanel2.ColumnStyles[0].Width;
            int availableWidth = tableLayoutPanel2.ClientSize.Width - iconWidth - labelMessage.Margin.Horizontal;

            if (availableWidth < 100)
                availableWidth = 100;

            labelMessage.MaximumSize = new Size(availableWidth, 0);
        }

        /// <summary>
        /// Adjusts the form height to the message, taking long-line wrapping into account.
        /// </summary>
        private void AdjustHeightForMessage()
        {
            int chromeHeight = tableLayoutPanel1.Padding.Vertical + panel1.Height + panelButtons.Height;
            int requiredHeight = chromeHeight + labelMessage.Height + labelMessage.Margin.Vertical + 2;

            if (requiredHeight < MinClientHeight)
                requiredHeight = MinClientHeight;

            if (requiredHeight != ClientSize.Height)
                ClientSize = new Size(ClientSize.Width, requiredHeight);
        }

        private static Image GetMessageBoxIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Question:
                    return SystemIcons.Question.ToBitmap();
                case MessageBoxIcon.Exclamation:
                    return SystemIcons.Exclamation.ToBitmap();
                case MessageBoxIcon.Error:
                    return SystemIcons.Error.ToBitmap();
                case MessageBoxIcon.Information:
                    return SystemIcons.Information.ToBitmap();
                default:
                    return null;
            }
        }

        private void GenerateButtons(MessageBoxButtons buttons)
        {
            DialogResult[] results = GetButtonResults(buttons);
            bool first = true;

            // FlowDirection = RightToLeft: первый добавленный элемент встаёт справа,
            // поэтому добавляем в обратном порядке — утвердительная кнопка остаётся слева.
            for (int i = results.Length - 1; i >= 0; i--)
            {
                DialogResult result = results[i];
                bool isPrimary = result == DialogResult.OK || result == DialogResult.Yes;
                string text = GetButtonText(result);

                int width = TextRenderer.MeasureText(text, isPrimary ? Theme.BodyBold : Theme.Body).Width + 36;
                if (width < ButtonMinWidth)
                    width = ButtonMinWidth;

                RoundButton button = new RoundButton
                {
                    Text = text,
                    Size = new Size(width, ButtonHeight),
                    Margin = new Padding(first ? 0 : ButtonSpacing, 0, 0, 0),
                    CornerRadius = 6,
                    Cursor = Cursors.Hand,
                    Font = isPrimary ? Theme.BodyBold : Theme.Body,
                    ForeColor = isPrimary ? Color.White : Theme.Text,
                    BackColor = isPrimary ? Theme.Accent : Color.FromArgb(235, 239, 245),
                    TextAlign = ContentAlignment.MiddleCenter,
                    TabStop = true
                };
                first = false;

                button.FlatAppearance.MouseOverBackColor = isPrimary ? Theme.AccentHover : Color.FromArgb(226, 231, 239);
                button.FlatAppearance.MouseDownBackColor = isPrimary ? Color.FromArgb(30, 80, 175) : Color.FromArgb(214, 221, 231);
                button.Tag = result;
                button.Click += Button_Click;

                if (isPrimary)
                    AcceptButton = button;

                if (result == DialogResult.Cancel)
                    CancelButton = button;

                flowButtons.Controls.Add(button);
            }
        }

        private static DialogResult[] GetButtonResults(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    return new[] { DialogResult.OK };
                case MessageBoxButtons.OKCancel:
                    return new[] { DialogResult.OK, DialogResult.Cancel };
                case MessageBoxButtons.YesNo:
                    return new[] { DialogResult.Yes, DialogResult.No };
                case MessageBoxButtons.YesNoCancel:
                    return new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null);
            }
        }

        private static string GetButtonText(DialogResult result)
        {
            switch (result)
            {
                case DialogResult.OK:
                    return "OK";
                case DialogResult.Yes:
                    return L("_YesBtn");
                case DialogResult.No:
                    return L("_NoBtn");
                case DialogResult.Cancel:
                    return L("_CancelBtn");
                default:
                    return result.ToString();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            RoundButton button = sender as RoundButton;
            if (button == null)
                return;

            DialogResult = button.Tag is DialogResult ? (DialogResult)button.Tag : DialogResult.None;
            Close();
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

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
