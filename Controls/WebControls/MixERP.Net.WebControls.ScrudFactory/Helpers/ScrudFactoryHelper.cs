using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.ListControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes;
using MixERP.Net.WebControls.ScrudFactory.Controls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public class ScrudFactoryHelper
    {
        public static void AddRow(HtmlTable table, string label, params Control[] controls)
        {
            HtmlTableRow newRow = new HtmlTableRow();
            HtmlTableCell labelCell = new HtmlTableCell();
            HtmlTableCell controlCell = new HtmlTableCell();
            Literal labelLiteral = new Literal();

            labelLiteral.Text = string.Format(System.Threading.Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", controls[0].ID, label);
            labelCell.Attributes.Add("class", "label-cell");

            labelCell.Controls.Add(labelLiteral);

            foreach (Control control in controls)
            {
                if (control != null)
                {
                    controlCell.Controls.Add(control);
                }
            }

            newRow.Cells.Add(labelCell);
            newRow.Cells.Add(controlCell);
            table.Rows.Add(newRow);
        }

        public static void AddField(HtmlTable t, string columnName, string defaultValue, bool isSerial, bool isNullable, string dataType, string domain, int maxLength, string parentTableSchema, string parentTable, string parentTableColumn, string displayFields, string displayViews, string selectedValues)
        {
            if (string.IsNullOrWhiteSpace(parentTableColumn))
            {
                switch (dataType)
                {
                    case "national character varying":
                    case "character varying":
                    case "national character":
                    case "character":
                    case "char":
                    case "varchar":
                    case "nvarchar":
                    case "text":
                        ScrudTextBox.AddTextBox(t, columnName, defaultValue, isNullable, maxLength);
                        break;
                    case "smallint":
                    case "integer":
                    case "bigint":
                        ScrudNumberTextBox.AddNumberTextBox(t, columnName, defaultValue, isSerial, isNullable, maxLength, domain);
                        break;
                    case "numeric":
                    case "money":
                    case "double":
                    case "double precision":
                    case "float":
                    case "real":
                    case "currency":
                        ScrudDecimalTextBox.AddDecimalTextBox(t, columnName, defaultValue, isNullable, maxLength, domain);
                        break;
                    case "boolean":
                        ScrudRadioButtonList.AddRadioButtonList(t, columnName, isNullable, Resources.ScrudResource.YesNo, "true,false", defaultValue);
                        break;
                    case "date":
                        ScrudDateTextBox.AddDateTextBox(t, columnName, defaultValue, isNullable);
                        break;
                    case "bytea":
                        ScrudFileUpload.AddFileUpload(t, columnName, isNullable);
                        break;
                    case "timestamp with time zone":
                        //Do not show this field
                        break;
                }
            }
            else
            {
                ScrudDropDownList.AddDropDownList(t, columnName, isNullable, parentTableSchema, parentTable, parentTableColumn, defaultValue, displayFields, displayViews, selectedValues);
            }
        }

        public static RequiredFieldValidator GetRequiredFieldValidator(Control controlToValidate)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ID = controlToValidate.ID + "RequiredValidator";
            validator.ErrorMessage = "<br/>" + Resources.ScrudResource.RequiredField;
            validator.CssClass = "form-error";
            validator.ControlToValidate = controlToValidate.ID;
            validator.EnableClientScript = true;
            validator.SetFocusOnError = true;
            validator.Display = ValidatorDisplay.Dynamic;

            return validator;
        }
    }
}
