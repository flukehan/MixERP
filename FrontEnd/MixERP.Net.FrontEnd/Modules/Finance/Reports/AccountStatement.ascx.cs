/********************************************************************************
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
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.Flag;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class AccountStatement : MixERPUserControl
    {
        private Literal accountMasterLiteral;
        private HtmlInputText accountNumberInputText;
        private Literal accountNumberLiteral;
        private HtmlSelect accountNumberSelect;
        private Literal baseCurrencyLiteral;
        private Literal cashFlowHeadingLiteral;
        private Literal confidentialLiteral;
        private Literal descriptionLiteral;
        private Literal externalCodeLiteral;
        private DateTextBox fromDateTextBox;
        private Literal headerLiteral;
        private Literal isCashAccountLiteral;
        private Literal isEmployeeLiteral;
        private Literal isPartyLiteral;
        private Literal isSystemAccountLiteral;
        private Literal normallyDebitLiteral;
        private Literal parentAccountLiteral;
        private Button showButton;
        private GridView statementGridView;
        private DateTextBox toDateTextBox;

        #region IDisposable

        private bool disposed;

        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.accountMasterLiteral != null)
                    {
                        this.accountMasterLiteral.Dispose();
                        this.accountMasterLiteral = null;
                    }

                    if (this.accountNumberInputText != null)
                    {
                        this.accountNumberInputText.Dispose();
                        this.accountNumberInputText = null;
                    }

                    if (this.accountNumberLiteral != null)
                    {
                        this.accountNumberLiteral.Dispose();
                        this.accountNumberLiteral = null;
                    }

                    if (this.accountNumberSelect != null)
                    {
                        this.accountNumberSelect.Dispose();
                        this.accountNumberSelect = null;
                    }

                    if (this.baseCurrencyLiteral != null)
                    {
                        this.baseCurrencyLiteral.Dispose();
                        this.baseCurrencyLiteral = null;
                    }

                    if (this.cashFlowHeadingLiteral != null)
                    {
                        this.cashFlowHeadingLiteral.Dispose();
                        this.cashFlowHeadingLiteral = null;
                    }

                    if (this.confidentialLiteral != null)
                    {
                        this.confidentialLiteral.Dispose();
                        this.confidentialLiteral = null;
                    }

                    if (this.descriptionLiteral != null)
                    {
                        this.descriptionLiteral.Dispose();
                        this.descriptionLiteral = null;
                    }

                    if (this.externalCodeLiteral != null)
                    {
                        this.externalCodeLiteral.Dispose();
                        this.externalCodeLiteral = null;
                    }

                    if (this.fromDateTextBox != null)
                    {
                        this.fromDateTextBox.Dispose();
                        this.fromDateTextBox = null;
                    }

                    if (this.headerLiteral != null)
                    {
                        this.headerLiteral.Dispose();
                        this.headerLiteral = null;
                    }

                    if (this.isCashAccountLiteral != null)
                    {
                        this.isCashAccountLiteral.Dispose();
                        this.isCashAccountLiteral = null;
                    }

                    if (this.isEmployeeLiteral != null)
                    {
                        this.isEmployeeLiteral.Dispose();
                        this.isEmployeeLiteral = null;
                    }

                    if (this.isPartyLiteral != null)
                    {
                        this.isPartyLiteral.Dispose();
                        this.isPartyLiteral = null;
                    }

                    if (this.isSystemAccountLiteral != null)
                    {
                        this.isSystemAccountLiteral.Dispose();
                        this.isSystemAccountLiteral = null;
                    }

                    if (this.normallyDebitLiteral != null)
                    {
                        this.normallyDebitLiteral.Dispose();
                        this.normallyDebitLiteral = null;
                    }

                    if (this.parentAccountLiteral != null)
                    {
                        this.parentAccountLiteral.Dispose();
                        this.parentAccountLiteral = null;
                    }

                    if (this.showButton != null)
                    {
                        this.showButton.Dispose();
                        this.showButton = null;
                    }

                    if (this.statementGridView != null)
                    {
                        this.statementGridView.Dispose();
                        this.statementGridView = null;
                    }

                    if (this.toDateTextBox != null)
                    {
                        this.toDateTextBox.Dispose();
                        this.toDateTextBox = null;
                    }
                }

                this.disposed = true;
            }
        }

        #endregion IDisposable

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopPanel(this.Placeholder1);
            this.CreateTabs(this.Placeholder1);
            this.CreateFlagPanel(this.Placeholder1);
            this.AutoInitialize();
            base.OnControlLoad(sender, e);
        }

        private void AutoInitialize()
        {
            string accountNumber = this.Request.QueryString["AccountNumber"];
            long accountId = Conversion.TryCastLong(this.Request.QueryString["AccountId"]);
            DateTime from = Conversion.TryCastDate(this.Request.QueryString["From"]);
            DateTime to = Conversion.TryCastDate(this.Request.QueryString["To"]);

            if (!string.IsNullOrWhiteSpace(accountNumber))
            {
                accountNumberInputText.Value = accountNumber;
            }
            else
            {
                if (accountId > 0)
                {
                    accountNumber = Data.Helpers.Accounts.GetAccountNumberByAccountId(accountId);

                    accountNumberInputText.Value = accountNumber;
                }
                else
                {
                    return;
                }
            }

            if (from != DateTime.MinValue)
            {
                fromDateTextBox.Text = from.Date.ToShortDateString();
            }

            if (to != DateTime.MinValue)
            {
                toDateTextBox.Text = to.Date.ToShortDateString();
            }

            this.BindGridView();
        }

        private void CreateFlagPanel(Control placeHolder)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";
                flag.Updated += FlagUpdated;

                placeHolder.Controls.Add(flag);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.AccountStatement;
                container.Controls.Add(header);
            }
        }

        private void FlagUpdated(object sender, FlagUpdatedEventArgs flagUpdatedEventArgs)
        {
            throw new NotImplementedException();
        }

        #region Tabs

        private void CreateTabs(Control container)
        {
            using (HtmlGenericControl tabMenu = new HtmlGenericControl("div"))
            {
                tabMenu.Attributes.Add("class", "ui top attached tabular menu");

                using (HtmlAnchor transactionStatementTabMenu = new HtmlAnchor())
                {
                    transactionStatementTabMenu.Attributes.Add("class", "active item");
                    transactionStatementTabMenu.Attributes.Add("data-tab", "first");
                    transactionStatementTabMenu.InnerText = Titles.TransactionStatement;

                    tabMenu.Controls.Add(transactionStatementTabMenu);
                }

                using (HtmlAnchor accountOverviewTabMenu = new HtmlAnchor())
                {
                    accountOverviewTabMenu.Attributes.Add("class", "item");
                    accountOverviewTabMenu.Attributes.Add("data-tab", "second");
                    accountOverviewTabMenu.InnerText = Titles.AccountOverview;

                    tabMenu.Controls.Add(accountOverviewTabMenu);
                }
                container.Controls.Add(tabMenu);
            }

            using (HtmlGenericControl transactionStatementTab = new HtmlGenericControl("div"))
            {
                transactionStatementTab.Attributes.Add("class", "ui bottom attached active form tab segment");
                transactionStatementTab.Attributes.Add("data-tab", "first");
                this.CreateFormPanel(transactionStatementTab);
                this.CreateGridPanel(transactionStatementTab);
                container.Controls.Add(transactionStatementTab);
            }

            using (HtmlGenericControl accountOverviewTab = new HtmlGenericControl("div"))
            {
                accountOverviewTab.Attributes.Add("class", "ui bottom attached tab segment");
                accountOverviewTab.Attributes.Add("data-tab", "second");
                this.CreateAccountOverviewPanel(accountOverviewTab);

                container.Controls.Add(accountOverviewTab);
            }
        }

        #endregion Tabs

        #region Account Overview Panel

        private void AddBodyRow(Table table, string text, Literal control)
        {
            using (TableRow row = new TableRow())
            {
                row.TableSection = TableRowSection.TableBody;

                using (TableCell definitionCell = new TableCell())
                {
                    definitionCell.Text = text;
                    row.Cells.Add(definitionCell);
                }

                using (TableCell controlCell = new TableCell())
                {
                    if (control == null)
                    {
                        control = new Literal();
                    }

                    controlCell.Controls.Add(control);

                    row.Cells.Add(controlCell);
                }

                table.Rows.Add(row);
            }
        }

        private void CreateAccountOverviewHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                headerLiteral = new Literal();
                header.Controls.Add(headerLiteral);

                container.Controls.Add(header);
            }

            using (HtmlGenericControl description = new HtmlGenericControl("div"))
            {
                description.Attributes.Add("class", "description");
                descriptionLiteral = new Literal();

                description.Controls.Add(descriptionLiteral);

                container.Controls.Add(description);
            }
        }

        private void CreateAccountOverviewPanel(HtmlGenericControl container)
        {
            this.CreateAccountOverviewHeader(container);
            this.CreateAccountOverviewTable(container);
        }

        private void CreateAccountOverviewTable(Control container)
        {
            using (Table table = new Table())
            {
                table.ID = "AccountOverViewGrid";
                table.CssClass = "ui definition table";
                this.CreateTableHeader(table);
                this.CreateTableBody(table);
                container.Controls.Add(table);
            }
        }

        private void CreateTableBody(Table table)
        {
            this.AddBodyRow(table, Titles.AccountNumber, this.accountNumberLiteral);
            this.AddBodyRow(table, Titles.ExternalCode, this.externalCodeLiteral);
            this.AddBodyRow(table, Titles.BaseCurrency, this.baseCurrencyLiteral);
            this.AddBodyRow(table, Titles.AccountMaster, this.accountMasterLiteral);
            this.AddBodyRow(table, Titles.Confidential, this.confidentialLiteral);
            this.AddBodyRow(table, Titles.CashFlowHeading, this.cashFlowHeadingLiteral);
            this.AddBodyRow(table, Titles.IsSystemAccount, this.isSystemAccountLiteral);
            this.AddBodyRow(table, Titles.IsCash, this.isCashAccountLiteral);
            this.AddBodyRow(table, Titles.IsEmployee, this.isEmployeeLiteral);
            this.AddBodyRow(table, Titles.IsParty, this.isPartyLiteral);
            this.AddBodyRow(table, Titles.NormallyDebit, this.normallyDebitLiteral);
            this.AddBodyRow(table, Titles.ParentAccount, this.parentAccountLiteral);
        }

        private void CreateTableHeader(Table table)
        {
            using (TableRow header = new TableRow())
            {
                header.TableSection = TableRowSection.TableHeader;

                using (TableHeaderCell emptyCell = new TableHeaderCell())
                {
                    header.Cells.Add(emptyCell);
                }

                using (TableHeaderCell definitionCell = new TableHeaderCell())
                {
                    definitionCell.Text = Titles.Definition;
                    header.Cells.Add(definitionCell);
                }

                table.Rows.Add(header);
            }
        }

        #endregion Account Overview Panel

        #region Form

        private void AddAccountNumberInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.AccountNumber, "AccountNumberInputText"))
                {
                    field.Controls.Add(label);
                }

                accountNumberInputText = new HtmlInputText();
                accountNumberInputText.ID = "AccountNumberInputText";
                field.Controls.Add(accountNumberInputText);

                container.Controls.Add(field);
            }
        }

        private void AddAccountNumberSelect(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("four wide field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Select, "AccountNumberSelect"))
                {
                    field.Controls.Add(label);
                }

                accountNumberSelect = new HtmlSelect();
                accountNumberSelect.ID = "AccountNumberSelect";
                field.Controls.Add(accountNumberSelect);
                container.Controls.Add(field);
            }
        }

        private void AddFromDateTextBox(HtmlGenericControl container)
        {
            fromDateTextBox = new DateTextBox();
            fromDateTextBox.ID = "FromDateTextBox";
            fromDateTextBox.Mode = Frequency.FiscalYearStartDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.From, fromDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void AddShowButton(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Prepare, "ShowButton"))
                {
                    field.Controls.Add(label);
                }

                showButton = new Button();
                showButton.ID = "ShowButton";
                showButton.Attributes.Add("class", "ui positive button");
                showButton.Text = Titles.Show;
                showButton.Click += ShowButton_Click;
                field.Controls.Add(showButton);

                container.Controls.Add(field);
            }
        }

        private void AddToDateTextBox(HtmlGenericControl container)
        {
            toDateTextBox = new DateTextBox();
            toDateTextBox.ID = "ToDateTextBox";
            toDateTextBox.Mode = Frequency.FiscalYearEndDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.To, toDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void BindGridView()
        {
            DateTime from = Conversion.TryCastDate(fromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(toDateTextBox.Text);
            int userId = SessionHelper.GetUserId();
            string accountNumber = accountNumberInputText.Value;
            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = Data.Reports.AccountStatement.GetAccountStatement(from, to, userId, accountNumber, officeId))
            {
                statementGridView.DataSource = table;
                statementGridView.DataBound += StatementGridViewDataBound;
                statementGridView.DataBind();
            }
        }

        private void CreateFormPanel(HtmlGenericControl container)
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", "fields");

                this.AddAccountNumberInputText(fields);
                this.AddAccountNumberSelect(fields);
                this.AddFromDateTextBox(fields);
                this.AddToDateTextBox(fields);
                this.AddShowButton(fields);

                container.Controls.Add(fields);
            }
        }

        private HtmlGenericControl GetDateField(string labelText, DateTextBox dateTextBox)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(labelText, dateTextBox.ID))
                {
                    field.Controls.Add(label);
                }

                using (HtmlGenericControl iconInput = new HtmlGenericControl("div"))
                {
                    iconInput.Attributes.Add("class", "ui icon input");

                    iconInput.Controls.Add(dateTextBox);

                    using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                    {
                        icon.Attributes.Add("class", "icon calendar pointer");
                        icon.Attributes.Add("onclick", string.Format(CultureInfo.InvariantCulture, "$('#{0}').datepicker('show');", dateTextBox.ID));
                    }

                    field.Controls.Add(iconInput);
                }

                return field;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGridView();
        }

        private void StatementGridViewDataBound(object sender, EventArgs eventArgs)
        {
            using (GridView grid = sender as GridView)
            {
                if (grid != null && grid.HeaderRow != null)
                {
                    grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        #endregion Form

        #region GridPanel

        private void CreateGridPanel(HtmlGenericControl container)
        {
            using (HtmlGenericControl autoOverflowPanel = new HtmlGenericControl("div"))
            {
                autoOverflowPanel.Attributes.Add("class", "auto-overflow-panel");

                this.CreateGridView(autoOverflowPanel);

                container.Controls.Add(autoOverflowPanel);
            }
        }

        private void CreateGridView(HtmlGenericControl container)
        {
            statementGridView = new GridView();
            statementGridView.ID = "StatementGridView";
            statementGridView.CssClass = "ui celled table nowrap";
            statementGridView.GridLines = GridLines.None;
            statementGridView.BorderStyle = BorderStyle.None;

            container.Controls.Add(statementGridView);
        }

        #endregion GridPanel

        #region Top Panel

        private void AddFlagButton(HtmlGenericControl container)
        {
            using (HtmlButton flagButton = new HtmlButton())
            {
                flagButton.ID = "FlagButton";
                flagButton.Attributes.Add("class", "ui button");
                flagButton.Attributes.Add("onclick", "return false");
                flagButton.InnerHtml = "<i class='icon flag'></i>" + Titles.Flag;
                container.Controls.Add(flagButton);
            }
        }

        private void AddNewButton(HtmlGenericControl container)
        {
            using (HtmlButton newButton = new HtmlButton())
            {
                newButton.ID = "AddNewButton";
                newButton.Attributes.Add("class", "ui button");
                newButton.Attributes.Add("onclick", "window.location='/Modules/Finance/Entry/JournalVoucher.mix';return false;");
                newButton.InnerHtml = "<i class='icon plus'></i>" + Titles.NewJournalEntry;
                container.Controls.Add(newButton);
            }
        }

        private void AddPrintButton(HtmlGenericControl container)
        {
            using (HtmlButton printButton = new HtmlButton())
            {
                printButton.ID = "PrintButton";
                printButton.Attributes.Add("class", "ui button");
                printButton.Attributes.Add("onclick", "return false");
                printButton.InnerHtml = "<i class='icon print'></i>" + Titles.Print;
                container.Controls.Add(printButton);
            }
        }

        private void CreateTopPanel(Control container)
        {
            using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
            {
                buttons.Attributes.Add("class", "ui icon buttons bpad16");

                this.AddNewButton(buttons);
                this.AddFlagButton(buttons);
                this.AddPrintButton(buttons);

                container.Controls.Add(buttons);
            }
        }

        #endregion Top Panel
    }
}