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

using System.Collections;
using System.Collections.Generic;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.DbFactory;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Parties
    {
        public static string GetPartyCodeByPartyId(int partyId)
        {
            Party party = Factory.Get<Party>("SELECT party_code FROM core.parties WHERE party_id=@0;", partyId).FirstOrDefault();

            if (party != null)
            {
                return party.PartyCode;
            }

            return string.Empty;
        }

        public static IEnumerable<Party> GetParties()
        {
            return Factory.Get<Party>("SELECT * FROM core.parties ORDER BY party_id;");
        }

        public static PartyDueModel GetPartyDue(string partyCode)
        {
            PartyDueModel model = new PartyDueModel();

            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = GetPartyDue(officeId, partyCode))
            {
                if (table.Rows.Count.Equals(1))
                {
                    model.CurrencyCode = Conversion.TryCastString(table.Rows[0]["currency_code"]);
                    model.CurrencySymbol = Conversion.TryCastString(table.Rows[0]["currency_symbol"]);
                    model.TotalDueAmount = Conversion.TryCastDecimal(table.Rows[0]["total_due_amount"]);
                    model.OfficeDueAmount = Conversion.TryCastDecimal(table.Rows[0]["office_due_amount"]);
                    model.AccruedInterest = Conversion.TryCastDecimal(table.Rows[0]["accrued_interest"]);
                    model.LastReceiptDate = Conversion.TryCastDate(table.Rows[0]["last_receipt_date"]);
                    model.TransactionValue = Conversion.TryCastDecimal(table.Rows[0]["transaction_value"]);
                }
            }

            return model;
        }

        public static PartyView GetPartyView(string partyCode)
        {
            return Factory.Get<PartyView>("SELECT * FROM core.party_view WHERE party_code=@PartyCode ORDER BY party_id").FirstOrDefault();
        }

        public static IEnumerable<ShippingAddress> GetShippingAddresses(string partyCode)
        {
            return Factory.Get<ShippingAddress>("SELECT * FROM core.shipping_addresses WHERE party_id=core.get_party_id_by_party_code(@0);", partyCode);
        }

        private static DataTable GetPartyDue(int officeId, string partyCode)
        {
            const string sql = "SELECT * FROM transactions.get_party_transaction_summary(@OfficeId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return DbOperation.GetDataTable(command);
            }
        }
    }
}