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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopSellingProductOfAllTimeCurrentWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.TopSellingProductOfAllTimeCurrentWidget" %>
<div class="panel panel-default widget">
    <div class="panel-heading">
        <h3 class="panel-title">Top 5 Selling Products of All Time(Todo: Admin Only)</h3>
    </div>
    <div class="panel-body">

        <table id="top-selling-products-datasource">
            <thead>
                <tr>
                    <th></th>
                    <th>Amount</th>
                    <th>Amount</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th>IBM Thinkpad II</th>
                    <td>10</td>
                    <td>20</td>
                </tr>
                <tr>
                    <th>MacBook Pro</th>
                    <td>35</td>
                    <td>35</td>
                </tr>
                <tr>
                    <th>Microsoft Office</th>
                    <td>35</td>
                    <td>45</td>
                </tr>
                <tr>
                    <th>Acer Iconia Tab</th>
                    <td>35</td>
                    <td>10</td>
                </tr>
                <tr>
                    <th>Samsung Galaxy Tab</th>
                    <td>35</td>
                    <td>80</td>
                </tr>
            </tbody>
        </table>

        <canvas id="top-selling-products-canvas" width="500px" height="180px"></canvas>
        <div id="top-selling-products-legend"></div>

    </div>
</div>

<script type="text/javascript">
    $(document).ready(function(){
        preparePieChart('top-selling-products-datasource', 'top-selling-products-canvas', 'top-selling-products-legend', 'pie');
    });
</script>
