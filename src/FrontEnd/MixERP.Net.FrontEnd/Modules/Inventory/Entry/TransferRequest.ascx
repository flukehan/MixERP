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
<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="TransferRequest.ascx.cs" 
    Inherits="MixERP.Net.Core.Modules.Inventory.Entry.TransferRequest"
    OverridePath="/Modules/Inventory/TransferRequest.mix"
     %>
<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
<input type="hidden" id="ItemIdHidden" />

<script type="text/javascript">
    function StockAdjustmentFactory_FormvView_SaveButton_Callback() {
        var valueDate = Date.parseExact(valueDateTextBox.val(), window.shortDateFormat);
        var referenceNumber = referenceNumberInputText.val();
        var statementReference = statementReferenceTextArea.val();
        var tableData = tableToJSON(transferGridView);

        var ajaxSaveTransfer = SaveTransfer(valueDate, referenceNumber, statementReference, tableData);

        ajaxSaveTransfer.success(function (msg) {
            var id = msg.d;
            window.location = "/Modules/Inventory/Confirmation/TransferRequest.mix?TranId=" + id;
        });

        ajaxSaveTransfer.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });
    };

    function SaveTransfer(valueDate, referenceNumber, statementReference, tableData) {

        url = "/Modules/Inventory/Services/Entry/TransferRequest.asmx/Save";

        data = appendParameter("", "valueDate", valueDate);
        data = appendParameter(data, "referenceNumber", referenceNumber);
        data = appendParameter(data, "statementReference", statementReference);
        data = appendParameter(data, "data", tableData);
        data = getData(data);
        return getAjax(url, data);
    };
</script>