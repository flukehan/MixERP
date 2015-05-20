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
using MixERP.Net.DbFactory;
using Npgsql;
using System.Data;
using System.Linq;

namespace MixERP.Net.Core.Modules.Sales.Data.Reports
{
    public static class TopSellingProducts
    {
        public static DataTable GetTopSellingProductsOfAllTime(string catalog)
        {
            const string sql = "SELECT * FROM transactions.get_top_selling_products_of_all_time();";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return DbOperation.GetDataTable(catalog, command);
            }
        }

        public static DataTable GetTopSellingProductsOfAllTimeByOffice(string catalog)
        {
            const string sql = "SELECT  id, office_code, item_name, total_sales FROM transactions.get_top_selling_products_by_office();";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return GetPivotTable(DbOperation.GetDataTable(catalog, command));
            }
        }

        private static DataTable GetPivotTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return null;
            }

            if (dataTable.Rows.Count.Equals(0))
            {
                return null;
            }

            const string ITEM_NAME = "ItemName";
            const string item_name = "item_name";
            const string office_code = "office_code";
            const string total_sales = "total_sales";

            using (DataTable table = new DataTable())
            {
                table.Columns.Add(ITEM_NAME);

                var items = dataTable.AsEnumerable().Select(s => s.Field<string>(item_name)).Distinct().ToList();
                var offices = dataTable.AsEnumerable().Select(s => s.Field<string>(office_code)).Distinct().ToList();

                foreach (var office in offices)
                {
                    table.Columns.Add(Conversion.TryCastString(office));
                }

                foreach (var item in items)
                {
                    DataRow row = table.NewRow();
                    string itemName = Conversion.TryCastString(item);
                    row[ITEM_NAME] = itemName;

                    foreach (DataColumn column in table.Columns)
                    {
                        string columnName = column.ColumnName;

                        if (columnName != ITEM_NAME)
                        {
                            decimal results = dataTable.AsEnumerable().
                                Where(r => r.Field<string>(office_code) == columnName
                                    && r.Field<string>(item_name) == itemName)
                           .Select(r => r.Field<decimal>(total_sales))
                           .Sum();

                            row[columnName] = results;
                        }
                    }

                    table.Rows.Add(row);
                }

                return table;
            }
        }
    }
}