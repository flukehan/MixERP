<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ProductViewControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductViewControl" %>
<asp:ScriptManager runat="server" />
<h1>
    <asp:Literal ID="TitleLiteral" runat="server" />
</h1>
<hr class="hr" />

<div class="vpad12">
    <table class="valignmiddle" style="border-collapse: collapse;">
        <tr>
            <td>
                <asp:LinkButton
                    ID="AddNewLinkButton"
                    runat="server"
                    CssClass="menu"
                    Text="<%$Resources:Titles, AddNew %>" />

                <asp:LinkButton
                    ID="MergeToOrderLinkButton"
                    runat="server"
                    CssClass="menu"
                    Text="<%$Resources:Titles, MergeBatchToSalesOrder %>"
                    OnClientClick="return getSelectedItems();"
                    OnClick="MergeToOrderLinkButton_Click"
                    Visible="false" />


                <asp:LinkButton
                    ID="MergeToDeliveryLinkButton"
                    runat="server"
                    CssClass="menu"
                    Text="<%$Resources:Titles, MergeBatchToSalesDelivery %>"
                    OnClientClick="return getSelectedItems();"
                    OnClick="MergeToDeliveryLinkButton_Click"
                    Visible="false" />

            </td>
            <td>
                <a href="#" class="menu" id="flagButton">Flag This Transaction</a>
            </td>
            <td>
                <a href="#" title="Print" class="menu">Print</a>
            </td>
        </tr>
    </table>


    <div id="flag-popunder" style="position: absolute; width: 300px; display: none;" class="popunder">
        <h3>Flag This Transaction</h3>
        <hr class="hr" />

        <div class="note">
            You can mark this transaction with a flag, however you will not be able to see the flags created by other users.                
        </div>
        <br />
        <p>Please select a flag</p>
        <p>
            <asp:DropDownList ID="FlagDropDownList" runat="server" Width="300px">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button
                ID="UpdateButton"
                runat="server"
                Text="Update"
                CssClass="menu"
                OnClientClick="return getSelectedItems();" />
            <a href="#" onclick="$('#flag-popunder').toggle(500);" class="menu">Close</a>
        </p>
    </div>
</div>
<asp:Label ID="ErrorLabel" runat="server" CssClass="error" />

<div id="filter" class="vpad8">
    <table class="form">
        <tr>
            <td>Date From
            </td>
            <td>Date To
            </td>
            <td>Office
            </td>
            <td>Party
            </td>
            <td>Price Type
            </td>
            <td>User
            </td>
            <td>Reference Number
            </td>
            <td>Statement Reference
            </td>
            <td></td>
        </tr>
        <tr>
            <td>

                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server" CssClass="date" Width="72px" Mode="MonthStartDate" />
            </td>
            <td>
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server" CssClass="date" Width="72px" Mode="MonthEndDate" />
            </td>
            <td>
                <asp:TextBox ID="OfficeTextBox" runat="server" Width="72px" />
            </td>
            <td>
                <asp:TextBox ID="PartyTextBox" runat="server" Width="72px" />
            </td>
            <td>
                <asp:TextBox ID="PriceTypeTextBox" runat="server" Width="72px" />
            </td>
            <td>
                <asp:TextBox ID="UserTextBox" runat="server" Width="72px" />
            </td>
            <td>
                <asp:TextBox ID="ReferenceNumberTextBox" runat="server" Width="150px" />
            </td>
            <td>
                <asp:TextBox ID="StatementReferenceTextBox" runat="server" Width="150px" />
            </td>
            <td>
                <asp:Button ID="ShowButton" runat="server" Text="Show" CssClass="button" Width="50px" OnClick="ShowButton_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="GridPanel" runat="server" Width="1024px" ScrollBars="Auto">
    <asp:GridView
        ID="ProductViewGridView"
        runat="server"
        CssClass="grid"
        Width="1424px"
        AutoGenerateColumns="false"
        OnRowDataBound="ProductViewGridView_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="56px" HeaderText="actions">
                <ItemTemplate>
                    <a href="#" id="PreviewAnchor" runat="server" title="Quick Preview" class="preview">
                        <img src="/Resource/Icons/search-16.png" />
                    </a>
                    <a href="#" id="PrintAnchor" runat="server" title="Print">
                        <img src="/Resource/Icons/print-16.png" />
                    </a>
                    <a href="#" title="Go To Top" onclick="window.scroll(0);">
                        <img src="/Resource/Icons/top-16.png" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="SelectCheckBox" runat="server" ClientIDMode="Predictable" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" />
            <asp:BoundField DataField="value_date" HeaderText="value_date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="office" HeaderText="office" />
            <asp:BoundField DataField="reference_number" HeaderText="reference_number" />
            <asp:BoundField DataField="party" HeaderText="party" />
            <asp:BoundField DataField="price_type" HeaderText="price_type" />
            <asp:BoundField DataField="transaction_ts" HeaderText="transaction_ts" DataFormatString="{0:D}" />
            <asp:BoundField DataField="user" HeaderText="user" />
            <asp:BoundField DataField="statement_reference" HeaderText="statement_reference" />
            <asp:BoundField DataField="flag_color" HeaderText="flag_color" />
        </Columns>
    </asp:GridView>
</asp:Panel>
<asp:HiddenField ID="SelectedValuesHidden" runat="server" />


<script type="text/javascript">
    var getSelectedItems = function () {
        var selection = [];

        //Get the grid instance.
        var grid = $("#ProductViewGridView");

        //Set the position of the column which contains the checkbox.
        var checkBoxColumnPosition = "2";

        //Set the position of the column which contains id.
        var idColumnPosition = "3";

        //Iterate through each row to investigate the selection.
        grid.find("tr").each(function () {

            //Get an instance of the current row in this loop.
            var row = $(this);

            //Get the instance of the cell which contains the checkbox.
            var checkBoxContainer = row.select("td:nth-child(" + checkBoxColumnPosition + ")");

            //Get the instance of the checkbox from the container.
            var checkBox = checkBoxContainer.find("input");

            if (checkBox) {
                //Check if the checkbox was selected or checked.
                if (checkBox.attr("checked") == "checked") {
                    //Get ID from the associated cell.
                    var id = row.find("td:nth-child(" + idColumnPosition + ")").html();

                    //Add the ID to the array.
                    selection.push(id);
                }
            }
        });


        if (selection.length > 0) {
            $("#SelectedValuesHidden").val(selection.join(','));
            return true;
        }
        else {
            alert("<%= Resources.Labels.NothingSelected %>");
            return false;
        }

        return false;
    }

    //Get FlagButton instance.
    var flagButton = $("#flagButton");

    flagButton.click(function () {
        //Get flag div instance which will be displayed under the button.
        var popunder = $("#flag-popunder");

        //Get FlagButton's position and height information.
        var left = $(this).position().left;
        var top = $(this).position().top;
        var height = $(this).height();

        //Margin in pixels.
        var margin = 12;

        popunder.css("left", left);
        popunder.css("top", top + height + margin);
        popunder.show(500);
    });


    $('#ProductViewGridView tr').click(function () {
        console.log('Grid row was clicked. Now, searching the radio button.');
        var checkBox = $(this).find('td input:checkbox')
        console.log('The check box was found.');
        toogleSelection(checkBox.attr("id"));
    });

    var toogleSelection = function (id) {
        var attribute = $("#" + id).attr("checked");
        if (attribute) {
            $("#" + id).removeAttr("checked");
        }
        else {
            $("#" + id).attr("checked", "checked");
        }

        console.log('Radio button "' + id + '" selected.');
    }



    $(document).ready(function () {
        shortcut.add("ALT+O", function () {
            $('#OfficeTextBox').foucs();
        });

        shortcut.add("CTRL+ENTER", function () {
            $('#ShowButton').click();
        });
    });



    function DropDown(el) {
        this.dd = el;
        this.placeholder = this.dd.children('span');
        this.opts = this.dd.find('ul.dropdown > li');
        this.val = '';
        this.index = -1;
        this.initEvents();
    }

    DropDown.prototype = {
        initEvents: function () {
            var obj = this;

            obj.dd.on('click', function (event) {
                $(this).toggleClass('active');
                event.stopPropagation();
            });

            obj.opts.on('click', function (e) {
                var opt = $(this);
                obj.val = opt.text();
                obj.index = opt.index();
                obj.placeholder.text(obj.val);
            });
        },

        getValue: function () {
            return this.val;
        },
        getIndex: function () {
            return this.index;
        }
    }

    $(function () {

        var dd = new DropDown($('#dd'));

        $(document).click(function () {
            // all dropdowns
            $('.wrapper-dropdown-5').removeClass('active');
        });

    });

</script>
