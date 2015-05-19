using System.Collections.Generic;
using System.Linq;
using MixERP.Net.Common.Helpers;
using MixERP.Net.DbFactory;
using Npgsql;

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
                const string sql = @"INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id) 
                                     SELECT @MenuText, @Url, @MenuCode, @Level, core.get_menu_id_by_menu_code(@ParentMenuCode);";

                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.AddWithValue("@MenuText", menu.Text);
                    command.Parameters.AddWithValue("@Url", url);
                    command.Parameters.AddWithValue("@MenuCode", menu.MenuCode);
                    command.Parameters.AddWithValue("@ParentMenuCode", menu.ParentMenuCode);

                    foreach (string catalog in GetCatalogs())
                    {
                        DbOperation.ExecuteNonQuery(catalog, command);                        
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