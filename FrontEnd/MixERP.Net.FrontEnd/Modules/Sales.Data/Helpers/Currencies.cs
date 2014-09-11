using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Currencies
    {
        public static System.Data.DataTable GetCurrencyDataTable()
        {
            return FormHelper.GetTable("core", "currencies");
        }
    }
}