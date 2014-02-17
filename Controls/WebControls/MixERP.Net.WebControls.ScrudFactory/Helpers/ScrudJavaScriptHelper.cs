/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Globalization;
using System.Text;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ScrudJavaScriptHelper
    {
        public static string GetScript(string keyColumn, string customFormUrl)
        {
            var resource = JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.ScrudFactory.Scrud.js");
            var script = new StringBuilder();

            script.Append(CreateVariable("localizedAreYouSure", ScrudResource.AreYouSure));
            script.Append(CreateVariable("localizedNothingSelected", ScrudResource.NothingSelected));
            script.Append(CreateVariable("reportTemplatePath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("TemplatePath"))));
            script.Append(CreateVariable("reportHeaderPath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("HeaderPath"))));
            script.Append(CreateVariable("containerMargin", ConfigurationHelper.GetScrudParameter("GridContainerMargin")));
            script.Append(CreateVariable("date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            script.Append(CreateVariable("keyColumn", keyColumn));
            script.Append(CreateVariable("customFormUrl", customFormUrl));

            script.Append(resource);
            return script.ToString();
        }

        private static string CreateVariable(string variableName, string initialValue)
        {
            var variable = new StringBuilder();
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
