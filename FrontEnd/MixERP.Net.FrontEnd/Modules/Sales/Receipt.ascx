<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Receipt" %>

<h1>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h1>

<div class="btn-toolbar" role="toolbar">
    <div class="btn-group">
        <button type="button" id="AddNewButton" runat="server" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-plus"></span>
            <asp:Literal runat="server" ID="AddNewLiteral"></asp:Literal>
        </button>
        <button type="button" id="flagButton" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-flag"></span>&nbsp;
            <asp:Literal runat="server" ID="FlagLiteral"></asp:Literal>
        </button>

        <button type="button" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-print"></span>&nbsp;
            <asp:Literal runat="server" ID="PrintLiteral"></asp:Literal>
        </button>
    </div>
</div>

<div id="flag-popunder" style="width: 300px; display: none;" class="popunder">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Literal runat="server" ID="FlagThisTransactionLiteral"></asp:Literal></h3>
        </div>
        <div class="panel-body">
            <div>
                <asp:Literal runat="server" ID="FlagDescriptionLiteral"></asp:Literal>
            </div>
            <br />
            <p>
                <asp:Literal runat="server" ID="SelectFlagLiteral"></asp:Literal>
            </p>
            <p>
                <select id="FlagDropDownList" class="form-control input-sm"></select>
                <asp:HiddenField ID="FlagTypeHidden" runat="server" />
            </p>
            <p>
                <asp:Button
                    ID="UpdateButton"
                    runat="server"
                    CssClass="btn btn-primary btn-sm"
                    OnClientClick="return getSelectedItems();"
                    OnClick="UpdateButton_Click" />
                <a href="javascript:void(0);" onclick="$('#flag-popunder').toggle(500);" class="btn btn-default btn-sm">
                    <asp:Literal runat="server" ID="CloseLiteral"></asp:Literal>
                </a>
            </p>
        </div>
    </div>
</div>

<div class="grey" style="margin: 8px 0 8px 0;">
    <div class="row" style="margin-left: 8px;">

        <div class="col-md-1 pad4" style="width: 120px;">
            <div class="input-group">
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="MonthStartDate"
                    Required="true" />
                <span class="input-group-addon" onclick="$('#DateFromDateTextBox').datepicker('show');">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
        </div>

        <div class="col-md-1 pad4" style="width: 120px;">
            <div class="input-group">
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server"
                    CssClass="date form-control input-sm"
                    Mode="MonthEndDate" Required="true" />
                <span class="input-group-addon" onclick="$('#DateToDateTextBox').datepicker('show');">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
        </div>

        <div class="col-md-1 pad4">
            <asp:TextBox ID="OfficeTextBox" runat="server" CssClass="form-control input-sm" placeholder="Office" />
        </div>

        <div class="col-md-1 pad4">
            <asp:TextBox ID="PartyTextBox" runat="server" CssClass="form-control input-sm" placeholder="Party" />
        </div>

        <div class="col-md-1 pad4">
            <asp:TextBox ID="UserTextBox" runat="server" CssClass="form-control input-sm" placeholder="User" />
        </div>

        <div class="col-md-2 pad4">
            <asp:TextBox ID="ReferenceNumberTextBox" runat="server" CssClass="form-control input-sm" placeholder="Reference Number" />
        </div>

        <div class="col-md-2 pad4">
            <asp:TextBox ID="StatementReferenceTextBox" runat="server" CssClass="form-control input-sm" placeholder="Statement Reference" />
        </div>
        <div class="col-md-1 pad4">
            <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="btn btn-default input-sm" OnClick="ShowButton_Click" />
        </div>
    </div>
</div>

<asp:Panel ID="GridPanel" runat="server" Width="100px" ScrollBars="Auto">
    <asp:GridView
        ID="ReceiptViewGridView"
        runat="server"
        CssClass="table2 table-bordered pointer"
        Width="2000px"
        AutoGenerateColumns="False"
        OnRowDataBound="ReceiptGridView_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="actions" ItemStyle-Width="96px">
                <ItemTemplate>
                    <a href="#" id="ChecklistAnchor" runat="server">
                        <img runat="server" src="~/Resource/Icons/checklist-16.png" />
                    </a>
                    <a href="#" id="PreviewAnchor" runat="server" class="preview">
                        <img runat="server" src="~/Resource/Icons/search-16.png" />
                    </a>
                    <a href="#" id="PrintAnchor" runat="server">
                        <img runat="server" src="~/Resource/Icons/print-16.png" />
                    </a>
                    <a href="#" title="Go To Top" onclick="window.scroll(0);">
                        <img runat="server" src="~/Resource/Icons/top-16.png" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="SelectCheckBox" runat="server" ClientIDMode="Predictable" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="transaction_master_id" HeaderText="id" />
            <asp:BoundField DataField="value_date" HeaderText="value_date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="office" HeaderText="office" />
            <asp:BoundField DataField="party" HeaderText="party" />
            <asp:BoundField DataField="user_name" HeaderText="user" />
            <asp:BoundField DataField="currency_code" HeaderText="currency" />
            <asp:BoundField DataField="amount" HeaderText="amount" />
            <asp:BoundField DataField="reference_number" HeaderText="reference_number" />
            <asp:BoundField DataField="statement_reference" HeaderText="statement_reference" />
            <asp:BoundField DataField="transaction_ts" HeaderText="transaction_ts" DataFormatString="{0:D}" />
            <asp:BoundField DataField="flag_background_color" HeaderText="flag_background_color" />
            <asp:BoundField DataField="flag_foreground_color" HeaderText="flag_foreground_color" />
        </Columns>
    </asp:GridView>
</asp:Panel>

<asp:HiddenField ID="SelectedValuesHidden" runat="server" />

<script type="text/javascript">
    var flagPopunder = $("#flag-popunder");
    var flagButton = $("#flagButton");
    var flagDropDownList = $("#FlagDropDownList");
    var flagTypeHidden = $("#FlagTypeHidden");
    var addNewButton = $("#AddNewButton");

    var receiptViewGridView = $("#ReceiptViewGridView");

    //Variables
    var url = "";
    var data = "";

    $(document).ready(function () {
        var contentWidth = $("#content").width();
        var menuWidth = $("#menu2").width();

        var margin = 20;
        var width = contentWidth - menuWidth - margin;

        $("#GridPanel").css("width", width + "px");
        loadFlags();

        createFlaggedRows(receiptViewGridView);
    });

    addNewButton.click(function () {
        window.location = "/Modules/Sales/Entry/Receipt.mix";
    });

    function loadFlags() {
        url = "/Modules/Sales/Services/Receipt/Accounts.asmx/GetFlags";
        ajaxDataBind(url, flagDropDownList, null);
    };

    flagButton.click(function () {
        popUnder(flagPopunder, flagButton);
    });

    var getSelectedItems = function () {
        flagDropDownList.updateHiddenFieldOnBlur(flagTypeHidden);

        //Set the position of the column which contains the checkbox.
        var checkBoxColumnPosition = "2";

        //Set the position of the column which contains id.
        var idColumnPosition = "3";

        var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, receiptViewGridView);

        if (selection.length > 0) {
            $("#SelectedValuesHidden").val(selection.join(','));
            return true;
        } else {
            $.notify(nothingSelectedLocalized, "error");
            return false;
        }
    };

    receiptViewGridView.find('tr').click(function () {
        var checkBox = $(this).find('td input:checkbox');
        toogleSelection(checkBox.attr("id"));
    });
</script>