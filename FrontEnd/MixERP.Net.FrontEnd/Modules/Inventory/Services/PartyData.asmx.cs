using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Inventory.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class PartyData : WebService
    {
        [WebMethod]
        public Collection<ListItem> GetParties()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Helpers.Parties.GetPartyDataTable())
            {
                string displayField = ConfigurationHelper.GetDbParameter("PartyDisplayField");
                table.Columns.Add("party", typeof(string), displayField);
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["party"].ToString(), dr["party_code"].ToString()));
                }
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetAddressByPartyCode(string partyCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = ShippingAddresses.GetShippingAddressView(partyCode))
            {
                string displayField = ConfigurationHelper.GetDbParameter("ShippingAddressDisplayField");
                table.Columns.Add("shipping_address", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["shipping_address_code"].ToString(), dr["shipping_address"].ToString()));
                }
            }

            return values;
        }

        [WebMethod]
        public Party GetPartyView(string partyCode)
        {
            Party party = new Party();
            using (DataTable table = Parties.GetPartyViewDataTable(partyCode))
            {
                party.PartyId = Conversion.TryCastInteger(table.Rows[0]["party_id"]);
                party.PartyTypeId = Conversion.TryCastInteger(table.Rows[0]["party_type_id"]);
                party.IsSupplier = Conversion.TryCastBoolean(table.Rows[0]["is_supplier"]);
                party.PartyType = Conversion.TryCastString(table.Rows[0]["party_type"]);
                party.PartyCode = Conversion.TryCastString(table.Rows[0]["party_code"]);
                party.FirstName = Conversion.TryCastString(table.Rows[0]["first_name"]);
                party.MiddleName = Conversion.TryCastString(table.Rows[0]["middle_name"]);
                party.LastName = Conversion.TryCastString(table.Rows[0]["last_name"]);
                party.PartyName = Conversion.TryCastString(table.Rows[0]["party_name"]);
                party.POBox = Conversion.TryCastString(table.Rows[0]["po_box"]);
                party.AddressLine1 = Conversion.TryCastString(table.Rows[0]["address_line_1"]);
                party.AddressLine2 = Conversion.TryCastString(table.Rows[0]["address_line_2"]);
                party.Street = Conversion.TryCastString(table.Rows[0]["street"]);
                party.City = Conversion.TryCastString(table.Rows[0]["city"]);
                party.State = Conversion.TryCastString(table.Rows[0]["state"]);
                party.Country = Conversion.TryCastString(table.Rows[0]["country"]);
                party.AllowCredit = Conversion.TryCastBoolean(table.Rows[0]["allow_credit"]);
                party.MaximumCreditPeriod = Conversion.TryCastInteger(table.Rows[0]["maximum_credit_period"]);
                party.MaximumCreditAmount = Conversion.TryCastDecimal(table.Rows[0]["maximum_credit_amount"]);
                party.ChargeInterest = Conversion.TryCastBoolean(table.Rows[0]["charge_interest"]);
                party.InterestRate = Conversion.TryCastDecimal(table.Rows[0]["interest_rate"]);
                party.InterestCompoundingFrequency = Conversion.TryCastString(table.Rows[0]["compounding_frequency"]);
                party.PANNumber = Conversion.TryCastString(table.Rows[0]["pan_number"]);
                party.SSTNumber = Conversion.TryCastString(table.Rows[0]["sst_number"]);
                party.CSTNumber = Conversion.TryCastString(table.Rows[0]["cst_number"]);
                party.Phone = Conversion.TryCastString(table.Rows[0]["phone"]);
                party.Fax = Conversion.TryCastString(table.Rows[0]["fax"]);
                party.Cell = Conversion.TryCastString(table.Rows[0]["cell"]);
                party.Email = Conversion.TryCastString(table.Rows[0]["email"]);
                party.Url = Conversion.TryCastString(table.Rows[0]["url"]);
                party.GLHead = Conversion.TryCastString(table.Rows[0]["gl_head"]);
            }

            return party;
        }

        [WebMethod]
        public Collection<PartyShippingAddress> GetShippingAddresses(string partyCode)
        {
            return Parties.GetShippingAddresses(partyCode);
        }

        [WebMethod]
        public string GetPartyCodeByPartyId(int partyId)
        {
            return Parties.GetPartyCodeByPartyId(partyId);
        }

        [WebMethod(EnableSession = true)]
        public PartyDueModel GetPartyDue(string partyCode)
        {
            return Parties.GetPartyDue(partyCode);
        }
    }
}