using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class ConversionHelper
    {
        public static object GetColumnValue(System.Data.DataRow row, string columnName)
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
