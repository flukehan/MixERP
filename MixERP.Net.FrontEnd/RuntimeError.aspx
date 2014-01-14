<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="RuntimeError.aspx.cs" Inherits="MixERP.Net.FrontEnd.RuntimeError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1>Error Occurred</h1>
    <hr class="hr" />

    <div class="error">I tried my best to complete the task, but it failed miserably.</div>
    <br />

    <p>You should notify the system administrator if this is an urgent issue. Nonetheless, the exception has been logged.</p>
    <br />

    <asp:Literal ID="ExceptionLiteral" runat="server" />
    <br />

    <a class="menu" href="javascript:history.go(-1);">Go Back to the Previous Page</a>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
