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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecurringInvoiceSetup.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Setup.RecurringInvoiceSetup" %>
<asp:PlaceHolder runat="server" ID="ScrudPlaceholder" />
<script type="text/javascript">
    function scrudCustomValidator() {
        var startFromTextbox = $("#starts_from_textbox");
        var endsOnTextbox = $("#ends_on_textbox");

        var startForm = parseDate(startFromTextbox.val());
        var endsOn = parseDate(endsOnTextbox.val());

        if (endsOn < startForm) {
            displayMessage(Resources.Warnings.DateErrorMessage());
            return false;
        };
        return true;
    };
</script>