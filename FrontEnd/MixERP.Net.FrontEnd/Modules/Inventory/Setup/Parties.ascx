<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartiesPopup.ascx.designer.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Setup.PartiesPopup" %>

<asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />
<script type="text/javascript">
    var p = "<%= GetPartyNameParameter() %>";
</script>
<script src="../Scripts/Setup/Parties.js"></script>