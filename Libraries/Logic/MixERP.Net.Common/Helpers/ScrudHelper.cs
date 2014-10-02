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

namespace MixERP.Net.Common.Helpers
{
    public static class ScrudHelper
    {
        public static void AddDisplayField(List<string> collection, string columnName, string expression)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + expression);
        }

        public static void AddDisplayView(List<string> collection, string columnName, string parentTable)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + parentTable);
        }

        public static void AddSelectedValue(List<string> collection, string columnName, string selectedValue)
        {
            if (collection == null)
            {
                return;
            }

            collection.Add(columnName + "-->" + selectedValue);
        }
    }
}