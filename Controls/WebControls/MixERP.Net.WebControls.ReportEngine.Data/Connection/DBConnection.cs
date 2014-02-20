/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Configuration;
using MixERP.Net.Common.Helpers;
using Npgsql;

namespace MixERP.Net.WebControls.ReportEngine.Data.Connection
{
    public static class DbConnection
    {
        public static string ReportConnectionString()
        {
            string host = ConfigurationManager.AppSettings["Server"];
            string database = ConfigurationManager.AppSettings["Database"];
            string userName = ConfigurationHelper.GetReportParameter("DbLoginName");
            string password = ConfigurationHelper.GetReportParameter("DbPassword");

            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
            connectionStringBuilder.Host = host;
            connectionStringBuilder.Database = database;
            connectionStringBuilder.UserName = userName;
            connectionStringBuilder.Password = password;

            return connectionStringBuilder.ConnectionString;
        }
    }
}
