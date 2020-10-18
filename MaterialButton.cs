using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialControl
{
    public class MaterialButtonAnimation
    {
        public int Duration = 100;
        public int Interval = 10;
        public int DeltaA = 0;
        public int DeltaR = 4;
        public int DeltaG = 0;
        public int DeltaB = 0;
        private int Next(int value, int delta)
        {
            var newValue = value + delta;
            if (newValue >= 0 && newValue < 256)
            {
                return newValue;
            }
            return value;
        }
        public Color NextColor(Color color)
        {
            return Color.FromArgb(Next(color.A, DeltaA),
                Next(color.R, DeltaR),
                Next(color.G, DeltaG), Next(color.B, DeltaB));
        }
    }
    public class MaterialButton : MaterialTextControl
    {
        public new Size DefaultSize { get => new Size(80, 25); }
        public Color DefaultBorderColor { get => MaterialColor.TextBoxText; }
        
        public Color BorderColor = MaterialColor.TextBoxText;
        public Color ColorOnHover = MaterialColor.Primary;
        public bool MouseHovered = false;
        public MaterialButtonAnimation Animation = new MaterialButtonAnimation();

        private Timer _AnimationTimer = new Timer();
        private int _AnimationDurationCounter = 0;
        private Color _BackColor;

        protected override void SetDefaultValues()
        {
            this.Font = DefaultFont;
            this.ClientSize = DefaultSize;
            this.BorderColor = DefaultBorderColor;
            _AnimationTimer.Tick += (sender, e) =>
            {
                BackColor = Animation.NextColor(BackColor);
                _AnimationDurationCounter += Animation.Interval;
                if (_AnimationDurationCounter >= Animation.Duration)
                {
                    BackColor = MouseHovered ? ColorOnHover : _BackColor;
                    _AnimationDurationCounter = 0;
                    _AnimationTimer.Stop();
                }
            };
        }

        public MaterialButton()
        {
            SetDefaultValues();
        }

        public MaterialButton(string text)
        {
            SetDefaultValues();
            this.Text = text;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.DisplayRectangle, BorderColor, ButtonBorderStyle.Solid); ;
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            e.Graphics.DrawString(this.Text, this.Font, MaterialBrush.TextBoxText, this.DisplayRectangle, sf);
            base.OnPaint(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            MouseHovered = true;
            _BackColor = BackColor;
            BackColor = MaterialColor.Hover;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            MouseHovered = false;
            BackColor = _BackColor;
            base.OnMouseLeave(e);
        }
        protected override void OnClick(EventArgs e)
        {
            BackColor = ColorOnHover;
            _AnimationTimer.Interval = Animation.Interval;
            _AnimationTimer.Start();
            base.OnClick(e);
        }
    }
}
