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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApiAccess.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Policy.ApiAccess" %>
<asp:PlaceHolder runat="server" ID="ScrudPlaceholder"></asp:PlaceHolder>

<script>
    $(document).ready(function () {
        addPocoSelect();
    });

    function addPocoSelect() {
        var pocoTypeNameTextbox = $("#poco_type_name_textbox");

        pocoTypeNameTextbox.hide();

        var select = "<select id='poco_type_name_select'><select>";

        pocoTypeNameTextbox.parent().prepend(select);

        var pocoTypeNameSelect = $("#poco_type_name_select");

        var options = window.pocos.toString().split(',');

        for (i = 0; i < options.length; i++) {
            pocoTypeNameSelect.append("<option>" + options[i] + "</option>");
        };


        pocoTypeNameSelect.blur(function () {
            pocoTypeNameTextbox.val($(this).getSelectedText());
        });
    };

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        //Fired on each ASP.net AJAX request.
        addPocoSelect();
    });

</script>