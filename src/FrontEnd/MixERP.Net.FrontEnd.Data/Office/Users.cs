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
using System.Data;
using System.Linq;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Policy;
using MixERP.Net.Entities.Office;
using Npgsql;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class User
    {
        public static bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            try
            {
                return Factory.Scalar<bool>("SELECT * FROM policy.change_password(@0::text, @1::text, @2::text);", userName, currentPassword, newPassword);
            }
            catch (NpgsqlException ex)
            {
                throw new MixERPException(ex.Message, ex);
            }
        }

        public static SignInView GetSignInView(long loginId)
        {
            return Factory.Get<SignInView>("SELECT * FROM office.sign_in_view WHERE login_id=@0;", loginId).FirstOrDefault();
        }

        public static long SignIn(int officeId, string userName, string password, string culture, bool remember, string challenge, HttpContext context)
        {
            if (context != null)
            {
                string remoteAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string remoteUser = HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                SignInResult result = SignIn(officeId, userName, password, context.Request.UserAgent, remoteAddress, remoteUser, culture, challenge);

                if (result.LoginId == 0)
                {
                    throw new MixERPException(result.Message);
                }
                return result.LoginId;
            }

            return 0;
        }


        [Obsolete("Replace DataTable with a Poco Class.")]
        private static SignInResult SignIn(int officeId, string userName, string password, string browser, string remoteAddress, string remoteUser, string culture, string challenge)
        {
            SignInResult result = new SignInResult();

            const string sql = "SELECT * FROM office.sign_in(@OfficeId::public.integer_strict, @UserName::text, @Password::text, @Browser::text, @IPAddress::text, @RemoteUser::text, @Culture::text, @Challenge::text);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Browser", browser);
                command.Parameters.AddWithValue("@IPAddress", remoteAddress);
                command.Parameters.AddWithValue("@RemoteUser", remoteUser);
                command.Parameters.AddWithValue("@Culture", culture);
                command.Parameters.AddWithValue("@Challenge", challenge);

                using (DataTable table = DbOperation.GetDataTable(command))
                {
                    if (table.Rows != null && table.Rows.Count.Equals(1))
                    {
                        result.LoginId = Conversion.TryCastLong(table.Rows[0]["login_id"]);
                        result.Message = Conversion.TryCastString(table.Rows[0]["message"]);
                    }
                }
            }

            return result;
        }
    }
}