using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.Framework.Controls;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.BackOffice.OTS
{
    public partial class OpeningInventory : MixERPUserControl, ITransaction
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.AdminOnly; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopPanel(this.Placeholder1);

            bool allowMultipleOpeningInventory = DbConfig.GetSwitch(AppUsers.GetCurrentUserDB(), "AllowMultipleOpeningInventory");
            bool entryExists = Data.OpeningInventory.Exists(AppUsers.GetCurrentUserDB(),
                AppUsers.GetCurrent().View.OfficeId.ToInt()); 

            if (entryExists && !allowMultipleOpeningInventory)
            {
                this.CreateMessage(this.Placeholder1);
                return;
            }

            this.CreateGridView(this.Placeholder1);
            this.CreateBottomPanel(this.Placeholder1);
        }


        private void CreateMessage(Control container)
        {
            using (HtmlGenericControl message = new HtmlGenericControl("div"))
            {
                message.Attributes.Add("class", "ui positive message");
                message.InnerText = Labels.OpeningInventoryAlreadyEntered;

                container.Controls.Add(message);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.OpeningInventory;
                container.Controls.Add(header);
            }
        }

        private void CreateSaveButton(Control container)
        {
            using (HtmlInputButton saveButton = new HtmlInputButton())
            {
                saveButton.ID = "SaveButton";
                saveButton.Attributes.Add("class", "small ui button red");
                saveButton.Value = Titles.Save;

                container.Controls.Add(saveButton);
            }
        }

        #region IDisposable

        private bool disposed;
        private DateTextBox dateTextBox;

        public override sealed void Dispose()
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

            if (this.dateTextBox != null)
            {
                this.dateTextBox.Dispose();
                this.dateTextBox = null;
            }

            this.disposed = true;
        }

        #endregion

        #region Top Panel

        private void AddDateTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ValueDateTextBox");
                    label.InnerText = Titles.ValueDate;
                    field.Controls.Add(label);
                }

                this.dateTextBox = new DateTextBox();
                this.dateTextBox.ID = "ValueDateTextBox";
                this.dateTextBox.Mode = FrequencyType.Today;
                this.dateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.dateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.dateTextBox);

                fields.Controls.Add(field);
            }
        }

        private void AddReferenceNumberTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ReferenceNumberInputText");
                    label.InnerText = Titles.ReferenceNumber;
                    field.Controls.Add(label);
                }

                using (HtmlInputText referenceNumberInputText = new HtmlInputText())
                {
                    referenceNumberInputText.ID = "ReferenceNumberInputText";
                    referenceNumberInputText.MaxLength = 24;

                    field.Controls.Add(referenceNumberInputText);
                }
                fields.Controls.Add(field);
            }
        }

        private void CreateTopPanel(Control container)
        {
            using (HtmlGenericControl form = new HtmlGenericControl())
            {
                form.TagName = "div";
                form.Attributes.Add("class", "ui form");

                using (HtmlGenericControl fields = HtmlControlHelper.GetInlineFields())
                {
                    this.AddDateTextBox(fields);
                    this.AddReferenceNumberTextBox(fields);

                    form.Controls.Add(fields);
                }
                container.Controls.Add(form);
            }
        }

        #endregion

        #region Bottom Panel

        private void AddStatementReferenceTextArea(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "StatementReferenceTextArea");
                    label.InnerText = Titles.StatementReference;
                    field.Controls.Add(label);
                }

                using (HtmlTextArea statementReferenceTextArea = new HtmlTextArea())
                {
                    statementReferenceTextArea.ID = "StatementReferenceTextArea";
                    statementReferenceTextArea.Rows = 4;

                    field.Controls.Add(statementReferenceTextArea);
                }

                fields.Controls.Add(field);
            }
        }

        private void CreateBottomPanel(Control container)
        {
            using (HtmlGenericControl fields = HtmlControlHelper.GetInlineFields())
            {
                fields.TagName = "div";
                fields.Attributes.Add("class", "ui form");
                fields.Attributes.Add("style", "width:290px;");

                this.AddStatementReferenceTextArea(fields);
                this.CreateSaveButton(fields);

                container.Controls.Add(fields);
            }
        }

        #endregion

        #region GridView

        private void CreateGridView(Control container)
        {
            using (Table openingInventoryGridView = new Table())
            {
                openingInventoryGridView.CssClass = "ui table";
                openingInventoryGridView.ID = "OpeningInventoryGridView";
                this.CreateHeaderRow(openingInventoryGridView);
                this.CreateControlRow(openingInventoryGridView);
                container.Controls.Add(openingInventoryGridView);
            }
        }

        #region Header Row

        private void CreateHeaderRow(Table table)
        {
            using (TableRow headerRow = new TableRow())
            {
                this.AddHeaderCell(headerRow, "ItemCodeInputText", Titles.ItemCode, "");
                this.AddHeaderCell(headerRow, "ItemSelect", Titles.ItemName, "");
                this.AddHeaderCell(headerRow, "StoreSelect", Titles.StoreName, "");
                this.AddHeaderCell(headerRow, "QuantityInputText", Titles.Quantity, "integer text-right");
                this.AddHeaderCell(headerRow, "UnitSelect", Titles.Unit, "");
                this.AddHeaderCell(headerRow, "AmountInputText", Titles.Amount, "currency text-right");
                this.AddHeaderCell(headerRow, "TotalInputText", Titles.Total, "currency text-right");
                this.AddHeaderCell(headerRow, "AddRowButton", Titles.Action, "");


                table.Controls.Add(headerRow);
                headerRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void AddHeaderCell(TableRow row, string targetControlId, string labelText, string cssClass)
        {
            using (TableHeaderCell cell = new TableHeaderCell())
            {
                using (HtmlGenericControl label = new HtmlGenericControl("label"))
                {
                    label.Attributes.Add("for", targetControlId);
                    label.InnerText = labelText;
                    cell.Attributes.Add("class", cssClass);
                    cell.Controls.Add(label);
                }

                row.Controls.Add(cell);
            }
        }

        #endregion

        #region Control Row

        private void CreateControlRow(Table table)
        {
            using (TableRow controlRow = new TableRow())
            {
                controlRow.Attributes.Add("class", "ui footer-row form");
                this.AddInputTextControlCell(controlRow, "ItemCodeInputText", "", false);
                this.AddSelectControlCell(controlRow, "ItemSelect");
                this.AddSelectControlCell(controlRow, "StoreSelect");
                this.AddInputTextControlCell(controlRow, "QuantityInputText", "integer text-right", false);
                this.AddSelectControlCell(controlRow, "UnitSelect");
                this.AddInputTextControlCell(controlRow, "AmountInputText", "currency text-right", false);
                this.AddInputTextControlCell(controlRow, "TotalInputText", "currency text-right", true);
                this.AddInputButtonControlCell(controlRow, "AddRowButton", "small ui blue button", "Add");

                table.Controls.Add(controlRow);
            }
        }

        private void AddInputTextControlCell(TableRow row, string controlId, string cssClass, bool disabled)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText targetControl = new HtmlInputText())
                {
                    targetControl.ID = controlId;
                    targetControl.Attributes.Add("class", cssClass);

                    if (disabled)
                    {
                        targetControl.Attributes.Add("readonly", "readonly");
                    }

                    cell.Controls.Add(targetControl);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddSelectControlCell(TableRow row, string controlId)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlSelect targetControl = new HtmlSelect())
                {
                    targetControl.ID = controlId;

                    cell.Controls.Add(targetControl);
                }

                row.Controls.Add(cell);
            }
        }


        private void AddInputButtonControlCell(TableRow row, string controlId, string cssClass, string value)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputButton targetControl = new HtmlInputButton())
                {
                    targetControl.ID = controlId;
                    targetControl.Attributes.Add("class", cssClass);
                    targetControl.Value = value;

                    cell.Controls.Add(targetControl);
                }

                row.Controls.Add(cell);
            }

            #endregion
        }

        #endregion
    }
}