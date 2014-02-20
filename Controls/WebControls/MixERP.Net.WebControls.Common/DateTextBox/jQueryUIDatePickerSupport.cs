/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.WebControls.Common.Helpers;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox
    {
        private void AddjQueryUiDatePicker()
        {
            string script = this.GetDatePickerScript(this.ID);

            Page p = HttpContext.Current.CurrentHandler as Page;
            PageUtility.ExecuteJavaScript("datePicker_" + this.ID + RandomNumber().ToString(CultureInfo.InvariantCulture), script, p);
        }

        private static int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(2000);
        }

        private string GetDatePickerScript(string controlId)
        {
            string selector = "$('#" + controlId + "')";
            string locale = JQueryUiHelper.GetDatePickerLocale();

            string script = "$(function() {" + selector + ".datepicker({";
            script += this.GetParameters();
            script += "}";

            if (!string.IsNullOrWhiteSpace(locale))
            {
                if (!locale.Contains("iv"))
                {
                    script += ",$.datepicker.regional['" + locale + "']";
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
            string format = JQueryUiHelper.GetDatePickerFormat();
            bool showWeekNumber = JQueryUiHelper.ShowWeekNumber();
            int weekStartDay = JQueryUiHelper.GetWeekStartDay();
            DateTime? minDate = this.MinDate;
            DateTime? maxDate = this.MaxDate;
            string numberOfMonths = JQueryUiHelper.GetNumberOfMonths();

            parameters.Add("dateFormat", "'" + format + "'");

            if (showWeekNumber)
            {
                parameters.Add("showWeek", "true");
            }
            else
            {
                parameters.Add("showWeek", "false");
            }


            parameters.Add("firstDay", (weekStartDay).ToString(CultureInfo.InvariantCulture));
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
    }
}
