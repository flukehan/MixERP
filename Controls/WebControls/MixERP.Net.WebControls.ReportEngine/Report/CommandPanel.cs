/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Text;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;

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
                cssClass = ConfigurationHelper.GetReportParameter("ImageButtonCssClass");

            }

            return cssClass;
        }

        private void AddCommandPanel(Panel p)
        {
            this.commandPanel = new Panel();
            this.commandPanel.CssClass = "report-command hide";

            this.AddEmailImageButton(this.commandPanel);
            this.AddPrintImageButton(this.commandPanel);
            this.AddExcelImageButton(this.commandPanel);
            this.AddWordImageButton(this.commandPanel);
            this.AddGoTopImageButton(this.commandPanel);
            this.AddGoBottomImageButton(this.commandPanel);
            this.AddFilterImageButton(this.commandPanel);
            this.AddCloseImageButton(this.commandPanel);

            p.Controls.Add(this.commandPanel);
        }


        private void AddEmailImageButton(Panel p)
        {
            this.emailImageButton = new ImageButton();
            this.emailImageButton.ID = "SendEmailImageButton";
            this.emailImageButton.CssClass = this.GetImageButtonCssClass();
            this.emailImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("EmailIcon"));

            p.Controls.Add(this.emailImageButton);
        }

        private void AddPrintImageButton(Panel p)
        {
            this.printImageButton = new ImageButton();
            this.printImageButton.ID = "PrintImageButton";
            this.printImageButton.CssClass = this.GetImageButtonCssClass();
            this.printImageButton.OnClientClick = "javascript:window.print();";
            this.printImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("PrintIcon"));
            
            p.Controls.Add(this.printImageButton);
        }

        private void AddExcelImageButton(Panel p)
        {
            this.excelImageButton = new ImageButton();
            this.excelImageButton.ID = "ExcelImageButton";
            this.excelImageButton.CssClass = this.GetImageButtonCssClass();
            this.excelImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("ExcelIcon"));
            this.excelImageButton.Click += this.ExcelImageButton_Click;
            this.excelImageButton.OnClientClick = this.GetReportHtmlScript();

            p.Controls.Add(this.excelImageButton);
        }

        private void AddWordImageButton(Panel p)
        {
            this.wordImageButton = new ImageButton();
            this.wordImageButton.ID = "WordImageButton";
            this.wordImageButton.CssClass = this.GetImageButtonCssClass();
            this.wordImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("WordIcon"));
            this.wordImageButton.Click += this.WordImageButton_Click;
            this.wordImageButton.OnClientClick =this.GetReportHtmlScript();

            p.Controls.Add(this.wordImageButton);
        }

        private void AddGoTopImageButton(Panel p)
        {
            this.goTopImageButton = new ImageButton();
            this.goTopImageButton.ID = "GoTop";
            this.goTopImageButton.CssClass = this.GetImageButtonCssClass();
            this.goTopImageButton.OnClientClick = "window.scrollTo(0, 0);";
            this.goTopImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("GoTopIcon"));
            
            p.Controls.Add(this.goTopImageButton);
        }

        private void AddGoBottomImageButton(Panel p)
        {
            this.goBottomImageButton = new ImageButton();
            this.goBottomImageButton.CssClass = this.GetImageButtonCssClass();
            this.goBottomImageButton.ID = "GoBottom";
            this.goBottomImageButton.OnClientClick = "window.scrollTo(0,document.body.scrollHeight);";
            this.goBottomImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("GoBottomIcon"));

            p.Controls.Add(this.goBottomImageButton);
        }

        private void AddFilterImageButton(Panel p)
        {
            this.filterImageButton = new ImageButton();
            this.filterImageButton.ID = "FilterImageButton";
            this.filterImageButton.CssClass = this.GetImageButtonCssClass();
            this.filterImageButton.OnClientClick =  "$('.report-parameter').toggle(500);";
            this.filterImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("FilterIcon"));

            p.Controls.Add(this.filterImageButton);
        }

        private void AddCloseImageButton(Panel p)
        {
            this.closeImageButton = new ImageButton();
            this.closeImageButton.CssClass = this.GetImageButtonCssClass();
            this.closeImageButton.ID = "CloseImageButton";
            this.closeImageButton.OnClientClick = "window.close();";
            this.closeImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("CloseIcon"));

            p.Controls.Add(this.closeImageButton);
        }

        private string GetReportHtmlScript()
        {
            this.EnsureChildControls();
            StringBuilder s = new StringBuilder();
            s.Append("$('#" + this.reportHidden.ClientID + "')");
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
