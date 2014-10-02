/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common.Models.Core;
using MixERP.Net.WebControls.Common.Resources;

/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox
    {
        public TextBox textBox;
        public CompareValidator compareValidator;
        public RequiredFieldValidator requiredValidator;

        protected override void Render(HtmlTextWriter w)
        {
            this.textBox.RenderControl(w);

            if (this.EnableValidation)
            {
                this.requiredValidator.RenderControl(w);
                this.compareValidator.RenderControl(w);
            }
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
            base.RecreateChildControls();
        }

        private void InitializeDate(Frequency frequency)
        {
            DateTime date = DateTime.Today;

            if (frequency == Frequency.MonthStartDate)
            {
                date = date.AddDays(1 - date.Day);
            }

            if (frequency == Frequency.MonthEndDate)
            {
                date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            }

            if (frequency == Frequency.FiscalYearStartDate)
            {
                date = new DateTime(date.Year, 1, 1);
            }

            if (frequency == Frequency.FiscalYearEndDate)
            {
                date = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12));
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
            this.compareValidator.Display = ValidatorDisplay.Static;

            this.compareValidator.ID = this.ID + "CompareValidator";
            this.compareValidator.ControlToValidate = this.ID;
            this.compareValidator.ValueToCompare = "1/1/1900";
            this.compareValidator.Type = ValidationDataType.Date;
            this.compareValidator.Operator = ValidationCompareOperator.GreaterThan;

            this.compareValidator.ErrorMessage = CommonResource.InvalidDate;
            this.compareValidator.EnableClientScript = true;
            this.compareValidator.CssClass = this.ValidatorCssClass;
            this.compareValidator.SetFocusOnError = true;

            this.requiredValidator = new RequiredFieldValidator();
            this.requiredValidator.Display = ValidatorDisplay.Static;

            this.requiredValidator.ID = this.ID + "RequiredFieldValidator";
            this.requiredValidator.ControlToValidate = this.ID;
            this.requiredValidator.ErrorMessage = CommonResource.RequiredField;
            this.requiredValidator.EnableClientScript = true;
            this.requiredValidator.CssClass = this.ValidatorCssClass;
            this.requiredValidator.SetFocusOnError = true;

            this.Controls.Add(this.textBox);

            if (this.Required)
            {
                this.Controls.Add(this.requiredValidator);
            }

            if (this.EnableValidation)
            {
                this.Controls.Add(this.compareValidator);
            }

            Net.Common.jQueryHelper.jQueryUI.AddjQueryUIDatePicker(this.Page, textBox.ID, this.MinDate, this.MaxDate);
            base.CreateChildControls();
        }
    }
}