saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxSavePurchaseOrder = savePurchaseOrder(valueDate, partyCode, referenceNumber, data, statementReference, attachments);

    ajaxSavePurchaseOrder.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Purchase/Confirmation/Order.html?TranId=" + id;
    });

    ajaxSavePurchaseOrder.fail(function (jqXHR) {
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var savePurchaseOrder = function (valueDate, partyCode, referenceNumber, data, statementReference, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);

    url = "/Modules/Purchase/Services/Order.asmx/Save";
    return getAjax(url, d);
};