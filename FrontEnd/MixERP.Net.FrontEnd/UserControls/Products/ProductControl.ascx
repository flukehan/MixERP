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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductControl" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<style type="text/css">
    table input[type=radio] {
        margin: 4px;
    }

    #ProductGridView th:nth-child(1) {
        width: 100px;
    }

    #ProductGridView th:nth-child(2) {
        width: 300px;
    }

    #ProductGridView th:nth-child(3) {
        width: 70px;
    }

    #ProductGridView th:nth-child(4) {
        width: 140px;
    }

    #ProductGridView th:nth-child(5) {
        width: 140px;
    }

    #ProductGridView th:nth-child(6) {
        width: 140px;
    }

    #ProductGridView th:nth-child(7) {
        width: 90px;
    }

    #ProductGridView th:nth-child(8) {
        width: 120px;
    }

    #ProductGridView th:nth-child(9) {
        width: 100px;
    }

    #ProductGridView th:nth-child(10) {
        width: 140px;
    }

    #ProductGridView th:nth-child(11) {
        width: 100px;
    }
</style>

<h2>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h2>

<div class="ui segment">

    <table class="ui form">
        <tr>
            <td>
                <asp:Literal ID="DateLiteral" runat="server" />
            </td>
            <td>
                <asp:Literal ID="StoreSelectLabel" runat="server" />
            </td>
            <td>
                <asp:Literal ID="PartyCodeInputTextLabel" runat="server" />
            </td>
            <td></td>
            <td>
                <asp:Literal ID="PriceTypeSelectLabel" runat="server" />
            </td>
            <td>
                <asp:Literal ID="ReferenceNumberInputTextLabel" runat="server" />
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <mixerp:DateTextBox ID="DateTextBox" runat="server" Mode="Today" CssClass="date small" />
            </td>
            <td>
                <select id="StoreSelect" class="small" runat="server"></select>
            </td>
            <td>
                <input type="text" id="PartyCodeInputText" runat="server" title="F2" class="small" />
            </td>
            <td>
                <select id="PartySelect" title="F2" class="normal"></select>
            </td>
            <td>
                <select id="PriceTypeSelect" class="compact" runat="server"></select>
            </td>
            <td>

                <input type="text" id="ReferenceNumberInputText" runat="server" maxlength="24" class="small" />
            </td>
            <td>
                <div class="ui toggle checkbox" id="CashTransactionDiv" runat="server">
                    <input id="CashTransactionInputCheckBox" type="checkbox" checked="checked">
                    <asp:Literal ID="CashTransactionLiteral" runat="server" />
                </div>
            </td>
            <td>
                <select id="PaymentTermSelect" title="F2" class="normal" runat="server"></select>
            </td>
        </tr>
    </table>
</div>

<div id="ShippingAddressInfoDiv" runat="server" style="width: 500px;" class="ui page form">
    <div class="two fields">
        <div class="field">
            <asp:Literal ID="ShippingCompanySelectLabel" runat="server" />
            <select id="ShippingCompanySelect"></select>
        </div>
        <div class="field">
            <asp:Literal ID="ShippingAddressSelectLabel" runat="server" />
            <select id="ShippingAddressSelect"></select>
        </div>
    </div>
</div>

<div id="SalesTypeDiv" runat="server" style="width: 200px;" class="ui page form">
    <div class="field">
        <asp:Literal ID="SalesTypeSelectLabel" runat="server" />
        <select id="SalesTypeSelect" runat="server">
        </select>
    </div>
</div>

<table id="ProductGridView" class="ui table" style="min-width: 1400px; max-width: 2000px;">
    <thead>
        <tr>
            <th>
                <asp:Literal ID="ItemCodeInputTextLabel" runat="server" />
            </th>
            <th>
                <asp:Literal ID="ItemSelectLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="QuantityInputTextLabel" runat="server" />
            </th>
            <th>
                <asp:Literal ID="UnitSelectLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="PriceInputTextLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="AmountInputTextLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="DiscountInputTextLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="ShippingChargeInputTextLabel" runat="server" />
            </th>
            <th class="text-right">
                <asp:Literal ID="SubTotalInputTextLabel" runat="server" />
            </th>
            <th>
                <asp:Literal ID="TaxSelectLabel" runat="server" />
            </th>
            <th>
                <asp:Literal ID="TaxInputTextLabel" runat="server" />
            </th>
            <th>
                <asp:Literal runat="server" Text="Action" />
            </th>
        </tr>
    </thead>
    <tbody>
        <tr class="ui footer-row form">
            <td>
                <input type="text" id="ItemCodeInputText" title='<asp:Literal runat="server" Text="Alt + C" />' />
            </td>
            <td>
                <select id="ItemSelect" title='<asp:Literal runat="server" Text="Ctrl + I" />'>
                </select>
            </td>
            <td>
                <input type="text" id="QuantityInputText" class="integer text-right" title='<asp:Literal runat="server" Text="Ctrl + Q" />' value="1" />
            </td>
            <td>
                <select id="UnitSelect" title='<asp:Literal runat="server" Text="Ctrl + U" />'></select>
            </td>
            <td>
                <input type="text" id="PriceInputText" class="text-right currency" title='<asp:Literal runat="server" Text="Alt + P" />' />
            </td>
            <td>
                <input type="text" id="AmountInputText" readonly="readonly" class="text-right currency" />
            </td>
            <td>
                <input type="text" id="DiscountInputText" class="text-right currency" title='<asp:Literal runat="server" Text="Ctrl + D" />' />
            </td>
            <td>
                <input type="text" id="ShippingChargeInputText" class="currency" runat="server" readonly="readonly" />
            </td>
            <td>
                <input type="text" id="SubTotalInputText" readonly="readonly" class="text-right currency" />
            </td>
            <td>
                <select id="TaxSelect" title='<asp:Literal runat="server" Text="Ctrl + T" />'></select>
            </td>
            <td>
                <input type="text" id="TaxInputText" readonly="readonly" class="text-right currency" />
            </td>
            <td>
                <input type="button"
                    id="AddButton"
                    class="small ui button blue"
                    value='<asp:Literal runat="server" Text="Add" />'
                    title='<asp:Literal runat="server" Text="Ctrl + Return" />' />
            </td>
        </tr>
    </tbody>
</table>

<asp:Panel ID="FormPanel" runat="server" Enabled="false">
    <asp:Label ID="ErrorLabel" runat="server" CssClass="big error" />
</asp:Panel>

<h4>
    <asp:Label ID="AttachmentLabel" runat="server" Text=" Attachments (+)" CssClass="" />
</h4>

<div id="attachment" class="shade" style="display: none; padding-left: 24px;">
    <mixerp:Attachment ID="Attachment1" runat="server" />
</div>

<div class="" style="width: 500px;">
    <div class="ui page form segment">
        <div class="field" id="ShippingAddressDiv" runat="server">
            <asp:Literal ID="ShippingAddressTextAreaLabel" runat="server" />
            <textarea id="ShippingAddressTextArea" readonly="readonly"></textarea>
        </div>
        <div class="three fields">
            <div class="field">
                <asp:Literal ID="RunningTotalInputTextLabel" runat="server" />
                <input type="text" id="RunningTotalInputText" class="currency" readonly="readonly" />
            </div>
            <div class="field">
                <asp:Literal ID="TaxTotalInputTextLabel" runat="server" />
                <input type="text" id="TaxTotalInputText" class="currency" readonly="readonly" />
            </div>
            <div class="field">
                <asp:Literal ID="GrandTotalInputTextInputTextLabel" runat="server" />
                <input type="text" id="GrandTotalInputText" class="currency" readonly="readonly" />
            </div>
        </div>
        <div class="two fields" id="CashRepositoryDiv" runat="server">
            <div class="field">
                <asp:Literal ID="CashRepositorySelectLabel" runat="server" />
                <select id="CashRepositorySelect"></select>
            </div>
            <div class="field">
                <asp:Literal ID="CashRepositoryBalanceInputTextLabel" runat="server" />
                <input type="text" id="CashRepositoryBalanceInputText" class="currency" readonly="readonly" />
            </div>
        </div>
        <div class="field" id="CostCenterDiv" runat="server">
            <asp:Literal ID="CostCenterSelectLabel" runat="server" />
            <select id="CostCenterSelect"></select>
        </div>
        <div class="field" id="SalespersonDiv" runat="server">
            <asp:Literal ID="SalesPersonSelectLabel" runat="server" />
            <select id="SalesPersonSelect" runat="server"></select>
        </div>
        <div class="field">
            <asp:Literal ID="StatementReferenceTextAreaLabel" runat="server" />
            <textarea id="StatementReferenceTextArea" runat="server"></textarea>
        </div>
        <button type="button" id="SaveButton" class="small ui button red">
            <asp:Literal runat="server" Text=" Save" />
        </button>
    </div>
</div>

<asp:HiddenField ID="ItemCodeHidden" runat="server"></asp:HiddenField>
<asp:HiddenField ID="ItemIdHidden" runat="server"></asp:HiddenField>
<asp:HiddenField ID="ModeHiddenField" runat="server" />
<asp:HiddenField ID="PartyCodeHidden" runat="server" />
<asp:HiddenField ID="PartyIdHidden" runat="server" />
<asp:HiddenField ID="ProductGridViewDataHidden" runat="server" />
<asp:HiddenField ID="PriceTypeIdHidden" runat="server" />
<asp:HiddenField ID="TranIdCollectionHiddenField" runat="server"></asp:HiddenField>
<asp:HiddenField ID="UnitIdHidden" runat="server"></asp:HiddenField>
<asp:HiddenField ID="UnitNameHidden" runat="server"></asp:HiddenField>

<asp:HiddenField ID="StoreIdHidden" runat="server" />
<asp:HiddenField ID="ShipperIdHidden" runat="server" />
<asp:HiddenField ID="ShippingAddressCodeHidden" runat="server" />
<asp:HiddenField ID="SalesPersonIdHidden" runat="server" />

<p>
    <asp:Label ID="ErrorLabelBottom" runat="server" CssClass="big error" />
</p>

<script type="text/javascript">
    var isSales = ("<%= this.Book %>" === "Sales");
    var tranBook = "<%= this.GetTranBook() %>";
    var taxAfterDiscount = "<%= Switches.TaxAfterDiscount().ToString() %>";
    var verifyStock = ("<%= this.VerifyStock %>" === "True");
</script>

<script src="/Scripts/UserControls/ProductControl.js"></script>