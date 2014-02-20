/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;
using System.Threading;
using MixERP.Net.Common;
using Npgsql;

namespace MixERP.Net.DBFactory
{
    public static class FormHelper
    {
        public static DataTable GetView(string tableSchema, string tableName, string orderBy, int limit, int offset)
        {
            var sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy LIMIT @Limit OFFSET @Offset;";

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

                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetTable(string tableSchema, string tableName)
        {
            var sql = "SELECT * FROM @TableSchema.@TableName;";
            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", Sanitizer.SanitizeIdentifierName(tableName));
                command.CommandText = sql;

                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValues)
        {
            if (string.IsNullOrWhiteSpace(columnNames))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(columnValues))
            {
                return null;
            }

            var columns = columnNames.Split(',');
            var values = columnValues.Split(',');

            if (!columns.Length.Equals(values.Length))
            {
                return null;
            }

            var counter = 0;
            var sql = "SELECT * FROM @TableSchema.@TableName WHERE ";

            foreach (var column in columns)
            {
                if (!counter.Equals(0))
                {
                    sql += " AND ";
                }

                sql += Sanitizer.SanitizeIdentifierName(column.Trim()) + " = @" + Sanitizer.SanitizeIdentifierName(column.Trim());

                counter++;
            }

            sql += ";";

            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", Sanitizer.SanitizeIdentifierName(tableName));


                command.CommandText = sql;

                counter = 0;
                foreach (var column in columns)
                {
                    command.Parameters.AddWithValue("@" + Sanitizer.SanitizeIdentifierName(column.Trim()), values[counter]);
                    counter++;
                }


                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValuesLike, int limit)
        {
            if (columnNames == null)
            {
                columnNames = string.Empty;
            }

            if (columnValuesLike == null)
            {
                columnValuesLike = string.Empty;
            }

            var columns = columnNames.Split(',');
            var values = columnValuesLike.Split(',');

            if (!columns.Length.Equals(values.Length))
            {
                return null;
            }

            var counter = 0;
            var sql = "SELECT * FROM @TableSchema.@TableName ";

            foreach (var column in columns)
            {
                if (!string.IsNullOrWhiteSpace(column))
                {
                    if (counter.Equals(0))
                    {
                        sql += " WHERE ";
                    }
                    else
                    {
                        sql += " AND ";
                    }

                    sql += " lower(" + Sanitizer.SanitizeIdentifierName(column.Trim()) + "::text) LIKE @" + Sanitizer.SanitizeIdentifierName(column.Trim());
                    counter++;
                }
            }

            sql += " LIMIT @Limit;";

            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", Sanitizer.SanitizeIdentifierName(tableName));


                command.CommandText = sql;

                counter = 0;
                foreach (var column in columns)
                {
                    if (!string.IsNullOrWhiteSpace(column))
                    {
                        command.Parameters.AddWithValue(Sanitizer.SanitizeIdentifierName(column.Trim()), "%" + values[counter].ToLower(Thread.CurrentThread.CurrentCulture) + "%");
                        counter++;
                    }
                }

                command.Parameters.AddWithValue("@Limit", limit);

                return DbOperations.GetDataTable(command);
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

                return Conversion.TryCastInteger(DbOperations.GetScalarValue(command));
            }
        }
    }
}
