/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MixERP.Net.DBFactory
{
    public static class DbOperations
    {
        public static bool IsServerAvailable()
        {
            try
            {
                using (var connection = new NpgsqlConnection(DbConnection.ConnectionString()))
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
                if (ValidateCommand(command))
                {
                    using (var connection = new NpgsqlConnection(DbConnection.ConnectionString()))
                    {
                        command.Connection = connection;
                        connection.Open();

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool ValidateCommand(NpgsqlCommand command)
        {
            return ValidateParameters(command);
        }

        private static Collection<string> GetCommandTextParameterCollection(string commandText)
        {
            var parameters = new Collection<string>();

            foreach (Match match in Regex.Matches(commandText, @"@(\w+)"))
            {
                parameters.Add(match.Value);
            }

            return parameters;

        }

        private static bool ValidateParameters(NpgsqlCommand command)
        {
            var commandTextParameters = GetCommandTextParameterCollection(command.CommandText);

            foreach (NpgsqlParameter npgsqlParameter in command.Parameters)
            {
                var match = false;

                foreach (var commandTextParameter in commandTextParameters)
                {
                    if (npgsqlParameter.ParameterName.Equals(commandTextParameter))
                    {
                        match = true;
                    }
                }

                if (!match)
                {
                    throw new InvalidOperationException("Invalid Npgsql parameter name '" + npgsqlParameter.ParameterName + "'. Make sure that the parameter name matches with your command text.");
                }
            }

            return true;
        }


        [CLSCompliant(false)]
        public static object GetScalarValue(NpgsqlCommand command)
        {
            if (command != null)
            {

                if (ValidateCommand(command))
                {
                    using (var connection = new NpgsqlConnection(DbConnection.ConnectionString()))
                    {
                        command.Connection = connection;
                        connection.Open();
                        return command.ExecuteScalar();
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataTable GetDataTable(NpgsqlCommand command, string connectionString)
        {
            if (command != null)
            {
                if (ValidateCommand(command))
                {
                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        command.Connection = connection;

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            using (var dataTable = new DataTable())
                            {
                                dataTable.Locale = CultureInfo.InvariantCulture;
                                adapter.Fill(dataTable);
                                return dataTable;
                            }
                        }
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataTable GetDataTable(NpgsqlCommand command)
        {
            return GetDataTable(command, DbConnection.ConnectionString());
        }



        [CLSCompliant(false)]
        public static NpgsqlDataReader GetDataReader(NpgsqlCommand command)
        {
            if (command != null)
            {
                if (ValidateCommand(command))
                {
                    using (var connection = new NpgsqlConnection(DbConnection.ConnectionString()))
                    {
                        command.Connection = connection;
                        command.Connection.Open();
                        return command.ExecuteReader();
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataView GetDataView(NpgsqlCommand command)
        {
            if (ValidateCommand(command))
            {
                using (var view = new DataView(GetDataTable(command)))
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
                if (ValidateCommand(command))
                {
                    using (var connection = new NpgsqlConnection(DbConnection.ConnectionString()))
                    {
                        command.Connection = connection;

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            return adapter;
                        }
                    }
                }
            }

            return null;
        }

        [CLSCompliant(false)]
        public static DataSet GetDataSet(NpgsqlCommand command)
        {
            if (ValidateCommand(command))
            {
                using (var adapter = GetDataAdapter(command))
                {
                    using (var set = new DataSet())
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
