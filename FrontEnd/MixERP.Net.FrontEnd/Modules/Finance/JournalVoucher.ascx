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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.JournalVoucher" %>
<%@ Register TagPrefix="mixerp" Namespace="MixERP.Net.WebControls.Common" Assembly="MixERP.Net.WebControls.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a724a47a0879d02f" %>

<h1>Journal Voucher Entry
</h1>
<script src="Scripts/JournalVoucher.js"></script>

<div class="ui icon buttons">
    <button type="button" id="AddNewButton" runat="server" class="ui button" onclick="window.location='Entry/JournalVoucher.mix';">
        <i class="icon plus"></i>
        <asp:Literal runat="server" Text="Add New" />
    </button>

    <button type="button" id="FlagButton" class="ui button">
        <i class="icon flag"></i>&nbsp;
        <asp:Literal runat="server" Text="Flag" />
    </button>

    <button type="button" id="ApproveButton" class="ui button">
        <i class="icon flag"></i>&nbsp;
        <asp:Literal runat="server" Text="Approve" />
    </button>

    <button type="button" id="RejectButton" class="ui button">
        <i class="icon flag"></i>&nbsp;
        <asp:Literal runat="server" Text="Reject" />
    </button>

    <button type="button" class="ui button">
        <i class="icon print"></i>&nbsp;
        <asp:Literal runat="server" Text="Print" />
    </button>
</div>

<div class="ui form segment">
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
                Mode="FiscalYearEndDate"
                Required="true" />
            <i class="icon calendar pointer" onclick=" $('#DateToDateTextBox').datepicker('show'); "></i>
        </div>
        <div class="small field">
            <input type="text" class="integer" placeholder="Tran Id" id="TranIdInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Tran Code" id="TranCodeInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Book" id="BookInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Reference#" id="ReferenceNumberInputText" runat="server" />
        </div>
        <div class="medium field">
            <input type="text" placeholder="Statement Reference" id="StatementReferenceInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Posted By" id="PostedByInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Office" id="OfficeInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Status" id="StatusInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Veririfed By" id="VerifiedByInputText" runat="server" />
        </div>
        <div class="small field">
            <input type="text" placeholder="Reason" id="ReasonInputText" runat="server" />
        </div>

        <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="blue ui button" OnClick="ShowButton_Click" />
    </div>
</div>

<div style="width: 1500px; overflow: auto;">
    <asp:GridView
        runat="server"
        ID="TransactionGridView"
        GridLines="None"
        AutoGenerateColumns="False"
        CssClass="ui celled table segment nowrap"
        OnRowDataBound="TransactionGridView_RowDataBound">
        <HeaderStyle CssClass="ui blue message"></HeaderStyle>
    </asp:GridView>
</div>

<asp:PlaceHolder runat="server" ID="FlagPlaceholder"></asp:PlaceHolder>

<script type="text/javascript">

    $(document).ready(function () {
        updateFlagColor();
    });

    var updateFlagColor = function () {
        var grid = $("#TransactionGridView");
        createFlaggedRows(grid);
    };

    var getSelectedItems = function () {
        alert("Nothing selected.");
        return false;
    };
</script>