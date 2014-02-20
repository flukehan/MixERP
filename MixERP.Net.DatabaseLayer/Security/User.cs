/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Data;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Office;
using MixERP.Net.DatabaseLayer.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Security
{
    public static class User
    {
        public static long SignIn(int officeId, string userName, string password, string browser, string remoteAddress, string remoteUser, string culture)
        {
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

                return Conversion.TryCastLong(DbOperations.GetScalarValue(command));
            }
        }

        public static SignInView GetLastSignInView(string userName)
        {
            SignInView view = new SignInView();

            const string sql = "SELECT * FROM office.sign_in_view WHERE user_name=@UserName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                using (DataTable table = DbOperations.GetDataTable(command))
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

            view.UserId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "user_id"));
            view.Role = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "role"));
            view.IsAdmin = Conversion.TryCastBoolean(ConversionHelper.GetColumnValue(row, "is_admin"));
            view.IsSystem = Conversion.TryCastBoolean(ConversionHelper.GetColumnValue(row, "is_system"));
            view.UserName = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "user_name"));
            view.FullName = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "full_name"));
            view.LogOnId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "login_id"));
            view.OfficeId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "office_id"));
            view.Culture = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "culture"));
            view.Office = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "office"));
            view.OfficeCode = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "office_code"));
            view.OfficeName = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "office_name"));
            view.Nickname = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "nick_name"));
            view.RegistrationDate = Conversion.TryCastDate(ConversionHelper.GetColumnValue(row, "registration_date"));
            view.RegistrationNumber = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "registration_number"));
            view.PanNumber = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "pan_number"));
            view.AddressLine1 = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "address_line_1"));
            view.AddressLine2 = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "address_line_2"));
            view.Street = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "street"));
            view.City = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "city"));
            view.State = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "state"));
            view.ZipCode = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "zip_code"));
            view.Country = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "country"));
            view.Phone = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "phone"));
            view.Fax = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "fax"));
            view.Email = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "email"));
            view.Url = new Uri(Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);


            return view;
        }


    }
}
