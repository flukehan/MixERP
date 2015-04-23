<%-- 
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
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="MixERP.Net.FrontEnd.Install" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Scripts/semantic-ui/semantic.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/semantic-ui/semantic.min.js"></script>
    <script src="bundles/scripts/libraries.js"></script>
    <style type="text/css">
        body {
            background-color: #FFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ui page grid">
            <div class="column">
                <img src="Resource/Static/images/mixerp-logo-light.png" />
                <div class="ui header">
                    <div class="content">
                        Install MixERP
                    </div>
                </div>

                <div class="ui small form segment" style="width: 700px;">
                    <div class="ui header">
                        <div class="content">
                            About Your Office
                        </div>
                    </div>
                    <div class="ui divider"></div>
                    <div class="field">
                        <label for="OfficeNameInputText">Office Name</label>
                        <input type="text" maxlength="150" id="OfficeNameInputText" />
                    </div>
                    <div class="three fields">
                        <div class="field">
                            <label for="OfficeCodeInputText">Office Code</label>
                            <input type="text" maxlength="12" id="OfficeCodeInputText" />
                        </div>
                        <div class="field">
                            <label for="NickNameInputText">Office Nick Name</label>
                            <input type="text" maxlength="50" id="NickNameInputText" />
                        </div>
                        <div class="field">
                            <label for="RegistrationDateInputText">Registration Date</label>
                            <input type="text" maxlength="50" id="RegistrationDateInputText" />
                        </div>
                    </div>

                    <div class="ui header">
                        <div class="content">
                            What Is Your Home Currency?
                        </div>
                    </div>
                    <div class="ui divider"></div>
                    <div class="four fields">
                        <div class="field">
                            <label for="CurrencyCodeInputText">Currency Code</label>
                            <input type="text" maxlength="12" id="CurrencyCodeInputText" />
                        </div>
                        <div class="field">
                            <label for="CurrencySymbolInputText">Currency Symbol</label>
                            <input type="text" maxlength="48" id="CurrencySymbolInputText" />
                        </div>
                        <div class="field">
                            <label for="CurrencyNameInputText">Currency Name</label>
                            <input type="text" maxlength="48" id="CurrencyNameInputText" />
                        </div>
                        <div class="field">
                            <label for="HundredthNameInputText">Hundredth Name</label>
                            <input type="text" maxlength="48" id="HundredthNameInputText" />
                        </div>
                    </div>

                    <div class="ui header">
                        <div class="content">
                            Create a User Account for Yourself
                        </div>
                    </div>
                    <div class="ui divider"></div>
                    <div class="two fields">
                        <div class="field">
                            <label for="AdminNameInputText">Your Name</label>
                            <input type="text" maxlength="100" id="AdminNameInputText" />
                        </div>
                        <div class="field">
                            <label for="UserNameInputText">User Name</label>
                            <input type="text" maxlength="50" id="UserNameInputText" />
                        </div>
                    </div>
                    <div class="two fields">
                        <div class="field">
                            <label for="PasswordInputPassword">Password</label>
                            <input type="text" maxlength="50" id="PasswordInputPassword" />
                        </div>
                        <div class="field">
                            <label for="ConfirmPasswordInputPassword">Confirm Password</label>
                            <input type="text" maxlength="50" id="ConfirmPasswordInputPassword" />
                        </div>
                    </div>

                    <button type="button" class="ui small red button" id="SaveButton">Save</button>
                </div>
                <div class="ui page dimmer">
                    <div class="content">
                        <div class="center">
                            <div class="ui yellow huge icon header">
                                <i class="ui inverted yellow setting loading icon"></i>Saving<div class="ui yellow sub header">Just a moment, please!</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    var isValid = true;
    var officeName = $('#OfficeNameInputText');
    var officeCode = $('#OfficeCodeInputText');
    var nickName = $('#NickNameInputText');
    var registrationDate = $('#RegistrationDateInputText');
    var currencyCode = $('#CurrencyCodeInputText');
    var currencySymbol = $('#CurrencySymbolInputText');
    var currencyName = $('#CurrencyNameInputText');
    var hundredthName = $('#HundredthNameInputText');
    var adminName = $('#AdminNameInputText');
    var userName = $('#UserNameInputText');
    var password = $('#PasswordInputPassword');
    var confirmPassword = $('#ConfirmPasswordInputPassword');
    var saveButton = $('#SaveButton');

    var validateFields = function () {
        if (isNullOrWhiteSpace(officeName.val()) || isNullOrWhiteSpace(officeCode.val()) || isNullOrWhiteSpace(nickName.val()) || isNullOrWhiteSpace(registrationDate.val()) || isNullOrWhiteSpace(currencyCode.val()) || isNullOrWhiteSpace(currencySymbol.val()) || isNullOrWhiteSpace(currencyName.val()) || isNullOrWhiteSpace(hundredthName.val()) || isNullOrWhiteSpace(adminName.val()) || isNullOrWhiteSpace(userName.val()) || isNullOrWhiteSpace(password.val()) || isNullOrWhiteSpace(confirmPassword.val())) {
            isValid = false;
            alert('Please input all fields');
        } else {
            isValid = true;
        }
        if (isValid) {
            validatePassword();
        }
    };

    var validatePassword = function () {
        if (password.val() !== confirmPassword.val()) {
            isValid = false;
            alert('Password not equal.');
        } else {
            isValid = true;
        }
    };

    saveButton.click(function () {

        validateFields();

        if (isValid) {
            $(".dimmer").dimmer('show');
            var url = "/Services/Install.asmx/SaveOffice";
            data = appendParameter("", "officeCode", officeCode.val());
            data = appendParameter(data, "officeName", officeName.val());
            data = appendParameter(data, "nickName", nickName.val());
            data = appendParameter(data, "registrationDate", registrationDate.val());
            data = appendParameter(data, "currencyCode", currencyCode.val());
            data = appendParameter(data, "currencySymbol", currencySymbol.val());
            data = appendParameter(data, "currencyName", currencyName.val());
            data = appendParameter(data, "hundredthName", hundredthName.val());
            data = appendParameter(data, "adminName", adminName.val());
            data = appendParameter(data, "userName", userName.val());
            data = appendParameter(data, "password", password.val());
            data = appendParameter(data, "confirmPassword", confirmPassword.val());
            data = getData(data);
            var saveOffice = getAjax(url, data);

            saveOffice.success(function (msg) {
                if (msg.d) {
                    window.location = "/SignIn.aspx";
                } else {
                    window.location = "/Install.aspx";
                }
            });

            saveOffice.error(function (xhr) {
                $(".dimmer").dimmer('hide');
                alert(xhr.responseText);
            });
        }
    });

</script>
</html>
