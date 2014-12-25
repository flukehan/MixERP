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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountStatement.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.AccountStatement" %>

<style type="text/css">
    #AccountOverViewGrid td:nth-child(1),
    #AccountOverViewGrid th:nth-child(1) {
        width: 200px;
    }
</style>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<div class="ui top attached tabular menu">
    <a class="active item" data-tab="first">Transaction Statement</a>
    <a class="item" data-tab="second">Account Overview</a>
</div>
<div class="ui bottom attached active form tab segment" data-tab="first">
    <asp:PlaceHolder runat="server" ID="FormPlaceholder"></asp:PlaceHolder>

    <div class="auto-overflow-panel">
        <asp:GridView runat="server" ID="StatementGridView" CssClass="ui celled table nowrap" GridLines="None" BorderStyle="None">
        </asp:GridView>
    </div>
</div>
<div class="ui bottom attached tab segment" data-tab="second">
    <h2>Liabilities</h2>
    <div class="description">
        Description
    </div>

    <table class="ui definition table" id="AccountOverViewGrid">
        <thead>
            <tr>
                <th></th>
                <th>Definition</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Account Number</td>
                <td>
                    <asp:Literal runat="server" ID="AccountNumberLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>External Code</td>
                <td>
                    <asp:Literal runat="server" ID="ExternalCodeLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Base Currency</td>
                <td>
                    <asp:Literal runat="server" ID="BaseCurrencyLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Account Master</td>
                <td>
                    <asp:Literal runat="server" ID="AccountMasterLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Confidential</td>
                <td>
                    <asp:Literal runat="server" ID="ConfidentialLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Cashflow Heading</td>
                <td>
                    <asp:Literal runat="server" ID="CashFlowHeadingLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Is System Account</td>
                <td>
                    <asp:Literal runat="server" ID="IsSystemAccountLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Is Cash</td>
                <td>
                    <asp:Literal runat="server" ID="IsCashAccountLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Is Employee</td>
                <td>
                    <asp:Literal runat="server" ID="IsEmployeeLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Is Party</td>
                <td>
                    <asp:Literal runat="server" ID="IsPartyLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Normally Debit</td>
                <td>
                    <asp:Literal runat="server" ID="NormallyDebitLiteral"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Parent Account</td>
                <td>
                    <asp:Literal runat="server" ID="ParentAccountLiteral"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
</div>

<asp:PlaceHolder runat="server" ID="FlagPlaceholder"></asp:PlaceHolder>