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
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using MixERP.Net.Core.Modules.Inventory.Data.Reports;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.TransactionGovernor;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.Flag;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Inventory.Reports
{
    public partial class AccountStatement : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopPanel(this.Placeholder1);
            this.CreateTabs(this.Placeholder1);
            this.CreateFlagPanel(this.Placeholder1);
            this.AutoInitialize();
        }

        private void AutoInitialize()
        {
            string itemCode = this.Request.QueryString["ItemCode"];
            int itemId = Conversion.TryCastInteger(this.Request.QueryString["ItemId"]);
            DateTime from = Conversion.TryCastDate(this.Request.QueryString["From"]);
            DateTime to = Conversion.TryCastDate(this.Request.QueryString["To"]);

            if (!string.IsNullOrWhiteSpace(itemCode))
            {
                this.itemCodeInputText.Value = itemCode;
            }
            else
            {
                if (itemId > 0)
                {
                    itemCode = Items.GetItemCodeByItemId(AppUsers.GetCurrentUserDB(), itemId);

                    this.itemCodeInputText.Value = itemCode;
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
            this.CreateAccountOverviewPanel(this.accountOverviewTab);
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
                flag.Catalog = AppUsers.GetCurrentUserDB();

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

            int userId = AppUsers.GetCurrent().View.UserId.ToInt();

            Flags.CreateFlag(AppUsers.GetCurrentUserDB(), userId, flagTypeId, resource, resourceKey,
                this.GetSelectedValues());

            this.BindGridView();
            this.CreateAccountOverviewPanel(this.accountOverviewTab);
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
                    accountOverviewTabMenu.InnerText = Titles.ItemOverview;

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

            this.accountOverviewTab = new HtmlGenericControl("div");
            this.accountOverviewTab.Attributes.Add("class", "ui bottom attached tab segment");
            this.accountOverviewTab.Attributes.Add("data-tab", "second");
            //this.CreateAccountOverviewPanel(this.accountOverviewTab);

            container.Controls.Add(this.accountOverviewTab);
        }

        #endregion Tabs

        #region IDisposable

        private HiddenField selectedValuesHidden;
        private Button showButton;
        private MixERPGridView statementGridView;
        private HtmlInputText itemCodeInputText;
        private HtmlSelect itemSelect;
        private HtmlSelect storeSelect;
        private HiddenField storeIdHidden;
        private DateTextBox fromDateTextBox;
        private DateTextBox toDateTextBox;
        private Literal headerLiteral;
        private HtmlGenericControl accountOverviewTab;

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

            if (this.accountOverviewTab != null)
            {
                this.accountOverviewTab.Dispose();
                this.accountOverviewTab = null;
            }

            if (this.headerLiteral != null)
            {
                this.headerLiteral.Dispose();
                this.headerLiteral = null;
            }

            if (this.itemCodeInputText != null)
            {
                this.itemCodeInputText.Dispose();
                this.itemCodeInputText = null;
            }

            if (this.itemSelect != null)
            {
                this.itemSelect.Dispose();
                this.itemSelect = null;
            }

            if (this.storeSelect != null)
            {
                this.storeSelect.Dispose();
                this.storeSelect = null;
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

        private void CreateAccountOverviewPanel(HtmlGenericControl container)
        {
            container.Controls.Clear();

            this.CreateAccountOverviewHeader(container);
            this.CreateAccountOverviewTable(container);
        }

        private void CreateAccountOverviewHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                this.headerLiteral = new Literal();
                header.Controls.Add(this.headerLiteral);

                container.Controls.Add(header);
            }
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
            if (this.itemCodeInputText == null)
            {
                return;
            }

            ItemView view =
                Factory.Get<ItemView>(AppUsers.GetCurrentUserDB(), "SELECT * FROM core.item_view WHERE item_code=@0",
                    this.itemCodeInputText.Value).FirstOrDefault();

            if (view == null)
            {
                return;
            }

            this.headerLiteral.Text = view.ItemName;
            this.AddBodyRow(table, Titles.ItemCode, view.ItemCode);
            this.AddBodyRow(table, Titles.ItemName, view.ItemName);
            this.AddBodyRow(table, Titles.ItemGroup, view.ItemGroup);
            this.AddBodyRow(table, Titles.ItemType, view.ItemType);
            this.AddBodyRow(table, Titles.Brand, view.Brand);
            this.AddBodyRow(table, Titles.PreferredSupplier, view.PreferredSupplier);
            this.AddBodyRow(table, Titles.LeadTime, view.LeadTimeInDays.ToString());
            this.AddBodyRow(table, Titles.UnitName, view.Unit);
            this.AddBodyRow(table, Titles.BaseUnitName, view.BaseUnit);
            this.AddBodyRow(table, Titles.ReorderUnitName, view.ReorderUnit);
        }

        private void AddBodyRow(Table table, string text, string definition)
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
                    controlCell.Text = definition;

                    row.Cells.Add(controlCell);
                }

                table.Rows.Add(row);
            }
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

        private void AddItemCodeField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ItemCode, "ItemCodeInputText"))
                {
                    field.Controls.Add(label);
                }

                this.itemCodeInputText = new HtmlInputText();
                this.itemCodeInputText.ID = "ItemCodeInputText";
                field.Controls.Add(this.itemCodeInputText);

                container.Controls.Add(field);
            }
        }

        private void AddItemSelectField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("four wide field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Select, "ItemSelect"))
                {
                    field.Controls.Add(label);
                }

                this.itemSelect = new HtmlSelect();
                this.itemSelect.ID = "ItemSelect";
                field.Controls.Add(this.itemSelect);
                container.Controls.Add(field);
            }
        }

        private void AddStoreSelectField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("two wide field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Store, "StoreSelect"))
                {
                    field.Controls.Add(label);
                }

                this.storeSelect = new HtmlSelect();
                this.storeSelect.ID = "StoreSelect";
                field.Controls.Add(this.storeSelect);
                container.Controls.Add(field);
            }
        }

        private void AddFromDateTextBox(HtmlGenericControl container)
        {
            this.fromDateTextBox = new DateTextBox();
            this.fromDateTextBox.ID = "FromDateTextBox";
            this.fromDateTextBox.Mode = FrequencyType.FiscalYearStartDate;
            this.fromDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
            this.fromDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

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
            this.toDateTextBox.Mode = FrequencyType.FiscalYearEndDate;
            this.toDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
            this.toDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (HtmlGenericControl field = this.GetDateField(Titles.To, this.toDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void BindGridView()
        {
            DateTime from = Conversion.TryCastDate(this.fromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.toDateTextBox.Text);
            int userId = AppUsers.GetCurrent().View.UserId.ToInt();
            string itemCode = this.itemCodeInputText.Value;
            int storeId = Conversion.TryCastInteger(this.storeIdHidden.Value);

            if (string.IsNullOrWhiteSpace(itemCode))
            {
                return;
            }

            if (storeId.Equals(0))
            {
                return;
            }

            this.statementGridView.DataSource = StockItems.GetAccountStatement(AppUsers.GetCurrentUserDB(), from, to,
                userId, itemCode, storeId);
            this.statementGridView.DataBound += this.StatementGridViewDataBound;
            this.statementGridView.DataBind();
        }


        private void CreateFormPanel(HtmlGenericControl container)
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", "fields");

                this.AddItemCodeField(fields);
                this.AddItemSelectField(fields);
                this.AddStoreSelectField(fields);
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
                        icon.Attributes.Add("onclick",
                            string.Format(CultureInfo.InvariantCulture, "$('#{0}').datepicker('show');", dateTextBox.ID));
                    }

                    field.Controls.Add(iconInput);
                }

                return field;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGridView();
            this.CreateAccountOverviewPanel(this.accountOverviewTab);
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
            GridViewHelper.AddDataBoundControl(this.statementGridView, "TranCode", Titles.TranCode);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "ValueDate", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(this.statementGridView, "Debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "Credit", Titles.Credit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "Balance", Titles.Balance, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "StatementReference", Titles.StatementReference);

            GridViewHelper.AddDataBoundControl(this.statementGridView, "Book", Titles.Book);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "ItemCode", Titles.ItemCode);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "ItemName", Titles.ItemName);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "FlagBg", string.Empty);
            GridViewHelper.AddDataBoundControl(this.statementGridView, "FlagFg", string.Empty);
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
            this.statementGridView = new MixERPGridView();
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

            this.storeIdHidden = new HiddenField();
            this.storeIdHidden.ID = "StoreIdHidden";

            container.Controls.Add(this.selectedValuesHidden);
            container.Controls.Add(this.storeIdHidden);
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

                this.AddFlagButton(buttons);
                this.AddPrintButton(buttons);
                this.AddHiddenFields(buttons);
                container.Controls.Add(buttons);
            }
        }

        #endregion Top Panel
    }
}