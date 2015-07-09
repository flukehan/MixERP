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

using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private TemplateField GetSelectColumnTemplateField()
        {
            var selectTemplate = new TemplateField();
            selectTemplate.HeaderText = Titles.Select;

            using (var itemTemplate = new ScrudItemSelectorSelectTemplate(this.Catalog))
            {
                selectTemplate.ItemTemplate = itemTemplate;
            }

            return selectTemplate;
        }

        private void LoadGridPanel(Panel gridPanel)
        {
            gridPanel.ID = "GridPanel";
            gridPanel.CssClass = this.GridPanelCssClass;
            gridPanel.Attributes.Add("style", "overflow:auto");
            gridPanel.Attributes.Add("padding", "4px");

            if (this.GridPanelHeight.Value > 0)
            {
                gridPanel.Height = this.GridPanelHeight;
            }

            if (this.GridPanelWidth.Value > 0)
            {
                gridPanel.Height = this.GridPanelWidth;
            }

            this.searchGridView = new MixERPGridView();
            this.searchGridView.ID = "SearchGridView";

            this.searchGridView.Attributes.Add("style", "white-space: nowrap;");
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

            using (HtmlGenericControl form = new HtmlGenericControl("div"))
            {
                form.Attributes.Add("class", "ui small form");
                using (var fields = new HtmlGenericControl("div"))
                {
                    fields.Attributes.Add("class", "inline fields");
                    using (var filterSelectField = new HtmlGenericControl("div"))
                    {
                        filterSelectField.Attributes.Add("class", "field");

                        this.filterSelect = new DropDownList();

                        this.filterSelect.ID = "FilterDropDownList";
                        this.filterSelect.CssClass = this.FilterDropDownListCssClass;
                        this.filterSelect.DataTextField = "ColumnName";
                        this.filterSelect.DataValueField = "ColumnName";
                        this.filterSelect.DataBound += this.FilterSelectDataBound;

                        filterSelectField.Controls.Add(this.filterSelect);
                        fields.Controls.Add(filterSelectField);
                    }
                    using (var filterInputTextField = new HtmlGenericControl("div"))
                    {
                        filterInputTextField.Attributes.Add("class", "field");

                        this.filterInputText = new TextBox();
                        this.filterInputText.ID = "FilterTextBox";
                        this.filterInputText.CssClass = this.FilterTextBoxCssClass;
                        filterInputTextField.Controls.Add(this.filterInputText);
                        fields.Controls.Add(filterInputTextField);
                    }

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
                    fields.Controls.Add(this.goButton);

                    form.Controls.Add(fields);
                    topPanel.Controls.Add(form);
                }
            }
        }
    }
}