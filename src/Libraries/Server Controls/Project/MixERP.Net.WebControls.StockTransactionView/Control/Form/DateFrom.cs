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
using MixERP.Net.Entities;
using MixERP.Net.WebControls.Common;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void CreateDateFromField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.dateFromDateTextBox = new DateTextBox();
                this.dateFromDateTextBox.ID = "DateFromDateTextBox";
                this.dateFromDateTextBox.CssClass = "date";
                this.dateFromDateTextBox.Mode = FrequencyType.FiscalYearStartDate;
                this.dateFromDateTextBox.Required = true;
                this.dateFromDateTextBox.Catalog = this.Catalog;
                this.dateFromDateTextBox.OfficeId = this.OfficeId;

                field.Controls.Add(this.dateFromDateTextBox);
                container.Controls.Add(field);
            }
        }
    }
}