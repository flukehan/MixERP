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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="MixERP.Net.FrontEnd.SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/Scripts/semantic-ui/css/semantic.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/semantic-ui/javascript/semantic.min.js"></script>

    <title>Sign In</title>
</head>
<body id="SignInBody">
    <form id="form1" runat="server">
        <div id="signin-form">
            <div class="sign-in-logo">
                <a href="/SignIn.aspx">
                    <asp:Image runat="server" ImageUrl="~/Resource/Static/images/mixerp-logo.png" />
                </a>
            </div>

            <div class="ui form segment" style="padding: 24px 48px;">

                <div class="ui large header" style="padding: 8px 0;">
                    <asp:Literal ID="SignInLiteral" runat="server" Text="<%$Resources:Titles, SignIn %>" />
                </div>
                <div class="ui divider">
                </div>

                <div class="field">
                    <label for="UserIdTextBox">
                        <asp:Literal ID="UserIdLiteral" runat="server" Text="<%$Resources:Titles, UserId %>" />
                    </label>
                    <asp:TextBox ID="UserIdTextBox" runat="server" placeholder="<%$Resources:Titles, UserId %>" />
                </div>
                <div class="field">
                    <label for="PasswordTextBox">
                        <asp:Literal ID="PasswordLiteral" runat="server" Text="<%$Resources:Titles, Password %>" />
                    </label>

                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="<%$Resources:Titles, Password %>" />
                </div>

                <div class="field">
                    <div class="ui toggle checkbox">
                        <asp:CheckBox ID="RememberMe" runat="server" />
                        <label>
                            <asp:Literal runat="server" Text="<%$Resources:Titles, RememberMe %>" />
                        </label>
                    </div>
                </div>

                <div class="ui horizontal icon divider">
                    <i class="circular user icon"></i>
                </div>

                <div class="field">
                    <label for="BranchDropDownList">
                        <asp:Literal ID="SelectBranchLiteral" runat="server" Text="<%$Resources:Titles, SelectYourBranch  %>" />
                    </label>

                    <asp:HiddenField runat="server" ID="BranchIdHiddenField"></asp:HiddenField>
                    <asp:DropDownList ID="BranchDropDownList" runat="server"
                        DataTextField="OfficeName" DataValueField="OfficeId"
                        placeholder="<%$Resources:Titles, SelectYourBranch  %>">
                    </asp:DropDownList>
                </div>

                <div class="field">
                    <label for="LanguageDropDownList">Select Language</label>

                    <asp:DropDownList ID="LanguageDropDownList" runat="server">
                        <asp:ListItem Text="English (United States)" Value="en-US" />
                        <asp:ListItem Text="English (Great Britain)" Value="en-GB" />
                        <asp:ListItem Text="Français (France)" Value="fr-FR" />
                        <asp:ListItem Text="Deutsch (Deutschland)" Value="de-DE" />
                        <asp:ListItem Text="नेपाली (नेपाल)" Value="ne-NP" />
                        <asp:ListItem Text="हिन्दी (India)" Value="hi-IN" />
                    </asp:DropDownList>
                </div>

                <div class="field">
                    <asp:Literal ID="MessageLiteral" runat="server" />
                </div>

                <div class="field">
                    <asp:Button ID="SignInButton" runat="server" Text="<%$Resources:Titles, SignIn %>" OnClick="SignInButton_Click" OnClientClick="$('#BranchIdHiddenField').val($('#BranchDropDownList').val());" CssClass="ui teal button" />
                </div>

                <div class="field">
                    <label></label>

                    <asp:LinkButton ID="CannotAccessAccountLinkButton" runat="server" Text="<%$Resources:Questions, CannotAccessAccount %>" />
                </div>
            </div>
            <script type="text/javascript">
                $("#UserIdTextBox").val('binod');
                $("#PasswordTextBox").val('binod');
                $(".ui.checkbox").checkbox();

                $(document).ready(function () {
                    if ($(".big.error").html()) {
                        $(".field").addClass("error");
                    };
                });
            </script>
        </div>
    </form>
</body>
</html>