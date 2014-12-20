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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Parties
    {
        public static string GetPartyCodeByPartyId(int partyId)
        {
            const string sql = "SELECT party_code FROM core.parties WHERE party_id=@PartyId;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyId", partyId);
                return Conversion.TryCastString(DbOperation.GetScalarValue((command)));
            }
        }

        public static DataTable GetPartyDataTable()
        {
            return FormHelper.GetTable("core", "parties", "party_id");
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

        public static DataTable GetPartyViewDataTable(string partyCode)
        {
            return FormHelper.GetTable("core", "party_view", "party_code", partyCode, "party_id");
        }

        public static Collection<PartyShippingAddress> GetShippingAddresses(string partyCode)
        {
            Collection<PartyShippingAddress> addresses = new Collection<PartyShippingAddress>();

            const string sql = "SELECT * FROM core.shipping_addresses WHERE party_id=core.get_party_id_by_party_code(@PartyCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                using (DataTable table = DbOperation.GetDataTable(command))
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        PartyShippingAddress address = new PartyShippingAddress();
                        address.POBox = Conversion.TryCastString(table.Rows[i]["po_box"]);
                        address.AddressLine1 = Conversion.TryCastString(table.Rows[i]["address_line_1"]);
                        address.AddressLine2 = Conversion.TryCastString(table.Rows[i]["address_line_2"]);
                        address.Street = Conversion.TryCastString(table.Rows[i]["street"]);
                        address.City = Conversion.TryCastString(table.Rows[i]["city"]);
                        address.State = Conversion.TryCastString(table.Rows[i]["state"]);
                        address.Country = Conversion.TryCastString(table.Rows[i]["country"]);

                        addresses.Add(address);
                    }
                }
            }

            return addresses;
        }

        private static DataTable GetPartyDue(int officeId, string partyCode)
        {
            const string sql =
                "SELECT * FROM transactions.get_party_transaction_summary(@OfficeId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return DbOperation.GetDataTable(command);
            }
        }
    }
}