/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

namespace MixERP.Net.BusinessLayer.Office
{
    public static class Stores
    {
        public static bool IsSalesAllowed(int storeId)
        {
            return DatabaseLayer.Office.Stores.IsSalesAllowed(storeId);
        }

    }
}
