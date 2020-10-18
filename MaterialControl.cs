using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialControl
{

    public abstract class MaterialTextControl : UserControl
    {
        [Browsable(true)]
        public override string Text { get; set; }

        public static new Color DefaultBackColor { get => MaterialColor.TextBox; }
        public static new Color DefaultForeColor { get => MaterialColor.TextBoxText; }
        public static new Font DefaultFont { get => MaterialFont.UI; }
        protected abstract void SetDefaultValues();
    }

    public abstract class MaterialContainerControl : UserControl
    {
        public static new Font DefaultFont { get => MaterialFont.UI; }
        public static new Color DefaultBackColor { get => MaterialColor.Surface; }
        protected abstract void SetDefaultValues();
    }
}
