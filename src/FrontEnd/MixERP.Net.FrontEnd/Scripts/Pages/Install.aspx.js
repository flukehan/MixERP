var isValid = true;
var officeNameInputText = $("#OfficeNameInputText");
var officeCodeInputText = $("#OfficeCodeInputText");
var nickNameInputText = $("#NickNameInputText");
var registrationDateInputText = $("#RegistrationDateInputText");
var currencyCodeInputText = $("#CurrencyCodeInputText");
var currencySymbolInputText = $("#CurrencySymbolInputText");
var currencyNameInputText = $("#CurrencyNameInputText");
var hundredthNameInputText = $("#HundredthNameInputText");
var fiscalYearCodeInputText = $("#FiscalYearCodeInputText");
var fiscalYearNameInputText = $("#FiscalYearNameInputText");
var startsFromInputText = $("#StartsFromInputText");
var endsOnInputText = $("#EndsOnInputText");
var adminNameInputText = $("#AdminNameInputText");
var usernameInputText = $("#UsernameInputText");
var passwordInputPassword = $("#PasswordInputPassword");
var confirmPasswordInputPassword = $("#ConfirmPasswordInputPassword");
var saveButton = $("#SaveButton");

$(document).ready(function() {
    var year = new Date().getYear() + 1900;

    fiscalYearCodeInputText.val("FY-" + year.toString().slice(-2));
    fiscalYearNameInputText.val("FY-" + year);
});

var validateFields = function() {
    if (isNullOrWhiteSpace(officeNameInputText.val()) ||
        isNullOrWhiteSpace(officeCodeInputText.val()) ||
        isNullOrWhiteSpace(nickNameInputText.val()) ||
        isNullOrWhiteSpace(registrationDateInputText.val()) ||
        isNullOrWhiteSpace(currencyCodeInputText.val()) ||
        isNullOrWhiteSpace(currencySymbolInputText.val()) ||
        isNullOrWhiteSpace(currencyNameInputText.val()) ||
        isNullOrWhiteSpace(hundredthNameInputText.val()) ||
        isNullOrWhiteSpace(fiscalYearCodeInputText.val()) ||
        isNullOrWhiteSpace(fiscalYearNameInputText.val()) ||
        isNullOrWhiteSpace(startsFromInputText.val()) ||
        isNullOrWhiteSpace(endsOnInputText.val()) ||
        isNullOrWhiteSpace(adminNameInputText.val()) ||
        isNullOrWhiteSpace(usernameInputText.val()) ||
        isNullOrWhiteSpace(passwordInputPassword.val()) ||
        isNullOrWhiteSpace(confirmPasswordInputPassword.val())) {
        isValid = false;
        alert(allFieldsRequiredLocalized);
    } else {
        isValid = true;
    };

    if (isValid) {
        validatePassword();
    };
};

var validatePassword = function() {
    if (passwordInputPassword.val() !== confirmPasswordInputPassword.val()) {
        isValid = false;
        alert(confirmedPasswordDoesNotMatch);
    } else {
        isValid = true;
    };
};

saveButton.click(function() {

    validateFields();

    if (isValid) {
        $(".form").addClass("loading");
        $(".dimmer").dimmer("show");

        var ajaxSaveOffice = saveOffice(officeCodeInputText.val(), officeNameInputText.val(), nickNameInputText.val(), registrationDateInputText.val(), currencyCodeInputText.val(), currencySymbolInputText.val(), currencyNameInputText.val(), hundredthNameInputText.val(), fiscalYearCodeInputText.val(), fiscalYearNameInputText.val(), startsFromInputText.val(), endsOnInputText.val(), adminNameInputText.val(), usernameInputText.val(), passwordInputPassword.val(), confirmPasswordInputPassword.val());

        ajaxSaveOffice.success(function(msg) {
            if (msg.d) {
                window.location = "/SignIn.aspx";
            } else {
                alert(msg.d);
            };
        });

        ajaxSaveOffice.error(function(xhr) {
            $(".form").removeClass("loading");
            $(".dimmer").dimmer("hide");
            alert(xhr.responseText);
        });
    };
});

function saveOffice(officeCode, officeName, nickName, registrationDate, currencyCode, currencySymbol, currencyName, hundredthName, fiscalYearCode, fiscalYearName, startsFrom, endsOn, adminName, username, password, confirmPassword) {
    var url = "/Services/Install.asmx/SaveOffice";

    var data = appendParameter("", "officeCode", officeCode);
    data = appendParameter(data, "officeName", officeName);
    data = appendParameter(data, "nickName", nickName);
    data = appendParameter(data, "registrationDate", registrationDate);
    data = appendParameter(data, "currencyCode", currencyCode);
    data = appendParameter(data, "currencySymbol", currencySymbol);
    data = appendParameter(data, "currencyName", currencyName);
    data = appendParameter(data, "hundredthName", hundredthName);
    data = appendParameter(data, "fiscalYearCode", fiscalYearCode);
    data = appendParameter(data, "fiscalYearName", fiscalYearName);
    data = appendParameter(data, "startsFrom", startsFrom);
    data = appendParameter(data, "endsOn", endsOn);
    data = appendParameter(data, "adminName", adminName);
    data = appendParameter(data, "username", username);
    data = appendParameter(data, "password", password);
    data = appendParameter(data, "confirmPassword", confirmPassword);
    data = getData(data);

    return getAjax(url, data);
};