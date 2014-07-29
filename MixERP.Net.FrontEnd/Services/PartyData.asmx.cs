/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.Common.Helpers;
using FormHelper = MixERP.Net.BusinessLayer.Helpers.FormHelper;

namespace MixERP.Net.FrontEnd.Services
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

            using (DataTable table = FormHelper.GetTable("core", "parties"))
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
        public string GetPartyCodeByPartyId(int partyId)
        {
            return Parties.GetPartyCodeByPartyId(partyId);
        }
    }

}
