<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="MixERP.Net.FrontEnd.Purchase.Entry.Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:Product runat="server"
        ID="PurchaseOrder"
        Book="Purchase"
        SubBook="Order"
        Text="<%$Resources:Titles, PurchaseOrder %>"
        OnSaveButtonClick="PurchaseOrder_SaveButtonClick"
         />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        saveButton.click(function () {
            if (validateProductControl()) {
                save();
            };
        });

        var save = function () {
            var ajaxSavePurchaseOrder = savePurchaseOrder(valueDate, partyCode, referenceNumber, data, statementReference, attachments);

            ajaxSavePurchaseOrder.done(function (response) {
                var id = response.d;
                window.location = "/Purchase/Confirmation/Order.aspx?TranId=" + id;
            });

            ajaxSavePurchaseOrder.fail(function (jqXHR) {
                var errorMessage = JSON.parse(jqXHR.responseText).Message;
                errorLabelBottom.html(errorMessage);
                logError(errorMessage);
            });
        };

        var savePurchaseOrder = function (valueDate, partyCode, referenceNumber, data, statementReference, attachments) {
            var d = "";
            d = appendParameter(d, "valueDate", valueDate);
            d = appendParameter(d, "partyCode", partyCode);
            d = appendParameter(d, "referenceNumber", referenceNumber);
            d = appendParameter(d, "data", data);
            d = appendParameter(d, "statementReference", statementReference);
            d = appendParameter(d, "attachmentsJSON", attachments);

            d = "{" + d + "}";

            return $.ajax({
                type: "POST",
                url: "/Services/Purchase/Order.asmx/Save",
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

    </script>
</asp:Content>
