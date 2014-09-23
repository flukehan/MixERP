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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionChecklistControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.TransactionChecklistControl" %>

<h1>
    <asp:Literal ID="TitleLiteral" runat="server" Text="<%$Resources:Titles, TransactionPostedSuccessfully %>" />
</h1>

<h4 class="vpad16">
    <asp:Label ID="VerificationLabel" runat="server" />
</h4>

<br />

<div class="panel panel-default panel-info" style="max-width: 400px;">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Literal ID="ChecklistLiteral" runat="server" Text="<%$Resources:Titles, Checklists %>" /></h3>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:LinkButton ID="WithdrawButton" runat="server" Text="<%$Resources:Titles, WithdrawThisTransaction %>" OnClientClick="$('#WithdrawDiv').toggle(200);return(false);" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="ViewReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="EmailReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="CustomerReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="PrintReceiptButton" runat="server" Text="<%$Resources:Titles, PrintReceipt %>" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="PrintGLButton" runat="server" Text="<%$Resources:Titles, PrintGLEntry %>" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="AttachmentButton" runat="server" Text="<%$Resources:Titles, UploadAttachmentForThisTransaction %>" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="BackButton" runat="server" Text="<%$Resources:Titles, Back %>" OnClientClick="javascript:history.go(-1);return false;" CssClass="list-group-item" CausesValidation="false" />
        </div>
    </div>
</div>

<div id="WithdrawDiv" class="panel panel-default panel-warning" style="max-width: 400px;">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Literal ID="WithdrawTransactionLiteral" runat="server" Text="<%$Resources:Titles, WithdrawTransaction %>" /></h3>
    </div>
    <div class="panel-body">
        <p>
            <asp:Literal ID="ReasonLiteral" runat="server" Text="<%$Resources:Questions, WithdrawalReason %>" />
        </p>
        <p>
            <asp:TextBox ID="ReasonTextBox" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Rows="5" />
        </p>
        <p>
            <asp:RequiredFieldValidator ID="ReasonTextBoxRequired" runat="server" ControlToValidate="ReasonTextBox" ErrorMessage="<%$Resources:Labels, FieldRequired %>" CssClass="error-message" Display="Dynamic" />
        </p>

        <p>
            <asp:Button ID="OkButton" runat="server" Text="<%$Resources:Titles, OK %>" CssClass="btn btn-sm btn-warning" OnClick="OkButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="<%$Resources:Titles, Cancel %>" CssClass="btn btn-default btn-sm" CausesValidation="false" OnClientClick="$('#WithdrawDiv').toggle(200);return(false);" />
        </p>
    </div>
</div>

<asp:Label ID="MessageLabel" runat="server" />

<script type="text/javascript">
    var withdrawDiv = $("#WithdrawDiv");
    var widthdrawButton = $("#WithdrawButton");

    $(document).ready(function () {
        withdrawDiv.hide();

        if (widthdrawButton.length) {
            withdrawDiv.position({
                my: "left top",
                at: "right top",
                of: "#WithdrawButton",
                collision: "fit"
            });
        };

    });
</script>