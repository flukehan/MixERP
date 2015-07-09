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
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private void AddTopFormLabels(HtmlTable table)
        {
            using (HtmlTableRow header = new HtmlTableRow())
            {
                TableHelper.AddCell(header, HtmlControlHelper.GetLabelHtml(Titles.ValueDate, "DateTextBox"));

                if (this.ShowStore)
                {
                    TableHelper.AddCell(header, HtmlControlHelper.GetLabelHtml(Titles.SelectStore, "StoreSelect"));
                }

                TableHelper.AddCell(header, HtmlControlHelper.GetLabelHtml(Titles.SelectParty, "PartyCodeInputText"));
                TableHelper.AddCell(header, string.Empty);

                if (this.ShowPriceTypes)
                {
                    TableHelper.AddCell(header, HtmlControlHelper.GetLabelHtml(Titles.PriceType, "PriceTypeSelect"));
                }

                TableHelper.AddCell(header, HtmlControlHelper.GetLabelHtml(Titles.ReferenceNumberAbbreviated, "ReferenceNumberInputText"));
                TableHelper.AddCell(header, string.Empty);
                TableHelper.AddCell(header, string.Empty);

                table.Rows.Add(header);
            }
        }
    }
}