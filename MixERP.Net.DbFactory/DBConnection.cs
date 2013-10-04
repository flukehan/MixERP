/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MixERP.Net.Common;
using Npgsql;

namespace MixERP.Net.DBFactory
{
    public static class DBConnection
    {
        public static string ConnectionString()
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
            connectionStringBuilder.Host = ConfigurationManager.AppSettings["Server"];
            connectionStringBuilder.Database = ConfigurationManager.AppSettings["Database"];
            connectionStringBuilder.UserName = ConfigurationManager.AppSettings["UserId"];
            connectionStringBuilder.Password = ConfigurationManager.AppSettings["Password"];

            return connectionStringBuilder.ConnectionString;
        }
    }
}
