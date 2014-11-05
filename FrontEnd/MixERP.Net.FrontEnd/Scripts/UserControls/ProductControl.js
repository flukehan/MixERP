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
/*global addDanger, ajaxDataBind, ajaxUpdateVal, appendParameter, fadeThis, focusNextElement, getAjax, getColumnText, getData, getFormattedNumber, gridViewEmptyWarningLocalized, insufficientStockWarningLocalized, invalidCashRepositoryWarningLocalized, invalidCostCenterWarningLocalized, invalidDateWarningLocalized, invalidPartyWarningLocalized,invalidPriceTypeWarningLocalized, invalidSalesPersonWarningLocalized, invalidShippingCompanyWarningLocalized, invalidStoreWarningLocalized, isDate, isNullOrWhiteSpace, isSales, logError, makeDirty, parseFloat2, parseFormattedNumber, removeDirty, repaint, rowData, selectDropDownListByValue, setColumnText, shortcut, showWindow, sumOfColumn, tableToJSON, taxAfterDiscount, tranBook, unitId, updateTaxLocalized, uploadedFilesHidden, verifyStock, parseInt2, updateTaxLocalized */

if (typeof updateTaxLocalized === "undefined") {
    updateTaxLocalized = "Update tax?";
};

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

if (typeof invalidCashRepositoryWarningLocalized === "undefined") {
    invalidCashRepositoryWarningLocalized = "Invalid cash repository.";
};

if (typeof invalidCostCenterWarningLocalized === "undefined") {
    invalidCostCenterWarningLocalized = "Invalid cost center.";
};

if (typeof invalidSalesPersonWarningLocalized === "undefined") {
    invalidSalesPersonWarningLocalized = "Invalid salesperson.";
};

//Controls
var addButton = $("#AddButton");
var amountInputText = $("#AmountInputText");
var attachmentLabel = $("#AttachmentLabel");
var cashRepositorySelect = $("#CashRepositorySelect");
var cashRepositoryBalanceInputText = $("#CashRepositoryBalanceInputText");
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
var productGridView = $("#ProductGridView");
var productGridViewDataHidden = $("#ProductGridViewDataHidden");
var priceInputText = $("#PriceInputText");
var priceTypeSelect = $("#PriceTypeSelect");
var priceTypeIdHidden = $("#PriceTypeIdHidden");

var quantityInputText = $("#QuantityInputText");

var runningTotalInputText = $("#RunningTotalInputText");
var referenceNumberInputText = $("#ReferenceNumberInputText");

var saveButton = $("#SaveButton");

var salesPersonSelect = $("#SalesPersonSelect");

var shippingAddressCodeHidden = $("#ShippingAddressCodeHidden");
var shippingAddressSelect = $("#ShippingAddressSelect");
var shippingAddressTextArea = $("#ShippingAddressTextArea");
var shippingChargeInputText = $("#ShippingChargeInputText");
var shippingCompanySelect = $("#ShippingCompanySelect");
var statementReferenceTextArea = $("#StatementReferenceTextArea");
var storeSelect = $("#StoreSelect");
var subTotalInputText = $("#SubTotalInputText");

var taxRateInputText = $("#TaxRateInputText");
var taxTotalInputText = $("#TaxTotalInputText");
var taxInputText = $("#TaxInputText");
var totalAmountInputText = $("#TotalAmountInputText");
var tranIdCollectionHiddenField = $("#TranIdCollectionHiddenField");

var unitIdHidden = $("#UnitIdHidden");
var unitSelect = $("#UnitSelect");
var unitNameHidden = $("#UnitNameHidden");

//Variables
var agentId;
var attachments;

var cashRepositoryId;
var costCenterId;

var data;

var isCredit = false;

var partyCode;
var priceTypeId;

var referenceNumber;

var shippingAddressCode;
var shipperId;
var shippingCharge;
var statementReference;
var storeId;

var transactionIds;
var transactionType;

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

    addShortcuts();
    initializeAjaxData();
});

function initializeAjaxData() {
    processCallBackActions();

    loadPriceTypes();
    loadParties();

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
    loadCashRepositories();
    loadAgents();
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

function itemSelect_OnBlur() {
    itemCodeInputText.val(itemSelect.getSelectedValue());
    loadUnits();
    getPrice();
};

function processCallBackActions() {
    var itemId = parseFloat2(itemIdHidden.val());

    itemIdHidden.val("");

    if (itemId > 0) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
        data = appendParameter("", "itemId", itemId);
        data = getData(data);

        ajaxUpdateVal(url, data, itemCodeInputText);
    };

    var partyId = parseFloat2(partyIdHidden.val());

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

addButton.click(function () {
    updateTax();
    calculateAmount();
    addRow();
});

amountInputText.blur(function () {
    updateTax();
    calculateAmount();
});

attachmentLabel.click(function () {
    $('#attachment').show(500).after(function () {
        repaint();
    });
});

cashRepositorySelect.change(function () {
    if (cashRepositorySelect.getSelectedValue()) {
        url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositoryBalance";
        data = appendParameter("", "cashRepositoryId", cashRepositorySelect.getSelectedValue());
        data = appendParameter(data, "currencyCode", "");
        data = getData(data);

        ajaxUpdateVal(url, data, cashRepositoryBalanceInputText);
    };
});

discountInputText.blur(function () {
    updateTax();
    calculateAmount();
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

var validateProductControl = function () {
    valueDate = dateTextBox.val();
    errorLabelBottom.html("");

    removeDirty(dateTextBox);
    removeDirty(partyCodeInputText);
    removeDirty(partySelect);
    removeDirty(priceTypeSelect);
    removeDirty(storeSelect);
    removeDirty(shippingCompanySelect);
    removeDirty(cashRepositorySelect);
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
        if (parseFloat2(storeSelect.getSelectedValue()) <= 0) {
            makeDirty(storeSelect);
            errorLabelBottom.html(invalidStoreWarningLocalized);
            return false;
        };
    };

    if (isNullOrWhiteSpace(partyCodeInputText.val())) {
        errorLabelBottom.html(invalidPartyWarningLocalized);
        makeDirty(partyCodeInputText);
        makeDirty(partySelect);
        return false;
    };

    if (priceTypeSelect.length) {
        if (parseFloat2(priceTypeSelect.getSelectedValue()) <= 0) {
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
        if (parseFloat2(shippingCompanySelect.getSelectedValue()) <= 0) {
            makeDirty(shippingCompanySelect);
            errorLabelBottom.html(invalidShippingCompanyWarningLocalized);
            return false;
        };
    };

    if (cashRepositorySelect.length) {
        if (!isCredit) {
            if (parseFloat2(cashRepositorySelect.getSelectedValue()) <= 0) {
                makeDirty(cashRepositorySelect);
                errorLabelBottom.html(invalidCashRepositoryWarningLocalized);
                return false;
            };
        };

        if (isCredit) {
            if (parseFloat2(cashRepositorySelect.getSelectedValue()) > 0) {
                makeDirty(cashRepositorySelect);
                errorLabelBottom.html(invalidCashRepositoryWarningLocalized);
                return false;
            };
        };
    };

    if (costCenterSelect.length) {
        if (parseFloat2(costCenterSelect.getSelectedValue()) <= 0) {
            makeDirty(costCenterSelect);
            errorLabelBottom.html(invalidCostCenterWarningLocalized);
            return false;
        };
    };

    if (salesPersonSelect.length) {
        if (parseFloat2(salesPersonSelect.getSelectedValue()) <= 0) {
            makeDirty(salesPersonSelect);
            errorLabelBottom.html(invalidSalesPersonWarningLocalized);
            return false;
        };
    };

    productGridViewDataHidden.val(tableToJSON(productGridView));
    agentId = parseFloat2(salesPersonSelect.getSelectedValue());
    attachments = uploadedFilesHidden.val();

    cashRepositoryId = parseFloat2(cashRepositorySelect.getSelectedValue());
    costCenterId = parseFloat2(costCenterSelect.getSelectedValue());

    data = productGridViewDataHidden.val();

    partyCode = partySelect.getSelectedValue();
    priceTypeId = parseFloat2(priceTypeSelect.getSelectedValue());

    referenceNumber = referenceNumberInputText.getSelectedValue();

    shippingAddressCode = shippingAddressSelect.getSelectedText();
    shipperId = parseFloat2(shippingCompanySelect.getSelectedValue());
    shippingCharge = parseFloat2(shippingChargeInputText.val());
    statementReference = statementReferenceTextArea.val();
    storeId = parseFloat2(storeSelect.getSelectedValue());

    transactionIds = tranIdCollectionHiddenField.val();

    return true;
};

shippingAddressSelect.change(function () {
    showShippingAddress();
});

shippingChargeInputText.blur(function () {
    summate();
});

taxRateInputText.blur(function () {
    updateTax();
    calculateAmount();
});

taxInputText.blur(function () {
    calculateAmount();
});

unitSelect.change(function () {
    unitNameHidden.val($(this).getSelectedText());
    unitIdHidden.val($(this).getSelectedValue());
});

unitSelect.blur(function () {
    getPrice();
});

function loadAddresses() {
    var partyCode = partySelect.val();

    url = "/Modules/Inventory/Services/PartyData.asmx/GetAddressByPartyCode";
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    var selectedValue = shippingAddressCodeHidden.val();

    ajaxDataBind(url, shippingAddressSelect, data, selectedValue);
};

function loadAgents() {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetAgents";
    ajaxDataBind(url, salesPersonSelect);
};

function loadCashRepositories() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositories";
    ajaxDataBind(url, cashRepositorySelect);
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
    ajaxDataBind(url, partySelect, null);
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
        ajaxDataBind(url, shippingCompanySelect);
    };
};

function loadStores() {
    if (storeSelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
        ajaxDataBind(url, storeSelect);
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

//GridView Data Function

//var clearData = function () {
//    var grid = productGridView;
//    var rows = grid.find("tr:not(:first-child):not(:last-child)");
//    rows.remove();
//};

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
        var taxRate = parseFloat2(rowData[i][8]);
        var tax = parseFloat2(rowData[i][9]);

        addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
    };
};

//New Row Helper Function
var calculateAmount = function () {
    amountInputText.val(parseFloat2(quantityInputText.val()) * parseFloat2(priceInputText.val()));

    subTotalInputText.val(parseFloat2(amountInputText.val()) - parseFloat2(discountInputText.val()));
    totalAmountInputText.val(parseFloat2(subTotalInputText.val()) + parseFloat2(taxInputText.val()));
};

var updateTax = function () {
    var total = parseFloat2(priceInputText.val()) * parseFloat2(quantityInputText.val());
    var subTotal = total - parseFloat2(discountInputText.val());
    var taxableAmount = total;

    if (taxAfterDiscount.toLowerCase() === "true") {
        taxableAmount = subTotal;
    };

    var tax = (taxableAmount * parseFloat2(parseFormattedNumber(taxRateInputText.val()))) / 100;

    if (parseFloat2(taxInputText.val()) === 0) {
        if (tax.toFixed) {
            taxInputText.val(getFormattedNumber(tax.toFixed(2)));
        } else {
            taxInputText.val(getFormattedNumber(tax));
        };
    };

    if (parseFloat2(tax).toFixed(2) !== parseFloat2(parseFormattedNumber(taxInputText.val())).toFixed(2)) {
        var question = confirm(updateTaxLocalized);

        if (question) {
            if (tax.toFixed) {
                taxInputText.val(getFormattedNumber(tax.toFixed(2)));
            } else {
                taxInputText.val(getFormattedNumber(tax));
            };
        };
    };
};

//GridView Manipulation
var addRow = function () {
    itemCodeInputText.val(itemSelect.getSelectedValue());
    var itemCode = itemCodeInputText.val();
    var itemName = itemSelect.getSelectedText();
    var quantity = parseInt2(quantityInputText.val());
    var unitId = parseFloat2(unitIdHidden.val());
    var unitName = unitNameHidden.val();
    var price = parseFloat2(priceInputText.val());
    var discount = parseFloat2(discountInputText.val());
    var taxRate = parseFloat2(taxRateInputText.val());
    var tax = parseFloat2(taxInputText.val());
    var storeId = parseFloat2(storeSelect.val());

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

    if (tax < 0) {
        makeDirty(taxInputText);
        return;
    };

    removeDirty(taxInputText);

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
                addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                return;
            };

            ajaxIsStockItem.done(function (ajaxIsStockItemResult) {
                var isStockItem = ajaxIsStockItemResult.d;

                if (!isStockItem) {
                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                    return;
                };

                ajaxCountItemInStock.done(function (ajaxCountItemInStockResult) {
                    var itemInStock = parseFloat2(ajaxCountItemInStockResult.d);

                    if (quantity > itemInStock) {
                        makeDirty(quantityInputText);
                        errorLabel.html(String.format(insufficientStockWarningLocalized, itemInStock, unitName, itemName));
                        return;
                    };

                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                });
            });
        });
    });
};

var addRowToTable = function (itemCode, itemName, quantity, unitName, price, discount, taxRate, tax) {
    var grid = productGridView;
    var rows = grid.find("tr:not(:first-child):not(:last-child)");
    var amount = price * quantity;
    var subTotal = amount - discount;
    var total = subTotal + tax;
    var match = false;

    rows.each(function () {
        var row = $(this);
        if (getColumnText(row, 0) === itemCode &&
            getColumnText(row, 1) === itemName &&
            getColumnText(row, 3) === unitName &&
            parseFloat2(getColumnText(row, 4)) === price &&
            parseFloat2(getColumnText(row, 8)) === taxRate &&
            parseFloat(getColumnText(row, 5)) / parseFloat(getColumnText(row, 6)) === amount / discount) {
            setColumnText(row, 2, parseFloat2(getColumnText(row, 2)) + quantity);
            setColumnText(row, 5, parseFloat2(getColumnText(row, 5)) + amount);
            setColumnText(row, 6, parseFloat2(getColumnText(row, 6)) + discount);
            setColumnText(row, 7, parseFloat2(getColumnText(row, 7)) + subTotal);
            setColumnText(row, 9, parseFloat2(getColumnText(row, 9)) + tax);
            setColumnText(row, 10, parseFloat2(getColumnText(row, 10)) + total);

            addDanger(row);

            match = true;
            return;
        };
    });

    if (!match) {
        var html = "<tr class='grid2-row'><td>" + itemCode + "</td><td>" + itemName + "</td><td class='text-right'>" + quantity + "</td><td>" + unitName + "</td><td class='text-right'>" + price + "</td><td class='text-right'>" + amount + "</td><td class='text-right'>" + discount + "</td><td class='text-right'>" + subTotal + "</td><td class='text-right'>" + taxRate + "</td><td class='text-right'>" + tax + "</td><td class='text-right'>" + total
            + "</td><td><a class='pointer' onclick='removeRow($(this));'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td></tr>";
        grid.find("tr:last").before(html);
    };

    summate();

    itemCodeInputText.val("");
    quantityInputText.val(1);
    priceInputText.val("");
    discountInputText.val("");
    taxInputText.val("");
    errorLabel.html("");
    itemCodeInputText.focus();
    repaint();
};

//Ajax Requests
var getPrice = function () {
    var itemCode = itemCodeInputText.val();
    var partyCode = partyCodeInputText.val();
    var priceTypeId = parseFloat2(priceTypeSelect.val());
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
        taxInputText.val("");
    });

    priceAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        logError(err, "error");
    });

    url = "/Modules/Inventory/Services/ItemData.asmx/GetTaxRate";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    ajaxUpdateVal(url, data, taxRateInputText);

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

//Logic & Validation
var summate = function () {
    var runningTotal = sumOfColumn("#ProductGridView", 4);
    var taxTotal = sumOfColumn("#ProductGridView", 9);
    var grandTotal = sumOfColumn("#ProductGridView", 10);

    grandTotal += parseFloat2(shippingChargeInputText.val());

    runningTotalInputText.val(runningTotal);
    taxTotalInputText.val(taxTotal);
    grandTotalInputText.val(grandTotal);
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
        taxInputText.focus();
    });

    shortcut.add("CTRL+U", function () {
        unitSelect.focus();
    });

    shortcut.add("CTRL+ENTER", function () {
        addButton.click();
    });
};