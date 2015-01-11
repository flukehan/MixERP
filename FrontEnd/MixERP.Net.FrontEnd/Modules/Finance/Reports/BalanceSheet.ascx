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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BalanceSheet.ascx.cs" Inherits="MixERP.Net.Core.Modules.Finance.Reports.BalanceSheet" %>
<%@ Import Namespace="MixERP.Net.WebControls.Common" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>
<%@ Import Namespace="MixERP.Net.Common.Models.Core" %>
<%@ Import Namespace="MixERP.Net.Common" %>
<%@ Import Namespace="System.Data" %>

<style type="text/css">
    th:first-child {
        width: 400px;
    }
</style>
<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<script type="text/javascript">
    $(function () {
        var balanceSheetGridView = $("#BalanceSheetGridView");
        var previousPeriodDateTextBox = $("#PreviousPeriodDateTextBox");
        var currentPeriodDateTextBox = $("#CurrentPeriodDateTextBox");
        var factorInputText = $("#FactorInputText");
        var printButton = $("#PrintButton");

        var previousPeriod = previousPeriodDateTextBox.val();
        var currentPeriod = currentPeriodDateTextBox.val();
        var factor = parseInt2(factorInputText.val());

        var url;
        var accountId;
        var previousBalanceCell;
        var currentBalanceCell;
        var previousBalance;
        var currentBalance;
        var html;

        function createVisualHint() {
            balanceSheetGridView.find("tr td:nth-child(4)").each(function () {
                accountId = parseInt2($(this).html());

                if (!accountId) {
                    $(this).parent().addClass("hint");
                } else {
                    var account = $(this).parent().find("td:first-child");
                    account.html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + account.html());
                };
            });

            balanceSheetGridView.find("tr td:nth-child(4), th:nth-child(4)").remove();
            balanceSheetGridView.find("tr td:nth-child(4), th:nth-child(4)").remove();

        };

        function createLinkedBalances() {
            url = "/Modules/Finance/Reports/AccountStatementPopup.mix?AccountId={0}&To={1}";

            balanceSheetGridView.find("tbody tr td:nth-child(4)").each(function () {
                accountId = parseInt2($(this).html());

                previousBalanceCell = $(this).parent().find("td:nth-child(2)");
                currentBalanceCell = $(this).parent().find("td:nth-child(3)");

                previousBalance = parseFloat2(previousBalanceCell.html());
                currentBalance = parseFloat2(currentBalanceCell.html());

                previousBalance = (previousBalance === 0) ? "" : previousBalance;
                currentBalance = (currentBalance === 0) ? "" : currentBalance;

                if (accountId) {
                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, accountId, previousPeriod) + "')>" + previousBalanceCell.html() + "</a>";
                    previousBalanceCell.html(html);

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, accountId, currentPeriod) + "')>" + currentBalanceCell.html() + "</a>";
                    currentBalanceCell.html(html);
                };

                if (previousBalance < 0) {
                    previousBalanceCell.addClass("strong error");
                };

                if (currentBalance < 0) {
                    currentBalanceCell.addClass("strong error");
                };
            });

            balanceSheetGridView.find("tbody tr td:nth-child(5)").each(function () {
                if ($(this).html().toLowerCase() === "true") {
                    url = "/Modules/Finance/Reports/RetainedEarningsPopup.mix?Date={0}&Factor={1}";

                    previousBalanceCell = $(this).parent().find("td:nth-child(2)");
                    currentBalanceCell = $(this).parent().find("td:nth-child(3)");

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, previousPeriod, factor) + "')>" + previousBalanceCell.html() + "</a>";
                    previousBalanceCell.html(html);

                    html = "<a class='dotted underline' href='javascript:void(0);' onclick=showWindow('" + String.format(url, currentPeriod, factor) + "')>" + currentBalanceCell.html() + "</a>";
                    currentBalanceCell.html(html);
                };

            });
        };

        createLinkedBalances();
        createVisualHint();

    });
</script>

<script runat="server">

    public override void OnControlLoad(object sender, EventArgs e)
    {
        this.CreateTopPanel(this.Placeholder1);
        this.CreateGridPanel(this.Placeholder1);
        this.BindGrid();
        base.OnControlLoad(sender, e);
    }

    #region IDispoable

    private DateTextBox previousPeriodDateTextBox;
    private DateTextBox currentPeriodDateTextBox;
    private HtmlInputText factorInputText;
    private Button showButton;
    private HtmlInputButton printButton;
    private GridView grid;

    private bool disposed;

    public override sealed void Dispose()
    {
        if (!this.disposed)
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        if (this.currentPeriodDateTextBox != null)
        {
            this.currentPeriodDateTextBox.Dispose();
            this.currentPeriodDateTextBox = null;
        }

        if (this.previousPeriodDateTextBox != null)
        {
            this.previousPeriodDateTextBox.Dispose();
            this.previousPeriodDateTextBox = null;
        }

        if (this.factorInputText != null)
        {
            this.factorInputText.Dispose();
            this.factorInputText = null;
        }

        if (this.showButton != null)
        {
            this.showButton.Dispose();
            this.showButton = null;
        }

        if (this.printButton != null)
        {
            this.printButton.Dispose();
            this.printButton = null;
        }

        if (this.grid != null)
        {
            this.grid.RowDataBound -= this.Grid_RowDataBound;
            this.grid.DataBound -= this.Grid_DataBound;
            this.grid.Dispose();
            this.grid = null;
        }

        this.disposed = true;
    }

    #endregion

    #region Grid

    private void CreateGridPanel(Control container)
    {
        using (HtmlGenericControl autoOverflowPanel = new HtmlGenericControl("div"))
        {
            autoOverflowPanel.Attributes.Add("class", "auto-overflow-panel");

            this.grid = new GridView();
            this.grid.ID = "BalanceSheetGridView";
            this.grid.CssClass = "ui celled definition table segment nowrap";
            this.grid.GridLines = GridLines.None;
            this.grid.RowDataBound += this.Grid_RowDataBound;
            this.grid.DataBound += this.Grid_DataBound;
            this.CreateBoundFields();
            autoOverflowPanel.Controls.Add(this.grid);

            container.Controls.Add(autoOverflowPanel);
        }
    }

    #region Bound Fields

    private void CreateBoundFields()
    {
        this.grid.AutoGenerateColumns = false;
        GridViewHelper.AddDataBoundControl(this.grid, "item", "");
        GridViewHelper.AddDataBoundControl(this.grid, "previous_period", "Previous Period", "{0:N}", true);
        GridViewHelper.AddDataBoundControl(this.grid, "current_period", "Current Period", "{0:N}", true);
        GridViewHelper.AddDataBoundControl(this.grid, "account_id", "Account Id");
        GridViewHelper.AddDataBoundControl(this.grid, "is_retained_earning", string.Empty);
    }

    #endregion

    private void Grid_DataBound(object sender, EventArgs e)
    {
        if (this.grid.HeaderRow != null)
        {
            this.grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    #endregion

    #region Top Panel

    private void CreateTopPanel(Control container)
    {
        using (HtmlGenericControl header = new HtmlGenericControl("h2"))
        {
            header.InnerText = "Balance Sheet";
            container.Controls.Add(header);
        }

        this.CreateForm(container);
    }

    #region Form Segment

    private void CreateForm(Control container)
    {
        using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment())
        {
            using (HtmlGenericControl inlineFields = HtmlControlHelper.GetInlineFields())
            {
                this.AddPreviousPeriodField(inlineFields);
                this.AddCurrentPeriodField(inlineFields);
                this.AddFactorField(inlineFields);
                this.AddShowButton(inlineFields);
                this.AddPrintButton(inlineFields);

                formSegment.Controls.Add(inlineFields);
            }

            container.Controls.Add(formSegment);
        }
    }

    private void AddPreviousPeriodField(HtmlGenericControl container)
    {
        using (HtmlGenericControl field = HtmlControlHelper.GetField())
        {
            using (HtmlGenericControl label = HtmlControlHelper.GetLabel("Previous Period", "PreviousPeriodDateTextBox"))
            {
                field.Controls.Add(label);
            }

            this.previousPeriodDateTextBox = new DateTextBox();
            this.previousPeriodDateTextBox.ID = "PreviousPeriodDateTextBox";
            this.previousPeriodDateTextBox.Mode = Frequency.FiscalYearStartDate;
            field.Controls.Add(this.previousPeriodDateTextBox);

            container.Controls.Add(field);
        }
    }

    private void AddCurrentPeriodField(HtmlGenericControl container)
    {
        using (HtmlGenericControl field = HtmlControlHelper.GetField())
        {
            using (HtmlGenericControl label = HtmlControlHelper.GetLabel("Current Period", "CurrentPeriodDateTextBox"))
            {
                field.Controls.Add(label);
            }

            this.currentPeriodDateTextBox = new DateTextBox();
            this.currentPeriodDateTextBox.ID = "CurrentPeriodDateTextBox";
            this.currentPeriodDateTextBox.Mode = Frequency.FiscalYearEndDate;
            field.Controls.Add(this.currentPeriodDateTextBox);

            container.Controls.Add(field);
        }
    }

    private void AddFactorField(HtmlGenericControl container)
    {
        using (HtmlGenericControl field = HtmlControlHelper.GetField())
        {
            using (HtmlGenericControl label = HtmlControlHelper.GetLabel("Factor", "FactorInputText"))
            {
                field.Controls.Add(label);
            }

            this.factorInputText = new HtmlInputText();
            this.factorInputText.ID = "FactorInputText";
            this.factorInputText.Value = "1000";
            this.factorInputText.Attributes.Add("class", "integer");
            field.Controls.Add(this.factorInputText);

            container.Controls.Add(field);
        }
    }

    private void AddShowButton(HtmlGenericControl container)
    {
        this.showButton = new Button();
        this.showButton.ID = "ShowButton";
        this.showButton.CssClass = "ui green button";
        this.showButton.Text = "Show";
        this.showButton.Click += this.ShowButton_Click;
        container.Controls.Add(this.showButton);
    }

    private void ShowButton_Click(object sender, EventArgs eventArgs)
    {
        this.BindGrid();
    }

    private void BindGrid()
    {
        DateTime previousTerm = Conversion.TryCastDate(this.previousPeriodDateTextBox.Text);
        DateTime currentTerm = Conversion.TryCastDate(this.currentPeriodDateTextBox.Text);
        int factor = Conversion.TryCastInteger(this.factorInputText.Value);
        int userId = SessionHelper.GetUserId();
        int officeId = SessionHelper.GetOfficeId();

        using (DataTable table = MixERP.Net.Core.Modules.Finance.Data.Reports.BalanceSheet.GetBalanceSheet(previousTerm, currentTerm, userId, officeId, factor))
        {
            this.grid.DataSource = table;
            this.grid.DataBind();
        }
    }

    private void AddPrintButton(HtmlGenericControl container)
    {
        this.printButton = new HtmlInputButton();
        this.printButton.ID = "PrintButton";
        this.printButton.Attributes.Add("class", "ui orange button");
        this.printButton.Value = "Print";

        container.Controls.Add(this.printButton);
    }

    #endregion

    #endregion</script>