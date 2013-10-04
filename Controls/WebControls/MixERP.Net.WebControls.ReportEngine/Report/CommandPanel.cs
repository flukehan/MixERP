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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        /// <summary>
        /// CommandPanel is displayed at the top of the report.
        /// </summary>
        Panel commandPanel;
        HtmlAnchor emailAnchor;
        HtmlAnchor printAnchor;
        HtmlAnchor excelAnchor;
        HtmlAnchor wordAnchor;
        HtmlAnchor goTopAnchor;
        HtmlAnchor goBottomAnchor;
        HtmlAnchor filterAnchor;
        HtmlAnchor closeAnchor;

        private void AddCommandPanel(Panel p)
        {
            commandPanel = new Panel();
            commandPanel.CssClass = "report-command hide";

            this.AddEmailAnchor(commandPanel);
            this.AddPrintAnchor(commandPanel);
            this.AddExcelAnchor(commandPanel);
            this.AddWordAnchor(commandPanel);
            this.AddGoTopAnchor(commandPanel);
            this.AddGoBottomAnchor(commandPanel);
            this.AddFilterAnchor(commandPanel);
            this.AddCloseAnchor(commandPanel);

            p.Controls.Add(commandPanel);
        }


        private void AddEmailAnchor(Panel p)
        {
            emailAnchor = new HtmlAnchor();
            emailAnchor.ID = "SendEmailAnchor";
            emailAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "EmailIcon") + "' />";
            p.Controls.Add(emailAnchor);
        }

        private void AddPrintAnchor(Panel p)
        {
            printAnchor = new HtmlAnchor();
            printAnchor.ID = "PrintAnchor";
            printAnchor.HRef = "javascript:window.print();";
            printAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "PrintIcon") + "' />";

            p.Controls.Add(printAnchor);
        }

        private void AddExcelAnchor(Panel p)
        {
            excelAnchor = new HtmlAnchor();
            excelAnchor.ID = "ExcelAnchor";
            excelAnchor.HRef = "#";
            excelAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "ExcelIcon") + "' />";
            excelAnchor.ServerClick += ExcelAnchor_ServerClick;

            excelAnchor.Attributes.Add("onclick", this.GetReportHtmlScript());
            p.Controls.Add(excelAnchor);
        }

        private void AddWordAnchor(Panel p)
        {
            wordAnchor = new HtmlAnchor();
            wordAnchor.ID = "WordAnchor";
            wordAnchor.HRef = "#";
            wordAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "WordIcon") + "' />";
            wordAnchor.ServerClick += WordAnchor_ServerClick;

            wordAnchor.Attributes.Add("onclick", this.GetReportHtmlScript());
            p.Controls.Add(wordAnchor);
        }

        private void AddGoTopAnchor(Panel p)
        {
            goTopAnchor = new HtmlAnchor();
            goTopAnchor.ID = "GoTop";
            goTopAnchor.HRef = "javascript:window.scrollTo(0, 0);";
            goTopAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "GoTopIcon") + "' />";
            p.Controls.Add(goTopAnchor);
        }

        private void AddGoBottomAnchor(Panel p)
        {
            goBottomAnchor = new HtmlAnchor();
            goBottomAnchor.ID = "GoBottom";
            goBottomAnchor.HRef = "javascript:window.scrollTo(0,document.body.scrollHeight);";
            goBottomAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "GoBottomIcon") + "' />";
            p.Controls.Add(goBottomAnchor);
        }

        private void AddFilterAnchor(Panel p)
        {
            filterAnchor = new HtmlAnchor();
            filterAnchor.ID = "FilterAnchor";
            filterAnchor.HRef = "#";
            filterAnchor.Attributes.Add("onclick", "$('.report-parameter').toggle(500);");
            filterAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "FilterIcon") + "' />";
            p.Controls.Add(filterAnchor);
        }

        private void AddCloseAnchor(Panel p)
        {
            closeAnchor = new HtmlAnchor();
            closeAnchor.ID = "CloseAnchor";
            closeAnchor.HRef = "#";
            closeAnchor.Attributes.Add("onclick", "window.close();");
            closeAnchor.InnerHtml = "<img src='" + MixERP.Net.Common.Helpers.ConfigurationHelper.GetSectionKey("MixERPReportParameters", "CloseIcon") + "' />";
            p.Controls.Add(closeAnchor);
        }

        private string GetReportHtmlScript()
        {
            EnsureChildControls();
            StringBuilder s = new StringBuilder();
            s.Append("$('#" + reportHidden.ClientID + "')");
            s.Append(".val(");
            s.Append("'<html>'");
            s.Append("+");
            s.Append("$('#report').html()");
            s.Append("+");
            s.Append("'</html>'");
            s.Append(");");

            return s.ToString();
        }

    }
}
