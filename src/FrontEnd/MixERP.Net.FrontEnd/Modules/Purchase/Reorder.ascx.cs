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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Purchase
{
    public partial class Reorder : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.AddTitle();
            this.AddRuler();
            this.AddButton();
            this.AddGridView();
        }

        protected void ReorderButton_Click(object sender, EventArgs e)
        {
        }

        private void AddBoundField(GridView grid, string text, string dataField)
        {
            BoundField field = new BoundField {HeaderText = text, DataField = dataField};

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
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (GridView grid = new GridView())
            {
                grid.GridLines = GridLines.None;
                this.CreateColumns(grid);
                grid.DataSource = Data.Transactions.Reorder.GetReorderView(AppUsers.GetCurrentUserDB(), officeId);
                grid.ID = "ReorderGrid";
                grid.AutoGenerateColumns = false;
                grid.DataBind();
                this.Placeholder1.Controls.Add(grid);
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
            this.AddBoundField(grid, Titles.ItemId, "ItemId");
            this.AddBoundField(grid, Titles.ItemCode, "ItemCode");
            this.AddBoundField(grid, Titles.ItemName, "ItemName");
            this.AddBoundField(grid, Titles.Unit, "Unit");
            this.AddTemplateField(grid, Titles.SelectSupplier);
            this.AddTemplateField(grid, Titles.SelectUnit);
            this.AddTemplateField(grid, Titles.ReorderQuantityAbbreviated);
            this.AddTemplateField(grid, Titles.Price);
            this.AddTemplateField(grid, Titles.TaxRate);
            this.AddBoundField(grid, Titles.QuantityOnHandAbbreviated, "QuantityOnHand");
            this.AddBoundField(grid, Titles.ReorderLevel, "ReorderLevel");
            this.AddBoundField(grid, Titles.DefaultReorderQuantityAbbreviated, "ReorderQuantity");
            this.AddBoundField(grid, Titles.PreferredSupplier, "PreferredSupplier");
            this.AddBoundField(grid, Titles.Price, "Price");
        }
    }
}