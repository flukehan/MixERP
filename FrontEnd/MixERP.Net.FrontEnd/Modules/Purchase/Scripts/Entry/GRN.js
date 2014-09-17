saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxSaveGRN = saveGRN(valueDate, storeId, partyCode, referenceNumber, data, statementReference, costCenterId, transactionIds, attachments);

    ajaxSaveGRN.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Purchase/Confirmation/GRN.mix?TranId=" + id;
    });

    ajaxSaveGRN.fail(function (jqXHR) {
        var errorMessage = getAjaxErrorMessage(jqXHR);
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveGRN = function (valueDate, storeId, partyCode, referenceNumber, data, statementReference, costCenterId, transactionIds, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "costCenterId", costCenterId);
    d = appendParameter(d, "transactionIds", transactionIds);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);
    url = "/Modules/Purchase/Services/GRN.asmx/Save";

    return getAjax(url, d);
};