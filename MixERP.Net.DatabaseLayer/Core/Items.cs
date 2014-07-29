/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Items
    {
        public static bool ItemExistsByCode(string itemCode)
        {
            const string sql = "SELECT 1 FROM core.items WHERE core.items.item_code=@ItemCode;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);

                return DbOperations.GetScalarValue(command).ToString().Equals("1");
            }
        }

        public static decimal GetItemSellingPrice(string itemCode, string partyCode, int priceTypeId, int unitId)
        {
            const string sql = "SELECT core.get_item_selling_price(core.get_item_id_by_item_code(@ItemCode), core.get_party_type_id_by_party_code(@PartyCode), @PriceTypeId, @UnitId);";
            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@PriceTypeId", priceTypeId);
                command.Parameters.AddWithValue("@UnitId", unitId);

                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }

        public static decimal GetItemCostPrice(string itemCode, string partyCode, int unitId)
        {
            const string sql = "SELECT core.get_item_cost_price(core.get_item_id_by_item_code(@ItemCode), core.get_party_id_by_party_code(@PartyCode), @UnitId);";
            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                command.Parameters.AddWithValue("@UnitId", unitId);

                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }

        public static decimal GetTaxRate(string itemCode)
        {
            const string sql = "SELECT core.get_item_tax_rate(core.get_item_id_by_item_code(@ItemCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }

        public static decimal CountItemInStock(string itemCode, int unitId, int storeId)
        {
            const string sql = "SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@ItemCode), @UnitId, @StoreId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@UnitId", unitId);
                command.Parameters.AddWithValue("@StoreId", storeId);
                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }

        public static decimal CountItemInStock(string itemCode, string unitName, int storeId)
        {
            const string sql = "SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@ItemCode), core.get_unit_id_by_unit_name(@UnitName), @StoreId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@UnitName", unitName);
                command.Parameters.AddWithValue("@StoreId", storeId);
                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }

        public static bool IsStockItem(string itemCode)
        {
            const string sql = "SELECT 1 FROM core.items WHERE item_code=@ItemCode AND maintain_stock=true;";
            using(NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }


        public static string GetItemCodeByItemId(int itemId)
        {
            const string sql = "SELECT item_code FROM core.items WHERE item_id=@ItemId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemId", itemId);

                return Conversion.TryCastString(DbOperations.GetScalarValue(command));
            }
        }
    }
}
