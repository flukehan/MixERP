/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Globalization;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using Wuqi.Webdiyer;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        Panel gridPanel;
        GridView formGridView;
        AspNetPager pager;
        TextBox lastValueHiddenTextBox;

        private void CreateGridPanel()
        {
            this.gridPanel = new Panel();
            this.gridPanel.ID = "GridPanel";
            this.gridPanel.ScrollBars = ScrollBars.Auto;
            this.gridPanel.Width = Unit.Parse(ConfigurationHelper.GetScrudParameter("GridPanelDefaultWidth"), CultureInfo.InvariantCulture);

            this.AddGridView(this.gridPanel);
            this.AddPager(this.gridPanel);
            this.AddLastValueHiddenField(this.gridPanel);
        }

        private void AddLastValueHiddenField(Panel p)
        {
            this.lastValueHiddenTextBox = new TextBox();
            this.lastValueHiddenTextBox.ID = "LastValueHidden";
            this.lastValueHiddenTextBox.Style.Add("display", "none;");
            p.Controls.Add(lastValueHiddenTextBox);
        }

        private void AddGridView(Panel p)
        {
            this.formGridView = new GridView();
            this.formGridView.ID = "FormGridView";
            this.formGridView.GridLines = GridLines.None;
            this.formGridView.CssClass = this.GetGridViewCssClass();
            this.formGridView.RowStyle.CssClass = "row";
            this.formGridView.AlternatingRowStyle.CssClass = "alt";
            this.formGridView.AutoGenerateColumns = true;
            this.formGridView.RowDataBound += this.FormGridView_RowDataBound;

            var selectField = new TemplateField();
            selectField.HeaderText = ScrudResource.Select;
            selectField.ItemStyle.Width = 20;
            this.formGridView.Columns.Add(selectField);

            p.Controls.Add(this.formGridView);
        }

        private void AddPager(Panel p)
        {
            this.pager = new AspNetPager();
            this.pager.ID = "Pager";
            this.pager.CssClass = "pager";
            this.pager.UrlPaging = true;
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
    }
}