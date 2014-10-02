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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flags.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Flags" %>
<asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />
<script type="text/javascript">
    //This event will be called by ASP.net AJAX during
    //asynchronous partial page rendering.
    function pageLoad() {
        //At this point, the GridView should have already been reloaded.
        //So, load color information on the grid once again.
        loadColor();
    }

    $(document).ready(function () {
        loadColor();
    });

    var loadColor = function () {
        //Get an instance of the form grid.

        var grid = $("#FormGridView");

        //Set position of the column which contains color value.
        var bgColorColumnPos = "4";
        var fgColorColumnPos = "5";

        //Iterate through all the rows of the grid.
        grid.find("tr").each(function () {

            //Get the current row instance from the loop.
            var row = $(this);

            //Read the color value from the associated column.
            var background = row.find("td:nth-child(" + bgColorColumnPos + ")").html();
            var foreground = row.find("td:nth-child(" + fgColorColumnPos + ")").html();

            if (background) {
                row.css("background", background);
            }

            if (foreground) {
                row.find("td").css("color", foreground);
            }

            //Iterate through all the columns of the current row.
            row.find("td").each(function () {
                //Prevent border display by unsetting the border information for each cell.
                $(this).css("border", "none");
            });

        });
    };
</script>