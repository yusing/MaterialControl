using MaterialControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MaterialControl.MaterialUtils;

namespace UITest
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
            this.form.BeginUpdate();
            this.form.Controls.Add(Field<MaterialTextBox>("Source"));
            this.form.Controls.Add(Field<MaterialTextBox>("Category"));
            this.form.Controls.Add(Field<MaterialDateTimeInput>("From"));
            this.form.Controls.Add(Field<MaterialTextBox>("Language"));
            this.form.Controls.Add(Field<MaterialTextBox>("Sort by"));
            this.form.Controls.Add(Field<MaterialDateTimeInput>("To"));
            this.form.Controls.Add(HSpacer());
            this.form.Controls.Add(new MaterialControl.MaterialButton("Search"));
            this.form.EndUpdate();
        }
    }
}
