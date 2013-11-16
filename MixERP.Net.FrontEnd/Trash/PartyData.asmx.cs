using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Trash
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class PartyData : System.Web.Services.WebService
    {
        [WebMethod]
        public List<Party> GetParties(string knownCategoryValues, string category)
        {
            List<Party> values = new List<Party>();

            using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "parties"))
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("PartyDisplayField");
                table.Columns.Add("party", typeof(string), displayField);
                foreach (System.Data.DataRow dr in table.Rows)
                {
                    values.Add(new Party(dr["party"].ToString(), dr["party_code"].ToString()));
                }
            }

            return values;
        }

        [WebMethod]
        public List<PartyAddress> GetAddressByPartyCode(string partyCode)
        {
            List<PartyAddress> values = new List<PartyAddress>();

            using (System.Data.DataTable table = MixERP.Net.BusinessLayer.Core.ShippingAddresses.GetShippingAddressView(partyCode))
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter("ShippingAddressDisplayField");
                table.Columns.Add("shipping_address", typeof(string), displayField);

                foreach (System.Data.DataRow dr in table.Rows)
                {
                    values.Add(new PartyAddress(dr["shipping_address_code"].ToString(), dr["shipping_address"].ToString()));
                }
            }

            return values;
        }


        public class PartyAddress
        {
            public string DisplayField { get; set; }
            public string Address { get; set; }

            public PartyAddress()
            {
            }

            public PartyAddress(string displayField, string address)
            {
                this.DisplayField = displayField;
                this.Address = address;
            }
        }

        public class Party
        {
            public string DisplayField { get; set; }
            public string PartyCode { get; set; }

            public Party()
            {

            }

            public Party(string displayField, string partyCode)
            {
                this.DisplayField = displayField;
                this.PartyCode = partyCode;
            }
        }
    }
}
