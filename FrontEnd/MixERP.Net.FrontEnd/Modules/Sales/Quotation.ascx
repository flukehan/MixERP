<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Quotation.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Quotation" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales"
    SubBook="Quotation"
    AddNewUrl="~/Modules/Sales/Entry/Quotation.mix"
    PreviewUrl="~/Modules/Sales/Reports/SalesQuotationReport.mix"
    ChecklistUrl="~/Modules/Sales/Confirmation/Quotation.mix"
    ShowMergeToOrderButton="True"
    ShowMergeToDeliveryButton="True" />