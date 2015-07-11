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
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Transactions;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Parties
    {
        public static IEnumerable<Party> GetParties(string catalog)
        {
            const string sql = "SELECT * FROM core.parties ORDER BY party_id;";
            return Factory.Get<Party>(catalog, sql);
        }

        public static string GetPartyCodeByPartyId(string catalog, int partyId)
        {
            const string sql = "SELECT party_code FROM core.parties WHERE party_id=@0;";
            Party party = Factory.Get<Party>(catalog, sql, partyId).FirstOrDefault();

            if (party != null)
            {
                return party.PartyCode;
            }

            return string.Empty;
        }

        public static PartyView GetPartyView(string catalog, string partyCode)
        {
            const string sql = "SELECT * FROM core.party_view WHERE party_code=@0 ORDER BY party_id";
            return Factory.Get<PartyView>(catalog, sql, partyCode).FirstOrDefault();
        }

        public static IEnumerable<ShippingAddress> GetShippingAddresses(string catalog, string partyCode)
        {
            const string sql =
                "SELECT * FROM core.shipping_addresses WHERE party_id=core.get_party_id_by_party_code(@0);";
            return Factory.Get<ShippingAddress>(catalog, sql, partyCode);
        }

        public static DbGetPartyTransactionSummaryResult GetPartyDue(string catalog, int officeId, string partyCode)
        {
            const string sql =
                "SELECT * FROM transactions.get_party_transaction_summary(@0::integer, core.get_party_id_by_party_code(@1)::bigint);";
            return Factory.Get<DbGetPartyTransactionSummaryResult>(catalog, sql, officeId, partyCode).FirstOrDefault();
        }
    }
}