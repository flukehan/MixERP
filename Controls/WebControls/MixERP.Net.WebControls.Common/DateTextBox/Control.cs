using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.Common.Helpers;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox : CompositeControl
    {
        //Todo: Display localized calendar.
        TextBox textBox;
        CompareValidator compareValidator;
        RequiredFieldValidator requiredValidator;

        protected override void Render(HtmlTextWriter w)
        {
            textBox.RenderControl(w);

            if (this.EnableValidation)
            {
                compareValidator.RenderControl(w);
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        private void InitializeDate(MixERP.Net.Common.Models.Core.Frequency frequency)
        {
            //Todo:Fix this implementation.
            DateTime date = DateTime.Today;

            if (frequency == Net.Common.Models.Core.Frequency.MonthStartDate)
            {
                date = date.AddDays(1 - date.Day);
            }

            if (frequency == Net.Common.Models.Core.Frequency.MonthEndDate)
            {
                date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            }

            if (textBox != null)
            {
                textBox.Text = date.ToShortDateString();
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            textBox = new TextBox();
            textBox.ID = this.ID;

            compareValidator = new CompareValidator();
            compareValidator.Display = ValidatorDisplay.Dynamic;

            compareValidator.ID = this.ID + "CompareValidator";
            compareValidator.ControlToValidate = this.ID;
            compareValidator.ValueToCompare = "1/1/1900";
            compareValidator.Type = ValidationDataType.Date;

            compareValidator.ErrorMessage = Resources.CommonResource.InvalidDate;
            compareValidator.EnableClientScript = true;
            compareValidator.CssClass = this.ValidatorCssClass;

            requiredValidator = new RequiredFieldValidator();
            requiredValidator.Display = ValidatorDisplay.Dynamic;

            requiredValidator.ID = this.ID + "RequiredFieldValidator";
            requiredValidator.ControlToValidate = this.ID;
            requiredValidator.ErrorMessage = Resources.CommonResource.RequiredField;
            requiredValidator.EnableClientScript = true;
            requiredValidator.CssClass = this.ValidatorCssClass;

            this.Controls.Add(textBox);

            if (this.EnableValidation)
            {
                this.Controls.Add(compareValidator);
            }

            if (this.Required)
            {
                this.Controls.Add(requiredValidator);
            }

            this.AddjQueryUIDatePicker();
        }
    }
}
