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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox : CompositeControl
    {
        TextBox textBox;
        CompareValidator validator;

        protected override void Render(HtmlTextWriter w)
        {
            textBox.RenderControl(w);

            if (this.EnableValidation)
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

            validator = new CompareValidator();
            validator.Display = ValidatorDisplay.Dynamic;

            validator.ID = this.ID + "CompareValidator";
            validator.ControlToValidate = this.ID;
            validator.ValueToCompare = "1/1/1900";
            validator.Type = ValidationDataType.Date;

            validator.ErrorMessage = Resources.CommonResource.InvalidDate;
            validator.EnableClientScript = true;
            validator.CssClass = this.ValidatorCssClass;

            this.Controls.Add(textBox);

            if (this.EnableValidation)
            {
                this.Controls.Add(validator);
            }

            this.AddScript();
        }

        private void AddScript()
        {
            string script = this.GetDatePickerScript(this.ID);

            Page p = System.Web.HttpContext.Current.CurrentHandler as Page;
            MixERP.Net.Common.PageUtility.ExecuteJavaScript("datePicker_" + this.ID, script, p);
        }

        private string GetDatePickerScript(string controlId)
        {
            string script = string.Empty;
            string selector = "$('#" + controlId + "')";
            string locale = this.GetDatePickerLocale();

            script = "$(function() {" + selector + ".datepicker({";
            script += this.GetParameters();
            script += "}";

            if (!string.IsNullOrWhiteSpace(locale))
            {
                if (!locale.Contains("iv"))
                {
                    script += ",$.datepicker.regional['" + this.GetDatePickerLocale() + "'";
                }
            }

            script += ");";
            script += "});";
            return script;
        }

        private string GetParameters()
        {
            string script = string.Empty;
            int index = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string format = this.GetDatePickerFormat();
            bool showWeekNumber = this.ShowWeekNumber();
            int weekStartDay = this.GetWeekStartDay();
            DateTime? minDate = this.MinDate;
            DateTime? maxDate = this.MaxDate;
            string numberOfMonths = this.GetNumberOfMonths();

            parameters.Add("dateFormat", "'" + format + "'");
            parameters.Add("showWeek", showWeekNumber.ToString().ToLower());
            parameters.Add("firstDay", (weekStartDay - 1).ToString());
            parameters.Add("constrainInput", "false");

            if (minDate != null)
            {
                parameters.Add("minDate", "new Date(" + minDate.Value.Year + "," + minDate.Value.Month + "," + minDate.Value.Day + ")");
            }

            if (maxDate != null)
            {
                parameters.Add("maxDate", "new Date(" + maxDate.Value.Year + "," + maxDate.Value.Month + "," + maxDate.Value.Day + ")");
            }

            parameters.Add("numberOfMonths", numberOfMonths);

            foreach (var item in parameters)
            {
                if (!index.Equals(0))
                {
                    script += ",";
                }

                script += item.Key + ":" + item.Value;
                index++;
            }

            return script;
        }

        private string GetDatePickerFormat()
        {
            return ConfigurationHelper.GetParameter("DatePickerDateFormat");
        }

        private string GetDatePickerLocale()
        {
            return MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture().TwoLetterISOLanguageName;
        }

        private int GetWeekStartDay()
        {
            return Conversion.TryCastInteger(ConfigurationHelper.GetParameter("DatePickerWeekStartDay"));
        }

        private bool ShowWeekNumber()
        {
            return ConfigurationHelper.GetParameter("DatePickerShowWeekNumber").Equals("true");
        }

        private string GetNumberOfMonths()
        {
            return ConfigurationHelper.GetParameter("DatePickerNumberOfMonths");
        }
    }
}
