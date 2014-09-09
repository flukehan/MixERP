<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Order" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Order"
    AddNewUrl="~/Modules/Purchase/Entry/Order.html"
    PreviewUrl="~/Modules/Purchase/Reports/PurchaseOrderReport.html"
    ChecklistUrl="~/Modules/Purchase/Confirmation/PurchaseOrder.html" />