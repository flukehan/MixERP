<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Entry.Receipt"
    OverridePath="~/Modules/Sales/Receipt.mix" %>

<h3>Receipt from Customer</h3>

<asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

<mixerp:PartyControl runat="server" />

<div id="receipt" class="panel-body table-bordered">
    <div role="form">

        <div class="row">
            <div class="col-md-4">
                <div class="form-group form-group-sm">
                    <label for="DueAmountTextBox">Total Due Amount (In Base Currency)</label>
                    <input type="text" id="DueAmountTextBox" readonly="readonly" class="currency form-control input-sm" />
                </div>
                <div class="form-group form-group-sm">
                    <label for="CurrencyTextBox">Base Currency</label>
                    <input type="text" id="CurrencyTextBox" readonly="readonly" class="form-control  input-sm text-right" />
                </div>

                <div class="form-group form-group-sm">
                    <label for="CurrencyDropDownList">Received Currency</label>
                    <select id="CurrencyDropDownList" class="form-control  text-right  input-sm"></select>
                </div>
                <div class="form-group form-group-sm">
                    <label for="AmountTextBox">Received Amount (In above Currency)</label>
                    <input type="text" id="AmountTextBox" class="currency form-control  input-sm" />
                </div>
                <div class="form-group form-group-sm">
                    <label for="DebitExchangeRateTextBox">Exchange Rate</label>
                    <input type="text" id="DebitExchangeRateTextBox" class="float form-control  input-sm text-right" />
                </div>
                <div class="form-group form-group-sm">
                    <label for="AmountInHomeCurrencyTextBox">Converted to Home Currency</label>
                    <input type="text" id="AmountInHomeCurrencyTextBox" class="currency form-control  input-sm" readonly="readonly" />
                </div>

                <div class="form-group form-group-sm">
                    <label for="CreditExchangeRateTextBox">Exchange Rate</label>
                    <input type="text" id="CreditExchangeRateTextBox" class="float form-control  input-sm text-right" />
                </div>
                <div class="form-group form-group-sm">
                    <label for="BaseAmountTextBox">Converted to Base Currency</label>
                    <input type="text" id="BaseAmountTextBox" readonly="readonly" class="currency form-control  input-sm text-right" />
                </div>

                <div class="form-group form-group-sm">
                    <label for="FinalDueAmountTextBox">Final Due Amount in Base Currency</label>
                    <input type="text" id="FinalDueAmountTextBox" readonly="readonly" class="currency form-control  input-sm text-right" />
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group form-group-sm">
                    <label>Receipt Type</label>
                    <div class="vpad8" id="ReceiptType">
                        <div class="btn-group btn-group-sm" data-toggle="buttons">
                            <label class="btn btn-success active">
                                <input type="radio" name="options" id="CashRadio">
                                Cash
                            </label>
                            <label class="btn btn-success">
                                <input type="radio" name="options" id="BankRadio">
                                Bank
                            </label>
                        </div>
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label for="CostCenterDropDownList">Cost Center</label>
                    <select id="CostCenterDropDownList" class="form-control input-sm"></select>
                </div>

                <div id="CashFormGroup">
                    <div class="form-group form-group-sm">
                        <label for="CashRepositoryDropDownList">Cash Repository</label>
                        <select id="CashRepositoryDropDownList" class="form-control input-sm"></select>
                    </div>
                </div>

                <div id="BankFormGroup" style="display: none;">
                    <div class="form-group form-group-sm">
                        <label for="BankDropDownList">Which Bank?</label>
                        <select id="BankDropDownList" class="form-control input-sm"></select>
                    </div>

                    <div class="form-group form-group-sm">
                        <label for="PostedDateTextBox">Posted Date</label>

                        <div class="input-group">
                            <mixerp:DateTextBox ID="PostedDateTextBox" runat="server"
                                CssClass="date form-control input-sm"
                                Mode="Today"
                                Required="true"
                                AssociatedControlId="Trigger1" />
                            <span class="input-group-addon" onclick="$('#PostedDateTextBox').datepicker('show');">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <label for="InstrumentCodeTextBox">Instrument Code</label>
                        <input type="text" id="InstrumentCodeTextBox" class="form-control  input-sm" />
                    </div>

                    <div class="form-group form-group-sm">
                        <label for="TransactionCodeTextBox">Bank Transaction Code</label>
                        <input type="text" id="TransactionCodeTextBox" class="form-control  input-sm" />
                    </div>
                </div>

                <div class="form-group form-group-sm">
                    <label for="ReferenceNumberTextBox">Reference Number</label>
                    <input type="text" id="ReferenceNumberTextBox" class="form-control  input-sm" />
                </div>
                <div class="form-group">
                    <label for="StatementReferenceTextBox">Statement Reference</label>
                    <textarea class="form-control  input-sm" rows="3"></textarea>
                </div>
            </div>
        </div>

        <button type="button" id="SaveButton" class="btn btn-default btn-sm">Save</button>
    </div>
</div>

<script type="text/javascript">
    var exchangeRateLocalized = "<%= this.ExchangeRateLocalized() %>";
</script>
<script src="../Scripts/Entry/Receipt.js"></script>