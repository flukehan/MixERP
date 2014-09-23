<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GRN.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.GRN"
    OverridePath="/Modules/Purchase/GRN.mix" %>

<mixerp:TransactionChecklist runat="server"
    AttachmentBookName="transaction"
    OverridePath="/Modules/Purchase/GRN.mix"
    DisplayWithdrawButton="true"
    DisplayViewReportButton="true"
    DisplayEmailReportButton="true"
    DisplayCustomerReportButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    ReportPath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix"
    ViewReportButtonText="View This Note"
    EmailReportButtonText="Email This Note" />