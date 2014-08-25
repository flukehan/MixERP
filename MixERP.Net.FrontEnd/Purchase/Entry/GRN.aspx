<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="GRN.aspx.cs" Inherits="MixERP.Net.FrontEnd.Purchase.Entry.GRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server"
        ID="GoodReceiptNote"
        Book="Purchase"
        SubBook="Receipt"
        Text="<%$Resources:Titles, GoodsReceiptNote %>"
        ShowTransactionType="False"
        ShowStore="True"
        ShowCostCenter="True" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        saveButton.click(function () {
            if (validateProductControl()) {
                save();
            };
        });

        var save = function () {
            var ajaxSaveGRN = saveGRN(valueDate, storeId, partyCode, referenceNumber, data, statementReference, costCenterId, transactionIds, attachments);

            ajaxSaveGRN.done(function (response) {
                var id = response.d;
                window.location = "/Purchase/Confirmation/GRN.aspx?TranId=" + id;
            });

            ajaxSaveGRN.fail(function (jqXHR) {
                var errorMessage = JSON.parse(jqXHR.responseText).Message;
                errorLabelBottom.html(errorMessage);
                logError(errorMessage);
            });
        };

        var saveGRN = function (valueDate, storeId, partyCode, referenceNumber, data, statementReference, costCenterId, transactionIds, attachments) {
            var d = "";
            d = appendParameter(d, "valueDate", valueDate);
            d = appendParameter(d, "storeId", storeId);
            d = appendParameter(d, "partyCode", partyCode);
            d = appendParameter(d, "referenceNumber", referenceNumber);
            d = appendParameter(d, "data", data);
            d = appendParameter(d, "statementReference", statementReference);
            d = appendParameter(d, "costCenterId", costCenterId);
            d = appendParameter(d, "transactionIds", transactionIds);
            d = appendParameter(d, "attachmentsJSON", attachments);

            d = "{" + d + "}";

            return $.ajax({
                type: "POST",
                url: "/Services/Purchase/GRN.asmx/Save",
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

    </script>
</asp:Content>
