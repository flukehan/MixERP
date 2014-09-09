var accruedInterestSpan = $("#AccruedInterestSpan");
var addressDiv = $("#AddressDiv");

var creditAllowedSpan = $("#CreditAllowedSpan");
var cSTNumberSpan = $("#CSTNumberSpan");

var defaultCurrencySpan = $("#DefaultCurrencySpan");

var emailAddressSpan = $("#EmailAddressSpan");

var gLHeadSpan = $("#GLHeadSpan");
var goButton = $("#GoButton");

var interestApplicableSpan = $("#InterestApplicableSpan");

var lastPaymentDateSpan = $("#LastPaymentDateSpan");

var maxCreditAmountSpan = $("#MaxCreditAmountSpan");
var maxCreditPeriodSpan = $("#MaxCreditPeriodSpan");

var officeDueAmountSpan = $("#OfficeDueAmountSpan");

var pANNumberSpan = $("#PANNumberSpan");
var partyCodeTextBox = $("#PartyCodeTextBox");
var partyDropDownList = $("#PartyDropDownList");
var partyTypeSpan = $("#PartyTypeSpan");

var shippingAddressesDiv = $("#ShippingAddressesDiv");
var sSTNumberSpan = $("#SSTNumberSpan");

var totalDueAmountSpan = $("#TotalDueAmountSpan");
var transactionValueSpan = $("#TransactionValueSpan");

var url = "";
var data = "";

//Variables
var partyCode = "";

//Page Events
$(document).ready(function () {
    loadParties();
});

//Control Events
goButton.click(function () {
    partyCode = partyDropDownList.getSelectedValue();
    if (isNullOrWhiteSpace(partyCode)) {
        $.notify(nothingSelectedLocalized, "error");
        return;
    };

    var ajaxGetPartyDue = getPartyDueModel(partyCode);

    ajaxGetPartyDue.success(function (msg) {
        var model = msg.d;
        var totalDue = convertToDebit(parseFloat2(model.TotalDueAmount));
        var officeDue = convertToDebit(parseFloat2(model.OfficeDueAmount));

        defaultCurrencySpan.html(model.CurrencyCode);

        totalDueAmountSpan.html(getFormattedCurrency(model.CurrencySymbol, totalDue));
        officeDueAmountSpan.html(getFormattedCurrency(model.CurrencySymbol, officeDue));
        accruedInterestSpan.html(getFormattedCurrency(model.CurrencySymbol, model.AccruedInterest));
        lastPaymentDateSpan.html(model.LastPaymentDate);
        transactionValueSpan.html(getFormattedCurrency(model.CurrencySymbol, model.TransactionValue));
    });

    ajaxGetPartyDue.fail(function (xhr) {
        alert(getAjaxErrorMessage(xhr));
    });

    var ajaxGetPartyView = getPartyView(partyCode);
    var partyCurrencySymbol = "रू.";//Todo

    ajaxGetPartyView.success(function (msg) {
        var partyView = msg.d;
        var interestDetails = [""];

        partyTypeSpan.html(partyView.PartyType);
        emailAddressSpan.html(String.format("<a href='mailto:{0}'>{0}</a>", partyView.Email));
        pANNumberSpan.html(partyView.PANNumber);
        sSTNumberSpan.html(partyView.SSTNumber);
        cSTNumberSpan.html(partyView.CSTNumber);
        creditAllowedSpan.html(localizeBool(partyView.AllowCredit));
        maxCreditPeriodSpan.html(partyView.MaximumCreditPeriod + "&nbsp;" + daysLowerCaseLocalized);
        maxCreditAmountSpan.html(getFormattedCurrency(partyCurrencySymbol, partyView.MaximumCreditAmount));

        interestDetails.push(localizeBool(partyView.ChargeInterest));

        if (partyView.ChargeInterest && partyView.InterestRate) {
            interestDetails.push("&nbsp", "(", partyView.InterestRate, "%");

            if (partyView.InterestCompoundingFrequency) {
                interestDetails.push(",", "&nbsp;", partyView.InterestCompoundingFrequency);
            };

            interestDetails.push(")");
        };

        interestApplicableSpan.html(interestDetails.join(""));

        gLHeadSpan.html(partyView.GLHead);
        addressDiv.html(getAddress(partyView.POBox, partyView.AddressLine1, partyView.AddressLine2, partyView.Street, partyView.City, partyView.State, partyView.Country, partyView.Phone, partyView.Cell, partyView.Fax, partyView.Url));
    });

    ajaxGetPartyView.fail(function (xhr) {
        alert(getAjaxErrorMessage(xhr));
    });

    var ajaxGetShippingAddresses = getShippingAddresses(partyCode);

    ajaxGetShippingAddresses.success(function (msg) {
        var shippingAddresses = [""];

        for (i = 0; i < msg.d.length; i++) {
            var address = msg.d[i];
            shippingAddresses.push(getAddress(address.POBox, address.AddressLine1, address.AddressLine2, address.Street, address.City, address.State, address.Country));
        };

        shippingAddressesDiv.html(shippingAddresses.join(""));
    });

    ajaxGetShippingAddresses.fail(function (xhr) {
        alert(getAjaxErrorMessage(xhr));
    });
});

function getAddress(poBox, line1, line2, street, city, state, country, phone, cell, fax, url) {
    var html = [""];
    html.push("<address>");

    if (poBox) {
        html.push(poBox, "<br />");
    };

    if (line1) {
        html.push(line1, "<br />");
    };

    if (line2) {
        html.push(line2, "<br />");
    };

    html.push(street, "&nbsp;", city, "<br />");
    html.push(state, "&nbsp;", country, "<br />");

    if (phone) {
        html.push("✆ ", phone);

        if (cell) {
            html.push(", ", cell);
        };

        html.push("<br />");
    } else {
        if (cell) {
            html.push("✆ ", cell, "<br />");
        };
    };

    if (fax) {
        html.push("📠 ", fax, "<br />");
    };

    if (url) {
        html.push("⊕ <a target='_blank' href='", url, "'>", url, "</a>");
    };

    html.push("</address>");

    return html.join("");
};

function getFormattedCurrency(symbol, value) {
    var money;

    if (value < 0) {
        money = symbol + " " + $.number(value * -1, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
        money = "<div class='error-message'><strong>(" + money + ")</strong></div>";
    } else {
        money = symbol + " " + $.number(value, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
    }

    return money;
};

function localizeBool(val) {
    if (val) {
        return yesLocalized;
    };

    return noLocalized;
};

function getPartyView(partyCode) {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetPartyView";//Todo--Parametrize these.
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    return getAjax(url, data);
};

function getPartyDueModel(partyCode) {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetPartyDue";
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    return getAjax(url, data);
};

function getShippingAddresses(partyCode) {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetShippingAddresses";
    data = appendParameter("", "partyCode", partyCode);
    data = getData(data);

    return getAjax(url, data);
};

partyDropDownList.change(function () {
    partyCodeTextBox.val(partyDropDownList.getSelectedValue());
});

partyCodeTextBox.blur(function () {
    partyDropDownList.val(partyCodeTextBox.val());
    if (partyDropDownList.val() != partyCodeTextBox.val()) {
        partyCodeTextBox.val("");
    };
});

//Ajax Data Binding
function loadParties() {
    url = "/Modules/Inventory/Services/PartyData.asmx/GetParties";
    var partyAjax = getAjax(url);

    partyAjax.success(function (msg) {
        partyDropDownList.bindAjaxData(msg.d);
    });

    partyAjax.error(function () {
        var err = $.parseJSON(xhr.responseText);
        appendItem(partyDropDownList, 0, err.Message);
    });
};