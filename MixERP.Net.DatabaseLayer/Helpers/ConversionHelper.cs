using System.Data;

namespace MixERP.Net.DatabaseLayer.Helpers
{
    public static class ConversionHelper
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
