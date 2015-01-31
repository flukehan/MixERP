using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;

namespace MixERP.Net.Core.Modules.BackOffice.OTS
{
    public partial class OpeningInventory : MixERPUserControl
    {

        public override void OnControlLoad(object sender, EventArgs e)
        {            
            this.CreateHeader(this.Placeholder1);

            if (Data.OpeningInventory.Exists(CurrentSession.GetOfficeId()))
            {
                this.CreateMessage(this.Placeholder1);
                return;
            }

            this.CreateGridView(this.Placeholder1);
            this.CreateSaveButton(this.Placeholder1);
        }

        private void CreateMessage(Control container)
        {
            using (HtmlGenericControl message = new HtmlGenericControl("div"))
            {
                message.Attributes.Add("class", "ui positive message");
                message.InnerText = "Opening inventory has already been entered for this office.";

                container.Controls.Add(message);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = "Opening Inventory";
                container.Controls.Add(header);
            }
        }

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
                this.AddHeaderCell(headerRow, "ItemCodeInputText", "Item Code", "");
                this.AddHeaderCell(headerRow, "ItemSelect", "Item Name","");
                this.AddHeaderCell(headerRow, "StoreSelect", "Store Name", "");
                this.AddHeaderCell(headerRow, "QuantityInputText", "Quantity", "integer text-right");
                this.AddHeaderCell(headerRow, "UnitSelect", "Unit", "");
                this.AddHeaderCell(headerRow, "AmountInputText", "Amount", "currency text-right");
                this.AddHeaderCell(headerRow, "TotalInputText", "Total", "currency text-right");
                this.AddHeaderCell(headerRow, "AddRowButton", "Action", "");
               

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

        private void CreateSaveButton(Control container)
        {
            using (HtmlInputButton saveButton = new HtmlInputButton())
            {
                saveButton.ID = "SaveButton";
                saveButton.Attributes.Add("class", "small ui button red");
                saveButton.Value = "Save";

                container.Controls.Add(saveButton);
            }
        }

    }
}