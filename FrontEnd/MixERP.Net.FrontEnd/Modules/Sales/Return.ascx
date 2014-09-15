<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Return.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Return" %>
<h2>Sales Return</h2>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales"
    SubBook="Return"
    PreviewUrl="~/Modules/Sales/Reports/SalesQuotationReport.mix"
    ChecklistUrl="~/Modules/Sales/Confirmation/Quotation.mix"
    ShowReturnButton="true" />