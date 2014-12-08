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

<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ProductViewControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductViewControl" %>
<h2>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h2>

<div class="ui icon buttons">
    <button type="button" id="AddNewButton" runat="server" class="ui button">
        <i class="icon plus"></i>
        <asp:Literal runat="server" Text="Add New" />
    </button>

    <button type="button" id="MergeToOrderButton" runat="server" class="ui button"
        onclick="if(!getSelectedItems()){return;};" onserverclick="MergeToOrderButton_Click" visible="False">
        <i class="icon magic"></i>
        <asp:Literal runat="server" Text="Merge Batch To Sales Order" />
    </button>

    <button type="button" id="MergeToDeliveryButton" runat="server"
        class="ui button"
        onclick="if(!getSelectedItems()){return;};"
        onserverclick="MergeToDeliveryButton_Click" visible="false">
        <i class="icon magic"></i>
        <asp:Literal runat="server" Text="Merge Batch To Sales Delivery" />
    </button>

    <button type="button" id="MergeToGRNButton" runat="server" class="ui button"
        onclick="if(!getSelectedItems()){return;};" onserverclick="MergeToGRNButton_Click" visible="False">
        <i class="icon magic"></i>
        <asp:Literal runat="server" Text="Merge Batch To GRN" />
    </button>

    <button type="button" id="ReturnButton" runat="server" class="ui button"
        onclick="if(!getSelectedItems()){return;};" onserverclick="ReturnButton_Click" visible="False">
        <i class="icon chevron circle left"></i>
        <asp:Literal runat="server" Text="Return" />
    </button>

    <button type="button" id="FlagButton" class="ui button">
        <i class="icon flag"></i>&nbsp;
        <asp:Literal runat="server" Text="Flag" />
    </button>

    <button type="button" class="ui button" id="PrintButton">
        <i class="icon print"></i>&nbsp;
        <asp:Literal runat="server" Text="Print" />
    </button>
</div>

<p>
    <asp:Label ID="ErrorLabel" runat="server" CssClass="big error" />
</p>

<div id="FilterDiv" class="ui segment">
    <div class="ui form" style="margin-left: 8px;">
        <div class="nine fields">
            <div class="field">
                <div class="ui icon input">
                    <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server"
                        CssClass="date"
                        Mode="FiscalYearStartDate"
                        Required="true" />
                    <i class="icon calendar pointer" onclick=" $('#DateFromDateTextBox').datepicker('show'); "></i>
                </div>
            </div>

            <div class="field">
                <div class="ui icon input">
                    <mixerp:DateTextBox ID="DateToDateTextBox" runat="server"
                        CssClass="date"
                        Mode="MonthEndDate" Required="true" />
                    <i class="icon calendar pointer" onclick=" $('#DateToDateTextBox').datepicker('show'); "></i>
                </div>
            </div>

            <div class="field">
                <asp:TextBox ID="OfficeTextBox" runat="server" placeholder="Office" />
            </div>

            <div class="field">
                <asp:TextBox ID="PartyTextBox" runat="server" placeholder="Party" />
            </div>

            <div id="PriceTypeDiv" runat="server" class="field">
                <asp:TextBox ID="PriceTypeTextBox" runat="server" placeholder="Price Type" />
            </div>
            <div class="small field">
                <asp:TextBox ID="UserTextBox" runat="server" placeholder="User" />
            </div>

            <div class="small field">
                <asp:TextBox ID="ReferenceNumberTextBox" runat="server" placeholder="Reference Number" />
            </div>

            <div class="field">
                <asp:TextBox ID="StatementReferenceTextBox" runat="server" placeholder="Statement Reference" />
            </div>

            <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="blue ui button" OnClick="ShowButton_Click" />
        </div>
    </div>
</div>

<asp:Panel ID="GridPanel" runat="server" Width="100%" ScrollBars="Auto">
    <asp:GridView
        ID="ProductViewGridView"
        runat="server"
        GridLines="None"
        CssClass="ui nowrap table"
        AutoGenerateColumns="false"
        OnRowDataBound="ProductViewGridView_RowDataBound">
    </asp:GridView>
</asp:Panel>

<asp:PlaceHolder runat="server" ID="FlagPlaceholder"></asp:PlaceHolder>

<asp:HiddenField ID="SelectedValuesHidden" runat="server" />
<script src="/Scripts/UserControls/ProductViewControl.js"></script>