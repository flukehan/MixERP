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

using MixERP.Net.WebControls.ReportEngine.Helpers;
using System.IO;
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
            return this.html;
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

        private string GetPath()
        {
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                return this.Path;
            }

            return Net.Common.Helpers.ConfigurationHelper.GetReportParameter("HeaderPath");
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
            this.html = ReportParser.ParseExpression(header, null);
        }
    }
}