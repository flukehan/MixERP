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
/*global getAjax, getAjaxErrorMessage, logError, saveButton, url:true, validateProductControl, errorLabelBottom, appendParameter, getData, valueDate, storeId, partyCode, referenceNumber, data, statementReference, cashRepositoryId, costCenterId, attachments, transactionType*/

if (typeof (saveButton) === "undefined") {
    saveButton = $("#SaveButton");
};


saveButton.click(function () {
    if (validateProductControl()) {
        save();
    };
});

var save = function () {
    saveButton.addClass("loading");
    var ajaxSaveDirectPurchase = saveDirectPurchase(valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, costCenterId, attachments);

    ajaxSaveDirectPurchase.done(function (response) {
        var id = response.d;
        window.location = "/Modules/Purchase/Confirmation/DirectPurchase.mix?TranId=" + id;
    });

    ajaxSaveDirectPurchase.fail(function (xhr) {
        saveButton.removeClass("loading");
        var errorMessage = getAjaxErrorMessage(xhr);
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var saveDirectPurchase = function (valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, costCenterId, attachments) {
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "storeId", storeId);
    d = appendParameter(d, "partyCode", partyCode);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "statementReference", statementReference);
    d = appendParameter(d, "transactionType", transactionType);
    d = appendParameter(d, "costCenterId", costCenterId);
    d = appendParameter(d, "attachmentsJSON", attachments);

    d = getData(d);

    url = "/Modules/Purchase/Services/Entry/DirectPurchase.asmx/Save";
    return getAjax(url, d);
};