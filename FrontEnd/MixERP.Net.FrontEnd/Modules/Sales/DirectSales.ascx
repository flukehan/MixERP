<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSales.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.DirectSales" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Sales" SubBook="Direct"
    AddNewUrl="~/Modules/Sales/Entry/DirectSales.mix"
    PreviewUrl="~/Modules/Sales/Reports/DirectSalesReport.mix"
    ChecklistUrl="~/Modules/Sales/Confirmation/DirectSales.mix" />