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

using System.Collections.Generic;
using MixERP.Net.Entities;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Tax
{
    /// <summary>The sales tax helper class.</summary>
    public class SalesTax
    {
        /// <summary>Gets sales tax rate depending upon the supplied parameters.</summary>
        /// <param name="tranBook">           Name of the transaction book.</param>
        /// <param name="storeId">            The store id.</param>
        /// <param name="partyCode">          The party code.</param>
        /// <param name="shippingAddressCode">The shipping address code.</param>
        /// <param name="priceTypeId">        The price type id.</param>
        /// <param name="itemCode">           The item code.</param>
        /// <param name="price">              The price.</param>
        /// <param name="quantity">           The quantity.</param>
        /// <param name="discount">           The discount.</param>
        /// <param name="shippingCharge">     The shipping charge.</param>
        /// <param name="salesTaxId">         The sales tax id.</param>
        /// <returns>The sales tax.</returns>
        public static decimal GetSalesTax(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, decimal price, int quantity, decimal discount, decimal shippingCharge, int salesTaxId)
        {
            const string sql = "SELECT SUM(tax) FROM transactions.get_sales_tax(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10);";
            return Factory.Scalar<decimal>(sql, tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId);
        }

        /// <summary>Gets a collection of various sales tax associated with this office.</summary>
        ///
        /// <param name="tranBook">Name of the transaction book.</param>
        /// <param name="officeId">The office on which sales tax are created.</param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the sales taxes in this collection.
        /// </returns>

        public static IEnumerable<Entities.Core.SalesTax> GetSalesTaxes(string tranBook, int officeId)
        {
            if (tranBook.ToUpperInvariant().Equals("SALES"))
            {
                return Factory.Get<Entities.Core.SalesTax>("SELECT * FROM core.sales_taxes WHERE office_id=@0;", officeId);
            }


            const string sql = "SELECT * FROM core.sales_taxes " +
                               "WHERE sales_tax_id IN " +
                               "(" +
                               "SELECT sales_tax_id " +
                               "FROM core.sales_tax_details " +
                               "INNER JOIN core.sales_tax_types " +
                               "ON core.sales_tax_details.sales_tax_type_id = core.sales_tax_types.sales_tax_type_id " +
                               "AND core.sales_tax_types.is_vat=true" +
                               ") " +
                               "AND office_id=@0;";

            return Factory.Get<Entities.Core.SalesTax>(sql, officeId);
        }

        /// <summary>Gets the preferred SalesTaxId depending upon the supplied parameters.</summary>
        ///
        /// <param name="tranBook">           Name of the transaction book.</param>
        /// <param name="storeId">            The store id.</param>
        /// <param name="partyCode">          The party code.</param>
        /// <param name="shippingAddressCode">The shipping address code.</param>
        /// <param name="priceTypeId">        The price type id.</param>
        /// <param name="itemCode">           The item code.</param>
        /// <param name="unitId">             The unit id.</param>
        /// <param name="price">              The price.</param>
        ///
        /// <returns>The sales tax identifier.</returns>

        public static int GetSalesTaxId(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, int unitId, decimal price)
        {
            const string sql = "SELECT transactions.get_sales_tax_id(@0, @1, @2, @3, @4, @5, @6, @7);";
            return Factory.Scalar<int>(sql, tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, unitId, price);
        }
    }
}