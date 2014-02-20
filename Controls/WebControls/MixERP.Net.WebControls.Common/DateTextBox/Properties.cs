/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox : CompositeControl
    {
        public override Unit Width
        {
            get
            {
                this.EnsureChildControls();
                return this.textBox.Width;
            }
            set
            {
                this.EnsureChildControls();
                this.textBox.Width = value;
            }
        }

        public override string CssClass 
        {
            get
            {
                this.EnsureChildControls();
                return this.textBox.CssClass;
            }
            set
            {
                this.EnsureChildControls();
                this.textBox.CssClass = value;
            }
        }

        public bool Disabled
        {
            get
            {
                this.EnsureChildControls();
                return !this.textBox.Enabled;
            }
            set
            {
                this.EnsureChildControls();
                this.textBox.Enabled = !value;
            }
        }
        public bool EnableValidation { get; set; }
        public override string ID { get; set; }
        public string Text 
        {
            get
            {
                this.EnsureChildControls();
                return this.textBox.Text;
            }
            set
            {
                this.EnsureChildControls();
                this.textBox.Text = value;
            }
        }

        public string ValidatorCssClass { get; set; }

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

        public bool Required { get; set; }


        private Frequency mode;
        public Frequency Mode 
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
                this.EnsureChildControls();
                this.InitializeDate(this.mode);
            }
        }
    }
}