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

using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private static TemplateField GetSelectColumnTemplateField()
        {
            var selectTemplate = new TemplateField();
            selectTemplate.HeaderText = Titles.Select;

            using (var itemTemplate = new ScrudItemSelectorSelectTemplate())
            {
                selectTemplate.ItemTemplate = itemTemplate;
            }

            return selectTemplate;
        }

        private void LoadGridPanel(Panel gridPanel)
        {
            gridPanel.ID = "GridPanel";
            gridPanel.ScrollBars = ScrollBars.Auto;
            gridPanel.CssClass = this.GridPanelCssClass;

            if (this.GridPanelHeight.Value > 0)
            {
                gridPanel.Height = this.GridPanelHeight;
            }

            if (this.GridPanelWidth.Value > 0)
            {
                gridPanel.Height = this.GridPanelWidth;
            }

            this.searchGridView = new GridView();
            this.searchGridView.ID = "SearchGridView";
            this.searchGridView.GridLines = GridLines.None;
            this.searchGridView.CssClass = this.GridViewCssClass;
            this.searchGridView.PagerStyle.CssClass = this.GridViewPagerCssClass;
            this.searchGridView.RowStyle.CssClass = this.GridViewRowCssClass;
            this.searchGridView.AlternatingRowStyle.CssClass = this.GridViewAlternateRowCssClass;
            this.searchGridView.AutoGenerateColumns = true;
            this.searchGridView.RowDataBound += this.SearchGridView_RowDataBound;
            this.searchGridView.Columns.Add(GetSelectColumnTemplateField());

            gridPanel.Controls.Add(this.searchGridView);
        }

        private void LoadItemSelector(Panel panel)
        {
            using (var topPanel = new Panel())
            {
                this.LoadTopPanel(topPanel);
                panel.Controls.Add(topPanel);
            }

            using (var gridPanel = new Panel())
            {
                this.LoadGridPanel(gridPanel);
                panel.Controls.Add(gridPanel);
            }
        }

        private void LoadTopPanel(Panel topPanel)
        {
            topPanel.CssClass = this.TopPanelCssClass;
            using (var table = new HtmlTable())
            {
                using (var row = new HtmlTableRow())
                {
                    using (var dropDownListCell = new HtmlTableCell())
                    {
                        this.filterDropDownList = new DropDownList();

                        this.filterDropDownList.ID = "FilterDropDownList";
                        this.filterDropDownList.CssClass = this.FilterDropDownListCssClass;
                        this.filterDropDownList.DataTextField = "column_name";
                        this.filterDropDownList.DataValueField = "column_name";
                        this.filterDropDownList.DataBound += this.FilterDropDownList_DataBound;

                        dropDownListCell.Controls.Add(this.filterDropDownList);
                        row.Cells.Add(dropDownListCell);
                    }
                    using (var textBoxCell = new HtmlTableCell())
                    {
                        this.filterTextBox = new TextBox();
                        this.filterTextBox.ID = "FilterTextBox";
                        this.filterTextBox.CssClass = this.FilterTextBoxCssClass;
                        textBoxCell.Controls.Add(this.filterTextBox);
                        row.Cells.Add(textBoxCell);
                    }

                    using (var buttonCell = new HtmlTableCell())
                    {
                        this.goButton = new Button();
                        this.goButton.ID = "GoButton";
                        this.goButton.CssClass = this.ButtonCssClass;

                        if (this.ButtonHeight.Value > 0)
                        {
                            this.goButton.Height = this.ButtonHeight;
                        }

                        if (this.ButtonWidth.Value > 0)
                        {
                            this.goButton.Width = this.ButtonWidth;
                        }

                        this.goButton.Click += this.GoButton_Click;
                        this.goButton.Text = Titles.Go;
                        buttonCell.Controls.Add(this.goButton);
                        row.Cells.Add(buttonCell);
                    }

                    table.Rows.Add(row);
                    topPanel.Controls.Add(table);
                }
            }
        }
    }
}