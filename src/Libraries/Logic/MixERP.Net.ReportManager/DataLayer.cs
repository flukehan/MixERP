using MixERP.Net.Common.Helpers;
using MixERP.Net.DbFactory;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace MixERP.Net.ReportManager
{
    internal static class DataLayer
    {
        internal static void AddMenus(IEnumerable<ReportMenu> menus)
        {
            menus = menus.Where(m => !string.IsNullOrWhiteSpace(m.MenuCode) && !string.IsNullOrWhiteSpace(m.ParentMenuCode));

            foreach (ReportMenu menu in menus)
            {
                string url = Config.ReportUrlExpression.Replace("{ReportName}", menu.FileName);
                const string sql = @"SELECT * FROM core.create_menu(@MenuText, @Url, @MenuCode, 2, core.get_menu_id_by_menu_code(@ParentMenuCode));";

                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@MenuText", menu.Text);
                    command.Parameters.AddWithValue("@Url", url);
                    command.Parameters.AddWithValue("@MenuCode", menu.MenuCode);
                    command.Parameters.AddWithValue("@ParentMenuCode", menu.ParentMenuCode);

                    foreach (string catalog in GetCatalogs())
                    {
                        if (DbOperation.IsServerAvailable(catalog))
                        {
                            DbOperation.ExecuteNonQuery(catalog, command);
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> GetCatalogs()
        {
            string catalogs = ConfigurationHelper.GetDbServerParameter("Catalogs");
            List<string> list = catalogs.Split(',').Select(p => p.Trim()).ToList();

            return list;
        }
    }
}