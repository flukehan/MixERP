using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    public static class DatabaseStatistics
    {
        public static void Vacuum()
        {
            const string sql = "VACUUM;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperations.ExecuteNonQuery(command);
            }
        }

        public static void VacuumFull()
        {
            const string sql = "VACUUM FULL;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperations.ExecuteNonQuery(command);
            }
        }

        public static void Analyze()
        {
            const string sql = "ANALYZE;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                DbOperations.ExecuteNonQuery(command);
            }
        }
    }
}