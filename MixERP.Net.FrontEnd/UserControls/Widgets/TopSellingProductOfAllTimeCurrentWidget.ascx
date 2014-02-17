<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopSellingProductOfAllTimeCurrentWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.TopSellingProductOfAllTimeCurrentWidget" %>
<div class="panel double-panel">
    <div class="panel-title">
        Top 5 Selling Products of All Time(Todo: Admin Only)
    </div>
    <div class="panel-content">

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
