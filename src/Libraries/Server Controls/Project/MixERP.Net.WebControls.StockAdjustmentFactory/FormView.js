var addButton = $("#AddButton");
var dateTextBox = $("#DateTextBox");
var errorLabel = $("#ErrorLabel");
var itemIdHidden = $("#ItemIdHidden");
var itemCodeInputText = $("#ItemCodeInputText");
var itemSelect = $("#ItemSelect");
var quantityInputText = $("#QuantityInputText");
var referenceNumberInputText = $("#ReferenceNumberInputText");
var saveButton = $("#SaveButton");
var statementReferenceTextArea = $("#StatementReferenceTextArea");
var storeSelect = $("#StoreSelect");
var transactionTypeSelect = $("#TransactionTypeSelect");
var transferGridView = $("#TransferGridView");
var unitSelect = $("#UnitSelect");
var valueDateTextBox = $("#ValueDateTextBox");
var shippingCompanySelect = $("#ShippingCompanySelect");
var sourceStoreSelect = $("#SourceStoreSelect");

var url = "";
var data = "";

$(document).ready(function () {
    loadStores();
    loadItems();
    loadShippers();
});

addButton.click(function () {
    addRow();
});

var addRow = function () {
    itemCodeInputText.val(itemSelect.getSelectedValue());

    var tranType = transactionTypeSelect.getSelectedValue();
    var storeName = storeSelect.getSelectedText();
    var itemCode = itemCodeInputText.val();
    var itemName = itemSelect.getSelectedText();
    var unitName = unitSelect.getSelectedText();
    var quantity = parseInt(quantityInputText.val() || 0);

    if (transactionTypeSelect.length) {
        if (isNullOrWhiteSpace(tranType) || tranType === Resources.Titles.Select()) {
            makeDirty(transactionTypeSelect);
            return;
        };
    };

    if (isNullOrWhiteSpace(storeName) || storeName === Resources.Titles.Select()) {
        makeDirty(storeSelect);
        return;
    };

    if (isNullOrWhiteSpace(itemCode)) {
        makeDirty(itemCodeInputText);
        return;
    };

    if (isNullOrWhiteSpace(itemName) || itemName === Resources.Titles.Select()) {
        makeDirty(itemSelect);
        return;
    };

    if (isNullOrWhiteSpace(unitName) || unitName === Resources.Titles.Select()) {
        makeDirty(unitSelect);
        return;
    };

    if (quantity <= 0) {
        makeDirty(quantityInputText);
        return;
    };

    removeDirty(transactionTypeSelect);
    removeDirty(storeSelect);
    removeDirty(itemCodeInputText);
    removeDirty(itemSelect);
    removeDirty(unitSelect);
    removeDirty(quantityInputText);

    appendToTable(tranType, storeName, itemCode, itemName, unitName, quantity);
    itemCodeInputText.val("");
    quantityInputText.val("");

    if (transactionTypeSelect.length) {
        transactionTypeSelect.focus();
        return;
    };

    storeSelect.attr("disabled", "disabled");
    itemCodeInputText.focus();
};

function appendToTable(tranType, storeName, itemCode, itemName, unitName, quantity) {
    var rows = transferGridView.find("tbody tr:not(:last-child)");
    var match = false;
    var html;

    if (transactionTypeSelect.length) {
        rows.each(function () {
            var row = $(this);
            if (getColumnText(row, 0) !== tranType &&
                getColumnText(row, 1) === storeName &&
                getColumnText(row, 2) === itemCode) {
                $.notify(Resources.Warnings.DuplicateEntry());

                makeDirty(itemSelect);
                match = true;
            };

            if (getColumnText(row, 0) === tranType &&
                getColumnText(row, 1) === storeName &&
                getColumnText(row, 2) === itemCode &&
                getColumnText(row, 4) === unitName) {
                setColumnText(row, 5, getFormattedNumber(parseFloat2(getColumnText(row, 5)) + quantity));

                addDanger(row);
                match = true;
                return;
            }
        });

        if (!match) {
            html = "<tr class='grid2-row'><td>" + tranType + "</td><td>" + storeName + "</td><td>" + itemCode + "</td><td>" + itemName + "</td><td>" + unitName + "</td><td class='text-right'>" + getFormattedNumber(quantity) + "</td>"
                + "</td><td><a class='pointer' onclick='removeRow($(this));'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td></tr>";
            transferGridView.find("tr:last").before(html);
        };

        return;
    };

    rows.each(function () {
        var row = $(this);
        if (getColumnText(row, 0) === storeName &&
            getColumnText(row, 1) === itemCode) {
            $.notify(Resources.Warnings.DuplicateEntry());

            makeDirty(itemSelect);
            match = true;
        };

        if (getColumnText(row, 0) === storeName &&
            getColumnText(row, 1) === itemCode &&
            getColumnText(row, 3) === unitName) {
            setColumnText(row, 4, getFormattedNumber(parseFloat2(getColumnText(row, 4)) + quantity));

            addDanger(row);
            match = true;
            return;
        }
    });

    if (!match) {
        html = "<tr class='grid2-row'><td>" + storeName + "</td><td>" + itemCode + "</td><td>" + itemName + "</td><td>" + unitName + "</td><td class='text-right'>" + getFormattedNumber(quantity) + "</td>"
            + "</td><td><a class='pointer' onclick='removeRow($(this));'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td></tr>";
        transferGridView.find("tr:last").before(html);
    };
};

saveButton.click(function () {
    errorLabel.html("");

    if (transferGridView.find("tr").length === 2) {
        errorLabel.html(Resources.Warnings.GridViewEmpty());
        return false;
    };

    var tableData = tableToJSON(transferGridView);

    //Ignore resharper warning on this
    if (validateSides == true) {
        if (Validate(tableData)) {
            Callback();
            return true;
        };
        return false;
    };

    Callback();
    return true;
});

function Callback() {
    if (typeof StockAdjustmentFactory_FormvView_SaveButton_Callback === "function") {
        StockAdjustmentFactory_FormvView_SaveButton_Callback();
    };
};

function Validate(tableData) {
    var table = JSON.parse(tableData);

    var model = function (itemName, unitName, dr, cr) {
        this.item = itemName;
        this.unit = unitName;
        this.debit = dr;
        this.credit = cr;
    };

    var models = [];
    var debit;
    var credit;
    var i;

    for (i = 0; i < table.length; i++) {
        debit = parseInt2(GetDebit(table, i));
        credit = parseInt2(GetCredit(table, i));

        var item = new model(table[i][2], table[i][4], debit, credit);
        var index = ItemIndex(models, item);

        if (index === -1) {
            models.push(item);
        }
        else {
            models[index].debit += debit;
            models[index].credit += credit;
        };
    };

    for (i = 0; i < models.length; i++) {
        if (models[i]["debit"] !== models[i]["credit"]) {
            $.notify(Resources.Errors.ReferencingSidesNotEqual());
            return false;
        };
    };

    return true;
};

function ItemIndex(models, item) {
    for (j = 0; j < models.length; j++) {
        if ((models[j]["item"] === item["item"])) {
            return j;
        };
    };
    return -1;
};

function GetDebit(table, index) {
    return table[index][0] === "Dr" ? table[index][5] : 0;
};

function GetCredit(table, index) {
    return table[index][0] === "Cr" ? table[index][5] : 0;
};

itemSelect.change(function () {
    itemCodeInputText.val(itemSelect.getSelectedValue());
    loadUnits();
});

itemSelect.blur(function () {
    itemCodeInputText.val(itemSelect.getSelectedValue());
    loadUnits();
});

itemCodeInputText.blur(function () {
    if (!isNullOrWhiteSpace(itemCodeInputText.val())) {
        itemSelect.val(itemCodeInputText.val());
    };
});

//Ajax Data-binding
function loadStores() {
    if (storeSelect.length) {
        url = storeServiceUrl;
        ajaxDataBind(url, storeSelect);
    };

    if (sourceStoreSelect.length) {
        url = storeServiceUrl;
        ajaxDataBind(url, sourceStoreSelect);
    };
};

//Ajax Data-binding
function loadShippers() {
    if (shippingCompanySelect.length) {
        url = shippingCompanyServiceUrl;
        ajaxDataBind(url, shippingCompanySelect);
    };
};

function loadItems() {
   
    url = itemServiceUrl;
    data = appendParameter("", "tranBook", "StockAdjustment");
    data = getData(data);

    ajaxDataBind(url, itemSelect, data, null, itemCodeInputText, null, "ItemCode", "ItemName");
};

function loadUnits() {
    var itemCode = itemCodeInputText.val();

    url = unitServiceUrl;
    data = appendParameter("", "itemCode", itemCode);
    data = getData(data);

    addLoader(transferGridView);
    ajaxDataBind(url, unitSelect, data, null, null, removeLoaderInstance);
};

function removeLoaderInstance() {
    removeLoader(transferGridView);
};

//Check if ItemPopup window has updated the hidden field.
function ajaxDataBindCallBack(targetControl) {
    if (targetControl.is(itemSelect)) {
        var itemId = parseInt(itemIdHidden.val() || 0);

        itemIdHidden.val("");

        if (itemId > 0) {
            var targetControls = $([]);
            targetControls.push(itemCodeInputText);
            targetControls.push(itemSelect);

            url = itemIdQuerySericeUrl;
            data = appendParameter("", "itemId", itemId);
            data = getData(data);

            ajaxUpdateVal(url, data, targetControls);
        }
    };
};

shortcut.add("F4", function () {
    url = itemPopupUrl;
    showWindow(url);
});

shortcut.add("CTRL+ENTER", function () {
    addButton.trigger('click');
});