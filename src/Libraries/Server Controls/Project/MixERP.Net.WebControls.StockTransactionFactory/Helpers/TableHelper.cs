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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionFactory.Helpers
{
    internal static class TableHelper
    {
        internal static HtmlTableCell GetFieldCell()
        {
            using (HtmlTableCell cell = new HtmlTableCell())
            {
                cell.Attributes.Add("class", "ui field");
                return cell;
            }
        }

        internal static void AddCell(HtmlTableRow row, string text)
        {
            using (HtmlTableCell cell = new HtmlTableCell())
            {
                cell.InnerHtml = text;

                row.Cells.Add(cell);
            }
        }

        internal static void CreateHeaderCell(TableRow row, string text, string targetControlId)
        {
            using (TableHeaderCell cell = new TableHeaderCell())
            {
                if (targetControlId != null)
                {
                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(text, targetControlId))
                    {
                        cell.Controls.Add(label);
                    }
                }
                else
                {
                    cell.Text = text;
                }

                row.Cells.Add(cell);
            }
        }
    }
}