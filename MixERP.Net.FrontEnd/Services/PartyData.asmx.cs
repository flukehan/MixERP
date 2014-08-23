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
