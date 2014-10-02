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

<div class="btn-toolbar" role="toolbar">
    <div class="btn-group">

        <button type="button" id="AddNewButton" runat="server" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-plus"></span>
            <asp:Literal runat="server" Text="Add New" />
        </button>

        <button type="button" id="MergeToOrderButton" runat="server" class="btn btn-default btn-sm"
            onclick="if(!getSelectedItems()){return;};" onserverclick="MergeToOrderButton_Click" visible="False">
            <span class="glyphicon glyphicon-tree-conifer"></span>
            <asp:Literal runat="server" Text="Merge Batch To Sales Order" />
        </button>

        <button type="button" id="MergeToDeliveryButton" runat="server"
            class="btn btn-default btn-sm"
            onclick="if(!getSelectedItems()){return;};"
            onserverclick="MergeToDeliveryButton_Click" visible="false">
            <span class="glyphicon glyphicon-tree-deciduous"></span>
            <asp:Literal runat="server" Text="Merge Batch To Sales Delivery" />
        </button>

        <button type="button" id="MergeToGRNButton" runat="server" class="btn btn-default btn-sm"
            onclick="if(!getSelectedItems()){return;};" onserverclick="MergeToGRNButton_Click" visible="False">
            <span class="glyphicon glyphicon-tree-conifer"></span>
            <asp:Literal runat="server" Text="Merge Batch To GRN" />
        </button>

        <button type="button" id="ReturnButton" runat="server" class="btn btn-default btn-sm"
            onclick="if(!getSelectedItems()){return;};" onserverclick="ReturnButton_Click" visible="False">
            <span class="glyphicon glyphicon-tree-conifer"></span>
            <asp:Literal runat="server" Text="Return" />
        </button>

        <button type="button" id="flagButton" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-flag"></span>&nbsp;
                <asp:Literal runat="server" Text="Flag" />
        </button>

        <button type="button" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-print"></span>&nbsp;
                <asp:Literal runat="server" Text="Print" />
        </button>
    </div>
</div>

<div id="flag-popunder" style="width: 300px; display: none;" class="popunder">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Flag This Transaction</h3>
        </div>
        <div class="panel-body">
            <div>
                You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
            </div>
            <br />
            <p>Please select a flag</p>
            <p>
                <asp:DropDownList ID="FlagDropDownList" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button
                    ID="UpdateButton"
                    runat="server"
                    Text="Update"
                    CssClass="btn btn-primary btn-sm"
                    OnClientClick="return getSelectedItems();"
                    OnClick="UpdateButton_Click" />
                <a href="javascript:void(0);" onclick="$('#flag-popunder').toggle(500);" class="btn btn-default btn-sm">Close</a>
            </p>
        </div>
    </div>
</div>

<asp:Label ID="ErrorLabel" runat="server" CssClass="error" />

<div id="FilterDiv" class="shade" style="margin: 8px 0 8px 0;">
    <div class="row" style="margin-left: 8px;">

        <div class="col-md-1 pad4" style="width: 120px;">
            <div class="input-group">
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="FiscalYearStartDate"
                    Required="true" />
                <span class="input-group-addon" onclick="$('#DateFromDateTextBox').datepicker('show');">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
        </div>

        <div class="col-md-1 pad4" style="width: 120px;">
            <div class="input-group">
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="MonthEndDate" Required="true" />
                <span class="input-group-addon" onclick="$('#DateToDateTextBox').datepicker('show');">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
        </div>

        <div class="col-md-1 pad4">
            <asp:TextBox ID="OfficeTextBox" runat="server" CssClass="form-control input-sm" placeholder="Office" />
        </div>

        <div class="col-md-1 pad4">
            <asp:TextBox ID="PartyTextBox" runat="server" CssClass="form-control input-sm" placeholder="Party" />
        </div>

        <div id="PriceTypeDiv" runat="server">
            <div class="col-md-1 pad4">
                <asp:TextBox ID="PriceTypeTextBox" runat="server" CssClass="form-control input-sm" placeholder="Price Type" />
            </div>
        </div>
        <div class="col-md-1 pad4">
            <asp:TextBox ID="UserTextBox" runat="server" CssClass="form-control input-sm" placeholder="User" />
        </div>

        <div class="col-md-2 pad4">
            <asp:TextBox ID="ReferenceNumberTextBox" runat="server" CssClass="form-control input-sm" placeholder="Reference Number" />
        </div>

        <div class="col-md-2 pad4">
            <asp:TextBox ID="StatementReferenceTextBox" runat="server" CssClass="form-control input-sm" placeholder="Statement Reference" />
        </div>
        <div class="col-md-1 pad4">
            <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="btn btn-default input-sm" OnClick="ShowButton_Click" />
        </div>
    </div>
</div>

<asp:Panel ID="GridPanel" runat="server" Width="100%" ScrollBars="Auto">
    <asp:GridView
        ID="ProductViewGridView"
        runat="server"
        CssClass="table table-bordered table-condensed pointer"
        AutoGenerateColumns="false"
        OnRowDataBound="ProductViewGridView_RowDataBound">
    </asp:GridView>
</asp:Panel>

<asp:HiddenField ID="SelectedValuesHidden" runat="server" />
<script src="/Scripts/UserControls/ProductViewControl.js"></script>