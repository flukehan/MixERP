/********************************************************************************
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
***********************************************************************************/

/*jshint -W032, -W098, -W020 */
/*global addDanger, ajaxDataBind, ajaxUpdateVal, appendParameter, fadeThis, focusNextElement, getAjax, getColumnText, getData, getFormattedNumber, gridViewEmptyWarningLocalized, insufficientStockWarningLocalized, invalidCostCenterWarningLocalized, invalidDateWarningLocalized, invalidPartyWarningLocalized,invalidPriceTypeWarningLocalized, invalidSalesPersonWarningLocalized, invalidShippingCompanyWarningLocalized, invalidStoreWarningLocalized, isDate, isNullOrWhiteSpace, isSales, logError, makeDirty, parseFloat2, parseFormattedNumber, removeDirty, repaint, rowData, selectDropDownListByValue, setColumnText, shortcut, showWindow, sumOfColumn, tableToJSON, taxAfterDiscount, tranBook, unitId, uploadedFilesHidden, verifyStock, parseInt2 */



if (typeof insufficientStockWarningLocalized === "undefined") {
    insufficientStockWarningLocalized = "Only {0} {1} of {2} left in stock.";
};

if (typeof invalidPartyWarningLocalized === "undefined") {
    invalidPartyWarningLocalized = "Invalid party.";
};

if (typeof invalidPriceTypeWarningLocalized === "undefined") {
    invalidPriceTypeWarningLocalized = "Invalid price type.";
};

if (typeof invalidStoreWarningLocalized === "undefined") {
    invalidStoreWarningLocalized = "Invalid store.";
};

if (typeof invalidShippingCompanyWarningLocalized === "undefined") {
    invalidShippingCompanyWarningLocalized = "Invalid shipping company.";
};

if (typeof invalidCostCenterWarningLocalized === "undefined") {
    invalidCostCenterWarningLocalized = "Invalid cost center.";
};

if (typeof invalidSalesPersonWarningLocalized === "undefined") {
    invalidSalesPersonWarningLocalized = "Invalid salesperson.";
};

if (typeof invalidPaymentTermLocalized === "undefined") {
    invalidPaymentTermLocalized = "Invalid payment term.";
};

//Controls
var addButton = $("#AddButton");
var amountInputText = $("#AmountInputText");
var attachmentToggler = $("#attachmentToggler");
var cashTransactionInputCheckBox = $("#CashTransactionInputCheckBox");
var costCenterSelect = $("#CostCenterSelect");

var dateTextBox = $("#DateTextBox");
var discountInputText = $("#DiscountInputText");

var errorLabel = $("#ErrorLabel");
var errorLabelBottom = $("#ErrorLabelBottom");

var grandTotalInputText = $("#GrandTotalInputText");

var itemCodeInputText = $("#ItemCodeInputText");
var itemIdHidden = $("#ItemIdHidden");
var itemSelect = $("#ItemSelect");

var partyIdHidden = $("#PartyIdHidden");
var partySelect = $("#PartySelect");
var partyCodeInputText = $("#PartyCodeInputText");
var paymentTermSelect = $("#PaymentTermSelect");
var productGridView = $("#ProductGridView");
var productGridViewDataHidden = $("#ProductGridViewDataHidden");
var priceInputText = $("#PriceInputText");
var priceTypeSelect = $("#PriceTypeSelect");
var priceTypeIdHidden = $("#PriceTypeIdHidden");

var quantityInputText = $("#QuantityInputText");

var runningTotalInputText = $("#RunningTotalInputText");
var referenceNumberInputText = $("#ReferenceNumberInputText");

var salesTypeSelect = $("#SalesTypeSelect");
var saveButton = $("#SaveButton");

var salesPersonSelect = $("#SalesPersonSelect");

var shippingAddressSelect = $("#ShippingAddressSelect");
var shippingAddressTextArea = $("#ShippingAddressTextArea");
var shippingChargeInputText = $("#ShippingChargeInputText");
var shippingCompanySelect = $("#ShippingCompanySelect");

var storeIdHidden = $("#StoreIdHidden");
var shipperIdHidden = $("#ShipperIdHidden");
var shippingAddressCodeHidden = $("#ShippingAddressCodeHidden");
var salesPersonIdHidden = $("#SalesPersonIdHidden");

var statementReferenceTextArea = $("#StatementReferenceTextArea");
var storeSelect = $("#StoreSelect");
var subTotalInputText = $("#SubTotalInputText");

var taxTotalInputText = $("#TaxTotalInputText");

var taxSelect = $("#TaxSelect");
var taxInputText = $("#TaxInputText");

var tranIdCollectionHiddenField = $("#TranIdCollectionHiddenField");

var unitIdHidden = $("#UnitIdHidden");
var unitSelect = $("#UnitSelect");
var unitNameHidden = $("#UnitNameHidden");

//Variables
var salespersonId;
var attachments;

var costCenterId;

var data;

var isCredit = false;

var partyCode;
var paymentTermId;
var priceTypeId;

var referenceNumber;

var shippingAddressCode;
var shipperId;
var shippingCharge;
var statementReference;
var storeId;

var transactionIds;
var transactionType;

var nonTaxable;

var url;

var valueDate;

//Page Load Event
$(document).ready(function () {
    $(".form-table td").each(function () {
        var content = $(this).html();
        if (!content.trim()) {
            $(this).html("");
            $(this).hide();
        };
    });

    if (cashTransactionInputCheckBox.length === 1) {
        if (cashTransactionInputCheckBox.is(':checked')) {
            paymentTermSelect.hide();
        };
    };

    addShortcuts();
    initializeAjaxData();
});

function initializeAjaxData() {
    processCallBackActions();

    loadPriceTypes();
    loadParties();
    loadPaymentTerms();

    partySelect.change(function () {
        partyCodeInputText.val(partySelect.getSelectedValue());
        showShippingAddress();
        loadAddresses();
    });

    loadAddresses();

    loadItems();

    itemSelect.blur(function () {
        itemSelect_OnBlur();
    });

    itemSelect.change(function () {
        itemSelect_OnBlur();
    });

    loadUnits();

    loadCostCenters();
    loadStores();
    loadTaxes(tranBook);
    loadSalespersons();
    loadShippers();

    restoreData();
};

itemSelect.keydown(function (event) {
    if (event.ctrlKey) {
        if (event.key === "Enter") {
            itemSelect_OnBlur();
            focusNextElement();
            return false;
        };
    };

    return true;
});

shippingCompanySelect.blur(function () {
    var shippingCharge = parseFloat2(shippingChargeInputText.val());
    var shippingCompanyId = parseInt2(shippingCompanySelect.getSelectedValue());

    if (shippingCompanyId) {
        shippingChargeInputText.removeAttr("readonly");

        if (!shippingCharge) {
            shippingCharge = parseFloat2(shippingChargeInputText.data("val"));
            shippingChargeInputText.removeData("val");

            if (shippingCharge) {
                shippingChargeInputText.val(shippingCharge);
            };
        };
    } else {
        shippingChargeInputText.attr("readonly", "readonly");
        shippingChargeInputText.data("val", shippingCharge);
        shippingChargeInputText.val("");
    };
});

function itemSelect_OnBlur() {
    itemCodeInputText.val(itemSelect.getSelectedValue());
    loadUnits();
    getPrice();
};

function processCallBackActions() {
    var itemId = parseInt2(itemIdHidden.val());

    itemIdHidden.val("");

    if (itemId > 0) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
        data = appendParameter("", "itemId", itemId);
        data = getData(data);

        ajaxUpdateVal(url, data, itemCodeInputText);
    };

    var partyId = parseInt2(partyIdHidden.val());

    partyIdHidden.val("");

    if (partyId > 0) {
        url = "/Modules/Inventory/Services/PartyData.asmx/GetPartyCodeByPartyId";
        data = appendParameter("", "partyId", partyId);
        data = getData(data);

        ajaxUpdateVal(url, data, partyCodeInputText);
    };
};

function ajaxUpdateValCallback(targetControls) {
    if (targetControls.is(itemCodeInputText)) {
        setTimeout(function () {
            itemSelect.val(itemCodeInputText.val()).trigger('change');
            quantityInputText.focus();
        }, 500);
    };
    if (targetControls.is(partyCodeInputText)) {
        setTimeout(function () {
            partySelect.val(partyCodeInputText.val()).trigger('change');
            priceTypeSelect.focus();
        }, 500);
    };
};

//Control Events

function taxRequired() {
    var salesTax = (salesTypeSelect.length === 1 && salesTypeSelect.getSelectedValue() === "1");
    return salesTax;
};

addButton.click(function () {
    if (updateTax() === -1) {
        if (taxRequired()) {
            logError("Please select a tax form");
            return;
        };
    };

    calculateAmount();
    addRow();
});

amountInputText.blur(function () {
    updateTax();
    calculateAmount();
});

attachmentToggler.click(function () {
    $('#attachment').show(500).after(function () {
        repaint();
    });
});

discountInputText.blur(function () {
    updateTax();
    calculateAmount();
    getDefaultSalesTax();
});

itemCodeInputText.blur(function () {
    selectDropDownListByValue(this.id, 'ItemSelect');
});

partyCodeInputText.blur(function () {
    selectDropDownListByValue(this.id, 'PartySelect');
});

priceInputText.blur(function () {
    updateTax();
    calculateAmount();
});

quantityInputText.blur(function () {
    updateTax();
    calculateAmount();
});

taxSelect.blur(function () {
    updateTax();
});

function updateTax() {
    if (!taxRequired() && taxSelect.find("option").length === 1) {
        return 0;
    };

    var storeId = parseInt2(storeSelect.getSelectedValue());
    var partyCode = partySelect.getSelectedValue();
    var shippingAddressCode = shippingAddressSelect.getSelectedText();
    var priceTypeId = parseInt2(priceTypeSelect.getSelectedValue());
    var itemCode = itemCodeInputText.val();
    var price = parseFloat2(priceInputText.val());
    var quantity = parseInt2(quantityInputText.val());
    var discount = parseFloat2(discountInputText.val());
    var shippingCharge = parseFloat2(shippingChargeInputText.val());
    var salesTaxId = parseInt2(taxSelect.getSelectedValue());

    if (storeId === 0) {
        return 0;
    };

    if (isNullOrWhiteSpace(partyCode)) {
        return 0;
    };

    if (salesTaxId === 0) {
        return -1;
    };

    if (price === 0) {
        return 0;
    };

    if (quantity === 0) {
        return 0;
    };

    if (salesTaxId === 0) {
        return 0;
    };

    var ajaxGetSalesTax = getSalesTax(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId);

    ajaxGetSalesTax.success(function (msg) {
        var tax = parseFloat2(msg.d);
        taxInputText.data("val", tax);

        taxInputText.val(tax);
    });

    ajaxGetSalesTax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};

function getDefaultSalesTax() {
    if (!taxRequired()) {
        return;
    };

    if (taxSelect.find("option").length === 1) {
        return;
    };

    if (salesTypeSelect.getSelectedValue() !== "1") {
        return;
    };

    var storeId = parseInt2(storeSelect.getSelectedValue());
    var partyCode = partySelect.getSelectedValue();
    var shippingAddressCode = shippingAddressSelect.getSelectedText();
    var priceTypeId = parseInt2(priceTypeSelect.getSelectedValue());
    var itemCode = itemCodeInputText.val();
    var unitId = parseInt2(unitSelect.getSelectedValue());
    var price = parseFloat2(priceInputText.val()) - parseFloat2(discountInputText.val());

    if (!storeId) {
        return;
    };

    if (isNullOrWhiteSpace(partyCode)) {
        return;
    };

    if (isNullOrWhiteSpace(itemCode)) {
        return;
    };

    if (!unitId) {
        return;
    };

    if (!price) {
        return;
    };

    var ajaxGetDefaultSalesTaxId = getDefaultSalesTaxId(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, unitId, price);
    ajaxGetDefaultSalesTaxId.success(function (msg) {
        var result = parseInt2(msg.d);

        if (result) {
            taxSelect.val(result);
        };
    });

    ajaxGetDefaultSalesTaxId.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};

var validateProductControl = function () {
    valueDate = Date.parseExact(dateTextBox.val(), window.shortDateFormat);
    errorLabelBottom.html("");

    removeDirty(dateTextBox);
    removeDirty(partyCodeInputText);
    removeDirty(partySelect);
    removeDirty(priceTypeSelect);
    removeDirty(storeSelect);
    removeDirty(shippingCompanySelect);
    removeDirty(costCenterSelect);
    removeDirty(salesPersonSelect);

    if (cashTransactionInputCheckBox) {
        if (cashTransactionInputCheckBox.is(":checked")) {
            isCredit = false;
            transactionType = "Cash";
        } else {
            isCredit = true;
            transactionType = "Credit";
        };
    };

    if (!isDate(valueDate)) {
        makeDirty(dateTextBox);
        errorLabelBottom.html(invalidDateWarningLocalized);
        return false;
    };

    if (storeSelect.length) {
        if (parseInt2(storeSelect.getSelectedValue()) <= 0) {
            makeDirty(storeSelect);
            errorLabelBottom.html(invalidStoreWarningLocalized);
            return false;
        };
    };

    if (paymentTermSelect.length && paymentTermSelect.is(":visible")) {
        if (parseInt2(paymentTermSelect.getSelectedValue()) <= 0) {
            makeDirty(paymentTermSelect);
            errorLabelBottom.html(invalidPaymentTermLocalized);
            return false;
        };
    };

    removeDirty(paymentTermSelect);

    if (isNullOrWhiteSpace(partyCodeInputText.val())) {
        errorLabelBottom.html(invalidPartyWarningLocalized);
        makeDirty(partyCodeInputText);
        makeDirty(partySelect);
        return false;
    };

    if (priceTypeSelect.length) {
        if (parseInt2(priceTypeSelect.getSelectedValue()) <= 0) {
            makeDirty(priceTypeSelect);
            errorLabelBottom.html(invalidPriceTypeWarningLocalized);
            return false;
        };
    };

    if (productGridView.find("tr").length === 2) {
        errorLabelBottom.html(gridViewEmptyWarningLocalized);
        return false;
    };

    if (shippingCompanySelect.length) {
        if (parseInt2(shippingCompanySelect.getSelectedValue()) <= 0) {
            makeDirty(shippingCompanySelect);
            errorLabelBottom.html(invalidShippingCompanyWarningLocalized);
            return false;
        };
    };

    if (costCenterSelect.length) {
        if (parseInt2(costCenterSelect.getSelectedValue()) <= 0) {
            makeDirty(costCenterSelect);
            errorLabelBottom.html(invalidCostCenterWarningLocalized);
            return false;
        };
    };

    if (salesPersonSelect.length) {
        if (parseInt2(salesPersonSelect.getSelectedValue()) <= 0) {
            makeDirty(salesPersonSelect);
            errorLabelBottom.html(invalidSalesPersonWarningLocalized);
            return false;
        };
    };

    productGridViewDataHidden.val(tableToJSON(productGridView));
    salespersonId = parseInt2(salesPersonSelect.getSelectedValue());
    attachments = uploadedFilesHidden.val();

    costCenterId = parseInt2(costCenterSelect.getSelectedValue());

    data = productGridViewDataHidden.val();

    partyCode = partySelect.getSelectedValue();
    paymentTermId = parseInt2(paymentTermSelect.getSelectedValue());
    priceTypeId = parseInt2(priceTypeSelect.getSelectedValue());

    referenceNumber = referenceNumberInputText.getSelectedValue();

    shippingAddressCode = shippingAddressSelect.getSelectedText();
    shipperId = parseInt2(shippingCompanySelect.getSelectedValue());
    shippingCharge = parseFloat2(shippingChargeInputText.val());
    statementReference = statementReferenceTextArea.val();
    storeId = parseInt2(storeSelect.getSelectedValue());

    transactionIds = tranIdCollectionHiddenField.val();

    nonTaxable = false;

    if (salesTypeSelect.length === 1 && salesTypeSelect.getSelectedValue() === "0") {
        nonTaxable = true;
    };

    return true;
};

cashTransactionInputCheckBox.change(function () {
    var checked = !cashTransactionInputCheckBox.is(":checked");
    setVisible(paymentTermSelect, checked, 500);
});

shippingAddressSelect.change(function () {
    showShippingAddress();
});

shippingChargeInputText.blur(function () {
    summate();
});

unitSelect.change(function () {
    unitNameHidden.val($(this).getSelectedText());
    unitIdHidden.val($(this).getSelectedValue());
});

unitSelect.blur(function () {
    getPrice();
});

function loadAddresses() {
    var partyCode = partyCodeInputText.val();

    url = "/Modules/Inventory/Services/PartyData.asmx/GetAddressByPartyCode";
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    ajaxDataBind(url, shippingAddressSelect, data);
};

function ajaxDataBindCallBack(targetControlId) {
    if (targetControlId.is(shippingAddressSelect)) {
        var selectedValue = shippingAddressCodeHidden.val();

        shippingAddressSelect.find("option:contains(" + selectedValue + ")").attr('selected', true);
        shippingAddressSelect.trigger("change");
    };
};

function loadSalespersons() {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetAgents";
    ajaxDataBind(url, salesPersonSelect, null, salesPersonIdHidden.val());
};

function loadCostCenters() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCostCenters";
    ajaxDataBind(url, costCenterSelect);
};

function loadItems() {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetItems";
    data = appendParameter("", "tranBook", tranBook);
    data = getData(data);

    ajaxDataBind(url, itemSelect, data);
};

function loadParties() {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetParties";
    ajaxDataBind(url, partySelect, null, partyCodeInputText.val());
};

function loadPaymentTerms() {
    if (priceTypeSelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetPaymentTerms";
        ajaxDataBind(url, paymentTermSelect);
    };
};

function loadPriceTypes() {
    if (priceTypeSelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetPriceTypes";
        var selectedValue = priceTypeIdHidden.val();

        ajaxDataBind(url, priceTypeSelect, null, selectedValue);
    };
};

function loadShippers() {
    if (shippingCompanySelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetShippers";
        ajaxDataBind(url, shippingCompanySelect, null, shipperIdHidden.val());
    };
};

function loadStores() {
    if (storeSelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
        ajaxDataBind(url, storeSelect, null, storeIdHidden.val());
    };
};

function loadTaxes(tranBook) {
    if (taxSelect.length) {
        url = "/Modules/BackOffice/Services/TaxData.asmx/GetSalesTaxes";
        data = appendParameter("", "tranBook", tranBook);
        data = getData(data);

        ajaxDataBind(url, taxSelect, data);
    };
};

function loadUnits() {
    var itemCode = itemCodeInputText.val();

    url = "/Modules/Inventory/Services/ItemData.asmx/GetUnits";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    var selectedValue = unitIdHidden.val();

    ajaxDataBind(url, unitSelect, data, selectedValue);
};

var restoreData = function () {
    var sourceControl = productGridViewDataHidden;

    if (isNullOrWhiteSpace(sourceControl.val())) {
        return;
    };

    var rowData = JSON.parse(sourceControl.val());

    for (var i = 0; i < rowData.length; i++) {
        var itemCode = rowData[i][0];
        var itemName = rowData[i][1];
        var quantity = parseFloat2(rowData[i][2]);
        var unitName = rowData[i][3];
        var price = parseFloat2(rowData[i][4]);
        var discount = parseFloat2(rowData[i][6]);
        var shippingCharge = parseFloat2(rowData[i][7]);

        var tax = rowData[i][9];
        var computedTax = parseFloat2(rowData[i][10]);

        addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
    };
};

//New Row Helper Function
var calculateAmount = function () {
    amountInputText.val(parseFloat2(quantityInputText.val()) * parseFloat2(priceInputText.val()));

    subTotalInputText.val(parseFloat2(amountInputText.val()) - parseFloat2(discountInputText.val()));
};

//GridView Manipulation
var addRow = function () {
    if (parseInt2(taxSelect.getSelectedValue()) > 0 && typeof taxInputText.data("val") === "undefined") {
        updateTax();
        return;
    };

    itemCodeInputText.val(itemSelect.getSelectedValue());
    var itemCode = itemCodeInputText.val();
    var itemName = itemSelect.getSelectedText();
    var quantity = parseInt2(quantityInputText.val());
    var unitId = parseInt2(unitIdHidden.val());
    var unitName = unitNameHidden.val();
    var price = parseFloat2(priceInputText.val());
    var discount = parseFloat2(discountInputText.val());
    var shippingCharge = parseFloat2(shippingChargeInputText.val());
    var tax = taxSelect.getSelectedText();
    var computedTax = parseFloat2(taxInputText.val());

    var storeId = parseInt2(storeSelect.val());

    if (isNullOrWhiteSpace(itemCode)) {
        makeDirty(itemCodeInputText);
        return;
    };

    removeDirty(itemCodeInputText);

    if (quantity < 1) {
        makeDirty(quantityInputText);
        return;
    };

    removeDirty(quantityInputText);

    if (price <= 0) {
        makeDirty(priceInputText);
        return;
    };

    removeDirty(priceInputText);

    if (discount < 0) {
        makeDirty(discountInputText);
        return;
    };

    removeDirty(discountInputText);

    if (discount > (price * quantity)) {
        makeDirty(discountInputText);
        return;
    };

    removeDirty(discountInputText);

    if (taxRequired() && tax === selectLocalized) {
        makeDirty(taxSelect);
        return;
    };

    removeDirty(taxSelect);

    if (tax === selectLocalized || tax === noneLocalized) {
        tax = "";
        computedTax = 0;
    };

    var ajaxItemCodeExists = itemCodeExists(itemCode);
    var ajaxUnitNameExists = unitNameExists(unitName);
    var ajaxIsStockItem = isStockItem(itemCode);
    var ajaxCountItemInStock = countItemInStock(itemCode, unitId, storeId);

    ajaxItemCodeExists.done(function (response) {
        var itemCodeExists = response.d;

        if (!itemCodeExists) {
            $.notify(String.format("Item '{0}' does not exist.", itemCode), "error");
            makeDirty(itemCodeInputText);
            return;
        };

        removeDirty(itemCodeInputText);

        ajaxUnitNameExists.done(function (ajaxUnitNameExistsResult) {
            var unitNameExists = ajaxUnitNameExistsResult.d;

            if (!unitNameExists) {
                $.notify(String.format("Unit '{0}' does not exist.", unitName), "error");
                makeDirty(unitSelect);
                return;
            };

            removeDirty(unitSelect);

            if (!verifyStock || !isSales) {
                addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
                return;
            };

            ajaxIsStockItem.done(function (ajaxIsStockItemResult) {
                var isStockItem = ajaxIsStockItemResult.d;

                if (!isStockItem) {
                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
                    return;
                };

                ajaxCountItemInStock.done(function (ajaxCountItemInStockResult) {
                    var itemInStock = parseFloat2(ajaxCountItemInStockResult.d);

                    if (quantity > itemInStock) {
                        makeDirty(quantityInputText);
                        errorLabel.html(String.format(insufficientStockWarningLocalized, itemInStock, unitName, itemName));
                        return;
                    };

                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
                });
            });
        });
    });
};

var addRowToTable = function (itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax) {
    var grid = productGridView;
    var rows = grid.find("tbody tr:not(:last-child)");
    var amount = price * quantity;
    var subTotal = amount - discount + shippingCharge;
    var match = false;

    rows.each(function () {
        var row = $(this);

        if (getColumnText(row, 0) === itemCode &&
            getColumnText(row, 1) === itemName && //Same Item
            getColumnText(row, 3) === unitName && //Same Unit
            parseFloat2(getColumnText(row, 4)) === price &&//Same Price
            getColumnText(row, 9) === tax //Same Tax
            ) {
            setColumnText(row, 2, getFormattedNumber(parseInt2(getColumnText(row, 2)) + quantity, true));
            setColumnText(row, 5, getFormattedNumber(parseFloat2(getColumnText(row, 5)) + amount));
            setColumnText(row, 6, getFormattedNumber(parseFloat2(getColumnText(row, 6)) + discount));
            setColumnText(row, 7, getFormattedNumber(parseFloat2(getColumnText(row, 7)) + shippingCharge));
            setColumnText(row, 8, getFormattedNumber(parseFloat2(getColumnText(row, 8)) + subTotal));
            setColumnText(row, 10, getFormattedNumber(parseFloat2(getColumnText(row, 10)) + computedTax));

            addDanger(row);

            match = true;
            return;
        };
    });

    if (!match) {
        var html = "<tr class='grid2-row'><td>" + itemCode + "</td><td>" + itemName + "</td><td class='text-right'>" + getFormattedNumber(quantity, true) + "</td><td>" + unitName + "</td><td class='text-right'>" + getFormattedNumber(price) + "</td><td class='text-right'>" + getFormattedNumber(amount) + "</td><td class='text-right'>" + getFormattedNumber(discount) + "</td><td class='text-right'>" + getFormattedNumber(shippingCharge) + "</td><td class='text-right'>" + getFormattedNumber(subTotal) + "</td><td>" + tax + "</td><td class='text-right'>" + getFormattedNumber(computedTax)
            + "</td><td><a class='pointer' onclick='removeRow($(this));summate();'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td></tr>";
        grid.find("tr:last").before(html);
    };

    summate();

    itemCodeInputText.val("");
    quantityInputText.val(1);
    priceInputText.val("");
    discountInputText.val("");
    taxInputText.val("");

    if (typeof taxInputText.data("val") !== "undefined") {
        taxInputText.removeData("val");
    };

    errorLabel.html("");
    itemCodeInputText.focus();
    repaint();
};

//Ajax Requests
var getPrice = function () {
    var itemCode = itemCodeInputText.val();
    var partyCode = partyCodeInputText.val();
    var priceTypeId = parseInt2(priceTypeSelect.val());
    var unitId = unitIdHidden.val();

    if (!unitId) return;

    if (tranBook.toLowerCase() === "sales") {
        if (priceTypeId <= 0) {
            $.notify(invalidPriceTypeWarningLocalized, "error");
            priceTypeSelect.focus();
            return;
        };
    };

    url = "/Modules/Inventory/Services/ItemData.asmx/GetPrice";
    data = appendParameter("", "tranBook", tranBook);
    data = appendParameter(data, "itemCode", itemCode);
    data = appendParameter(data, "partyCode", partyCode);
    data = appendParameter(data, "priceTypeId", priceTypeId);
    data = appendParameter(data, "unitId", unitId);

    data = getData(data);

    var priceAjax = getAjax(url, data);

    priceAjax.success(function (msg) {
        priceInputText.val(msg.d);
    });

    priceAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        logError(err, "error");
    });

    calculateAmount();
};

//Boolean Validation
var itemCodeExists = function (itemCode) {
    url = "/Modules/Inventory/Services/ItemData.asmx/ItemCodeExists";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    return getAjax(url, data);
};

var isStockItem = function (itemCode) {
    url = "/Modules/Inventory/Services/ItemData.asmx/IsStockItem";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    return getAjax(url, data);
};

var unitNameExists = function (unitName) {
    url = "/Modules/Inventory/Services/ItemData.asmx/UnitNameExists";
    data = appendParameter("", "unitName", unitName);
    data = getData(data);
    return getAjax(url, data);
};

//Validation Helper Functions
var countItemInStock = function (itemCode, unitId, storeId) {
    url = "/Modules/Inventory/Services/ItemData.asmx/CountItemInStock";
    data = appendParameter("", "itemCode", itemCode);
    data = appendParameter(data, "unitId", unitId);
    data = appendParameter(data, "storeId", storeId);
    data = getData(data);

    return getAjax(url, data);
};

var countItemInStockByUnitName = function (itemCode, unitName, storeId) {
    url = "/Modules/Inventory/Services/ItemData.asmx/CountItemInStockByUnitName";
    data = appendParameter("", "itemCode", itemCode);
    data = appendParameter(data, "unitId", unitId);
    data = appendParameter(data, "storeId", storeId);
    data = getData(data);

    return getAjax(url, data);
};

var getDefaultSalesTaxId = function (tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, unitId, price) {
    url = "/Modules/BackOffice/Services/TaxData.asmx/GetSalesTaxId";
    data = appendParameter("", "tranBook", tranBook);
    data = appendParameter(data, "storeId", storeId);
    data = appendParameter(data, "partyCode", partyCode);
    data = appendParameter(data, "shippingAddressCode", shippingAddressCode);
    data = appendParameter(data, "priceTypeId", priceTypeId);
    data = appendParameter(data, "itemCode", itemCode);
    data = appendParameter(data, "unitId", unitId);
    data = appendParameter(data, "price", price);

    data = getData(data);

    return getAjax(url, data);
};

var getSalesTax = function (tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId) {
    url = "/Modules/BackOffice/Services/TaxData.asmx/GetSalesTax";
    data = appendParameter("", "storeId", storeId);
    data = appendParameter(data, "tranBook", tranBook);
    data = appendParameter(data, "partyCode", partyCode);
    data = appendParameter(data, "shippingAddressCode", shippingAddressCode);
    data = appendParameter(data, "priceTypeId", priceTypeId);
    data = appendParameter(data, "itemCode", itemCode);
    data = appendParameter(data, "price", price);
    data = appendParameter(data, "quantity", quantity);
    data = appendParameter(data, "discount", discount);
    data = appendParameter(data, "shippingCharge", shippingCharge);
    data = appendParameter(data, "salesTaxId", salesTaxId);
    data = getData(data);

    return getAjax(url, data);
};

//Logic & Validation
var summate = function () {
    var runningTotal = parseFloat2(sumOfColumn("#ProductGridView", 8));
    runningTotalInputText.val(runningTotal);

    var taxTotal = parseFloat2(sumOfColumn("#ProductGridView", 10));
    taxTotalInputText.val(taxTotal);

    grandTotalInputText.val(runningTotal + taxTotal);
};

var showShippingAddress = function () {
    shippingAddressTextArea.val((shippingAddressSelect.val()));
};

//Utilities
function addShortcuts() {
    shortcut.add("F2", function () {
        url = "/Modules/Inventory/Setup/PartiesPopup.mix?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=PartyIdHidden";
        showWindow(url);
    });

    shortcut.add("F4", function () {
        url = "/Modules/Inventory/Setup/ItemsPopup.mix?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=ItemIdHidden";
        showWindow(url);
    });

    shortcut.add("ALT+C", function () {
        itemCodeInputText.focus();
    });

    shortcut.add("CTRL+I", function () {
        itemSelect.focus();
    });

    shortcut.add("CTRL+Q", function () {
        quantityInputText.focus();
    });

    shortcut.add("ALT+P", function () {
        priceInputText.focus();
    });

    shortcut.add("CTRL+D", function () {
        discountInputText.focus();
    });

    shortcut.add("CTRL+R", function () {
        initializeAjaxData();
    });

    shortcut.add("CTRL+T", function () {
        taxSelect.focus();
    });

    shortcut.add("CTRL+U", function () {
        unitSelect.focus();
    });

    //$(document).keyup(function (e) {
    //    if (e.keyCode == 13) {
    //        if (e.ctrlKey) {
    //            addButton.click();
    //        };
    //    };
    //});

    shortcut.add("CTRL+ENTER", function () {
        if (taxSelect.is(":focus")) {
            addButton.focus();
            return;
        };

        addButton.click();
    });
};