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
using PetaPoco;

namespace MixERP.Net.FrontEnd.Data.Core
{
    public static class Menu
    {
        public static IEnumerable<Entities.Core.Menu> GetMenuCollection(string catalog, int officeId, int userId,
            string culture)
        {
            const string sql = "SELECT * FROM policy.get_menu(@0::integer, @1::integer, @2::text) ORDER BY menu_id;";
            return Factory.Get<Entities.Core.Menu>(catalog, sql, userId, officeId, culture);
        }
    }
}