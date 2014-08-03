<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>

<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" MasterPageFile="~/MixERPBlankMaster.Master" CodeBehind="PartiesPopup.aspx.cs" Inherits="MixERP.Net.FrontEnd.Inventory.Setup.PartiesPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link href="/bundles/stylesheets/parties-popup.min.css" rel="stylesheet" />

    <title>Select Party</title>

    <style type="text/css">
        form {
            background-color: white !important;
        }

        #GridPanel {
            width: 99% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">


        <div class="container" runat="server" id="container">
            <asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />
        </div>
        <script type="text/javascript">

            $("#first_name_textbox").blur(function () {
                updatePartyName();
            });

            $("#middle_name_textbox").blur(function () {
                updatePartyName();
            });

            $("#last_name_textbox").blur(function () {
                updatePartyName();
            });            


            var updatePartyName = function () {
                var p = "<%= GetPartyNameParameter() %>";

                var firstName = $("#first_name_textbox").val();
                var middleName = $("#middle_name_textbox").val();
                var lastName = $("#last_name_textbox").val();

                var partyName = p.replace("FirstName", firstName);
                partyName = partyName.replace("MiddleName", middleName);
                partyName = partyName.replace("LastName", lastName);

                var partyNameTextBox = $("#party_name_textbox");

                //if (partyNameTextBox.val() == "") {
                    $("#party_name_textbox").val(partyName.trim().replace(/ +(?= )/g, ''));
                //}
            };

            var isInIframe = (window.location != window.parent.location) ? true : false;

            if (!isInIframe) {
                $(".container").css("padding", "24px");
            }

        </script>
</asp:Content>
