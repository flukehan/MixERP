<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Quotation.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Quotation"
    OverridePath="/Modules/Sales/Quotation.html" %>
<mixerp:Product runat="server"
    ID="SalesQuotationControl"
    Book="Sales"
    SubBook="Quotation"
    ShowPriceTypes="True"
    ShowShippingInformation="True"
    ShowSalesAgents="True"
    ShowStore="True" />
<script src="../Scripts/Entry/Quotation.js"></script>