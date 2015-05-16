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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountStatement.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.AccountStatement" %>
<%@ Register TagPrefix="mixerp" Namespace="MixERP.Net.WebControls.Common" Assembly="MixERP.Net.WebControls.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a724a47a0879d02f" %>

<style type="text/css">
    #AccountOverViewGrid td:nth-child(1),
    #AccountOverViewGrid th:nth-child(1) {
        width: 200px;
    }
</style>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<div class="ui small modal" id="ReconcileModal">
    <i class="close icon"></i>
    <div class="header">
        <i class="circle notched icon"></i>
        Reconcile
    </div>
    <div class="content">
        <div class="ui form">
            <div class="two fields">
                <div class="field">
                    <label for="TranCodeInputText">Tran Code</label>
                    <input type="text" disabled="" id="TranCodeInputText" />
                </div>
                <div class="field">
                    <label for="CurrentBookDateInputText">Current Book Date</label>
                    <input type="text" disabled="" id="CurrentBookDateInputText" />
                </div>
            </div>
            <div class="field">
                <label>New Book Date</label>
                <div class="three fields">
                    <div class="field">
                        <input id="YearInputText" placeholder="Year" type="text" class="integer">
                    </div>
                    <div class="field">
                        <input id="MonthInputText" placeholder="Month" type="text" class="integer">
                    </div>
                    <div class="field">
                        <input id="DayInputText" placeholder="Day" type="text" class="integer">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="actions">
        <div class="ui red button">Cancel</div>
        <div class="ui green button">OK</div>
    </div>
</div>

<script src="/Modules/Finance/Scripts/Reports/AccountStatement.js"></script>
