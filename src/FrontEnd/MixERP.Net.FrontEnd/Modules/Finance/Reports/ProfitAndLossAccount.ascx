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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfitAndLossAccount.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.ProfitAndLossAccount" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<style>
    th[rowspan] {
        border: 1px solid #D4D4D5;
    }
</style>
<script type="text/javascript">
    var grid = $("#PLAccountGridView");
    var compactCheckBox = $("#CompactCheckBox");
    var isCompactHiddenField = $("#IsCompactHidden");

    $(document).ready(function () {
        grid.find("tr").each(function () {
            var isSummation = $(this).find("td:last, th:last");

            if (isSummation.find("input").is(":checked") === true) {
                $(this).addClass("positive");
                $(this).find("td").addClass("strong");
            };

            isSummation.remove();

            var isProfit = $(this).find("td:last, th:last");

            if (isProfit.find("input").is(":checked") === true) {
                $(this).addClass("negative");
                $(this).find("td").addClass("strong");
            };
            isProfit.hide();
        });

        grid.find("tr").each(function () {
            if ($(this).is(".positive, .negative") === false) {
                var cell = $(this).find("td:first-child");
                cell.html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + cell.html());
            };
        });

    });

    compactCheckBox.change(function () {
        if (compactCheckBox.is(":checked")) {
            isCompactHiddenField.val("1");
            return;
        };

        isCompactHiddenField.val("0");
    });

    var printButton = $("#PrintButton");

    printButton.click(function () {
        var templatePath = "/Reports/Print.html";
        var headerPath = "/Reports/Assets/Header.aspx";
        var title = $("h2").html();
        var targetControlId = "PLAccountGridView";
        var date = now;
        var windowName = "PLAccountGridView";
        var offsetFirst = 0;
        var offsetLast = 0;

        printGridView(templatePath, headerPath, title, targetControlId, date, user, office, windowName, offsetFirst, offsetLast);
    });

</script>