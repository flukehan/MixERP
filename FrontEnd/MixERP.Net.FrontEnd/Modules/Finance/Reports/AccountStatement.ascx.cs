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

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.Flag;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class AccountStatement : MixERPUserControl
    {
        private Literal accountMasterLiteral;
        private HtmlInputText accountNumberInputText;
        private Literal accountNumberLiteral;
        private HtmlSelect accountNumberSelect;
        private Literal baseCurrencyLiteral;
        private Literal confidentialLiteral;
        private Literal descriptionLiteral;
        private Literal externalCodeLiteral;
        private DateTextBox fromDateTextBox;
        private Literal headerLiteral;
        private Literal isEmployeeLiteral;
        private Literal isPartyLiteral;
        private Literal isSystemAccountLiteral;
        private Literal normallyDebitLiteral;
        private Literal parentAccountLiteral;
        private HiddenField selectedValuesHidden;
        private Button showButton;
        private GridView statementGridView;
        private DateTextBox toDateTextBox;

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
                this.accountNumberInputText.Value = accountNumber;
            }
            else
            {
                if (accountId > 0)
                {
                    accountNumber = Data.Helpers.Accounts.GetAccountNumberByAccountId(accountId);

                    this.accountNumberInputText.Value = accountNumber;
                }
                else
                {
                    return;
                }
            }

            if (from != DateTime.MinValue)
            {
                this.fromDateTextBox.Text = from.Date.ToShortDateString();
            }

            if (to != DateTime.MinValue)
            {
                this.toDateTextBox.Text = to.Date.ToShortDateString();
            }

            this.BindGridView();
            this.BindOverview();
        }

        private void CreateFlagPanel(Control placeHolder)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";
                flag.Updated += this.Flag_Updated;

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

        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
            int flagTypeId = e.FlagId;

            const string resource = "account_statement";
            const string resourceKey = "transaction_code";

            int userId = SessionHelper.GetUserId();

            TransactionGovernor.Flags.CreateFlag(userId, flagTypeId, resource, resourceKey, this.GetSelectedValues());

            this.BindGridView();
            this.BindOverview();
        }

        private Collection<string> GetSelectedValues()
        {
            string selectedValues = this.selectedValuesHidden.Value;

            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<string>();
            }

            Collection<string> values = new Collection<string>();

            foreach (string value in selectedValues.Split(','))
            {
                values.Add(value);
            }

            return values;
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
            if (!disposing)
            {
                return;
            }
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

            if (this.selectedValuesHidden != null)
            {
                this.selectedValuesHidden.Dispose();
                this.selectedValuesHidden = null;
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


            this.disposed = true;
        }

        #endregion IDisposable

        #region Account Overview Panel

        private void AddBodyRow(Table table, string text, ref Literal control)
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
                this.headerLiteral = new Literal();
                header.Controls.Add(this.headerLiteral);

                container.Controls.Add(header);
            }

            using (HtmlGenericControl description = new HtmlGenericControl("div"))
            {
                description.Attributes.Add("class", "description");
                this.descriptionLiteral = new Literal();

                description.Controls.Add(this.descriptionLiteral);

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
            this.AddBodyRow(table, Titles.AccountNumber, ref this.accountNumberLiteral);
            this.AddBodyRow(table, Titles.ExternalCode, ref this.externalCodeLiteral);
            this.AddBodyRow(table, Titles.BaseCurrency, ref this.baseCurrencyLiteral);
            this.AddBodyRow(table, Titles.AccountMaster, ref this.accountMasterLiteral);
            this.AddBodyRow(table, Titles.Confidential, ref this.confidentialLiteral);
            this.AddBodyRow(table, Titles.IsSystemAccount, ref this.isSystemAccountLiteral);
            this.AddBodyRow(table, Titles.IsEmployee, ref this.isEmployeeLiteral);
            this.AddBodyRow(table, Titles.IsParty, ref this.isPartyLiteral);
            this.AddBodyRow(table, Titles.NormallyDebit, ref this.normallyDebitLiteral);
            this.AddBodyRow(table, Titles.ParentAccount, ref this.parentAccountLiteral);
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

                this.accountNumberInputText = new HtmlInputText();
                this.accountNumberInputText.ID = "AccountNumberInputText";
                field.Controls.Add(this.accountNumberInputText);

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

                this.accountNumberSelect = new HtmlSelect();
                this.accountNumberSelect.ID = "AccountNumberSelect";
                field.Controls.Add(this.accountNumberSelect);
                container.Controls.Add(field);
            }
        }

        private void AddFromDateTextBox(HtmlGenericControl container)
        {
            this.fromDateTextBox = new DateTextBox();
            this.fromDateTextBox.ID = "FromDateTextBox";
            this.fromDateTextBox.Mode = Frequency.FiscalYearStartDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.From, this.fromDateTextBox))
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

                this.showButton = new Button();
                this.showButton.ID = "ShowButton";
                this.showButton.Attributes.Add("class", "ui positive button");
                this.showButton.Text = Titles.Show;
                this.showButton.Click += this.ShowButton_Click;
                field.Controls.Add(this.showButton);

                container.Controls.Add(field);
            }
        }

        private void AddToDateTextBox(HtmlGenericControl container)
        {
            this.toDateTextBox = new DateTextBox();
            this.toDateTextBox.ID = "ToDateTextBox";
            this.toDateTextBox.Mode = Frequency.FiscalYearEndDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.To, this.toDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void BindGridView()
        {
            DateTime from = Conversion.TryCastDate(this.fromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.toDateTextBox.Text);
            int userId = SessionHelper.GetUserId();
            string accountNumber = this.accountNumberInputText.Value;
            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = Data.Reports.AccountStatement.GetAccountStatement(from, to, userId, accountNumber, officeId))
            {
                this.statementGridView.DataSource = table;
                this.statementGridView.DataBound += this.StatementGridViewDataBound;
                this.statementGridView.DataBind();
            }
        }

        private void BindOverview()
        {
            string accountNumber = this.accountNumberInputText.Value;

            using (DataTable table = Data.Reports.AccountStatement.GetAccountOverview(accountNumber))
            {
                if (table.Rows != null && table.Rows.Count.Equals(1) && table.Rows[0] != null)
                {
                    this.headerLiteral.Text = Conversion.TryCastString(table.Rows[0]["account"]);
                    this.descriptionLiteral.Text = Conversion.TryCastString(table.Rows[0]["description"]);
                    this.accountNumberLiteral.Text = Conversion.TryCastString(table.Rows[0]["account_number"]);
                    this.externalCodeLiteral.Text = Conversion.TryCastString(table.Rows[0]["external_code"]);
                    this.baseCurrencyLiteral.Text = Conversion.TryCastString(table.Rows[0]["currency_code"]);
                    this.accountMasterLiteral.Text = Conversion.TryCastString(table.Rows[0]["account_master_code"]);
                    this.confidentialLiteral.Text = Conversion.TryCastString(table.Rows[0]["confidential"]);
                    this.isSystemAccountLiteral.Text = Conversion.TryCastString(table.Rows[0]["sys_type"]);
                    this.normallyDebitLiteral.Text = Conversion.TryCastString(table.Rows[0]["normally_debit"]);
                    this.parentAccountLiteral.Text = Conversion.TryCastString(table.Rows[0]["parent_account"]);
                }
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
            this.BindOverview();
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

        #region Template Fields

        private void AddTemplateFields()
        {
            TemplateField actionTemplateField = new TemplateField();
            actionTemplateField.HeaderText = string.Empty;
            actionTemplateField.ItemTemplate = new ActionTemplate();
            this.statementGridView.Columns.Add(actionTemplateField);

            TemplateField checkBoxTemplateField = new TemplateField();
            checkBoxTemplateField.HeaderText = Titles.Select;
            checkBoxTemplateField.ItemTemplate = new GridViewHelper.GridViewSelectTemplate();
            this.statementGridView.Columns.Add(checkBoxTemplateField);
        }

        internal class ActionTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl previewIcon = new HtmlGenericControl("i"))
                {
                    previewIcon.Attributes.Add("class", "icon print");
                    previewIcon.Attributes.Add("onclick", "showPreview(this);");
                    container.Controls.Add(previewIcon);
                }
            }
        }

        #endregion Template Fields

        private void CreateColumns()
        {
            this.AddTemplateFields();
            GridViewHelper.AddDataBoundControl(this.statementGridView, "tran_code", Titles.TranCode);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "value_date", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(this.statementGridView, "debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "credit", Titles.Credit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "balance", Titles.Balance, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "statement_reference", Titles.StatementReference);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "office", Titles.Office);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "book", Titles.Book);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "account_number", Titles.AccountNumber);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "account", Titles.Account);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "flag_bg", string.Empty);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "flag_fg", string.Empty);
        }

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
            this.statementGridView = new GridView();
            this.statementGridView.ID = "StatementGridView";
            this.statementGridView.CssClass = "ui celled table nowrap";
            this.statementGridView.GridLines = GridLines.None;
            this.statementGridView.BorderStyle = BorderStyle.None;
            this.statementGridView.AutoGenerateColumns = false;

            this.CreateColumns();

            container.Controls.Add(this.statementGridView);
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

        private void AddHiddenFields(HtmlGenericControl container)
        {
            this.selectedValuesHidden = new HiddenField();
            this.selectedValuesHidden.ID = "SelectedValuesHidden";

            container.Controls.Add(this.selectedValuesHidden);
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
                this.AddHiddenFields(buttons);
                container.Controls.Add(buttons);
            }
        }

        #endregion Top Panel
    }
}