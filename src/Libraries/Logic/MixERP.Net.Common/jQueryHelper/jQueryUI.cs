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

using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.Common.jQueryHelper
{

    public static class jQueryUI
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Add jQueryUI datepicker control.</summary>
        ///
        /// <param name="p">        The page instance.</param>
        /// <param name="controlId">Target control id to bind jQuery UI datepicker to.</param>
        /// <param name="minDate">  The minimum date.</param>
        /// <param name="maxDate">  The maximum date.</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

            PageUtility.RegisterJavascript("datePicker_" + controlId + RandomNumber().ToString(CultureInfo.InvariantCulture), script, p, true);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets datepicker script.</summary>
        ///
        /// <param name="controlId">Target control id to bind jQuery UI datepicker to.</param>
        /// <param name="minDate">  The minimum date.</param>
        /// <param name="maxDate">  The maximum date.</param>
        ///
        /// <returns>The date picker script.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        private static int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(2000);
        }

        #region Helpers

        public static string GetDatePickerFormat()
        {
            string datePattern = CurrentCulture.GetCurrentUICulture().DateTimeFormat.ShortDatePattern;
            return ConvertDateFormat(datePattern);
        }

        public static string GetDatePickerLocale()
        {
            return CurrentCulture.GetCurrentUICulture().TwoLetterISOLanguageName;
        }

        public static string GetNumberOfMonths()
        {
            return ConfigurationHelper.GetParameter("DatePickerNumberOfMonths");
        }

        public static int GetWeekStartDay()
        {
            return (int)CurrentCulture.GetCurrentUICulture().DateTimeFormat.FirstDayOfWeek;
        }

        public static bool ShowWeekNumber()
        {
            return ConfigurationHelper.GetParameter("DatePickerShowWeekNumber").Equals("true");
        }

        /// <summary>
        ///     Credit to: http://www.rajeeshcv.com/post/details/31/jqueryui-datepicker-in-asp-net-mvc
        ///     Converts the .net supported date format current culture format into JQuery Datepicker format.
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

        #endregion Helpers
    }
}