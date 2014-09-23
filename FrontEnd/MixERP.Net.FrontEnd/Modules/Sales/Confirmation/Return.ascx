<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Return.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Return"
    OverridePath="~/Modules/Sales/Return.mix" %>

<mixerp:TransactionChecklist runat="server"
    ID="TransactionChecklist1"
    AttachmentBookName="transaction"
    OverridePath="~/Modules/Sales/Return.mix"
    DisplayWithdrawButton="true"
    DisplayViewReportButton="false"
    DisplayEmailReportButton="false"
    DisplayCustomerReportButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />