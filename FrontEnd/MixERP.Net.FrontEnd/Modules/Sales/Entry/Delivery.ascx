<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Delivery"
    OverridePath="/Modules/Sales/Delivery.html" %>
<mixerp:Product runat="server"
    ID="ProductView1"
    Book="Sales"
    SubBook="Delivery"
    ShowPriceTypes="True"
    ShowShippingInformation="True"
    ShowSalesAgents="True"
    ShowStore="True"
    ShowCostCenter="True"
    VerifyStock="True"
    TopPanelWidth="750" />
<script src="../Scripts/Entry/Delivery.js"></script>