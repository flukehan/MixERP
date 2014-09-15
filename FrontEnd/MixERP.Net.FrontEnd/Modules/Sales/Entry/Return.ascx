<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Return.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Return"
    OverridePath="~/Modules/Sales/Return.mix" %>

<h3>Sales Return</h3>

<mixerp:Product runat="server"
    ID="SalesQuotationControl"
    Book="Sales"
    SubBook="Quotation"
    ShowPriceTypes="True"
    ShowStore="True" />
<script src="../Scripts/Entry/Return.js"></script>