<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Delivery"
    OverridePath="/Modules/Sales/Delivery.mix" %>
<mixerp:TransactionChecklist
    ID="TransactionCheckList1"
    runat="server"
    AttachmentBookName="transaction"
    OverridePath="/Modules/Sales/Delivery.mix"
    DisplayWithdrawButton="true"
    DisplayViewReportButton="true"
    DisplayEmailReportButton="true"
    DisplayCustomerReportButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    ReportPath="~/Modules/Sales/Reports/DeliveryReport.mix"
    CustomerReportPath="~/Modules/Sales/Reports/DeliveryNoteReport.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />