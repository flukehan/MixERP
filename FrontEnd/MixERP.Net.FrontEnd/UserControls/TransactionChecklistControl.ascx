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
    <asp:Literal ID="TitleLiteral" runat="server" />
</h1>
<hr />
<h3>
    <asp:Literal ID="Title2Literal" runat="server" Text="The transaction was posted successfully." />
</h3>
<hr />

<h4 class="vpad16">
    <asp:Label ID="VerificationLabel" runat="server" />
</h4>

<br />

<div class="panel panel-default panel-info" style="max-width: 400px;">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Literal ID="ChecklistLiteral" runat="server" Text="Checklists" /></h3>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:LinkButton ID="WithdrawButton" runat="server" Text="Withdraw This Transaction" OnClientClick="$('#WithdrawDiv').toggle(200);return(false);" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="ViewReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="EmailReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" OnClick="EmailReportButton_Click" />
            <asp:LinkButton ID="CustomerReportButton" runat="server" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="PrintReceiptButton" runat="server" Text="Print Receipt" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="PrintGLButton" runat="server" Text="Print GL Entry" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="AttachmentButton" runat="server" Text="Upload Attachments for This Transaction" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="BackButton" runat="server" Text="Back" OnClientClick="javascript:history.go(-1);return false;" CssClass="list-group-item" CausesValidation="false" />
        </div>
    </div>
</div>

<div id="WithdrawDiv" class="panel panel-default panel-warning" style="max-width: 400px;">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Literal ID="WithdrawTransactionLiteral" runat="server" Text="Withdraw Transaction" /></h3>
    </div>
    <div class="panel-body">
        <p>
            <asp:Literal ID="ReasonLiteral" runat="server" Text="Withdrawal Reason" />
        </p>
        <p>
            <asp:TextBox ID="ReasonTextBox" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Rows="5" />
        </p>
        <p>
            <asp:RequiredFieldValidator ID="ReasonTextBoxRequired" runat="server" ControlToValidate="ReasonTextBox" ErrorMessage="This field is required." CssClass="error-message" Display="Dynamic" />
        </p>

        <p>
            <asp:Button ID="OkButton" runat="server" Text="OK" CssClass="btn btn-sm btn-warning" OnClick="OkButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-default btn-sm" CausesValidation="false" OnClientClick="$('#WithdrawDiv').toggle(200);return(false);" />
        </p>
    </div>
</div>

<asp:HiddenField runat="server" ID="EmailHidden"></asp:HiddenField>

<asp:Label ID="MessageLabel" runat="server" />

<script type="text/javascript">
    var withdrawDiv = $("#WithdrawDiv");
    var widthdrawButton = $("#WithdrawButton");

    $(document).ready(function () {

        if (widthdrawButton.length) {

            withdrawDiv.position({
                my: "left top",
                at: "right top",
                of: widthdrawButton,
                collision: "flip"
            });
        };

        withdrawDiv.hide();

        var url = $("#ViewReportButton").attr("data-url");
        if (url) {
            prepareEmail(url);
        };
    });

    function prepareEmail(url) {
        $.get(
            url,
            function (response) {
                $("#EmailHidden").val(response);
            });

    };
</script>