namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
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