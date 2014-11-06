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
    OverridePath="~/Modules/Sales/Receipt.mix" %>

<h3>Receipt from Customer</h3>

<asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

<mixerp:PartyControl runat="server" />

<div id="receipt" class="">
    <div class="row ui grid fluid form segment">
        <div class="four wide column">
            <div class="field">
                <label for="DueAmountInputText">Total Due Amount (In Base Currency)</label>
                <input type="text" id="DueAmountInputText" readonly="readonly" class="currency" />
            </div>
            <div class="field">
                <label for="CurrencyInputText">Base Currency</label>
                <input type="text" id="CurrencyInputText" readonly="readonly" class="text-right" />
            </div>

            <div class="field">
                <label for="CurrencySelect">Received Currency</label>
                <select id="CurrencySelect" class="text-right"></select>
            </div>
            <div class="field">
                <label for="AmountInputText">Received Amount (In above Currency)</label>
                <input type="text" id="AmountInputText" class="currency" />
            </div>
            <div class="field">
                <label for="DebitExchangeRateInputText">Exchange Rate</label>
                <input type="text" id="DebitExchangeRateInputText" class="decimal text-right" />
            </div>
            <div class="field">
                <label for="AmountInHomeCurrencyInputText">Converted to Home Currency</label>
                <input type="text" id="AmountInHomeCurrencyInputText" class="currency" readonly="readonly" />
            </div>

            <div class="field">
                <label for="CreditExchangeRateInputText">Exchange Rate</label>
                <input type="text" id="CreditExchangeRateInputText" class="decimal text-right" />
            </div>
            <div class="field">
                <label for="BaseAmountInputText">Converted to Base Currency</label>
                <input type="text" id="BaseAmountInputText" readonly="readonly" class="currency text-right" />
            </div>

            <div class="field">
                <label for="FinalDueAmountInputText">Final Due Amount in Base Currency</label>
                <input type="text" id="FinalDueAmountInputText" readonly="readonly" class="currency text-right" />
            </div>

            <button type="button" id="SaveButton" class="ui small submit button green">Save</button>
        </div>
        <div class="four wide column">
            <div class="field">
                <label>Receipt Type</label>
                <div class="vpad8" id="ReceiptType">

                    <div class="blue ui buttons">
                        <input type="button" class="ui active button" id="CashButton" value="Cash" />
                        <div class="or"></div>
                        <input type="button" class="ui button" id="BankButton" value="Bank" />
                    </div>
                </div>
            </div>

            <div class="field">
                <label for="CostCenterSelect">Cost Center</label>
                <select id="CostCenterSelect"></select>
            </div>

            <div id="CashFormGroup">
                <div class="field">
                    <label for="CashRepositorySelect">Cash Repository</label>
                    <select id="CashRepositorySelect"></select>
                </div>
            </div>

            <div id="BankFormGroup" style="display: none;">
                <div class="field">
                    <label for="BankSelect">Which Bank?</label>
                    <select id="BankSelect"></select>
                </div>

                <div class="field">
                    <label for="PostedDateTextBox">Posted Date</label>

                    <mixerp:DateTextBox ID="PostedDateTextBox" runat="server"
                        CssClass="date"
                        Required="true"
                        AssociatedControlId="Trigger1" />
                </div>
                <div class="field">
                    <label for="InstrumentCodeInputText">Instrument Code</label>
                    <input type="text" id="InstrumentCodeInputText" />
                </div>

                <div class="field">
                    <label for="TransactionCodeInputText">Bank Transaction Code</label>
                    <input type="text" id="TransactionCodeInputText" />
                </div>
            </div>

            <div class="field">
                <label for="ReferenceNumberInputText">Reference Number</label>
                <input type="text" id="ReferenceNumberInputText" />
            </div>
            <div class="field">
                <label for="StatementReferenceTextArea">Statement Reference</label>
                <textarea id="StatementReferenceTextArea" rows="3"></textarea>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var exchangeRateLocalized = "<%= this.ExchangeRateLocalized() %>";
</script>
<script src="../Scripts/Entry/Receipt.js"></script>
