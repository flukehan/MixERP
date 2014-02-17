/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
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
                        this.goButton.Text = ScrudResource.Go;
                        buttonCell.Controls.Add(this.goButton);
                        row.Cells.Add(buttonCell);
                    }


                    table.Rows.Add(row);
                    topPanel.Controls.Add(table);
                }
            }
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

        private static TemplateField GetSelectColumnTemplateField()
        {
            var selectTemplate = new TemplateField();
            selectTemplate.HeaderText = ScrudResource.Select;

            using (var itemTemplate = new ScrudItemSelectorSelectTemplate())
            {
                selectTemplate.ItemTemplate = itemTemplate;
            }

            return selectTemplate;
        }



    }
}
