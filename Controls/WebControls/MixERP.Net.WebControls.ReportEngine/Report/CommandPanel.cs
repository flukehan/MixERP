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
        ImageButton emailImageButton;
        ImageButton printImageButton;
        ImageButton excelImageButton;
        ImageButton wordImageButton;
        ImageButton goTopImageButton;
        ImageButton goBottomImageButton;
        ImageButton filterImageButton;
        ImageButton closeImageButton;

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
            emailImageButton = new ImageButton();
            emailImageButton.ID = "SendEmailAnchor";
            emailImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("EmailIcon"));
            
            p.Controls.Add(emailImageButton);
        }

        private void AddPrintAnchor(Panel p)
        {
            printImageButton = new ImageButton();
            printImageButton.ID = "PrintAnchor";
            printImageButton.OnClientClick = "javascript:window.print();";
            printImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("PrintIcon"));

            p.Controls.Add(printImageButton);
        }

        private void AddExcelAnchor(Panel p)
        {
            excelImageButton = new ImageButton();
            excelImageButton.ID = "ExcelAnchor";
            excelImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("ExcelIcon"));
            excelImageButton.Click += ExcelImageButton_Click;
            excelImageButton.OnClientClick = this.GetReportHtmlScript();
            
            p.Controls.Add(excelImageButton);
        }

        private void AddWordAnchor(Panel p)
        {
            wordImageButton = new ImageButton();
            wordImageButton.ID = "WordAnchor";
            wordImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("WordIcon"));
            wordImageButton.Click += WordImageButton_Click;
            wordImageButton.OnClientClick =this.GetReportHtmlScript();

            p.Controls.Add(wordImageButton);
        }

        private void AddGoTopAnchor(Panel p)
        {
            goTopImageButton = new ImageButton();
            goTopImageButton.ID = "GoTop";
            goTopImageButton.OnClientClick = "window.scrollTo(0, 0);";
            goTopImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("GoTopIcon"));
            
            p.Controls.Add(goTopImageButton);
        }

        private void AddGoBottomAnchor(Panel p)
        {
            goBottomImageButton = new ImageButton();
            goBottomImageButton.ID = "GoBottom";
            goBottomImageButton.OnClientClick = "window.scrollTo(0,document.body.scrollHeight);";
            goBottomImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("GoBottomIcon"));

            p.Controls.Add(goBottomImageButton);
        }

        private void AddFilterAnchor(Panel p)
        {
            filterImageButton = new ImageButton();
            filterImageButton.ID = "FilterAnchor";
            filterImageButton.OnClientClick =  "$('.report-parameter').toggle(500);";
            filterImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("FilterIcon"));

            p.Controls.Add(filterImageButton);
        }

        private void AddCloseAnchor(Panel p)
        {
            closeImageButton = new ImageButton();
            closeImageButton.ID = "CloseAnchor";
            closeImageButton.OnClientClick = "window.close();";
            closeImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("CloseIcon"));

            p.Controls.Add(closeImageButton);
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
