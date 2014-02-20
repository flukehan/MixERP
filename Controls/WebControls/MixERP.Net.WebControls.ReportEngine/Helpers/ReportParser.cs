/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
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
