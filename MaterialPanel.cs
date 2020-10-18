using System.Windows.Forms;
using System.Drawing;

namespace MaterialControl
{
    public class MaterialPanel : MaterialContainerControl
    {
        public string LabelText;
        public static new Color DefaultForeColor { get => MaterialColor.TextBoxText; }
        public virtual new Font DefaultFont { get => MaterialFont.UIBold; }
        public virtual int DefaultHeight { get => 60; }
        protected override void SetDefaultValues()
        {
            this.Font = DefaultFont;
            this.BackColor = DefaultBackColor;
            this.ForeColor = DefaultForeColor;
            this.Height = DefaultHeight;
        }
        public MaterialPanel()
        {
            SetDefaultValues();
        }
        public MaterialPanel(string labelText)
        {
            SetDefaultValues();
            this.LabelText = labelText;
        }
        public MaterialPanel(string labelText, Control control)
        {
            SetDefaultValues();
            this.LabelText = labelText;
            control.Dock = DockStyle.Bottom;
            this.Controls.Add(control);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            TextRenderer.DrawText(e.Graphics, LabelText, Font, e.ClipRectangle, MaterialColor.TextBoxText, this.BackColor, TextFormatFlags.Top | TextFormatFlags.HorizontalCenter);
            ControlPaint.DrawBorder(e.Graphics, this.Bounds, this.BackColor, ButtonBorderStyle.Solid);
        }
    }
}
