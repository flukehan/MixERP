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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reorder.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Reorder" %>

<asp:PlaceHolder runat="server" ID="Placeholder1" />

<style type="text/css">
    #ReorderGrid td, #ReorderGrid th {
        vertical-align: middle !important;
        padding: 4px;
        cursor: pointer;
    }

    input[type=checkbox] { margin-left: 10px; }
</style>
<script type="text/javascript">
    var reorderGrid = $("#ReorderGrid");

    if (reorderGrid) {

        reorderGrid.on("click", "td", function() {
            var checkBox = $(this).parent().find("input[type=checkbox]");

            console.log(checkBox.is(":checked"));

            if (checkBox.is(":checked")) {
                checkBox.removeProp("checked");
            } else {
                checkBox.prop("checked", "checked");
            }

            check(checkBox);
        });

        reorderGrid.on("click", "input, select", function() {
            e.stopPropagation();
        });

        reorderGrid.addClass("table table-bordered table-hover");

        reorderGrid.find("tr td:first-child").each(function() {
            $(this).css("width", "40px");
            $(this).html("<input type='checkbox' onclick='check($(this));' />");
        });

        reorderGrid.find("tr td:nth-child(6)").each(function() {
            $(this).css("width", "224px");
            $(this).css("padding", "0");
            $(this).html("<select data-role='party' class='form-control input-sm' disabled='disabled'></select>");
        });

        reorderGrid.find("tr td:nth-child(7)").each(function() {
            $(this).css("width", "124px");
            $(this).css("padding", "0");
            $(this).html("<select data-role='unit' class='form-control input-sm' disabled='disabled'></select>");
        });

        reorderGrid.find("tr td:nth-child(8)").each(function() {
            var value = $(this).parent().find("td:nth-child(13)").html();
            $(this).css("width", "78px");
            $(this).css("padding", "0");
            $(this).html(String.format("<input type='text' data-role='quantity' class='form-control input-sm integer' disabled='disabled' value={0} />", value));
        });

        reorderGrid.find("tr td:nth-child(9)").each(function() {
            var value = $(this).parent().find("td:nth-child(15)").html();
            $(this).css("width", "100px");
            $(this).css("padding", "0");
            $(this).html(String.format("<input type='text' data-role='price' class='form-control input-sm integer' disabled='disabled' value={0} />", value));
        });

        reorderGrid.find("tr td:nth-child(10)").each(function() {
            var value = $(this).parent().find("td:nth-child(16)").html();
            $(this).css("width", "70px");
            $(this).css("padding", "0");
            $(this).html(String.format("<input type='text' data-role='tax_rate' class='form-control input-sm integer' disabled='disabled' value={0} />", value));
        });

        loadParties();
        loadUnits();
    };

    function check(sender) {
        sender.parent().parent().find("select").attr("disabled", !sender.is(":checked"));
        sender.parent().parent().find("input[type=text]").attr("disabled", !sender.is(":checked"));
    };

    //Ajax Data Binding
    function loadParties() {
        url = "/Modules/Inventory/Services/PartyData.asmx/GetParties";
        var parties = $("[data-role='party']");
        ajaxDataBind(url, parties);
    };

    function loadUnits() {

        reorderGrid.find("tr").not(":first-child").each(function() {
            var row = $(this);

            var itemCode = row.find("td:nth-child(3)").html();
            var unitSelect = row.find("td:nth-child(7)").find("select");

            url = "/Modules/Inventory/Services/ItemData.asmx/GetUnits";
            data = appendParameter("", "itemCode", itemCode);
            data = getData(data);

            ajaxDataBind(url, unitSelect, data);

        });

    };

    function ajaxDataBindCallBack(targetControl) {
        var selectedText;

        if (targetControl.attr("data-role") === "party") {
            targetControl.each(function() {
                selectedText = $(this).parent().parent().find("td:nth-child(14)").html();

                $(this).find("option").filter(function() {
                    return this.text === selectedText;
                }).attr("selected", true);
            });
        };

        if (targetControl.attr("data-role") === "unit") {
            selectedText = targetControl.parent().parent().find("td:nth-child(5)").html();
            targetControl.find("option").filter(function() {
                return this.text === selectedText;
            }).attr("selected", true);
        }
    };

    function ReorderInputButtonClick() {

        function Detail(itemId, supplierCode, unitId, orderQuantity, price, taxRate) {
            this.ItemId = itemId;
            this.SupplierCode = supplierCode;
            this.UnitId = unitId;
            this.OrderQuantity = orderQuantity;
            this.Price = price;
            this.TaxRate = taxRate;
        };

        if (reorderGrid) {
            var details = new Array();

            reorderGrid.find("tr").not(":first-child").each(function() {
                var selected = $(this).find(":first-child").find("input").is(":checked");

                if (selected) {
                    var itemId = parseInt2($(this).find("td:nth-child(2)").html());
                    var supplierCode = $(this).find("td:nth-child(6)").find("select").getSelectedValue();
                    var unitId = parseInt2($(this).find("td:nth-child(7)").find("select").getSelectedValue());
                    var orderQuantity = parseFloat2($(this).find("td:nth-child(8)").find("input").val());
                    var price = parseFloat2($(this).find("td:nth-child(9)").find("input").val());
                    var taxRate = parseFloat2($(this).find("td:nth-child(10)").find("input").val());

                    var detail = new Detail(itemId, supplierCode, unitId, orderQuantity, price, taxRate);
                    details.push(detail);
                };
            });

            if (details.length === 0) {
                $.notify(nothingSelectedLocalized);
                return;
            };
            var confirmLocalized = "This action will create purchase orders automatically for you. Would you like to continue?";
            if (confirm(confirmLocalized)) {
                var ajaxsaveReorder = saveReorder(details);

                ajaxsaveReorder.success(function(msg) {
                    if (msg.d) {
                        window.location = "/Modules/Purchase/Order.mix";
                    };
                });

                ajaxsaveReorder.fail(function(xhr) {
                    logAjaxErrorMessage(xhr);
                });

            };
        };
    };

    function saveReorder(details) {
        var d;
        d = appendParameter("", "details", details);
        d = getData(d);

        var url = "/Modules/Purchase/Services/Reorder.asmx/Save";
        return getAjax(url, d);
    };
</script>