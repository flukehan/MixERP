<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductControl" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<div style="margin: 0;">
    <div id="info-panel">
        <asp:Literal runat="server" Text="<%$Resources:Titles,ShortCuts %>"></asp:Literal>
        <br />
        <hr class="hr" style="border-color: #97d300;" />
        <table>
            <tr>
                <td>F2
                </td>
                <td>
                    <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewParty %>"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>F4
                </td>
                <td>
                    <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewItem %>"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>CTRL + RET
                </td>
                <td>
                    <asp:Literal runat="server" Text="<%$Resources:Titles,AddNewRow%>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>


    <asp:Label ID="TitleLabel" CssClass="title" runat="server" />

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
                        ToolTip="F2" />
                    <asp:DropDownList ID="PartyDropDownList" runat="server" Width="150"
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
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltC%>" />'
                            style="width: 60px;" />
                    </td>
                    <td>
                        <select name="ItemDropDownList"
                            id="ItemDropDownList"
                            style="width: 300px;"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlI%>" />'>
                        </select>
                    </td>
                    <td>
                        <input type="text"
                            id="QuantityTextBox"
                            class="right"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlQ%>" />'
                            style="width: 50px;"
                            value="1" />
                    </td>
                    <td>
                        <select name="UnitDropDownList"
                            id="UnitDropDownList"
                            style="width: 70px;"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlU%>" />'>
                        </select>
                    </td>
                    <td>
                        <input type="text"
                            id="PriceTextBox"
                            class="right number"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,AltP%>" />'
                            style="width: 65px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="AmountTextBox"
                            readonly="readonly"
                            class="right number"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="DiscountTextBox"
                            class="right number"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlD%>" />'
                            style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="SubTotalTextBox"
                            readonly="readonly"
                            class="right number"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TaxRateTextBox"
                            class="right"
                            style="width: 40px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TaxTextBox"
                            class="right number"
                            title='<asp:Literal runat="server" Text="<%$Resources:Titles,CtrlT%>" />'
                            style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text"
                            id="TotalTextBox"
                            readonly="readonly"
                            class="right number"
                            style="width: 70px;" />
                    </td>
                    <td>
                        <input type="button"
                            id="AddButton"
                            class="button"
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
                        <asp:DropDownList ID="ShippingAddressDropDownList" runat="server" Width="200">
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
    var isSales = ("<%= this.Book %>" == "Sales");
    var tranBook = "<%=this.GetTranBook() %>";
    var taxAfterDiscount = "<%=Switches.TaxAfterDiscount().ToString()%>";
    var verifyStock = ("<%= this.VerifyStock %>" == "True");


    var areYouSureLocalized = "<%=Resources.Questions.AreYouSure %>";
    var selectLocalized = "<%=Resources.Titles.Select %>";
    var noneLocalized = "<%=Resources.Titles.None %>";
    var insufficientStockWarningLocalized = "<%= Resources.Warnings.InsufficientStockWarning %>";

</script>

<script src="../../Scripts/UserControls/ProductControl.js"></script>