/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

/*jshint -W032*/
/*global getAjax, getAjaxErrorMessage, getParameterByName, logError, saveButton, url:true, validateProductControl, errorLabelBottom, appendParameter, getData, valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, agentId, shipperId, shippingAddressCode, shippingCharge, costCenterId, transactionIds, attachments*/

saveButton.click(function() {
    if (validateProductControl()) {
        save();
    };
});

var save = function() {
    saveButton.addClass("loading");
    var tranId = getParameterByName("TranId");
    var ajaxSaveOder = saveOrder(tranId, valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, costCenterId, salespersonId, statementReference, transactionIds, attachments);

    ajaxSaveOder.done(function(response) {
        var id = response.d;
        window.location = "/Modules/Sales/Confirmation/Return.mix?TranId=" + id;
    });

    ajaxSaveOder.fail(function(jqXHR) {
        saveButton.removeClass("loading");
        var errorMessage = getAjaxErrorMessage(jqXHR);
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveOrder = function(tranId, valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, costCenterId, salespersonId, statementReference, transactionIds, attachments) {
    var d = "";
    d = appendParameter(d, "tranId", tranId);
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "priceTypeId", priceTypeId);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);
    url = "/Modules/Sales/Services/Entry/Return.asmx/Save";

    return getAjax(url, d);
};