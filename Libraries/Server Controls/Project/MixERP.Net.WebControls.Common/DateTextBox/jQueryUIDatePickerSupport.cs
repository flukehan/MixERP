using MixERP.Net.Common;
using MixERP.Net.WebControls.Common.Helpers;

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
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox
    {
        private void AddjQueryUiDatePicker()
        {
            string script = this.GetDatePickerScript(this.ID);

            Page p = HttpContext.Current.CurrentHandler as Page;
            PageUtility.RegisterJavascript("datePicker_" + this.ID + RandomNumber().ToString(CultureInfo.InvariantCulture), script, p);
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