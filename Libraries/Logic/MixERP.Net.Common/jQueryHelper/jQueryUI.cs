using MixERP.Net.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.Common.jQueryHelper
{
    public class jQueryUI
    {
        #region "Helpers"

        /// <summary>
        /// Credit to: http://www.rajeeshcv.com/post/details/31/jqueryui-datepicker-in-asp-net-mvc
        /// Converts the .net supported date format current culture format into JQuery Datepicker format.
        /// </summary>
        /// <param name="format">Date format supported by .NET.</param>
        /// <returns>Format string that supported in JQuery Datepicker.</returns>
        private static string ConvertDateFormat(string format)
        {
            /*
             *  Date used in this comment : 5th - Nov - 2009 (Thursday)
             *
             *  .NET    JQueryUI        Output      Comment
             *  --------------------------------------------------------------
             *  d       d               5           day of month(No leading zero)
             *  dd      dd              05          day of month(two digit)
             *  ddd     D               Thu         day short name
             *  dddd    DD              Thursday    day long name
             *  M       m               11          month of year(No leading zero)
             *  MM      mm              11          month of year(two digit)
             *  MMM     M               Nov         month name short
             *  MMMM    MM              November    month name long.
             *  yy      y               09          Year(two digit)
             *  yyyy    yy              2009        Year(four digit)             *
             */

            string currentFormat = format;

            // Convert the date
            currentFormat = currentFormat.Replace("dddd", "DD");
            currentFormat = currentFormat.Replace("ddd", "D");

            // Convert month
            if (currentFormat.Contains("MMMM"))
            {
                currentFormat = currentFormat.Replace("MMMM", "MM");
            }
            else if (currentFormat.Contains("MMM"))
            {
                currentFormat = currentFormat.Replace("MMM", "M");
            }
            else if (currentFormat.Contains("MM"))
            {
                currentFormat = currentFormat.Replace("MM", "mm");
            }
            else
            {
                currentFormat = currentFormat.Replace("M", "m");
            }

            // Convert year
            currentFormat = currentFormat.Contains("yyyy") ? currentFormat.Replace("yyyy", "yy") : currentFormat.Replace("yy", "y");

            return currentFormat;
        }

        public static string GetDatePickerFormat()
        {
            string datePattern = LocalizationHelper.GetCurrentCulture().DateTimeFormat.ShortDatePattern;
            return ConvertDateFormat(datePattern);
        }

        public static string GetDatePickerLocale()
        {
            return LocalizationHelper.GetCurrentCulture().TwoLetterISOLanguageName;
        }

        public static int GetWeekStartDay()
        {
            return (int)LocalizationHelper.GetCurrentCulture().DateTimeFormat.FirstDayOfWeek;
        }

        public static bool ShowWeekNumber()
        {
            return ConfigurationHelper.GetParameter("DatePickerShowWeekNumber").Equals("true");
        }

        public static string GetNumberOfMonths()
        {
            return ConfigurationHelper.GetParameter("DatePickerNumberOfMonths");
        }

        #endregion "Helpers"

        public static void AddjQueryUIDatePicker(Page p, string controlId, DateTime? minDate, DateTime? maxDate)
        {
            if (string.IsNullOrWhiteSpace(controlId))
            {
                return;
            }

            if (p == null)
            {
                p = HttpContext.Current.Handler as Page;
            }

            string script = GetDatePickerScript(controlId, minDate, maxDate);

            PageUtility.RegisterJavascript("datePicker_" + controlId + RandomNumber().ToString(CultureInfo.InvariantCulture), script, p);
        }

        private static int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(2000);
        }

        private static string GetDatePickerScript(string controlId, DateTime? minDate, DateTime? maxDate)
        {
            string selector = "$('#" + controlId + "')";
            string locale = GetDatePickerLocale();

            string script = "$(function() {" + selector + ".datepicker({";

            script += GetParameters(minDate, maxDate);
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

        private static string GetParameters(DateTime? minDate, DateTime? maxDate)
        {
            string script = string.Empty;
            int index = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string format = GetDatePickerFormat();
            bool showWeekNumber = ShowWeekNumber();
            int weekStartDay = GetWeekStartDay();

            string numberOfMonths = GetNumberOfMonths();

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