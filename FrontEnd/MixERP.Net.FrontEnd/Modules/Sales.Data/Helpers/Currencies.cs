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

using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

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