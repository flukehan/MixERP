<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Return.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Return"
    OverridePath="~/Modules/Sales/Return.mix" %>

<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="false"
    DisplayEmailInvoiceButton="false"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />