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
/*global addDanger, ajaxDataBind, ajaxUpdateVal, appendParameter, fadeThis, getAjax, getColumnText, getData, getFormattedNumber, Resources, Resources, isDate, isNullOrWhiteSpace, isSales, logError, makeDirty, parseFloat, parseFormattedNumber, removeDirty, repaint, rowData, selectDropDownListByValue, setColumnText, shortcut, showWindow, sumOfColumn, tableToJSON, tranBook, unitId, uploadedFilesHidden, verifyStock, parseInt */

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
            return false;
        };
    };

    return true;
});

shippingCompanySelect.blur(function () {
    var shippingCharge = parseFloat(shippingChargeInputText.val() || 0);
    var shippingCompanyId = parseInt(shippingCompanySelect.getSelectedValue() || 0);

    if (shippingCompanyId) {
        shippingChargeInputText.removeAttr("readonly");

        if (!shippingCharge) {
            shippingCharge = parseFloat(shippingChargeInputText.data("val") || 0);
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

    var isCompoundItem = itemSelect.find(":selected").closest("optgroup").data("compound-item");

    if (isCompoundItem) {
        unitSelect.disable();
        priceInputText.disable();
        discountInputText.disable();
        shippingChargeInputText.disable();
        return;
    };

    unitSelect.enable();
    priceInputText.enable();
    discountInputText.enable();
    shippingChargeInputText.enable();

    loadUnits();
    getPrice();
};


function processCallBackActions() {
    var itemId = parseInt(itemIdHidden.val() || 0);

    itemIdHidden.val("");

    if (itemId > 0) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
        data = appendParameter("", "itemId", itemId);
        data = getData(data);

        ajaxUpdateVal(url, data, itemCodeInputText);
    };

    var partyId = parseInt(partyIdHidden.val() || 0);

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
    var isCompoundItem = itemSelect.find(":selected").closest("optgroup").data("compound-item");

    if (isCompoundItem) {
        addCompoundItems();
        return;
    };


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
    $('#attachment').toggle(500).after(function () {
        $('#attachment').removeClass("initially hidden");
        repaint();
    });
});

discountInputText.blur(function () {
    updateTax();
    calculateAmount();
    getDefaultSalesTax();
});

shippingChargeInputText.blur(function () {
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

taxSelect.blur(function () {
    updateTax();
});

function updateTax() {
    if (!taxRequired() && taxSelect.find("option").length === 1) {
        return 0;
    };

    var storeId = parseInt(storeSelect.getSelectedValue() || 0);
    var partyCode = partySelect.getSelectedValue();
    var shippingAddressCode = shippingAddressSelect.getSelectedText();
    var priceTypeId = parseInt(priceTypeSelect.getSelectedValue() || 0);
    var itemCode = itemCodeInputText.val();
    var price = parseFloat(priceInputText.val() || 0);
    var quantity = parseInt(quantityInputText.val() || 0);
    var discount = parseFloat(discountInputText.val() || 0);
    var shippingCharge = parseFloat(shippingChargeInputText.val() || 0);
    var salesTaxId = parseInt(taxSelect.getSelectedValue() || 0);

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


    addLoader(productGridView.parent());
    var ajaxGetSalesTax = getSalesTax(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, price, quantity, discount, shippingCharge, salesTaxId);

    ajaxGetSalesTax.success(function (msg) {
        removeLoader(productGridView.parent());

        var tax = parseFloat(msg.d || 0);
        taxInputText.data("val", tax);

        taxInputText.val(tax);
    });

    ajaxGetSalesTax.fail(function (xhr) {
        removeLoader(productGridView.parent());

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

    var storeId = parseInt(storeSelect.getSelectedValue() || 0);
    var partyCode = partySelect.getSelectedValue();
    var shippingAddressCode = shippingAddressSelect.getSelectedText();
    var priceTypeId = parseInt(priceTypeSelect.getSelectedValue() || 0);
    var itemCode = itemCodeInputText.val();
    var unitId = parseInt(unitSelect.getSelectedValue() || 0);
    var price = parseFloat(priceInputText.val() || 0) - parseFloat(discountInputText.val() || 0);

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

    addLoader(productGridView.parent());

    var ajaxGetDefaultSalesTaxId = getDefaultSalesTaxId(tranBook, storeId, partyCode, shippingAddressCode, priceTypeId, itemCode, unitId, price);
    ajaxGetDefaultSalesTaxId.success(function (msg) {
        removeLoader(productGridView.parent());

        var result = parseInt(msg.d || 0);

        if (result) {
            taxSelect.val(result);
        };
    });

    ajaxGetDefaultSalesTaxId.fail(function (xhr) {
        removeLoader(productGridView.parent());

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
        errorLabelBottom.html(Resources.Warnings.InvalidDate());
        return false;
    };

    if (storeSelect.length) {
        if (parseInt(storeSelect.getSelectedValue() || 0) <= 0) {
            makeDirty(storeSelect);
            errorLabelBottom.html(Resources.Warnings.InvalidStore());
            return false;
        };
    };

    if (paymentTermSelect.length && paymentTermSelect.is(":visible")) {
        if (parseInt(paymentTermSelect.getSelectedValue() || 0) <= 0) {
            makeDirty(paymentTermSelect);
            errorLabelBottom.html(Resources.Warnings.InvalidPaymentTerm());
            return false;
        };
    };

    removeDirty(paymentTermSelect);

    if (isNullOrWhiteSpace(partyCodeInputText.val())) {
        errorLabelBottom.html(Resources.Warnings.InvalidParty());
        makeDirty(partyCodeInputText);
        makeDirty(partySelect);
        return false;
    };

    if (priceTypeSelect.length) {
        if (parseInt(priceTypeSelect.getSelectedValue() || 0) <= 0) {
            makeDirty(priceTypeSelect);
            errorLabelBottom.html(Resources.Warnings.InvalidPriceType());
            return false;
        };
    };

    if (productGridView.find("tr").length === 2) {
        errorLabelBottom.html(Resources.Warnings.GridViewEmpty());
        return false;
    };

    if (shippingCompanySelect.length) {
        if (parseInt(shippingCompanySelect.getSelectedValue() || 0) <= 0) {
            makeDirty(shippingCompanySelect);
            errorLabelBottom.html(Resources.Warnings.InvalidShippingCompany());
            return false;
        };
    };

    if (costCenterSelect.length) {
        if (parseInt(costCenterSelect.getSelectedValue() || 0) <= 0) {
            makeDirty(costCenterSelect);
            errorLabelBottom.html(Resources.Warnings.InvalidCostCenter());
            return false;
        };
    };

    if (salesPersonSelect.length) {
        if (parseInt(salesPersonSelect.getSelectedValue() || 0) <= 0) {
            makeDirty(salesPersonSelect);
            errorLabelBottom.html(Resources.Warnings.InvalidSalesPerson());
            return false;
        };
    };

    productGridViewDataHidden.val(tableToJSON(productGridView));
    salespersonId = parseInt(salesPersonSelect.getSelectedValue() || 0);
    attachments = uploadedFilesHidden.val();

    costCenterId = parseInt(costCenterSelect.getSelectedValue() || 0);

    data = productGridViewDataHidden.val();

    partyCode = partySelect.getSelectedValue();
    paymentTermId = parseInt(paymentTermSelect.getSelectedValue() || 0);
    priceTypeId = parseInt(priceTypeSelect.getSelectedValue() || 0);

    referenceNumber = referenceNumberInputText.val();

    shippingAddressCode = shippingAddressSelect.getSelectedText();
    shipperId = parseInt(shippingCompanySelect.getSelectedValue() || 0);
    shippingCharge = parseFloat(shippingChargeInputText.val() || 0);
    statementReference = statementReferenceTextArea.val();
    storeId = parseInt(storeSelect.getSelectedValue() || 0);

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

    var ajax = getAjax(url, data);

    ajax.success(function (msg) {
        itemSelect.html("<option>" + Resources.Titles.Select() + "</option>");
        var itemGroup = $("<optgroup/>");
        var compoundItemGroup = $("<optgroup/>");

        itemGroup.prop("label", Resources.Titles.Items());
        compoundItemGroup.prop("label", Resources.Titles.CompoundItems());
        compoundItemGroup.attr("data-compound-item", "true");

        var items = msg.d;

        $(items).each(function () {
            var item = $("<option/>");
            item.prop("value", this.ItemCode);
            item.html(this.ItemName);

            if (this.IsCompoundItem) {
                compoundItemGroup.append(item);
            }
            else {
                itemGroup.append(item);
            };
        });

        if (compoundItemGroup.html()) {
            itemSelect.append(itemGroup);
            itemSelect.append(compoundItemGroup);
        } else {
            itemSelect.append(itemGroup.html());
        };
    });

    ajax.fail(function (xhr) {
        alert(xhr.responseText);
    });
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
        var quantity = parseFloat(rowData[i][2] || 0);
        var unitName = rowData[i][3];
        var price = parseFloat(rowData[i][4] || 0);
        var discount = parseFloat(rowData[i][6] || 0);
        var shippingCharge = parseFloat(rowData[i][7] || 0);

        var tax = rowData[i][9];
        var computedTax = parseFloat(rowData[i][10] || 0);

        addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
    };
};

//New Row Helper Function
var calculateAmount = function () {
    var amount = parseFloat(quantityInputText.val() || 0) * parseFloat(priceInputText.val() || 0);
    var subTotal = amount - parseFloat(discountInputText.val() || 0) + parseFloat(shippingChargeInputText.val() || 0);

    amountInputText.val(amount);
    subTotalInputText.val(subTotal);
};

//GridView Manipulation
var addRow = function () {
    if (parseInt(taxSelect.getSelectedValue() || 0) > 0 && typeof taxInputText.data("val") === "undefined") {
        updateTax();
        return;
    };

    itemCodeInputText.val(itemSelect.getSelectedValue());
    var itemCode = itemCodeInputText.val();
    var itemName = itemSelect.getSelectedText();
    var quantity = parseInt(quantityInputText.val() || 0);
    var unitId = parseInt(unitIdHidden.val() || 0);
    var unitName = unitNameHidden.val();
    var price = parseFloat(priceInputText.val() || 0);
    var discount = parseFloat(discountInputText.val() || 0);
    var shippingCharge = parseFloat(shippingChargeInputText.val() || 0);
    var tax = taxSelect.getSelectedText();
    var computedTax = parseFloat(taxInputText.val() || 0);

    var storeId = parseInt(storeSelect.val() || 0);

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

    if (taxRequired() && tax === Resources.Titles.Select()) {
        makeDirty(taxSelect);
        return;
    };

    removeDirty(taxSelect);

    if (tax === Resources.Titles.Select() || tax === Resources.Titles.None()) {
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
                    var itemInStock = parseFloat(ajaxCountItemInStockResult.d || 0);

                    if (quantity > itemInStock) {
                        makeDirty(quantityInputText);
                        errorLabel.html(String.format(Resources.Warnings.InsufficientStockWarning(), itemInStock, unitName, itemName));
                        return;
                    };

                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax);
                });
            });
        });
    });
};

function addCompoundItems() {
    var ajaxGetCompoundItemDetails = getCompoundItemDetails();

    ajaxGetCompoundItemDetails.success(function (msg) {
        var items = msg.d;

        $(items).each(function () {
            var item = this;
            addRowToTable(item.ItemCode, item.ItemName, item.Quantity, item.UnitName, item.Price, item.Discount, 0, taxSelect.getSelectedText(), item.ComputedTax);
        });

        unitSelect.enable();
        priceInputText.enable();
        discountInputText.enable();
        shippingChargeInputText.enable();
    });

    ajaxGetCompoundItemDetails.fail(function (xhr) {
        alert(xhr.responseText);
    });
};

var addRowToTable = function (itemCode, itemName, quantity, unitName, price, discount, shippingCharge, tax, computedTax) {
    var editMode = (addButton.val() === Resources.Titles.Update());
    var updateIndex = parseInt(addButton.attr("data-update-index") || 0);
    var rows = $("#ProductGridView").find("tbody tr:not(:last-child)");
    var amount = price * quantity;
    var subTotal = amount - discount + shippingCharge;
    var match = false;

    if (!editMode) {
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
    };

    if (!match) {
        var html = "<td>" + itemCode + "</td><td>" + itemName + "</td><td class='text-right'>" + getFormattedNumber(quantity, true) + "</td><td>" + unitName + "</td><td class='text-right'>" + getFormattedNumber(price) + "</td><td class='text-right'>" + getFormattedNumber(amount) + "</td><td class='text-right'>" + getFormattedNumber(discount) + "</td><td class='text-right'>" + getFormattedNumber(shippingCharge) + "</td><td class='text-right'>" + getFormattedNumber(subTotal) + "</td><td>" + tax + "</td><td class='text-right'>" + getFormattedNumber(computedTax)
            + "</td><td><a class='pointer' onclick='editRow($(this));'><i class='ui edit icon'></i></a><a class='pointer' onclick='removeRow($(this));summate();'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td>";

        if (editMode) {
            var target = $("#ProductGridView").find("tbody tr:nth-child(" + updateIndex + ")");
        
            var deleteAnchor = target.find("a:has(> .delete.icon)");
            deleteAnchor.removeClass("no-pointer-events");

            target.html(html);
            addButton.val(Resources.Titles.Add());
            $("#ProductGridView").find("tr").removeClass("active");
            addButton.removeAttr("data-update-index");
            target.addClass("warning");
            setInterval(function () { target.removeClass('warning') }, 3000);
        } else {
            $("#ProductGridView").find("tr:last").before("<tr class='grid2-row'>" + html + "</tr>");
        };
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
    var priceTypeId = parseInt(priceTypeSelect.val() || 0);
    var unitId = unitIdHidden.val();

    if (!unitId) return;

    if (tranBook.toLowerCase() === "sales") {
        if (priceTypeId <= 0) {
            $.notify(Resources.Warnings.InvalidPriceType(), "error");
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

    addLoader(productGridView.parent());
    var priceAjax = getAjax(url, data);

    priceAjax.success(function (msg) {
        removeLoader(productGridView.parent());

        priceInputText.val(msg.d);
    });

    priceAjax.error(function (xhr) {
        removeLoader(productGridView.parent());

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


var getCompoundItemDetails = function () {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetCompoundItemDetails";
    data = appendParameter("", "compoundItemCode", itemCodeInputText.val());
    data = appendParameter(data, "salesTaxCode", taxSelect.getSelectedText());
    data = appendParameter(data, "tranBook", window.tranBook);
    data = appendParameter(data, "storeId", storeSelect.getSelectedValue());
    data = appendParameter(data, "partyCode", partyCodeInputText.val());
    data = appendParameter(data, "priceTypeId", priceTypeSelect.getSelectedValue());
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
    var runningTotal = sumOfColumn("#ProductGridView", 8);
    runningTotalInputText.val(runningTotal);

    var taxTotal = sumOfColumn("#ProductGridView", 10);
    taxTotalInputText.val(taxTotal);

    grandTotalInputText.val(runningTotal + taxTotal);
};

var showShippingAddress = function () {
    shippingAddressTextArea.val((shippingAddressSelect.val()));
};

//Utilities
function addShortcuts() {
    shortcut.add("F2", function () {
        url = "/Modules/Inventory/Setup/PartiesPopup.mix?modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden";
        showWindow(url);
    });

    shortcut.add("F4", function () {
        url = "/Modules/Inventory/Setup/ItemsPopup.mix?modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=ItemIdHidden";
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

    shortcut.add("CTRL+ENTER", function () {
        if (taxSelect.is(":focus")) {
            addButton.focus();
            return;
        };

        addButton.click();
    });
};

function editRow(el) {
    var rows = productGridView.find("tr");
    var selectedRow = el.parent().parent();
    var selectedIndex = rows.index(selectedRow);
    var footerRow = $(".footer-row");
    var deleteAnchor = el.parent().find("a:has(> .delete.icon)");

    deleteAnchor.addClass("no-pointer-events");

    var itemCode = selectedRow.find("td:nth-child(1)").html();
    var quantity = selectedRow.find("td:nth-child(3)").html();
    var unitName = selectedRow.find("td:nth-child(4)").html();
    var price = selectedRow.find("td:nth-child(5)").html();
    var amount = selectedRow.find("td:nth-child(6)").html();
    var discount = selectedRow.find("td:nth-child(7)").html();
    var shippingCharge = selectedRow.find("td:nth-child(8)").html();
    var subTotal = selectedRow.find("td:nth-child(9)").html();
    var taxForm = selectedRow.find("td:nth-child(10)").html();
    var tax = selectedRow.find("td:nth-child(11)").html();

    itemCodeInputText.val(itemCode);
    itemSelect.val(itemCode);
    quantityInputText.val(quantity);
    unitSelect.setSelectedText(unitName);
    priceInputText.val(price);
    amountInputText.val(amount);
    discountInputText.val(discount);
    shippingChargeInputText.val(shippingCharge);
    subTotalInputText.val(subTotal);
    taxSelect.setSelectedText(taxForm);
    taxInputText.val(tax);

    rows.removeClass("active");
    selectedRow.addClass("active");
    footerRow.addClass("active");

    addButton.val(Resources.Titles.Update());

    addButton.attr("data-update-index", selectedIndex);
};