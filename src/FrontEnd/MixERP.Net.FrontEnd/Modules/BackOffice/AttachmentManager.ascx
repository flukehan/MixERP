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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentManager.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.AttachmentManager" %>
<style>
    @media screen and (min-width: 768px) {
        .modal-wide .modal-dialog {
            width: 700px;
        }
    }
</style>

<div class="ui massive teal header">Upload Attachments</div>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<div class="ui massive teal header">
    <asp:Literal ID="TitleLiteral" runat="server"/>
</div>

<div id="images">
</div>

<!-- Modal -->
<div class="ui large modal" id="opener">
    <i class="close icon"></i>
    <div class="ui teal header">
    </div>
    <div class="content">

        <div class="ui segment">
            <img src="/" alt="" />
            <p class="vpad8"></p>
        </div>

        <div class="actions">
            <div class="ui teal button">
                Okay
            </div>
        </div>
    </div>
</div>
<script src="Scripts/AttachmentManager.ascx.js"></script>