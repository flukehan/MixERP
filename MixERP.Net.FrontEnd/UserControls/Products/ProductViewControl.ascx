<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ProductViewControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductViewControl" %>
<%@ Import Namespace="Resources" %>
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
                OnClientClick="return getSelectedItems();"
                OnClick="UpdateButton_Click" />
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
                <mixerp:DateTextBox ID="DateFromDateTextBox" runat="server" CssClass="date" Width="72px" Mode="MonthStartDate" Required="true" />
            </td>
            <td>
                <mixerp:DateTextBox ID="DateToDateTextBox" runat="server" CssClass="date" Width="72px" Mode="MonthEndDate" Required="true" />
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
<asp:Panel ID="GridPanel" runat="server" Width="100px" ScrollBars="Auto">
    <asp:GridView
        ID="ProductViewGridView"
        runat="server"
        CssClass="grid"
        Width="100%"
        AutoGenerateColumns="false"
        OnRowDataBound="ProductViewGridView_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="72px" HeaderText="actions">
                <ItemTemplate>
                    <a href="#" id="ChecklistAnchor" runat="server" title="Go to Checklist">
                        <img runat="server" src="~/Resource/Icons/checklist-16.png" alt="Go to Checklist" />
                    </a>
                    <a href="#" id="PreviewAnchor" runat="server" title="Quick Preview" class="preview">
                        <img runat="server" src="~/Resource/Icons/search-16.png" alt="Search" />
                    </a>
                    <a href="#" id="PrintAnchor" runat="server" title="Print">
                        <img runat="server" src="~/Resource/Icons/print-16.png" alt="Print" />
                    </a>
                    <a href="#" title="Go To Top" onclick="window.scroll(0);">
                        <img runat="server" src="~/Resource/Icons/top-16.png" alt="Go to Top" />
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
            <asp:BoundField DataField="amount" HeaderText="amount" />
            <asp:BoundField DataField="transaction_ts" HeaderText="transaction_ts" DataFormatString="{0:D}" />
            <asp:BoundField DataField="user" HeaderText="user" />
            <asp:BoundField DataField="statement_reference" HeaderText="statement_reference" />
            <asp:BoundField DataField="flag_background_color" HeaderText="flag_background_color"  />
            <asp:BoundField DataField="flag_foreground_color" HeaderText="flag_foreground_color" />
        </Columns>
    </asp:GridView>
</asp:Panel>

<asp:HiddenField ID="SelectedValuesHidden" runat="server" />
<script src="/Scripts/UserControls/ProductViewControl.js"></script>