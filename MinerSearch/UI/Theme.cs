using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// GUI palette and base style elements.
    /// </summary>
    internal static class Theme
    {
        public static readonly Color Background = Color.FromArgb(244, 246, 249);
        public static readonly Color Sidebar = Color.FromArgb(26, 32, 44);
        public static readonly Color SidebarActive = Color.FromArgb(45, 108, 223);
        public static readonly Color SidebarHover = Color.FromArgb(38, 46, 61);
        public static readonly Color SidebarText = Color.FromArgb(198, 204, 214);

        public static readonly Color Card = Color.White;
        public static readonly Color Border = Color.FromArgb(224, 228, 234);
        public static readonly Color Text = Color.FromArgb(31, 36, 45);
        public static readonly Color Muted = Color.FromArgb(112, 120, 132);
        public static readonly Color Accent = Color.FromArgb(45, 108, 223);
        public static readonly Color AccentHover = Color.FromArgb(36, 92, 194);
        public static readonly Color Success = Color.FromArgb(24, 128, 76);
        public static readonly Color Warning = Color.FromArgb(176, 118, 8);
        public static readonly Color Danger = Color.FromArgb(192, 48, 48);
        public static readonly Color Track = Color.FromArgb(230, 234, 240);

        public static readonly Font H1 = new Font("Segoe UI", 18F, FontStyle.Bold);
        public static readonly Font H2 = new Font("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font Body = new Font("Segoe UI", 10F);
        public static readonly Font BodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font Small = new Font("Segoe UI", 9F);
        public static readonly Font Metric = new Font("Segoe UI", 20F, FontStyle.Bold);

        public static Label Label(string text, Font font, Color color)
        {
            return new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                AutoSize = true,
                BackColor = Color.Transparent
            };
        }

        public static RoundButton PrimaryButton(string text)
        {
            RoundButton button = new RoundButton
            {
                Text = text,
                Font = BodyBold,
                ForeColor = Color.White,
                BackColor = Accent,
                Height = 38,
                Width = 160,
                Cursor = Cursors.Hand,
                CornerRadius = 6
            };
            return button;
        }

        public static RoundButton SecondaryButton(string text)
        {
            RoundButton button = new RoundButton
            {
                Text = text,
                Font = Body,
                ForeColor = Text,
                BackColor = Card,
                Height = 38,
                Width = 160,
                Cursor = Cursors.Hand,
                CornerRadius = 6,
                FlatStyle = FlatStyle.Flat
            };
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Border;
            return button;
        }

        /// <summary>
        /// White card with a one-pixel border.
        /// </summary>
        public static Panel CardPanel()
        {
            Panel panel = new Panel
            {
                BackColor = Card,
                Padding = new Padding(18)
            };
            panel.Paint += delegate (object sender, PaintEventArgs e)
            {
                using (Pen pen = new Pen(Border))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };
            return panel;
        }
    }
}
