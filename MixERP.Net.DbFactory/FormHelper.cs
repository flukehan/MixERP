/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MixERP.Net.DBFactory
{
    public static class FormHelper
    {
        public static DataTable GetView(string tableSchema, string tableName, string orderBy, int limit, int offset)
        {
            string sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy LIMIT @Limit OFFSET @Offset;";

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                //We are 100% sure that the following parameters do not come from user input.
                //Having said that, it is nice to sanitize the objects before sending it to the database server.
                sql = sql.Replace("@TableSchema", DBFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DBFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", DBFactory.Sanitizer.SanitizeIdentifierName(orderBy));
                sql = sql.Replace("@Limit", MixERP.Net.Common.Conversion.TryCastString(limit));
                sql = sql.Replace("@Offset", MixERP.Net.Common.Conversion.TryCastString(offset));
                command.CommandText = sql;

                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
            }
        }

        public static DataTable GetTable(string tableSchema, string tableName)
        {
            string sql = "SELECT * FROM @TableSchema.@TableName;";
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DBFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DBFactory.Sanitizer.SanitizeIdentifierName(tableName));
                command.CommandText = sql;

                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
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

            string[] columns = columnNames.Split(',');
            string[] values = columnValues.Split(',');

            if (!columns.Length.Equals(values.Length))
            {
                return null;
            }

            int counter = 0;
            string sql = "SELECT * FROM @TableSchema.@TableName WHERE ";

            foreach (string column in columns)
            {
                if (!counter.Equals(0))
                {
                    sql += " AND ";
                }

                sql += DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim()) + " = @" + DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim());

                counter++;
            }

            sql += ";";

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DBFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DBFactory.Sanitizer.SanitizeIdentifierName(tableName));


                command.CommandText = sql;

                counter = 0;
                foreach (string column in columns)
                {
                    command.Parameters.Add(DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim()), values[counter]);
                    counter++;
                }


                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
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

            string[] columns = columnNames.Split(',');
            string[] values = columnValuesLike.Split(',');

            if (!columns.Length.Equals(values.Length))
            {
                return null;
            }

            int counter = 0;
            string sql = "SELECT * FROM @TableSchema.@TableName ";

            foreach (string column in columns)
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

                    sql += " lower(" + DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim()) + "::text) LIKE @" + DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim());
                    counter++;
                }
            }

            sql += " LIMIT @Limit;";

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DBFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DBFactory.Sanitizer.SanitizeIdentifierName(tableName));


                command.CommandText = sql;

                counter = 0;
                foreach (string column in columns)
                {
                    if (!string.IsNullOrWhiteSpace(column))
                    {
                        command.Parameters.Add(DBFactory.Sanitizer.SanitizeIdentifierName(column.Trim()), "%" + values[counter].ToLower(System.Threading.Thread.CurrentThread.CurrentCulture) + "%");
                        counter++;
                    }
                }

                command.Parameters.Add("@Limit", limit);

                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
            }
        }

        public static int GetTotalRecords(string tableSchema, string tableName)
        {
            string sql = "SELECT COUNT(*) FROM @TableSchema.@TableName";
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DBFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DBFactory.Sanitizer.SanitizeIdentifierName(tableName));

                command.CommandText = sql;

                return MixERP.Net.Common.Conversion.TryCastInteger(MixERP.Net.DBFactory.DBOperations.GetScalarValue(command));
            }
        }
    }
}
