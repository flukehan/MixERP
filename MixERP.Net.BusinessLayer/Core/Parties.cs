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

using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class Parties
    {
        public static bool IsCreditAllowed(string partyCode)
        {
            return DatabaseLayer.Core.Parties.IsCreditAllowed(partyCode);
        }

        public static string GetPartyCodeByPartyId(int partyId)
        {
            return DatabaseLayer.Core.Parties.GetPartyCodeByPartyId(partyId);
        }

        public static Collection<PartyShippingAddress> GetShippingAddresses(string partyCode)
        {
            Collection<PartyShippingAddress> addresses = new Collection<PartyShippingAddress>();

            using (DataTable table = DatabaseLayer.Core.Parties.GetShippingAddresses(partyCode))
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

            return addresses;
        }

        public static PartyDueModel GetPartyDue(string partyCode)
        {
            PartyDueModel model = new PartyDueModel();

            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = DatabaseLayer.Core.Parties.GetPartyDue(officeId, partyCode))
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
    }
}
