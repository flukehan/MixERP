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

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using MixERP.Net.Common.Helpers;
using MixERP.Net.DbFactory.Resources;
using Npgsql;

namespace MixERP.Net.DBFactory
{
    public class DbOperation
    {
        public EventHandler<NpgsqlNoticeEventArgs> Listen;

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
                                dataTable.Locale = Thread.CurrentThread.CurrentCulture;
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

        public void ListenNonQuery(NpgsqlCommand command)
        {
            if (command != null)
            {
                if (ValidateCommand(command))
                {
                    ThreadStart queryStart = delegate
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString()))
                        {
                            command.Connection = connection;
                            connection.Notice += Connection_Notice;
                            connection.Open();
                            command.ExecuteNonQueryAsync();
                        }
                    };

                    queryStart += () => { Thread.Sleep(15000); };

                    Thread query = new Thread(queryStart) {IsBackground = true};
                    query.Start();
                }
            }
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

        private static bool ValidateCommand(NpgsqlCommand command)
        {
            return ValidateParameters(command);
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
                    throw new InvalidOperationException(string.Format(LocalizationHelper.GetCurrentCulture(), Warnings.InvalidParameterName, npgsqlParameter.ParameterName));
                }
            }

            return true;
        }

        private void Connection_Notice(object sender, NpgsqlNoticeEventArgs e)
        {
            var listen = this.Listen;

            if (listen != null)
            {
                listen(this, e);
            }
        }
    }
}