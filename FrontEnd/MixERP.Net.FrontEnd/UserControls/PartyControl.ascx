<%--
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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>

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
        <asp:Literal runat="server" Text="Home" />
    </a></li>
    <li><a href="#party-summary" role="tab" data-toggle="tab">

        <asp:Literal runat="server" Text="Party Summary" />
    </a></li>
    <li><a href="#transaction-summary" role="tab" data-toggle="tab">
        <asp:Literal runat="server" Text="Transaction Summary" />
    </a></li>
    <li><a href="#addresses-and-contact-info" role="tab" data-toggle="tab">
        <asp:Literal runat="server" Text="Address & Contact Information" />
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
                        <asp:Literal runat="server" Text="Party Type" />
                    </strong>
                </td>
                <td>
                    <span id="PartyTypeSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="Email" />
                    </strong>
                </td>
                <td>
                    <span id="EmailAddressSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="PAN Number" />
                    </strong>
                </td>
                <td>
                    <span id="PANNumberSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="SST Number" />
                    </strong>
                </td>
                <td>
                    <span id="SSTNumberSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="CST Number" />
                    </strong>
                </td>
                <td>
                    <span id="CSTNumberSpan"></span>
                </td>
            </tr>
            <tr class="info">
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="Credit Allowed" />
                    </strong>
                </td>
                <td>
                    <span id="CreditAllowedSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="Maximum Credit Period" />
                    </strong>
                </td>
                <td>
                    <span id="MaxCreditPeriodSpan"></span>
                </td>
            </tr>
            <tr class="info">
                <td><strong>
                    <asp:Literal runat="server" Text="Maximum Credit Amount" />
                </strong>
                </td>
                <td>
                    <span id="MaxCreditAmountSpan"></span>
                </td>
            </tr>
            <tr class="warning">
                <td><strong>
                    <asp:Literal runat="server" Text="Interest Applicable" />
                </strong>
                </td>
                <td>
                    <span id="InterestApplicableSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="GL Head" />
                </strong>
                </td>
                <td>
                    <span id="GLHeadSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="Default Currency" />
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
                        <asp:Literal runat="server" Text="Total Due Amount" />
                    </strong>
                </td>
                <td>
                    <span id="TotalDueAmountSpan"></span>
                </td>
            </tr>
            <tr>
                <td style="width: 300px;">
                    <strong>
                        <asp:Literal runat="server" Text="Due Amount (Currenct Office)" />
                    </strong>
                </td>
                <td>
                    <span id="OfficeDueAmountSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="Accrued Interest" />
                </strong>
                </td>
                <td>
                    <span id="AccruedInterestSpan"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal runat="server" Text="Last Payment Date" />
                    </strong>
                </td>
                <td>
                    <span id="LastPaymentDateSpan"></span>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="Transaction Value" />
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
                        <asp:Literal runat="server" Text="Address" />
                    </strong>
                </td>
                <td>
                    <div id="AddressDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Literal runat="server" Text="Shipping Addresses" />
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