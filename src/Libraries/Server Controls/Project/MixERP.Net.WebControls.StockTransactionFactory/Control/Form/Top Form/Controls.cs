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

using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private void AddTopFormControls(HtmlTable table)
        {
            using (HtmlTableRow row = new HtmlTableRow())
            {
                this.AddDateTextBoxCell(row);

                if (this.ShowStore)
                {
                    AddStoreSelectCell(row);
                }

                AddPartyCodeInputTextCell(row);
                AddPartySelectCell(row);

                if (this.ShowPriceTypes)
                {
                    AddPriceTypeSelectCell(row);
                }

                this.AddReferenceNumberInputTextCell(row);
                this.AddCashTransactionDivCell(row);
                this.AddPaymentTermSelectCell(row);

                table.Controls.Add(row);
            }
        }
    }
}