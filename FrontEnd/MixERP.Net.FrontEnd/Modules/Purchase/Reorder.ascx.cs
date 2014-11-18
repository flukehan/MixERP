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
using MixERP.Net.Core.Modules.Purchase.Resources;
using MixERP.Net.FrontEnd.Base;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class Reorder : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.AddTitle();
            this.AddRuler();
            this.AddButton();
            this.AddGridView();
            base.OnControlLoad(sender, e);
        }

        protected void ReorderButton_Click(object sender, EventArgs e)
        {
        }

        private void AddBoundField(GridView grid, string text, string dataField)
        {
            BoundField field = new BoundField { HeaderText = text, DataField = dataField };

            grid.Columns.Add(field);
        }

        private void AddButton()
        {
            using (HtmlGenericControl div = new HtmlGenericControl())
            {
                div.TagName = "div";
                div.Attributes.Add("class", "vpad16");

                using (HtmlInputButton reorderButton = new HtmlInputButton())
                {
                    reorderButton.Attributes.Add("class", "ui positive button");
                    reorderButton.Attributes.Add("onclick", "ReorderInputButtonClick()");
                    reorderButton.Value = Titles.PlaceReorderRequests;
                    div.Controls.Add(reorderButton);
                    this.Placeholder1.Controls.Add(div);
                }
            }
        }

        private void AddGridView()
        {
            int officeId = SessionHelper.GetOfficeId();

            using (GridView grid = new GridView())
            {
                using (DataTable table = Data.Transactions.Reorder.GetReorderView(officeId))
                {
                    grid.GridLines = GridLines.None;
                    this.CreateColumns(grid);
                    grid.DataSource = table;
                    grid.ID = "ReorderGrid";
                    grid.AutoGenerateColumns = false;
                    grid.DataBind();
                    this.Placeholder1.Controls.Add(grid);
                }
            }
        }

        private void AddRuler()
        {
            using (HtmlGenericControl ruler = new HtmlGenericControl("div"))
            {
                ruler.Attributes.Add("class", "ui divider");
                this.Placeholder1.Controls.Add(ruler);
            }
        }

        private void AddTemplateField(GridView grid,
            string text)
        {
            TemplateField template = new TemplateField();
            template.HeaderText = text;
            grid.Columns.Add(template);
        }

        private void AddTitle()
        {
            using (HtmlGenericControl heading = new HtmlGenericControl())
            {
                heading.TagName = "h1";
                heading.Attributes.Add("class", "lead heading");
                heading.InnerText = Titles.ItemsBelowReorderLevel;
                this.Placeholder1.Controls.Add(heading);
            }
        }

        private void CreateColumns(GridView grid)
        {
            this.AddTemplateField(grid, Titles.Check);
            this.AddBoundField(grid, Titles.ItemId, "item_id");
            this.AddBoundField(grid, Titles.ItemCode, "item_code");
            this.AddBoundField(grid, Titles.ItemName, "item_name");
            this.AddBoundField(grid, Titles.Unit, "unit");
            this.AddTemplateField(grid, Titles.SelectSupplier);
            this.AddTemplateField(grid, Titles.SelectUnit);
            this.AddTemplateField(grid, Titles.ReorderQuantityAbbreviated);
            this.AddTemplateField(grid, Titles.Price);
            this.AddTemplateField(grid, Titles.TaxRate);
            this.AddBoundField(grid, Titles.QuantityOnHandAbbreviated, "quantity_on_hand");
            this.AddBoundField(grid, Titles.ReorderLevel, "reorder_level");
            this.AddBoundField(grid, Titles.DefaultReorderQuantityAbbreviated, "reorder_quantity");
            this.AddBoundField(grid, Titles.PreferredSupplier, "preferred_supplier");
            this.AddBoundField(grid, Titles.Price, "price");
            this.AddBoundField(grid, Titles.TaxRate, "tax_rate");
        }
    }
}