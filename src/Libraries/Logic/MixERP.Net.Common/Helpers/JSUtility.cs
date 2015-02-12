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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.UI;

namespace MixERP.Net.Common.Helpers
{
    public static class JSUtility
    {
        public static void AddJSReference(Page page, string resourceName, string key, Type type)
        {
            if (page != null)
            {
                string script = "<script type='text/javascript' src='" + page.Request.Url.GetLeftPart(UriPartial.Authority) + page.ClientScript.GetWebResourceUrl(type, resourceName) + "'></script>";
                PageUtility.RegisterJavascript(key, script, page, false);
            }
        }

        public static string GetEmbeddedScript(string embeddedScriptName, Assembly assembly)
        {
            if (assembly == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(embeddedScriptName))
            {
                return string.Empty;
            }

            Stream stream = assembly.GetManifestResourceStream(embeddedScriptName);

            if (stream == null)
            {
                return string.Empty;
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetVar(string name, object value, bool isString = true)
        {
            string script = "var {0}={1};";

            if (isString)
            {
                script = "var {0}='{1}';";
            }

            if (value == null)
            {
                script = string.Format(CultureInfo.InvariantCulture, script, name, string.Empty);
            }
            else
            {
                script = string.Format(CultureInfo.InvariantCulture, script, name, value.ToString().Replace("'", @"\'"));
            }

            return script;
        }
    }
}