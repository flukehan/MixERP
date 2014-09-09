<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Entry.JournalVoucher" %>

<h2>
    <asp:Label ID="TitleLabel" runat="server" />
</h2>

<div class="form form-inline grey" style="padding: 24px; max-width: 600px;" role="form">
    <div class="form-group">
        <label for="ValueDateTextBox">
            <asp:Literal ID="ValueDateLiteral" runat="server" />
        </label>
        <asp:TextBox ID="ValueDateTextBox" runat="server" Width="100" CssClass="date form-control input-sm" />
    </div>
    <div class="form-group">
        <label for="ReferenceNumberTextBox">
            <asp:Literal ID="ReferenceNumberLiteral" runat="server" />
        </label>
        <asp:TextBox ID="ReferenceNumberTextBox" runat="server" CssClass="form-control input-sm" />
    </div>
</div>

<input type="hidden" id="TransactionGridViewHidden" />
<table id="TransactionGridView" class="table table-hover" runat="server">
    <tbody>
        <tr>
            <th style="width: 200px;">
                <asp:Literal runat="server" ID="StatementReferenceLiteral" />
            </th>
            <th scope="col" style="width: 100px;">
                <asp:Literal runat="server" ID="AccountCodeLiteral" />
            </th>
            <th style="width: 250px;">
                <asp:Literal runat="server" ID="AccountLiteral" />
            </th>
            <th style="width: 160px;">
                <asp:Literal runat="server" ID="CashRepositoryLiteral" />
            </th>
            <th style="width: 120px;">
                <asp:Literal runat="server" ID="CurrencyLiteral" />
            </th>
            <th class="text-right" style="width: 100px;">
                <asp:Literal runat="server" ID="DebitLiteral" />
            </th>
            <th class="text-right" style="width: 100px;">
                <asp:Literal runat="server" ID="CreditLiteral" />
            </th>
            <th class="text-right" style="width: 80px;">
                <asp:Literal runat="server" ID="ERLiteral" />
            </th>
            <th class="text-right" style="width: 100px;">
                <asp:Literal runat="server" ID="LCDebitLiteral" />
            </th>
            <th class="text-right" style="width: 100px;">
                <asp:Literal runat="server" ID="LCCreditLiteral" />
            </th>
            <th style="width: 100px;">
                <asp:Literal runat="server" ID="ActionLiteral" />
            </th>
        </tr>
        <tr class="footer-row">
            <td>
                <input type="text"
                    id="StatementReferenceTextBox"
                    class="form-control input-sm"
                    title='Ctrl + Alt +S' />
            </td>
            <td>
                <input type="text"
                    id="AccountCodeTextBox"
                    title='Ctrl + Alt + T'
                    class="form-control input-sm" />
            </td>
            <td>
                <select name="AccountDropDownList"
                    id="AccountDropDownList"
                    class="form-control  input-sm"
                    title='Ctrl + Alt + A'>
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
                    title='Ctrl + Alt + D' />
            </td>
            <td>
                <input type="text"
                    id="CreditTextBox"
                    class="text-right currency form-control input-sm"
                    title='Ctrl + Alt + C' />
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
                    title='Ctrl + Alt + D' />
            </td>
            <td>
                <input type="text"
                    id="LCCreditTextBox"
                    class="text-right currency form-control input-sm"
                    disabled="disabled"
                    title='Ctrl + Alt + C' />
            </td>
            <td>
                <input type="button"
                    id="AddButton"
                    runat="server"
                    class="btn btn-sm btn-default"
                    title='Ctrl + Return' />
            </td>
        </tr>
    </tbody>
</table>

<h4>
    <asp:Label ID="AttachmentLabel" runat="server" />
</h4>
<div id="AttachmentDiv" class="grey" style="display: none; padding-left: 24px;">
    <mixerp:Attachment ID="Attachment1" runat="server" />
</div>

<div class="grey" role="form" style="max-width: 600px; padding: 24px;">
    <div class="form-group">
        <label for="CostCenterDropDownList">
            <asp:Literal ID="CostCenterLiteral" runat="server" />
        </label>
        <select name="CostCenterDropDownList"
            id="CostCenterDropDownList"
            class="form-control  input-sm">
        </select>
    </div>
    <div class="form-group">
        <label for="DebitTotalTextBox">
            <asp:Literal ID="DebitTotalLiteral" runat="server" />
        </label>
        <input type="text"
            id="DebitTotalTextBox"
            readonly="readonly"
            class="text-right currency form-control input-sm" />
    </div>
    <div class="form-group">
        <label for="CreditTotalLiteral">
            <asp:Literal ID="CreditTotalLiteral" runat="server" />
        </label>
        <input type="text"
            id="CreditTotalTextBox"
            readonly="readonly"
            class="text-right currency form-control input-sm" />
    </div>
    <button id="PostButton" type="button" class="btn btn-primary btn-sm">
        <asp:Literal runat="server" ID="PostTransactionLiteral" />
    </button>
</div>
<asp:Label runat="server" ID="ErrorLabelBottom" CssClass="error"></asp:Label>

<script src="../Scripts/Entry/JournalVoucher.js"></script>