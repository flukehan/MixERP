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

using MixERP.Net.ApplicationState;
using MixERP.Net.Common.jQueryHelper;
using MixERP.Net.Entities;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common.Helpers;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public sealed partial class DateTextBox : CompositeControl
    {
        public CompareValidator compareValidator;
        public CompareValidator maxDateCompareValidator;
        public RequiredFieldValidator requiredValidator;
        public TextBox textBox;

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            this.textBox = new TextBox();
            this.textBox.ID = this.ID;
            this.textBox.CssClass = "date";

            this.AddMinDateValidator();
            this.AddMaxDateValidator();

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

            jQueryUI.AddjQueryUIDatePicker(this.Page, this.textBox.ID, this.MinDate, this.MaxDate);

            base.CreateChildControls();
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
            base.RecreateChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.EnsureChildControls();
            if (this.textBox != null)
            {
                this.textBox.RenderControl(w);
            }


            if (this.EnableValidation)
            {
                if (this.requiredValidator != null)
                {
                    this.requiredValidator.RenderControl(w);
                }

                if (this.compareValidator != null)
                {
                    this.compareValidator.RenderControl(w);
                }
            }
        }

        private void AddMaxDateValidator()
        {
            if (this.MaxDate == null)
            {
                return;
            }

            this.maxDateCompareValidator = new CompareValidator();
            this.maxDateCompareValidator.Display = ValidatorDisplay.Static;

            this.maxDateCompareValidator.ID = this.ID + "MaxDateCompareValidator";
            this.maxDateCompareValidator.ControlToValidate = this.ID;
            this.maxDateCompareValidator.Type = ValidationDataType.Date;
            this.maxDateCompareValidator.Operator = ValidationCompareOperator.LessThan;

            this.maxDateCompareValidator.EnableClientScript = true;
            this.maxDateCompareValidator.CssClass = this.ValidatorCssClass;
            this.maxDateCompareValidator.SetFocusOnError = true;


            this.maxDateCompareValidator.ValueToCompare = this.MaxDate.ToString();
            this.maxDateCompareValidator.ErrorMessage = string.Format(CultureInfo.CurrentCulture, CommonResource.DateMustBeLessThan, this.MinDate);
        }

        private void AddMinDateValidator()
        {
            this.compareValidator = new CompareValidator();
            this.compareValidator.Display = ValidatorDisplay.Static;

            this.compareValidator.ID = this.ID + "CompareValidator";
            this.compareValidator.ControlToValidate = this.ID;
            this.compareValidator.Type = ValidationDataType.Date;
            this.compareValidator.Operator = ValidationCompareOperator.GreaterThan;

            this.compareValidator.EnableClientScript = true;
            this.compareValidator.CssClass = this.ValidatorCssClass;
            this.compareValidator.SetFocusOnError = true;


            if (this.MinDate != null)
            {
                this.compareValidator.ValueToCompare = this.MinDate.ToString();
                this.compareValidator.ErrorMessage = string.Format(CultureInfo.CurrentCulture, CommonResource.DateMustBeGreaterThan, this.MinDate);
            }
            else
            {
                this.compareValidator.ValueToCompare = "1/1/1900";
                this.compareValidator.ErrorMessage = CommonResource.InvalidDate;
            }
        }

        private void InitializeDate()
        {
            if (this.officeId.Equals(0))
            {
                return;
            }

            FrequencyDates model = DatePersister.GetFrequencyDates(this.Catalog, this.officeId);

            DateTime date = model.Today;

            switch (this.mode)
            {
                case FrequencyType.MonthStartDate:
                    date = model.MonthStartDate;
                    break;
                case FrequencyType.MonthEndDate:
                    date = model.MonthEndDate;
                    break;
                case FrequencyType.QuarterStartDate:
                    date = model.QuarterStartDate;
                    break;
                case FrequencyType.QuarterEndDate:
                    date = model.QuarterEndDate;
                    break;
                case FrequencyType.HalfStartDate:
                    date = model.FiscalHalfStartDate;
                    break;
                case FrequencyType.HalfEndDate:
                    date = model.FiscalHalfEndDate;
                    break;
                case FrequencyType.FiscalYearStartDate:
                    date = model.FiscalYearStartDate;
                    break;
                case FrequencyType.FiscalYearEndDate:
                    date = model.FiscalYearEndDate;
                    break;
            }


            if (this.textBox != null)
            {
                this.textBox.Text = date.ToShortDateString();
            }
        }
    }
}