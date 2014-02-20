/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.IO;
using System.Web.UI;
using MixERP.Net.WebControls.ReportEngine.Helpers;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    [ToolboxData("<{0}:ReportHeader runat=server></{0}:ReportHeader>")]
    public partial class ReportHeader : CompositeControl
    {
        private string html;

        public string GetHtml()
        {
            return this.html;
        }

        private string GetPath()
        {
            if (!string.IsNullOrWhiteSpace(this.path))
            {
                return this.path;
            }

            return Common.Helpers.ConfigurationHelper.GetReportParameter("HeaderPath");
        }

        private bool IsValid()
        {
            string headerPath = this.GetPath();

            if (string.IsNullOrWhiteSpace(headerPath))
            {
                return false;
            }

            if (!File.Exists(this.Page.Server.MapPath(headerPath)))
            {
                return false;
            }

            return true;
        }

        private void PrepareReportHeader()
        {

            string header = File.ReadAllText(this.Page.Server.MapPath(this.GetPath()));
            this.html = ReportParser.ParseExpression(header);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            if (!this.IsValid())
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
