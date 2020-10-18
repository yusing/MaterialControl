using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MaterialControl
{
    public class MaterialUtils
    {
        public static void DrawBorder(Graphics graphics, Rectangle bounds, Color borderColor, ButtonBorderStyle buttonBorderStyle = ButtonBorderStyle.Solid, int width = 1)
        {
            ControlPaint.DrawBorder(graphics, bounds, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle);
        }
        public static void DrawBorder(Control control, Color borderColor, ButtonBorderStyle buttonBorderStyle = ButtonBorderStyle.Solid, int width = 1)
        {
            using (var graphics = control.CreateGraphics())
                ControlPaint.DrawBorder(graphics, control.ClientRectangle, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle, borderColor, width, buttonBorderStyle);
        }

        public static MaterialPanel Field<FieldControl>(string labelText, string name = null) where FieldControl : Control, new()
        {
            var field = new MaterialPanel(labelText, new FieldControl() { Dock = DockStyle.Bottom })
            {
                Name = name is null ? labelText : name
            };
            return field;
        }

        public static Control HSpacer(int size = 8)
        {
            return new Control
            {
                Enabled = false,
                Visible = false,
                Height = size
            };
        }
    }
}
