namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Items
    {
        public static bool IsStockItem(string itemCode)
        {
            return DatabaseLayer.Core.Items.IsStockItem(itemCode);
        }

        public static decimal CountItemInStock(string itemCode, string unitName, int storeId)
        {
            return DatabaseLayer.Core.Items.CountItemInStock(itemCode, unitName, storeId);
        }
    }
}