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

using MixERP.Net.Common;
using Npgsql;

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

namespace MixERP.Net.DbFactory
{
    public static class FormHelper
    {
        public static DataTable GetView(string tableSchema, string tableName, string orderBy, int limit, int offset)
        {
            var sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy ASC LIMIT @Limit OFFSET @Offset;";

            using (var command = new NpgsqlCommand())
            {
                //We are 100% sure that the following parameters do not come from user input.
                //Having said that, it is nice to sanitize the objects before sending it to the database server.
                sql = sql.Replace("@TableSchema", Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", Sanitizer.SanitizeIdentifierName(orderBy));
                sql = sql.Replace("@Limit", Conversion.TryCastString(limit));
                sql = sql.Replace("@Offset", Conversion.TryCastString(offset));
                command.CommandText = sql;

                return DbOperation.GetDataTable(command);
            }
        }

        public static int GetTotalRecords(string tableSchema, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM @TableSchema.@TableName";
            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", Sanitizer.SanitizeIdentifierName(tableName));

                command.CommandText = sql;

                return Conversion.TryCastInteger(DbOperation.GetScalarValue(command));
            }
        }
    }
}