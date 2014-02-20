/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Threading;
using System.Web;
using System.Resources;
using System.Collections;
using System.Globalization;
using System.ComponentModel.Design;

namespace MixERP.Net.Common.Helpers
{
    public static class LocalizationHelper
    {
        public static string GetResourceString(string className, string key)
        {
            if (string.IsNullOrWhiteSpace(key) || HttpContext.Current == null)
            {
                return string.Empty;
            }

            try
            {

                var globalResourceObject = HttpContext.GetGlobalResourceObject(className, key, GetCurrentCulture());

                if (globalResourceObject != null)
                {
                    return globalResourceObject.ToString();
                }

                return string.Empty;
            }
            catch
            {
                throw new InvalidOperationException("Resource could not be found for the key " + key + " on class " + className + " .");
            }
        }

        public static string GetResourceString(string className, string key, bool throwError)
        {
            if (string.IsNullOrWhiteSpace(key) || HttpContext.Current == null)
            {
                return string.Empty;
            }
            try
            {
                var globalResourceObject = HttpContext.GetGlobalResourceObject(className, key, GetCurrentCulture());
                if (globalResourceObject != null)
                {
                    return globalResourceObject.ToString();
                }

                return string.Empty;
            }
            catch
            {
                if (throwError)
                {
                    throw new InvalidOperationException("Resource could not be found for the key " + key + " on class " + className + " .");
                }
            }

            return key;
        }

        public static void AddResourceString(string path, string key, string value)
        {
            using (ResXResourceReader reader = new ResXResourceReader(path))
            {
                reader.UseResXDataNodes = true;

                Hashtable resources = new Hashtable();

                using (ResXResourceWriter writer = new ResXResourceWriter(path))
                {
                    ITypeResolutionService iResoulution = null;

                    foreach (DictionaryEntry entry in reader)
                    {

                        if (entry.Value == null)
                        {
                            resources.Add(entry.Key.ToString(), "");
                        }
                        else
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            resources.Add(entry.Key.ToString(), ((ResXDataNode)entry.Value).GetValue(iResoulution).ToString());
                        }

                        ResXDataNode dataNode = (ResXDataNode)entry.Value;

                        if (dataNode != null) writer.AddResource(dataNode);
                    }


                    if (!resources.ContainsKey(key))
                    {
                        writer.AddResource(key, value);
                    }

                    writer.Generate();
                }
            }
        }

        public static CultureInfo GetCurrentCulture()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            return culture;
        }

        public static string GetThousandSeparator()
        {
            CultureInfo culture = GetCurrentCulture();
            return culture.NumberFormat.CurrencyGroupSeparator;
        }

        public static string GetDecimalSeparator()
        {
            CultureInfo culture = GetCurrentCulture();
            return culture.NumberFormat.CurrencyDecimalSeparator;
        }

        public static int GetDecimalPlaces()
        {
            CultureInfo culture = GetCurrentCulture();
            return culture.NumberFormat.CurrencyDecimalDigits;
        }

        public static string GetShortDateFormat()
        {
            CultureInfo culture = GetCurrentCulture();
            return culture.DateTimeFormat.ShortDatePattern;
        }

    }
}
