<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GRN.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.GRN" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Receipt"
    AddNewUrl="~/Modules/Purchase/Entry/GRN.mix"
    PreviewUrl="~/Modules/Purchase/Reports/GRNReport.mix"
    ChecklistUrl="~/Modules/Purchase/Confirmation/GRN.mix" />