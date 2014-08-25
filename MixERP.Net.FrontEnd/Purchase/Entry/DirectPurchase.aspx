<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DirectPurchase.aspx.cs" Inherits="MixERP.Net.FrontEnd.Purchase.Entry.DirectPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server" ID="DirectPurchaseControl"
        Book="Purchase"
        SubBook="Direct"
        Text="<%$Resources:Titles, DirectPurchase %>"
        ShowTransactionType="true"
        ShowCashRepository="true"
        ShowStore="True"
        ShowCostCenter="True"
        OnSaveButtonClick="Purchase_SaveButtonClick"
        TopPanelWidth="750px" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        saveButton.click(function () {
            if (validateProductControl()) {
                save();
            };
        });

        var save = function () {
            var ajaxSaveDirectPurchase = saveDirectPurchase(valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, cashRepositoryId, costCenterId, attachments);

            ajaxSaveDirectPurchase.done(function (response) {
                var id = response.d;
                window.location = "/Purchase/Confirmation/DirectPurchase.aspx?TranId=" + id;
            });

            ajaxSaveDirectPurchase.fail(function (jqXHR) {
                var errorMessage = JSON.parse(jqXHR.responseText).Message;
                errorLabelBottom.html(errorMessage);
                logError(errorMessage);
            });
        };

        var saveDirectPurchase = function (valueDate, storeId, partyCode, referenceNumber, data, statementReference, transactionType, cashRepositoryId, costCenterId, attachments) {
            var d = "";
            d = appendParameter(d, "valueDate", valueDate);
            d = appendParameter(d, "storeId", storeId);
            d = appendParameter(d, "partyCode", partyCode);
            d = appendParameter(d, "referenceNumber", referenceNumber);
            d = appendParameter(d, "data", data);
            d = appendParameter(d, "statementReference", statementReference);
            d = appendParameter(d, "transactionType", transactionType);
            d = appendParameter(d, "cashRepositoryId", cashRepositoryId);
            d = appendParameter(d, "costCenterId", costCenterId);
            d = appendParameter(d, "attachmentsJSON", attachments);

            d = "{" + d + "}";

            return $.ajax({
                type: "POST",
                url: "/Services/Purchase/DirectPurchase.asmx/Save",
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

    </script>
</asp:Content>
