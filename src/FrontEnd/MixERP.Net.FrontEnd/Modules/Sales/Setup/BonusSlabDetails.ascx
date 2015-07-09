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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BonusSlabDetails.ascx.cs"
    Inherits="MixERP.Net.Core.Modules.Sales.Setup.BonusSlabDetails" %>
<asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />

<script type="text/javascript">
    function scrudCustomValidator() {
        var amountFromTextBox = $("#amount_from_textbox");
        var amountToTextBox = $("#amount_to_textbox");

        var from = parseFloat2(amountFromTextBox.val());
        var to = parseFloat2(amountToTextBox.val());

        if (to <= from) {
            displayMessage(Resources.Warnings.CompareAmountErrorMessage());
            return false;
        };

        return true;
    };
</script>