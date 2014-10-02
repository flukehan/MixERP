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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Order" %>
<mixerp:ProductView
    ID="ProductView1"
    runat="server"
    Book="Purchase" SubBook="Order"
    AddNewUrl="~/Modules/Purchase/Entry/Order.mix"
    PreviewUrl="~/Modules/Purchase/Reports/PurchaseOrderReport.mix"
    ChecklistUrl="~/Modules/Purchase/Confirmation/Order.mix" />