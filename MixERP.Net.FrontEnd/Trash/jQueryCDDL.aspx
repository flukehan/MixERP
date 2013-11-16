<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jQueryCDDL.aspx.cs" Inherits="MixERP.Net.FrontEnd.Trash.jQueryCDDL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        Select Party
        <br />
        <asp:DropDownList ID="PartyDropDownList" runat="server" /><br />

        Select Address<br />
        <asp:DropDownList ID="ShippingAddressDropDownList" runat="server" /><br />
    </form>

    <script type="text/javascript">
        $().ready(function () {
            $.ajax({
                type: "POST",
                url: "PartyData.asmx/GetParties",
                data: "{knownCategoryValues:'', category:''}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    BindParties(data.d)
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    addListItem("PartyDropDownList", 0, err.Message);
                }
            });

            $("#PartyDropDownList").change(function () {
                var partyCode = $(this).attr('value');
                $("#ShippingAddressDropDownList").empty();

                $.ajax({
                    type: "POST",
                    url: "PartyData.asmx/GetAddressByPartyCode",
                    data: "{partyCode:'" + partyCode + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        BindAddresses(msg.d)
                    },
                    error: function (xhr, status, error) {
                        var err = eval("(" + xhr.responseText + ")");
                        addListItem("ShippingAddressDropDownList", 0, err.Message);
                    }
                });
            });
        });

        function addListItem(dropDownListId, value, text) {
            var dropDownList = $("#" + dropDownListId);
            dropDownList.append($("<option></option>").val(value).html(text));
        }

        function BindParties(data) {
            if (data.length == 0) {
                addListItem("PartyDropDownList", 0, 'None');
                return;
            }

            addListItem("PartyDropDownList", 0, 'SELECT');

            $.each(data, function () {
                addListItem("PartyDropDownList", this['PartyCode'], this['DisplayField']);
            });
        }

        function BindAddresses(data) {
            if (data.length == 0) {
                addListItem("ShippingAddressDropDownList", 0, 'None');
                return;
            }

            addListItem("ShippingAddressDropDownList", 0, 'SELECT');

            $.each(data, function () {
                addListItem("ShippingAddressDropDownList", this['Address'], this['DisplayField']);
            });
        }
    </script>
</body>
</html>
