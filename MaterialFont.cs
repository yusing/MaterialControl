using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialControl
{
    public static class MaterialFont
    {
        public static readonly Font UI = new Font(new FontFamily("Segoe UI"), 11);
        public static readonly Font UIBold = new Font(UI, FontStyle.Bold);
        public static readonly Font Input = new Font(new FontFamily("Consolas"), 11);
        public static readonly Font InputBold = new Font(UI, FontStyle.Bold);
    }
}
