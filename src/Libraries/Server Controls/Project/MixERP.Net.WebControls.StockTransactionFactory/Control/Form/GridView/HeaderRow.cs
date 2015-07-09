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
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private static void CreateHeaderRow(Table grid)
        {
            using (TableRow header = new TableRow())
            {
                header.TableSection = TableRowSection.TableHeader;

                TableHelper.CreateHeaderCell(header, Titles.ItemCode, "ItemCodeInputText");
                TableHelper.CreateHeaderCell(header, Titles.ItemName, "ItemSelect");
                TableHelper.CreateHeaderCell(header, Titles.QuantityAbbreviated, "QuantityInputText");
                TableHelper.CreateHeaderCell(header, Titles.Unit, "UnitSelect");
                TableHelper.CreateHeaderCell(header, Titles.Price, "PriceInputText");
                TableHelper.CreateHeaderCell(header, Titles.Amount, "AmountInputText");
                TableHelper.CreateHeaderCell(header, Titles.Discount, "DiscountInputText");
                TableHelper.CreateHeaderCell(header, Titles.ShippingCharge, "ShippingChargeInputText");
                TableHelper.CreateHeaderCell(header, Titles.SubTotal, "SubTotalInputText");
                TableHelper.CreateHeaderCell(header, Titles.TaxForm, "TaxSelect");
                TableHelper.CreateHeaderCell(header, Titles.Tax, "TaxInputText");
                TableHelper.CreateHeaderCell(header, Titles.Action, null);

                grid.Rows.Add(header);
            }
        }
    }
}