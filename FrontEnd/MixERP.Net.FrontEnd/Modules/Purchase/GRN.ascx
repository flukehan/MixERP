<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GRN.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.GRN" %>
<mixerp:ProductView
    id="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Receipt"
    AddNewUrl="~/Modules/Purchase/Entry/GRN.html"
    PreviewUrl="~/Modules/Purchase/Reports/GRNReport.html"
    ChecklistUrl="~/Modules/Purchase/Confirmation/GRN.html" />