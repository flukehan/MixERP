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
using System.Collections.ObjectModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.BackOffice.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class TaxData : WebService
    {
        [WebMethod]
        public decimal GetSalesTax(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, decimal price, int quantity, decimal discount, decimal shippingCharge, int salesTaxId)
        {
            return Data.Tax.SalesTax.GetSalesTax(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId);
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetSalesTaxes()
        {
            int officeId = SessionHelper.GetOfficeId();

            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Tax.SalesTax.GetSalesTaxes(officeId))
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["sales_tax_code"].ToString(), dr["sales_tax_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public int GetSalesTaxId(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode)
        {
            return Data.Tax.SalesTax.GetSalesTaxId(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode);
        }
    }
}