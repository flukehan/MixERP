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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public Collection<ListItem> GetAddressByPartyCode(string partyCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (ShippingAddressView view in ShippingAddresses.GetShippingAddressView(AppUsers.GetCurrentUserDB(), partyCode))
            {
                string address = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{1}{3}{4}{5}{1}{6}{4}{7}", view.AddressLine1, Environment.NewLine, view.AddressLine2, view.Street, ", ", view.City, view.State, view.Country);

                values.Add(new ListItem(view.ShippingAddressCode, address));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetParties()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (Party party in Parties.GetParties(AppUsers.GetCurrentUserDB()))
            {
                values.Add(new ListItem(party.PartyName, party.PartyCode));
            }

            return values;
        }

        [WebMethod]
        public string GetPartyCodeByPartyId(int partyId)
        {
            return Parties.GetPartyCodeByPartyId(AppUsers.GetCurrentUserDB(), partyId);
        }

        [WebMethod]
        public DbGetPartyTransactionSummaryResult GetPartyDue(string partyCode)
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            return Parties.GetPartyDue(AppUsers.GetCurrentUserDB(), officeId, partyCode);
        }

        [WebMethod]
        public PartyView GetPartyView(string partyCode)
        {
            return Parties.GetPartyView(AppUsers.GetCurrentUserDB(), partyCode);
        }

        [WebMethod]
        public IEnumerable<ShippingAddress> GetShippingAddresses(string partyCode)
        {
            return Parties.GetShippingAddresses(AppUsers.GetCurrentUserDB(), partyCode);
        }
    }
}