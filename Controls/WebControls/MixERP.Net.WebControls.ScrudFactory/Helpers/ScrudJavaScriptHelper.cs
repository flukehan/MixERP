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
        public static string GetScript()
        {
            string resource = Helpers.JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.ScrudFactory.Scrud.js");
            StringBuilder script = new StringBuilder();

            script.Append(CreateVariable("localizedAreYouSure", Resources.ScrudResource.AreYouSure));
            script.Append(CreateVariable("localizedNothingSelected", Resources.ScrudResource.NothingSelected));

            script.Append(CreateVariable("reportTemplatePath", MixERP.Net.Common.PageUtility.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("TemplatePath"))));
            script.Append(CreateVariable("reportHeaderPath", MixERP.Net.Common.PageUtility.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("HeaderPath"))));
            script.Append(CreateVariable("containerMargin", MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("GridContainerMargin")));
            script.Append(CreateVariable("date", DateTime.Now.ToString()));

            script.Append(resource);
            return script.ToString();
        }

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
