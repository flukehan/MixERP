<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Order" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Order"
    AddNewUrl="~/Modules/Purchase/Entry/Order.mix"
    PreviewUrl="~/Modules/Purchase/Reports/PurchaseOrderReport.mix"
    ChecklistUrl="~/Modules/Purchase/Confirmation/PurchaseOrder.mix" />