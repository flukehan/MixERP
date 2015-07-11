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

using System.Collections.Generic;
using System.Linq;
using MixERP.Net.Entities.Core;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class AccountHelper
    {
        public static bool AccountNumberExists(string catalog, string accountNumber)
        {
            const string sql = "SELECT * FROM core.accounts WHERE account_number=@0;";
            return Factory.Get<Account>(catalog, sql, accountNumber).Count().Equals(1);
        }

        public static string GetAccountNumberByAccountId(string catalog, long accountId)
        {
            const string sql = "SELECT account_number FROM core.accounts WHERE account_id=@0;";
            return Factory.Scalar<string>(catalog, sql, accountId);
        }

        public static long GetAccountIdByAccountNumber(string catalog, string accountNumber)
        {
            const string sql = "SELECT account_id FROM core.accounts WHERE account_number=@0;";
            return Factory.Scalar<long>(catalog, sql, accountNumber);
        }

        public static IEnumerable<Account> GetAccounts(string catalog)
        {
            const string sql = "SELECT * FROM core.accounts ORDER BY account_id;";
            return Factory.Get<Account>(catalog, sql);
        }

        public static IEnumerable<Account> GetChildAccounts(string catalog)
        {
            const string sql =
                "SELECT * FROM core.accounts WHERE is_transaction_node AND NOT sys_type OR account_master_id IN(10101, 10102) ORDER BY account_id;";
            return Factory.Get<Account>(catalog, sql);
        }

        public static IEnumerable<Account> GetNonConfidentialAccounts(string catalog)
        {
            const string sql = "SELECT * FROM core.accounts WHERE NOT confidential ORDER BY account_id;";
            return Factory.Get<Account>(catalog, sql);
        }

        public static IEnumerable<Account> GetNonConfidentialChildAccounts(string catalog)
        {
            const string sql =
                "SELECT * FROM core.accounts WHERE is_transaction_node AND NOT confidential AND (NOT sys_type OR account_master_id IN(10101, 10102));";
            return Factory.Get<Account>(catalog, sql);
        }

        public static bool IsCashAccount(string catalog, long accountId)
        {
            const string sql = "SELECT * FROM core.accounts WHERE account_master_id=10101 AND account_id=@0;";
            return Factory.Get<Account>(catalog, sql, accountId).Count().Equals(1);
        }

        public static bool IsCashAccount(string catalog, string accountNumber)
        {
            const string sql = "SELECT * FROM core.accounts WHERE account_master_id=10101 AND account_number=@0;";
            return Factory.Get<Account>(catalog, sql, accountNumber).Count().Equals(1);
        }
    }
}