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

using System.Linq;
using System.Web;
using MixERP.Net.Entities.Office;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class User
    {
        public static bool ChangePassword(string catalog, string userName, string currentPassword, string newPassword)
        {
            try
            {
                return Factory.Scalar<bool>(catalog,
                    "SELECT * FROM policy.change_password(@0::text, @1::text, @2::text);",
                    userName, currentPassword, newPassword);
            }
            catch (NpgsqlException ex)
            {
                throw new MixERPException(ex.Message, ex);
            }
        }

        public static GlobalLogin GetGloblalLogin(long globalLoginId)
        {
            const string sql = "SELECT * FROM public.global_logins WHERE global_login_id=@0;";
            GlobalLogin login = Factory.Get<GlobalLogin>(Factory.MetaDatabase, sql, globalLoginId).FirstOrDefault();

            if (login != null)
            {
                string catalog = login.Catalog;

                SignInView view =
                    Factory.Get<SignInView>(catalog, "SELECT * FROM office.sign_in_view WHERE login_id=@0;",
                        login.LoginId).FirstOrDefault();

                if (view != null)
                {
                    login.View = view;
                    return login;
                }
            }

            return null;
        }

        public static long SignIn(string catalog, int officeId, string userName, string password, string culture,
            bool remember,
            string challenge, HttpContext context)
        {
            if (context != null)
            {
                string remoteAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string remoteUser = HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                DbSignInResult result = SignIn(catalog, officeId, userName, password, context.Request.UserAgent,
                    remoteAddress,
                    remoteUser, culture, challenge);

                if (result.LoginId == 0)
                {
                    throw new MixERPException(result.Message);
                }


                long globalLoginId = GetGlobalLogin(catalog, result.LoginId);

                return globalLoginId;
            }

            return 0;
        }

        private static long GetGlobalLogin(string catalog, long loginId)
        {
            const string sql =
                "INSERT INTO public.global_logins(catalog, login_id) SELECT @0::text, @1::bigint RETURNING global_login_id;";

            return Factory.Scalar<long>(Factory.MetaDatabase, sql, catalog, loginId);
        }

        private static DbSignInResult SignIn(string catalog, int officeId, string userName, string password,
            string browser,
            string remoteAddress, string remoteUser, string culture, string challenge)
        {
            const string sql =
                "SELECT * FROM office.sign_in(@0::public.integer_strict, @1::text, @2::text, @3::text, @4::text, @5::text, @6::text, @7::text);";

            return
                Factory.Get<DbSignInResult>(catalog, sql, officeId, userName, password, browser, remoteAddress,
                    remoteUser,
                    culture, challenge).FirstOrDefault();
        }
    }
}