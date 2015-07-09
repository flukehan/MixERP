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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesTaxExempts.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Tax.SalesTaxExempts" %>

<asp:PlaceHolder runat="server" ID="ScrudPlaceholder" />
<script type="text/javascript">
    function scrudCustomValidator() {
        var validFromTextbox = $("#valid_from_textbox");
        var validTillTextbox = $("#valid_till_textbox");
        var priceFromTextbox = $("#price_from_textbox");
        var priceToTextbox = $("#price_to_textbox");

        var validFrom = parseDate(validFromTextbox.val());
        var validTill = parseDate(validTillTextbox.val());
        var priceFrom = parseFloat2(priceFromTextbox.val());
        var priceTo = parseFloat2(priceToTextbox.val());

        if (validTill < validFrom) {
            displayMessage(Resources.Warnings.DateErrorMessage());
            return false;
        };
        if (priceTo <= priceFrom) {
            displayMessage(Resources.Warnings.ComparePriceErrorMessage());
            return false;
        };

        return true;
    };

</script>