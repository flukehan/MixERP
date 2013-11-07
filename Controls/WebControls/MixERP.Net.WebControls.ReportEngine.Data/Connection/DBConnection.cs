using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Npgsql;

namespace MixERP.Net.WebControls.ReportEngine.Data.Connection
{
    public static class DBConnection
    {
        public static string ReportConnectionString()
        {
            string host = ConfigurationManager.AppSettings["Server"];
            string database = ConfigurationManager.AppSettings["Database"];
            string userName = MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("DbLoginName");
            string password = MixERP.Net.Common.Helpers.ConfigurationHelper.GetReportParameter("DbPassword");

            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
            connectionStringBuilder.Host = host;
            connectionStringBuilder.Database = database;
            connectionStringBuilder.UserName = userName;
            connectionStringBuilder.Password = password;

            return connectionStringBuilder.ConnectionString;
        }
    }
}
