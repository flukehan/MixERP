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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private void AddSalesTypeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl salesTypeDiv = HtmlControlHelper.GetField())
            {
                salesTypeDiv.ID = "SalesTypeDiv";
                salesTypeDiv.Attributes.Add("style", "width:200px");

                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.SalesType, "SalesTypeSelect"))
                    {
                        field.Controls.Add(label);
                    }

                    using (HtmlSelect salesTypeSelect = new HtmlSelect())
                    {
                        salesTypeSelect.ID = "SalesTypeSelect";
                        salesTypeSelect.DataSource = this.GetSalesTypes();
                        salesTypeSelect.DataTextField = "Text";
                        salesTypeSelect.DataValueField = "Value";
                        salesTypeSelect.DataBind();

                        field.Controls.Add(salesTypeSelect);
                    }

                    salesTypeDiv.Controls.Add(field);
                }

                container.Controls.Add(salesTypeDiv);
            }
        }

        private IEnumerable<ListItem> GetSalesTypes()
        {
            Collection<ListItem> items = new Collection<ListItem>();

            items.Add(new ListItem(Titles.TaxableSales, "1"));
            items.Add(new ListItem(Titles.NonTaxableSales, "0"));


            if (this.model.NonTaxableSales)
            {
                items[1].Selected = true;
            }

            return items;
        }
    }
}