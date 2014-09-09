<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchase.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Entry.DirectPurchase"
    OverridePath="/Modules/Purchase/DirectPurchase.html" %>

<mixerp:Product runat="server" ID="DirectPurchaseControl"
    Book="Purchase"
    SubBook="Direct"
    ShowTransactionType="true"
    ShowCashRepository="true"
    ShowStore="True"
    ShowCostCenter="True"
    OnSaveButtonClick="Purchase_SaveButtonClick"
    TopPanelWidth="750px" />

<script src="../Scripts/Entry/DirectPurchase.js"></script>