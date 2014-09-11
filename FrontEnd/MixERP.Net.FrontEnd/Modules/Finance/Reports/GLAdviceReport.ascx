<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GLAdviceReport.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.GLAdviceReport"
    MasterPageId="MixERPReportMaster.Master" OverridePath="/Modules/Finance/JournalVoucher.mix" %>
<mixerp:Report ID="Report1" runat="server"
    Path="~/Modules/Finance/Reports/Source/Transactions.GLEntry.xml"
    AutoInitialize="true" />