<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartiesPopup.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Setup.PartiesPopup" MasterPageId="MixERPBlankMaster.Master" %>
<style type="text/css">
    form {
        background-color: white !important;
    }

    #GridPanel {
        width: 99% !important;
    }

    .container {
        padding: 24px;
    }
</style>

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

        partyNameTextBox.val(partyName.trim().replace(/ +(?= )/g, ''));
    };

    var isInIframe = (window.location != window.parent.location) ? true : false;
</script>