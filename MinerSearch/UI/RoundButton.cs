using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MSearch.UI
{
    public class RoundButton : Button
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public int CornerRadius { get; set; } = 4;

        public RoundButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }



        // Если цвет кнопки меняется динамически — рамка остаётся невидимой
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            FlatAppearance.BorderColor = BackColor == Color.Transparent ? Color.White : BackColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            FlatAppearance.BorderColor = BackColor == Color.Transparent ? Color.White : BackColor;
            UpdateRegion();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            FlatAppearance.BorderColor = BackColor == Color.Transparent ? Color.White : BackColor;

            var hRgn = CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius);
            if (hRgn != IntPtr.Zero)
            {
                Region = Region.FromHrgn(hRgn);
                DeleteObject(hRgn);
            }
        }
    }





}
