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

using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Tax
{
    public static class SalesTax
    {
        public static decimal GetSalesTax(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, decimal price, int quantity, decimal discount, decimal shippingCharge, int salesTaxId)
        {
            const string sql = "SELECT SUM(tax) FROM transactions.get_sales_tax(@TranBook, @StoreId, @PartyCode, @ShippingAddressCode, @PriceTypeId, @ItemCode, @Price, @Quantity, @Discount, @ShippingCharge, @SalesTaxId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TranBook", tranBook);
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@ShippingAddressCode", shippingAddressCode);
                command.Parameters.AddWithValue("@PriceTypeId", priceTypeId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Discount", discount);
                command.Parameters.AddWithValue("@ShippingCharge", shippingCharge);
                command.Parameters.AddWithValue("@SalesTaxId", salesTaxId);

                return Conversion.TryCastDecimal(DbOperation.GetScalarValue(command));
            }
        }

        public static DataTable GetSalesTaxes(int officeId, string tranBook)
        {
            string sql;

            if (tranBook.ToUpperInvariant().Equals("SALES"))
            {
                sql = "SELECT * FROM core.sales_taxes WHERE office_id=@OfficeId;";
            }
            else
            {
                sql = "SELECT * FROM core.sales_taxes WHERE sales_tax_id IN (SELECT sales_tax_id FROM core.sales_tax_details INNER JOIN core.sales_tax_types ON core.sales_tax_details.sales_tax_type_id = core.sales_tax_types.sales_tax_type_id AND core.sales_tax_types.is_vat=true) AND office_id=@OfficeId;";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);

                return DbOperation.GetDataTable(command);
            }
        }

        public static int GetSalesTaxId(string tranBook, int storeId, string partyCode, string shippingAddressCode, int priceTypeId, string itemCode, int unitId, decimal price)
        {
            const string sql = "SELECT transactions.get_sales_tax_id(@TranBook, @StoreId, @PartyCode, @ShippingAddressCode, @PriceTypeId, @ItemCode, @UnitId, @Price);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TranBook", tranBook);
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@ShippingAddressCode", shippingAddressCode);
                command.Parameters.AddWithValue("@PriceTypeId", priceTypeId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@UnitId", unitId);
                command.Parameters.AddWithValue("@Price", price);

                return Conversion.TryCastInteger(DbOperation.GetScalarValue(command));
            }
        }
    }
}