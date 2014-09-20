using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;
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
            return FormHelper.GetTable("core", "currencies", "currency_code");
        }

        public static string GetHomeCurrency(int officeId)
        {
            const string sql = "SELECT core.get_currency_code_by_office_id(@OfficeId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                return Conversion.TryCastString(DbOperations.GetScalarValue(command));
            }
        }
    }
}