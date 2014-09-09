saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxsaveDirectSales = saveDirectSales(valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments);

    ajaxsaveDirectSales.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Sales/Confirmation/DirectSales.html?TranId=" + id;
    });

    ajaxsaveDirectSales.fail(function (jqXHR) {
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveDirectSales = function (valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "priceTypeId", priceTypeId);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "transactionType", transactionType);
    d = appendParameter(d, "agentId", agentId);
    d = appendParameter(d, "shipperId", shipperId);
    d = appendParameter(d, "shippingAddressCode", shippingAddressCode);
    d = appendParameter(d, "shippingCharge", shippingCharge);
    d = appendParameter(d, "cashRepositoryId", cashRepositoryId);
    d = appendParameter(d, "costCenterId", costCenterId);
    d = appendParameter(d, "transactionIds", transactionIds);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);

    url = "/Modules/Sales/Services/DirectSales.asmx/Save";
    return getAjax(url, d);
};