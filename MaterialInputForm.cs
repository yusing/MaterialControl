using System;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialControl
{
    public class MaterialInputForm : UserControl
    {
        private bool _Update = true;
        public int ItemPaddingX = 8, ItemPaddingY = 8;
        public Color BorderColor = MaterialColor.Surface;
        public int BorderWidth = 5;
        private int _Rows = 0, _Cols = 0;
        public int Rows { get => _Rows; }
        public MaterialInputForm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ResizeRedraw = true;
        }
        
        public void BeginUpdate()
        {
            _Update = false;
            this.SuspendLayout();
        }
        public void EndUpdate()
        {
            _Update = true;
            UpdateControlLocation();
            this.ResumeLayout(false);
        }

        public void UpdateControlLocation()
        {
            var heightMax = 0;
            var point = new Point(ItemPaddingX, ItemPaddingY);
            _Rows = _Cols = 0;
            foreach (Control control in this.Controls)
            {
                if (!control.Enabled && !control.Visible) // HSpacer
                {
                    point.Y += heightMax + control.Height;
                    point.X = ItemPaddingX;
                    heightMax = 0;
                    continue;
                }
                if (point.X + control.Width < this.Location.X + this.Width - ItemPaddingX)
                {
                    control.Location = point;
                    point.X += control.Width + ItemPaddingX * 2;
                }
                else
                {
                    // control heightMax + HSpacer height
                    ++_Rows;
                    _Cols = 0;
                    point.Y += heightMax + ItemPaddingY * 2;
                    point.X = ItemPaddingX;
                    heightMax = 0;
                    control.Location = point;
                    point.X += control.Width + ItemPaddingX * 2;
                }
                if (control.Height > heightMax)
                {
                    heightMax = control.Height;
                }
                Console.WriteLine($"Control {control.Name} (row {_Rows}, col {_Cols}) is at ({control.Location.X},{control.Location.Y})");
                ++_Cols;
            }
            
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (_Update)
                UpdateControlLocation();
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (_Update)
                UpdateControlLocation();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (BorderWidth != 0)
                MaterialUtils.DrawBorder(e.Graphics, e.ClipRectangle, BorderColor, ButtonBorderStyle.Solid, BorderWidth);
        }
    }
}
