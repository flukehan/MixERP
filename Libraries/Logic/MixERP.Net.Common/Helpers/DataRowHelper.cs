using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Helpers
{
    public static class DataRowHelper
    {
        public static object GetColumnValue(DataRow row, string columnName)
        {
            object value = new object();

            if (row != null && !string.IsNullOrWhiteSpace(columnName))
            {
                if (row.Table.Columns.Contains(columnName))
                {
                    value = row[columnName];
                }
            }

            return value;
        }
    }
}