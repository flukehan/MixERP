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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferRequest.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.TransferRequest" %>
<%@ Import Namespace="MixERP.Net.i18n.Resources" %>
<%@ Register TagPrefix="mixerp" Namespace="MixERP.Net.WebControls.Common" Assembly="MixERP.Net.WebControls.Common, Version=1.2.0.1, Culture=neutral, PublicKeyToken=a724a47a0879d02f" %>

<h2><%= Titles.StockTransferRequest %></h2>
<div class="basic ui buttons">
    <input id="AddNewButton" value="<%= Titles.AddNew %>" class="ui button" onclick=" window.location = 'Entry/TransferRequest.mix' " type="button">
    <input id="FlagButton" value="<%= Titles.Flag %>" class="ui button" type="button">
    <input id="PrintButton" value="<%= Titles.Print %>" class="ui button" type="button">
</div>

<div id="FilterDiv" class="ui segment">
    <div class="ui form" style="margin-left: 8px;">
        <div class="ten fields">
            <div class="field">
                <label>From</label>
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server" Mode="MonthStartDate"/>
            </div>
            <div class="field">
                <label>To</label>
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server" Mode="MonthEndDate"></mixerp:DateTextBox>
            </div>
            <div class="field">
                <label>Office</label>
                <input id="OfficeTextBox" placeholder="Office" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>Store</label>
                <input id="StoreTextBox" placeholder="Store" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>Authorized</label>
                <input id="AuthorizedTextBox" placeholder="Price Type" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label>Acknowledged</label>
                <input id="AcknowledgedTextBox" placeholder="Price Type" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label>User</label>
                <input id="UserTextBox" placeholder="User" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>Ref #</label>
                <input id="ReferenceNumberTextBox" placeholder="Reference Number" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>Statement Reference</label>
                <input id="StatementReferenceTextBox" placeholder="Statement Reference" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>&nbsp;</label>
                <asp:Button runat="server" ID="ShowButton" CssClass="blue ui button" Text="Show" OnClick="ShowButton_Click"/>
            </div>
        </div>
    </div>
</div>

<asp:PlaceHolder ID="GridViewPlaceholder" runat="server"/>