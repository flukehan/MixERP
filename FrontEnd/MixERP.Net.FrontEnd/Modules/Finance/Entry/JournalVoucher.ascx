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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs"
    Inherits="MixERP.Net.Core.Modules.Finance.Entry.JournalVoucher"
    OverridePath="/Modules/Finance/JournalVoucher.mix" %>
<%@ Register TagPrefix="mixerp" Namespace="MixERP.Net.WebControls.Common" Assembly="MixERP.Net.WebControls.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a724a47a0879d02f" %>
<%@ Register TagPrefix="mixerp" TagName="Attachment" Src="~/UserControls/AttachmentUserControl.ascx" %>

<h2>
    <asp:Label ID="TitleLabel" runat="server" />
</h2>

<div class="ui tiny form segment">
    <div class="two fields">
        <div class="field">
            <label for="ValueDateTextBox">
                <asp:Literal ID="ValueDateLiteral" runat="server" />
            </label>
            <mixerp:DateTextBox ID="ValueDateTextBox" runat="server" Mode="Today" CssClass="date" />
        </div>
        <div class="field">
            <label for="ReferenceNumberInputText">
                <asp:Literal ID="ReferenceNumberLiteral" runat="server" />
            </label>
            <input type="text" id="ReferenceNumberInputText" runat="server" />
        </div>
    </div>
</div>

<input type="hidden" id="TransactionGridViewHidden" />

<table id="TransactionGridView" class="ui table">
    <thead>
        <tr>
            <th style="width: 200px;">
                <label for="StatementReferenceInputText">
                    <asp:Literal runat="server" ID="StatementReferenceLiteral" />
                </label>
            </th>
            <th scope="col" style="width: 100px;">
                <label for="AccountCodeInputText">
                    <asp:Literal runat="server" ID="AccountCodeLiteral" />
                </label>
            </th>
            <th style="width: 250px;">
                <label for="AccountSelect">
                    <asp:Literal runat="server" ID="AccountLiteral" />
                </label>
            </th>
            <th style="width: 160px;">
                <label for="CashRepositorySelect">
                    <asp:Literal runat="server" ID="CashRepositoryLiteral" />
                </label>
            </th>
            <th style="width: 120px;">
                <label for="CurrencySelect">
                    <asp:Literal runat="server" ID="CurrencyLiteral" />
                </label>
            </th>
            <th class="text-right" style="width: 100px;">
                <label for="DebitInputText">
                    <asp:Literal runat="server" ID="DebitLiteral" />
                </label>
            </th>
            <th class="text-right" style="width: 100px;">
                <label for="CreditInputText">
                    <asp:Literal runat="server" ID="CreditLiteral" />
                </label>
            </th>
            <th class="text-right" style="width: 80px;">
                <label for="ERInputText">
                    <asp:Literal runat="server" ID="ERLiteral" />
                </label>
            </th>
            <th class="text-right" style="width: 100px;">
                <label for="LCDebitInputText">
                    <asp:Literal runat="server" ID="LCDebitLiteral" />
                </label>
            </th>
            <th class="text-right" style="width: 100px;">
                <label for="LCCreditInputText">
                    <asp:Literal runat="server" ID="LCCreditLiteral" />
                </label>
            </th>
            <th style="width: 100px;">
                <asp:Literal runat="server" ID="ActionLiteral" />
            </th>
        </tr>
    </thead>
    <tbody>
        <tr class="ui form footer-row">
            <td>
                <input type="text" id="StatementReferenceInputText" title='Ctrl + Alt +S' />
            </td>
            <td>
                <input type="text" id="AccountCodeInputText" title='Ctrl + Alt + T' />
            </td>
            <td>
                <select id="AccountSelect" title='Ctrl + Alt + A'></select>
            </td>
            <td>
                <select id="CashRepositorySelect"></select>
            </td>
            <td>
                <select id="CurrencySelect"></select>
            </td>
            <td>
                <input type="text" id="DebitInputText" class="text-right currency" title='Ctrl + Alt + D' />
            </td>
            <td>
                <input type="text" id="CreditInputText" class="text-right currency" title='Ctrl + Alt + C' />
            </td>
            <td>
                <input type="text" id="ERInputText" class="text-right decimal" />
            </td>
            <td>
                <input type="text" id="LCDebitInputText" class="text-right currency" readonly="readonly" title='Ctrl + Alt + D' />
            </td>
            <td>
                <input type="text" id="LCCreditInputText" class="text-right currency" readonly="readonly" title='Ctrl + Alt + C' />
            </td>
            <td>
                <input type="button" id="AddInputButton" runat="server" class="ui small blue button" title='Ctrl + Return' />
            </td>
        </tr>
    </tbody>
</table>

<h4>
    <asp:Label ID="AttachmentLabel" runat="server" Text="Attachments" />
</h4>
<div id="AttachmentDiv" class="grey" style="display: none; padding-left: 24px;">
    <mixerp:Attachment ID="Attachment1" runat="server" />
</div>

<div class="ui tiny form segment">
    <div class="field">
        <label for="CostCenterDropDownList">
            <asp:Literal ID="CostCenterLiteral" runat="server" />
        </label>
        <select name="CostCenterDropDownList"
            id="CostCenterDropDownList">
        </select>
    </div>
    <div class="field">
        <label for="DebitTotalTextBox">
            <asp:Literal ID="DebitTotalLiteral" runat="server" />
        </label>
        <input type="text"
            id="DebitTotalTextBox"
            readonly="readonly"
            class="text-right currency" />
    </div>
    <div class="field">
        <label for="CreditTotalLiteral">
            <asp:Literal ID="CreditTotalLiteral" runat="server" />
        </label>
        <input type="text"
            id="CreditTotalTextBox"
            readonly="readonly"
            class="text-right currency" />
    </div>
    <button id="PostButton" type="button" class="ui small positive button">
        <asp:Literal runat="server" ID="PostTransactionLiteral" />
    </button>
</div>
<asp:Label runat="server" ID="ErrorLabelBottom" CssClass="big error"></asp:Label>

<script src="../Scripts/Entry/JournalVoucher.js"></script>