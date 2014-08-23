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
namespace MixERP.Net.BusinessLayer.Core
{
    public static class Items
    {
        public static bool ItemExistsByCode(string itemCode)
        {
            return DatabaseLayer.Core.Items.ItemExistsByCode(itemCode);
        }

        public static decimal GetItemSellingPrice(string itemCode, string partyCode, int priceTypeId, int unitId)
        {
            return DatabaseLayer.Core.Items.GetItemSellingPrice(itemCode, partyCode, priceTypeId, unitId);
        }

        public static decimal GetItemCostPrice(string itemCode, string partyCode, int unitId)
        {
            return DatabaseLayer.Core.Items.GetItemCostPrice(itemCode, partyCode, unitId);
        }

        public static decimal GetTaxRate(string itemCode)
        {
            return DatabaseLayer.Core.Items.GetTaxRate(itemCode);
        }

        public static decimal CountItemInStock(string itemCode, int unitId, int storeId)
        {
            return DatabaseLayer.Core.Items.CountItemInStock(itemCode, unitId, storeId);
        }

        public static decimal CountItemInStock(string itemCode, string unitName, int storeId)
        {
            return DatabaseLayer.Core.Items.CountItemInStock(itemCode, unitName, storeId);
        }

        public static bool IsStockItem(string itemCode)
        {
            return DatabaseLayer.Core.Items.IsStockItem(itemCode);
        }

        public static string GetItemCodeByItemId(int itemId)
        {
            return DatabaseLayer.Core.Items.GetItemCodeByItemId(itemId);
        }
    }
}
