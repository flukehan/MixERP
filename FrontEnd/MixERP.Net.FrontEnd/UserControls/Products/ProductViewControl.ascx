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
        <i class="icon left"></i>
        <asp:Literal runat="server" Text="Return" />
    </button>

    <button type="button" id="flagButton" class="ui button">
        <i class="icon flag"></i>&nbsp;
        <asp:Literal runat="server" Text="Flag" />
    </button>

    <button type="button" class="ui button">
        <i class="icon print"></i>&nbsp;
        <asp:Literal runat="server" Text="Print" />
    </button>
</div>

<div id="flag-popunder" style="width: 300px; display: none;" class="ui segment">
    <h3 class="panel-title">Flag This Transaction</h3>

    <div>
        You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
    </div>
    <br />
    <p>Select a flag</p>
    <p>
        <asp:DropDownList ID="FlagDropDownList" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button
            ID="UpdateButton"
            runat="server"
            Text="Update"
            CssClass="green small ui button"
            OnClientClick=" return getSelectedItems(); "
            OnClick="UpdateButton_Click" />
        <a href="javascript:void(0);" onclick=" $('#flag-popunder').toggle(500); " class="red small ui button">Close</a>
    </p>
</div>

<p>
    <asp:Label ID="ErrorLabel" runat="server" CssClass="big error" />
</p>

<div id="FilterDiv" class="ui segment">
    <div class="ui form" style="margin-left: 8px;">
        <div class="inline fields">
            <div class="small field">
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="FiscalYearStartDate"
                    Required="true" />
                <i class="icon calendar pointer" onclick=" $('#DateFromDateTextBox').datepicker('show'); "></i>
            </div>

            <div class="small field">
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="MonthEndDate" Required="true" />
                <i class="icon calendar pointer" onclick=" $('#DateToDateTextBox').datepicker('show'); "></i>
            </div>

            <div class="small field">
                <asp:TextBox ID="OfficeTextBox" runat="server" CssClass="form-control input-sm" placeholder="Office" />
            </div>

            <div class="field">
                <asp:TextBox ID="PartyTextBox" runat="server" CssClass="form-control input-sm" placeholder="Party" />
            </div>

            <div id="PriceTypeDiv" runat="server" class="small field">
                <asp:TextBox ID="PriceTypeTextBox" runat="server" CssClass="form-control input-sm" placeholder="Price Type" />
            </div>
            <div class="small field">
                <asp:TextBox ID="UserTextBox" runat="server" CssClass="form-control input-sm" placeholder="User" />
            </div>

            <div class="small field">
                <asp:TextBox ID="ReferenceNumberTextBox" runat="server" CssClass="form-control input-sm" placeholder="Reference Number" />
            </div>

            <div class="field">
                <asp:TextBox ID="StatementReferenceTextBox" runat="server" CssClass="form-control input-sm" placeholder="Statement Reference" />
            </div>

            <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="blue ui button" OnClick="ShowButton_Click" />
        </div>
    </div>
</div>

<asp:Panel ID="GridPanel" runat="server" Width="100%" ScrollBars="Auto">
    <asp:GridView
        ID="ProductViewGridView"
        runat="server"
        CssClass="ui celled nowrap table segment"
        AutoGenerateColumns="false"
        OnRowDataBound="ProductViewGridView_RowDataBound">
    </asp:GridView>
</asp:Panel>

<asp:HiddenField ID="SelectedValuesHidden" runat="server" />
<script src="/Scripts/UserControls/ProductViewControl.js"></script>