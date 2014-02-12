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

        private string GetImageButtonCssClass()
        { 
            string cssClass = this.ImageButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("ImageButtonCssClass");

            }

            return cssClass;
        }

        private void AddCommandPanel(Panel p)
        {
            commandPanel = new Panel();
            commandPanel.CssClass = "report-command hide";

            this.AddEmailImageButton(commandPanel);
            this.AddPrintImageButton(commandPanel);
            this.AddExcelImageButton(commandPanel);
            this.AddWordImageButton(commandPanel);
            this.AddGoTopImageButton(commandPanel);
            this.AddGoBottomImageButton(commandPanel);
            this.AddFilterImageButton(commandPanel);
            this.AddCloseImageButton(commandPanel);

            p.Controls.Add(commandPanel);
        }


        private void AddEmailImageButton(Panel p)
        {
            emailImageButton = new ImageButton();
            emailImageButton.ID = "SendEmailImageButton";
            emailImageButton.CssClass = this.GetImageButtonCssClass();
            emailImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("EmailIcon"));

            p.Controls.Add(emailImageButton);
        }

        private void AddPrintImageButton(Panel p)
        {
            printImageButton = new ImageButton();
            printImageButton.ID = "PrintImageButton";
            printImageButton.CssClass = this.GetImageButtonCssClass();
            printImageButton.OnClientClick = "javascript:window.print();";
            printImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("PrintIcon"));
            
            p.Controls.Add(printImageButton);
        }

        private void AddExcelImageButton(Panel p)
        {
            excelImageButton = new ImageButton();
            excelImageButton.ID = "ExcelImageButton";
            excelImageButton.CssClass = this.GetImageButtonCssClass();
            excelImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("ExcelIcon"));
            excelImageButton.Click += ExcelImageButton_Click;
            excelImageButton.OnClientClick = this.GetReportHtmlScript();

            p.Controls.Add(excelImageButton);
        }

        private void AddWordImageButton(Panel p)
        {
            wordImageButton = new ImageButton();
            wordImageButton.ID = "WordImageButton";
            wordImageButton.CssClass = this.GetImageButtonCssClass();
            wordImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("WordIcon"));
            wordImageButton.Click += WordImageButton_Click;
            wordImageButton.OnClientClick =this.GetReportHtmlScript();

            p.Controls.Add(wordImageButton);
        }

        private void AddGoTopImageButton(Panel p)
        {
            goTopImageButton = new ImageButton();
            goTopImageButton.ID = "GoTop";
            goTopImageButton.CssClass = this.GetImageButtonCssClass();
            goTopImageButton.OnClientClick = "window.scrollTo(0, 0);";
            goTopImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("GoTopIcon"));
            
            p.Controls.Add(goTopImageButton);
        }

        private void AddGoBottomImageButton(Panel p)
        {
            goBottomImageButton = new ImageButton();
            goBottomImageButton.CssClass = this.GetImageButtonCssClass();
            goBottomImageButton.ID = "GoBottom";
            goBottomImageButton.OnClientClick = "window.scrollTo(0,document.body.scrollHeight);";
            goBottomImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("GoBottomIcon"));

            p.Controls.Add(goBottomImageButton);
        }

        private void AddFilterImageButton(Panel p)
        {
            filterImageButton = new ImageButton();
            filterImageButton.ID = "FilterImageButton";
            filterImageButton.CssClass = this.GetImageButtonCssClass();
            filterImageButton.OnClientClick =  "$('.report-parameter').toggle(500);";
            filterImageButton.ImageUrl = this.Page.ResolveUrl(MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("FilterIcon"));

            p.Controls.Add(filterImageButton);
        }

        private void AddCloseImageButton(Panel p)
        {
            closeImageButton = new ImageButton();
            closeImageButton.CssClass = this.GetImageButtonCssClass();
            closeImageButton.ID = "CloseImageButton";
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
