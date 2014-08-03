<%@ Page Title="Select Item" Language="C#" AutoEventWireup="true" MasterPageFile="~/MixERPBlankMaster.Master" CodeBehind="ItemsPopup.aspx.cs" Inherits="MixERP.Net.FrontEnd.Inventory.Setup.ItemsPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link href="/bundles/stylesheets/parties-popup.min.css" rel="stylesheet" />
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
        var isInIframe = (window.location != window.parent.location) ? true : false;

        if (!isInIframe) {
            $(".container").css("padding", "24px");
        }
    </script>
</asp:Content>
