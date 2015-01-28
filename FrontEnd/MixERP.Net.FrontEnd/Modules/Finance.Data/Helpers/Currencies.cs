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
    public static class Currencies
    {
        public static IEnumerable<Currency> GetCurrencies()
        {
            return Factory.Get<Currency>("SELECT * FROM core.currencies ORDER BY currency_code;");
        }

        public static string GetCurrencyCode(string accountNumber)
        {
            Account account = Factory.Get<Account>("SELECT currency_code FROM core.accounts WHERE account_number=@0", accountNumber).FirstOrDefault();

            if (account != null)
            {
                return account.CurrencyCode;
            }

            return string.Empty;
        }

        public static IEnumerable<Currency> GetExchangeCurrencies(int officeId)
        {
            return Factory.Get<Currency>("SELECT currency_code, currency_symbol, currency_name, hundredth_name FROM core.currencies WHERE currency_code != core.get_currency_code_by_office_id(@0);", officeId);
        }
    }
}