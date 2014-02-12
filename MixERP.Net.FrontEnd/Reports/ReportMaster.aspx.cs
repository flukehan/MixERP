/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Reports
{
    public partial class ReportMaster : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            using (System.Web.UI.HtmlControls.HtmlGenericControl iFrame = new System.Web.UI.HtmlControls.HtmlGenericControl())
            {
                iFrame.TagName = "iframe";
                iFrame.Attributes.Add("src", ResolveUrl("~/Reports/ReportViewer.aspx?Id=" + this.RouteData.Values["path"]));
                iFrame.Attributes.Add("style", "width:100%;height:100%;border:1px solid #C0C0C0;");
                IFramePlaceholder.Controls.Add(iFrame);
            }
        }
    }
}