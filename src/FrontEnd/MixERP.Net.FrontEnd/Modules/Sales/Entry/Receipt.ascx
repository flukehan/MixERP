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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Receipt"
    OverridePath="/Modules/Sales/Receipt.mix" %>
<%@ Register TagPrefix="mixerp" Namespace="MixERP.Net.WebControls.Common" Assembly="MixERP.Net.WebControls.Common, Version=1.2.0.1, Culture=neutral, PublicKeyToken=a724a47a0879d02f" %>


<h2>
    <asp:Literal runat="server" ID="TitleLiteral" />
</h2>

<asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<div id="receipt" class="">
    <div class="row ui grid fluid form segment">
        <div class="four wide column">
            <div class="field">
                <label for="DueAmountInputText">
                    <asp:Literal runat="server" ID="TotalDueAmountInBaseCurrencyLiteral" />
                </label>
                <input type="text" id="DueAmountInputText" readonly="readonly" class="currency" />
            </div>
            <div class="field">
                <label for="CurrencyInputText">
                    <asp:Literal runat="server" ID="BaseCurrencyLiteral" />
                </label>
                <input type="text" id="CurrencyInputText" readonly="readonly" class="text-right" />
            </div>

            <div class="field">
                <label for="CurrencySelect">
                    <asp:Literal runat="server" ID="ReceivedCurrencyLiteral" />
                </label>
                <select id="CurrencySelect" class="text-right"></select>
            </div>
            <div class="field">
                <label for="AmountInputText">
                    <asp:Literal runat="server" ID="ReceivedAmountInaboveCurrencyLiteral" />
                </label>
                <input type="text" id="AmountInputText" class="currency" />
            </div>
            <div class="field">
                <label for="DebitExchangeRateInputText">
                    <asp:Literal runat="server" ID="DebitExchangeRateLiteral" />
                </label>
                <input type="text" id="DebitExchangeRateInputText" class="decimal text-right" />
            </div>
            <div class="field">
                <label for="AmountInHomeCurrencyInputText">
                    <asp:Literal runat="server" ID="ConvertedToHomeCurrencyLiteral" />
                </label>
                <input type="text" id="AmountInHomeCurrencyInputText" class="currency" readonly="readonly" />
            </div>

            <div class="field">
                <label for="CreditExchangeRateInputText">
                    <asp:Literal runat="server" ID="CreditExchangeRateLiteral" />
                </label>
                <input type="text" id="CreditExchangeRateInputText" class="decimal text-right" />
            </div>
            <div class="field">
                <label for="BaseAmountInputText">
                    <asp:Literal runat="server" ID="ConvertedToBaseCurrencyLiteral" />
                </label>
                <input type="text" id="BaseAmountInputText" readonly="readonly" class="currency text-right" />
            </div>

            <div class="field">
                <label for="FinalDueAmountInputText">
                    <asp:Literal runat="server" ID="FinalDueAmountInBaseCurrencyLiteral" />
                </label>
                <input type="text" id="FinalDueAmountInputText" readonly="readonly" class="currency text-right" />
            </div>
        </div>
        <div class="four wide column">
            <div class="field">
                <label>
                    <asp:Literal runat="server" ID="ReceiptTypeLiteral" />
                </label>
                <div class="vpad8" id="ReceiptType">

                    <div class="blue ui buttons">
                        <input type="button" class="ui active button" id="CashButton" value="Cash" />
                        <div class="or"></div>
                        <input type="button" class="ui button" id="BankButton" value="Bank" />
                    </div>
                </div>
            </div>

            <div class="field">
                <label for="CostCenterSelect">
                    <asp:Literal runat="server" ID="CostCenterLiteral" />
                </label>
                <select id="CostCenterSelect"></select>
            </div>

            <div id="CashFormGroup" style="padding-bottom: 12px;">
                <div class="field">
                    <label for="CashRepositorySelect">
                        <asp:Literal runat="server" ID="CashRepositoryLiteral" />
                    </label>
                    <select id="CashRepositorySelect"></select>
                </div>
            </div>

            <div id="BankFormGroup" style="display: none; padding-bottom: 12px;">
                <div class="field">
                    <label for="BankSelect">
                        <asp:Literal runat="server" ID="WhichBankLiteral" />
                    </label>
                    <select id="BankSelect"></select>
                </div>

                <div class="field">
                    <label for="PaymentCardSelect">
                        <asp:Literal runat="server" ID="PaymentCardLiteral" />
                    </label>
                    <select id="PaymentCardSelect"></select>
                </div>
                <div class="two fields">
                    <div class="field">
                        <label for="MerchantFeeInputText">
                            <asp:Literal ID="MerchantFeeLiteral" runat="server" />

                        </label>
                        <input type="text" id="MerchantFeeInputText" readonly="readonly" />
                    </div>
                    <div class="field">
                        <label>
                            <asp:Literal runat="server" ID="CustomerPaysFeesLiteral" />
                        </label>
                        <div class="ui checkbox">
                            <input type="radio" disabled="disabled" id="CustomerPaysFeeRadio" />
                            <label>
                                <asp:Literal ID="YesLiteral" runat="server" />
                            </label>
                        </div>
                    </div>
                </div>
                <div class="field">
                    <label for="PostedDateTextBox">
                        <asp:Literal runat="server" ID="PostedDateLiteral" />
                    </label>

                    <mixerp:DateTextBox ID="PostedDateTextBox" runat="server"
                        CssClass="date"
                        Required="true"
                        AssociatedControlId="Trigger1" />
                </div>
                <div class="field">
                    <label for="InstrumentCodeInputText">
                        <asp:Literal runat="server" ID="InstrumentCodeLiteral" />
                    </label>
                    <input type="text" id="InstrumentCodeInputText" />
                </div>

                <div class="field">
                    <label for="TransactionCodeInputText">
                        <asp:Literal runat="server" ID="BankTransactionCodeLiteral" />
                    </label>
                    <input type="text" id="TransactionCodeInputText" />
                </div>
            </div>

            <div class="field">
                <label for="ReferenceNumberInputText">
                    <asp:Literal runat="server" ID="ReferenceNumberLiteral" />
                </label>
                <input type="text" id="ReferenceNumberInputText" />
            </div>

            <div class="field">
                <label for="StatementReferenceTextArea">
                    <asp:Literal runat="server" ID="StatementReferenceLiteral" />

                </label>
                <textarea id="StatementReferenceTextArea" rows="3"></textarea>
            </div>
            <button type="button" id="SaveButton" class="ui small submit button green">
                <asp:Literal runat="server" ID="SaveLiteral" />
            </button>
        </div>
    </div>
</div>

<script type="text/javascript">
    var exchangeRateLocalized = "<%= this.ExchangeRateLocalized() %>";
</script>
<script src="../Scripts/Entry/Receipt.js"></script>
