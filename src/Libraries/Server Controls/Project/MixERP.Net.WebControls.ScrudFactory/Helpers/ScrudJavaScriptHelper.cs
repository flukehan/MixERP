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
    internal static class ScrudJavascriptHelper
    {
        internal static string GetScript(string keyColumn, string customFormUrl, string formGridViewId, string gridPanelId, string userIdHiddenId, string officeCodeHiddenId, string titleLabelId, string formPanelId, string cancelButtonId)
        {
            string resource = JSUtility.GetEmbeddedScript("MixERP.Net.WebControls.ScrudFactory.Scrud.js", Assembly.GetExecutingAssembly());
            string script = string.Empty;

            script += JSUtility.GetVar("gridPanelId", gridPanelId);
            script += JSUtility.GetVar("userIdHiddenId", userIdHiddenId);
            script += JSUtility.GetVar("formGridViewId", formGridViewId);
            script += JSUtility.GetVar("officeCodeHiddenId", officeCodeHiddenId);
            script += JSUtility.GetVar("titleLabelId", titleLabelId);
            script += JSUtility.GetVar("formPanelId", formPanelId);
            script += JSUtility.GetVar("cancelButtonId ", cancelButtonId);

            script += JSUtility.GetVar("scrudAreYouSureLocalized", Titles.AreYouSure);
            script += JSUtility.GetVar("scrudNothingSelectedLocalized", Titles.NothingSelected);
            script += JSUtility.GetVar("reportTemplatePath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("TemplatePath")));
            script += JSUtility.GetVar("reportHeaderPath", PageUtility.ResolveUrl(ConfigurationHelper.GetScrudParameter("HeaderPath")));
            script += JSUtility.GetVar("date", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            script += JSUtility.GetVar("keyColumn", keyColumn);
            script += JSUtility.GetVar("customFormUrl", customFormUrl);

            script += resource;
            return script;
        }
    }
}