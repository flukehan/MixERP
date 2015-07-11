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
using MixERP.Net.Entities.Localization;
using PetaPoco;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    public static class LocalizeMixERP
    {
        public static void Save(string catalog, string cultureCode, string key, string value)
        {
            const string sql = "SELECT * FROM localization.add_localized_resource(@0, @1, @2);";
            Factory.NonQuery(catalog, sql, cultureCode, key, value);
        }

        public static IEnumerable<Culture> GetCultures(string catalog)
        {
            const string sql = "SELECT * FROM localization.cultures;";
            return Factory.Get<Culture>(catalog, sql);
        }
    }
}