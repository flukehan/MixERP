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

using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private Control GetTabs()
        {
            using (HtmlGenericControl tabList = ControlHelper.GetGenericControl(@"ul", @"nav nav-tabs", @"tablist"))
            {
                using (HtmlGenericControl homeListItem = ControlHelper.GetGenericControl(@"li", @"active"))
                {
                    using (HtmlAnchor homeAnchor = ControlHelper.GetAnchor(@"#home", @"tab", @"tab", "Home"))
                    {
                        homeListItem.Controls.Add(homeAnchor);
                    }

                    tabList.Controls.Add(homeListItem);
                }

                using (HtmlGenericControl partySummaryListItem = ControlHelper.GetGenericControl(@"li", string.Empty))
                {
                    using (HtmlAnchor partySummaryAnchor = ControlHelper.GetAnchor(@"#party-summary", @"tab", @"tab", "Party Summary"))
                    {
                        partySummaryListItem.Controls.Add(partySummaryAnchor);
                    }
                    tabList.Controls.Add(partySummaryListItem);
                }

                using (HtmlGenericControl transactionSummaryListItem = ControlHelper.GetGenericControl(@"li", string.Empty))
                {
                    using (HtmlAnchor transactionSummaryAnchor = ControlHelper.GetAnchor(@"#transaction-summary", @"tab", @"tab", "Transaction Summary"))
                    {
                        transactionSummaryListItem.Controls.Add(transactionSummaryAnchor);
                    }

                    tabList.Controls.Add(transactionSummaryListItem);
                }

                using (HtmlGenericControl contactInfoListItem = ControlHelper.GetGenericControl(@"li", string.Empty))
                {
                    using (HtmlAnchor contactInfoAnchor = ControlHelper.GetAnchor(@"#addresses-and-contact-info", @"tab", @"tab", "Address & Contact Information"))
                    {
                        contactInfoListItem.Controls.Add(contactInfoAnchor);
                    }

                    tabList.Controls.Add(contactInfoListItem);
                }

                return tabList;
            }
        }
    }
}