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

    table.input-sm {
        margin: 4px 0 4px 0 !important;
    }

        table.input-sm tr {
            vertical-align: bottom;
        }
</style>

<div id="info-panel">
    <asp:Literal runat="server" Text="<%$Resources:Titles,ShortCuts %>"></asp:Literal>
    <hr style="border-color: #97d300;" />
    <table>
        <tr>
            <td>F2
            </td>
            <td>
                <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewParty %>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>F4
            </td>
            <td>
                <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewItem %>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>CTRL + RET
            </td>
            <td>
                <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewRow%>"></asp:Literal>
            </td>
        </tr>
    </table>
</div>

<h2>
    <asp:Label ID="TitleLabel" runat="server" />
</h2>

<div class="grey">
    <div class="row" style="margin-left: 8px;">
        <div class="col-md-1 pad4">
            <div class="form-group">
                <asp:Literal ID="DateLiteral" runat="server" />
                <mixerp:DateTextBox ID="DateTextBox" runat="server" Mode="Today" CssClass="date form-control input-sm" />
            </div>
        </div>

        <div class="col-md-1 pad4" id="StoreDiv" runat="server">
            <div class="form-group">
                <asp:Literal ID="StoreLiteral" runat="server" />
                <asp:DropDownList ID="StoreDropDownList" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-1 pad4">
            <div class="form-group">
                <asp:Literal ID="PartyLiteral" runat="server" />
                <asp:TextBox ID="PartyCodeTextBox" runat="server"
                    ToolTip="F2" CssClass="form-control  input-sm" />
            </div>
        </div>

        <div class="col-md-2 pad4">
            <asp:Literal ID="Literal1" runat="server" Text="<label>&nbsp;</label>" />
            <asp:DropDownList ID="PartyDropDownList" runat="server"
                ToolTip="F2" CssClass="form-control  input-sm">
            </asp:DropDownList>
        </div>

        <div class="col-md-1 pad4" id="PriceTypeDiv" runat="server">
            <div class="form-group">
                <asp:Literal ID="PriceTypeLiteral" runat="server" />
                <asp:DropDownList ID="PriceTypeDropDownList" runat="server" CssClass="form-control  input-sm">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-1 pad4">
            <div class="form-group">
                <asp:Literal ID="ReferenceNumberLiteral" runat="server" />
                <asp:TextBox ID="ReferenceNumberTextBox" runat="server" MaxLength="24" CssClass="form-control  input-sm" />
            </div>
        </div>

        <div class="col-md-2" id="TransactionTypeDiv" runat="server">
            <div class="form-group">
                <asp:Literal ID="TransactionTypeLiteral" runat="server" />
                <asp:RadioButtonList ID="TransactionTypeRadioButtonList" runat="server" CssClass="input-sm" RepeatDirection="Horizontal">
                    <asp:ListItem Text="<%$ Resources:Titles, Cash %>" Value="<%$ Resources:Titles, Cash %>" Selected="True" />
                    <asp:ListItem Text="<%$ Resources:Titles, Credit %>" Value="<%$ Resources:Titles, Credit %>" />
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="col-md-3">
        </div>
    </div>
</div>

<div>
    <table id="ProductGridView" class="table table-hover" runat="server">
        <tbody>
            <tr>
                <th scope="col" style="width: 90px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Code %>" />
                </th>
                <th style="width: 300px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,ItemName %>" />
                </th>
                <th class="text-right" style="width: 50px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,QuantityAbbreviated %>" />
                </th>
                <th style="width: 120px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Unit%>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Price %>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Amount %>" />
                </th>
                <th class="text-right" style="width: 50px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Discount %>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,SubTotal %>" />
                </th>
                <th class="text-right" style="width: 60px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Rate %>" />
                </th>
                <th class="text-right" style="width: 100px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Tax %>" />
                </th>
                <th class="text-right" style="width: 110px;">
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Total %>" />
                </th>
                <th>
                    <asp:Literal runat="server" Text="<%$ Resources:Titles,Action %>" />
                </th>
            </tr>
            <tr class="footer-row">
                <td>
                    <input type="text"
                        id="ItemCodeTextBox"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltC%>" />'
                        class="form-control input-sm" />
                </td>
                <td>
                    <select name="ItemDropDownList"
                        id="ItemDropDownList"
                        class="form-control  input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlI%>" />'>
                    </select>
                </td>
                <td>
                    <input type="text"
                        id="QuantityTextBox"
                        class="form-control text-right input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlQ%>" />'
                        value="1" />
                </td>
                <td>
                    <select name="UnitDropDownList"
                        id="UnitDropDownList"
                        class="form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlU%>" />'>
                    </select>
                </td>
                <td>
                    <input type="text"
                        id="PriceTextBox"
                        class="text-right currency form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltP%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="AmountTextBox"
                        readonly="readonly"
                        class="text-right currency form-control input-sm" />
                </td>
                <td>
                    <input type="text"
                        id="DiscountTextBox"
                        class="text-right currency form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlD%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="SubTotalTextBox"
                        readonly="readonly"
                        class="text-right currency form-control input-sm" />
                </td>
                <td>
                    <input type="text"
                        id="TaxRateTextBox"
                        class="text-right form-control input-sm" />
                </td>
                <td>
                    <input type="text"
                        id="TaxTextBox"
                        class="text-right currency form-control input-sm"
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlT%>" />' />
                </td>
                <td>
                    <input type="text"
                        id="TotalTextBox"
                        readonly="readonly"
                        class="text-right currency form-control input-sm" />
                </td>
                <td>
                    <input type="button"
                        id="AddButton"
                        class="btn btn-primary btn-sm"
                        value='<asp:Literal runat="server" Text="<%$Resources:Titles,Add%>" />'
                        title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlReturn%>" />' />
                </td>
            </tr>
        </tbody>
    </table>

    <asp:Panel ID="FormPanel" runat="server" Enabled="false">
        <asp:Label ID="ErrorLabel" runat="server" CssClass="error" />
    </asp:Panel>

    <h4>
        <asp:Label ID="AttachmentLabel" runat="server" Text="<%$ Resources:Titles, AttachmentsPlus %>" CssClass="" />
    </h4>

    <div id="attachment" class="grey" style="display: none; padding-left: 24px;">
        <mixerp:Attachment ID="Attachment1" runat="server" />
    </div>

    <asp:Panel ID="BottomPanel" CssClass="table-form-pad grey" runat="server" Style="margin: 4px; width: 780px;">
        <asp:Table runat="server">
            <asp:TableRow ID="ShippingAddressRow" runat="server">
                <asp:TableCell Style="vertical-align: top!important;" Width="190px">
                    <asp:Literal ID="ShippingAddressDropDownListLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="ShippingAddressDropDownList" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                    <div style="padding: 4px 0 4px 0">
                        <asp:TextBox
                            ID="ShippingAddressTextBox"
                            runat="server"
                            ReadOnly="true"
                            TextMode="MultiLine"
                            CssClass="form-control input-sm"
                            Height="72px" />
                    </div>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="ShippingCompanyRow" runat="server">
                <asp:TableCell>
                    <asp:Literal ID="ShippingCompanyDropDownListLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="ShippingCompanyDropDownList" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="ShippingChargeRow" runat="server">
                <asp:TableCell>
                    <asp:Literal ID="ShippingChargeTextBoxLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="ShippingChargeTextBox" runat="server" Width="140px" CssClass="currency form-control input-sm">
                    </asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <label>
                        <asp:Literal ID="TotalsLiteral" runat="server" Text="<%$Resources:Titles, Totals %>">
                        </asp:Literal>
                    </label>
                </asp:TableCell><asp:TableCell>
                    <table style="border-collapse: collapse; width: 100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="RunningTotalTextBoxLabelLiteral" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="TaxTotalTextBoxLabelLiteral" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="GrandTotalTextBoxLabelLiteral" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="RunningTotalTextBox" runat="server" CssClass="form-control input-sm" ReadOnly="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="TaxTotalTextBox" runat="server" CssClass="form-control input-sm" ReadOnly="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="GrandTotalTextBox" runat="server" CssClass="form-control input-sm" ReadOnly="true" />
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="CashRepositoryRow" runat="server">
                <asp:TableCell runat="server">
                    <asp:Literal ID="CashRepositoryDropDownListLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="CashRepositoryDropDownList" runat="server"
                        CssClass="form-control input-sm">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="CashRepositoryBalanceRow" runat="server">
                <asp:TableCell>
                    <asp:Literal ID="CashRepositoryBalanceTextBoxLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="CashRepositoryBalanceTextBox" runat="server" Width="140" CssClass="form-control input-sm" ReadOnly="true" />
                    <asp:Literal ID="DrLiteral" runat="server" Text="<%$Resources:Titles, Dr %>" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="CostCenterRow" runat="server">
                <asp:TableCell>
                    <asp:Literal ID="CostCenterDropDownListLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="CostCenterDropDownList" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="SalespersonRow" runat="server">
                <asp:TableCell>
                    <asp:Literal ID="SalespersonDropDownListLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="SalespersonDropDownList" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Literal ID="StatementReferenceTextBoxLabelLiteral" runat="server" />
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="StatementReferenceTextBox" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="100">
                    </asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                                    &nbsp;
                </asp:TableCell>
                <asp:TableCell>
                        <button type="button" id="SaveButton" class="btn btn-default btn-sm">
                            <asp:Literal runat="server" Text="<%$Resources:Titles, Save%>" />
                        </button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

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
        <asp:Label ID="ErrorLabelBottom" runat="server" CssClass="error" />
    </p>
</div>

<script type="text/javascript">
    var isSales = ("<%= this.Book %>" == "Sales");
    var tranBook = "<%=this.GetTranBook() %>";
    var taxAfterDiscount = "<%=Switches.TaxAfterDiscount().ToString()%>";
    var verifyStock = ("<%= this.VerifyStock %>" == "True");
</script>

<script src="/Scripts/UserControls/ProductControl.js"></script>