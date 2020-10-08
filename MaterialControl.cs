using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaterialControl
{
    public static class MaterialColor
    {
        public static readonly Color COLOR_SURFACE = Color.FromArgb(0x12, 0x12, 0x12);
        public static readonly Color COLOR_ON_SURFACE = Color.White;
        public static readonly Color COLOR_PRIMARY = Color.FromArgb(0xbb, 0x86, 0xfc);
        public static readonly Color COLOR_PRIMARY_VARIANT = Color.FromArgb(0x37, 0x00, 0xb3);
        public static readonly Color COLOR_DARK_PRIMARY = Color.FromArgb(0x1f, 0x1b, 0x24);
        public static readonly Color COLOR_HIGHLIGHT = Color.FromArgb(0x03, 0xda, 0xc5);

        public static readonly SolidBrush COLOR_SURFACE_BRUSH = new SolidBrush(COLOR_SURFACE);
        public static readonly SolidBrush COLOR_ON_SURFACE_BRUSH = new SolidBrush(COLOR_ON_SURFACE);
        public static readonly SolidBrush COLOR_PRIMARY_BRUSH = new SolidBrush(COLOR_PRIMARY);
        public static readonly SolidBrush COLOR_PRIMARY_VARIANT_BRUSH = new SolidBrush(COLOR_PRIMARY_VARIANT);
        public static readonly SolidBrush COLOR_DARK_PRIMARY_BRUSH = new SolidBrush(COLOR_DARK_PRIMARY);
        public static readonly SolidBrush COLOR_HIGHLIGHT_BRUSH = new SolidBrush(COLOR_HIGHLIGHT);
    }
    public class HSpacer : UserControl
    {
        public HSpacer(int height = 8)
        {
            this.Size = new Size(0, height);
            this.Enabled = false;
            this.Visible = false;
        }
    }
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
    public class MaterialButton : UserControl
    {
        public new string Text;
        public Color BorderColor = MaterialColor.COLOR_ON_SURFACE;
        public Color ColorOnHover = MaterialColor.COLOR_PRIMARY;
        public bool MouseHovered = false;
        public MaterialButtonAnimation Animation = new MaterialButtonAnimation();

        private Timer _AnimationTimer = new Timer();
        private int _AnimationDurationCounter = 0;
        private Color _BackColor;

        public MaterialButton(string text = null)
        {
            if (!(text is null))
                this.Text = text;
            this.ClientSize = new Size(80, 25);
            this.BorderColor = MaterialColor.COLOR_ON_SURFACE;
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
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.DisplayRectangle, BorderColor, ButtonBorderStyle.Solid); ;
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            e.Graphics.DrawString(this.Text, this.Font, MaterialColor.COLOR_ON_SURFACE_BRUSH, this.DisplayRectangle, sf);
            base.OnPaint(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            MouseHovered = true;
            _BackColor = BackColor;
            BackColor = MaterialColor.COLOR_PRIMARY;
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
    public class MaterialTextBox : TextBox
    {
        public MaterialTextBox()
        {
            this.BorderStyle = BorderStyle.None;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Console.WriteLine("TextBox OnPaintBackground");
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, MaterialColor.COLOR_HIGHLIGHT, ButtonBorderStyle.Solid);
            base.OnPaintBackground(e);
        }
    }
    public class MaterialTextField : GroupBox
    {
        public Label Label;
        public MaterialTextBox TextBox;
        public new string Text
        {
            get => TextBox.Text;
            set => TextBox.Text = value;
        }
        public MaterialTextField(string labelText)
        {
            this.FlatStyle = FlatStyle.Flat;
            this.Label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Dock = DockStyle.Top,
            };
            this.TextBox = new MaterialTextBox
            {
                Dock = DockStyle.Bottom,
                BackColor = MaterialColor.COLOR_DARK_PRIMARY,
                ForeColor = MaterialColor.COLOR_HIGHLIGHT
            };
            this.Height = this.TextBox.Height * 3;
            this.Controls.Add(this.Label);
            this.Controls.Add(this.TextBox);
        }
    }
    public class MaterialDateTimePicker : DateTimePicker
    {
        public new Color BackColor;
        public new string Text
        {
            get => Value.ToString(this.CustomFormat);
        }
        public MaterialDateTimePicker()
        {
            this.BackColor = MaterialColor.COLOR_DARK_PRIMARY;
            this.CalendarMonthBackground = MaterialColor.COLOR_DARK_PRIMARY;
            this.CalendarForeColor = MaterialColor.COLOR_HIGHLIGHT;
            this.CalendarTitleBackColor = MaterialColor.COLOR_DARK_PRIMARY;
            this.CalendarTitleForeColor = MaterialColor.COLOR_HIGHLIGHT;
            this.CalendarTrailingForeColor = MaterialColor.COLOR_HIGHLIGHT;
            this.Dock = DockStyle.Bottom;
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "yyyy-MM-dd";
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Console.WriteLine("DateTimePicker OnPaintBackground");
            pevent.Graphics.Clear(this.BackColor);
            base.OnPaintBackground(pevent);
        }
    }
    public class MaterialDateTimePickerField : GroupBox
    {
        public Label Label;
        public MaterialDateTimePicker Picker = new MaterialDateTimePicker();
        public new string Text
        {
            get => Picker.Value.ToString(this.Picker.CustomFormat);
        }
        public MaterialDateTimePickerField(string labelText)
        {
            this.FlatStyle = FlatStyle.Flat;
            this.Label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Dock = DockStyle.Top
            };
            this.Height = this.Picker.Height * 3;
            this.Controls.Add(this.Label);
            this.Controls.Add(this.Picker);
        }
    }
    public class MaterialListView : ListView
    {
        public MaterialListView()
        {
            this.OwnerDraw = true;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = MaterialColor.COLOR_SURFACE;
            this.ForeColor = MaterialColor.COLOR_ON_SURFACE;
            this.View = View.Details;
            this.LabelEdit = false;
            this.AllowColumnReorder = false;
            this.CheckBoxes = false;
            this.FullRowSelect = false;
            this.GridLines = false;
            this.Sorting = SortOrder.None;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(MaterialColor.COLOR_PRIMARY_BRUSH, e.Bounds);
            e.Graphics.DrawString(e.Header.Text, e.Font, MaterialColor.COLOR_ON_SURFACE_BRUSH, e.Bounds);
            // base.OnDrawColumnHeader(e);
        }
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if ((e.ItemState & ListViewItemStates.Selected) == 0)
            {
                e.DrawDefault = true;
                e.DrawBackground();
                e.DrawText();
            }
            else
            {
                e.DrawDefault = false;
                e.Graphics.FillRectangle(MaterialColor.COLOR_HIGHLIGHT_BRUSH, e.Bounds);
                e.Graphics.DrawString(e.Item.Text, this.Font, MaterialColor.COLOR_ON_SURFACE_BRUSH, e.Bounds);
            }
            // base.OnDrawSubItem(e);
        }
    }
    public class MaterialInputForm : UserControl
    {
        private bool _Update = true;
        public int ItemPaddingX = 8, ItemPaddingY = 8;
        public MaterialInputForm()
        {

        }
        public void BeginUpdate()
        {
            _Update = false;
        }
        public void EndUpdate()
        {
            _Update = true;
            UpdateControlLocation();
        }
        public void UpdateControlLocation()
        {
            var heightMax = 0;
            var point = new Point(ItemPaddingX, ItemPaddingY);
            this.SuspendLayout();
            foreach (Control control in this.Controls)
            {
                if (control is HSpacer)
                {
                    point.Y += heightMax + control.Height;
                    point.X = ItemPaddingX;
                    heightMax = 0;
                    continue;
                }
                if (point.X + control.Width < this.Width - ItemPaddingX)
                {
                    control.Location = point;
                    point.X += control.Width + ItemPaddingX * 2;
                }
                else
                {
                    // control heightMax + HSpacer height
                    point.Y += heightMax + ItemPaddingY * 2;
                    point.X = ItemPaddingX;
                    heightMax = 0;
                    control.Location = point;
                }
                if (control.Height > heightMax)
                {
                    heightMax = control.Height;
                }
            }
            this.ResumeLayout(false);
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
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Console.WriteLine("Draw InputForm border");
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, MaterialColor.COLOR_HIGHLIGHT, ButtonBorderStyle.Solid);
            base.OnPaintBackground(e);
        }
    }
}
