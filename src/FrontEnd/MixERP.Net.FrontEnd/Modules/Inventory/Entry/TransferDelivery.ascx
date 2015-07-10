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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferDelivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Entry.TransferDelivery" %>
<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
<script type="text/javascript">
    var grid = $("#TransferGridView");

    $(document).ready(function() {
        grid.addClass("loading");
        var requestId = parseFloat(getQueryStringByName("RequestId") || 0);

        if (!requestId) {
            return;
        };


        var ajaxGetModel = GetModel(requestId);

        ajaxGetModel.success(function (msg) {
            loadModel(msg.d);
        });

        ajaxGetModel.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });

    });

    function loadModel(models) {

        for (i = 0; i < models.length; i++) {
            var storeName = models[i].StoreName;
            var itemCode = models[i].ItemCode;
            var itemName = models[i].ItemName;
            var unitName = models[i].UnitName;
            var quantity = models[i].Quantity;

            appendToTable("Cr", storeName, itemCode, itemName, unitName, quantity);
        };

        grid.removeClass("loading");
    };

    function GetModel(requestId) {
        url = "/Modules/Inventory/Services/Entry/TransferDelivery.asmx/GetModel";

        data = appendParameter("", "requestId", requestId);

        data = getData(data);
        return getAjax(url, data);
    };
</script>