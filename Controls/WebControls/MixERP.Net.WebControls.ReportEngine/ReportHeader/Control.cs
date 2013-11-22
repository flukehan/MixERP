/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    [ToolboxData("<{0}:ReportHeader runat=server></{0}:ReportHeader>")]
    public partial class ReportHeader : CompositeControl
    {
        private string html;

        public string GetHtml()
        {
            return html;
        }

        private bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(this.path))
            {
                return false;
            }

            if(!System.IO.File.Exists(this.Page.Server.MapPath(this.path)))
            {
                return false;
            }

            return true;
        }

        private void PrepareReportHeader()
        {

            string header = System.IO.File.ReadAllText(this.Page.Server.MapPath(this.Path));
            html = MixERP.Net.WebControls.ReportEngine.Helpers.ReportParser.ParseExpression(header);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            if(!this.IsValid())
            {
                return;
            }

            if (w == null)
            {
                return;
            }

            this.PrepareReportHeader();

            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.AddAttribute(HtmlTextWriterAttribute.Id, "reportheader");
            w.Write(this.html);
            w.RenderEndTag();
            base.Render(w);
        }
    }
}
