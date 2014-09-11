<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchase.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.DirectPurchase" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Direct"
    AddNewUrl="~/Modules/Purchase/Entry/DirectPurchase.mix"
    PreviewUrl="~/Modules/Purchase/Reports/DirectPurchaseReport.mix"
    ChecklistUrl="~/Modules/Purchase/Confirmation/DirectPurchase.mix" />