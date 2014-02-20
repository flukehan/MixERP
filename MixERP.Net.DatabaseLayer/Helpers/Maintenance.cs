/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class Maintenance
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
