<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchase.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.DirectPurchase" %>
<mixerp:ProductView
    Id="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Direct"
    AddNewUrl="~/Modules/Purchase/Entry/DirectPurchase.html"
    PreviewUrl="~/Modules/Purchase/Reports/DirectPurchaseReport.html"
    ChecklistUrl="~/Modules/Purchase/Confirmation/DirectPurchase.html" />