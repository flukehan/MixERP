<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Flags.aspx.cs" Inherits="MixERP.Net.FrontEnd.Setup.Flags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        //This event will be called by ASP.net AJAX during
        //asynchronous partial page rendering.
        function pageLoad(sender, args) {
            //At this point, the GridView should have already been reloaded.
            //So, load color information on the grid once again.
            loadColor();
        }

        $(document).ready(function () {
            loadColor();
        });

        var loadColor = function ()
        {
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
        }
    </script>
</asp:Content>