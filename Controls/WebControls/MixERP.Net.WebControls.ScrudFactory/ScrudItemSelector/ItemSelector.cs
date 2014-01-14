/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private void LoadItemSelector(Panel container)
        {
            using (Panel topPanel = new Panel())
            {
                this.LoadTopPanel(topPanel);
                container.Controls.Add(topPanel);
            }

            using (Panel gridPanel = new Panel())
            {
                this.LoadGridPanel(gridPanel);
                container.Controls.Add(gridPanel);
            }
        }

        private void LoadTopPanel(Panel topPanel)
        {
            topPanel.CssClass = this.TopPanelCssClass;
            using (HtmlTable table = new HtmlTable())
            {
                using (HtmlTableRow row = new HtmlTableRow())
                {
                    using (HtmlTableCell dropDownListCell = new HtmlTableCell())
                    {
                        filterDropDownList = new DropDownList();

                        filterDropDownList.ID = "FilterDropDownList";
                        filterDropDownList.CssClass = this.FilterDropDownListCssClass;
                        filterDropDownList.DataTextField = "column_name";
                        filterDropDownList.DataValueField = "column_name";
                        filterDropDownList.DataBound += this.FilterDropDownList_DataBound;

                        dropDownListCell.Controls.Add(filterDropDownList);
                        row.Cells.Add(dropDownListCell);

                    }
                    using (HtmlTableCell textBoxCell = new HtmlTableCell())
                    {
                        filterTextBox = new TextBox();
                        filterTextBox.ID = "FilterTextBox";
                        filterTextBox.CssClass = this.FilterTextBoxCssClass;
                        textBoxCell.Controls.Add(filterTextBox);
                        row.Cells.Add(textBoxCell);
                    }

                    using (HtmlTableCell buttonCell = new HtmlTableCell())
                    {
                        goButton = new Button();
                        goButton.ID = "GoButton";
                        goButton.CssClass = this.ButtonCssClass;

                        if (this.ButtonHeight != null)
                        {
                            goButton.Height = this.ButtonHeight;
                        }

                        if (this.ButtonWidth != null)
                        {
                            goButton.Width = this.ButtonWidth;
                        }

                        goButton.Click += this.GoButton_Click;
                        goButton.Text = Resources.ScrudResource.Go;
                        buttonCell.Controls.Add(goButton);
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

            if (this.GridPanelHeight != null)
            {
                gridPanel.Height = this.GridPanelHeight;
            }

            if (this.GridPanelWidth != null)
            {
                gridPanel.Height = this.GridPanelWidth;
            }

            searchGridView = new GridView();
            searchGridView.ID = "SearchGridView";
            searchGridView.GridLines = GridLines.None;
            searchGridView.CssClass = this.GridViewCssClass;
            searchGridView.PagerStyle.CssClass = this.GridViewPagerCssClass;
            searchGridView.RowStyle.CssClass = this.GridViewRowCssClass;
            searchGridView.AlternatingRowStyle.CssClass = this.GridViewAlternateRowCssClass;
            searchGridView.AutoGenerateColumns = true;
            searchGridView.RowDataBound += this.SearchGridView_RowDataBound;
            searchGridView.Columns.Add(this.GetSelectColumnTemplateField());

            gridPanel.Controls.Add(searchGridView);
        }

        private TemplateField GetSelectColumnTemplateField()
        {
            TemplateField selectTemplate = new TemplateField();
            selectTemplate.HeaderText = Resources.ScrudResource.Select;

            using (ScrudItemSelectorSelectTemplate itemTemplate = new ScrudItemSelectorSelectTemplate())
            {
                selectTemplate.ItemTemplate = itemTemplate;
            }

            return selectTemplate;
        }



    }
}
