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

using MixERP.Net.DbFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Sales.Data.Reports
{
    public static class SalesByMonth
    {
        public static DataTable GetSalesByOffice(int officeId)
        {
            const string sql = "SELECT * FROM transactions.get_sales_by_offices(@OfficeId, 1000)";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);

                return DbOperation.GetDataTable(command);
            }
        }

        public static DataTable GetSalesByOffice()
        {
            const string sql = "SELECT * FROM transactions.get_sales_by_offices(1000)";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return DbOperation.GetDataTable(command);
            }
        }
    }
}