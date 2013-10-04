/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox : CompositeControl
    {
        TextBox textBox;
        CalendarExtender extender;
        CompareValidator validator;

        protected override void Render(HtmlTextWriter w)
        {
            textBox.RenderControl(w);
            extender.RenderControl(w);

            if(this.EnableValidation)
            {
                validator.RenderControl(w);
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            textBox = new TextBox();
            textBox.ID = this.ID;

            extender = new CalendarExtender();
            extender.ID = this.ID + "CalendarExtender";

            extender.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            extender.TodaysDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;
            extender.TargetControlID = textBox.ClientID;
            extender.PopupButtonID = textBox.ClientID;

            validator = new CompareValidator();
            validator.Display = ValidatorDisplay.Dynamic;

            validator.ID = this.ID + "CompareValidator";
            validator.ControlToValidate = this.ID;
            validator.ValueToCompare = "1/1/1900";
            validator.Type = ValidationDataType.Date;

            if(string.IsNullOrWhiteSpace(this.InvalidDateValidationMessage))
            {
                this.InvalidDateValidationMessage = "Invalid date";
            }

            validator.ErrorMessage = this.InvalidDateValidationMessage;
            validator.EnableClientScript = true;
            validator.CssClass = this.ValidatorCssClass;

            this.Controls.Add(textBox);
            this.Controls.Add(extender);

            if(this.EnableValidation)
            {
                this.Controls.Add(validator);
            }
        }

    }
}
