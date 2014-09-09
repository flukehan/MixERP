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