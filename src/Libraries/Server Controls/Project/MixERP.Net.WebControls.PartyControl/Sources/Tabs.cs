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

using MixERP.Net.i18n.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private Control GetTabs()
        {
            using (HtmlGenericControl tabList = ControlHelper.GetGenericControl(@"div", @"ui tabular top attached menu"))
            {
                using (HtmlAnchor homeAnchor = ControlHelper.GetAnchor("active item", Titles.Home, "home", "home icon"))
                {
                    tabList.Controls.Add(homeAnchor);
                }

                using (HtmlAnchor partySummaryAnchor = ControlHelper.GetAnchor("item", Titles.PartySummary, "party-summary", "user icon"))
                {
                    tabList.Controls.Add(partySummaryAnchor);
                }

                using (HtmlAnchor transactionSummaryAnchor = ControlHelper.GetAnchor("item", Titles.TransactionSummary, "transaction-summary", "book icon"))
                {
                    tabList.Controls.Add(transactionSummaryAnchor);
                }

                using (HtmlAnchor contactInfoAnchor = ControlHelper.GetAnchor("item", Titles.AddressAndContactInfo, "contact-info", "globe icon"))
                {
                    tabList.Controls.Add(contactInfoAnchor);
                }

                return tabList;
            }
        }
    }
}