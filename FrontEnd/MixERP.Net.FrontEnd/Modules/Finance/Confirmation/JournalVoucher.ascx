<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Confirmation.JournalVoucher" OverridePath="/Modules/Finance/JournalVoucher.mix" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />