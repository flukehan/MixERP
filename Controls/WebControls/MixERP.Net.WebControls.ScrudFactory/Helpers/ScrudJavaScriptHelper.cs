/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ScrudJavaScriptHelper
    {
        public static string GetScrudScript()
        {
            string resource = GetResource();
            StringBuilder script = new StringBuilder();

            script.Append(CreateVariable("localizedAreYouSure", Resources.ScrudResource.AreYouSure));
            script.Append(CreateVariable("localizedNothingSelected", Resources.ScrudResource.NothingSelected));

            script.Append(CreateVariable("reportTemplatePath", MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("TemplatePath")));
            script.Append(CreateVariable("reportHeaderPath", MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("HeaderPath")));
            script.Append(CreateVariable("containerMargin", MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("GridContainerMargin")));
            script.Append(CreateVariable("date", DateTime.Now.ToString()));

            script.Append(resource);
            return script.ToString();
        }


        private static string GetResource()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MixERP.Net.WebControls.ScrudFactory.Scrud.js";

            Stream stream = assembly.GetManifestResourceStream(resourceName);

            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream = null;
                }
            }

        }


        //private static string CreateVariable(string variableName)
        //{
        //    if (string.IsNullOrWhiteSpace(variableName))
        //    {
        //        return string.Empty;
        //    }

        //    StringBuilder variable = new StringBuilder();
        //    variable.Append("var ");
        //    variable.Append(variableName);
        //    variable.Append(";");
        //    variable.Append(Environment.NewLine);

        //    return variable.ToString();
        //}

        private static string CreateVariable(string variableName, string initialValue)
        {
            StringBuilder variable = new StringBuilder();
            variable.Append("var ");
            variable.Append(variableName);
            variable.Append("='");
            variable.Append(HttpUtility.JavaScriptStringEncode(initialValue));
            variable.Append("';");
            variable.Append(Environment.NewLine);

            return variable.ToString();
        }

    }
}
