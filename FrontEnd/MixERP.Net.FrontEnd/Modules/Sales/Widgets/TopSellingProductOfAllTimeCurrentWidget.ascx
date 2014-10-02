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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopSellingProductOfAllTimeCurrentWidget.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Widgets.TopSellingProductOfAllTimeCurrentWidget" %>

<div class="panel panel-default widget">
    <div class="panel-heading">
        <h3 class="panel-title">
            <asp:Literal runat="server" ID="TopSellingProductsLiteral" />
        </h3>
    </div>
    <div class="panel-body">
        <asp:GridView runat="server" ID="TopSellingProductsOfAllTimeGridView" DataKeyNames="id" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="ItemName" DataField="item_name" />
                <asp:BoundField HeaderText="TotalSales" DataField="total_sales" />
            </Columns>
        </asp:GridView>
        <canvas id="top-selling-products-canvas" width="500px" height="180px"></canvas>
        <div id="top-selling-products-legend"></div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        preparePieChart('TopSellingProductsOfAllTimeGridView', 'top-selling-products-canvas', 'top-selling-products-legend', 'pie', true);
    });
</script>