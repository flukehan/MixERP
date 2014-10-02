<%--
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Delivery"
    OverridePath="/Modules/Sales/Delivery.mix" %>
<mixerp:Product runat="server"
    ID="ProductView1"
    Book="Sales"
    SubBook="Delivery"
    ShowPriceTypes="True"
    ShowShippingInformation="True"
    ShowSalesAgents="True"
    ShowStore="True"
    ShowCostCenter="True"
    VerifyStock="True"
    TopPanelWidth="750" />
<script src="../Scripts/Entry/Delivery.js"></script>