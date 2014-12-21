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

using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Accounts
    {
        public static DataTable GetAccounts()
        {
            return FormHelper.GetTable("core", "accounts", "account_id");
        }

        public static bool AccountNumberExists(string accountNumber)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE account_number=@AccountNumber;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                return DbOperation.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static bool IsCashAccount(int accountId)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_id=@AccountId;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountId", accountId);

                return DbOperation.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static bool IsCashAccount(string accountNumber)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_number=@AccountNumber;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                return DbOperation.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static DataTable GetNonConfidentialAccounts()
        {
            return FormHelper.GetTable("core", "accounts", "confidential", "0", "account_id");
        }

        public static DataTable GetChildAccounts()
        {
            return FormHelper.GetTable("core", "account_view", "has_child", "0", "account_id");
        }

        public static DataTable GetNonConfidentialChildAccounts()
        {
            return FormHelper.GetTable("core", "account_view", "has_child, confidential", "0, 0", "account_id");
        }
    }
}