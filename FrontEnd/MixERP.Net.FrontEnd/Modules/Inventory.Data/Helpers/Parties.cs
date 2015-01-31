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
using System.Linq;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Transactions;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Parties
    {
        public static IEnumerable<Party> GetParties()
        {
            return Factory.Get<Party>("SELECT * FROM core.parties ORDER BY party_id;");
        }

        public static string GetPartyCodeByPartyId(int partyId)
        {
            Party party = Factory.Get<Party>("SELECT party_code FROM core.parties WHERE party_id=@0;", partyId).FirstOrDefault();

            if (party != null)
            {
                return party.PartyCode;
            }

            return string.Empty;
        }

        public static DbGetPartyTransactionSummaryResult GetPartyDue(string partyCode)
        {
            int officeId = CurrentSession.GetOfficeId();
            return GetPartyDue(officeId, partyCode);
        }

        public static PartyView GetPartyView(string partyCode)
        {
            return Factory.Get<PartyView>("SELECT * FROM core.party_view WHERE party_code=@PartyCode ORDER BY party_id").FirstOrDefault();
        }

        public static IEnumerable<ShippingAddress> GetShippingAddresses(string partyCode)
        {
            return Factory.Get<ShippingAddress>("SELECT * FROM core.shipping_addresses WHERE party_id=core.get_party_id_by_party_code(@0);", partyCode);
        }

        private static DbGetPartyTransactionSummaryResult GetPartyDue(int officeId, string partyCode)
        {
            return Factory.Get<DbGetPartyTransactionSummaryResult>("SELECT * FROM transactions.get_party_transaction_summary(@0, core.get_party_id_by_party_code(@1));", officeId, partyCode).FirstOrDefault();
        }
    }
}