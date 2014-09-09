<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Delivery" %>

<mixerp:productview
    ID="ProductView1"
    runat="server"
    book="Sales" subbook="Delivery"
    addnewurl="~/Modules/Sales/Entry/Delivery.html"
    previewurl="~/Modules/Sales/Reports/SalesDeliveryReport.html"
    checklisturl="~/Modules/Sales/Confirmation/Delivery.html" />