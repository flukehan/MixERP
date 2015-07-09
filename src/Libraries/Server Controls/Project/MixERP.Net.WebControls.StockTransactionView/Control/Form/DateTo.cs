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
        private void CreateDateToField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.dateToDateTextBox = new DateTextBox();
                this.dateToDateTextBox.ID = "DateToDateTextBox";
                this.dateToDateTextBox.CssClass = "date";
                this.dateToDateTextBox.Mode = FrequencyType.MonthEndDate;
                this.dateToDateTextBox.Required = true;
                this.dateToDateTextBox.Catalog = this.Catalog;
                this.dateToDateTextBox.OfficeId = this.OfficeId;

                field.Controls.Add(this.dateToDateTextBox);
                container.Controls.Add(field);
            }
        }
    }
}