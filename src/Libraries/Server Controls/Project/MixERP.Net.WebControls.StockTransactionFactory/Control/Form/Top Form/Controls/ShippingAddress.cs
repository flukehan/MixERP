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
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private static void AddShippingAddressCompositeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl shippingAddressInfoDiv = HtmlControlHelper.GetField())
            {
                shippingAddressInfoDiv.ID = "ShippingAddressInfoDiv";
                shippingAddressInfoDiv.Attributes.Add("style", "width:500px;");

                using (HtmlGenericControl fields = HtmlControlHelper.GetFields("two fields"))
                {
                    AddShippingCompanyField(fields);
                    AddShippingAddressField(fields);

                    shippingAddressInfoDiv.Controls.Add(fields);
                }


                container.Controls.Add(shippingAddressInfoDiv);
            }
        }

        private static void AddShippingAddressField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ShippingAddress, "ShippingAddressSelect"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlSelect shippingAddressSelect = new HtmlSelect())
                {
                    shippingAddressSelect.ID = "ShippingAddressSelect";
                    field.Controls.Add(shippingAddressSelect);
                }

                container.Controls.Add(field);
            }
        }

        private static void AddShippingCompanyField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ShippingCompany, "ShippingCompanySelect"))
                {
                    field.Controls.Add(label);
                }
                using (HtmlSelect shippingCompanySelect = new HtmlSelect())
                {
                    shippingCompanySelect.ID = "ShippingCompanySelect";
                    field.Controls.Add(shippingCompanySelect);
                }

                container.Controls.Add(field);
            }
        }
    }
}