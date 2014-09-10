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
    var p = "<%= GetPartyNameParameter() %>";
</script>
<script src="../Scripts/Setup/Parties.js"></script>