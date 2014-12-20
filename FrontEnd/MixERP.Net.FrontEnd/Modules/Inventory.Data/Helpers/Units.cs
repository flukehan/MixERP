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

using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Units
    {
        public static DataTable GetUnitViewByItemCode(string itemCode)
        {
            const string sql = "SELECT * FROM core.get_associated_units_from_item_code(@ItemCode) ORDER BY unit_id;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);

                return DbOperation.GetDataTable(command);
            }
        }

        public static bool UnitExistsByName(string unitName)
        {
            const string sql = "SELECT 1 FROM core.units WHERE core.units.unit_name=@UnitName;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UnitName", unitName);

                var value = DbOperation.GetScalarValue(command);
                if (value != null)
                {
                    return value.ToString().Equals("1");
                }
            }
            return false;
        }
    }
}