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

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private void CreateBottomFormPanel(Control container)
        {
            using (HtmlGenericControl formContainer = new HtmlGenericControl("div"))
            {
                formContainer.Attributes.Add("style", "width:500px");

                using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment()) //ui page form segment
                {
                    if (this.ShowShippingInformation)
                    {
                        AddShippingAddressTextAreaField(formSegment);
                    }

                    AddTotalFields(formSegment);

                    if (this.ShowCostCenter)
                    {
                        AddCostCenterField(formSegment);
                    }

                    if (this.ShowSalesAgents)
                    {
                        AddSalespersonField(formSegment);
                    }

                    this.AddStatementReferenceField(formSegment);
                    AddSaveButton(formSegment);

                    formContainer.Controls.Add(formSegment);
                }

                container.Controls.Add(formContainer);
            }
        }
    }
}