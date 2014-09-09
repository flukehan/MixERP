using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Parties
    {
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

        public static string GetPartyCodeByPartyId(int partyId)
        {
            return DatabaseLayer.Core.Parties.GetPartyCodeByPartyId(partyId);
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