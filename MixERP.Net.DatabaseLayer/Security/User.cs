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

        public static MixERP.Net.Common.Models.Office.SignInView GetLastSignInView(string userName)
        {
            MixERP.Net.Common.Models.Office.SignInView view = new Common.Models.Office.SignInView();

            string sql = "SELECT * FROM office.sign_in_view WHERE user_name=@UserName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@UserName", userName);

                using (DataTable table = MixERP.Net.DBFactory.DBOperations.GetDataTable(command))
                {
                    if (table != null && table.Rows.Count.Equals(1))
                    {
                        view = GetSignInView(table.Rows[0]);
                    }
                }
            }

            return view;
        }

        public static MixERP.Net.Common.Models.Office.SignInView GetSignInView(DataRow row)
        {
            MixERP.Net.Common.Models.Office.SignInView view = new Common.Models.Office.SignInView();

            view.UserId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "user_id"));
            view.Role = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "role"));
            view.IsAdmin = MixERP.Net.Common.Conversion.TryCastBoolean(Helpers.ConversionHelper.GetColumnValue(row, "is_admin"));
            view.IsSystem = MixERP.Net.Common.Conversion.TryCastBoolean(Helpers.ConversionHelper.GetColumnValue(row, "is_system"));
            view.UserName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "user_name"));
            view.FullName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "full_name"));
            view.LogOnId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "login_id"));
            view.OfficeId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "office_id"));
            view.Culture = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "culture"));
            view.Office = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "office"));
            view.OfficeCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "office_code"));
            view.OfficeName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "office_name"));
            view.NickName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "nick_name"));
            view.RegistrationDate = MixERP.Net.Common.Conversion.TryCastDate(Helpers.ConversionHelper.GetColumnValue(row, "registration_date"));
            view.RegistrationNumber = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "registration_number"));
            view.PanNumber = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "pan_number"));
            view.AddressLine1 = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "address_line_1"));
            view.AddressLine2 = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "address_line_2"));
            view.Street = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "street"));
            view.City = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "city"));
            view.State = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "state"));
            view.ZipCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "zip_code"));
            view.Country = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "country"));
            view.Phone = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "phone"));
            view.Fax = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "fax"));
            view.Email = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "email"));
            view.Url = new Uri(MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "url")), UriKind.RelativeOrAbsolute);


            return view;
        }


    }
}
