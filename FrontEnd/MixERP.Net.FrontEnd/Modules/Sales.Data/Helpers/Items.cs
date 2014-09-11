using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Items
    {
        public static bool IsStockItem(string itemCode)
        {
            const string sql = "SELECT core.is_stock_item(@ItemCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
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
    }
}