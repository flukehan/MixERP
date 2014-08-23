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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="MixERP.Net.FrontEnd.SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link href="/bundles/stylesheets/sign-in.min.css" rel="stylesheet" />    
    <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>    
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>

    <title>Sign In</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="signin-form" style="">

            <div style="margin-left: 24px;">
                <div class="sign-in-logo">
                    <a href="/SignIn.aspx">
                        <asp:Image runat="server" ImageUrl="~/Themes/purple/images/mixerp-logo.png" />
                    </a>
                </div>
            </div>

            <div class="panel container" style="width: 100%; margin: 0 24px;">
                <div class="sign-in" role="form">
                    <h1>
                        <asp:Literal ID="SignInLiteral" runat="server" Text="<%$Resources:Titles, SignIn %>" />
                    </h1>







                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4 control-label">
                                <asp:Literal ID="UserIdLiteral" runat="server" Text="<%$Resources:Titles, UserId %>" />
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="UserIdTextBox" runat="server" CssClass="form-control input-sm" placeholder="<%$Resources:Titles, UserId %>" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputPassword3" class="col-sm-4 control-label">
                                <asp:Literal ID="PasswordLiteral" runat="server" Text="<%$Resources:Titles, Password %>" />
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control input-sm" placeholder="<%$Resources:Titles, Password %>" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="BranchDropDownList" class="col-sm-4 control-label">
                                <asp:Literal ID="SelectBranchLiteral" runat="server" Text="<%$Resources:Titles, SelectYourBranch  %>" />
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="BranchDropDownList" runat="server" CssClass="form-control input-sm"
                                    DataTextField="OfficeName" DataValueField="OfficeId" placeholder="<%$Resources:Titles, SelectYourBranch  %>">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="BranchDropDownList" class="col-sm-4 control-label">Select Language</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="LanguageDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="English (United States)" Value="en-US" />
                                    <asp:ListItem Text="English (Great Britain)" Value="en-GB" />
                                    <asp:ListItem Text="Français (France)" Value="fr-FR" />
                                    <asp:ListItem Text="Deutsch (Deutschland)" Value="de-DE" />
                                    <asp:ListItem Text="नेपाली (नेपाल)" Value="ne-NP" />
                                    <asp:ListItem Text="हिन्दी (India)" Value="hi-IN" />
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label"></label>
                            <div class="col-sm-8">


                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="RememberMe" runat="server" />
                                        <asp:Literal runat="server" Text="<%$Resources:Titles, RememberMe %>"></asp:Literal>
                                    </label>
                                </div>

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label"></label>
                            <div class="col-sm-8">
                                <asp:Button ID="SignInButton" runat="server" Text="<%$Resources:Titles, SignIn %>" OnClick="SignInButton_Click" CssClass="btn btn-default btn-sm" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label"></label>
                            <div class="col-sm-8">
                                <asp:LinkButton ID="CannotAccessAccountLinkButton" runat="server" Text="<%$Resources:Questions, CannotAccessAccount %>" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-4 control-label"></label>
                            <div class="col-sm-8">
                                <asp:Literal ID="MessageLiteral" runat="server" />
                            </div>
                        </div>


                    </div>


                </div>
                <script type="text/javascript">
                    $("#UserIdTextBox").val('binod');
                    $("#PasswordTextBox").val('binod');
                </script>
            </div>
        </div>
    </form>
</body>
</html>
