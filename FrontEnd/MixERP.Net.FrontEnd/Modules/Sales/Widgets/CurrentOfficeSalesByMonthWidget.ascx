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
<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="CurrentOfficeSalesByMonthWidget.ascx.cs" 
    Inherits="MixERP.Net.Core.Modules.Sales.Widgets.CurrentOfficeSalesByMonthWidget" %>

<div class="eight wide column widget">
    <div class="ui segment">
        <h2>
            <asp:Literal runat="server" ID="TitleLiteral" />
        </h2>
        <asp:GridView runat="server" ID="SalesByMonthGridView" DataKeyNames="office" AutoGenerateColumns="False" CssClass="hidden">
            <Columns>
                <asp:BoundField HeaderText="OfficeCode" DataField="office" />
                <asp:BoundField HeaderText="Jan" DataField="jan" />
                <asp:BoundField HeaderText="Feb" DataField="feb" />
                <asp:BoundField HeaderText="Mar" DataField="mar" />
                <asp:BoundField HeaderText="Apr" DataField="apr" />
                <asp:BoundField HeaderText="May" DataField="may" />
                <asp:BoundField HeaderText="Jun" DataField="jun" />
                <asp:BoundField HeaderText="Jul" DataField="jul" />
                <asp:BoundField HeaderText="Aug" DataField="aug" />
                <asp:BoundField HeaderText="Sep" DataField="sep" />
                <asp:BoundField HeaderText="Oct" DataField="oct" />
                <asp:BoundField HeaderText="Nov" DataField="nov" />
                <asp:BoundField HeaderText="Dec" DataField="dec" />
            </Columns>
        </asp:GridView>

        <canvas id="curr-office-sales-by-month-canvas" width="500px" height="180px"></canvas>
        <div id="curr-office-sales-by-month-legend"></div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        prepareChart("SalesByMonthGridView", "curr-office-sales-by-month-canvas", "curr-office-sales-by-month-legend", 'line', false);
    });
</script>