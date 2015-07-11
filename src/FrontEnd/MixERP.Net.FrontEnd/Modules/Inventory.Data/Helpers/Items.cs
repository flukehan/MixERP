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
using System.Linq;
using MixERP.Net.Entities.Core;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Items
    {
        public static decimal CountItemInStock(string catalog, string itemCode, int unitId, int storeId)
        {
            const string sql = "SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), @1, @2);";
            return Factory.Scalar<decimal>(catalog, sql, itemCode, unitId, storeId);
        }

        public static decimal CountItemInStock(string catalog, string itemCode, string unitName, int storeId)
        {
            const string sql =
                "SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), core.get_unit_id_by_unit_name(@1), @2);";
            return Factory.Scalar<decimal>(catalog, sql, itemCode, unitName, storeId);
        }

        public static decimal CountItemInStock(string catalog, string itemCode, string unitName, string storeName)
        {
            const string sql =
                "SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), core.get_unit_id_by_unit_name(@1), office.get_store_id_by_store_name(@2));";
            return Factory.Scalar<decimal>(catalog, sql, itemCode, unitName, storeName);
        }

        public static string GetItemCodeByItemId(string catalog, int itemId)
        {
            const string sql = "SELECT item_code FROM core.items WHERE item_id=@0;";
            return Factory.Scalar<string>(catalog, sql, itemId);
        }

        public static decimal GetItemCostPrice(string catalog, string itemCode, string partyCode, int unitId)
        {
            const string sql =
                "SELECT core.get_item_cost_price(core.get_item_id_by_item_code(@0)::integer, core.get_party_id_by_party_code(@1)::bigint, @2::integer);";
            return Factory.Scalar<decimal>(catalog, sql, itemCode, partyCode, unitId);
        }

        public static IEnumerable<DbGetCompoundItemDetailsResult> GetCompoundItemDetails(string catalog,
            string compoundItemCode, string salesTaxCode, string tranBook, int storeId, string partyCode,
            int priceTypeId)
        {
            const string sql =
                "SELECT * FROM core.get_compound_item_details(@0::national character varying(12), @1::national character varying(24), @2::national character varying(48), @3::integer, @4::national character varying(12), @5::integer);";
            return Factory.Get<DbGetCompoundItemDetailsResult>(catalog, sql, compoundItemCode, salesTaxCode, tranBook,
                storeId, partyCode, priceTypeId);
        }

        public static IEnumerable<Item> GetItems(string catalog)
        {
            const string sql = "SELECT * FROM core.items ORDER BY item_id;";
            return Factory.Get<Item>(catalog, sql);
        }

        public static IEnumerable<CompoundItem> GetCompoundItems(string catalog)
        {
            const string sql = "SELECT * FROM core.compound_items ORDER BY compound_item_id;";
            return Factory.Get<CompoundItem>(catalog, sql);
        }

        public static decimal GetItemSellingPrice(string catalog, string itemCode, string partyCode, int priceTypeId,
            int unitId)
        {
            const string sql =
                "SELECT core.get_item_selling_price(core.get_item_id_by_item_code(@0)::integer, core.get_party_type_id_by_party_code(@1)::integer, @2::integer, @3::integer);";
            return Factory.Scalar<decimal>(catalog, sql, itemCode, partyCode, priceTypeId, unitId);
        }

        public static IEnumerable<Item> GetStockItems(string catalog)
        {
            const string sql = "SELECT * FROM core.items WHERE maintain_stock ORDER BY item_id;";
            return Factory.Get<Item>(catalog, sql);
        }

        public static decimal GetTaxRate(string catalog, string itemCode)
        {
            const string sql = "SELECT core.get_item_tax_rate(core.get_item_id_by_item_code(@0));";
            return Factory.Scalar<decimal>(catalog, sql, itemCode);
        }

        public static bool IsStockItem(string catalog, string itemCode)
        {
            const string sql = "SELECT core.is_stock_item(@0);";
            return Factory.Scalar<bool>(catalog, sql, itemCode);
        }

        public static bool ItemExistsByCode(string catalog, string itemCode)
        {
            const string sql = "SELECT * FROM core.items WHERE core.items.item_code=@0;";
            return Factory.Get<Item>(catalog, sql, itemCode).Count().Equals(1);
        }
    }
}