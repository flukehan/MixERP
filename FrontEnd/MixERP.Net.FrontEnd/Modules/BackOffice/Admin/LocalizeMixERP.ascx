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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalizeMixERP.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Admin.LocalizeMixERP" %>
<h2 class="ui green header">Localize MixERP</h2>

<style type="text/css">
    #LocalizationGridView th:nth-child(1) { width: 10%; }

    #LocalizationGridView th:nth-child(2) { width: 20%; }

    #LocalizationGridView th:nth-child(3) { width: 20%; }
</style>

<div class="ui yellow icon message">
    <i class="users icon"></i>
    <div class="content">
        <div class="header">
            Community Contribution
        </div>
        <p>
            Please read the <a target="_blank" href="http://mixerp.org/cla.html">Contributor License Agreement</a> before proceeding.
        </p>
    </div>
    <i class="close icon"></i>
</div>


<div class="ui teal button">Initialize Resources</div>
<div class="ui large flowing popup">
    <div class="vpad8">
        You can start translating MixERP as soon as you initialize the resources.
    </div>
    <asp:Button
        ID="LetsDoThatNowButton"
        Text="Let's Do That Now"
        OnClick="LetsDoThatNowButton_OnClick"
        OnClientClick=" $(this).addClass('loading'); "
        CssClass="ui primary button"
        runat="server"/>
</div>


<div class="ui positive button">Culture</div>
<div class="ui flowing popup" style="width: 400px;">
    <div class="ui large form">
        <div class="field">
            <label for="CultureSelect">Select Culture</label>
            <asp:DropDownList ID="CultureSelect" runat="server"></asp:DropDownList>
        </div>

        <asp:Button type="button" ID="ShowButton" OnClick="ShowButton_Click" CssClass="ui positive button" Text="Show" runat="server"/>
    </div>
</div>

<div class="ui primary button">Download the Translation</div>

<div class="ui orange button">Save All</div>

<div class="vpad8">
    <asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
</div>

<h2>

    <asp:Literal runat="server" ID="Literal1"></asp:Literal>
</h2>
<script type="text/javascript">
    $(".button").popup({
        inline: true,
        hoverable: true,
        position: 'bottom left',
        delay: {
            show: 300,
            hide: 800
        }
    });
    $('.message .close').on('click', function() {
        $(this).closest('.message').fadeOut();
    });

    $(document).ready(function() {
        var grid = $("#LocalizationGridView");

        grid.find("tbody tr").each(function() {
            var cell = $(this).find("td:last-child");

            var value = cell.html();
            var html = "<div class='ui action input'><input type='text' value='" + value + "' /><div class='ui icon teal button' title='CTRL + RETURN'><i class='save icon'></i></div></div>";

            cell.html(html);
        });

        grid.find("tbody").addClass("ui form");

        grid.removeClass("initially hidden");

    });
</script>