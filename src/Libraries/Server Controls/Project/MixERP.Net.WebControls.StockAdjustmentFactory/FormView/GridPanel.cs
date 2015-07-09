using MixERP.Net.i18n.Resources;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void CreateGridPanel()
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl("div"))
            {
                gridPanel.Attributes.Add("style", "width: 100%;overflow:auto;");

                using (Table table = new Table())
                {
                    table.ID = "TransferGridView";
                    table.Attributes.Add("class", "ui table segment");
                    table.Attributes.Add("style", "min-width:1000px;max-width:2000px;");

                    table.Rows.Add(this.GetHeaderRow());
                    table.Rows.Add(this.GetControlRow());

                    gridPanel.Controls.Add(table);
                }

                this.container.Controls.Add(gridPanel);
            }
        }

        private TableHeaderCell GetHeaderCell(string innerText, string AssociatedControlId, bool alignRight, int width)
        {
            using (TableHeaderCell header = new TableHeaderCell())
            {
                if (alignRight)
                {
                    header.Attributes.Add("class", "text-right");
                }

                if (width > 0)
                {
                    header.Attributes.Add("style", "width:" + width.ToString(CultureInfo.InvariantCulture) + "px;");
                }

                if (string.IsNullOrWhiteSpace(AssociatedControlId))
                {
                    header.Text = innerText;
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

        private TableHeaderRow GetHeaderRow()
        {
            using (TableHeaderRow row = new TableHeaderRow())
            {
                row.TableSection = TableRowSection.TableHeader;

                if (!this.HideSides)
                {
                    row.Cells.Add(this.GetHeaderCell(Titles.Type, "TransactionTypeSelect", false, 80));
                }

                row.Cells.Add(this.GetHeaderCell(Titles.Store, "StoreSelect", false, 140));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemCode, "ItemCodeInputText", false, 90));
                row.Cells.Add(this.GetHeaderCell(Titles.ItemName, "ItemSelect", false, 0));
                row.Cells.Add(this.GetHeaderCell(Titles.Unit, "UnitSelect", false, 120));
                row.Cells.Add(this.GetHeaderCell(Titles.Quantity, "QuantityInputText", true, 120));
                row.Cells.Add(this.GetHeaderCell(Titles.Action, null, false, 120));

                return row;
            }
        }
    }
}