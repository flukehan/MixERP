/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class Maintenance
    {
        public static void Vacuum()
        {
            string sql = "VACUUM;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
            }
        }

        public static void VacuumFull()
        {
            string sql = "VACUUM FULL;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
            }            
        }

        public static void Analyze()
        {
            string sql = "ANALYZE;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.CommandTimeout = 3600;
                MixERP.Net.DBFactory.DBOperations.ExecuteNonQuery(command);
            }
        }
    }
}
