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
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Office;
using MixERP.Net.Common.Models.Policy;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Data;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class User
    {
        public static SignInView GetSignInView(long loginId)
        {
            SignInView view = new SignInView();

            const string sql = "SELECT * FROM office.sign_in_view WHERE login_id=@LoginId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@LoginId", loginId);

                using (DataTable table = DbOperation.GetDataTable(command))
                {
                    if (table != null && table.Rows.Count.Equals(1))
                    {
                        view = GetSignInView(table.Rows[0]);
                    }
                }
            }

            return view;
        }

        public static SignInView GetSignInView(DataRow row)
        {
            SignInView view = new SignInView();

            view.UserId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "user_id"));
            view.Role = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "role"));
            view.IsAdmin = Conversion.TryCastBoolean(DataRowHelper.GetColumnValue(row, "is_admin"));
            view.IsSystem = Conversion.TryCastBoolean(DataRowHelper.GetColumnValue(row, "is_system"));
            view.UserName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "user_name"));
            view.FullName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "full_name"));
            view.LogOnId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "login_id"));
            view.OfficeId = Conversion.TryCastInteger(DataRowHelper.GetColumnValue(row, "office_id"));
            view.Culture = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "culture"));
            view.Office = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office"));
            view.OfficeCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_code"));
            view.OfficeName = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "office_name"));
            view.Nickname = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "nick_name"));
            view.RegistrationDate = Conversion.TryCastDate(DataRowHelper.GetColumnValue(row, "registration_date"));
            view.RegistrationNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "registration_number"));
            view.PanNumber = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "pan_number"));
            view.AddressLine1 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_1"));
            view.AddressLine2 = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "address_line_2"));
            view.Street = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "street"));
            view.City = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "city"));
            view.State = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "state"));
            view.ZipCode = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "zip_code"));
            view.Country = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "country"));
            view.Phone = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "phone"));
            view.Fax = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "fax"));
            view.Email = Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "email"));
            view.Url = new Uri(Conversion.TryCastString(DataRowHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);

            return view;
        }

        public static long SignIn(int officeId, string userName, string password, string culture, bool remember, Page page)
        {
            if (page != null)
            {
                string remoteAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string remoteUser = HttpContext.Current.Request.ServerVariables["REMOTE_USER"];

                SignInResult result = SignIn(officeId, userName, Conversion.HashSha512(password, userName), page.Request.UserAgent, remoteAddress, remoteUser, culture);

                if (result.LoginId == 0)
                {
                    throw new MixERPException(result.Message);
                }
                return result.LoginId;
            }

            return 0;
        }

        private static SignInResult SignIn(int officeId, string userName, string password, string browser, string remoteAddress, string remoteUser, string culture)
        {
            SignInResult result = new SignInResult();

            const string sql = "SELECT * FROM office.sign_in(@OfficeId, @UserName, @Password, @Browser, @IPAddress, @RemoteUser, @Culture);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Browser", browser);
                command.Parameters.AddWithValue("@IPAddress", remoteAddress);
                command.Parameters.AddWithValue("@RemoteUser", remoteUser);
                command.Parameters.AddWithValue("@Culture", culture);

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