/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.ListControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes;
using MixERP.Net.WebControls.ScrudFactory.Controls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ScrudFactoryHelper
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Web.UI.WebControls.Literal.set_Text(System.String)")]
        public static void AddRow(HtmlTable htmlTable, string label, params Control[] controls)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (controls == null)
            {
                return;
            }

            if (controls.Length.Equals(0))
            {
                return;
            }

            using (var newRow = new HtmlTableRow())
            {
                using (var labelCell = new HtmlTableCell())
                {
                    using (var controlCell = new HtmlTableCell())
                    {
                        using (var labelLiteral = new Literal())
                        {
                            labelLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", controls[0].ID, label);
                            labelCell.Attributes.Add("class", "label-cell");

                            labelCell.Controls.Add(labelLiteral);

                            foreach (var control in controls)
                            {
                                if (control != null)
                                {
                                    controlCell.Controls.Add(control);
                                }
                            }

                            newRow.Cells.Add(labelCell);
                            newRow.Cells.Add(controlCell);
                            htmlTable.Rows.Add(newRow);
                        }
                    }
                }
            }
        }

        public static void AddField(HtmlTable htmlTable, string resourceClassName, string itemSelectorPath, string columnName, string defaultValue, bool isSerial, bool isNullable, string dataType, string domain, int maxLength, string parentTableSchema, string parentTable, string parentTableColumn, string displayFields, string displayViews, bool useDisplayFieldAsParent, string selectedValues)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

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
                        ScrudTextBox.AddTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, maxLength);
                        break;
                    case "smallint":
                    case "integer":
                    case "bigint":
                        ScrudNumberTextBox.AddNumberTextBox(htmlTable, resourceClassName, columnName, defaultValue, isSerial, isNullable, maxLength, domain);
                        break;
                    case "numeric":
                    case "money":
                    case "double":
                    case "double precision":
                    case "float":
                    case "real":
                    case "currency":
                        ScrudDecimalTextBox.AddDecimalTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, maxLength, domain);
                        break;
                    case "boolean":
                        ScrudRadioButtonList.AddRadioButtonList(htmlTable, resourceClassName, columnName, isNullable, ScrudResource.YesNo, "true,false", defaultValue);
                        break;
                    case "date":
                        ScrudDateTextBox.AddDateTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable);
                        break;
                    case "bytea":
                        ScrudFileUpload.AddFileUpload(htmlTable, resourceClassName, columnName, isNullable);
                        break;
                    case "timestamp with time zone":
                        //Do not show this field
                        break;
                }
            }
            else
            {
                //Todo: Add an implementation of overriding the behavior of the parent table data being populated into the list.
                ScrudDropDownList.AddDropDownList(htmlTable, resourceClassName, itemSelectorPath, columnName, isNullable, parentTableSchema, parentTable, parentTableColumn, defaultValue, displayFields, displayViews, useDisplayFieldAsParent ,selectedValues);
            }
        }

        public static RequiredFieldValidator GetRequiredFieldValidator(Control controlToValidate)
        {
            if (controlToValidate == null)
            {
                return null;
            }

            using (var validator = new RequiredFieldValidator())
            {
                validator.ID = controlToValidate.ID + "RequiredValidator";
                validator.ErrorMessage = @"<br/>" + ScrudResource.RequiredField;
                validator.CssClass = "form-error";
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;

                return validator;
            }
        }
    }
}
