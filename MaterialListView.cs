using System;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialControl
{
    public class MaterialListView : ListView
    {
        public new Color DefaultBackColor { get => MaterialColor.Surface; }
        public new Color DefaultForeColor { get => MaterialColor.TextBoxText; }
        public MaterialListView()
        {
            this.OwnerDraw = true;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = DefaultBackColor;
            this.ForeColor = DefaultForeColor;
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
            e.Graphics.FillRectangle(MaterialBrush.Primary, e.Bounds);
            e.Graphics.DrawString(e.Header.Text, e.Font, MaterialBrush.TextBoxText, e.Bounds);
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
                e.Graphics.FillRectangle(MaterialBrush.TextBox, e.Bounds);
                e.Graphics.DrawString(e.Item.Text, this.Font, MaterialBrush.TextBoxText, e.Bounds);
            }
        }
    }
}
