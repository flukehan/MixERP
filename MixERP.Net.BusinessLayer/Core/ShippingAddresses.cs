/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class ShippingAddresses
    {
        public static DataTable GetShippingAddressView(int partyId)
        {
            return DatabaseLayer.Core.ShippingAddresses.GetShippingAddressView(partyId);
        }

        public static DataTable GetShippingAddressView(string partyCode)
        {
            return DatabaseLayer.Core.ShippingAddresses.GetShippingAddressView(partyCode);
        }

    }
}
