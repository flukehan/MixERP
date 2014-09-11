<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Delivery" %>

<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales" SubBook="Delivery"
    AddNewUrl="~/Modules/Sales/Entry/Delivery.mix"
    PreviewUrl="~/Modules/Sales/Reports/SalesDeliveryReport.mix"
    ChecklistUrl="~/Modules/Sales/Confirmation/Delivery.mix" />