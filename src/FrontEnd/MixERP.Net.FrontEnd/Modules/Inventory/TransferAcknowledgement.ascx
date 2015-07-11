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
<h2><%= Titles.StockTransferAcknowledgement %></h2>
<div class="basic ui buttons">
    <input id="FlagButton" value="<%= Titles.Flag %>" class="ui button" type="button">
    <input id="ReceiveButton" value="<%= Titles.Receive %>" class="ui button" type="button">
    <input id="PrintButton" value="<%= Titles.Print %>" class="ui button" type="button">
</div>

<div id="FilterDiv" class="ui segment">
    <div class="ui form" style="margin-left: 8px;">
        <div class="eight fields">
            <div class="field">
                <label><%= Titles.From %></label>
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server" Mode="MonthStartDate"/>
            </div>
            <div class="field">
                <label><%= Titles.To %></label>
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server" Mode="MonthEndDate"></mixerp:DateTextBox>
            </div>
            <div class="field">
                <label><%= Titles.Office %></label>
                <input id="OfficeTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.Store %></label>
                <input id="StoreTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.Authorized %></label>
                <input id="AuthorizedTextBox" type="text" value="true" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.Delivered %></label>
                <input id="DeliveredTextBox" type="text" value="true" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.Received %></label>
                <input id="ReceivedTextBox" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.User %></label>
                <input id="UserTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.ReferenceNumberAbbreviated %></label>
                <input id="ReferenceNumberTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%= Titles.StatementReference %></label>
                <input id="StatementReferenceTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label>&nbsp;</label>
                <asp:Button runat="server" ID="ShowButton" CssClass="blue ui button" Text="Show" OnClick="ShowButton_Click"/>
            </div>
        </div>
    </div>
</div>

<asp:PlaceHolder ID="GridViewPlaceholder" runat="server"/>
<script type="text/javascript">
    var receiveButton = $("#ReceiveButton");

    var checkListUrlOverride = "Confirmation/TransferDelivery.mix?TranId=%s";
    var reportUrlOverride = "Reports/InventoryTransferDeliveryReport.mix?TranId=%1$s";

    receiveButton.click(function() {
        getSelectedItems();
        var tranId = parseFloat($("#SelectedValuesHidden").val().split(",")[0] || 0);

        if (!tranId) {
            displayMessage(Resources.Warnings.NothingSelected(), "error");
            return;
        };

        var confirmed = confirm(Resources.Questions.AreYouSure());

        if (!confirmed) {
            return;
        };

        var ajaxReceive = Receive(tranId);

        ajaxReceive.success(function (msg) {
            var id = msg.d;
            var transferUrl = "/Modules/Inventory/Confirmation/Transfer.mix?TranId=" + id;

            window.location = transferUrl;
        });

        ajaxReceive.fail(function(xhr) {
            logAjaxErrorMessage(xhr);
        });
    });

    function Receive(tranId) {

        url = "/Modules/Inventory/Services/TransferAcknowledgement.asmx/Receive";

        data = appendParameter("", "tranId", tranId);

        data = getData(data);

        return getAjax(url, data);
    };


</script>


<script src="Scripts/TransferRequest.js"></script>