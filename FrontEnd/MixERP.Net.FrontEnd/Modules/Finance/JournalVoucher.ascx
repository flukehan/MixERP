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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalVoucher.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.JournalVoucher" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<div style="width: 1500px; overflow: auto;">
    <asp:GridView
        runat="server"
        ID="TransactionGridView"
        GridLines="None"
        AutoGenerateColumns="False"
        CssClass="ui table nowrap"
        OnRowDataBound="TransactionGridView_RowDataBound"
        OnDataBound="TransactionGridView_DataBound">
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <i class="icon list layout" onclick="showCheckList(this);"></i>
                    <i class="icon print" onclick="showPreview(this);"></i>
                    <i class="icon grid layout" onclick="showStockDetail(this);"></i>
                    <i class="icon chevron circle up" onclick="window.scroll(0);"></i>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <div class="ui toggle checkbox">
                        <input type="checkbox" />
                        <label></label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<asp:HiddenField runat="server" ID="SelectedValuesHidden"></asp:HiddenField>

<asp:PlaceHolder runat="server" ID="FlagPlaceholder"></asp:PlaceHolder>

<script src="Scripts/JournalVoucher.js"></script>