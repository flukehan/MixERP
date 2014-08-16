<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.Entry.Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server"
        ID="SalesOrder"
        Book="Sales"
        SubBook="Order"
        Text="<%$Resources:Titles, SalesOrder %>"
        DisplayTransactionTypeRadioButtonList="false"
         />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        saveButton.click(function () {
            if (validateProductControl()) {
                save();
            };
        });

        var save = function() {
            var valueDate = dateTextBox.val();
            var storeId = parseFloat2(storeDropDownList.getSelectedValue());
            var partyCode = partyDropDownList.getSelectedValue();
            var priceTypeId = parseFloat2(priceTypeDropDownList.getSelectedValue());
            var referenceNumber = referenceNumberTextBox.getSelectedValue();

            var data = productGridViewDataHidden.val();
            var shippingAddressCode = shippingAddressDropDownList.getSelectedText();
            var shipperId = parseFloat2(shippingCompanyDropDownList.getSelectedValue());
            var shippingCharge = parseFloat2(shippingChargeTextBox.val());
            var cashRepositoryId = parseFloat2(cashRepositoryDropDownList.getSelectedValue());
            var costCenterId = parseFloat2(costCenterDropDownList.getSelectedValue());
            var agentId = parseFloat2(salesPersonDropDownList.getSelectedValue());
            var statementReference = statementReferenceTextBox.val();
            var transactionIds = tranIdCollectionHiddenField.val();
            var attachments = uploadedFilesHidden.val();

            var ajaxSaveOder = saveOrder(valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, cashRepositoryId, costCenterId, agentId, statementReference, transactionIds, attachments);

            ajaxSaveOder.done(function (response) {
                var id = response.d;
                window.location = "/Sales/Order.aspx?TranId=" + id;
            });

            ajaxSaveOder.fail(function (jqXHR, textStatus) {
                logError(jqXHR.responseText);
            });

        };

        var saveOrder = function (valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, shippingAddressCode, shipperId, shippingCharge, cashRepositoryId, costCenterId, agentId, statementReference, transactionIds, attachments) {

            var d = "";
            d = appendParameter(d, "valueDate", valueDate);
            d = appendParameter(d, "storeId", storeId);
            d = appendParameter(d, "partyCode", partyCode);
            d = appendParameter(d, "priceTypeId", priceTypeId);
            d = appendParameter(d, "referenceNumber", referenceNumber);
            d = appendParameter(d, "data", data);
            d = appendParameter(d, "statementReference", statementReference);
            d = appendParameter(d, "transactionIds", transactionIds);
            d = appendParameter(d, "attachmentsJSON", attachments);

            d = "{" + d + "}";

            return $.ajax({
                type: "POST",
                url: "/Services/Sales/SalesOrder.asmx/Save",
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

    </script>
</asp:Content>
