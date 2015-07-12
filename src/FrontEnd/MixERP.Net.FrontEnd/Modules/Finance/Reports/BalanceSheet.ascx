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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BalanceSheet.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.BalanceSheet" %>

<style type="text/css">
    th:first-child {
        width: 400px;
    }
</style>
<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<script type="text/javascript">

    var previousPeriodDateTextBox = $("#PreviousPeriodDateTextBox");
    var currentPeriodDateTextBox = $("#CurrentPeriodDateTextBox");
    var factorInputText = $("#FactorInputText");
    var printButton = $("#PrintButton");

    $(function () {

        var balanceSheetGridView = $("#BalanceSheetGridView");
        var previousPeriod = Date.parseExact(previousPeriodDateTextBox.val(), window.shortDateFormat);
        var currentPeriod = Date.parseExact(currentPeriodDateTextBox.val(), window.shortDateFormat);
        var factor = parseInt2(factorInputText.val());

        var url;
        var accountId;
        var previousBalanceCell;
        var currentBalanceCell;
        var previousBalance;
        var currentBalance;
        var html;

        function createVisualHint() {
            balanceSheetGridView.find("tr td:nth-child(4)").each(function () {
                accountId = parseInt2($(this).html());

                if (!accountId) {
                    $(this).parent().addClass("hint");
                } else {
                    var account = $(this).parent().find("td:first-child");
                    account.html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + account.html());
                };
            });

            balanceSheetGridView.find("tr td:nth-child(4), th:nth-child(4)").remove();
            balanceSheetGridView.find("tr td:nth-child(4), th:nth-child(4)").remove();

        };

        function createLinkedBalances() {
            url = "/Modules/Finance/Reports/AccountStatementPopup.mix?AccountId={0}&To={1}";

            balanceSheetGridView.find("tbody tr td:nth-child(4)").each(function () {
                accountId = parseInt2($(this).html());

                previousBalanceCell = $(this).parent().find("td:nth-child(2)");
                currentBalanceCell = $(this).parent().find("td:nth-child(3)");

                previousBalance = parseFloat2(previousBalanceCell.html());
                currentBalance = parseFloat2(currentBalanceCell.html());

                previousBalance = (previousBalance === 0) ? "" : previousBalance;
                currentBalance = (currentBalance === 0) ? "" : currentBalance;

                if (accountId) {
                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=\"showWindow('" + String.format(url, accountId, previousPeriod) + "');\">" + previousBalanceCell.html() + "</a>";
                    previousBalanceCell.html(html);

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=\"showWindow('" + String.format(url, accountId, currentPeriod) + "');\">" + currentBalanceCell.html() + "</a>";
                    currentBalanceCell.html(html);
                };

                if (previousBalance < 0) {
                    previousBalanceCell.addClass("strong error");
                };

                if (currentBalance < 0) {
                    currentBalanceCell.addClass("strong error");
                };
            });

            balanceSheetGridView.find("tbody tr td:nth-child(5)").each(function () {
                if ($(this).html().toLowerCase() === "true") {
                    url = "/Modules/Finance/Reports/RetainedEarningsPopup.mix?Date={0}&Factor={1}";

                    previousBalanceCell = $(this).parent().find("td:nth-child(2)");
                    currentBalanceCell = $(this).parent().find("td:nth-child(3)");

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, previousPeriod, factor) + "')>" + previousBalanceCell.html() + "</a>";
                    previousBalanceCell.html(html);

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, currentPeriod, factor) + "')>" + currentBalanceCell.html() + "</a>";
                    currentBalanceCell.html(html);
                };

            });
        };

        createLinkedBalances();
        createVisualHint();

    });

    printButton.click(function () {
        var report = "BalanceSheetReport.mix?PreviousPeriod={0}&CurrentPeriod={1}&Factor={2}";
        var previousPeriod = Date.parseExact(previousPeriodDateTextBox.val(), window.shortDateFormat).toDateString();
        var currentPeriod = Date.parseExact(currentPeriodDateTextBox.val(), window.shortDateFormat).toDateString();
        var factor = factorInputText.val();

        report = String.format(report, previousPeriod, currentPeriod, factor);
        showWindow(report);
    });
</script>

