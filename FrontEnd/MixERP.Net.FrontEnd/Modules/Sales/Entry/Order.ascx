<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Order"
    OverridePath="/Modules/Sales/Order.html" %>
<mixerp:Product runat="server"
    ID="SalesOrderControl"
    Book="Sales"
    SubBook="Order"
    ShowPriceTypes="True"
    ShowShippingInformation="True"
    ShowSalesAgents="True"
    ShowStore="True" />
<script src="../Scripts/Entry/Order.js"></script>