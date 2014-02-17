/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;
using System.Collections.ObjectModel;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
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
