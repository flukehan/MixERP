//Controls
var addButton = $("#AddButton");
var amountTextBox = $("#AmountTextBox");
var attachmentLabel = $("#AttachmentLabel");
var cashRepositoryDropDownList = $("#CashRepositoryDropDownList");
var cashRepositoryBalanceTextBox = $("#CashRepositoryBalanceTextBox");
var costCenterDropDownList = $("#CostCenterDropDownList");

var dateTextBox = $("#DateTextBox");
var discountTextBox = $("#DiscountTextBox");

var errorLabel = $("#ErrorLabel");
var errorLabelBottom = $("#ErrorLabelBottom");
var errorLabelTop = $("#ErrorLabelTop");

var grandTotalTextBox = $("#GrandTotalTextBox");

var itemCodeHidden = $("#ItemCodeHidden");
var itemCodeTextBox = $("#ItemCodeTextBox");
var itemIdHidden = $("#ItemIdHidden");
var itemDropDownList = $("#ItemDropDownList");

var partyIdHidden = $("#PartyIdHidden");
var partyDropDownList = $("#PartyDropDownList");
var partyCodeHidden = $("#PartyCodeHidden");
var partyCodeTextBox = $("#PartyCodeTextBox");
var productGridView = $("#ProductGridView");
var productGridViewDataHidden = $("#ProductGridViewDataHidden");
var priceTextBox = $("#PriceTextBox");
var priceTypeDropDownList = $("#PriceTypeDropDownList");
var priceTypeIdHidden = $("#PriceTypeIdHidden");

var quantityTextBox = $("#QuantityTextBox");

var runningTotalTextBox = $("#RunningTotalTextBox");
var referenceNumberTextBox = $("#ReferenceNumberTextBox");

var saveButton = $("#SaveButton");

var salesPersonDropDownList = $("#SalespersonDropDownList");

var shippingAddressCodeHidden = $("#ShippingAddressCodeHidden");
var shippingAddressDropDownList = $("#ShippingAddressDropDownList");
var shippingAddressTextBox = $("#ShippingAddressTextBox");
var shippingChargeTextBox = $("#ShippingChargeTextBox");
var shippingCompanyDropDownList = $("#ShippingCompanyDropDownList");
var statementReferenceTextBox = $("#StatementReferenceTextBox");
var storeDropDownList = $("#StoreDropDownList");
var subTotalTextBox = $("#SubtotalTextBox");

var taxRateTextBox = $("#TaxRateTextBox");
var taxTotalTextBox = $("#TaxTotalTextBox");
var taxTextBox = $("#TaxTextBox");
var totalTextBox = $("#TotalTextBox");
var tranIdCollectionHiddenField = $("#TranIdCollectionHiddenField");
var transactionTypeRadioButtonList = $("#TransactionTypeRadioButtonList");

var unitIdHidden = $("#UnitIdHidden");
var unitDropDownList = $("#UnitDropDownList");
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
        }
    });

    addShortcuts();
    initializeAjaxData();
    fadeThis("#info-panel");
});

function initializeAjaxData() {
    processCallBackActions();

    loadPriceTypes();
    loadParties();

    partyDropDownList.change(function () {
        partyCodeTextBox.val(partyDropDownList.getSelectedValue());
        partyCodeHidden.val(partyDropDownList.getSelectedValue());
        showShippingAddress();
        loadAddresses();
    });

    loadAddresses();

    loadItems();

    itemDropDownList.blur(function () {
        itemDropDownList_OnBlur();
    });

    loadUnits();

    loadCostCenters();
    loadStores();
    loadCashRepositories();
    loadAgents();
    loadShippers();

    restoreData();
};

itemDropDownList.keydown(function (event) {
    if (event.ctrlKey) {
        if (event.key == "Enter") {
            itemDropDownList_OnBlur();
            focusNextElement();

            //Swallow the key combination on the document level.
            return false;
        };
    };

    return true;
});

function itemDropDownList_OnBlur() {
    itemCodeTextBox.val(itemDropDownList.getSelectedValue());
    itemCodeHidden.val(itemDropDownList.getSelectedValue());
    loadUnits();
    getPrice();
};

function processCallBackActions() {
    var itemId = parseFloat2(itemIdHidden.val());

    itemIdHidden.val("");

    var itemCode = "";

    if (itemId > 0) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
        data = appendParameter("", "itemId", itemId);
        data = getData(data);

        ajaxUpdateVal(url, itemCodeHidden, data);
    }

    var partyId = parseFloat2(partyIdHidden.val());

    partyIdHidden.val("");

    var partyCode = "";

    if (partyId > 0) {
        url = "/Modules/Inventory/Services/PartyData.asmx/GetPartyCodeByPartyId";
        data = appendParameter("", "partyId", partyId);
        data = getData(data);

        ajaxUpdateVal(url, partyCodeHidden, data);
    }
};

//Control Events

addButton.click(function () {
    updateTax();
    calculateAmount();
    addRow();
});

amountTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

attachmentLabel.click(function () {
    $('#attachment').show(500).after(function () {
        repaint();
    });
});

cashRepositoryDropDownList.change(function () {
    if (cashRepositoryDropDownList.getSelectedValue()) {
        url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositoryBalance";
        data = appendParameter("", "cashRepositoryId", cashRepositoryDropDownList.getSelectedValue());
        data = appendParameter(data, "currencyCode", "");
        data = getData(data);

        ajaxUpdateVal(url, cashRepositoryBalanceTextBox, data);
    };
});

discountTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

itemCodeTextBox.blur(function () {
    selectDropDownListByValue(this.id, 'ItemDropDownList');
});

partyCodeTextBox.blur(function () {
    selectDropDownListByValue(this.id, 'PartyDropDownList');
});

priceTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

quantityTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

var validateProductControl = function () {
    valueDate = dateTextBox.val();
    errorLabelBottom.html("");

    removeDirty(dateTextBox);
    removeDirty(partyCodeTextBox);
    removeDirty(partyDropDownList);
    removeDirty(priceTypeDropDownList);
    removeDirty(storeDropDownList);
    removeDirty(shippingCompanyDropDownList);
    removeDirty(cashRepositoryDropDownList);
    removeDirty(costCenterDropDownList);
    removeDirty(salesPersonDropDownList);

    transactionType = transactionTypeRadioButtonList.find("input:checked").val();

    if (transactionType) {
        isCredit = (transactionType.toLowerCase() == "credit");
    };

    if (!isDate(valueDate)) {
        makeDirty(dateTextBox);
        errorLabelBottom.html(invalidDateWarningLocalized);
        return false;
    };

    if (storeDropDownList.length) {
        if (parseFloat2(storeDropDownList.getSelectedValue()) <= 0) {
            makeDirty(storeDropDownList);
            errorLabelBottom.html(invalidStoreWarningLocalized);
            return false;
        };
    };

    if (isNullOrWhiteSpace(partyCodeTextBox.val())) {
        errorLabelBottom.html(invalidPartyWarningLocalized);
        makeDirty(partyCodeTextBox);
        makeDirty(partyDropDownList);
        return false;
    };

    if (priceTypeDropDownList.length) {
        if (parseFloat2(priceTypeDropDownList.getSelectedValue()) <= 0) {
            makeDirty(priceTypeDropDownList);
            errorLabelBottom.html(invalidPriceTypeWarningLocalized);
            return false;
        };
    }

    if (productGridView.find("tr").length == 2) {
        errorLabelBottom.html(gridViewEmptyWarningLocalized);
        return false;
    };

    if (shippingCompanyDropDownList.length) {
        if (parseFloat2(shippingCompanyDropDownList.getSelectedValue()) <= 0) {
            makeDirty(shippingCompanyDropDownList);
            errorLabelBottom.html(invalidShippingCompanyWarningLocalized);
            return false;
        };
    };

    if (cashRepositoryDropDownList.length) {
        if (!isCredit) {
            if (parseFloat2(cashRepositoryDropDownList.getSelectedValue()) <= 0) {
                makeDirty(cashRepositoryDropDownList);
                errorLabelBottom.html(invalidCashRepositoryWarningLocalized);
                return false;
            };
        }
    };

    if (costCenterDropDownList.length) {
        if (parseFloat2(costCenterDropDownList.getSelectedValue()) <= 0) {
            makeDirty(costCenterDropDownList);
            errorLabelBottom.html(invalidCostCenterWarningLocalized);
            return false;
        };
    };

    if (salesPersonDropDownList.length) {
        if (parseFloat2(salesPersonDropDownList.getSelectedValue()) <= 0) {
            makeDirty(salesPersonDropDownList);
            errorLabelBottom.html(invalidSalesPersonWarningLocalized);
            return false;
        };
    };

    productGridViewDataHidden.val(tableToJSON(productGridView));

    agentId = parseFloat2(salesPersonDropDownList.getSelectedValue());
    attachments = uploadedFilesHidden.val();

    cashRepositoryId = parseFloat2(cashRepositoryDropDownList.getSelectedValue());
    costCenterId = parseFloat2(costCenterDropDownList.getSelectedValue());

    data = productGridViewDataHidden.val();

    partyCode = partyDropDownList.getSelectedValue();
    priceTypeId = parseFloat2(priceTypeDropDownList.getSelectedValue());

    referenceNumber = referenceNumberTextBox.getSelectedValue();

    shippingAddressCode = shippingAddressDropDownList.getSelectedText();
    shipperId = parseFloat2(shippingCompanyDropDownList.getSelectedValue());
    shippingCharge = parseFloat2(shippingChargeTextBox.val());
    statementReference = statementReferenceTextBox.val();
    storeId = parseFloat2(storeDropDownList.getSelectedValue());

    transactionIds = tranIdCollectionHiddenField.val();

    return true;
};

shippingAddressDropDownList.change(function () {
    showShippingAddress();
});

shippingChargeTextBox.blur(function () {
    summate();
});

taxRateTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

taxTextBox.blur(function () {
    calculateAmount();
});

unitDropDownList.change(function () {
    unitNameHidden.val($(this).getSelectedText());
    unitIdHidden.val($(this).getSelectedValue());
});

unitDropDownList.blur(function () {
    getPrice();
});

function loadAddresses() {
    var partyCode = partyDropDownList.val();

    url = "/Modules/Inventory/Services/PartyData.asmx/GetAddressByPartyCode";
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    var selectedValue = shippingAddressCodeHidden.val();

    ajaxDataBind(url, shippingAddressDropDownList, data, selectedValue);
};

function loadAgents() {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetAgents";
    ajaxDataBind(url, salesPersonDropDownList);
};

function loadCashRepositories() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositories";
    ajaxDataBind(url, cashRepositoryDropDownList);
};

function loadCostCenters() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCostCenters";
    ajaxDataBind(url, costCenterDropDownList);
};

function loadItems() {
    url = "/Modules/Inventory/Services/ItemData.asmx/GetItems";
    data = appendParameter("", "tranBook", tranBook);
    data = getData(data);

    var selectedValue = itemCodeHidden.val();

    ajaxDataBind(url, itemDropDownList, data, selectedValue, itemCodeTextBox);
};

function loadParties() {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetParties";
    var selectedValue = partyCodeHidden.val();

    ajaxDataBind(url, partyDropDownList, null, selectedValue, partyCodeTextBox);
};

function loadPriceTypes() {
    if (priceTypeDropDownList.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetPriceTypes";
        var selectedValue = priceTypeIdHidden.val();

        ajaxDataBind(url, priceTypeDropDownList, null, selectedValue);
    };
};

function loadShippers() {
    if (shippingCompanyDropDownList.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetShippers";
        ajaxDataBind(url, shippingCompanyDropDownList);
    };
};

function loadStores() {
    if (storeDropDownList.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
        ajaxDataBind(url, storeDropDownList);
    };
};

function loadUnits() {
    var itemCode = itemCodeHidden.val();

    url = "/Modules/Inventory/Services/ItemData.asmx/GetUnits";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    var selectedValue = unitIdHidden.val();

    ajaxDataBind(url, unitDropDownList, data, selectedValue);
};

//GridView Data Function

var clearData = function () {
    var grid = productGridView;
    var rows = grid.find("tr:not(:first-child):not(:last-child)");
    rows.remove();
};

var restoreData = function () {
    var sourceControl = productGridViewDataHidden;

    if (isNullOrWhiteSpace(sourceControl.val())) {
        return;
    }

    rowData = JSON.parse(sourceControl.val());

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
    }
};

var tableToJSON = function (grid) {
    var colData = new Array;
    var rowData = new Array;

    var rows = grid.find("tr:not(:first-child):not(:last-child)");

    rows.each(function () {
        var row = $(this);

        colData = new Array();

        row.find("td:not(:last-child)").each(function () {
            colData.push($(this).html());
        });

        rowData.push(colData);
    });

    data = JSON.stringify(rowData);

    return data;
};

//New Row Helper Function
var calculateAmount = function () {
    amountTextBox.val(parseFloat2(quantityTextBox.val()) * parseFloat2(priceTextBox.val()));

    subTotalTextBox.val(parseFloat2(amountTextBox.val()) - parseFloat2(discountTextBox.val()));
    totalTextBox.val(parseFloat2(subTotalTextBox.val()) + parseFloat2(taxTextBox.val()));
};

var updateTax = function () {
    var total = parseFloat2(priceTextBox.val()) * parseFloat2(quantityTextBox.val());
    var subTotal = total - parseFloat2(discountTextBox.val());
    var taxableAmount = total;

    if (taxAfterDiscount.toLowerCase() == "true") {
        taxableAmount = subTotal;
    }

    var tax = (taxableAmount * parseFloat2(parseFormattedNumber(taxRateTextBox.val()))) / 100;

    if (parseFloat2(taxTextBox.val()) == 0) {
        if (tax.toFixed) {
            taxTextBox.val(getFormattedNumber(tax.toFixed(2)));
        } else {
            taxTextBox.val(getFormattedNumber(tax));
        }
    }

    if (parseFloat2(tax).toFixed(2) != parseFloat2(parseFormattedNumber(taxTextBox.val())).toFixed(2)) {
        var question = confirm(updateTaxLocalized);

        if (question) {
            if (tax.toFixed) {
                taxTextBox.val(getFormattedNumber(tax.toFixed(2)));
            } else {
                taxTextBox.val(getFormattedNumber(tax));
            }
        }
    }
};

//GridView Manipulation
var addRow = function () {
    itemCodeTextBox.val(itemDropDownList.getSelectedValue());
    itemCodeHidden.val(itemDropDownList.getSelectedValue());

    var itemCode = itemCodeTextBox.val();
    var itemName = itemDropDownList.getSelectedText();
    var quantity = parseFloat2(quantityTextBox.val());
    var unitId = parseFloat2(unitIdHidden.val());
    var unitName = unitNameHidden.val();
    var price = parseFloat2(priceTextBox.val());
    var discount = parseFloat2(discountTextBox.val());
    var taxRate = parseFloat2(taxRateTextBox.val());
    var tax = parseFloat2(taxTextBox.val());
    var storeId = parseFloat2(storeDropDownList.val());

    if (isNullOrWhiteSpace(itemCode)) {
        makeDirty(itemCodeTextBox);
        return;
    }

    removeDirty(itemCodeTextBox);

    if (quantity < 1) {
        makeDirty(quantityTextBox);
        return;
    }

    removeDirty(quantityTextBox);

    if (price <= 0) {
        makeDirty(priceTextBox);
        return;
    }

    removeDirty(priceTextBox);

    if (discount < 0) {
        makeDirty(discountTextBox);
        return;
    }

    removeDirty(discountTextBox);

    if (discount > (price * quantity)) {
        makeDirty(discountTextBox);
        return;
    }

    removeDirty(discountTextBox);

    if (tax < 0) {
        makeDirty(taxTextBox);
        return;
    }

    removeDirty(taxTextBox);

    var ajaxItemCodeExists = itemCodeExists(itemCode);
    var ajaxUnitNameExists = unitNameExists(unitName);
    var ajaxIsStockItem = isStockItem(itemCode);
    var ajaxCountItemInStock = countItemInStock(itemCode, unitId, storeId);

    ajaxItemCodeExists.done(function (response) {
        var itemCodeExists = response.d;

        if (!itemCodeExists) {
            $.notify(String.format("Item '{0}' does not exist.", itemCode), "error");
            makeDirty(itemCodeTextBox);
            return;
        }

        removeDirty(itemCodeTextBox);

        ajaxUnitNameExists.done(function (ajaxUnitNameExistsResult) {
            var unitNameExists = ajaxUnitNameExistsResult.d;

            if (!unitNameExists) {
                $.notify(String.format("Unit '{0}' does not exist.", unitName), "error");
                makeDirty(unitDropDownList);
                return;
            }

            removeDirty(unitDropDownList);

            if (!verifyStock || !isSales) {
                addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                return;
            }

            ajaxIsStockItem.done(function (ajaxIsStockItemResult) {
                var isStockItem = ajaxIsStockItemResult.d;

                if (!isStockItem) {
                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                    return;
                }

                ajaxCountItemInStock.done(function (ajaxCountItemInStockResult) {
                    var itemInStock = parseFloat2(ajaxCountItemInStockResult.d);

                    if (quantity > itemInStock) {
                        makeDirty(quantityTextBox);
                        errorLabel.html(String.format(insufficientStockWarningLocalized, itemInStock, unitName, itemName));
                        return;
                    }

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
        if (getColumnText(row, 0) == itemCode &&
            getColumnText(row, 1) == itemName &&
            getColumnText(row, 3) == unitName &&
            getColumnText(row, 4) == price &&
            getColumnText(row, 8) == taxRate &&
            parseFloat(getColumnText(row, 5)) / parseFloat(getColumnText(row, 6)) == amount / discount) {
            setColumnText(row, 2, parseFloat2(getColumnText(row, 2)) + quantity);
            setColumnText(row, 5, parseFloat2(getColumnText(row, 5)) + amount);
            setColumnText(row, 6, parseFloat2(getColumnText(row, 6)) + discount);
            setColumnText(row, 7, parseFloat2(getColumnText(row, 7)) + subTotal);
            setColumnText(row, 9, parseFloat2(getColumnText(row, 9)) + tax);
            setColumnText(row, 10, parseFloat2(getColumnText(row, 10)) + total);

            addDanger(row);

            match = true;
            return;
        }
    });

    if (!match) {
        var html = "<tr class='grid2-row'><td>" + itemCode + "</td><td>" + itemName + "</td><td class='text-right'>" + quantity + "</td><td>" + unitName + "</td><td class='text-right'>" + price + "</td><td class='text-right'>" + amount + "</td><td class='text-right'>" + discount + "</td><td class='text-right'>" + subTotal + "</td><td class='text-right'>" + taxRate + "</td><td class='text-right'>" + tax + "</td><td class='text-right'>" + total
            + "</td><td><span class='glyphicon glyphicon-remove-circle pointer span-icon' onclick='removeRow($(this));'></span><span class='glyphicon glyphicon-ok-sign pointer span-icon' onclick='toggleDanger($(this));'></span><span class='glyphicon glyphicon glyphicon-thumbs-up pointer span-icon' onclick='toggleSuccess($(this));'></span></td></tr>";
        grid.find("tr:last").before(html);
    }

    summate();

    itemCodeTextBox.val("");
    quantityTextBox.val(1);
    priceTextBox.val("");
    discountTextBox.val("");
    taxTextBox.val("");
    errorLabel.html("");
    itemCodeTextBox.focus();
    repaint();
};

//Ajax Requests
var getPrice = function () {
    var itemCode = itemCodeHidden.val();
    var partyCode = partyCodeHidden.val();
    var priceTypeId = parseFloat2(priceTypeDropDownList.val());
    var unitId = unitIdHidden.val();

    if (!unitId) return;

    if (tranBook.toLowerCase() == "sales") {
        if (priceTypeId <= 0) {
            $.notify(invalidPriceTypeWarningLocalized, "error");
            priceTypeDropDownList.focus();
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
        priceTextBox.val(msg.d);
        taxTextBox.val("");
    });

    priceAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        logError(err, "error");
    });

    url = "/Modules/Inventory/Services/ItemData.asmx/GetTaxRate";
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    ajaxUpdateVal(url, taxRateTextBox, data);

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

    grandTotal += parseFloat2(shippingChargeTextBox.val());

    runningTotalTextBox.val(runningTotal);
    taxTotalTextBox.val(taxTotal);
    grandTotalTextBox.val(grandTotal);
};

var showShippingAddress = function () {
    shippingAddressTextBox.val((shippingAddressDropDownList.val()));
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
        itemCodeTextBox.focus();
    });

    shortcut.add("CTRL+I", function () {
        itemDropDownList.focus();
    });

    shortcut.add("CTRL+Q", function () {
        quantityTextBox.focus();
    });

    shortcut.add("ALT+P", function () {
        priceTextBox.focus();
    });

    shortcut.add("CTRL+D", function () {
        discountTextBox.focus();
    });

    shortcut.add("CTRL+R", function () {
        initializeAjaxData();
    });

    shortcut.add("CTRL+T", function () {
        taxTextBox.focus();
    });

    shortcut.add("CTRL+U", function () {
        unitDropDownList.focus();
    });

    shortcut.add("CTRL+ENTER", function () {
        addButton.click();
    });
};