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

namespace UITest
{
    public partial class UI : Form
    {
        public UI()
        {
            var layout = new List<Control>()
            {
                new MaterialTextField("Query"), 
                new MaterialTextField("Source"), 
                new MaterialTextField("Category"),
                new MaterialDateTimePickerField("From"),
                new MaterialTextField("Language"), 
                new MaterialTextField("Sort by"), 
                new MaterialDateTimePickerField("To"),
                new HSpacer(),
                new MaterialTextField("Dummy"),
                new MaterialButton("Search"),
            };
            InitializeComponent();
            this.form.BeginUpdate();
            foreach(var control in layout)
            {
                this.form.Controls.Add(control);
            }
            this.form.EndUpdate();
        }
    }
}
