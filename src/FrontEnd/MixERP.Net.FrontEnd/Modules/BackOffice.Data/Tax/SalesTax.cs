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
using PetaPoco;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Tax
{
    /// <summary>The sales tax helper class.</summary>
    public class SalesTax
    {
        public static decimal GetSalesTax(string catalog, string tranBook, int storeId, string partyCode,
            string shippingAddressCode, int priceTypeId, string itemCode, decimal price, int quantity, decimal discount,
            decimal shippingCharge, int salesTaxId)
        {
            const string sql =
                "SELECT COALESCE(SUM(tax), 0) FROM transactions.get_sales_tax(@0::national character varying(12),@1::integer,@2::national character varying(12),@3::national character varying(12),@4::integer,@5::national character varying(12),@6::public.money_strict2,@7::public.integer_strict2,@8::public.money_strict2,@9::public.money_strict2,@10::integer);";
            return Factory.Scalar<decimal>(catalog, sql, tranBook, storeId, partyCode, shippingAddressCode, priceTypeId,
                itemCode, price, quantity, discount, shippingCharge, salesTaxId);
        }

        public static IEnumerable<Entities.Core.SalesTax> GetSalesTaxes(string catalog, string tranBook, int officeId)
        {
            if (tranBook.ToUpperInvariant().Equals("SALES"))
            {
                return Factory.Get<Entities.Core.SalesTax>(catalog, "SELECT * FROM core.sales_taxes WHERE office_id=@0;",
                    officeId);
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

            return Factory.Get<Entities.Core.SalesTax>(catalog, sql, officeId);
        }

        public static int GetSalesTaxId(string catalog, string tranBook, int storeId, string partyCode,
            string shippingAddressCode, int priceTypeId, string itemCode, int unitId, decimal price)
        {
            const string sql =
                "SELECT transactions.get_sales_tax_id(@0::national character varying(12), @1::integer, @2::national character varying(12), @3::national character varying(12), @4::integer, @5::national character varying(12), @6::integer, @7::public.money_strict);";
            return Factory.Scalar<int>(catalog, sql, tranBook, storeId, partyCode, shippingAddressCode, priceTypeId,
                itemCode, unitId, price);
        }
    }
}