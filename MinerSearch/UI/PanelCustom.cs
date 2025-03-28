using System.Drawing;
using System.Windows.Forms;

namespace MSearch
{
    public class PanelCustom : Panel
    {
        public bool ShowTopBorder { get; set; } = true;
        public bool ShowBottomBorder { get; set; } = true;
        public bool ShowLeftBorder { get; set; } = false;
        public bool ShowRightBorder { get; set; } = false;

        public Color BorderColor { get; set; } = Color.Black;
        public int BorderWidth { get; set; } = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(BorderColor, BorderWidth))
            {
                Graphics g = e.Graphics;

                // Draw border lines
                if (ShowTopBorder)
                    g.DrawLine(pen, 0, 0, this.Width, 0); // top

                if (ShowLeftBorder)
                    g.DrawLine(pen, 0, 0, 0, this.Height); // left

                if (ShowRightBorder)
                    g.DrawLine(pen, this.Width - BorderWidth, 0, this.Width - BorderWidth, this.Height); // right

                if (ShowBottomBorder)
                    g.DrawLine(pen, 0, this.Height - BorderWidth, this.Width, this.Height - BorderWidth); // bottom
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.Clear(this.BackColor);
        }
    }
}
