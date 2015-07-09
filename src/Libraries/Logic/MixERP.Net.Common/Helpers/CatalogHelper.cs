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

using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using System.Collections.Generic;
using System.Linq;

namespace MixERP.Net.Common.Helpers
{
    public static class CatalogHelper
    {
        public static void ValidateCatalog(string catalog)
        {
            if (string.IsNullOrWhiteSpace(catalog))
            {
                return;
            }

            string catalogs = ConfigurationHelper.GetDbServerParameter("Catalogs");
            string meta = ConfigurationHelper.GetDbServerParameter("MetaDatabase");

            List<string> list = catalogs.Split(',').Select(p => p.Trim()).ToList();
            list.Add(meta);

            if (!list.Contains(catalog))
            {
                throw new MixERPException(Titles.AccessIsDenied);
            }
        }
    }
}