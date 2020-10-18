using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialControl
{
    public class MaterialTextBox : TextBox
    {
        public new Font DefaultFont { get => MaterialTextControl.DefaultFont; }
        public Color DefaultHintColor { get => MaterialColor.Hint; }
        public Color DefaultHoverBorderColor { get => MaterialColor.Hover; }
        public new Color DefaultBackColor { get => MaterialTextControl.DefaultBackColor; }
        public new Color DefaultForeColor { get => MaterialTextControl.DefaultForeColor; }
        public int DefaultHeight { get => 20; }
        public bool DefaultShortcutsEnabled { get => true; }
        public bool DefaultShowHoverBorder { get => true; }
        public bool DefaultShowTooltipOnHover { get => true; }
        public HorizontalAlignment DefaultTextAlign { get => HorizontalAlignment.Center; }
        
        public Color HintColor;
        public Color HoverBorderColor;
        public bool ShowHoverBorder;
        public bool ShowTooltipOnHover;
        public string Hint = "";
        private ToolTip _ToolTip = new ToolTip();
        public MaterialTextBox()
        {
            this.Font = DefaultFont;
            this.BackColor = DefaultBackColor;
            this.ForeColor = DefaultForeColor;
            this.HintColor = DefaultHintColor;
            this.HoverBorderColor = DefaultHoverBorderColor;
            this.BorderStyle = BorderStyle.None;
            this.Height = DefaultHeight;
            this.ShortcutsEnabled = DefaultShortcutsEnabled;
            this.TextAlign = DefaultTextAlign;
            this.ShowHoverBorder = DefaultShowHoverBorder;
            this.ShowTooltipOnHover = DefaultShowTooltipOnHover;
        }
        protected virtual void DrawHintTextIfEmpty()
        {
            if (Text == "" && Hint != "")
            {
                using (var graphics = CreateGraphics())
                {
                    TextRenderer.DrawText(graphics, Hint, Font, ClientRectangle, Color.Gray, BackColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.WordEllipsis);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            DrawHintTextIfEmpty();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ShowHoverBorder)
                MaterialUtils.DrawBorder(this, HoverBorderColor);
            if (ShowTooltipOnHover && Hint != "")
                _ToolTip.Show(this.Hint, this, PointToClient(Cursor.Position));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ShowHoverBorder)
                MaterialUtils.DrawBorder(this, BackColor);
            DrawHintTextIfEmpty();
            _ToolTip.Hide(this);
            base.OnMouseLeave(e);
        }
    }

    public class MaterialTextField : MaterialPanel
    {
        public MaterialTextBox InputBox = new MaterialTextBox
        {
            Dock = DockStyle.Bottom,
        };
        public new string Text
        {
            get => InputBox.Text;
            set => InputBox.Text = value;
        }
        public MaterialTextField(string labelText)
        {
            this.LabelText = labelText;
            this.Controls.Add(this.InputBox);
        }
    }
}
