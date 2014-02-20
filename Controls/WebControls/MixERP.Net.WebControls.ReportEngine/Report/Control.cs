/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    [ToolboxData("<{0}:Report runat=server></{0}:Report>")]
    public partial class Report : CompositeControl
    {
        Panel reportContainer;

        protected override void CreateChildControls()
        {
            this.reportContainer = new Panel();
            this.AddHiddenControls(this.reportContainer);
            this.AddCommandPanel(this.reportContainer);
            this.AddReportBody(this.reportContainer);
            this.Controls.Add(this.reportContainer);

            if(this.AutoInitialize)
            {
                this.InitializeReport();
            }
        }

   
        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.reportContainer.RenderControl(w);
        }
    }
}
