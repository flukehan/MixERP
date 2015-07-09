using MixERP.Net.i18n.Resources;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddButton(TableRow row)
        {
            using (TableCell cell = this.GetCell())
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

        private void AddItemCodeInputText(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlInputText itemCodeInputText = new HtmlInputText())
                {
                    itemCodeInputText.ID = "ItemCodeInputText";

                    cell.Controls.Add(itemCodeInputText);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddItemSelect(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlSelect itemSelect = new HtmlSelect())
                {
                    itemSelect.ID = "ItemSelect";

                    cell.Controls.Add(itemSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddQuantityInputText(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlInputText quantityInputText = new HtmlInputText())
                {
                    quantityInputText.ID = "QuantityInputText";
                    quantityInputText.Attributes.Add("class", "text-right integer");
                    cell.Controls.Add(quantityInputText);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddStoreSelect(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlSelect storeSelect = new HtmlSelect())
                {
                    storeSelect.ID = "StoreSelect";

                    cell.Controls.Add(storeSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddTransactionTypeSelect(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlSelect transactionTypeSelect = new HtmlSelect())
                {
                    transactionTypeSelect.ID = "TransactionTypeSelect";

                    transactionTypeSelect.Items.Add(new ListItem(@"Cr", @"Cr"));
                    transactionTypeSelect.Items.Add(new ListItem(@"Dr", @"Dr"));

                    cell.Controls.Add(transactionTypeSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private void AddUnitSelect(TableRow row)
        {
            using (TableCell cell = this.GetCell())
            {
                using (HtmlSelect unitSelect = new HtmlSelect())
                {
                    unitSelect.ID = "UnitSelect";
                    cell.Controls.Add(unitSelect);
                }

                row.Controls.Add(cell);
            }
        }

        private TableCell GetCell()
        {
            using (TableCell cell = new TableCell())
            {
                return cell;
            }
        }

        private TableRow GetControlRow()
        {
            using (TableRow row = new TableRow())
            {
                row.Attributes.Add("class", "footer-row ui form");

                if (!this.HideSides)
                {
                    this.AddTransactionTypeSelect(row);
                }

                this.AddStoreSelect(row);
                this.AddItemCodeInputText(row);
                this.AddItemSelect(row);
                this.AddUnitSelect(row);
                this.AddQuantityInputText(row);
                this.AddButton(row);
                return row;
            }
        }
    }
}