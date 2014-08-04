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

        .container {
            padding:24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="container" runat="server" id="container">
        <asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />
    </div>
</asp:Content>
