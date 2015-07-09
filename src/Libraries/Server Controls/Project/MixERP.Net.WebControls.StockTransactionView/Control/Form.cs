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
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void CreateFilterSegment(Control container)
        {
            using (HtmlGenericControl filterSegment = HtmlControlHelper.GetSegment())
            {
                filterSegment.ID = "FilterDiv";

                using (HtmlGenericControl form = HtmlControlHelper.GetForm())
                {
                    form.Attributes.Add("margin-left", "8px;");

                    using (HtmlGenericControl nineFields = HtmlControlHelper.GetFields("nine fields"))
                    {
                        this.CreateDateFromField(nineFields);
                        this.CreateDateToField(nineFields);
                        this.CreateOfficeField(nineFields);
                        this.CreatePartyField(nineFields);
                        this.CreatePriceTypeField(nineFields);
                        this.CreateUserField(nineFields);
                        this.CreateReferenceNumberField(nineFields);
                        this.CreateStatementReferenceField(nineFields);
                        this.CreateShowButtonField(nineFields);

                        form.Controls.Add(nineFields);
                    }

                    filterSegment.Controls.Add(form);
                }

                container.Controls.Add(filterSegment);
            }
        }
    }
}