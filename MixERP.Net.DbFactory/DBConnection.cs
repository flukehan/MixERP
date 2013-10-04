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

namespace MixERP.Net.DBFactory
{
    public static class DBConnection
    {
        public static string ConnectionString()
        {
            Npgsql.NpgsqlConnectionStringBuilder connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder();
            connectionStringBuilder.Host = Conversion.TryCastString(ConfigurationManager.AppSettings["Server"]);
            connectionStringBuilder.Database = Conversion.TryCastString(ConfigurationManager.AppSettings["Database"]);
            connectionStringBuilder.UserName = Conversion.TryCastString(ConfigurationManager.AppSettings["UserId"]);
            connectionStringBuilder.Password = Conversion.TryCastString(ConfigurationManager.AppSettings["Password"]);

            return connectionStringBuilder.ConnectionString;
        }
    }
}
