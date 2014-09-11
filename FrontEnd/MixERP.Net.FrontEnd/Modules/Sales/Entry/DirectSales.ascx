<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSales.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.DirectSales"
    OverridePath="/Modules/Sales/DirectSales.mix" %>
<mixerp:Product runat="server"
    ID="DirectSalesControl"
    Book="Sales"
    SubBook="Direct"
    ShowPriceTypes="True"
    ShowShippingInformation="True"
    ShowSalesAgents="True"
    ShowStore="True"
    ShowCashRepository="True"
    ShowTransactionType="True"
    ShowCostCenter="True"
    VerifyStock="true"
    TopPanelWidth="850" />
<script src="../Scripts/Entry/DirectSales.js"></script>