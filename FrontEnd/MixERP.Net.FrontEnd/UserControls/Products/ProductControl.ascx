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
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<style type="text/css">
    table input[type=radio] {
        margin: 4px;
    }

    #ProductGridView th:nth-child(1) {
        width: 7%;
    }

    #ProductGridView th:nth-child(2) {
        width: 21%;
    }

    #ProductGridView th:nth-child(3) {
        width: 5%;
    }

    #ProductGridView th:nth-child(4) {
        width: 10%;
    }

    #ProductGridView th:nth-child(5) {
        width: 10%;
    }

    #ProductGridView th:nth-child(6) {
        width: 10%;
    }

    #ProductGridView th:nth-child(7) {
        width: 6%;
    }

    #ProductGridView th:nth-child(8) {
        width: 7%;
    }

    #ProductGridView th:nth-child(9) {
        width: 7%;
    }

    #ProductGridView th:nth-child(10) {
        width: 10%;
    }

    #ProductGridView th:nth-child(11) {
        width: 7%;
    }

    table.form {
        width: 100%;
    }

        table.form td:nth-child(1), table.form td:nth-child(2), table.form td:nth-child(3), table.form td:nth-child(6) {
            width: 10%;
        }

        table.form td:nth-child(5) {
            max-width: 10%;
        }

        table.form td:nth-child(4) {
            width: 24%;
        }


        table.form .ui.toggle.checkbox {
            width: 150px;
        }
</style>

<h2>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h2>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<table id="ProductGridView" class="ui table" style="max-width: 2000px;">
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


<asp:Label ID="ErrorLabel" runat="server" CssClass="big error" />

<h2>
    <asp:Label ID="AttachmentLabel" runat="server" Text=" Attachments (+)" CssClass="" />
</h2>

<div id="attachment" class="ui segment initially hidden">
    <mixerp:Attachment ID="Attachment1" runat="server"/>
</div>


<asp:PlaceHolder runat="server" ID="Placeholder2"></asp:PlaceHolder>



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
