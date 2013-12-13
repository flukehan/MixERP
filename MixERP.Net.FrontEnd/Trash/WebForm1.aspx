<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MixERP.Net.FrontEnd.Trash.WebForm1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:PlaceHolder ID="HeadHolder" runat="server">
        <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
        <script src="//code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <link href="<%=ResolveUrl("~/Scripts/jquery-ui/css/custom-theme/jquery-ui-1.10.3.custom.min.css") %>" rel="stylesheet" type="text/css" />
         <script src="<%=ResolveUrl("~/Scripts/jqueryNumber/jquery.number.min.js") %>"></script>
       <script src="<%=ResolveUrl("~/Scripts/shortcut.js") %>"></script>
        <link href="../Themes/purple/main.css" rel="stylesheet" />
    </asp:PlaceHolder>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        var today = "<%= System.DateTime.Now.ToShortDateString() %>";
        var shortDateFormat = "<%= MixERP.Net.Common.Helpers.LocalizationHelper.GetShortDateFormat() %>";
        var thousandSeparator = "<%= MixERP.Net.Common.Helpers.LocalizationHelper.GetThousandSeparator() %>";
        var decimalSeparator = "<%= MixERP.Net.Common.Helpers.LocalizationHelper.GetDecimalSeparator() %>";
        var decimalPlaces = "<%= MixERP.Net.Common.Helpers.LocalizationHelper.GetDecimalPlaces() %>";
    </script>

    <script src="<%=ResolveUrl("~/Scripts/mixerp.js") %>"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
