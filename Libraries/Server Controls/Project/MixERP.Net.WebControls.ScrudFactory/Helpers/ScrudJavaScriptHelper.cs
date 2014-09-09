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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ScrudJavaScriptHelper
    {
        public static string GetScript(string keyColumn, string customFormUrl, string formGridViewId, string gridPanelId, string userIdHiddenId, string officeCodeHiddenId, string titleLabelId, string formPanelId, string cancelButtonId)
        {
            var resource = JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.ScrudFactory.Scrud.js", Assembly.GetExecutingAssembly());
            var script = new StringBuilder();

            script.Append(CreateVariable("formGridViewId", formGridViewId));
            script.Append(CreateVariable("gridPanelId", gridPanelId));
            script.Append(CreateVariable("userIdHiddenId", userIdHiddenId));
            script.Append(CreateVariable("officeCodeHiddenId", officeCodeHiddenId));
            script.Append(CreateVariable("titleLabelId", titleLabelId));
            script.Append(CreateVariable("formPanelId", formPanelId));
            script.Append(CreateVariable("cancelButtonId ", cancelButtonId));

            script.Append(CreateVariable("scrudAreYouSureLocalized", ScrudResource.AreYouSure));
            script.Append(CreateVariable("scrudNothingSelectedLocalized", ScrudResource.NothingSelected));
            script.Append(CreateVariable("reportTemplatePath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("TemplatePath"))));
            script.Append(CreateVariable("reportHeaderPath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("HeaderPath"))));
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