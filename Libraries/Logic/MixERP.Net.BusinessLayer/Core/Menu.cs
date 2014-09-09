using MixERP.Net.Common;

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

using MixERP.Net.Common.Helpers;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class Menu
    {
        public static Collection<Common.Models.Core.Menu> GetMenuCollection(string path, short level)
        {
            Collection<Common.Models.Core.Menu> collection = new Collection<Common.Models.Core.Menu>();

            string relativePath = Conversion.GetRelativePath(path);
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();
            string culture = SessionHelper.GetCulture().TwoLetterISOLanguageName;

            using (DataTable table = DatabaseLayer.Core.Menu.GetMenuTable(relativePath, level, userId, officeId, culture))
            {
                if (table == null)
                {
                    return null;
                }

                foreach (DataRow row in table.Rows)
                {
                    Common.Models.Core.Menu model = new Common.Models.Core.Menu();

                    model.MenuId = Conversion.TryCastInteger(row["menu_id"]);
                    model.MenuText = Conversion.TryCastString(row["menu_text"]);
                    model.Url = Conversion.ResolveUrl(Conversion.TryCastString(row["url"]));
                    model.MenuCode = Conversion.TryCastString(row["menu_code"]);
                    model.Level = Conversion.TryCastInteger(row["level"]);
                    model.ParentMenuId = Conversion.TryCastInteger(row["parent_menu_id"]);

                    collection.Add(model);
                }
            }
            return collection;
        }

        public static Collection<Common.Models.Core.Menu> GetRootMenuCollection(string path)
        {
            Collection<Common.Models.Core.Menu> collection = new Collection<Common.Models.Core.Menu>();

            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();
            string culture = SessionHelper.GetCulture().TwoLetterISOLanguageName;

            using (DataTable table = DatabaseLayer.Core.Menu.GetRootMenuTable(path, userId, officeId, culture))
            {
                if (table == null)
                {
                    return null;
                }

                foreach (DataRow row in table.Rows)
                {
                    Common.Models.Core.Menu model = new Common.Models.Core.Menu();

                    model.MenuId = Conversion.TryCastInteger(row["menu_id"]);
                    model.MenuText = Conversion.TryCastString(row["menu_text"]);
                    model.Url = Conversion.ResolveUrl(Conversion.TryCastString(row["url"]));
                    model.MenuCode = Conversion.TryCastString(row["menu_code"]);
                    model.Level = Conversion.TryCastInteger(row["level"]);
                    model.ParentMenuId = Conversion.TryCastInteger(row["parent_menu_id"]);

                    collection.Add(model);
                }
            }

            return collection;
        }

        public static Collection<Common.Models.Core.Menu> GetMenuCollection(int parentMenuId, short level)
        {
            Collection<Common.Models.Core.Menu> collection = new Collection<Common.Models.Core.Menu>();

            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();
            string culture = SessionHelper.GetCulture().TwoLetterISOLanguageName;

            using (DataTable table = DatabaseLayer.Core.Menu.GetMenuTable(parentMenuId, level, userId, officeId, culture))
            {
                if (table == null)
                {
                    return null;
                }

                foreach (DataRow row in table.Rows)
                {
                    Common.Models.Core.Menu model = new Common.Models.Core.Menu();

                    model.MenuId = Conversion.TryCastInteger(row["menu_id"]);
                    model.MenuText = Conversion.TryCastString(row["menu_text"]);
                    model.Url = Conversion.ResolveUrl(Conversion.TryCastString(row["url"]));
                    model.MenuCode = Conversion.TryCastString(row["menu_code"]);
                    model.Level = Conversion.TryCastInteger(row["level"]);
                    model.ParentMenuId = Conversion.TryCastInteger(row["parent_menu_id"]);

                    collection.Add(model);
                }
            }

            return collection;
        }
    }
}