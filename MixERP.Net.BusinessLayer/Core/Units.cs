/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class Units
    {
        public static DataTable GetUnitViewByItemCode(string itemCode)
        {
            return DatabaseLayer.Core.Units.GetUnitViewByItemCode(itemCode);
        }

        public static bool UnitExistsByName(string unitName)
        {
            return DatabaseLayer.Core.Units.UnitExistsByName(unitName);
        }
    }
}
