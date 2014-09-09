<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Confirmation.JournalVoucher" OverridePath="/Modules/Finance/JournalVoucher.html" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayPrintGLEntryButton="true"
    DisplayAttachmentButton="true"
    GLAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.html" />