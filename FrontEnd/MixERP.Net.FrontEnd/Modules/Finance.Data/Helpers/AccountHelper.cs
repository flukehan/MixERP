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
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class AccountHelper
    {
        public static bool AccountNumberExists(string accountNumber)
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE account_number=@0;", accountNumber).Count().Equals(1);
        }

        public static string GetAccountNumberByAccountId(long accountId)
        {
            return Factory.Scalar<string>("SELECT account_number FROM core.accounts WHERE account_id=@0;", accountId);
        }

        public static long GetAccountIdByAccountNumber(string accountNumber)
        {
            return Factory.Scalar<long>("SELECT account_id FROM core.accounts WHERE account_number=@0;", accountNumber);
        }

        public static IEnumerable<Account> GetAccounts()
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts ORDER BY account_id;");
        }

        public static IEnumerable<Account> GetChildAccounts()
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE is_transaction_node AND NOT sys_type OR account_master_id IN(10101, 10102) ORDER BY account_id;");
        }

        public static IEnumerable<Account> GetNonConfidentialAccounts()
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE NOT confidential ORDER BY account_id;");
        }

        public static IEnumerable<Account> GetNonConfidentialChildAccounts()
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE is_transaction_node AND NOT confidential AND (NOT sys_type OR account_master_id IN(10101, 10102));");
        }

        public static bool IsCashAccount(long accountId)
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE account_master_id=10101 AND account_id=@0;", accountId).Count().Equals(1);
        }

        public static bool IsCashAccount(string accountNumber)
        {
            return Factory.Get<Account>("SELECT * FROM core.accounts WHERE account_master_id=10101 AND account_id=@0;", accountNumber).Count().Equals(1);
        }
    }
}