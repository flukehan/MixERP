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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Policy;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Policy
{
   public static class Menu
    {
       public static IEnumerable<DbGetMenuPolicyResult> GetMenuPolicy(int userId, int officeId, string culture)
       {
           return Factory.Get<DbGetMenuPolicyResult>("SELECT * FROM policy.get_menu_policy(@0, @1, @2)", userId, officeId, culture);
       }

       public static void SaveMenuPolicy(int userId, int officeId, string menus)
       {
           const string sql = "SELECT * FROM policy.save_menu_policy(@0, @1, string_to_array(@2, ',')::int[]);";
           Factory.NonQuery(sql, userId, officeId, menus);
       }
    }
}
