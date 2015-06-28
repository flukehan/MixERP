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
    <div class="fields">
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

        <div class="field">
            <label for="OfficeInputText">
                Select API
            </label>
            <select id="ModuleSelect" class="ui dropdown"></select>
        </div>
        <div class="field">
            <label for="RequestButton">Request</label>
            <input type="button" id="RequestButton" value="Request" class="ui pink button" />
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

<input type="button" id="SaveButton" value="Save" class="ui green button" />

<script src="Scripts/UpdateExchangeRates.js"></script>