using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitsYourOut.Classes
{
    public class TextBoxWithPlaceHolder : TextBox
    {
        public string Placeholder { get; set; }

        public TextBoxWithPlaceHolder(string placeholder)
        {
            Placeholder = placeholder;
            ForeColor = Color.Gray;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!DesignMode)
            {
                Text = Placeholder;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (Text.Equals(string.Empty))
            {
                Text = Placeholder;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (Text.Equals(Placeholder))
            {
                Text = string.Empty;
            }
        }
    }
}
