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
using MixERP.Net.Common.Helpers;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

namespace MixERP.Net.Core.Modules.BackOffice.Data
{
    public class ReportWriter
    {
        public static DataTable GetTable(string catalog, string sql, Collection<ReportParameter> parameters)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(catalog)))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    foreach (ReportParameter parameter in parameters)
                    {
                        if (parameter != null && !string.IsNullOrWhiteSpace(parameter.Name))
                        {
                            command.Parameters.AddWithValue(parameter.Name, parameter.TestValue);
                        }
                    }

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            dataTable.Locale = Thread.CurrentThread.CurrentCulture;
                            dataTable.TableName = "Report";
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
        }

        public static string GetConnectionString(string catalog)
        {
            CatalogHelper.ValidateCatalog(catalog);

            string host = ConfigurationHelper.GetDbServerParameter("Server");
            int port = Conversion.TryCastInteger(ConfigurationHelper.GetDbServerParameter("Port"));
            string userName = ConfigurationHelper.GetDbServerParameter("ReportUserId");
            string password = ConfigurationHelper.GetDbServerParameter("ReportUserPassword");


            if (string.IsNullOrWhiteSpace(host))
            {
                return DbFactory.DbConnection.GetConnectionString(catalog);
            }

            return DbFactory.DbConnection.GetConnectionString(host, catalog, userName, password, port);
        }

        public class DataSource
        {
            public string Query { get; set; }
            public string RunningTotalFieldIndices { get; set; }
            public string RunningTotalTextColumnIndex { get; set; }
            public IEnumerable<ReportParameter> Parameters { get; set; }
        }

        public class ReportParameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string TestValue { get; set; }
        }

        public class Grid
        {
            public int DataSourceIndex { get; set; }
            public string CssClass { get; set; }
            public string Style { get; set; }
        }        
    }
}