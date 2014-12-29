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
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.Finance.Data.Reports
{
    public static class AccountStatement
    {
        public static DataTable GetAccountOverview(string accountNumber)
        {
            const string sql = "SELECT * FROM core.account_view WHERE account_number=@AccountNumber;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                return DbOperation.GetDataTable(command);
            }
        }

        public static DataTable GetAccountStatement(DateTime from, DateTime to, int userId, string accountNumber, int officeId)
        {
            const string sql = "SELECT * FROM transactions.get_account_statement(@From::date, @To::date, @UserId, core.get_account_id_by_account_number(@AccountNumber), @OfficeId) ORDER BY id;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@From", from);
                command.Parameters.AddWithValue("@To", to);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@OfficeId", officeId);

                return DbOperation.GetDataTable(command);
            }
        }
    }
}