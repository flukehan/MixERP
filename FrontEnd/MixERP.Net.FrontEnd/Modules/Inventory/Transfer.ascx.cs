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

using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Inventory
{
    public partial class Transfer : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateTopPanel();
            this.CreateGridPanel();
            this.AddErrorLabelBottom();
            this.CreateBottomPanel();
            base.OnControlLoad(sender, e);
        }

        private void CreateTopPanel()
        {
            this.CreateHeader();
            this.AddRuler();
            using (HtmlGenericControl form = new HtmlGenericControl())
            {
                form.TagName = "div";
                form.Attributes.Add("class", "ui form");

                using (HtmlGenericControl fields = this.GetFields())
                {
                    this.AddDateTextBox(fields);
                    this.AddReferenceNumberTextBox(fields);
                    form.Controls.Add(fields);
                }
                this.Placeholder1.Controls.Add(form);
            }
        }

        private HtmlGenericControl GetFields()
        {
            using (HtmlGenericControl row = new HtmlGenericControl())
            {
                row.TagName = "div";
                row.Attributes.Add("class", "inline fields");

                return row;
            }
        }

        private void AddDateTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = this.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ValueDateTextBox");
                    label.InnerText = Titles.ValueDate;
                    field.Controls.Add(label);
                }

                using (DateTextBox dateTextBox = new DateTextBox())
                {
                    dateTextBox.ID = "ValueDateTextBox";
                    dateTextBox.Mode = Frequency.Today;
                    field.Controls.Add(dateTextBox);
                }

                fields.Controls.Add(field);
            }
        }

        private HtmlGenericControl GetField()
        {
            using (HtmlGenericControl formGroup = new HtmlGenericControl())
            {
                formGroup.TagName = "div";
                formGroup.Attributes.Add("class", "small field");

                return formGroup;
            }
        }

        private void AddReferenceNumberTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = this.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ReferenceNumberInputText");
                    label.InnerText = Titles.RefererenceNumberAbbreviated;
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

        private void AddErrorLabelBottom()
        {
            using (HtmlGenericControl errorLabel = new HtmlGenericControl())
            {
                errorLabel.TagName = "div";
                errorLabel.ID = "ErrorLabel";
                errorLabel.Attributes.Add("class", "big error vpad16");
                this.Placeholder1.Controls.Add(errorLabel);
            }
        }

        private void CreateHeader()
        {
            using (HtmlGenericControl header = new HtmlGenericControl())
            {
                header.TagName = "h2";
                header.InnerText = Titles.StockTransferJournal;
                this.Placeholder1.Controls.Add(header);
            }
        }

        private void AddRuler()
        {
            using (HtmlGenericControl divider = new HtmlGenericControl("div"))
            {
                divider.Attributes.Add("class", "ui divider");
                this.Placeholder1.Controls.Add(divider);
            }
        }

        private void CreateGridPanel()
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl("div"))
            {
                gridPanel.Attributes.Add("style", "width: 1100px;");

                using (HtmlTable table = new HtmlTable())
                {
                    table.ID = "TransferGridView";
                    table.Attributes.Add("class", "ui table form segment");
                    table.Rows.Add(this.GetHeaderRow());
                    table.Rows.Add(this.GetControlRow());

                    gridPanel.Controls.Add(table);
                }

                this.Placeholder1.Controls.Add(gridPanel);
            }
        }

        private HtmlTableRow GetHeaderRow()
        {
            using (HtmlTableRow row = new HtmlTableRow())
            {
                row.Cells.Add(this.GetHeaderCell(Titles.Type, "TransactionTypeSelect", false));
                row.Cells.Add(this.GetHeaderCell(Titles.Store, "StoreSelect", false));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemCode, "ItemCodeInputText", false));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemName, "ItemSelect", false));
                row.Cells.Add(this.GetHeaderCell(Titles.Unit, "UnitSelect", false));
                row.Cells.Add(this.GetHeaderCell(Titles.Quantity, "QuantityInputText", true));
                row.Cells.Add(this.GetHeaderCell(Titles.Action, null, false));

                return row;
            }
        }

        private HtmlTableCell GetHeaderCell(string innerText, string AssociatedControlId, bool alignRight)
        {
            using (HtmlTableCell header = new HtmlTableCell("th"))
            {
                if (alignRight)
                {
                    header.Attributes.Add("class", "text-right");
                }

                if (string.IsNullOrWhiteSpace(AssociatedControlId))
                {
                    header.InnerText = innerText;
                }
                else
                {
                    using (HtmlGenericControl label = new HtmlGenericControl())
                    {
                        label.TagName = "label";
                        label.Attributes.Add("for", AssociatedControlId);
                        label.InnerText = innerText;

                        header.Controls.Add(label);
                    }
                }

                return header;
            }
        }

        private HtmlTableRow GetControlRow()
        {
            using (HtmlTableRow row = new HtmlTableRow())
            {
                row.Attributes.Add("class", "footer-row");

                this.AddTransactionTypeSelect(row);
                this.AddStoreSelect(row);
                this.AddItemCodeInputText(row);
                this.AddItemSelect(row);
                this.AddUnitSelect(row);
                this.AddQuantityInputText(row);
                this.AddButton(row);
                return row;
            }
        }

        private void AddTransactionTypeSelect(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlSelect transactionTypeSelect = new HtmlSelect())
                {
                    transactionTypeSelect.ID = "TransactionTypeSelect";

                    transactionTypeSelect.Items.Add(new ListItem(@"Dr", @"Dr"));
                    transactionTypeSelect.Items.Add(new ListItem(@"Cr", @"Cr"));

                    cell.Controls.Add(transactionTypeSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddStoreSelect(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlSelect storeSelect = new HtmlSelect())
                {
                    storeSelect.ID = "StoreSelect";

                    cell.Controls.Add(storeSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddItemCodeInputText(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlInputText itemCodeInputText = new HtmlInputText())
                {
                    itemCodeInputText.ID = "ItemCodeInputText";

                    cell.Controls.Add(itemCodeInputText);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddItemSelect(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlSelect itemSelect = new HtmlSelect())
                {
                    itemSelect.ID = "ItemSelect";

                    cell.Controls.Add(itemSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddUnitSelect(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlSelect unitSelect = new HtmlSelect())
                {
                    unitSelect.ID = "UnitSelect";
                    cell.Controls.Add(unitSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddQuantityInputText(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlInputText quantityInputText = new HtmlInputText())
                {
                    quantityInputText.ID = "QuantityInputText";
                    quantityInputText.Attributes.Add("class", "text-right");
                    cell.Controls.Add(quantityInputText);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddButton(HtmlTableRow row)
        {
            using (HtmlTableCell cell = this.GetCell())
            {
                using (HtmlInputButton addButton = new HtmlInputButton())
                {
                    addButton.ID = "AddButton";
                    addButton.Value = Titles.Add;
                    addButton.Attributes.Add("class", "ui small red button");

                    cell.Controls.Add(addButton);
                }

                row.Controls.Add(cell);
            }
        }

        private HtmlTableCell GetCell()
        {
            using (HtmlTableCell cell = new HtmlTableCell("td"))
            {
                return cell;
            }
        }

        private void CreateBottomPanel()
        {
            using (HtmlGenericControl fields = this.GetFields())
            {
                fields.TagName = "div";
                fields.Attributes.Add("class", "ui form");
                fields.Attributes.Add("style", "width:290px;");

                this.AddStatementReferenceTextArea(fields);
                this.AddSaveButton(fields);

                this.Placeholder1.Controls.Add(fields);
            }
        }

        private void AddStatementReferenceTextArea(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = this.GetField())
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

        private void AddSaveButton(HtmlGenericControl container)
        {
            using (HtmlInputButton button = new HtmlInputButton())
            {
                button.ID = "SaveButton";
                button.Value = Titles.Save;
                button.Attributes.Add("class", "ui small blue button");
                container.Controls.Add(button);
            }
        }
    }
}