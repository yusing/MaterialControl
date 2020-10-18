using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Threading;
namespace MaterialControl
{
    public class MaterialDateTimeInput : MaterialTextBox
    {
        public Color DefaultColorOnError { get => MaterialColor.Error; }
        public string DefaultDateTimeFormat { get => "yyyy-MM-dd"; }
        public new ImeMode DefaultImeMode { get => ImeMode.Disable; }
        public DateTime DefaultDateTime { get => DateTime.Now; }
        public Color ColorOnError;
        public string DateTimeFormat
        {
            get => Hint;
            set => Hint = value;
        }
        public DateTime DateTime
        {
            get => DateTime.ParseExact(Text, DateTimeFormat, CultureInfo.InvariantCulture);
        }
        public bool IsValid
        {
            get => _IsValid;
        }
        private bool _IsValid {
            get => default;
            set
            {
                ShowHoverBorder = value;
            }
        }
        private Thread _CheckFormatThread;
        private ThreadStart _CheckFormatThreadStart;
        public MaterialDateTimeInput()
        {
            this.ColorOnError = DefaultColorOnError;
            this.DateTimeFormat = DefaultDateTimeFormat;
            this.Text = DefaultDateTime.ToString(DefaultDateTimeFormat);
            this.ImeMode = DefaultImeMode;
            this.TextChanged += (sender, e) => CheckFormat();
            _CheckFormatThreadStart = new ThreadStart(() =>
            {
                if (Text == string.Empty)
                    return;
                while (true)
                {
                    try
                    {
                        var dt = DateTime.ParseExact(Text, DateTimeFormat, CultureInfo.InvariantCulture);
                        _CheckFormatThread = null;
                        _IsValid = true;
                        MaterialUtils.DrawBorder(this, BackColor, width: 2);
                        return;
                    }
                    catch
                    {
                        _IsValid = false;
                        MaterialUtils.DrawBorder(this, ColorOnError, width: 2);
                    }
                    Thread.Sleep(500);
                }
            });
        }

        protected virtual void CheckFormat()
        {
            if (_CheckFormatThread == null)
            {
                _CheckFormatThread = new Thread(_CheckFormatThreadStart)
                {
                    IsBackground = true
                };
                _CheckFormatThread.Start();
            }
        }
    }
    public class MaterialDateTimeInputField : MaterialPanel
    {
        public MaterialDateTimeInput Picker = new MaterialDateTimeInput();
        public new string Text
        {
            get => Picker.Text;
        }
        public MaterialDateTimeInputField(string labelText)
        {
            this.LabelText = labelText;
            this.Controls.Add(this.Picker);
        }
    }
}
