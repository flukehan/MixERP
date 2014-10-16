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
            using (HtmlGenericControl shadeDiv = new HtmlGenericControl())
            {
                shadeDiv.TagName = "div";
                shadeDiv.Attributes.Add("class", "shade");
                shadeDiv.Attributes.Add("style", "width:300px");

                using (HtmlGenericControl rowDiv = this.GetRow())
                {
                    this.AddDateTextBox(rowDiv);
                    this.AddReferenceNumberTextBox(rowDiv);
                    shadeDiv.Controls.Add(rowDiv);
                }
                this.Placeholder1.Controls.Add(shadeDiv);
            }
        }

        private HtmlGenericControl GetRow()
        {
            using (HtmlGenericControl row = new HtmlGenericControl())
            {
                row.TagName = "div";
                row.Attributes.Add("class", "row");
                row.Attributes.Add("style", "margin-left: 2px;");

                return row;
            }
        }

        private void AddDateTextBox(HtmlGenericControl container)
        {
            using (HtmlGenericControl columnDiv = this.GetColumn(5))
            {
                using (HtmlGenericControl formGroup = this.GetFormGroup())
                {
                    using (HtmlGenericControl label = new HtmlGenericControl())
                    {
                        label.TagName = "label";
                        label.Attributes.Add("for", "DateTextBox");
                        label.InnerText = Titles.ValueDate;
                        formGroup.Controls.Add(label);
                    }

                    using (DateTextBox dateTextBox = new DateTextBox())
                    {
                        dateTextBox.ID = "ValueDateTextBox";
                        dateTextBox.Mode = Frequency.Today;
                        dateTextBox.CssClass = "date form-control input-sm";
                        formGroup.Controls.Add(dateTextBox);
                    }

                    columnDiv.Controls.Add(formGroup);
                }
                container.Controls.Add(columnDiv);
            }
        }

        private HtmlGenericControl GetFormGroup()
        {
            using (HtmlGenericControl formGroup = new HtmlGenericControl())
            {
                formGroup.TagName = "div";
                formGroup.Attributes.Add("class", "form-group");

                return formGroup;
            }
        }

        private HtmlGenericControl GetColumn(int colSpan)
        {
            using (HtmlGenericControl column = new HtmlGenericControl())
            {
                if (colSpan == 0)
                {
                    colSpan = 12;
                }

                column.TagName = "div";
                column.Attributes.Add("class", "col-xs-" + colSpan + " pad4");

                return column;
            }
        }

        private void AddReferenceNumberTextBox(HtmlGenericControl container)
        {
            using (HtmlGenericControl columnDiv = this.GetColumn(5))
            {
                using (HtmlGenericControl formGroup = this.GetFormGroup())
                {
                    using (HtmlGenericControl label = new HtmlGenericControl())
                    {
                        label.TagName = "label";
                        label.Attributes.Add("for", "ReferenceNumberInputText");
                        label.InnerText = Titles.RefererenceNumberAbbreviated;
                        formGroup.Controls.Add(label);
                    }

                    using (HtmlInputText referenceNumberInputText = new HtmlInputText())
                    {
                        referenceNumberInputText.ID = "ReferenceNumberInputText";
                        referenceNumberInputText.Attributes.Add("class", "form-control input-sm");
                        referenceNumberInputText.MaxLength = 24;

                        formGroup.Controls.Add(referenceNumberInputText);
                    }

                    columnDiv.Controls.Add(formGroup);
                }
                container.Controls.Add(columnDiv);
            }
        }

        private void AddErrorLabelBottom()
        {
            using (HtmlGenericControl errorLabel = new HtmlGenericControl())
            {
                errorLabel.TagName = "div";
                errorLabel.ID = "ErrorLabel";
                errorLabel.Attributes.Add("class", "error");
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
            using (HtmlGenericControl ruler = new HtmlGenericControl())
            {
                ruler.TagName = "hr";
                this.Placeholder1.Controls.Add(ruler);
            }
        }

        private void CreateGridPanel()
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl())
            {
                gridPanel.TagName = "div";
                gridPanel.Attributes.Add("style", "width: 1100px;");

                using (HtmlTable table = new HtmlTable())
                {
                    table.ID = "TransferGridView";
                    table.Attributes.Add("class", "table table-hover");
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
                row.Cells.Add(this.GetHeaderCell(Titles.Type, "TransactionTypeSelect"));
                row.Cells.Add(this.GetHeaderCell(Titles.Store, "StoreSelect"));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemCode, "ItemCodeInputText"));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemName, "ItemSelect"));
                row.Cells.Add(this.GetHeaderCell(Titles.Unit, "UnitSelect"));
                row.Cells.Add(this.GetHeaderCell(Titles.Quantity, "QuantityInputText"));
                row.Cells.Add(this.GetHeaderCell(Titles.Action, null));

                return row;
            }
        }

        private HtmlTableCell GetHeaderCell(string innerText, string AssociatedControlId)
        {
            using (HtmlTableCell header = new HtmlTableCell("th"))
            {
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
                    transactionTypeSelect.Attributes.Add("class", "form-control input-sm");

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
                    storeSelect.Attributes.Add("class", "form-control input-sm");

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
                    itemCodeInputText.Attributes.Add("class", "form-control input-sm");

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
                    itemSelect.Attributes.Add("class", "form-control input-sm");

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
                    unitSelect.Attributes.Add("class", "form-control input-sm");

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
                    quantityInputText.Attributes.Add("class", "form-control input-sm number");

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
                    addButton.Attributes.Add("class", "btn btn-sm btn-primary");

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
            using (HtmlGenericControl shadeDiv = new HtmlGenericControl())
            {
                shadeDiv.TagName = "div";
                shadeDiv.Attributes.Add("class", "shade");
                shadeDiv.Attributes.Add("style", "width:300px");

                using (HtmlGenericControl rowDiv = this.GetRow())
                {
                    rowDiv.TagName = "div";
                    rowDiv.Attributes.Add("style", "margin-left: 2px;");

                    this.AddStatementReferenceTextArea(rowDiv);
                    this.AddSaveButton(rowDiv);

                    shadeDiv.Controls.Add(rowDiv);
                }

                this.Placeholder1.Controls.Add(shadeDiv);
            }
        }

        private void AddStatementReferenceTextArea(HtmlGenericControl container)
        {
            using (HtmlGenericControl columnDiv = this.GetColumn(11))
            {
                using (HtmlGenericControl formGroup = this.GetFormGroup())
                {
                    using (HtmlGenericControl label = new HtmlGenericControl())
                    {
                        label.TagName = "label";
                        label.Attributes.Add("for", "StatementReferenceTextArea");
                        label.InnerText = Titles.StatementReference;
                        formGroup.Controls.Add(label);
                    }

                    using (HtmlTextArea statementReferenceTextArea = new HtmlTextArea())
                    {
                        statementReferenceTextArea.ID = "StatementReferenceTextArea";
                        statementReferenceTextArea.Attributes.Add("class", "form-control input-sm");
                        statementReferenceTextArea.Rows = 4;

                        formGroup.Controls.Add(statementReferenceTextArea);
                    }

                    columnDiv.Controls.Add(formGroup);
                }
                container.Controls.Add(columnDiv);
            }
        }

        private void AddSaveButton(HtmlGenericControl container)
        {
            using (HtmlGenericControl columnDiv = this.GetColumn(12))
            {
                using (HtmlInputButton button = new HtmlInputButton())
                {
                    button.ID = "SaveButton";
                    button.Value = Titles.Save;
                    button.Attributes.Add("class", "btn btn-default btn-sm");
                    columnDiv.Controls.Add(button);
                }

                container.Controls.Add(columnDiv);
            }
        }
    }
}