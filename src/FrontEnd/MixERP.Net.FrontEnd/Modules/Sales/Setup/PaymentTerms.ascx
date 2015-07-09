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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentTerms.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Setup.PaymentTerms" %>
<asp:PlaceHolder runat="server" ID="ScrudPlaceholder" />
<script type="text/javascript">
    function scrudCustomValidator() {
        var dueDaysTextbox = $("#due_days_textbox");
        var dueFrequencyIdDropdownlist = $("#due_frequency_id_dropdownlist");
        var lateFeeIdDropdownlist = $("#late_fee_id_dropdownlist");
        var lateFeePostingFrequencyIdDropdownlist = $("#late_fee_posting_frequency_id_dropdownlist");

        var dueDays = parseInt2(dueDaysTextbox.val());
        var dueFrequency = parseInt2(dueFrequencyIdDropdownlist.getSelectedValue());
        var lateFee = parseInt2(lateFeeIdDropdownlist.getSelectedValue());
        var lateFeePostingFrequency = parseInt2(lateFeePostingFrequencyIdDropdownlist.getSelectedValue());

        if (!dueFrequency && dueDays === 0 || dueFrequency && dueDays > 0) {
            displayMessage(Resources.Warnings.DueFrequencyErrorMessage());
            return false;
        };
        
        if (!lateFee && lateFeePostingFrequency || lateFee && !lateFeePostingFrequency) {
           displayMessage(Resources.Warnings.LateFeeErrorMessage());
           return false;
       };
        return true;
    };
</script>   