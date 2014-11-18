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

using MixERP.Net.WebControls.ScrudFactory.Controls;
using MixERP.Net.WebControls.ScrudFactory.Controls.ListControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Reflection;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    internal static class ScrudFactoryHelper
    {
        internal static void AddDropDownList(HtmlTable htmlTable, string label, DropDownList dropDownList, HtmlAnchor itemSelectorAnchor, RequiredFieldValidator required)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (dropDownList == null)
            {
                return;
            }

            using (var labelCell = new HtmlTableCell())
            {
                using (var controlCell = new HtmlTableCell())
                {
                    using (var labelLiteral = new Literal())
                    {
                        labelLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", dropDownList.ID, label);
                        labelCell.Attributes.Add("class", "label-cell");

                        labelCell.Controls.Add(labelLiteral);
                        controlCell.Attributes.Add("class", "control-cell");

                        using (HtmlTable t = new HtmlTable())
                        {
                            using (HtmlTableRow row = new HtmlTableRow())
                            {
                                using (HtmlTableCell cell1 = new HtmlTableCell())
                                {
                                    cell1.Controls.Add(dropDownList);

                                    if (required != null)
                                    {
                                        cell1.Controls.Add(required);
                                    }

                                    row.Cells.Add(cell1);
                                }

                                using (HtmlTableCell cell2 = new HtmlTableCell())
                                {
                                    if (itemSelectorAnchor != null)
                                    {
                                        cell2.Controls.Add(itemSelectorAnchor);
                                        cell2.Style.Add("width", "24px");
                                    }

                                    row.Cells.Add(cell2);
                                }

                                t.Style.Add("width", "100%");
                                t.Style.Add("border-collapse", "collapse");
                                t.Rows.Add(row);
                                t.Attributes.Add("role", "item-selector-table");

                                controlCell.Controls.Add(t);

                                using (var newRow = new HtmlTableRow())
                                {
                                    newRow.Cells.Add(labelCell);
                                    newRow.Cells.Add(controlCell);
                                    htmlTable.Rows.Add(newRow);
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void AddField(HtmlTable htmlTable, string resourceClassName, string itemSelectorPath, string columnName, string defaultValue, bool isSerial, bool isNullable, string dataType, string domain, int maxLength, string parentTableSchema, string parentTable, string parentTableColumn, string displayFields, string displayViews, bool useDisplayFieldAsParent, string selectedValues, string errorCssClass, Assembly assembly)
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
                        ScrudTextBox.AddTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, maxLength, errorCssClass, assembly);
                        break;

                    case "smallint":
                    case "integer":
                    case "bigint":
                        ScrudNumberTextBox.AddNumberTextBox(htmlTable, resourceClassName, columnName, defaultValue, isSerial, isNullable, maxLength, domain, errorCssClass, assembly);
                        break;

                    case "numeric":
                    case "money":
                    case "double":
                    case "double precision":
                    case "float":
                    case "real":
                    case "currency":
                        ScrudDecimalTextBox.AddDecimalTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, maxLength, domain, errorCssClass, assembly);
                        break;

                    case "boolean":
                        ScrudRadioButtonList.AddRadioButtonList(htmlTable, resourceClassName, columnName, isNullable, Titles.YesNo, "true,false", defaultValue, errorCssClass, assembly);
                        break;

                    case "date":
                        ScrudDateTextBox.AddDateTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, errorCssClass, assembly);
                        break;

                    case "bytea":
                        ScrudFileUpload.AddFileUpload(htmlTable, resourceClassName, columnName, isNullable, errorCssClass, assembly);
                        break;

                    case "timestamp with time zone":
                        //Do not show this field
                        break;
                }
            }
            else
            {
                //Todo: Add an implementation of overriding the behavior of the parent table data being populated into the list.
                ScrudDropDownList.AddDropDownList(htmlTable, resourceClassName, itemSelectorPath, columnName, isNullable, parentTableSchema, parentTable, parentTableColumn, defaultValue, displayFields, displayViews, useDisplayFieldAsParent, selectedValues, errorCssClass, assembly);
            }
        }

        internal static void AddRow(HtmlTable htmlTable, string label, params Control[] controls)
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

            using (var labelCell = new HtmlTableCell())
            {
                using (var controlCell = new HtmlTableCell())
                {
                    using (var labelLiteral = new Literal())
                    {
                        labelLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", controls[0].ID, label);
                        labelCell.Attributes.Add("class", "label-cell");

                        labelCell.Controls.Add(labelLiteral);
                        controlCell.Attributes.Add("class", "control-cell");

                        foreach (var control in controls)
                        {
                            if (control != null)
                            {
                                if (control is WebControl)
                                {
                                    using (WebControl c = control as WebControl)
                                    {
                                        controlCell.Controls.Add(c);
                                    }
                                }
                                else
                                {
                                    controlCell.Controls.Add(control);
                                }
                            }
                        }

                        using (var newRow = new HtmlTableRow())
                        {
                            newRow.Cells.Add(labelCell);
                            newRow.Cells.Add(controlCell);
                            htmlTable.Rows.Add(newRow);
                        }
                    }
                }
            }
        }

        internal static RequiredFieldValidator GetRequiredFieldValidator(Control controlToValidate, string cssClass)
        {
            if (controlToValidate == null)
            {
                return null;
            }

            using (var validator = new RequiredFieldValidator())
            {
                validator.ID = controlToValidate.ID + "RequiredValidator";
                validator.ErrorMessage = @"<br/>" + Titles.RequiredField;
                validator.CssClass = cssClass;
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;

                return validator;
            }
        }
    }
}