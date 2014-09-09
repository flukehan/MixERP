<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GRN.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Entry.GRN"
    OverridePath="/Modules/Purchase/GRN.html" %>

<mixerp:Product runat="server"
    ID="GoodReceiptNote"
    Book="Purchase"
    SubBook="Receipt"
    ShowTransactionType="False"
    ShowStore="True"
    ShowCostCenter="True" />

<script src="../Scripts/Entry/GRN.js"></script>