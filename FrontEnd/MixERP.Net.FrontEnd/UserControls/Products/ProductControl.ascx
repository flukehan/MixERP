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
        width: 90px;
    }

    #ProductGridView th:nth-child(2) {
        width: 300px;
    }

    #ProductGridView th:nth-child(3) {
        width: 70px;
    }

    #ProductGridView th:nth-child(4) {
        width: 120px;
    }

    #ProductGridView th:nth-child(5) {
        width: 120px;
    }

    #ProductGridView th:nth-child(6) {
        width: 120px;
    }

    #ProductGridView th:nth-child(7) {
        width: 90px;
    }

    #ProductGridView th:nth-child(8) {
        width: 120px;
    }

    #ProductGridView th:nth-child(9) {
        width: 70px;
    }

    #ProductGridView th:nth-child(10) {
        width: 100px;
    }

    #ProductGridView th:nth-child(11) {
        width: 120px;
    }
</style>

<h2>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h2>

<div class="ui segment">

    <div class="ui form">
        <div class="inline fields">

            <div class="field">
                <asp:Literal ID="DateLiteral" runat="server" />
                <mixerp:DateTextBox ID="DateTextBox" runat="server" Mode="Today" CssClass="date" />
            </div>

            <div class="field" id="StoreDiv" runat="server">
                <asp:Literal ID="StoreSelectLabel" runat="server" />
                <select id="StoreSelect"></select>
            </div>

            <div class="field">
                <asp:Literal ID="PartyCodeInputTextLabel" runat="server" />
                <input type="text" id="PartyCodeInputText" runat="server" title="F2" />
            </div>

            <div class="field">
                <select id="PartySelect" title="F2"></select>
            </div>

            <div class="field" id="PriceTypeDiv" runat="server">
                <asp:Literal ID="PriceTypeSelectLabel" runat="server" />
                <select id="PriceTypeSelect"></select>
            </div>

            <div class="field">
                <asp:Literal ID="ReferenceNumberInputTextLabel" runat="server" />
                <input type="text" id="ReferenceNumberInputText" runat="server" maxlength="24" />
            </div>

            <div class="field" id="CashTransactionDiv" runat="server">
                <div class="ui toggle checkbox">
                    <input id="CashTransactionInputCheckBox" type="checkbox" checked="checked">
                    <asp:Literal ID="CashTransactionLiteral" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>

<div>
    <table id="ProductGridView" class="ui form celled table segment" runat="server">
        <tbody>
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
                    <asp:Literal ID="SubTotalInputTextLabel" runat="server" />
                </th>
                <th class="text-right">
                    <asp:Literal ID="TaxRateInputTextLabel" runat="server" />
                </th>
                <th class="text-right">
                    <asp:Literal ID="TaxInputTextLabel" runat="server" />
                </th>
                <th class="text-right">
                    <asp:Literal ID="TotalAmountInputTextLabel" runat="server" />
                </th>
                <th>
                    <asp:Literal runat="server" Text="Action" />
                </th>
            </tr>
            <tr class="footer-row">
                <td>
                    <input type="text" id="ItemCodeInputText" title='<asp:Literal runat="server" Text="Alt + C" />' />
                </td>
                <td>
                    <select name="ItemSelect" id="ItemSelect" title='<asp:Literal runat="server" Text="Ctrl + I" />'>
                    </select>
                </td>
                <td>
                    <input type="text" id="QuantityInputText" class="integer text-right" title='<asp:Literal runat="server" Text="Ctrl + Q" />' value="1" />
                </td>
                <td>
                    <select name="UnitSelect" id="UnitSelect" title='<asp:Literal runat="server" Text="Ctrl + U" />'></select>
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
                    <input type="text" id="SubTotalInputText" readonly="readonly" class="text-right currency" />
                </td>
                <td>
                    <input type="text" id="TaxRateInputText" class="text-right" />
                </td>
                <td>
                    <input type="text" id="TaxInputText" class="text-right currency" title='<asp:Literal runat="server" Text="Ctrl + T" />' />
                </td>
                <td>
                    <input type="text" id="TotalAmountInputText" readonly="readonly" class="text-right currency" />
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
        <asp:Label ID="ErrorLabel" runat="server" CssClass="error" />
    </asp:Panel>

    <h4>
        <asp:Label ID="AttachmentLabel" runat="server" Text=" Attachments (+)" CssClass="" />
    </h4>

    <div id="attachment" class="shade" style="display: none; padding-left: 24px;">
        <mixerp:Attachment ID="Attachment1" runat="server" />
    </div>

    <div class="" style="width: 500px;">
        <div class="ui page form segment">
            <div id="ShippingAddressDiv" runat="server">
                <div class="field">
                    <asp:Literal ID="ShippingAddressSelectLabel" runat="server" />
                    <select id="ShippingAddressSelect"></select>
                </div>
                <div class="field">
                    <textarea id="ShippingAddressTextArea" readonly="readonly"></textarea>
                </div>
            </div>
            <div class="two fields">
                <div class="field" id="ShippingCompanyDiv" runat="server">
                    <asp:Literal ID="ShippingCompanySelectLabel" runat="server" />
                    <select id="ShippingCompanySelect"></select>
                </div>
                <div class="field" id="ShippingChargeDiv" runat="server">
                    <asp:Literal ID="ShippingChargeInputTextLabel" runat="server" />
                    <input type="text" id="ShippingChargeInputText" class="currency" />
                </div>
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
                    <asp:Literal ID="GrandTotalInputTextLabel" runat="server" />
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
    <asp:HiddenField ID="ShippingAddressCodeHidden" runat="server" />
    <asp:HiddenField ID="TranIdCollectionHiddenField" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="UnitIdHidden" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="UnitNameHidden" runat="server"></asp:HiddenField>
    <p>
        <asp:Label ID="ErrorLabelBottom" runat="server" CssClass="big error" />
    </p>
</div>

<script type="text/javascript">
    var isSales = ("<%= this.Book %>" == "Sales");
    var tranBook = "<%= this.GetTranBook() %>";
    var taxAfterDiscount = "<%= Switches.TaxAfterDiscount().ToString() %>";
    var verifyStock = ("<%= this.VerifyStock %>" == "True");
</script>

<script src="/Scripts/UserControls/ProductControl.js"></script>
