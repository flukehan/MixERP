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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.BackOffice.Data.Tax;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.BackOffice.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class TaxData : WebService
    {
        /// <summary>Gets sales tax rate.</summary>
        ///
        /// <param name="tranBook">           The transaction book.</param>
        /// <param name="storeId">            The store id.</param>
        /// <param name="partyCode">          The party code.</param>
        /// <param name="shippingAddressCode">The shipping address code.</param>
        /// <param name="priceTypeId">        The price type id.</param>
        /// <param name="itemCode">           The item code.</param>
        /// <param name="price">              The price.</param>
        /// <param name="quantity">           The quantity.</param>
        /// <param name="discount">           The discount.</param>
        /// <param name="shippingCharge">     The shipping charge amount.</param>
        /// <param name="salesTaxId">         Sales tax id.</param>
        ///
        /// <returns>The sales tax.</returns>

        [WebMethod]
        public decimal GetSalesTax(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, decimal price, int quantity, decimal discount, decimal shippingCharge, int salesTaxId)
        {
            return SalesTax.GetSalesTax(AppUsers.GetCurrentUserDB(), tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId);
        }

        /// <summary>Gets a collection sales taxes of the current office.</summary>
        ///
        /// <param name="tranBook">Name of the transaction book to filter the result by.</param>
        ///
        /// <returns>The sales taxes.</returns>

        [WebMethod]
        public Collection<ListItem> GetSalesTaxes(string tranBook)
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            Collection<ListItem> values = new Collection<ListItem>();

            foreach (Net.Entities.Core.SalesTax salesTax in SalesTax.GetSalesTaxes(AppUsers.GetCurrentUserDB(), tranBook, officeId))
            {
                values.Add(new ListItem(salesTax.SalesTaxCode, salesTax.SalesTaxId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        /// <summary>Gets the preferred SalesTaxId depending upon the supplied parameters.</summary>
        ///
        /// <param name="tranBook">           Name of the transaction book.</param>
        /// <param name="storeId">            Id of the store to search the result for.</param>
        /// <param name="partyCode">          The party code.</param>
        /// <param name="shippingAddressCode">The shipping address code.</param>
        /// <param name="priceTypeId">        Id of the price type.</param>
        /// <param name="itemCode">           The item code.</param>
        /// <param name="unitId">             Id of the unit.</param>
        /// <param name="price">              The price.</param>
        ///
        /// <returns>The sales tax identifier.</returns>

        [WebMethod]
        public int GetSalesTaxId(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, int unitId, decimal price)
        {
            return SalesTax.GetSalesTaxId(AppUsers.GetCurrentUserDB(), tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, unitId, price);
        }
    }
}