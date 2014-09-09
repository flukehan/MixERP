<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Entry.Order"
    OverridePath="/Modules/Purchase/Order.html" %>

<mixerp:Product runat="server"
    ID="PurchaseOrder"
    Book="Purchase"
    SubBook="Order"
    OnSaveButtonClick="PurchaseOrder_SaveButtonClick" />
<script src="../Scripts/Entry/Order.js"></script>