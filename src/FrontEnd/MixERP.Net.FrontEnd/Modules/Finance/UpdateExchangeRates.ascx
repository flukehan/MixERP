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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateExchangeRates.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.UpdateExchangeRates" %>
<style type="text/css">
    #ExchangeRatesGridView th:nth-child(5) {
        width: 125px;
    }

    #ExchangeRatesGridView th:nth-child(6) {
        width: 250px;
    }
</style>
<h1>Update Exchange Rates</h1>
<div class="ui divider"></div>

<div class="ui form">
    <div class="inline fields">
        <div class="field">
            <label for="OfficeInputText">
                Office
            </label>
            <input type="text" id="OfficeInputText" readonly="readonly" runat="server" />
        </div>
        <div class="field">
            <label for="CurrencyInputText">
                Base Currency
            </label>
            <input type="text" id="CurrencyInputText" readonly="readonly" runat="server" />
        </div>
    </div>
</div>

<table class="ui very compact small striped collapsing table" id="ExchangeRatesGridView">
    <thead>
        <tr>
            <th>Currency Code</th>
            <th>Currency Symbol</th>
            <th>Currency Name</th>
            <th>Hundredth Name</th>
            <th>Exchange Rate</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
    </tbody>
</table>

<script type="text/javascript">
    var exchangeRatesGridView = $("#ExchangeRatesGridView");
    var currencyInputText = $("#CurrencyInputText");
    var url;
    var data;

    $(document).ready(function () {
        GetCurrencies();
    });

    function BindCurrencies(currencies) {

        $(currencies).each(function () {
            AddRow(this.CurrencyCode, this.CurrencySymbol, this.CurrencyName, this.HundredthName);
        });
    };

    function AddRow(currencyCode, currencySymbol, currencyName, hundredthName) {
        exchangeRatesGridView.find("tbody").append(String.format("<tr class='ui form'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><input type='text' id='{0}ExchangRateInputText' class='text-right currency' onblur='displayDescription(this);' /></td><td></td></tr>", currencyCode, currencySymbol, currencyName, hundredthName));
    };

    function GetCurrencies() {
        var ajaxGetExchangeCurrencies = GetExchangeCurrencies();

        ajaxGetExchangeCurrencies.success(function (msg) {
            BindCurrencies(msg.d);
        });

        ajaxGetExchangeCurrencies.fail(function (xhr) {
            alert(xhr.responseText);
        });

    };

    function displayDescription(element) {
        var exchangeRate = parseFloat2($(element).val());

        if (!exchangeRate) {
            return;
        };

        var descriptionCell = $(element).parent().parent().find("td:nth-child(6)");
        var currencyCode = $(element).parent().parent().find("td:nth-child(1)").html();
        var exchangeRateReverse = (1.00 / exchangeRate).toFixed(4);

        var description = exchangeRate + " " + currencyInputText.val() + " = 1 " + currencyCode + " (" + exchangeRateReverse + " " + currencyCode + " = 1 " + currencyInputText.val() + ")";

        descriptionCell.html(description);
    };

    function GetExchangeCurrencies() {
        url = "/Modules/Finance/Services/CurrencyData.asmx/GetExchangeCurrencies";

        return getAjax(url);
    };
</script>