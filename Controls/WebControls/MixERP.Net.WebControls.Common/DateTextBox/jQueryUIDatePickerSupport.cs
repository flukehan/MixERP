using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.Common.Helpers;

namespace MixERP.Net.WebControls.Common
{
    public partial class DateTextBox
    {
        private void AddjQueryUIDatePicker()
        {
            string script = this.GetDatePickerScript(this.ID);

            Page p = System.Web.HttpContext.Current.CurrentHandler as Page;
            MixERP.Net.Common.PageUtility.ExecuteJavaScript("datePicker_" + this.ID + this.RandomNumber().ToString(), script, p);
        }

        private int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(2000);
        }

        private string GetDatePickerScript(string controlId)
        {
            string script = string.Empty;
            string selector = "$('#" + controlId + "')";
            string locale = JQueryUIHelper.GetDatePickerLocale();

            script = "function pageLoad(sender, args) {$(function() {" + selector + ".datepicker({";
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
            script += "}";
            return script;
        }

        private string GetParameters()
        {
            string script = string.Empty;
            int index = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string format = JQueryUIHelper.GetDatePickerFormat();
            bool showWeekNumber = JQueryUIHelper.ShowWeekNumber();
            int weekStartDay = JQueryUIHelper.GetWeekStartDay();
            DateTime? minDate = this.MinDate;
            DateTime? maxDate = this.MaxDate;
            string numberOfMonths = JQueryUIHelper.GetNumberOfMonths();

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
