<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartiesPopup.aspx.cs" Inherits="MixERP.Net.FrontEnd.Inventory.Setup.PartiesPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/themes/purple/main.css" rel="stylesheet" type="text/css" runat="server" />
    <script src="/Scripts/jquery-ui/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/shortcut.js"></script>
    <script src="/Scripts/colorbox/jquery.colorbox-min.js"></script>
    <link href="/Scripts/colorbox/colorbox.css" rel="stylesheet" />

    <title></title>

    <style type="text/css">
        form {
            background-color: white!important;
        }

        #GridPanel {
            width: 99%!important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" runat="server" id="container">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        </div>
        <script type="text/javascript">
            $("#party_name_textbox").focus(function () {
                var p = "<%= GetPartyNameParameter() %>";
            updatePartyName(p);
        });


        var updatePartyName = function (p) {
            var firstName = $("#first_name_textbox").val();
            var middleName = $("#middle_name_textbox").val();
            var lastName = $("#last_name_textbox").val();

            var partyName = p.replace("FirstName", firstName);
            var partyName = partyName.replace("MiddleName", middleName);
            var partyName = partyName.replace("LastName", lastName);

            var partyNameTextBox = $("#party_name_textbox");

            if (partyNameTextBox.val() == "") {
                $("#party_name_textbox").val(partyName.trim().replace(/ +(?= )/g, ''));
            }
        }

        var isInIframe = (window.location != window.parent.location) ? true : false;

        if (!isInIframe) {
            $(".container").css("padding", "24px");
        }

        </script>
    </form>
</body>
</html>
