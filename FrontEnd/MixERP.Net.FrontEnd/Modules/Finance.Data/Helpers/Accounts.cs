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

using System.Data;
using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Accounts
    {
        public static bool AccountNumberExists(string accountNumber)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE account_number=@AccountNumber;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                return DbOperation.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static string GetAccountNumberByAccountId(long accountId)
        {
            const string sql = "SELECT account_number FROM core.accounts WHERE account_id=@AccountId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountId", accountId);

                return Conversion.TryCastString(DbOperation.GetScalarValue(command));
            }
        }

        public static DataTable GetAccounts()
        {
            return FormHelper.GetTable("core", "accounts", "account_id");
        }

        public static DataTable GetChildAccounts()
        {
            const string sql = "SELECT account_number, account_name FROM core.accounts WHERE NOT sys_type OR is_cash;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return DbOperation.GetDataTable(command);
            }
        }

        public static DataTable GetNonConfidentialAccounts()
        {
            return FormHelper.GetTable("core", "accounts", "confidential", "0", "account_id");
        }

        public static DataTable GetNonConfidentialChildAccounts()
        {
            const string sql = "SELECT account_number, account_name FROM core.accounts WHERE NOT confidential AND (NOT sys_type OR is_cash);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return DbOperation.GetDataTable(command);
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
    }
}