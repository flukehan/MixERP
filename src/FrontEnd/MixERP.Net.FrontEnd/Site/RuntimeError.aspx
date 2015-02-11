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

<%@ Page Title="" Language="C#" MasterPageFile="~/MixERPMaster.Master" AutoEventWireup="true" CodeBehind="RuntimeError.aspx.cs" Inherits="MixERP.Net.FrontEnd.Site.RuntimeError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h3>Damn<em>!</em> This Went Completely Wrong. O.O</h3>
    <hr />
    <h4 class="exception">Man, this <em>literally</em> sucks! Listen, I really tried my best to <em>get the job done</em>, but it <em>failed miserably</em>. :O</h4>
    <asp:Literal ID="ExceptionLiteral" runat="server" />
    <hr />

    <h4>So, what went wrong was logged into the repository. You should notify the admin, case you feel this, an urgent issue.</h4>
    <br />
    <a class="btn btn-danger btn-sm" href="javascript:history.go(-1);">Go Back to the Previous Page</a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>