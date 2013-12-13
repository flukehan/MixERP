<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductControl" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<div style="width: 1000px; overflow: hidden; margin: 0;">
    <asp:Label ID="TitleLabel" CssClass="title" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="ajax-container">
                <img runat="server" alt="progress" src="~/spinner.gif" class="ajax-loader" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="CashRepositoryDropDownList" />
            <asp:AsyncPostBackTrigger ControlID="ProductGridView" />
            <asp:AsyncPostBackTrigger ControlID="OkButton" />
            <asp:AsyncPostBackTrigger ControlID="CancelButton" />
            <asp:AsyncPostBackTrigger ControlID="ShippingChargeTextBox" />
            <asp:PostBackTrigger ControlID="SaveButton" />
        </Triggers>
        <ContentTemplate>
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
                                onchange="if(this.selectedIndex == 0) { return false };document.getElementById('PartyCodeTextBox').value = this.options[this.selectedIndex].value;"
                                ToolTip="F2">
                            </asp:DropDownList>
                            <asp:HiddenField ID="PartyHidden" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="PriceTypeDropDownList" runat="server" Width="80">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="ReferenceNumberTextBox" runat="server" Width="60" MaxLength="24" />
                        </td>
                        <td>
                            <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:Titles, OK %>" Width="40" OnClick="OkButton_Click" />
                            <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:Titles, Cancel %>" Width="50" Enabled="false" OnClick="CancelButton_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <p>
                <asp:Label ID="ErrorLabelTop" runat="server" CssClass="error" />
            </p>
            <div class="center">
                <asp:GridView ID="ProductGridView" runat="server" EnableTheming="False"
                    CssClass="grid2 grid3" ShowHeaderWhenEmpty="true"
                    AlternatingRowStyle-CssClass="grid2-row-alt"
                    OnRowDataBound="ProductGridView_RowDataBound"
                    OnRowCommand="ProductGridView_RowCommand"
                    AutoGenerateColumns="False"
                    ShowFooter="true">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="<%$ Resources:Titles, Code %>" />
                        <asp:BoundField DataField="ItemName" HeaderText="<%$ Resources:Titles, ItemName %>" />
                        <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources:Titles, QuantityAbbreviated %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" />
                        <asp:BoundField DataField="Unit" HeaderText="<%$ Resources:Titles, Unit %>" />
                        <asp:BoundField DataField="Price" HeaderText="<%$ Resources:Titles, Price %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="Amount" HeaderText="<%$ Resources:Titles, Amount %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="Discount" HeaderText="<%$ Resources:Titles, Discount %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="SubTotal" HeaderText="<%$ Resources:Titles, SubTotal %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="Rate" HeaderText="<%$ Resources:Titles, Rate %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="Tax" HeaderText="<%$ Resources:Titles, Tax %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Titles, Total %>" ItemStyle-CssClass="right" HeaderStyle-CssClass="right" DataFormatString="{0:N}" />
                        <asp:TemplateField ShowHeader="False" HeaderText="<%$ Resources:Titles, Action %>">
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteImageButton" ClientIDMode="Predictable" runat="server"
                                    CausesValidation="false"
                                    OnClientClick="return(confirmAction());"
                                    ImageUrl="~/Resource/Icons/delete-16.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="grid2-row-alt" />
                    <FooterStyle CssClass="grid2-footer" />
                    <HeaderStyle CssClass="grid2-header" />
                    <RowStyle CssClass="grid2-row" />
                </asp:GridView>
                <%--                <asp:HiddenField ID="UnitNameHidden" runat="server" />
                <asp:HiddenField ID="UnitIdHidden" runat="server" />--%>

                <asp:Panel ID="FormPanel" runat="server" Enabled="false">
                    <asp:Label ID="ErrorLabel" runat="server" CssClass="error" />
                </asp:Panel>
                <div class="vpad8"></div>
                <asp:Panel ID="BottomPanel" CssClass="form" runat="server" Width="600px" Enabled="false">
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
                                <asp:TextBox ID="ShippingChargeTextBox" runat="server" AutoPostBack="true" CssClass="number" OnTextChanged="ShippingChargeTextBox_TextChanged" Width="100px">
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
                            </asp:TableCell><asp:TableCell>
                                <asp:Button ID="SaveButton" runat="server" CssClass="button" Text="<%$Resources:Titles, Save %>" OnClick="SaveButton_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <p>
                    <asp:Label ID="ErrorLabelBottom" runat="server" CssClass="error" />
                </p>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AjaxPageLoadHandler);
    });

    function AjaxPageLoadHandler(sender, args) {
        showShippingAddress();
    }

    var getPrice = function () {
        var itemDropDownList = $('#ItemDropDownList');
        var unitDropDownList = $('#UnitDropDownList');

        if (unitDropDownList.val()) {
            triggerChange(unitDropDownList.attr('id'));
        }
    }



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
    }

    var updateTax = function () {
        var taxRateTextBox = $("#TaxRateTextBox");
        var taxTextBox = $("#TaxTextBox");
        var priceTextBox = $("#PriceTextBox");
        var discountTextBox = $("#DiscountTextBox");
        var quantityTextBox = $("#QuantityTextBox");

        var total = parseFloat2(priceTextBox.val()) * parseFloat2(quantityTextBox.val());
        var subTotal = total - parseFloat2(discountTextBox.val());
        var taxableAmount = total;
        var taxAfterDiscount = "<%=MixERP.Net.Common.Helpers.Switches.TaxAfterDiscount().ToString()%>";

        if (taxAfterDiscount.toLowerCase() == "true") {
            taxableAmount = subTotal;
        }

        var tax = (taxableAmount * parseFloat2(parseFormattedNumber(taxRateTextBox.val()))) / 100;

        if (parseFloat2(tax).toFixed(2) != parseFloat2(parseFormattedNumber(taxTextBox.val())).toFixed(2)) {
            var question = confirm(localizedUpdateTax);
            if (question) {
                if (tax.toFixed) {
                    taxTextBox.val(getFormattedNumber(tax.toFixed(2)));
                }
                else {
                    taxTextBox.val(getFormattedNumber(tax));
                }
            }
        }
    }

    var showShippingAddress = function () {
        $('#ShippingAddressTextBox').val(($('#ShippingAddressDropDownList').val()));
    }


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
            var url = "/Items/Setup/PartiesPopup.aspx";
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
    });

    //Called on Ajax Postback caused by ASP.net
    function Page_EndRequest(sender, args) {
        initializeAjaxData();
    }

    function initializeAjaxData() {
        loadAddresses();
        loadUnits();

        $("#PartyDropDownList").change(function () {
            loadAddresses();
        });

        $("#ItemDropDownList").change(function () {
            loadUnits();
        });
    }

    function loadAddresses() {
        var partyCode = $("#PartyDropDownList").val();
        $("#ShippingAddressDropDownList").empty();

        $.ajax({
            type: "POST",
            url: "<%=ResolveUrl("~/Services/PartyData.asmx/GetAddressByPartyCode") %>",
            data: "{partyCode:'" + partyCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindAddresses(msg.d)
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("ShippingAddressDropDownList", 0, err.Message);
            }
        });
    }

    function loadUnits() {
        var itemCode = $("#ItemDropDownList").val();
        $("#UnitDropDownList").empty();

        $.ajax({
            type: "POST",
            url: "<%=ResolveUrl("~/Services/ItemData.asmx/GetUnits") %>",
            data: "{itemCode:'" + itemCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                bindUnits(msg.d)
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                addListItem("UnitDropDownList", 0, err.Message);
            }
        });
    }

    function addListItem(dropDownListId, value, text) {
        var dropDownList = $("#" + dropDownListId);
        dropDownList.append($("<option></option>").val(value).html(text));
    }

    var selectLocalized = "Select";
    var noneLocalized = "None";

    function bindAddresses(data) {
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

    function bindUnits(data) {
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
