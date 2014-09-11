using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Units
    {
        public static DataTable GetUnitViewByItemCode(string itemCode)
        {
            const string sql = "SELECT * FROM core.get_associated_units_from_item_code(@ItemCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);

                return DbOperations.GetDataTable(command);
            }
        }

        public static bool UnitExistsByName(string unitName)
        {
            const string sql = "SELECT 1 FROM core.units WHERE core.units.unit_name=@UnitName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UnitName", unitName);

                var value = DbOperations.GetScalarValue(command);
                if (value != null)
                {
                    return value.ToString().Equals("1");
                }
            }
            return false;
        }
    }
}