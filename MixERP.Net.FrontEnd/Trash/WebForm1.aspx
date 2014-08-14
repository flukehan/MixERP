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
        var decimalPlaces = "<%= LocalizationHelper.GetDecimalPlaces() %>";
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
