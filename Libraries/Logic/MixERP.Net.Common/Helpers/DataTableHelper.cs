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
using System.Collections.ObjectModel;
using System.Data;
using System.Web.UI.WebControls;

namespace MixERP.Net.Common.Helpers
{
    public static class DataTableHelper
    {
        public static Collection<KeyValuePair<string, string>> GetKeyValuePairCollectionFromDataTable(DataTable table, string keyColumn, string valueColumn)
        {
            Collection<KeyValuePair<string, string>> collection = new Collection<KeyValuePair<string, string>>();

            if (table == null || table.Rows == null || table.Rows.Count.Equals(0))
            {
                return collection;
            }

            foreach (DataRow row in table.Rows)
            {
                string key = Conversion.TryCastString(row[keyColumn]);
                string value = Conversion.TryCastString(row[valueColumn]);

                collection.Add(new KeyValuePair<string, string>(key, value));
            }

            return collection;
        }

        public static Collection<ListItem> GetListeItemCollectionFromDataTable(DataTable table, string keyColumn, string textColumn)
        {
            Collection<ListItem> collection = new Collection<ListItem>();

            if (table == null || table.Rows == null || table.Rows.Count.Equals(0))
            {
                return collection;
            }

            foreach (DataRow row in table.Rows)
            {
                string value = Conversion.TryCastString(row[keyColumn]);
                string text = Conversion.TryCastString(row[textColumn]);

                collection.Add(new ListItem(text, value));
            }

            return collection;
        }
    }
}