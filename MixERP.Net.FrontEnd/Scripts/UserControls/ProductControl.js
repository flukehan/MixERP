//Controls
var addButton = $("#AddButton");
var amountTextBox = $("#AmountTextBox");

var cashRepositoryDropDownList = $("#CashRepositoryDropDownList");
var cashRepositoryBalanceTextBox = $("#CashRepositoryBalanceTextBox");

var discountTextBox = $("#DiscountTextBox");

var errorLabel = $("#ErrorLabel");

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

var quantityTextBox = $("#QuantityTextBox");

var runningTotalTextBox = $("#RunningTotalTextBox");

var saveButton = $("#SaveButton");
var shippingAddressCodeHidden = $("#ShippingAddressCodeHidden");
var shippingAddressDropDownList = $("#ShippingAddressDropDownList");
var shippingAddressTextBox = $("#ShippingAddressTextBox");
var shippingChargeTextBox = $("#ShippingChargeTextBox");
var storeDropDownList = $("#StoreDropDownList");
var subTotalTextBox = $("#SubtotalTextBox");

var taxRateTextBox = $("#TaxRateTextBox");
var taxTotalTextBox = $("#TaxTotalTextBox");
var taxTextBox = $("#TaxTextBox");
var totalTextBox = $("#TotalTextBox");

var unitIdHidden = $("#UnitIdHidden");
var unitDropDownList = $("#UnitDropDownList");
var unitNameHidden = $("#UnitNameHidden");





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
                var err = eval("(" + xhr.responseText + ")");
                $.notify(err, "error");
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
                var err = eval("(" + xhr.responseText + ")");
                $.notify(err, "error");
            }
        });
    }
};




//Control Events

addButton.click(function() {
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
        data: "{cashRepositoryId:'" + cashRepositoryDropDownList.find("option:selected").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            cashRepositoryBalanceTextBox.val(msg.d);
        },
        error: function (xhr) {
            var err = eval("(" + xhr.responseText + ")");
            $.notify(err, "error");
        }
    });

});

discountTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

itemCodeTextBox.blur(function() {
    selectDropDownListByValue(this.id, 'ItemDropDownList');
});

itemDropDownList.blur(function() {
    getPrice();
});

itemDropDownList.change(function() {
    itemCodeTextBox.val(itemDropDownList.find("option:selected").val());
    itemCodeHidden.val(itemDropDownList.find("option:selected").val());
});

partyCodeTextBox.blur(function() {
    selectDropDownListByValue(this.id, 'PartyDropDownList');
});

partyDropDownList.change(function()
{
    partyCodeTextBox.val(partyDropDownList.find("option:selected").val());
    partyCodeHidden.val(partyDropDownList.find("option:selected").val());
    showShippingAddress();
});

priceTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

quantityTextBox.blur(function() {
    updateTax();
    calculateAmount();
});

saveButton.click(function () {
    updateData();
});

shippingAddressDropDownList.change(function() {
    showShippingAddress();
});

shippingChargeTextBox.blur(function()
{
    summate();
});

taxRateTextBox.blur(function () {
    updateTax();
    calculateAmount();
});

taxTextBox.blur(function () {
    calculateAmount();
});

unitDropDownList.change(function() {
    unitNameHidden.val($(this).children('option').filter(':selected').text());
    unitIdHidden.val($(this).children('option').filter(':selected').val());
});

unitDropDownList.blur(function () {
    getPrice();
});





//List Control Bindings

function addListItem(dropDownListId, value, text) {
    var dropDownList = $("#" + dropDownListId);
    dropDownList.append($("<option></option>").val(value).html(text));
};

function bindAddresses(data) {
    shippingAddressDropDownList.empty();

    if (data.length == 0) {
        addListItem("ShippingAddressDropDownList", "", noneLocalized);
        return;
    }

    addListItem("ShippingAddressDropDownList", "", selectLocalized);

    $.each(data, function () {
        addListItem("ShippingAddressDropDownList", this["Value"], this["Text"]);
    });

    shippingAddressDropDownList.val(shippingAddressCodeHidden.val());
};

function bindItems(data) {
    itemDropDownList.empty();

    if (data.length == 0) {
        addListItem("ItemDropDownList", "", noneLocalized);
        return;
    }

    addListItem("ItemDropDownList", "", selectLocalized);

    $.each(data, function () {
        addListItem("ItemDropDownList", this["Value"], this["Text"]);
    });

    itemCodeTextBox.val(itemCodeHidden.val());
    itemDropDownList.val(itemCodeHidden.val());
};

function bindParties(data) {
    partyDropDownList.empty();

    if (data.length == 0) {
        addListItem("PartyDropDownList", "", noneLocalized);
        return;
    }

    addListItem("PartyDropDownList", "", selectLocalized);

    $.each(data, function () {
        addListItem("PartyDropDownList", this["Value"], this["Text"]);
    });

    partyCodeTextBox.val(partyCodeHidden.val());
    partyDropDownList.val(partyCodeHidden.val());
};

function bindUnits(data) {
    unitDropDownList.empty();

    if (data.length == 0) {
        addListItem("UnitDropDownList", "", noneLocalized);
        return;
    }

    addListItem("UnitDropDownList", "", selectLocalized);

    $.each(data, function () {
        addListItem("UnitDropDownList", this["Value"], this["Text"]);
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
            var err = eval("(" + xhr.responseText + ")");
            addListItem("ShippingAddressDropDownList", 0, err.Message);
        }
    });
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
            var err = eval("(" + xhr.responseText + ")");
            addListItem("ItemDropDownList", 0, err.Message);
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
            var err = eval("(" + xhr.responseText + ")");
            addListItem("PartyDropDownList", 0, err.Message);
        }
    });
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
            var err = eval("(" + xhr.responseText + ")");
            addListItem("UnitDropDownList", 0, err.Message);
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
    var itemName = itemDropDownList.find("option:selected").text();
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
    var priceTypeId = priceTypeDropDownList.val();
    var unitId = unitIdHidden.val();


    if (!unitId) return;




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
            var err = eval("(" + xhr.responseText + ")");
            $.notify(err, "error");
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
            var err = eval("(" + xhr.responseText + ")");
            $.notify(err, "error");
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

