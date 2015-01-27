/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Data;
using System.Linq;
using System.Threading;
using Npgsql;

namespace MixERP.Net.DbFactory
{
    public static class TableHelper
    {
        public static DataTable GetTable(string schema, string tableName, string exclusion)
        {
            string sql;

            if (!string.IsNullOrWhiteSpace(exclusion))
            {
                var exclusions = exclusion.Split(',');
                var paramNames = exclusions.Select((s, i) => "@Parameter" + i.ToString(Thread.CurrentThread.CurrentCulture).Trim()).ToArray();
                var inClause = string.Join(",", paramNames);

                sql = string.Format(Thread.CurrentThread.CurrentCulture, @"select * from scrud.mixerp_table_view where table_schema=@Schema AND table_name=@TableName AND column_name NOT IN({0}) ORDER BY ordinal_position ASC;", inClause);

                using (var command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@Schema", schema);
                    command.Parameters.AddWithValue("@TableName", tableName);

                    for (var i = 0; i < paramNames.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramNames[i], exclusions[i].Trim());
                    }

                    return DbOperation.GetDataTable(command);
                }
            }

            sql = "select * from scrud.mixerp_table_view where table_schema=@Schema AND table_name=@TableName ORDER BY ordinal_position ASC;";

            using (var command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Schema", schema);
                command.Parameters.AddWithValue("@TableName", tableName);

                return DbOperation.GetDataTable(command);
            }
        }
    }
}