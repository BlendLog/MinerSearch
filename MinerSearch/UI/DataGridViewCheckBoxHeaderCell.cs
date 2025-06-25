using System;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        public event EventHandler CheckBoxClicked;
        private Point _location;
        private Size _size;
        private CheckState _checkState = CheckState.Unchecked;

        public CheckState CheckState
        {
            get => _checkState;
            set
            {
                if (_checkState != value)
                {
                    _checkState = value;
                    this.DataGridView?.InvalidateCell(this);
                }
            }
        }

        public void SetState(CheckState state)
        {
            _checkState = state;
            this.DataGridView?.InvalidateCell(this);
        }


        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
                                      int rowIndex, DataGridViewElementStates dataGridViewElementState,
                                      object value, object formattedValue, string errorText,
                                      DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            // Background
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                using (var backBrush = new SolidBrush(cellStyle.BackColor))
                    graphics.FillRectangle(backBrush, cellBounds);

            // Border
            if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

            // Checkbox rectangle
            _size = new Size(14, 14);
            _location = new Point(
                cellBounds.X + 4,
                cellBounds.Y + (cellBounds.Height / 2) - (_size.Height / 2)
            );
            Rectangle checkboxRect = new Rectangle(_location, _size);

            ButtonState state = ButtonState.Normal;
            if (_checkState == CheckState.Checked)
                state |= ButtonState.Checked;
            else if (_checkState == CheckState.Indeterminate)
                state |= ButtonState.Inactive;

            ControlPaint.DrawCheckBox(graphics, checkboxRect, state);

            if (_checkState == CheckState.Indeterminate)
            {
                Rectangle inner = Rectangle.Inflate(checkboxRect, -4, -4);
                graphics.FillRectangle(Brushes.Gray, inner);
            }

            // Draw text manually to avoid overlap
            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
            {
                string headerText = this.Value?.ToString() ?? string.Empty;
                using (Brush textBrush = new SolidBrush(cellStyle.ForeColor))
                {
                    Rectangle textRect = new Rectangle(
                        _location.X + _size.Width + 4,
                        cellBounds.Y,
                        cellBounds.Width - (_size.Width + 8),
                        cellBounds.Height
                    );

                    StringFormat sf = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    };

                    graphics.DrawString(headerText, cellStyle.Font, textBrush, textRect, sf);
                }
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Rectangle checkBoxRect = new Rectangle(_location, _size);

            if (checkBoxRect.Contains(e.Location))
            {
                if (_checkState == CheckState.Unchecked)
                    _checkState = CheckState.Checked;
                else if (_checkState == CheckState.Checked)
                    _checkState = CheckState.Unchecked;
                else if (_checkState == CheckState.Indeterminate)
                    _checkState = CheckState.Checked;

                CheckBoxClicked?.Invoke(this, EventArgs.Empty);
                this.DataGridView?.InvalidateCell(this);
            }

            base.OnMouseClick(e);
        }
    }

}
