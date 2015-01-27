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
using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using Npgsql;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Items
    {
        public static IEnumerable<Item> GetItems()
        {
            return Factory.Get<Item>("SELECT * FROM core.items ORDER BY item_id");
        }

        public static IEnumerable<Item> GetStockItems()
        {
            return Factory.Get<Item>("SELECT * FROM core.items WHERE maintain_stock ORDER BY item_id");
        }

        public static bool ItemExistsByCode(string itemCode)
        {
            return Factory.Get<Item>("SELECT * FROM core.items WHERE core.items.item_code=@0;", itemCode).Count().Equals(1);
        }

        public static decimal GetItemSellingPrice(string itemCode, string partyCode, int priceTypeId, int unitId)
        {
            return Factory.Scalar<decimal>("SELECT core.get_item_selling_price(core.get_item_id_by_item_code(@0), core.get_party_type_id_by_party_code(@1), @2, @3);", itemCode, partyCode, priceTypeId, unitId);
        }

        public static decimal GetItemCostPrice(string itemCode, string partyCode, int unitId)
        {
            return Factory.Scalar<decimal>("SELECT core.get_item_cost_price(core.get_item_id_by_item_code(@0), core.get_party_id_by_party_code(@1), @2);", itemCode, partyCode, unitId);
        }

        public static decimal GetTaxRate(string itemCode)
        {
            return Factory.Scalar<decimal>("SELECT core.get_item_tax_rate(core.get_item_id_by_item_code(@0));", itemCode);
        }

        public static decimal CountItemInStock(string itemCode, int unitId, int storeId)
        {
            return Factory.Scalar<decimal>("SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), @1, @2);", itemCode, unitId, storeId);
        }

        public static decimal CountItemInStock(string itemCode, string unitName, int storeId)
        {
            return Factory.Scalar<decimal>("SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), core.get_unit_id_by_unit_name(@1), @2);", itemCode, unitName, storeId);
        }

        public static decimal CountItemInStock(string itemCode, string unitName, string storeName)
        {
            return Factory.Scalar<decimal>("SELECT core.count_item_in_stock(core.get_item_id_by_item_code(@0), core.get_unit_id_by_unit_name(@1), office.get_store_id_by_store_name(@2));", itemCode, unitName, storeName);            
        }

        public static bool IsStockItem(string itemCode)
        {
            return Factory.Scalar<bool>("SELECT core.is_stock_item(@0);", itemCode);
        }

        public static string GetItemCodeByItemId(int itemId)
        {
            return Factory.Scalar<string>("SELECT item_code FROM core.items WHERE item_id=@0;", itemId);
        }
    }
}