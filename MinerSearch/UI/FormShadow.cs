using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MSearch
{
    public class FormShadow : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        // DWM properties
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWA_COLOR_ONLY = 0;
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWCP_ROUND = 2;

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int SHADOW_SIZE = 16;
        private const int SHADOW_EXTRA = 32; // запас для размытия верхней тени
        private const int CORNER_RADIUS = 8;

        private readonly static int[] _captionColors = { 45, 85, 205 }; // RoyalBlue
        private readonly static int[] _borderColors = { 30, 30, 30 };

        private readonly static Color _shadowTopStart = Color.FromArgb(50, 0, 0, 0);
        private readonly static Color _shadowTopEnd = Color.FromArgb(0, 0, 0, 0);
        private readonly static Color _shadowBottomStart = Color.FromArgb(50, 0, 0, 0);
        private readonly static Color _shadowBottomEnd = Color.FromArgb(0, 0, 0, 0);
        private readonly static Color _shadowLeftStart = Color.FromArgb(50, 0, 0, 0);
        private readonly static Color _shadowLeftEnd = Color.FromArgb(0, 0, 0, 0);
        private readonly static Color _shadowRightStart = Color.FromArgb(50, 0, 0, 0);
        private readonly static Color _shadowRightEnd = Color.FromArgb(0, 0, 0, 0);
        private readonly static Color _cornerShadowStart = Color.FromArgb(45, 0, 0, 0);
        private readonly static Color _cornerShadowEnd = Color.FromArgb(0, 0, 0, 0);

        private bool _isCompositionEnabled = false;

        // Кэш: Bitmap с тенью + фоном (полная отрисовка)
        private Bitmap _cachedBitmap;
        private int _cachedW;
        private int _cachedH;

        // Кэш GraphicsPath для скруглённого фона
        private GraphicsPath _cachedPath;
        private int _cachedPathW;
        private int _cachedPathH;

        public FormShadow()
        {
            FormBorderStyle = FormBorderStyle.None;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
        }

        ~FormShadow()
        {
            _cachedBitmap?.Dispose();
            _cachedPath?.Dispose();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            _isCompositionEnabled = IsCompositionEnabled();

            if (_isCompositionEnabled)
            {
                int preference = DWMWCP_ROUND;
                DwmSetWindowAttribute(Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));

                int captionColor = ColorTranslator.ToWin32(Color.FromArgb(_captionColors[0], _captionColors[1], _captionColors[2]));
                DwmSetWindowAttribute(Handle, DWMWA_CAPTION_COLOR, ref captionColor, sizeof(int));

                int borderColor = ColorTranslator.ToWin32(Color.FromArgb(_borderColors[0], _borderColors[1], _borderColors[2]));
                DwmSetWindowAttribute(Handle, DWMWA_BORDER_COLOR, ref borderColor, sizeof(int));

                int colorOnly = DWMWA_COLOR_ONLY;
                DwmSetWindowAttribute(Handle, DWMWA_COLOR_ONLY, ref colorOnly, sizeof(int));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InvalidateCache();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_isCompositionEnabled)
            {
                base.OnPaint(e);
                return;
            }

            // Фон + тень — из кэша, один DrawImageUnscaled
            if (_cachedBitmap != null)
            {
                e.Graphics.DrawImageUnscaled(_cachedBitmap, 0, 0);
            }

            // Рисуем дочерние контролы поверх фона
            base.OnPaint(e);
        }

        private void InvalidateCache()
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;

            if (w <= 0 || h <= 0) return;

            // Если размер не изменился — ничего не перерисовываем
            if (_cachedBitmap != null && _cachedW == w && _cachedH == h)
                return;

            _cachedBitmap?.Dispose();

            // Bitmap с запасом для тени: тени рисуются за пределами ClientSize
            // Но тень рисуется в координатах клиента — поэтому Bitmap = ClientSize
            _cachedBitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (var g = Graphics.FromImage(_cachedBitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;

                DrawShadow(g, w, h);
                DrawBackground(g, w, h);
            }

            _cachedW = w;
            _cachedH = h;
        }

        private void DrawShadow(Graphics g, int w, int h)
        {
            // Верхняя тень — с запасом для размытия
            using (var brush = new LinearGradientBrush(new Point(0, 0), new Point(0, SHADOW_SIZE + SHADOW_EXTRA),
                _shadowTopStart, _shadowTopEnd))
                g.FillRectangle(brush, 0, 0, w, SHADOW_SIZE + SHADOW_EXTRA);

            // Нижняя тень
            using (var brush = new LinearGradientBrush(new Point(0, h - SHADOW_SIZE), new Point(0, h),
                _shadowBottomEnd, _shadowBottomStart))
                g.FillRectangle(brush, 0, h - SHADOW_SIZE, w, SHADOW_SIZE);

            // Левая тень
            using (var brush = new LinearGradientBrush(new Point(0, 0), new Point(SHADOW_SIZE, 0),
                _shadowLeftStart, _shadowLeftEnd))
                g.FillRectangle(brush, 0, 0, SHADOW_SIZE, h);

            // Правая тень
            using (var brush = new LinearGradientBrush(new Point(w - SHADOW_SIZE, 0), new Point(w, 0),
                _shadowRightEnd, _shadowRightStart))
                g.FillRectangle(brush, w - SHADOW_SIZE, 0, SHADOW_SIZE, h);

            // Углы тени
            int cr2 = CORNER_RADIUS * 2;
            using (var brush = new LinearGradientBrush(new Point(0, 0), new Point(CORNER_RADIUS, CORNER_RADIUS),
                _cornerShadowStart, _cornerShadowEnd))
            {
                g.FillEllipse(brush, 0, 0, cr2, cr2);
                g.FillEllipse(brush, w - cr2, 0, cr2, cr2);
                g.FillEllipse(brush, 0, h - cr2, cr2, cr2);
                g.FillEllipse(brush, w - cr2, h - cr2, cr2, cr2);
            }
        }

        private void DrawBackground(Graphics g, int w, int h)
        {
            var path = GetRoundedRectPath(w, h);
            using (var brush = new SolidBrush(Color.FromArgb(245, 245, 245)))
                g.FillPath(brush, path);
        }

        private GraphicsPath GetRoundedRectPath(int w, int h)
        {
            if (_cachedPath != null && _cachedPathW == w && _cachedPathH == h)
                return _cachedPath;

            _cachedPath?.Dispose();
            _cachedPath = CreateRoundedRectPath(0, 0, w, h, CORNER_RADIUS);
            _cachedPathW = w;
            _cachedPathH = h;
            return _cachedPath;
        }

        static GraphicsPath CreateRoundedRectPath(int x, int y, int width, int height, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0083: // WM_NCLBUTTONDBLCLK
                    m.Result = IntPtr.Zero;
                    return;
                case WM_NCHITTEST when (int)m.Result == HTCLIENT:
                    m.Result = (IntPtr)HTCAPTION;
                    return;
            }
            base.WndProc(ref m);
        }

        private bool IsCompositionEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6) return false;
            int enabled = 0;
            DwmIsCompositionEnabled(ref enabled);
            return enabled == 1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cachedBitmap?.Dispose();
                _cachedPath?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
