saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    var ajaxSalesDelivery = saveSalesDelivery(valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments);

    ajaxSalesDelivery.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Sales/Confirmation/Delivery.mix?TranId=" + id;
    });

    ajaxSalesDelivery.fail(function (jqXHR) {
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveSalesDelivery = function (valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments) {
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

    url = "/Modules/Sales/Services/Entry/Delivery.asmx/Save";

    return getAjax(url, d);
};