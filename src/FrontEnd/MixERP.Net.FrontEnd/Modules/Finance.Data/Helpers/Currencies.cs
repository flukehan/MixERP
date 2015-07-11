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
    public static class Currencies
    {
        public static IEnumerable<Currency> GetCurrencies(string catalog)
        {
            const string sql = "SELECT * FROM core.currencies ORDER BY currency_code;";
            return Factory.Get<Currency>(catalog, sql);
        }

        public static string GetCurrencyCode(string catalog, string accountNumber)
        {
            const string sql = "SELECT currency_code FROM core.accounts WHERE account_number=@0";

            Account account = Factory.Get<Account>(catalog, sql, accountNumber).FirstOrDefault();

            if (account != null)
            {
                return account.CurrencyCode;
            }

            return string.Empty;
        }

        public static string GetLocalCurrencyCode(string catalog, int officeId)
        {
            const string sql = "SELECT * FROM transactions.get_default_currency_code_by_office_id(@0)";
            return Factory.Scalar<string>(catalog, sql, officeId);
        }

        public static IEnumerable<Currency> GetExchangeCurrencies(string catalog, int officeId)
        {
            const string sql =
                "SELECT currency_code, currency_symbol, currency_name, hundredth_name FROM core.currencies WHERE currency_code != core.get_currency_code_by_office_id(@0);";
            return Factory.Get<Currency>(catalog, sql, officeId);
        }
    }
}