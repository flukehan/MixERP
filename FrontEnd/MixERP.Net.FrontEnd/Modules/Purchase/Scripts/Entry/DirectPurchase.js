saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxSaveDirectPurchase = saveDirectPurchase(valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, cashRepositoryId, costCenterId, attachments);

    ajaxSaveDirectPurchase.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Purchase/Confirmation/DirectPurchase.mix?TranId=" + id;
    });

    ajaxSaveDirectPurchase.fail(function (jqXHR) {
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveDirectPurchase = function (valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, cashRepositoryId, costCenterId, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "transactionType", transactionType);
    d = appendParameter(d, "cashRepositoryId", cashRepositoryId);
    d = appendParameter(d, "costCenterId", costCenterId);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);

    url = "/Modules/Purchase/Services/DirectPurchase.asmx/Save";
    return getAjax(url, d);
};