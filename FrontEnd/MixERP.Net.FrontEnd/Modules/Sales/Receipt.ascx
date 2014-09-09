<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Receipt" %>

<h3>Receipt from Customer</h3>

<mixerp:PartyControl runat="server" />

<div id="receipt" class="panel-body table-bordered">
    <div role="form" style="max-width: 300px;">

        <div class="form-group form-group-sm">
            <label for="DueAmountTextBox">Total Due Amount (In Base Currency)</label>
            <input type="text" id="DueAmountTextBox" readonly="readonly" class="currency form-control" />
        </div>
        <div class="form-group form-group-sm">
            <label for="CurrencyTextBox">Base Currency</label>
            <input type="text" id="CurrencyTextBox" readonly="readonly" class="currency form-control" />
        </div>

        <div class="form-group form-group-sm">
            <label for="CurrencyDropDownList">Received Currency</label>
            <select id="CurrencyDropDownList" class="form-control"></select>
        </div>

        <div class="form-group form-group-sm">
            <label for="AmountTextBox">Received Amount (In above Currency)</label>
            <input type="text" id="AmountTextBox" class="currency form-control" />
        </div>
        <div class="form-group form-group-sm">
            <label for="ExchangeRateTextBox">Exchange Rate</label>
            <input type="text" id="ExchangeRateTextBox" class="float form-control" />
        </div>
        <div class="form-group form-group-sm">
            <label for="BaseAmountTextBox">Converted to Base Currency</label>
            <input type="text" id="BaseAmountTextBox" readonly="readonly" class="currency form-control" />
        </div>

        <div class="form-group form-group-sm">
            <label for="FinalDueAmountTextBox">Final Due Amount in Base Currency</label>
            <input type="text" id="FinalDueAmountTextBox" readonly="readonly" class="currency form-control" />
        </div>

        <div class="form-group form-group-sm">
            <label>Receipt Type</label>
            <div class="vpad8" id="ReceiptType">
                <div class="btn-group btn-group-sm" data-toggle="buttons">
                    <label class="btn btn-success active" onclick="toggleTransactionType($(this));repaint();">
                        <input type="radio" name="options" id="CashRadio">
                        Cash
                    </label>
                    <label class="btn btn-success" onclick="toggleTransactionType($(this));repaint();">
                        <input type="radio" name="options" id="BankRadio">
                        Bank
                    </label>
                </div>
            </div>
        </div>

        <div id="CashFormGroup">
            <div class="form-group form-group-sm">
                <label for="CashRepositoryDropDownList">Cash Repository</label>
                <select id="CashRepositoryDropDownList" class="form-control"></select>
            </div>
        </div>

        <div id="BankFormGroup" style="display: none;">
            <div class="form-group form-group-sm">
                <label for="BankDropDownList">Which Bank?</label>
                <select id="BankDropDownList" class="form-control"></select>
            </div>

            <div class="form-group form-group-sm">
                <label for="PostedDateTextBox">Posted Date</label>
                <input type="text" id="PostedDateTextBox" class="form-control date" />
            </div>
            <div class="form-group form-group-sm">
                <label for="InstrumentCodeTextBox">Bank Instrument Code</label>
                <input type="text" id="InstrumentCodeTextBox" class="form-control" />
            </div>

            <div class="form-group form-group-sm">
                <label for="TransactionCodeTextBox">Bank Transaction Code</label>
                <input type="text" id="TransactionCodeTextBox" class="form-control" />
            </div>
        </div>

        <div class="form-group">
            <label for="StatementReferenceTextBox">Statement Reference</label>
            <textarea class="form-control" rows="3"></textarea>
        </div>

        <button type="button" id="SaveButton" class="btn btn-default btn-sm">Save</button>
    </div>
</div>

<script type="text/javascript">
        $(document).ready(function () {
            $("#receipt").appendTo("#home");
        });

        var toggleTransactionType = function (e) {

            if (e.find("input").attr("id") == "BankRadio") {
                if (!$("#BankFormGroup").is(":visible"));
                {
                    $("#BankFormGroup").show(500);
                    $("#CashFormGroup").hide();
                    return;
                }
            };

            if (e.find("input").attr("id") == "CashRadio") {
                if (!$("#CashFormGroup").is(":visible"));
                {
                    $("#CashFormGroup").show(500);
                    $("#BankFormGroup").hide();
                    return;
                }
            };

        };
</script>