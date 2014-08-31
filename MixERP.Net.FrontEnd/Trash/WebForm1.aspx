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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MixERP.Net.FrontEnd.Trash.WebForm1" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:PlaceHolder ID="HeadHolder" runat="server">
        <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
        <script src="//code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <link href="/Scripts/jquery-ui/css/custom-theme/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
        <script src="/Scripts/jqueryNumber/jquery.number.js"></script>
        <script src="/Scripts/shortcut.js"></script>
        <link href="../Themes/purple/stylesheets/main.css" rel="stylesheet" />
    </asp:PlaceHolder>
    <title>Scrud Test</title>
</head>
<body>
    <script type="text/javascript">
        var today = "<%= DateTime.Now.ToShortDateString() %>";
        var shortDateFormat = "<%= LocalizationHelper.GetShortDateFormat() %>";
        var thousandSeparator = "<%= LocalizationHelper.GetThousandSeparator() %>";
        var decimalSeparator = "<%= LocalizationHelper.GetDecimalSeparator() %>";
        var currencyDecimalPlaces = "<%= MixERP.Net.Common.Helpers.LocalizationHelper.GetCurrencyDecimalPlaces() %>";
    </script>

    <script src="/Scripts/mixerp/mixerp.js"></script>

    <form id="form1" runat="server">
        <asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />
        <mixerp:ScrudForm
            ID="MenuScrud"
            runat="server"
            Description="This is a description"
            TableSchema="core"
            Table="menus"
            ViewSchema="core"
            View="menus"
            KeyColumn="menu_id"
            PageSize="10" />
    </form>
</body>
</html>
