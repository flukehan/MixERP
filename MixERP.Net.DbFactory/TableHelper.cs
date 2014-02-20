/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Linq;
using System.Data;
using System.Threading;
using Npgsql;

namespace MixERP.Net.DBFactory
{
    public static class TableHelper
    {
        public static DataTable GetTable(string schema, string tableName, string exclusion)
        {
            string sql;

            if (!string.IsNullOrWhiteSpace(exclusion))
            {
                var exclusions = exclusion.Split(',');
                var paramNames = exclusions.Select((s, i) => "@Paramter" + i.ToString(Thread.CurrentThread.CurrentCulture).Trim()).ToArray();
                var inClause = string.Join(",", paramNames);

                sql = string.Format(Thread.CurrentThread.CurrentCulture, @"select * from scrud.mixerp_table_view where table_schema=@Schema AND table_name=@TableName AND column_name NOT IN({0});", inClause);

                using (var command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@Schema", schema);
                    command.Parameters.AddWithValue("@TableName", tableName);

                    for (var i = 0; i < paramNames.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramNames[i], exclusions[i].Trim());
                    }

                    return DbOperations.GetDataTable(command);
                }
            }

            sql = "select * from scrud.mixerp_table_view where table_schema=@Schema AND table_name=@TableName;";

            using (var command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Schema", schema);
                command.Parameters.AddWithValue("@TableName", tableName);

                return DbOperations.GetDataTable(command);
            }
        }
    }
}
