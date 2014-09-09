<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartyControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.PartyControl" %>
<div class="grey form-inline" role="form">
    <div class="form-group form-group-sm">
        <label for="PartyDropDownList">Select Customer</label>

        <div class="form-group form-group-sm">
            <input type="text" id="PartyCodeTextBox" class="form-control input-sm" style="width: 100px;" />
        </div>

        <div class="input-group input-group-sm">
            <select id="PartyDropDownList" class="form-control"></select>
            <span class="input-group-btn">
                <button id="GoButton" class="btn btn-primary" type="button">Go!</button>
            </span>
        </div>
    </div>
</div>

<ul class="nav nav-tabs" role="tablist">
    <li class="active"><a href="#home" role="tab" data-toggle="tab">
        <asp:Literal runat="server" Text="<%$Resources:Titles, Home %>"></asp:Literal>
    </a></li>
    <li><a href="#party-summary" role="tab" data-toggle="tab">

        <asp:Literal runat="server" Text="<%$Resources:Titles, PartySummary %>"></asp:Literal>
    </a></li>
    <li><a href="#transaction-summary" role="tab" data-toggle="tab">
        <asp:Literal runat="server" Text="<%$Resources:Titles, TransactionSummary %>"></asp:Literal>
    </a></li>
    <li><a href="#addresses-and-contact-info" role="tab" data-toggle="tab">
        <asp:Literal runat="server" Text="<%$Resources:Titles, AddressAndContactInfo %>"></asp:Literal>
    </a></li>
</ul>

<!-- Tab panes -->
<div class="tab-content">
    <div class="tab-pane fade in active" id="home">
    </div>
    <div class="tab-pane fade" id="party-summary">
        <table class="table table-bordered table-hover">
            <tr>
                <td style="width: 300px;">
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:TItles, PartyType %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="PartyTypeSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, Email %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="EmailAddressSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, PanNumber %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="PANNumberSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, SSTNumber %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="SSTNumberSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, CSTNumber %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="CSTNumberSpan"></span>
                </td>
            </tr>
            <tr class="info">
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, CreditAllowed %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="CreditAllowedSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, MaxCreditPeriod %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="MaxCreditPeriodSpan"></span>
                </td>
            </tr>
            <tr class="info">
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, MaxCreditAmount %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="MaxCreditAmountSpan"></span>
                </td>
            </tr>
            <tr class="warning">
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, InterestApplicable %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="InterestApplicableSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, GLHead %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="GLHeadSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, DefaultCurrency %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="DefaultCurrencySpan"></span>
                </td>
            </tr>
        </table>
    </div>
    <div class="tab-pane  fade" id="transaction-summary">
        <table class="table table-bordered table-hover">
            <tr>
                <td style="width: 300px;">
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, TotalDueAmount %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="TotalDueAmountSpan"></span>
                </td>
            </tr>
            <tr>
                <td style="width: 300px;">
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, OfficeDueAmount %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="OfficeDueAmountSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, AccruedInterest %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="AccruedInterestSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, LastPaymentDate %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <span id="LastPaymentDateSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, TransactionValue %>"></asp:Literal>
                </strong>
                </td>
                <td>
                    <span id="TransactionValueSpan"></span>
                </td>
            </tr>
        </table>
    </div>
    <div class="tab-pane  fade" id="addresses-and-contact-info">
        <h4>Address</h4>

        <table class="table table-bordered table-hover">
            <tr>
                <td style="width: 300px;">
                    <strong>
                        <asp:Literal runat="server" Text="<%$Resources:Titles, Address %>"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <div id="AddressDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="<%$Resources:Titles, ShippingAddresses %>"></asp:Literal>
                </strong>
                </td>

                <td>
                    <div id="ShippingAddressesDiv">
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<script src="/Scripts/UserControls/PartyControl.js"></script>