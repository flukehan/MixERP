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
        private void AddCashTransactionDivCell(HtmlTableRow row)
        {
            using (HtmlTableCell cell = TableHelper.GetFieldCell())
            {
                if (this.ShowTransactionType)
                {
                    using (HtmlGenericControl toggleCheckBox = HtmlControlHelper.GetToggleCheckBox())
                    {
                        toggleCheckBox.ID = "CashTransactionDiv";
                        using (HtmlInputCheckBox cashTransactionInputCheckBox = new HtmlInputCheckBox())
                        {
                            cashTransactionInputCheckBox.ID = "CashTransactionInputCheckBox";
                            cashTransactionInputCheckBox.Attributes.Add("checked", "checked");
                            toggleCheckBox.Controls.Add(cashTransactionInputCheckBox);
                        }

                        using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.CashTransaction))
                        {
                            toggleCheckBox.Controls.Add(label);
                        }

                        cell.Controls.Add(toggleCheckBox);
                    }
                }

                row.Cells.Add(cell);
            }
        }
    }
}