<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GRN.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.GRN"
    OverridePath="/Modules/Purchase/GRN.mix" %>

<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />