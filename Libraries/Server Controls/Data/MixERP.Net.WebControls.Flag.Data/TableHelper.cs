using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.WebControls.Flag.Data
{
    public static class TableHelper
    {
        public static DataTable GetFlags()
        {
            const string sql = "SELECT * FROM core.flag_types ORDER by flag_type_id;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return DbOperations.GetDataTable(command);
            }
        }
    }
}