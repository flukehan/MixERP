using System.Collections.Generic;
using System.Data;
using MixERP.Net.DbFactory;
using Npgsql;

namespace MixERP.Net.i18n
{
    internal static class DbResources
    {
        internal static IDictionary<string, string> GetLocalizedResources()
        {
            Dictionary<string, string> resources = new Dictionary<string, string>();
            const string sql = "SELECT * FROM localization.localized_resource_view;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DbOperation.GetDataTable(command))
                {
                    if (table == null || table.Rows == null)
                    {
                        return resources;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        string key = row[0].ToString();
                        string value = row[1].ToString();

                        resources.Add(key, value);
                    }
                }
            }

            return resources;
        }
    }
}