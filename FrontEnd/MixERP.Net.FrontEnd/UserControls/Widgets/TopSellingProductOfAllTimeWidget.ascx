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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopSellingProductOfAllTimeWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.TopSellingProductOfAllTimeWidget" %>
<div class="panel panel-default widget">
    <div class="panel-heading">
        <h3 class="panel-title">Top 5 Selling Products of All Time(Todo: Same)</h3>
    </div>
    <div class="panel-body">
        <table id="curr-office-top-selling-products-datasource">
            <thead>
                <tr>
                    <th></th>
                    <th>California</th>
                    <th>Brooklyn</th>
                    <th>Memphis</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th>IBM Thinkpad II</th>
                    <td>15</td>
                    <td>55</td>
                    <td>20</td>
                </tr>
                <tr>
                    <th>MacBook Pro</th>
                    <td>40</td>
                    <td>30</td>
                    <td>80</td>
                </tr>
                <tr>
                    <th>Microsoft Office</th>
                    <td>45</td>
                    <td>55</td>
                    <td>65</td>
                </tr>
                <tr>
                    <th>Acer Iconia Tab</th>
                    <td>20</td>
                    <td>85</td>
                    <td>48</td>
                </tr>
                <tr>
                    <th>Samsung Galaxy Tab</th>
                    <td>80</td>
                    <td>20</td>
                    <td>65</td>
                </tr>
            </tbody>
        </table>

        <canvas id="curr-office-top-selling-products-canvas" width="500px" height="180px"></canvas>
        <div id="curr-office-top-selling-products-legend"></div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function(){
        prepareChart("curr-office-top-selling-products-datasource", "curr-office-top-selling-products-canvas", "curr-office-top-selling-products-legend", 'bar');
    });
</script>