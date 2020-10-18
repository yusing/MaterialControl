using System;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialControl
{
    public class MaterialLabel : Label
    {
        public new Font DefaultFont { get => MaterialTextControl.DefaultFont; }
        public new Color DefaultBackColor { get => MaterialTextControl.DefaultBackColor; }
        public new Color DefaultForeColor { get => MaterialTextControl.DefaultForeColor; }
        public MaterialLabel()
        {
            this.Font = DefaultFont;
            this.BackColor = DefaultBackColor;
            this.ForeColor = DefaultForeColor;
        }
    }
}
