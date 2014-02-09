/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Security
{
    public static class User
    {
        public static long SignIn(int officeId, string userName, string password, string browser, string remoteAddress, string remoteUser, string culture)
        {
            string sql = "SELECT * FROM office.sign_in(@OfficeId, @UserName, @Password, @Browser, @IPAddress, @RemoteUser, @Culture);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@OfficeId", officeId);
                command.Parameters.Add("@UserName", userName);
                command.Parameters.Add("@Password", password);
                command.Parameters.Add("@Browser", browser);
                command.Parameters.Add("@IPAddress", remoteAddress);
                command.Parameters.Add("@RemoteUser", remoteUser);
                command.Parameters.Add("@Culture", culture);

                return MixERP.Net.Common.Conversion.TryCastLong(MixERP.Net.DBFactory.DBOperations.GetScalarValue(command));
            }
        }

        public static DataTable GetSignInView(string userName)
        {
            string sql = "SELECT * FROM office.sign_in_view WHERE user_name=@UserName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@UserName", userName);

                return MixERP.Net.DBFactory.DBOperations.GetDataTable(command);
            }
        }
    }
}
