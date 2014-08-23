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
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.ReportEngine.Helpers
{
    public static class ReportParser
    {
        public static string ParseExpression(string expression)
        {
            if(string.IsNullOrWhiteSpace(expression))
            {
                return string.Empty;
            }

            string logo = ConfigurationHelper.GetReportParameter("LogoPath");
            expression = expression.Replace("{LogoPath}", PageUtility.GetCurrentDomainName() + PageUtility.ResolveUrl(logo));//Or else logo will not be exported into excel.
            expression = expression.Replace("{PrintDate}", DateTime.Now.ToString(LocalizationHelper.GetCurrentCulture()));

            foreach(var match in Regex.Matches(expression, "{.*?}"))
            {
                string word = match.ToString();


                if(word.StartsWith("{Session.", StringComparison.OrdinalIgnoreCase))
                {
                    string sessionKey = RemoveBraces(word);
                    sessionKey = sessionKey.Replace("Session.", "");
                    sessionKey = sessionKey.Trim();
                    expression = expression.Replace(word, GetSessionValue(sessionKey));
                }
                else if(word.StartsWith("{Resources.", StringComparison.OrdinalIgnoreCase))
                {
                    string res = RemoveBraces(word);
                    string[] resource = res.Split('.');

                    expression = expression.Replace(word, LocalizationHelper.GetResourceString(resource[1], resource[2]));
                }
            }

            return expression;
        }

        public static string ParseDataSource(string expression, Collection<DataTable> table)
        {
            if(string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            if(table == null)
            {
                return null;
            }


            foreach(var match in Regex.Matches(expression, "{.*?}"))
            {
                string word = match.ToString();

                if(word.StartsWith("{DataSource", StringComparison.OrdinalIgnoreCase))
                {

                    int index = Conversion.TryCastInteger(word.Split('.').First().Replace("{DataSource[", "").Replace("]", ""));
                    string column = word.Split('.').Last().Replace("}", "");

                    if(table[index] != null)
                    {
                        if(table[index].Rows.Count > 0)
                        {
                            if(table[index].Columns.Contains(column))
                            {
                                string value = table[index].Rows[0][column].ToString();
                                expression = expression.Replace(word, value);
                            }
                        }
                    }
                }
            }

            return expression;
        }

        public static string RemoveBraces(string expression)
        {
            if(string.IsNullOrWhiteSpace(expression))
            {
                return string.Empty;
            }

            return expression.Replace("{", "").Replace("}", "");
        }

        public static string GetSessionValue(string key)
        {
            var val = HttpContext.Current.Session[key];

            if(val != null)
            {
                return val.ToString();
            }

            return string.Empty;
        }
    }
}
