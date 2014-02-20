<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionChecklistControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.TransactionChecklistControl" %>
<%@ Import Namespace="MixERP.Net.BusinessLayer.Transactions" %>
<%@ Import Namespace="MixERP.Net.Common" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>
<%@ Import Namespace="MixERP.Net.Common.Models.Transactions" %>
<%@ Import Namespace="Resources" %>
<h1>
    <asp:Literal ID="TitleLiteral" runat="server" Text="<%$Resources:Titles, TransactionPostedSuccessfully %>" />
</h1>
<hr class="hr" />

<asp:Label ID="VerificationLabel" runat="server" />


<br />

<div style="float: left;">
    <h2>
        <asp:Literal ID="ChecklistLiteral" runat="server" Text="<%$Resources:Titles, Checklists %>" />
    </h2>
    <div class="transaction-confirmation" style="margin-top: 12px;">
        <asp:LinkButton ID="WithdrawButton" runat="server" Text="<%$Resources:Titles, WithdrawThisTransaction %>" OnClientClick="$('#withdraw').toggle(200);return(false);" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="ViewInvoiceButton" runat="server" Text="<%$Resources:Titles, ViewThisInvoice %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="EmailInvoiceButton" runat="server" Text="<%$Resources:Titles, EmailThisInvoice %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="CustomerInvoiceButton" runat="server" Text="<%$Resources:Titles, PrintThisInvoice %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="PrintReceiptButton" runat="server" Text="<%$Resources:Titles, PrintReceipt %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="PrintGLButton" runat="server" Text="<%$Resources:Titles, PrintGLEntry %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="AttachmentButton" runat="server" Text="<%$Resources:Titles, UploadAttachmentForThisTransaction %>" CssClass="linkblock" CausesValidation="false" />
        <asp:LinkButton ID="BackButton" runat="server" Text="<%$Resources:Titles, Back %>" OnClientClick="javascript:history.go(-1);return false;" CssClass="linkblock" CausesValidation="false" />
    </div>
</div>

<div id="withdraw" style="float: left; margin-left: 12px; display: none;">
    <h2>
        <asp:Literal ID="WithdrawTransactionLiteral" runat="server" Text="<%$Resources:Titles, WithdrawTransaction %>" />
    </h2>

    <div class="transaction-confirmation" style="margin-top: 12px;">
        <p>
            <asp:Literal ID="ReasonLiteral" runat="server" Text="<%$Resources:Titles, WithdrawalReason %>" />
        </p>
        <p>
            <asp:TextBox ID="ReasonTextBox" runat="server" TextMode="MultiLine" Width="96%" Height="120" />
        </p>
        <p>
            <asp:RequiredFieldValidator ID="ReasonTextBoxRequired" runat="server" ControlToValidate="ReasonTextBox" ErrorMessage="<%$Resources:Labels, FieldRequired %>" CssClass="form-error" Display="Dynamic" />
        </p>

        <p>
            <asp:Button ID="OkButton" runat="server" Text="<%$Resources:Titles, OK %>" CssClass="button" OnClick="OkButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="<%$Resources:Titles, Cancel %>" CssClass="button" CausesValidation="false" OnClientClick="$('#withdraw').toggle(200);return(false);" />
        </p>

    </div>
</div>

<div style="clear: both;"></div>

<asp:Label ID="MessageLabel" runat="server" />


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
        if(
            model.Verification.Equals(0) //Awaiting verification 
            ||
            model.Verification.Equals(2) //Automatically Approved by Workflow
            )
        {
            //Withdraw this transaction.                        
            if(transactionMasterId > 0)
            {
                if(Verification.WithdrawTransaction(transactionMasterId, MixERP.Net.BusinessLayer.Helpers.SessionHelper.GetUserId(), this.ReasonTextBox.Text))
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

        switch(model.Verification)
        {
            case -3:
                this.VerificationLabel.CssClass = "info pink";
                this.VerificationLabel.Text = string.Format(Labels.VerificationRejectedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                break;
            case -2:
                this.VerificationLabel.CssClass = "info red";
                this.VerificationLabel.Text = string.Format(Labels.VerificationClosedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                break;
            case -1:
                this.VerificationLabel.Text = string.Format(Labels.VerificationWithdrawnMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()), model.VerificationReason);
                this.VerificationLabel.CssClass = "info yellow";
                break;
            case 0:
                this.VerificationLabel.Text = Labels.VerificationAwaitingMessage;
                this.VerificationLabel.CssClass = "info purple";
                break;
            case 1:
            case 2:
                this.VerificationLabel.Text = string.Format(Labels.VerificationApprovedMessage, model.VerifierName, model.VerifiedDate.ToString(LocalizationHelper.GetCurrentCulture()));
                this.VerificationLabel.CssClass = "info green";
                break;
        }
    }

</script>
