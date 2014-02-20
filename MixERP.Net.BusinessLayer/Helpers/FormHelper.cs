/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class FormHelper
    {
        public static DataTable GetTable(string tableSchema, string tableName)
        {
            return DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValues)
        {
            return DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValues);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValuesLike, int limit)
        {
            return DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValuesLike, limit);
        }
    }
}
