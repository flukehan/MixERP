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
using System.Collections.Generic;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Office;
using Npgsql;

namespace MixERP.Net.FrontEnd.Data.Office
{
    public static class Offices
    {
        public static IEnumerable<DbGetOfficesResult> GetOffices()
        {
            return Factory.Get<DbGetOfficesResult>("SELECT * FROM office.get_offices();");
        }

        public static bool SaveOffice(string officeCode, string officeName, string nickName, DateTime registrationDate,
            string currencyCode,string currencySymbol, string currencyName, string hundredthName, string adminName, string userName,
            string password)
        {
            string sql =
                "SELECT * FROM office.add_office(@OfficeCode, @OfficeName, @NickName, @RegistrationDate, @CurrencyCode, @CurrencySymbol, @CurrencyName, @HundredthName, @AdminName, @UserName, @Password);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeCode", officeCode);
                command.Parameters.AddWithValue("@OfficeName", officeName);
                command.Parameters.AddWithValue("@NickName", nickName);
                command.Parameters.AddWithValue("@RegistrationDate", registrationDate);
                command.Parameters.AddWithValue("@CurrencyCode", currencyCode);
                command.Parameters.AddWithValue("@CurrencySymbol", currencySymbol);
                command.Parameters.AddWithValue("@CurrencyName", currencyName);
                command.Parameters.AddWithValue("@HundredthName", hundredthName);
                command.Parameters.AddWithValue("@AdminName", adminName);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                return DbOperation.ExecuteNonQuery(command);
            }
        }
    }
}