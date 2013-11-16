/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox : CompositeControl
    {
        public override Unit Width
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

        public override string CssClass 
        {
            get
            {
                EnsureChildControls();
                return textBox.CssClass;
            }
            set
            {
                EnsureChildControls();
                textBox.CssClass = value;
            }
        }

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

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

        //Todo
        public bool Required { get; set; }
    }
}