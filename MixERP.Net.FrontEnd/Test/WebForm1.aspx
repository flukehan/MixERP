<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MixERP.Net.FrontEnd.Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var decimalPlaces = 2;
        var decimalSeparator = ".";
        var thousandSeparator = ",";
    </script>
    <script src="../bundles/scripts/master-page.js"></script>
    <link href="../bundles/stylesheets/master-page.css" rel="stylesheet" />
    <style type="text/css">
        body, html {
            background-color:white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <mixerp:Attachment ID="Attachment" runat="server" />
    </div>
    </form>
</body>
</html>
