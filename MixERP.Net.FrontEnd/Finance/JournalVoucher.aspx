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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="JournalVoucher.aspx.cs" Inherits="MixERP.Net.FrontEnd.Finance.JournalVoucher" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h2>
        <asp:Label ID="TitleLabel" runat="server" Text="<%$Resources:Titles, JournalVoucherEntry %>" />
    </h2>


    <div class="form form-inline grey" style="padding: 24px; max-width: 600px;" role="form">
        <div class="form-group">
            <label for="ValueDateTextBox">
                <asp:Literal ID="ValueDateLiteral" runat="server" Text="<%$Resources:Titles, ValueDate %>" />
            </label>
            <mixerp:DateTextBox ID="ValueDateTextBox" runat="server" Width="100" CssClass="date form-control input-sm" />

        </div>
        <div class="form-group">
            <label for="ReferenceNumberTextBox">
                <asp:Literal ID="ReferenceNumberLiteral" runat="server" Text="<%$Resources:Titles, ReferenceNumber %>" />
            </label>
            <asp:TextBox ID="ReferenceNumberTextBox" runat="server" CssClass="form-control input-sm" />
        </div>
    </div>

    <input type="hidden" id="TransactionGridViewHidden" />
    <table id="TransactionGridView" class="table table-hover" runat="server">
        <tbody>
            <tr>
                <th style="width: 200px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,StatementReference%>" />
                </th>
                <th scope="col" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,AccountCode %>" />
                </th>
                <th style="width: 250px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Account %>" />
                </th>
                <th style="width: 160px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,CashRepository%>" />
                </th>
                <th style="width: 120px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Currency %>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Debit%>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Credit%>" />
                </th>
                <th class="text-right" style="width: 80px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,ER%>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,LCDebit%>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,LCCredit%>" />
                </th>
                <th style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Action %>" />
                </th>
            </tr>
            <tr class="footer-row">
                <td>
                    <input type="text"
                        id="StatementReferenceTextBox"
                        class="form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltS%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="AccountCodeTextBox"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltT%>" />'
                        class="form-control input-sm" />
                </td>
                <td>
                    <select name="AccountDropDownList"
                        id="AccountDropDownList"
                        class="form-control  input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltA%>" />'>
                    </select>
                </td>
                <td>
                    <select name="CashRepositoryDropDownList"
                        id="CashRepositoryDropDownList"
                        class="form-control  input-sm">
                    </select>
                </td>
                <td>
                    <select name="CurrencyDropDownList"
                        id="CurrencyDropDownList"
                        class="form-control  input-sm">
                    </select>
                </td>
                <td>
                    <input type="text"
                        id="DebitTextBox"
                        class="text-right currency form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltD%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="CreditTextBox"
                        class="text-right currency form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltC%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="ERTextBox"
                        class="text-right float form-control input-sm" />
                </td>
                <td>
                    <input type="text"
                        id="LCDebitTextBox"
                        class="text-right currency form-control input-sm"
                        disabled="disabled"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltD%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="LCCreditTextBox"
                        class="text-right currency form-control input-sm"
                        disabled="disabled"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlAltC%>" />' />
                </td>
                <td>
                    <input type="button"
                        id="AddButton"
                        class="btn btn-sm btn-default"
                        value='<asp:Literal runat="server" Text="<%$Resources:Titles,Add%>" />'
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlReturn%>" />' />
                </td>
            </tr>
        </tbody>
    </table>

    <h4>
        <asp:Label ID="AttachmentLabel" runat="server" Text="<%$ Resources:Titles, AttachmentsPlus %>" />
    </h4>
    <div id="AttachmentDiv" class="grey" style="display: none; padding-left: 24px;">
        <mixerp:Attachment ID="Attachment1" runat="server" />
    </div>


    <div class="grey" role="form" style="max-width: 600px; padding: 24px;">
        <div class="form-group">
            <label for="CostCenterDropDownList">
                <asp:Literal ID="CostCenterLiteral" runat="server" Text="<%$Resources:Titles, CostCenter %>" />
            </label>
            <select name="CostCenterDropDownList"
                id="CostCenterDropDownList"
                class="form-control  input-sm">
            </select>

        </div>
        <div class="form-group">
            <label for="DebitTotalTextBox">
                <asp:Literal ID="DebitTotalLiteral" runat="server" Text="<%$Resources:Titles, DebitTotal %>" />
            </label>
            <input type="text"
                id="DebitTotalTextBox"
                readonly="readonly"
                class="text-right currency form-control input-sm" />
        </div>
        <div class="form-group">
            <label for="CreditTotalLiteral">
                <asp:Literal ID="CreditTotalLiteral" runat="server" Text="<%$Resources:Titles, CreditTotal %>" />
            </label>
            <input type="text"
                id="CreditTotalTextBox"
                readonly="readonly"
                class="text-right currency form-control input-sm" />

        </div>
        <button id="PostButton" type="button" class="btn btn-primary btn-sm">
            <asp:Literal runat="server" Text="<%$Resources:Titles, PostTransaction %>"></asp:Literal>
        </button>

    </div>
    <asp:Label runat="server" ID="ErrorLabelBottom" CssClass="error"></asp:Label>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script src="/Scripts/Finance/JournalVoucher.js"></script>
</asp:Content>
