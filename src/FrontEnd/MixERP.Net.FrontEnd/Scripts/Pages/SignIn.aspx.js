var usernameInputText = $("#UsernameInputText");
var passwordInputPassword = $("#PasswordInputPassword");
var rememberInputCheckBox = $("#RememberInputCheckBox");
var branchSelect = $("#BranchSelect");
var languageSelect = $("#LanguageSelect");
var signInButton = $("#SignInButton");
var companySelect = $("#CompanySelect");

$(document).ready(function () {
    usernameInputText.val('binod');
    passwordInputPassword.val('binod');

    var languageSelect = $("#LanguageSelect");

    var userLang = navigator.language || navigator.userLanguage;

    if (languageSelect.find('option[value=' + userLang + ']').length) {
        languageSelect.val(userLang);
    };

    $(".ui.checkbox").checkbox();

    var message = getQueryStringByName("Message");

    if (message) {
        $(".exception").append(message).addClass("big error");
        makeDirty();
    };
});

signInButton.click(function() {
    $(".dimmer").dimmer('show');

    var catalog = companySelect.getSelectedValue();
    var username = usernameInputText.val();
    var rememberMe = rememberInputCheckBox.is(":checked");
    var branchId = parseInt2(branchSelect.getSelectedValue());
    var language = languageSelect.getSelectedValue();
    var password = getPassword(username, passwordInputPassword.val(), challenge);
    
    var ajaxAuthenticate = authenticate(catalog, username, password, rememberMe, language, branchId);

    $(".form").addClass("loading");

    ajaxAuthenticate.success(function (msg) {
        if (msg.d === "OK") {
            window.location = getRedirectUrl();
            return;
        };

        window.location = window.location.href.split('?')[0] + "?Message=" + msg.d;
    });

    ajaxAuthenticate.fail(function(xhr) {
        logAjaxErrorMessage(xhr);
        makeDirty();
        $(".dimmer").dimmer('hide');
    });
});

companySelect.change(function() {
    $(".form").addClass("loading");

    var catalog = $(this).getSelectedValue();

    var ajaxGetOffices = getOffices(catalog);

    ajaxGetOffices.success(function (msg) {
        bindBranchSelect(msg.d);
        $(".form").removeClass("loading");
    });

    ajaxGetOffices.fail(function(xhr) {
        $(".form").removeClass("loading");
        window.location = window.location.href.split('?')[0] + "?Message=" + JSON.parse(xhr.responseText).Message;
    });
});

function getRedirectUrl() {
    var url = getQueryStringByName("ReturnUrl");

    if (isNullOrWhiteSpace(url)) {
        url = "/";
    };


    return url;
};

function bindBranchSelect(data) {
    branchSelect.html("");

    var list = "";

    $.each(data, function (index, item) {
        list += "<option value='" + item.OfficeId + "'>" + item.OfficeName + "</option>";
    });

    branchSelect.html(list);
};


function makeDirty() {
    $(".field").addClass("error");
};

function getOffices(catalog) {
    var url = "/Services/Office.asmx/GetOffices";
    var data = appendParameter("", "catalog", catalog);

    data = getData(data);

    return getAjax(url, data);
};

function authenticate(catalog, username, password, rememberMe, language, branchId) {
    var url = "/Services/User.asmx/Authenticate";
    var data = appendParameter("", "catalog", catalog);
    data = appendParameter(data, "username", username);
    data = appendParameter(data, "password", password);
    data = appendParameter(data, "rememberMe", rememberMe);
    data = appendParameter(data, "language", language);
    data = appendParameter(data, "branchId", branchId);

    data = getData(data);

    return getAjax(url, data);
};

function getPassword(username, password, token) {
    var hex = new jsSHA(username + password, 'TEXT').getHash('SHA-512', 'HEX');
    hex = new jsSHA(hex + token, 'TEXT').getHash('SHA-512', 'HEX');

    return hex;
};


$(document).keyup(function (e) {
    if (e.ctrlKey && e.which === 13) {
        if (!$(".form").hasClass("loading")) {
            signInButton.trigger("click");
        };
    };
});
