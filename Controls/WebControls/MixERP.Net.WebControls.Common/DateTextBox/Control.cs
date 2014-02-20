/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.WebControls.Common.Resources;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox //: CompositeControl
    {
        //Todo: Display localized calendar.
        TextBox textBox;
        CompareValidator compareValidator;
        RequiredFieldValidator requiredValidator;

        protected override void Render(HtmlTextWriter w)
        {
            this.textBox.RenderControl(w);

            if (this.EnableValidation)
            {
                this.compareValidator.RenderControl(w);
            }
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        private void InitializeDate(Frequency frequency)
        {
            //Todo:Fix this implementation.
            DateTime date = DateTime.Today;

            if (frequency == Frequency.MonthStartDate)
            {
                date = date.AddDays(1 - date.Day);
            }

            if (frequency == Frequency.MonthEndDate)
            {
                date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            }

            if (this.textBox != null)
            {
                this.textBox.Text = date.ToShortDateString();
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            this.textBox = new TextBox();
            this.textBox.ID = this.ID;

            this.compareValidator = new CompareValidator();
            this.compareValidator.Display = ValidatorDisplay.Dynamic;

            this.compareValidator.ID = this.ID + "CompareValidator";
            this.compareValidator.ControlToValidate = this.ID;
            this.compareValidator.ValueToCompare = "1/1/1900";
            this.compareValidator.Type = ValidationDataType.Date;

            this.compareValidator.ErrorMessage = CommonResource.InvalidDate;
            this.compareValidator.EnableClientScript = true;
            this.compareValidator.CssClass = this.ValidatorCssClass;

            this.requiredValidator = new RequiredFieldValidator();
            this.requiredValidator.Display = ValidatorDisplay.Dynamic;

            this.requiredValidator.ID = this.ID + "RequiredFieldValidator";
            this.requiredValidator.ControlToValidate = this.ID;
            this.requiredValidator.ErrorMessage = CommonResource.RequiredField;
            this.requiredValidator.EnableClientScript = true;
            this.requiredValidator.CssClass = this.ValidatorCssClass;

            this.Controls.Add(this.textBox);

            if (this.EnableValidation)
            {
                this.Controls.Add(this.compareValidator);
            }

            if (this.Required)
            {
                this.Controls.Add(this.requiredValidator);
            }

            this.AddjQueryUiDatePicker();
        }
    }
}
