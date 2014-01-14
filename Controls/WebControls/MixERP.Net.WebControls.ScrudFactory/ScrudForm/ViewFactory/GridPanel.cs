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
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        Panel gridPanel;
        GridView formGridView;
        AspNetPager pager;

        private void CreateGridPanel()
        {
            gridPanel = new Panel();
            gridPanel.ID = "GridPanel";
            gridPanel.ScrollBars = ScrollBars.Auto;
            gridPanel.Width = Unit.Parse(MixERP.Net.Common.Helpers.ConfigurationHelper.GetScrudParameter("GridPanelDefaultWidth"));

            this.AddGridView(gridPanel);
            this.AddPager(gridPanel);
        }

        private void AddGridView(Panel p)
        {
            formGridView = new GridView();
            formGridView.ID = "FormGridView";
            formGridView.GridLines = GridLines.None;
            formGridView.CssClass = this.GetGridViewCssClass();
            formGridView.RowStyle.CssClass = "row";
            formGridView.AlternatingRowStyle.CssClass = "alt";
            formGridView.AutoGenerateColumns = true;
            formGridView.RowDataBound += this.FormGridView_RowDataBound;

            TemplateField selectField = new TemplateField();
            selectField.HeaderText = Resources.ScrudResource.Select;
            selectField.ItemStyle.Width = 20;
            formGridView.Columns.Add(selectField);

            p.Controls.Add(formGridView);
        }

        private void AddPager(Panel p)
        {
            pager = new AspNetPager();
            pager.ID = "Pager";
            pager.CssClass = "pager";
            pager.UrlPaging = true;
            pager.PagingButtonType = PagingButtonType.Text;
            pager.NumericButtonType = PagingButtonType.Text;
            pager.NavigationButtonType = PagingButtonType.Text;
            pager.ShowNavigationToolTip = true;
            pager.ShowPageIndexBox = ShowPageIndexBox.Never;
            pager.ShowPageIndex = true;
            pager.AlwaysShowFirstLastPageNumber = true;
            pager.AlwaysShow = false;
            pager.UrlPageIndexName = "page";

            p.Controls.Add(pager);
        }
    }
}