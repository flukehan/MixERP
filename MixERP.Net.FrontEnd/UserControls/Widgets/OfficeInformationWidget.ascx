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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfficeInformationWidget.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Widgets.OfficeInformationWidget" %>
<div class="panel panel-default widget">
    <div class="panel-heading">
        <h3 class="panel-title">Office Information (Todo)</h3>
    </div>
    <div class="panel-body">
        Your Office : PES-NY-MEM (Memphis Branch)
                    <br />
        Logged in to : PES-NY-BK (Brooklyn Branch)
                    <br />
        Last Login IP : 192.168.0.200
                <br />
        Last Login On : <%=DateTime.Now.ToString(MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture()) %>
        <br />
        Current Login IP : 192.168.0.200
                <br />
        Current Login On: <%=DateTime.Now.ToString(MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrentCulture()) %>
        <br />
        Role : ADM (Administrators)
                    <br />
        Department : ITD (IT Department)
    </div>
</div>
