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
    <input id="AuthorizeButton" value="<%=Titles.Authorize %>" class="ui button" type="button">
    <input id="SendButton" value="<%=Titles.Send %>" class="ui button" type="button">
    <input id="EditButton" value="<%=Titles.EditAndSend %>" class="ui button" type="button">
    <input id="ReceiveButton" value="<%=Titles.Receive %>" class="ui button" type="button">
    <input id="EditReceiveButton" value="<%=Titles.EditAndReceive %>" class="ui button" type="button">
    <input id="PrintButton" value="<%= Titles.Print %>" class="ui button" type="button">
</div>

<div id="FilterDiv" class="ui segment">
    <div class="ui form" style="margin-left: 8px;">
        <div class="eight fields">
            <div class="field">
                <label><%=Titles.From %></label>
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server" Mode="MonthStartDate"/>
            </div>
            <div class="field">
                <label><%=Titles.To %></label>
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server" Mode="MonthEndDate"></mixerp:DateTextBox>
            </div>
            <div class="field">
                <label><%=Titles.Office %></label>
                <input id="OfficeTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.Store %></label>
                <input id="StoreTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.Authorized %></label>
                <input id="AuthorizedTextBox" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.Acknowledged %></label>
                <input id="AcknowledgedTextBox" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.Withdrawn %></label>
                <input id="WithdrawnTextBox" type="text" value="false" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.User %></label>
                <input id="UserTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.ReferenceNumberAbbreviated %></label>
                <input id="ReferenceNumberTextBox" type="text" runat="server"/>
            </div>
            <div class="field">
                <label><%=Titles.StatementReference %></label>
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

    $(window).load(function () {
        var grid = $("#TransferRequestGridView");
        var header = grid.find("thead tr");
        var rows = grid.find("tbody tr");



        var iconTemplate = "<a href=\"Confirmation/TransferRequest.mix?TranId=%s\" title=\"%s\">" +
                                "<i class=\"list icon\"></i>" +
                            "</a>" +
                            "<a title=\"%s\" onclick=\"showWindow('Reports/TransferRequestReport.mix?TranId=%1$s');\">" +
                                "<i class=\"print icon\"></i>" +
                            "</a>" +
                            "<a title=\"%s\" onclick=\"window.scroll(0);\">" +
                                "<i class=\"arrow up icon\"></i>" +
                            "</a>";

        if (header.length) {
            header.prepend("<th>" + Resources.Titles.Actions() + "</th><th>" + Resources.Titles.Select() + "</th>");
            rows.prepend("<td></td><td><div class='ui toggle checkbox'><input type='checkbox' /><label></label></div></td>");

            rows.click(function () {
                var el = $(this);
                toogleSelection(el.find("input"));
            });


            rows.each(function () {
                var iconCell = $(this).find("td:first-child");
                var tranId = $(this).find("td:nth-child(3)").html();

                var template = sprintf(iconTemplate, tranId, Resources.Labels.GoToChecklistWindow(), Resources.Titles.Print(), Resources.Labels.GoToTop());

                iconCell.html(template);

            });

        };
    });
</script>