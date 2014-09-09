namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Stores
    {
        public static bool IsSalesAllowed(int storeId)
        {
            return DatabaseLayer.Office.Stores.IsSalesAllowed(storeId);
        }
    }
}