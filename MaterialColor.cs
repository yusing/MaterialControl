using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialControl
{
    public static class MaterialColor
    {
        public static readonly Color Surface = Color.FromArgb(0x12, 0x12, 0x12);
        public static readonly Color TextBoxText = Color.White;
        public static readonly Color Primary = Color.FromArgb(0xbb, 0x86, 0xfc);
        public static readonly Color PrimaryVariant = Color.FromArgb(0x37, 0x00, 0xb3);
        public static readonly Color DarkPrimary = Color.FromArgb(0x1f, 0x1b, 0x24);
        public static readonly Color TextBox = Color.FromArgb(0x03, 0xda, 0xc5);
        public static readonly Color TextBoxBorder = Color.FromArgb(0xbb, 0x86, 0xfc);
        public static readonly Color Hover = Color.FromArgb(0xbb, 0x86, 0xfc);
        public static readonly Color Hint = Color.Gray;
        public static readonly Color Error = Color.Red;
    }

    public static class MaterialBrush
    {
        public static readonly SolidBrush Surface = new SolidBrush(MaterialColor.Surface);
        public static readonly SolidBrush TextBoxText = new SolidBrush(MaterialColor.TextBoxText);
        public static readonly SolidBrush Primary = new SolidBrush(MaterialColor.Primary);
        public static readonly SolidBrush PrimaryVariant = new SolidBrush(MaterialColor.PrimaryVariant);
        public static readonly SolidBrush DarkPrimary = new SolidBrush(MaterialColor.DarkPrimary);
        public static readonly SolidBrush TextBox = new SolidBrush(MaterialColor.TextBox);
    }
}
