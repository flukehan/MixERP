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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private GridView formGridView;
        private Panel gridPanel;
        private TextBox lastValueHiddenTextBox;
        private AspNetPager pager;

        private void AddGridView(Panel p)
        {
            this.formGridView = new GridView();
            this.formGridView.ID = "FormGridView";
            this.formGridView.GridLines = GridLines.None;
            this.formGridView.CssClass = this.GetGridViewCssClass();
            this.formGridView.RowStyle.CssClass = this.GetGridViewRowCssClass();
            this.formGridView.AlternatingRowStyle.CssClass = this.GetGridViewAlternateRowCssClass();
            this.formGridView.AutoGenerateColumns = true;
            this.formGridView.RowDataBound += this.FormGridView_RowDataBound;

            var selectField = new TemplateField();
            selectField.HeaderText = Titles.Select;
            selectField.ItemStyle.Width = 20;
            this.formGridView.Columns.Add(selectField);

            p.Controls.Add(this.formGridView);
        }

        private void AddLastValueHiddenField(Panel p)
        {
            this.lastValueHiddenTextBox = new TextBox();
            this.lastValueHiddenTextBox.ID = "LastValueHidden";
            this.lastValueHiddenTextBox.Style.Add("display", "none;");
            p.Controls.Add(lastValueHiddenTextBox);
        }

        private void AddPager(Panel p)
        {
            this.pager = new AspNetPager();
            this.pager.ID = "Pager";
            this.pager.CssClass = this.GetPagerCssClass();
            this.pager.UrlPaging = true;

            this.pager.CurrentPageButtonClass = this.GetPagerCurrentPageCssClass();

            this.pager.PagingButtonsClass = this.GetPagerPageButtonCssClass();

            //Avoiding conflict with Semantic UI Pagination Menu
            //because pager control automatically generates an attribute "margin-right:5px";
            this.pager.PagingButtonsStyle = "margin-right:0;";
            this.pager.CurrentPageButtonStyle = "margin-right:0;";

            this.pager.PagingButtonType = PagingButtonType.Text;
            this.pager.NumericButtonType = PagingButtonType.Text;
            this.pager.NavigationButtonType = PagingButtonType.Text;

            this.pager.ShowNavigationToolTip = true;
            this.pager.ShowPageIndexBox = ShowPageIndexBox.Never;
            this.pager.ShowPageIndex = true;
            this.pager.AlwaysShowFirstLastPageNumber = true;
            this.pager.AlwaysShow = false;
            this.pager.UrlPageIndexName = "page";

            p.Controls.Add(this.pager);
        }

        private void CreateGridPanel()
        {
            this.gridPanel = new Panel();
            this.gridPanel.ID = "GridPanel";
            this.gridPanel.CssClass = this.GetGridPanelCssClass();
            this.gridPanel.Width = this.GetGridPanelWidth();

            if (!string.IsNullOrWhiteSpace(this.GetGridPanelStyle()))
            {
                this.gridPanel.Attributes.Add("style", this.GetGridPanelStyle());
            }

            this.AddGridView(this.gridPanel);
            this.AddPager(this.gridPanel);
            this.AddLastValueHiddenField(this.gridPanel);
        }

        private Unit GetGridPanelWidth()
        {
            if (this.GridPanelWidth.Value.Equals(0))
            {
                var width = Conversion.TryCastUnit(ConfigurationHelper.GetScrudParameter("GridPanelDefaultWidth"));

                return width;
            }

            return this.GridPanelWidth;
        }

        private string GetGridPanelStyle()
        {
            if (this.GridPanelWidth.Value.Equals(0))
            {
                var style = Conversion.TryCastString(ConfigurationHelper.GetScrudParameter("GridPanelStyle"));

                return style;
            }

            return this.GridPanelStyle;
        }
    }
}