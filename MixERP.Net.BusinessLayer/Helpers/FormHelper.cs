using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class FormHelper
    {
        public static DataTable GetTable(string tableSchema, string tableName)
        {
            return MixERP.Net.DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValues)
        {
            return MixERP.Net.DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValues);
        }

        public static DataTable GetTable(string tableSchema, string tableName, string columnNames, string columnValuesLike, int limit)
        {
            return MixERP.Net.DatabaseLayer.Helpers.FormHelper.GetTable(tableSchema, tableName, columnNames, columnValuesLike, limit);
        }
    }
}
