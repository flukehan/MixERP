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

using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
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
            this.commandPanel.ID = "ReportParameterPanel";
            this.commandPanel.CssClass = "report-command hide";

            this.AddEmailImageButton(this.commandPanel);
            this.AddPrintImageButton(this.commandPanel);
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
            this.emailImageButton.ToolTip = Titles.Email;

            p.Controls.Add(this.emailImageButton);
        }

        private void AddPrintImageButton(Panel p)
        {
            this.printImageButton = new ImageButton();
            this.printImageButton.ID = "PrintImageButton";
            this.printImageButton.CssClass = this.GetImageButtonCssClass();
            this.printImageButton.OnClientClick = "javascript:window.print();return false;";
            this.printImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("PrintIcon"));
            this.printImageButton.ToolTip = Titles.Print;

            p.Controls.Add(this.printImageButton);
        }

        private void AddGoTopImageButton(Panel p)
        {
            this.goTopImageButton = new ImageButton();
            this.goTopImageButton.ID = "GoTop";
            this.goTopImageButton.CssClass = this.GetImageButtonCssClass();
            this.goTopImageButton.OnClientClick = "window.scrollTo(0, 0);";
            this.goTopImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("GoTopIcon"));
            this.goTopImageButton.ToolTip = Titles.GoToTop;

            p.Controls.Add(this.goTopImageButton);
        }

        private void AddGoBottomImageButton(Panel p)
        {
            this.goBottomImageButton = new ImageButton();
            this.goBottomImageButton.CssClass = this.GetImageButtonCssClass();
            this.goBottomImageButton.ID = "GoBottom";
            this.goBottomImageButton.OnClientClick = "window.scrollTo(0,document.body.scrollHeight);";
            this.goBottomImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("GoBottomIcon"));
            this.goBottomImageButton.ToolTip = Titles.GoToBottom;

            p.Controls.Add(this.goBottomImageButton);
        }

        private void AddFilterImageButton(Panel p)
        {
            this.filterImageButton = new ImageButton();
            this.filterImageButton.ID = "FilterImageButton";
            this.filterImageButton.CssClass = this.GetImageButtonCssClass();
            this.filterImageButton.OnClientClick = "$('.report-parameter').toggle(500);return false;";
            this.filterImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("FilterIcon"));
            this.filterImageButton.ToolTip = Titles.Filter;

            p.Controls.Add(this.filterImageButton);
        }

        private void AddCloseImageButton(Panel p)
        {
            this.closeImageButton = new ImageButton();
            this.closeImageButton.CssClass = this.GetImageButtonCssClass();
            this.closeImageButton.ID = "CloseImageButton";
            this.closeImageButton.OnClientClick = "closeWindow();";
            this.closeImageButton.ImageUrl = this.Page.ResolveUrl(ConfigurationHelper.GetReportParameter("CloseIcon"));
            this.closeImageButton.ToolTip = Titles.Close;

            p.Controls.Add(this.closeImageButton);
        }
    }
}