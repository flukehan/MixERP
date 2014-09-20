using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Currencies
    {
        public static DataTable GetCurrencyDataTable(string accountCode)
        {
            return FormHelper.GetTable("core", "accounts", "account_code", accountCode, "account_id");
        }

        public static DataTable GetCurrencyDataTable()
        {
            return FormHelper.GetTable("core", "currencies", "currency_code");
        }
    }
}