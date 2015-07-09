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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Adjustment.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Adjustment" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
<script type="text/javascript">
    var showButotn = $("#ShowButton");
    var storeHidden = $("#StoreHidden");
    var storeSelect = $("#StoreSelect");
    var grid = $("#grid");
    var actualInputText = $(".actual");
    var differenceInputText = $(".difference");
    var saveButton = $("#SaveButton");
    var referenceNumberInputText = $("#ReferenceNumberInputText");
    var statementReferenceTextArea = $("#StatementReferenceTextArea");
    var valueDateTextBox = $("#ValueDateTextBox");
    var url;
    var data;

    function ShowButton_ClientClick() {
        var selected = storeSelect.getSelectedValue();
        storeHidden.val(selected);

        if (!parseInt2(selected)) {
            return false;
        };

        return true;
    };

    $(document).ready(function () {
        loadStores();
        createControls();

        var actualInputText = $(".actual");
        var differenceInputText = $(".difference");

        actualInputText.number(true, 0, decimalSeparator, thousandSeparator);
        differenceInputText.number(true, 0, decimalSeparator, thousandSeparator);

        actualInputText.blur(function () {
            var quantity = parseInt2($(this).parent().parent().find("td:nth-last-child(3)").html());
            var actual = ($(this).val());
           
            if (isNullOrWhiteSpace(actual)) {
                $(this).val(quantity);
                actual = quantity;
            }
           
            if (parseInt2(actual) > quantity) {
                makeDirty($(this).parent());
                return;
            };

            removeDirty($(this).parent());
           
            $(this).parent().parent().find("td:nth-last-child(1) input").val(quantity - actual);
        });

        differenceInputText.blur(function () {
            var quantity = parseInt2($(this).parent().parent().find("td:nth-last-child(3)").html());
            var difference = parseInt2($(this).val());

            if (difference > quantity) {
                makeDirty($(this).parent());
                return;
            };

            removeDirty($(this).parent());

            $(this).parent().parent().find("td:nth-last-child(2) input").val(quantity - difference);
        });

    });

    function createControls() {
        var header = String.format("<th>{0}</th><th>{1}</th>", Resources.Titles.Actual(), Resources.Titles.Difference());
        grid.find("thead tr").append(header);

        var rows = grid.find("tbody tr");

        var actual = "<td><input type='text' tabindex='{0}' class='actual' /></td>";
        var difference = "<td><input type='text' tabindex='{0}' class='difference' /></td>";

        rows.each(function () {

            $(this).append(String.format(actual, $(this).index() + 1));
            $(this).append(String.format(difference, $(this).index() + rows.length + 1));
        });
    };

    function loadStores() {
        if (storeSelect.length) {
            url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
            ajaxDataBind(url, storeSelect, null, storeHidden.val());
        };
    };

    var MixERP = {
        StockAdjustmentModel: function (itemCode, itemName, quantity, storeName, transferType, unitName) {
            this.ItemCode = itemCode;
            this.ItemName = itemName;
            this.Quantity = quantity;
            this.StoreName = storeName;
            this.TransferType = transferType;
            this.UnitName = unitName;
        }
    };

    saveButton.click(function () {
        var valueDate = Date.parseExact(valueDateTextBox.val(), window.shortDateFormat);
        var referenceNumber = referenceNumberInputText.val();
        var statementReference = statementReferenceTextArea.val();

        var models = new Array();

        grid.find("tbody tr").each(function () {
            var itemCode = $(this).find("td:nth-child(2)").html();
            var itemName = $(this).find("td:nth-child(3)").html();
            var quantity = parseInt2($(this).find("td:nth-child(8)").find("input").val());
            var storeName = storeSelect.getSelectedText();
            var transferType = "Credit";
            var unitName = $(this).find("td:nth-child(5)").html();

            if (quantity) {
                models.push(new MixERP.StockAdjustmentModel(itemCode, itemName, quantity, storeName, transferType, unitName));
            };
        });

        if (models.length) {
            var ajaxSave = save(valueDate, referenceNumber, statementReference, models);

            ajaxSave.success(function (msg) {
                var tranId = parseInt2(msg.d);

                if (tranId) {
                    window.location = "/Modules/Inventory/Confirmation/Adjustment.mix?TranId=" + tranId;
                };
            });

            ajaxSave.fail(function (xhr) {
                logAjaxErrorMessage(xhr);
            });
        };

    });

    function save(valueDate, referenceNumber, statementReference, models) {
        url = "/Modules/Inventory/Services/Entry/Adjustment.asmx/Save";

        data = appendParameter("", "valueDate", valueDate);
        data = appendParameter(data, "referenceNumber", referenceNumber);
        data = appendParameter(data, "statementReference", statementReference);
        data = appendParameter(data, "models", JSON.stringify(models));

        data = getData(data);

        return getAjax(url, data);
    };
</script>
