using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Stores
    {
        public static bool IsSalesAllowed(int storeId)
        {
            const string sql = "SELECT 1 FROM office.stores WHERE store_id=@StoreId and allow_sales='yes';";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StoreId", storeId);
                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }
    }
}