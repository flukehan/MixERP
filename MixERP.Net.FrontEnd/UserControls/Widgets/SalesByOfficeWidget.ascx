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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesByOfficeWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.SalesByOfficeWidget" %>
<div class="panel panel-default widget">

    <div class="panel-heading">
        <h3 class="panel-title">Sales By Office (Todo: Admin Only/Child Offices Only)
        </h3>
    </div>

    <div class="panel-body">
        <table id="sales-by-month-datasource">
            <thead>
                <tr>
                    <th></th>
                    <th>Jan</th>
                    <th>Feb</th>
                    <th>Mar</th>
                    <th>Apr</th>
                    <th>May</th>
                    <th>Jun</th>
                    <th>Jul</th>
                    <th>Aug</th>
                    <th>Sep</th>
                    <th>Oct</th>
                    <th>Nov</th>
                    <th>Dec</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th>PES-NY-MEM</th>
                    <td>20</td>
                    <td>10</td>
                    <td>85</td>
                    <td>55</td>
                    <td>75</td>
                    <td>50</td>
                    <td>30</td>
                    <td>80</td>
                    <td>45</td>
                    <td>80</td>
                    <td>85</td>
                    <td>100</td>
                </tr>
                <tr>
                    <th>PES-NY-BK</th>
                    <td>50</td>
                    <td>30</td>
                    <td>45</td>
                    <td>35</td>
                    <td>66</td>
                    <td>70</td>
                    <td>74</td>
                    <td>45</td>
                    <td>85</td>
                    <td>90</td>
                    <td>92</td>
                    <td>95</td>
                </tr>
            </tbody>
        </table>

        <canvas id="sales-by-month-canvas" width="500px" height="180px"></canvas>
        <div id="sales-by-month-legend"></div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        prepareChart("sales-by-month-datasource", "sales-by-month-canvas", "sales-by-month-legend", 'bar');
    });
</script>
