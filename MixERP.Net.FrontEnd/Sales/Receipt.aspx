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
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.Receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h3>Receipt from Customer</h3>
    <mixerp:Party runat="server" />
    <%--core.party_user_control_view--%>
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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
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
</asp:Content>
