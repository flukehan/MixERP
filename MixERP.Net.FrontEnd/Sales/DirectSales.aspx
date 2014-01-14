<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DirectSales.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.DirectSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server"
        ID="DirectSalesControl"
        Book="Sales"
        SubBook="Direct"
        Text="<%$Resources:Titles, DirectSales %>"
        DisplayTransactionTypeRadioButtonList="true"
        ShowCashRepository="true"
        VerifyStock="true"
        TopPanelWidth="850"
        OnSaveButtonClick="Sales_SaveButtonClick" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
