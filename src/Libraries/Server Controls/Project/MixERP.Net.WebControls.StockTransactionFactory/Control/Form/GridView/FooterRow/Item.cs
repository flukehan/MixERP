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
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private static void CreateItemCodeField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText itemCodeInputText = new HtmlInputText())
                {
                    itemCodeInputText.ID = "ItemCodeInputText";
                    itemCodeInputText.Attributes.Add("title", "ALT + C");

                    cell.Controls.Add(itemCodeInputText);
                }

                row.Cells.Add(cell);
            }
        }

        private static void CreateItemField(TableRow row)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlSelect itemSelect = new HtmlSelect())
                {
                    itemSelect.ID = "ItemSelect";
                    itemSelect.Attributes.Add("title", "Ctrl + I");
                    cell.Controls.Add(itemSelect);
                }

                row.Cells.Add(cell);
            }
        }

    }
}