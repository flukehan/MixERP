<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductControl" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<div style="width: 1000px; overflow: hidden; margin: 0;">
    <div id="info-panel">
        S H O R T C U T S :
        <br />
        <hr class="hr" style="border-color: #97d300;" />
        <table>
            <tr>
                <td>F2
                </td>
                <td>Add a New Party
                </td>
            </tr>
            <tr>
                <td>F4
                </td>
                <td>Add a New Item
                </td>
            </tr>
            <tr>
                <td>CTRL + RET
                </td>
                <td>Add a New Row
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="TitleLabel" CssClass="title" runat="server" />

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="ajax-container">
                <img runat="server" alt="progress" src="~/spinner.gif" class="ajax-loader" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:HiddenField ID="ModeHiddenField" runat="server" />

    <asp:Panel ID="TopPanel" CssClass="form" runat="server">
        <table class="form-table">
            <tr>
                <td>
                    <asp:Literal ID="DateLiteral" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="StoreLiteral" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="TransactionTypeLiteral" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="PartyLiteral" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="PriceTypeLiteral" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="ReferenceNumberLiteral" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr style="vertical-align: middle;">
                <td>
                    <mixerp:DateTextBox ID="DateTextBox" runat="server" Width="70" CssClass="date" />
                </td>
                <td>
                    <asp:DropDownList ID="StoreDropDownList" runat="server" Width="80">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RadioButtonList ID="TransactionTypeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="<%$ Resources:Titles, Cash %>" Value="<%$ Resources:Titles, Cash %>" Selected="True" />
                        <asp:ListItem Text="<%$ Resources:Titles, Credit %>" Value="<%$ Resources:Titles, Credit %>" />
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="PartyCodeTextBox" runat="server" Width="80"
                        onblur="selectDropDownListByValue(this.id, 'PartyDropDownList');"
                        ToolTip="F2" />
                    <asp:DropDownList ID="PartyDropDownList" runat="server" Width="150"
                        onchange="if(this.selectedIndex == 0) { return false };document.getElementById('PartyCodeTextBox').value = this.options[this.selectedIndex].value;document.getElementById('PartyCodeHidden').value = this.options[this.selectedIndex].value;"
                        ToolTip="F2">
                    </asp:DropDownList>
                    <asp:HiddenField ID="PartyCodeHidden" runat="server" />
                    <asp:HiddenField ID="PartyIdHidden" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="PriceTypeDropDownList" runat="server" Width="80">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="ReferenceNumberTextBox" runat="server" Width="60" MaxLength="24" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <p>
        <asp:Label ID="ErrorLabelTop" runat="server" CssClass="error" />
    </p>
    <div class="center">


        <asp:HiddenField ID="ProductGridViewDataHidden" runat="server" />
        <table id="ProductGridView" class="grid2 grid3" style="width: 100%;" runat="server">
            <tbody>
                <tr class="grid2-header">
                    <th scope="col">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Code %>" />
                    </th>
                    <th>
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,ItemName %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,QuantityAbbreviated %>" />
                    </th>
                    <th>
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Unit%>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Price %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Amount %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Discount %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,SubTotal %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Rate %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Tax %>" />
                    </th>
                    <th class="right">
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Total %>" />
                    </th>
                    <th>
                        <asp:Literal runat="server" Text="<%$ Resources:Titles,Action %>" />
                    </th>
                </tr>
                <tr class="grid2-footer">
                    <td>
                        <input type="text"
                            id="ItemCodeTextBox"
                            onblur="selectDropDownListByValue(this.id, 'ItemDropDownList');"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltC%>" />'
                            style="width: 60px;" />
                    </td>
                    <td>
                        <select name="ItemDropDownList"
                            id="ItemDropDownList"
                            onchange="document.getElementById('ItemCodeTextBox').value = this.options[this.selectedIndex].value;document.getElementById('ItemCodeHidden').value = this.options[this.selectedIndex].value;if(this.selectedIndex == 0) { return false; };"
                            onblur="getPrice();"
                            style="width: 300px;"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlI%>" />'>
                        </select>
                    </td>
                    <td>
                        <input type="text"
                            id="QuantityTextBox"
                            class="right"
                            onblur="updateTax();calculateAmount();"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlQ%>" />'
                            style="width: 50px;"
                            value="1" />
                    </td>
                    <td>
                        <select name="UnitDropDownList"
                            id="UnitDropDownList"
                            onchange="$('#UnitNameHidden').val($(this).children('option').filter(':selected').text());$('#UnitIdHidden').val($(this).children('option').filter(':selected').val());"
                            onblur="getPrice();"
                            style="width: 70px;"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlU%>" />'>
                        </select>
                    </td>
                    <td>
                        <input type="text"
                            id="PriceTextBox"
                            class="right number"
                            onblur="updateTax();calculateAmount();"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltP%>" />'
                            style="width: 65px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="AmountTextBox"
                            readonly="readonly"
                            class="right number"
                            onblur="updateTax();calculateAmount();"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="DiscountTextBox"
                            class="right number"
                            onblur="updateTax();calculateAmount();"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlD%>" />'
                            style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="SubTotalTextBox"
                            readonly="readonly"
                            class="right number"
                            onblur="updateTax();calculateAmount();"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TaxRateTextBox"
                            class="right"
                            onblur="updateTax();calculateAmount();"
                            style="width: 40px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TaxTextBox"
                            class="right number"
                            onblur="calculateAmount();"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlT%>" />'
                            style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TotalTextBox"
                            readonly="readonly"
                            class="right number"
                            onblur="updateTax();calculateAmount();"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="button"
                            id="AddButton"
                            class="button"
                            onclick="updateTax(); calculateAmount(); addRow();"
                            value='<asp:Literal runat="server" Text="<%$Resources:Titles,Add%>" />'
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlReturn%>" />' />

                    </td>
                </tr>
            </tbody>
        </table>

        <asp:Panel ID="FormPanel" runat="server" Enabled="false">
            <asp:Label ID="ErrorLabel" runat="server" CssClass="error" />
        </asp:Panel>
        <div class="vpad8"></div>

        <asp:Label ID="AttachmentLabel" runat="server" Text="<%$ Resources:Titles, AttachmentsPlus %>" CssClass="sub-title" onclick="$('#attachment').show(500);" />

        <div id="attachment" style="display: none; margin: 4px;">
            <mixerp:Attachment ID="Attachment1" runat="server" />
        </div>

        <asp:Panel ID="BottomPanel" CssClass="form" runat="server" Style="margin: 4px;">
            <asp:Table runat="server" CssClass="form-table grid3">
                <asp:TableRow ID="ShippingAddressRow" runat="server">
                    <asp:TableCell Style="vertical-align: top!important;">
                        <asp:Literal ID="ShippingAddressDropDownListLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:DropDownList ID="ShippingAddressDropDownList" runat="server" Width="200" onchange="showShippingAddress()">
                        </asp:DropDownList>
                        <asp:HiddenField ID="ShippingAddressCodeHidden" runat="server" />
                        <p>
                            <asp:TextBox
                                ID="ShippingAddressTextBox"
                                runat="server"
                                ReadOnly="true"
                                TextMode="MultiLine"
                                Width="410px"
                                Height="72px" />
                        </p>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="ShippingCompanyRow" runat="server">
                    <asp:TableCell>
                        <asp:Literal ID="ShippingCompanyDropDownListLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:DropDownList ID="ShippingCompanyDropDownList" runat="server" Width="200">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="ShippingChargeRow" runat="server">
                    <asp:TableCell>
                        <asp:Literal ID="ShippingChargeTextBoxLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="ShippingChargeTextBox" runat="server" CssClass="number" Width="100px">
                        </asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Literal ID="TotalsLiteral" runat="server" Text="<%$Resources:Titles, Totals %>">
                        </asp:Literal>
                    </asp:TableCell><asp:TableCell>
                        <table style="border-collapse: collapse; width: 100%;">
                            <tr>
                                <td>
                                    <asp:Literal ID="RunningTotalTextBoxLabelLiteral" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="TaxTotalTextBoxLabelLiteral" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="GrandTotalTextBoxLabelLiteral" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="RunningTotalTextBox" runat="server" Width="100" ReadOnly="true" />

                                </td>
                                <td>
                                    <asp:TextBox ID="TaxTotalTextBox" runat="server" Width="100" ReadOnly="true" />

                                </td>
                                <td>
                                    <asp:TextBox ID="GrandTotalTextBox" runat="server" Width="100" ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CashRepositoryRow" runat="server">
                    <asp:TableCell runat="server">
                        <asp:Literal ID="CashRepositoryDropDownListLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:DropDownList ID="CashRepositoryDropDownList" runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="CashRepositoryDropDownList_SelectIndexChanged"
                            Width="300px">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CashRepositoryBalanceRow" runat="server">
                    <asp:TableCell>
                        <asp:Literal ID="CashRepositoryBalanceTextBoxLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="CashRepositoryBalanceTextBox" runat="server" Width="100" ReadOnly="true" />
                        <asp:Literal ID="DrLiteral" runat="server" Text="<%$Resources:Titles, Dr %>" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CostCenterRow" runat="server">
                    <asp:TableCell>
                        <asp:Literal ID="CostCenterDropDownListLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:DropDownList ID="CostCenterDropDownList" runat="server" Width="300">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="SalespersonRow" runat="server">
                    <asp:TableCell>
                        <asp:Literal ID="SalespersonDropDownListLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:DropDownList ID="SalespersonDropDownList" runat="server" Width="300">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Literal ID="StatementReferenceTextBoxLabelLiteral" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="StatementReferenceTextBox" runat="server" TextMode="MultiLine" Width="410" Height="100">
                        </asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                                    &nbsp;
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="SaveButton" runat="server" CssClass="button" Text="<%$Resources:Titles, Save %>" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

        <asp:HiddenField ID="ItemCodeHidden" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="ItemIdHidden" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="UnitIdHidden" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="UnitNameHidden" runat="server"></asp:HiddenField>

        <p>
            <asp:Label ID="ErrorLabelBottom" runat="server" CssClass="error" />
        </p>

    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AjaxPageLoadHandler);
    });


    $("#SaveButton").click(function () {
        updateData();
        alert('foo');
    });

    function AjaxPageLoadHandler() {
        showShippingAddress();
    }

    var getPrice = function () {
        var tranBook = "<%=this.GetTranBook() %>";
        var itemCode = $("#ItemCodeHidden").val();
        var partyCode = $("#PartyCodeHidden").val();
        var priceTypeId = $("#PriceTypeDropDownList").val();
        var unitId = $("#UnitIdHidden").val();


        if (!unitId) return;


        var priceTextBox = $("#PriceTextBox");
        var taxRateTextBox = $("#TaxRateTextBox");


        $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/GetPrice") %>",
            data: "{tranBook:'" + tranBook + "', itemCode:'" + itemCode + "', partyCode:'" + partyCode + "', priceTypeId:'" + priceTypeId + "', unitId:'" + unitId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                priceTextBox.val(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                $.notify(err, "error");
            }
        });

        $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/GetTaxRate") %>",
            data: "{itemCode:'" + itemCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                taxRateTextBox.val(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                $.notify(err, "error");
            }
        });



        calculateAmount();
    };


    var addRow = function () {
        var errorLabel = $("#ErrorLabel");
        var itemCodeTextBox = $("#ItemCodeTextBox");
        var itemDropDownList = $("#ItemDropDownList");
        var quantityTextBox = $("#QuantityTextBox");
        var unitDropDownList = $("#UnitDropDownList");
        var unitIdHidden = $("#UnitIdHidden");
        var unitNameHidden = $("#UnitNameHidden");
        var priceTextBox = $("#PriceTextBox");
        var discountTextBox = $("#DiscountTextBox");
        var taxRateTextBox = $("#TaxRateTextBox");
        var taxTextBox = $("#TaxTextBox");
        var storeDropDownList = $("#StoreDropDownList");
        var verifyStock = ("<%= this.VerifyStock %>" == "True");
        var isSales = ("<%= this.Book %>" == "Sales");

        var itemCode = itemCodeTextBox.val();
        var itemName = itemDropDownList.find("option:selected").text();
        var quantity = parseFloat2(quantityTextBox.val());
        var unitId = parseFloat2(unitIdHidden.val());
        var unitName = unitNameHidden.val();
        var price = parseFloat2(priceTextBox.val());
        var discount = parseFloat2(discountTextBox.val());
        var taxRate = parseFloat2(taxRateTextBox.val());
        var tax = parseFloat2(taxTextBox.val());
        var storeId = parseFloat2(storeDropDownList.val());

        var insufficientStockWarningLocalized = "<%= Resources.Warnings.InsufficientStockWarning %>";

        if (isNullOrWhiteSpace(itemCode)) {
            makeDirty(itemCodeTextBox);
            return;
        }

        removeDirty(itemCodeTextBox);

        if (quantity < 1) {
            makeDirty(quantityTextBox);
            return;
        }

        removeDirty(quantityTextBox);

        if (price <= 0) {
            makeDirty(priceTextBox);
            return;
        }

        removeDirty(priceTextBox);

        if (discount < 0) {
            makeDirty(discountTextBox);
            return;
        }

        removeDirty(discountTextBox);

        if (discount > (price * quantity)) {
            makeDirty(discountTextBox);
            return;
        }

        removeDirty(discountTextBox);

        if (tax < 0) {
            makeDirty(taxTextBox);
            return;
        }

        removeDirty(taxTextBox);

        //alert('foo');

        var ajaxItemCodeExists = itemCodeExists(itemCode);
        var ajaxUnitNameExists = unitNameExists(unitName);
        var ajaxIsStockItem = isStockItem(itemCode);
        var ajaxCountItemInStock = countItemInStock(itemCode, unitId, storeId);

        ajaxItemCodeExists.done(function (response) {
            var itemCodeExists = response.d;

            if (!itemCodeExists) {
                $.notify(String.format("Item '{0}' does not exist.", itemCode), "error");
                makeDirty(itemCodeTextBox);
                return;
            }


            removeDirty(itemCodeTextBox);


            ajaxUnitNameExists.done(function (response) {
                var unitNameExists = response.d;

                if (!unitNameExists) {
                    $.notify(String.format("Unit '{0}' does not exist.", unitName), "error");
                    makeDirty(unitDropDownList);
                    return;
                }

                removeDirty(unitDropDownList);

                if (!verifyStock || !isSales) {
                    addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                    return;
                }

                ajaxIsStockItem.done(function (response) {
                    var isStockItem = response.d;

                    if (!isStockItem) {
                        addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                        return;
                    }

                    ajaxCountItemInStock.done(function (response) {
                        var itemInStock = parseFloat2(response.d);

                        if (quantity > itemInStock) {
                            makeDirty(quantityTextBox);
                            errorLabel.html(String.format(insufficientStockWarningLocalized, itemInStock, unitName, itemName));
                            return;
                        }

                        addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
                    });

                });
            });
        });
    };


    var summate = function () {
        var runningTotal = getSum(4);
        var taxTotal = getSum(9);
        var grandTotal = getSum(10);

        $("#RunningTotalTextBox").val(runningTotal);
        $("#TaxTotalTextBox").val(taxTotal);
        $("#GrandTotalTextBox").val(grandTotal);

    };

    var getSum = function (index) {
        var total = 0;

        $('#ProductGridView tr').each(function () {
            var value = parseFloat2($('td', this).eq(index).text());
            total += value;
        });

        return total;
    };

    var itemCodeExists = function (itemCode) {
        return $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/ItemCodeExists") %>",
            data: "{itemCode:'" + itemCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };


    var countItemInStock = function (itemCode, unitId, storeId) {
        return $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/CountItemInStock") %>",
            data: "{itemCode:'" + itemCode + "', unitId:'" + unitId + "', storeId:'" + storeId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    var countItemInStockByUnitName = function (itemCode, unitName, storeId) {
        return $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/CountItemInStockByUnitName") %>",
            data: "{itemCode:'" + itemCode + "', unitId:'" + unitId + "', storeId:'" + storeId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    var isStockItem = function (itemCode) {
        return $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/IsStockItem") %>",
            data: "{itemCode:'" + itemCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    var unitNameExists = function (unitName) {
        return $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/UnitNameExists") %>",
            data: "{unitName:'" + unitName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    var updateData = function () {
        var targetControl = $("#ProductGridViewDataHidden");
        var colData = new Array;
        var rowData = new Array;

        var grid = $("#ProductGridView");
        var rows = grid.find("tr:not(:first-child):not(:last-child)");

        rows.each(function () {
            var row = $(this);

            colData = new Array();

            row.find("td:not(:last-child)").each(function () {
                colData.push($(this).html());
            });

            rowData.push(colData);
        });

        data = JSON.stringify(rowData);
        targetControl.val(data);
    };


    var clearData = function () {
        var grid = $("#ProductGridView");
        var rows = grid.find("tr:not(:first-child):not(:last-child)");
        rows.remove();
    };

    var restoreData = function () {

        var sourceControl = $("#ProductGridViewDataHidden");

        if (isNullOrWhiteSpace(sourceControl.val())) {
            return;
        }

        rowData = JSON.parse(sourceControl.val());

        for (var i = 0; i < rowData.length; i++) {
            var itemCode = rowData[i][0];
            var itemName = rowData[i][1];
            var quantity = parseFloat2(rowData[i][2]);
            var unitName = rowData[i][3];
            var price = parseFloat2(rowData[i][4]);
            var discount = parseFloat2(rowData[i][6]);
            var taxRate = parseFloat2(rowData[i][8]);
            var tax = parseFloat2(rowData[i][9]);

            addRowToTable(itemCode, itemName, quantity, unitName, price, discount, taxRate, tax);
        }
    };


    var addRowToTable = function (itemCode, itemName, quantity, unitName, price, discount, taxRate, tax) {

        var grid = $("#ProductGridView");
        var rows = grid.find("tr:not(:first-child):not(:last-child)");
        var amount = price * quantity;
        var subTotal = amount - discount;
        var total = subTotal + tax;
        var match = false;


        rows.each(function () {
            var row = $(this);
            if (getColumnText(row, 0) == itemCode &&
                getColumnText(row, 1) == itemName &&
                getColumnText(row, 3) == unitName &&
                getColumnText(row, 4) == price &&
                getColumnText(row, 8) == taxRate &&
                parseFloat(getColumnText(row, 5)) / parseFloat(getColumnText(row, 6)) == amount / discount) {

                setColumnText(row, 2, parseFloat2(getColumnText(row, 2)) + quantity);
                setColumnText(row, 5, parseFloat2(getColumnText(row, 5)) + amount);
                setColumnText(row, 6, parseFloat2(getColumnText(row, 6)) + discount);
                setColumnText(row, 7, parseFloat2(getColumnText(row, 7)) + subTotal);
                setColumnText(row, 9, parseFloat2(getColumnText(row, 9)) + tax);
                setColumnText(row, 10, parseFloat2(getColumnText(row, 10)) + total);

                match = true;
                return;
            }
        });

        if (!match) {
            var html = "<tr class='grid2-row'><td>" + itemCode + "</td><td>" + itemName + "</td><td class='right'>" + quantity + "</td><td>" + unitName + "</td><td class='right'>" + price + "</td><td class='right'>" + amount + "</td><td class='right'>" + discount + "</td><td class='right'>" + subTotal + "</td><td class='right'>" + taxRate + "</td><td class='right'>" + tax + "</td><td class='right'>" + total
                + "</td><td><input type='image' src='/Resource/Icons/delete-16.png' onclick='removeRow($(this));' /> </td></tr>";
            grid.find("tr:last").before(html);
        }

        summate();

        $("#ItemCodeTextBox").val("");
        $("#QuantityTextBox").val(1);
        $("#PriceTextBox").val("");
        $("#DiscountTextBox").val("");
        $("#TaxTextBox").val("");
        $("#ErrorLabel").html("");
        $("#ItemCodeTextBox").focus();
    };


    var removeRow = function (cell) {
        var areYouSureLocalized = "<%=Resources.Questions.AreYouSure %>";

        if (confirm(areYouSureLocalized)) {
            cell.closest('tr').remove();
        }
    };

    var getColumnText = function (row, columnIndex) {
        return row.find("td:eq(" + columnIndex + ")").html();
    };

    var setColumnText = function (row, columnIndex, value) {
        row.find("td:eq(" + columnIndex + ")").html(value);
    };


    var calculateAmount = function () {
        var quantityTextBox = $("#QuantityTextBox");
        var priceTextBox = $("#PriceTextBox");
        var amountTextBox = $("#AmountTextBox");
        var discountTextBox = $("#DiscountTextBox");
        var subTotalTextBox = $("#SubtotalTextBox");
        var taxTextBox = $("#TaxTextBox");
        var totalTextBox = $("#TotalTextBox");

        amountTextBox.val(parseFloat2(quantityTextBox.val()) * parseFloat2(priceTextBox.val()));

        subTotalTextBox.val(parseFloat2(amountTextBox.val()) - parseFloat2(discountTextBox.val()));
        totalTextBox.val(parseFloat2(subTotalTextBox.val()) + parseFloat2(taxTextBox.val()));
    };

    var updateTax = function () {
        var taxRateTextBox = $("#TaxRateTextBox");
        var taxTextBox = $("#TaxTextBox");
        var priceTextBox = $("#PriceTextBox");
        var discountTextBox = $("#DiscountTextBox");
        var quantityTextBox = $("#QuantityTextBox");

        var total = parseFloat2(priceTextBox.val()) * parseFloat2(quantityTextBox.val());
        var subTotal = total - parseFloat2(discountTextBox.val());
        var taxableAmount = total;
        var taxAfterDiscount = "<%=Switches.TaxAfterDiscount().ToString()%>";

        if (taxAfterDiscount.toLowerCase() == "true") {
            taxableAmount = subTotal;
        }

        var tax = (taxableAmount * parseFloat2(parseFormattedNumber(taxRateTextBox.val()))) / 100;

        if (parseFloat2(taxTextBox.val()) == 0) {
            if (tax.toFixed) {
                taxTextBox.val(getFormattedNumber(tax.toFixed(2)));
            } else {
                taxTextBox.val(getFormattedNumber(tax));
            }
        }

        if (parseFloat2(tax).toFixed(2) != parseFloat2(parseFormattedNumber(taxTextBox.val())).toFixed(2)) {
            var question = confirm(localizedUpdateTax);

            if (question) {
                if (tax.toFixed) {
                    taxTextBox.val(getFormattedNumber(tax.toFixed(2)));
                } else {
                    taxTextBox.val(getFormattedNumber(tax));
                }
            }
        }
    };

    var showShippingAddress = function () {
        $('#ShippingAddressTextBox').val(($('#ShippingAddressDropDownList').val()));
    };


    $(document).ready(function () {
        $(".form-table td").each(function () {
            var content = $(this).html();
            if (!content.trim()) {
                $(this).html('');
                $(this).hide();
            }
        });
    });


    $(document).ready(function () {
        shortcut.add("F2", function () {
            var url = "/Inventory/Setup/PartiesPopup.aspx?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=PartyIdHidden";
            showWindow(url);
        });

        shortcut.add("F4", function () {
            var url = "/Inventory/Setup/ItemsPopup.aspx?modal=1&CallBackFunctionName=initializeAjaxData&AssociatedControlId=ItemIdHidden";
            //var url = "test.html";
            showWindow(url);
        });

        shortcut.add("ALT+C", function () {
            $('#ItemCodeTextBox').focus();
        });

        shortcut.add("CTRL+I", function () {
            $('#ItemDropDownList').focus();
        });

        shortcut.add("CTRL+Q", function () {
            $('#QuantityTextBox').focus();
        });

        shortcut.add("ALT+P", function () {
            $('#PriceTextBox').focus();
        });

        shortcut.add("CTRL+D", function () {
            $('#DiscountTextBox').focus();
        });

        shortcut.add("CTRL+R", function () {
            //Refresh Non-Disabled Dropdownlist
            //Persist their values
            initializeAjaxData();
        });

        shortcut.add("CTRL+T", function () {
            $('#TaxTextBox').focus();
        });

        shortcut.add("CTRL+U", function () {
            $('#UnitDropDownList').focus();
        });

        shortcut.add("CTRL+ENTER", function () {
            $('#AddButton').click();
        });
    });

    $().ready(function () {
        initializeAjaxData();
        bounceInfoPanel();
    });

    var bounceInfoPanel = function () {
        var options = {};
        var panel = $("#info-panel");
        panel.effect("bounce", options, 500).delay(2000).effect("fade", options, 500);
    };

    //Called on Ajax Postback caused by ASP.net
    function Page_EndRequest() {
        initializeAjaxData();
    };

    function initializeAjaxData() {
        console.log('Initializing AJAX data.');

        processCallBackActions();

        loadParties();
        $("#PartyDropDownList").change(function () {
            loadAddresses();
        });
        loadAddresses();

        loadItems();
        $("#ItemDropDownList").change(function () {
            loadUnits();
        });

        loadUnits();
        restoreData();
    };

    function processCallBackActions() {
        var itemIdHidden = $("#ItemIdHidden");
        var itemId = parseFloat2(itemIdHidden.val());

        itemIdHidden.val("");

        var itemCode = "";
        var itemCodeHidden = $("#ItemCodeHidden");


        if (itemId > 0) {
            $.ajax({
                type: "POST",
                url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/GetItemCodeByItemId") %>",
                data: "{itemId:'" + itemId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    itemCode = msg.d;
                    itemCodeHidden.val(itemCode);
                },
                error: function (xhr) {
                    var err = eval("(" + xhr.responseText + ")");
                    $.notify(err, "error");
                }
            });
        }


        var partyIdHidden = $("#PartyIdHidden");
        var partyId = parseFloat2(partyIdHidden.val());

        partyIdHidden.val("");

        var partyCode = "";
        var partyCodeHidden = $("#PartyCodeHidden");

        if (partyId > 0) {
            $.ajax({
                type: "POST",
                url: "<%=this.ResolveUrl("~/Services/PartyData.asmx/GetPartyCodeByPartyId") %>",
                data: "{partyId:'" + partyId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    partyCode = msg.d;
                    partyCodeHidden.val(partyCode);
                },
                error: function (xhr) {
                    var err = eval("(" + xhr.responseText + ")");
                    $.notify(err, "error");
                }
            });
        }
    }



    function loadParties() {
        $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/PartyData.asmx/GetParties") %>",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindParties(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("PartyDropDownList", 0, err.Message);
            }
        });
    };

    function loadAddresses() {
        var partyCode = $("#PartyDropDownList").val();

        $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/PartyData.asmx/GetAddressByPartyCode") %>",
            data: "{partyCode:'" + partyCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindAddresses(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("ShippingAddressDropDownList", 0, err.Message);
            }
        });
    };

    function loadItems() {
        $.ajax({

            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/GetItems") %>",
            data: "{tranBook:'<%=this.GetTranBook() %>'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindItems(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("ItemDropDownList", 0, err.Message);
            }
        });
    };

    function loadUnits() {
        var itemCode = $("#ItemCodeHidden").val();
        console.log('Loading units.');

        $.ajax({
            type: "POST",
            url: "<%=this.ResolveUrl("~/Services/ItemData.asmx/GetUnits") %>",
            data: "{itemCode:'" + itemCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindUnits(msg.d);
            },
            error: function (xhr) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("UnitDropDownList", 0, err.Message);
            }
        });
    };


    function addListItem(dropDownListId, value, text) {
        var dropDownList = $("#" + dropDownListId);
        dropDownList.append($("<option></option>").val(value).html(text));
    }

    var selectLocalized = "Select";
    var noneLocalized = "None";

    function bindAddresses(data) {
        $("#ShippingAddressDropDownList").empty();

        if (data.length == 0) {
            addListItem("ShippingAddressDropDownList", "", noneLocalized);
            return;
        }

        addListItem("ShippingAddressDropDownList", "", selectLocalized);

        $.each(data, function () {
            addListItem("ShippingAddressDropDownList", this['Value'], this['Text']);
        });

        $("#ShippingAddressDropDownList").val($("#ShippingAddressCodeHidden").val());
    }

    function bindParties(data) {
        $("#PartyDropDownList").empty();

        if (data.length == 0) {
            addListItem("PartyDropDownList", "", noneLocalized);
            return;
        }

        addListItem("PartyDropDownList", "", selectLocalized);

        $.each(data, function () {
            addListItem("PartyDropDownList", this['Value'], this['Text']);
        });

        $("#PartyCodeTextBox").val($("#PartyCodeHidden").val());
        $("#PartyDropDownList").val($("#PartyCodeHidden").val());
    }

    function bindItems(data) {
        $("#ItemDropDownList").empty();

        if (data.length == 0) {
            addListItem("ItemDropDownList", "", noneLocalized);
            return;
        }

        addListItem("ItemDropDownList", "", selectLocalized);

        $.each(data, function () {
            addListItem("ItemDropDownList", this['Value'], this['Text']);
        });

        $("#ItemCodeTextBox").val($("#ItemCodeHidden").val());
        $("#ItemDropDownList").val($("#ItemCodeHidden").val());
    }

    function bindUnits(data) {
        $("#UnitDropDownList").empty();

        if (data.length == 0) {
            addListItem("UnitDropDownList", "", noneLocalized);
            return;
        }

        addListItem("UnitDropDownList", "", selectLocalized);

        $.each(data, function () {
            addListItem("UnitDropDownList", this['Value'], this['Text']);
        });

        $("#UnitDropDownList").val($("#UnitIdHidden").val());
    }
</script>
