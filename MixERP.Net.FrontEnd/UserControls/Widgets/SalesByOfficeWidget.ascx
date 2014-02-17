<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesByOfficeWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.SalesByOfficeWidget" %>
<div class="panel double-panel">
    <div class="panel-title">
        Sales By Office (Todo: Admin Only/Child Offices Only)
    </div>
    <div class="panel-content">
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
    $(document).ready(function(){
        prepareChart("sales-by-month-datasource", "sales-by-month-canvas", "sales-by-month-legend", 'bar');
    });
</script>
