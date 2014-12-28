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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountStatement.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.AccountStatement" %>

<style type="text/css">
    #AccountOverViewGrid td:nth-child(1),
    #AccountOverViewGrid th:nth-child(1) {
        width: 200px;
    }
</style>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<script type="text/javascript">
    var accountNumberSelect = $("#AccountNumberSelect");
    var accountNumberInputText = $("#AccountNumberInputText");
    var statementGridView = $("#StatementGridView");
    var printIcon = $(".print.icon");

    printIcon.click(function () {
        var selected = $(this).parent().parent().find("td:nth-child(3)").html().trim();
        if (!isNullOrWhiteSpace(selected)) {
            var report = "/Modules/Finance/Reports/GLAdviceReport.mix?TranCode=" + selected;
            showWindow(report);
        };
    });

    function getSelectedItems() {
        var checkBoxColumnPosition = "2";
        var idColumnPosition = "3";

        var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, statementGridView);

        if (selection.length > 0) {
            $("#SelectedValuesHidden").val(selection.join(','));
            return true;
        } else {
            $.notify(nothingSelectedLocalized, "error");
            return false;
        }

    };

    $(document).ready(function () {
        loadAccounts();
        createCascadingPair(accountNumberSelect, accountNumberInputText);
        createFlaggedRows(statementGridView);
    });

    function loadAccounts() {
        var selected = accountNumberInputText.val();

        url = "/Modules/Finance/Services/AccountData.asmx/ListAccounts";
        ajaxDataBind(url, accountNumberSelect, null, selected, accountNumberInputText);
    };

    statementGridView.find('tr').click(function () {
        var checkBox = $(this).find('td input:checkbox');
        toogleSelection(checkBox);
    });
</script>