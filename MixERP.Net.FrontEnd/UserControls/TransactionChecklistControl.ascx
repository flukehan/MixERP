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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionChecklistControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.TransactionChecklistControl" %>
<%@ Import Namespace="MixERP.Net.BusinessLayer.Transactions" %>
<%@ Import Namespace="MixERP.Net.Common" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>
<%@ Import Namespace="MixERP.Net.Common.Models.Transactions" %>
<%@ Import Namespace="Resources" %>

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
            <asp:LinkButton ID="ViewInvoiceButton" runat="server" Text="<%$Resources:Titles, ViewThisInvoice %>" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="EmailInvoiceButton" runat="server" Text="<%$Resources:Titles, EmailThisInvoice %>" CssClass="list-group-item" CausesValidation="false" />
            <asp:LinkButton ID="CustomerInvoiceButton" runat="server" Text="<%$Resources:Titles, PrintThisInvoice %>" CssClass="list-group-item" CausesValidation="false" />
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


        withdrawDiv.position({
            my: "left top",
            at: "right top",
            of: "#WithdrawButton",
            collision: "fit"
        });

        withdrawDiv.hide();
    });
</script>


<script runat="server">
    public bool DisplayWithdrawButton { get; set; }
    public bool DisplayViewInvoiceButton { get; set; }
    public bool DisplayEmailInvoiceButton { get; set; }
    public bool DisplayCustomerInvoiceButton { get; set; }
    public bool DisplayPrintReceiptButton { get; set; }
    public bool DisplayPrintGlEntryButton { get; set; }
    public bool DisplayAttachmentButton { get; set; }
    public bool IsNonGlTransaction { get; set; }
    public string InvoicePath { get; set; }
    public string CustomerInvoicePath { get; set; }
    public string GlAdvicePath { get; set; }

    protected void OkButton_Click(object sender, EventArgs e)
    {

        DateTime transactionDate = DateTime.Now;
        long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);

        VerificationModel model = Verification.GetVerificationStatus(transactionMasterId);
        if (
            model.Verification.Equals(0) //Awaiting verification 
            ||
            model.Verification.Equals(2) //Automatically Approved by Workflow
            )
        {
            //Withdraw this transaction.                        
            if (transactionMasterId > 0)
            {
                if (Verification.WithdrawTransaction(transactionMasterId, MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetUserId(), this.ReasonTextBox.Text))
                {
                    this.MessageLabel.Text = string.Format(Labels.TransactionWithdrawnMessage, transactionDate.ToShortDateString());
                    this.MessageLabel.CssClass = "success vpad12";
                }
            }
        }
        else
        {
            this.MessageLabel.Text = Warnings.CannotWithdrawTransaction;
            this.MessageLabel.CssClass = "error vpad12";
        }

        this.ShowVerificationStatus();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.WithdrawButton.Visible = this.DisplayWithdrawButton;
        this.ViewInvoiceButton.Visible = this.DisplayViewInvoiceButton;
        this.EmailInvoiceButton.Visible = this.DisplayEmailInvoiceButton;
        this.CustomerInvoiceButton.Visible = this.DisplayCustomerInvoiceButton;
        this.PrintReceiptButton.Visible = this.DisplayPrintReceiptButton;
        this.PrintGLButton.Visible = this.DisplayPrintGlEntryButton;
        this.AttachmentButton.Visible = this.DisplayAttachmentButton;

        string invoiceUrl = this.ResolveUrl(this.InvoicePath + "?TranId=" + this.Request["TranId"]);
        string customerInvoiceUrl = this.ResolveUrl(this.CustomerInvoicePath + "?TranId=" + this.Request["TranId"]);
        string glAdviceUrl = this.ResolveUrl(this.GlAdvicePath + "?TranId=" + this.Request["TranId"]);

        this.ViewInvoiceButton.Attributes.Add("onclick", "showWindow('" + invoiceUrl + "');return false;");
        this.CustomerInvoiceButton.Attributes.Add("onclick", "showWindow('" + customerInvoiceUrl + "');return false;");
        this.PrintGLButton.Attributes.Add("onclick", "showWindow('" + glAdviceUrl + "');return false;");

        this.ShowVerificationStatus();
    }

    private void ShowVerificationStatus()
    {
        long transactionMasterId = Conversion.TryCastLong(this.Request["TranId"]);
        VerificationModel model = Verification.GetVerificationStatus(transactionMasterId);

        switch (model.Verification)
        {
            case -3:
                this.VerificationLabel.CssClass = "alert-danger";
                this.VerificationLabel.Text = string.Format(Labels.VerificationRejectedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                break;
            case -2:
                this.VerificationLabel.CssClass = "alert-warning";
                this.VerificationLabel.Text = string.Format(Labels.VerificationClosedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                break;
            case -1:
                this.VerificationLabel.Text = string.Format(Labels.VerificationWithdrawnMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                this.VerificationLabel.CssClass = "alert-warning";
                break;
            case 0:
                this.VerificationLabel.Text = Labels.VerificationAwaitingMessage;
                this.VerificationLabel.CssClass = "alert-info";
                break;
            case 1:
            case 2:
                this.VerificationLabel.Text = string.Format(Labels.VerificationApprovedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()));
                this.VerificationLabel.CssClass = "alert-success";
                break;
        }
    }

</script>
