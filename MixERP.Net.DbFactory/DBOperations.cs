/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using Npgsql;
using System;
using System.Data;
using System.Globalization;

namespace MixERP.Net.DBFactory
{
    public static class DBOperations
    {
        public static bool IsServerAvailable()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
                {
                    connection.Open();
                }

                return true;
            }
            catch (NpgsqlException)
            {
                //swallow exception
            }

            return false;
        }

        [CLSCompliant(false)]
        public static bool ExecuteNonQuery(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
                {
                    try
                    {
                        command.Connection = connection;
                        connection.Open();

                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return false;
        }

        [CLSCompliant(false)]
        public static object GetScalarValue(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
                {
                    command.Connection = connection;
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataTable GetDataTable(NpgsqlCommand command, string connectionString)
        {
            if (command != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    command.Connection = connection;

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            dataTable.Locale = CultureInfo.InvariantCulture;
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataTable GetDataTable(NpgsqlCommand command)
        {
            return GetDataTable(command, DBConnection.ConnectionString());
        }



        [CLSCompliant(false)]
        public static NpgsqlDataReader GetDataReader(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    return command.ExecuteReader();
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataView GetDataView(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (DataView view = new DataView(GetDataTable(command)))
                {
                    return view;
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static NpgsqlDataAdapter GetDataAdapter(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DBConnection.ConnectionString()))
                {
                    command.Connection = connection;

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        return adapter;
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataSet GetDataSet(NpgsqlCommand command)
        {
            if (command != null)
            {
                using (NpgsqlDataAdapter adapter = GetDataAdapter(command))
                {
                    using (DataSet set = new DataSet())
                    {
                        adapter.Fill(set);
                        set.Locale = CultureInfo.CurrentUICulture;
                        return set;
                    }
                }
            }

            return null;
        }
    }
}
