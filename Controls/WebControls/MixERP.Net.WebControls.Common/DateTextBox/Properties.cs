using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox : CompositeControl
    {
        public Unit ControlWidth
        {
            get
            {
                EnsureChildControls();
                return textBox.Width;
            }
            set
            {
                EnsureChildControls();
                textBox.Width = value;
            }
        }
        public override string CssClass { get; set; }
        public bool Disabled
        {
            get
            {
                EnsureChildControls();
                return !textBox.Enabled;
            }
            set
            {
                EnsureChildControls();
                textBox.Enabled = !value;
            }
        }
        public bool EnableValidation { get; set; }
        public override string ID { get; set; }
        public string InvalidDateValidationMessage { get; set; }
        public string Text 
        {
            get
            {
                EnsureChildControls();
                return textBox.Text;
            }
            set
            {
                EnsureChildControls();
                textBox.Text = value;
            }
        }
        public string ValidatorCssClass { get; set; }

    }
}
