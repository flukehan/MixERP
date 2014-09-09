<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSales.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.DirectSales" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales" SubBook="Direct"
    AddNewUrl="~/Modules/Sales/Entry/DirectSales.html"
    PreviewUrl="~/Modules/Sales/Reports/DirectSalesReport.html"
    ChecklistUrl="~/Modules/Sales/Confirmation/DirectSales.html" />