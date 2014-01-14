/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.Common.Helpers
{
    public static class JQueryUIHelper
    {
        public static string GetDatePickerFormat()
        {
            string datePattern = MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture().DateTimeFormat.ShortDatePattern;
            return ConvertDateFormat(datePattern);
        }

        /// <summary>
        /// Credit to: http://www.rajeeshcv.com/post/details/31/jqueryui-datepicker-in-asp-net-mvc
        /// Converts the .net supported date format current culture format into JQuery Datepicker format.
        /// </summary>
        /// <param name="html">HtmlHelper object.</param>
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

        public static string GetDatePickerLocale()
        {
            return MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture().TwoLetterISOLanguageName;
        }

        public static int GetWeekStartDay()
        {
            return (int)MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture().DateTimeFormat.FirstDayOfWeek;
        }

        public static bool ShowWeekNumber()
        {
            return ConfigurationHelper.GetParameter("DatePickerShowWeekNumber").Equals("true");
        }

        public static string GetNumberOfMonths()
        {
            return ConfigurationHelper.GetParameter("DatePickerNumberOfMonths");
        }
    }
}
