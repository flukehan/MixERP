saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxSaveQuotation = saveQuotation(valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, cashRepositoryId, costCenterId, agentId, statementReference, transactionIds, attachments);

    ajaxSaveQuotation.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Sales/Quotation.mix?TranId=" + id;
    });

    ajaxSaveQuotation.fail(function (jqXHR) {
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveQuotation = function (valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, cashRepositoryId, costCenterId, agentId, statementReference, transactionIds, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "priceTypeId", priceTypeId);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "transactionIds", transactionIds);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);

    url = "/Modules/Sales/Services/Entry/Quotation.asmx/Save";
    return getAjax(url, d);
};