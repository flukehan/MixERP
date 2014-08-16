//Controls
var addButton = $("#AddButton");
var amountTextBox = $("#AmountTextBox");

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

    addShortCuts();

    initializeAjaxData();
    bounceThis("#info-panel");
});

function initializeAjaxData() {
    logToConsole("Initializing AJAX data.");

    processCallBackActions();
    loadPriceTypes();

    loadParties();
    partyDropDownList.change(function () {
        loadAddresses();
    });
    loadAddresses();

    loadItems();

    itemDropDownList.change(function () {
        loadUnits();
    });

    loadUnits();


    loadCostCenters();
    loadStores();
    loadCashRepositories();
    loadAgents();
    loadShippers();

    restoreData();
};

function processCallBackActions() {
    var itemId = parseFloat2(itemIdHidden.val());

    itemIdHidden.val("");

    var itemCode = "";

    if (itemId > 0) {
        $.ajax({
            type: "POST",
            url: "/Services/ItemData.asmx/GetItemCodeByItemId",
            data: "{itemId:'" + itemId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                itemCode = msg.d;
                itemCodeHidden.val(itemCode);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                logError(err, "error");
            }
        });
    }

    var partyId = parseFloat2(partyIdHidden.val());

    partyIdHidden.val("");

    var partyCode = "";

    if (partyId > 0) {
        $.ajax({
            type: "POST",
            url: "/Services/PartyData.asmx/GetPartyCodeByPartyId",
            data: "{partyId:'" + partyId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                partyCode = msg.d;
                partyCodeHidden.val(partyCode);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                logError(err, "error");
            }
        });
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

cashRepositoryDropDownList.change(function () {
    $.ajax({
        type: "POST",
        url: "/Services/AccountData.asmx/GetCashRepositoryBalance",
        data: "{cashRepositoryId:'" + cashRepositoryDropDownList.getSelectedValue() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            cashRepositoryBalanceTextBox.val(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            logError(err, "error");
        }
    });

});

discountTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

itemCodeTextBox.blur(function () {
    selectDropDownListByValue(this.id, 'ItemDropDownList');
});

itemDropDownList.blur(function () {
    getPrice();
});

itemDropDownList.change(function () {
    itemCodeTextBox.val(itemDropDownList.getSelectedValue());
    itemCodeHidden.val(itemDropDownList.getSelectedValue());
});

partyCodeTextBox.blur(function () {
    selectDropDownListByValue(this.id, 'PartyDropDownList');
});

partyDropDownList.change(function () {
    partyCodeTextBox.val(partyDropDownList.getSelectedValue());
    partyCodeHidden.val(partyDropDownList.getSelectedValue());
    showShippingAddress();
});

priceTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

quantityTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

//Todo:Need to support localized dates
function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
}


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
        if (parseFloat2(cashRepositoryDropDownList.getSelectedValue()) <= 0) {
            makeDirty(cashRepositoryDropDownList);
            errorLabelBottom.html(invalidCashRepositoryWarningLocalized);
            return false;
        };
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



    updateData();

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
    transactionType = transactionTypeRadioButtonList.find("input:checked").val();

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

function appendItem(dropDownList, value, text) {
    dropDownList.append($("<option></option>").val(value).html(text));
};

function bindAddresses(data) {
    shippingAddressDropDownList.empty();

    if (data.length == 0) {
        appendItem(shippingAddressDropDownList, "", noneLocalized);
        return;
    }

    appendItem(shippingAddressDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(shippingAddressDropDownList, this["Value"], this["Text"]);
    });

    shippingAddressDropDownList.val(shippingAddressCodeHidden.val());
};

function bindAgents(data) {
    salesPersonDropDownList.empty();

    if (data.length == 0) {
        appendItem(salesPersonDropDownList, "", noneLocalized);
        return;
    }

    appendItem(salesPersonDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(salesPersonDropDownList, this["Value"], this["Text"]);
    });
};

function bindCashRepositories(data) {
    cashRepositoryDropDownList.empty();

    if (data.length == 0) {
        appendItem(cashRepositoryDropDownList, "", noneLocalized);
        return;
    }

    appendItem(cashRepositoryDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(cashRepositoryDropDownList, this["Value"], this["Text"]);
    });
};

function bindCostCenters(data) {
    costCenterDropDownList.empty();

    if (data.length == 0) {
        appendItem(costCenterDropDownList, "", noneLocalized);
        return;
    }

    appendItem(costCenterDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(costCenterDropDownList, this["Value"], this["Text"]);
    });
};

function bindItems(data) {
    itemDropDownList.empty();

    if (data.length == 0) {
        appendItem(itemDropDownList, "", noneLocalized);
        return;
    }

    appendItem(itemDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(itemDropDownList, this["Value"], this["Text"]);
    });

    itemCodeTextBox.val(itemCodeHidden.val());
    itemDropDownList.val(itemCodeHidden.val());
};

function bindParties(data) {
    partyDropDownList.empty();

    if (data.length == 0) {
        appendItem(partyDropDownList, "", noneLocalized);
        return;
    }

    appendItem(partyDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(partyDropDownList, this["Value"], this["Text"]);
    });

    partyCodeTextBox.val(partyCodeHidden.val());
    partyDropDownList.val(partyCodeHidden.val());
};

function bindPriceTypes(data) {
    priceTypeDropDownList.empty();

    if (data.length == 0) {
        appendItem(priceTypeDropDownList, "", noneLocalized);
        return;
    }

    appendItem(priceTypeDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(priceTypeDropDownList, this["Value"], this["Text"]);
    });

    priceTypeDropDownList.val(priceTypeIdHidden.val());
};

function bindShippers(data) {
    shippingCompanyDropDownList.empty();

    if (data.length == 0) {
        appendItem(shippingCompanyDropDownList, "", noneLocalized);
        return;
    }

    appendItem(shippingCompanyDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(shippingCompanyDropDownList, this["Value"], this["Text"]);
    });
};

function bindStores(data) {
    storeDropDownList.empty();

    if (data.length == 0) {
        appendItem(storeDropDownList, "", noneLocalized);
        return;
    }

    appendItem(storeDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(storeDropDownList, this["Value"], this["Text"]);
    });
};

function bindUnits(data) {
    unitDropDownList.empty();

    if (data.length == 0) {
        appendItem(unitDropDownList, "", noneLocalized);
        return;
    }

    appendItem(unitDropDownList, "", selectLocalized);

    $.each(data, function () {
        appendItem(unitDropDownList, this["Value"], this["Text"]);
    });

    unitDropDownList.val(unitIdHidden.val());
};

function loadAddresses() {
    var partyCode = partyDropDownList.val();

    $.ajax({
        type: "POST",
        url: "/Services/PartyData.asmx/GetAddressByPartyCode",
        data: "{partyCode:'" + partyCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindAddresses(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            appendItem(shippingAddressDropDownList, 0, err.Message);
        }
    });
};

function loadAgents() {
    if (salesPersonDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/ItemData.asmx/GetAgents",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindAgents(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(salesPersonDropDownList, 0, err.Message);
            }
        });
    }
};

function loadCashRepositories() {
    if (cashRepositoryDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/AccountData.asmx/GetCashRepositories",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindCashRepositories(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(cashRepositoryDropDownList, 0, err.Message);
            }
        });
    }
};

function loadCostCenters() {
    if (costCenterDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/AccountData.asmx/GetCostCenters",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindCostCenters(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(costCenterDropDownList, 0, err.Message);
            }
        });
    }
};

function loadItems() {
    $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/GetItems",
        data: "{tranBook:'" + tranBook + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindItems(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            appendItem(itemDropDownList, 0, err.Message);
        }
    });
};

function loadParties() {
    $.ajax({
        type: "POST",
        url: "/Services/PartyData.asmx/GetParties",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindParties(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            appendItem(partyDropDownList, 0, err.Message);
        }
    });
};

function loadPriceTypes() {
    if (priceTypeDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/ItemData.asmx/GetPriceTypes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindPriceTypes(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(priceTypeDropDownList, 0, err.Message);
            }
        });
    }
};

function loadShippers() {
    if (shippingCompanyDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/ItemData.asmx/GetShippers",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindShippers(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(shippingCompanyDropDownList, 0, err.Message);
            }
        });
    }
};

function loadStores() {
    if (storeDropDownList.length) {
        $.ajax({
            type: "POST",
            url: "/Services/ItemData.asmx/GetStores",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindStores(msg.d);
            },
            error: function (xhr) {
                var err = xhr.responseText;
                appendItem(storeDropDownList, 0, err.Message);
            }
        });
    }
};

function loadUnits() {
    var itemCode = itemCodeHidden.val();
    logToConsole("Loading units.");

    $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/GetUnits",
        data: "{itemCode:'" + itemCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindUnits(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            appendItem(unitDropDownList, 0, err.Message);
        }
    });
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

var updateData = function () {
    var targetControl = productGridViewDataHidden;
    var colData = new Array;
    var rowData = new Array;

    var grid = productGridView;
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
    targetControl.val(data);
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
        var question = confirm(localizedUpdateTax);

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


        ajaxUnitNameExists.done(function (result) {
            var unitNameExists = result.d;

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

            // ReSharper disable once DuplicatingLocalDeclaration
            ajaxIsStockItem.done(function (result) {
                var isStockItem = result.d;

                if (!isStockItem) {
                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                    return;
                }

                // ReSharper disable once DuplicatingLocalDeclaration
                ajaxCountItemInStock.done(function (result) {
                    var itemInStock = parseFloat2(result.d);

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

            match = true;
            return;
        }
    });

    if (!match) {
        var html = "<tr class='grid2-row'><td>" + itemCode + "</td><td>" + itemName + "</td><td class='right'>" + quantity + "</td><td>" + unitName + "</td><td class='right'>" + price + "</td><td class='right'>" + amount + "</td><td class='right'>" + discount + "</td><td class='right'>" + subTotal + "</td><td class='right'>" + taxRate + "</td><td class='right'>" + tax + "</td><td class='right'>" + total
            + "</td><td><input type='image' src='/Resource/Icons/delete-16.png' onclick='removeRow($(this));' /> </td></tr>";
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
};

var removeRow = function (cell) {

    if (confirm(areYouSureLocalized)) {
        cell.closest("tr").remove();
    }
};



//Boolean Validation
var itemCodeExists = function (itemCode) {
    return $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/ItemCodeExists",
        data: "{itemCode:'" + itemCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};

var isStockItem = function (itemCode) {
    return $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/IsStockItem",
        data: "{itemCode:'" + itemCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};

var unitNameExists = function (unitName) {
    return $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/UnitNameExists",
        data: "{unitName:'" + unitName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};



//Validation Helper Functions
var countItemInStock = function (itemCode, unitId, storeId) {
    return $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/CountItemInStock",
        data: "{itemCode:'" + itemCode + "', unitId:'" + unitId + "', storeId:'" + storeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};

var countItemInStockByUnitName = function (itemCode, unitName, storeId) {
    return $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/CountItemInStockByUnitName",
        data: "{itemCode:'" + itemCode + "', unitId:'" + unitId + "', storeId:'" + storeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};



//Ajax Requests
var getPrice = function () {
    var itemCode = itemCodeHidden.val();
    var partyCode = partyCodeHidden.val();
    var priceTypeId = parseFloat2(priceTypeDropDownList.val());
    var unitId = unitIdHidden.val();

    if (!unitId) return;
    if (priceTypeId <= 0) {
        $.notify(invalidPriceTypeWarningLocalized, "error");
        priceTypeDropDownList.focus();
        return;
    };


    $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/GetPrice",
        data: "{tranBook:'" + tranBook + "', itemCode:'" + itemCode + "', partyCode:'" + partyCode + "', priceTypeId:'" + priceTypeId + "', unitId:'" + unitId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            priceTextBox.val(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            logError(err, "error");
        }
    });

    $.ajax({
        type: "POST",
        url: "/Services/ItemData.asmx/GetTaxRate",
        data: "{itemCode:'" + itemCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            taxRateTextBox.val(msg.d);
        },
        error: function (xhr) {
            var err = xhr.responseText;
            logError(err, "error");
        }
    });



    calculateAmount();
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
function addShortCuts() {
    shortcut.add("F2", function () {
        var url = "/Inventory/Setup/PartiesPopup.aspx?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=PartyIdHidden";
        showWindow(url);
    });

    shortcut.add("F4", function () {
        var url = "/Inventory/Setup/ItemsPopup.aspx?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=ItemIdHidden";
        //var url = "test.html";
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

