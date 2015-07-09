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
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace MixERP.Net.WebControls.ScrudFactory.Data
{
    public static class FormHelper
    {
        public static bool DeleteRecord(string catalog, string tableSchema, string tableName, string keyColumn, string keyColumnValue)
        {
            string sql = "DELETE FROM @TableSchema.@TableName WHERE @KeyColumn=@KeyValue";

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@KeyColumn", DbFactory.Sanitizer.SanitizeIdentifierName(keyColumn));
                command.CommandText = sql;

                command.Parameters.AddWithValue("@KeyValue", keyColumnValue);

                try
                {
                    return DbOperation.ExecuteNonQuery(catalog, command);
                }
                catch (NpgsqlException ex)
                {
                    Log.Warning("{Sql}/{Exception}.", sql, ex);
                    throw new MixERPException(ex.Message, ex);
                }
            }
        }

        public static DataTable GetTable(string catalog, string tableSchema, string tableName, string orderBy)
        {
            var sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy ASC;";
            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", DbFactory.Sanitizer.SanitizeIdentifierName(orderBy));
                command.CommandText = sql;

                return DbOperation.GetDataTable(catalog, command);
            }
        }

        public static DataTable GetTable(string catalog, string tableSchema, string tableName, string columnNames, string columnValues, string orderBy)
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

                sql += DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim()) + " = @" + DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim());

                counter++;
            }

            sql += " ORDER BY @OrderBy ASC;";

            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", DbFactory.Sanitizer.SanitizeIdentifierName(orderBy));

                command.CommandText = sql;

                counter = 0;
                foreach (var column in columns)
                {
                    command.Parameters.AddWithValue("@" + DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim()), values[counter]);
                    counter++;
                }

                return DbOperation.GetDataTable(catalog, command);
            }
        }

        public static DataTable GetTable(string catalog, string tableSchema, string tableName, string columnNames, string columnValuesLike, int limit, string orderBy)
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

                    sql += " lower(" + DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim()) + "::text) LIKE @" + DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim());
                    counter++;
                }
            }

            sql += " ORDER BY @OrderBy ASC LIMIT @Limit;";

            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", DbFactory.Sanitizer.SanitizeIdentifierName(orderBy));

                command.CommandText = sql;

                counter = 0;
                foreach (var column in columns)
                {
                    if (!string.IsNullOrWhiteSpace(column))
                    {
                        command.Parameters.AddWithValue("@" + DbFactory.Sanitizer.SanitizeIdentifierName(column.Trim()), "%" + values[counter].ToLower(Thread.CurrentThread.CurrentCulture) + "%");
                        counter++;
                    }
                }

                command.Parameters.AddWithValue("@Limit", limit);

                return DbOperation.GetDataTable(catalog, command);
            }
        }

        public static int GetTotalRecords(string catalog, string tableSchema, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM @TableSchema.@TableName";
            using (var command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));

                command.CommandText = sql;

                return Conversion.TryCastInteger(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static DataTable GetView(string catalog, string tableSchema, string tableName, string orderBy, int limit, int offset)
        {
            string sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy ASC LIMIT @Limit OFFSET @Offset;";

            if (limit < 0)
            {
                sql = "SELECT * FROM @TableSchema.@TableName ORDER BY @OrderBy ASC;";
            }

            using (var command = new NpgsqlCommand())
            {
                //We are 100% sure that the following parameters do not come from user input.
                //Having said that, it is still nice to sanitize the objects before sending it to the database server.
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@OrderBy", DbFactory.Sanitizer.SanitizeIdentifierName(orderBy));

                if (limit >= 0)
                {
                    sql = sql.Replace("@Limit", Conversion.TryCastString(limit));
                    sql = sql.Replace("@Offset", Conversion.TryCastString(offset));
                }

                command.CommandText = sql;

                return DbOperation.GetDataTable(catalog, command);
            }
        }

        public static long InsertRecord(string catalog, int userId, string tableSchema, string tableName, string keyColumnName, Collection<KeyValuePair<string, object>> data, string imageColumn)
        {
            if (data == null)
            {
                return 0;
            }

            string columns = string.Empty;
            string columnParameters = string.Empty;

            int counter = 0;

            foreach (KeyValuePair<string, object> pair in data)
            {
                counter++;

                if (counter.Equals(1))
                {
                    columns += DbFactory.Sanitizer.SanitizeIdentifierName(pair.Key);
                    columnParameters += "@" + pair.Key;
                }
                else
                {
                    columns += ", " + DbFactory.Sanitizer.SanitizeIdentifierName(pair.Key);
                    columnParameters += ", @" + pair.Key;
                }
            }

            string sql = "INSERT INTO @TableSchema.@TableName(" + columns + ", audit_user_id) SELECT " + columnParameters +
                         ", @AuditUserId RETURNING @PrimaryKey;";
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@PrimaryKey", DbFactory.Sanitizer.SanitizeIdentifierName(keyColumnName));

                command.CommandText = sql;

                foreach (KeyValuePair<string, object> pair in data)
                {
                    if (pair.Value == null)
                    {
                        command.Parameters.AddWithValue("@" + pair.Key, DBNull.Value);
                    }
                    else
                    {
                        if (pair.Key.Equals(imageColumn))
                        {
                            using (FileStream stream = new FileStream(pair.Value.ToString(), FileMode.Open, FileAccess.Read))
                            {
                                using (BinaryReader reader = new BinaryReader(new BufferedStream(stream)))
                                {
                                    byte[] byteArray = reader.ReadBytes(Convert.ToInt32(stream.Length));
                                    command.Parameters.AddWithValue("@" + pair.Key, byteArray);
                                }
                            }
                        }
                        else
                        {
                            if (pair.Value == null)
                            {
                                command.Parameters.AddWithValue("@" + pair.Key, DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                            }
                        }
                    }
                }

                command.Parameters.AddWithValue("@AuditUserId", userId);

                try
                {
                    return Conversion.TryCastLong(DbOperation.GetScalarValue(catalog, command));
                }
                catch (NpgsqlException ex)
                {
                    Log.Warning("{Sql}/{Data}/{Exception}.", sql, data, ex);
                    throw new MixERPException(ex.Message, ex, ex.ConstraintName);
                }
            }
        }

        public static bool UpdateRecord(string catalog, int userId, string tableSchema, string tableName, Collection<KeyValuePair<string, object>> data, string keyColumn, string keyColumnValue, string imageColumn, string[] exclusion)
        {
            if (data == null)
            {
                return false;
            }

            string columns = string.Empty;

            int counter = 0;

            //Adding the current user to the column collection.
            KeyValuePair<string, object> auditUserId = new KeyValuePair<string, object>("audit_user_id", userId);
            data.Add(auditUserId);


            foreach (KeyValuePair<string, object> pair in data)
            {
                if (!exclusion.Contains(pair.Key.ToUpperInvariant()))
                {
                    counter++;

                    if (counter.Equals(1))
                    {
                        columns += DbFactory.Sanitizer.SanitizeIdentifierName(pair.Key) + "=@" + pair.Key;
                    }
                    else
                    {
                        columns += ", " + DbFactory.Sanitizer.SanitizeIdentifierName(pair.Key) + "=@" + pair.Key;
                    }
                }
            }

            string sql = "UPDATE @TableSchema.@TableName SET " + columns + ", audit_ts=NOW() WHERE @KeyColumn=@KeyValue;";

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                sql = sql.Replace("@TableSchema", DbFactory.Sanitizer.SanitizeIdentifierName(tableSchema));
                sql = sql.Replace("@TableName", DbFactory.Sanitizer.SanitizeIdentifierName(tableName));
                sql = sql.Replace("@KeyColumn", DbFactory.Sanitizer.SanitizeIdentifierName(keyColumn));

                command.CommandText = sql;

                foreach (KeyValuePair<string, object> pair in data)
                {
                    if (!exclusion.Contains(pair.Key.ToUpperInvariant()))
                    {
                        if (pair.Value == null)
                        {
                            command.Parameters.AddWithValue("@" + pair.Key, DBNull.Value);
                        }
                        else
                        {
                            if (pair.Key.Equals(imageColumn))
                            {
                                FileStream stream = new FileStream(pair.Value.ToString(), FileMode.Open, FileAccess.Read);
                                try
                                {
                                    using (BinaryReader reader = new BinaryReader(new BufferedStream(stream)))
                                    {
                                        byte[] byteArray = reader.ReadBytes(Convert.ToInt32(stream.Length));
                                        command.Parameters.AddWithValue("@" + pair.Key, byteArray);
                                    }
                                }
                                finally
                                {
                                    stream.Close();
                                }
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                            }
                        }
                    }
                }

                command.Parameters.AddWithValue("@KeyValue", keyColumnValue);

                try
                {
                    return DbOperation.ExecuteNonQuery(catalog, command);
                }
                catch (NpgsqlException ex)
                {
                    Log.Warning("{Sql}/{Data}/{Exception}.", sql, data, ex);
                    throw new MixERPException(ex.Message, ex, ex.ConstraintName);
                }
            }
        }
    }
}