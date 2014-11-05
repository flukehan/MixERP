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

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private void AddHiddenField(System.Web.UI.WebControls.Panel p, string id)
        {
            using (System.Web.UI.WebControls.HiddenField hidden = new System.Web.UI.WebControls.HiddenField())
            {
                hidden.ID = id;
                p.Controls.Add(hidden);
            }
        }

        private void AddTabBody(Panel p)
        {
            using (HtmlGenericControl homeTab = ControlHelper.GetGenericControl(@"div", "ui active tab bottom stacked attached segment"))
            {
                homeTab.Attributes.Add("data-tab", "home");
                homeTab.ID = "home";
                p.Controls.Add(homeTab);
            }

            p.Controls.Add(this.GetPartySummaryTab());
            p.Controls.Add(this.GetTransactionSummaryTab());
            p.Controls.Add(this.GetAddressInfoTab());
        }

        private HtmlGenericControl GetPartySummaryTab()
        {
            using (HtmlGenericControl partSummaryDiv = ControlHelper.GetGenericControl(@"div", @"ui tab bottom stacked attached green segment"))
            {
                partSummaryDiv.Attributes.Add("data-tab", "party-summary");

                partSummaryDiv.ID = "party-summary";

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "ui table segment");
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
            using (HtmlGenericControl transactionSummaryDiv = ControlHelper.GetGenericControl(@"div", @"ui tab bottom stacked attached red segment"))
            {
                transactionSummaryDiv.Attributes.Add("data-tab", "transaction-summary");

                transactionSummaryDiv.ID = "transaction-summary";

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "ui table segment");
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
            using (HtmlGenericControl addressInfoDiv = ControlHelper.GetGenericControl(@"div", @"ui tab bottom stacked attached teal segment"))
            {
                addressInfoDiv.Attributes.Add("data-tab", "contact-info");
                addressInfoDiv.ID = "addresses-and-contact-info";

                using (HtmlGenericControl h3 = ControlHelper.GetGenericControl("h3", "ui header"))
                {
                    h3.InnerHtml = "  <i class='globe icon'></i>Address & Contact Information";

                    addressInfoDiv.Controls.Add(h3);
                }

                using (HtmlTable table = new HtmlTable())
                {
                    table.Attributes.Add("class", "ui table segment");
                    table.Rows.Add(ControlHelper.GetNewRow("Default Address", @"AddressDiv", "div"));
                    table.Rows.Add(ControlHelper.GetNewRow("Shipping Address(es)", @"ShippingAddressesDiv", "div"));
                    addressInfoDiv.Controls.Add(table);
                }

                return addressInfoDiv;
            }
        }
    }
}