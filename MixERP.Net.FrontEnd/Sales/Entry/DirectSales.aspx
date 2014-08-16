<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DirectSales.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.Entry.DirectSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server"
        ID="DirectSalesControl"
        Book="Sales"
        SubBook="Direct"
        Text="<%$Resources:Titles, DirectSales %>"
        DisplayTransactionTypeRadioButtonList="true"
        ShowCashRepository="true"
        VerifyStock="true"
        TopPanelWidth="850" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        saveButton.click(function () {
            if (validateProductControl()) {
                save();
            };
        });

        var save = function () {
            var ajaxsaveDirectSales = saveDirectSales(valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments);

            ajaxsaveDirectSales.done(function (response) {
                var id = response.d;
                window.location = "/Sales/Confirmation/DirectSales.aspx?TranId=" + id;
            });

            ajaxsaveDirectSales.fail(function (jqXHR, textStatus) {
                logError(jqXHR.responseText);
            });

        };

        var saveDirectSales = function (valueDate, storeId, partyCode, priceTypeId, referenceNumber, data, statementReference, transactionType, agentId, shipperId, shippingAddressCode, shippingCharge, cashRepositoryId, costCenterId, transactionIds, attachments) {
            var d = "";
            d = appendParameter(d, "valueDate", valueDate);
            d = appendParameter(d, "storeId", storeId);
            d = appendParameter(d, "partyCode", partyCode);
            d = appendParameter(d, "priceTypeId", priceTypeId);
            d = appendParameter(d, "referenceNumber", referenceNumber);
            d = appendParameter(d, "data", data);
            d = appendParameter(d, "statementReference", statementReference);
            d = appendParameter(d, "transactionType", transactionType);
            d = appendParameter(d, "agentId", agentId);
            d = appendParameter(d, "shipperId", shipperId);
            d = appendParameter(d, "shippingAddressCode", shippingAddressCode);
            d = appendParameter(d, "shippingCharge", shippingCharge);
            d = appendParameter(d, "cashRepositoryId", cashRepositoryId);
            d = appendParameter(d, "costCenterId", costCenterId);
            d = appendParameter(d, "transactionIds", transactionIds);
            d = appendParameter(d, "attachmentsJSON", attachments);

            d = "{" + d + "}";

            return $.ajax({
                type: "POST",
                url: "/Services/Sales/DirectSales.asmx/Save",
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

    </script>

</asp:Content>
