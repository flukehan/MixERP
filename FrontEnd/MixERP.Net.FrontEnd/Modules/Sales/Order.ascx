<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Order" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales" SubBook="Order"
    AddNewUrl="~/Modules/Sales/Entry/Order.mix"
    PreviewUrl="~/Modules/Sales/Reports/SalesOrderReport.mix"
    ChecklistUrl="~/Modules/Sales/Confirmation/Order.mix"
    ShowMergeToDeliveryButton="True" />