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
                    <div class="three fields">
                        <div class="field">
                            <label for="CurrencyCodeInputText">Currency Code</label>
                            <input type="text" maxlength="12" id="CurrencyCodeInputText" />
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

                    <button type="button" class="ui small red button">Save</button>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
