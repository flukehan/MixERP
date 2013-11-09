/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
            if (string.IsNullOrWhiteSpace(key) || System.Web.HttpContext.Current == null)
            {
                return string.Empty;
            }
            try
            {
                return System.Web.HttpContext.GetGlobalResourceObject(className, key, GetCurrentCulture()).ToString();
            }
            catch
            {
                throw new InvalidOperationException("Resource could not be found for the key " + key + " on class " + className + " .");
            }
        }

        public static string GetResourceString(string className, string key, bool throwError)
        {
            if (string.IsNullOrWhiteSpace(key) || System.Web.HttpContext.Current == null)
            {
                return string.Empty;
            }
            try
            {
                return System.Web.HttpContext.GetGlobalResourceObject(className, key, GetCurrentCulture()).ToString();
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

                    if (reader != null)
                    {
                        foreach (DictionaryEntry entry in reader)
                        {

                            if (entry.Value == null)
                            {
                                resources.Add(entry.Key.ToString(), "");
                            }
                            else
                            {
                                resources.Add(entry.Key.ToString(), ((ResXDataNode)entry.Value).GetValue(iResoulution).ToString());
                            }

                            ResXDataNode dataNode = (ResXDataNode)entry.Value;

                            writer.AddResource(dataNode);
                        }


                        if (!resources.ContainsKey(key))
                        {
                            writer.AddResource(key, value);
                        }
                        
                        writer.Generate();
                    }
                }
            }
        }

        public static CultureInfo GetCurrentCulture()
        {
            //Todo
            CultureInfo culture = new CultureInfo(CultureInfo.InvariantCulture.Name);
            return culture;
        }

    }
}
