using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
                using (DataTable table = GetDataTable(command))
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


        internal static IEnumerable<LocalizedResource> GetLocalizationTable(string language)
        {
            const string sql = "SELECT * FROM localization.get_localization_table(@0);";
            return Factory.Get<LocalizedResource>(string.Empty, sql, language);
        }        

        private static DataTable GetDataTable(NpgsqlCommand command)
        {
            string connectionString = ConfigurationHelper.GetConnectionString();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                command.Connection = connection;

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        dataTable.Locale = Thread.CurrentThread.CurrentCulture;
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}