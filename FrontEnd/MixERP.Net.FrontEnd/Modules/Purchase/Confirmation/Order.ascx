<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.Order"
    OverridePath="/Modules/Purchase/Order.mix" %>

<mixerp:TransactionChecklist runat="server"
    AttachmentBookName="non-gltransaction"
    OverridePath="/Modules/Purchase/Order.mix"
    DisplayWithdrawButton="true"
    DisplayViewReportButton="true"
    DisplayEmailReportButton="true"
    DisplayCustomerReportButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    ReportPath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.mix"
    GlAdvicePath="~/Modules/Finance/Confirmation/GLAdvice.mix"
    ViewReportButtonText="View This Order"
    EmailReportButtonText="Email This Order" />