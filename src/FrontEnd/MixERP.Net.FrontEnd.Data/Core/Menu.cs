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

using System.Collections.Generic;
using MixERP.Net.Entities;

namespace MixERP.Net.FrontEnd.Data.Core
{
    public static class Menu
    {
        public static IEnumerable<Entities.Core.Menu> GetMenuCollection(int officeId, int userId, string culture, string path, short level)
        {
            string relativePath = path;
            return Factory.Get<Entities.Core.Menu>("SELECT * FROM policy.get_menu(@0, @1, @2) WHERE parent_menu_id=(SELECT menu_id FROM core.menus WHERE url=@3) AND level=@4 ORDER BY menu_id;", userId, officeId, culture, relativePath, level);
        }

        public static IEnumerable<Entities.Core.Menu> GetMenuCollection(int officeId, int userId, string culture, int parentMenuId, short level)
        {
            if (parentMenuId > 0)
            {
                return Factory.Get<Entities.Core.Menu>("SELECT * FROM policy.get_menu(@0, @1, @2) WHERE parent_menu_id=@3 AND level=@4 ORDER BY menu_id;", userId, officeId, culture, parentMenuId, level);
            }

            return Factory.Get<Entities.Core.Menu>("SELECT * FROM policy.get_menu(@0, @1, @2) WHERE parent_menu_id is null ORDER BY menu_id;", userId, officeId, culture);
        }

        public static IEnumerable<Entities.Core.Menu> GetRootMenuCollection(int officeId, int userId, string culture, string path)
        {
            return Factory.Get<Entities.Core.Menu>("SELECT * FROM policy.get_menu(@0, @1, @2) WHERE parent_menu_id=core.get_root_parent_menu_id(@3) ORDER BY menu_id;", userId, officeId, culture, path);
        }
    }
}