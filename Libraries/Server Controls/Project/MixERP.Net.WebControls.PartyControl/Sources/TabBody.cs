using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private Control GetTabBody()
        {
            using (HtmlGenericControl tabContentDiv = ControlHelper.GetGenericControl(@"div", @"tab-content"))
            {
                using (HtmlGenericControl homeTab = ControlHelper.GetGenericControl(@"div", "tab-pane fade in active"))
                {
                    homeTab.ID = "home";
                    tabContentDiv.Controls.Add(homeTab);
                }

                tabContentDiv.Controls.Add(this.GetPartySummaryTab());
                tabContentDiv.Controls.Add(this.GetTransactionSummaryTab());
                tabContentDiv.Controls.Add(this.GetAddressInfoTab());

                return tabContentDiv;
            }
        }

        private HtmlGenericControl GetPartySummaryTab()
        {
            using (HtmlGenericControl partSummaryDiv = ControlHelper.GetGenericControl(@"div", @"tab-pane fade"))
            {
                partSummaryDiv.ID = "party-summary";

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "table table-bordered table-hover");
                    table.Rows.Add(ControlHelper.GetNewRow("Party Type", @"PartyTypeSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Email Address", @"EmailAddressSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("PAN Number", @"PANNumberSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("SST Number", @"SSTNumberSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("CST Number", @"CSTNumberSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Credit Allowed", @"CreditAllowedSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Maximum Credit Period", @"MaxCreditPeriodSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Maximum Credit Amount", @"MaxCreditAmountSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Interest Applicable Span", @"InterestApplicableSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("GL Head", @"GLHeadSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Default Currency", @"DefaultCurrencySpan", @"span"));
                    partSummaryDiv.Controls.Add(table);
                }

                return partSummaryDiv;
            }
        }

        private HtmlGenericControl GetTransactionSummaryTab()
        {
            using (HtmlGenericControl transactionSummaryDiv = ControlHelper.GetGenericControl(@"div", @"tab-pane fade"))
            {
                transactionSummaryDiv.ID = "transaction-summary";

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "table table-bordered table-hover");
                    table.Rows.Add(ControlHelper.GetNewRow("Total Due Amount", @"TotalDueAmountSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Total Due Amount (Current Office)", @"OfficeDueAmountSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Accrued Interest", @"AccruedInterestSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Last Payment Date", @"LastPaymentDateSpan", @"span"));
                    table.Rows.Add(ControlHelper.GetNewRow("Transaction Value", @"TransactionValueSpan", @"span"));

                    transactionSummaryDiv.Controls.Add(table);
                }

                return transactionSummaryDiv;
            }
        }

        private HtmlGenericControl GetAddressInfoTab()
        {
            using (HtmlGenericControl addressInfoDiv = ControlHelper.GetGenericControl(@"div", @"tab-pane fade"))
            {
                addressInfoDiv.ID = "addresses-and-contact-info";

                using (HtmlGenericControl h4 = ControlHelper.GetGenericControl("h4", string.Empty))
                {
                    h4.InnerText = "Address & Contact Information";

                    addressInfoDiv.Controls.Add(h4);
                }

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "table table-bordered table-hover");
                    table.Rows.Add(ControlHelper.GetNewRow("Default Address", @"AddressDiv", "div"));
                    table.Rows.Add(ControlHelper.GetNewRow("Shipping Address(es)", @"ShippingAddressesDiv", "div"));
                    addressInfoDiv.Controls.Add(table);
                }

                return addressInfoDiv;
            }
        }
    }
}