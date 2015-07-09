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

using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.ScrudFactory.Controls;
using MixERP.Net.WebControls.ScrudFactory.Controls.ListControls;
using MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes;
using System.Linq;
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

            using (HtmlTableCell labelCell = new HtmlTableCell())
            {
                using (HtmlTableCell controlContainer = new HtmlTableCell())
                {
                    using (Literal labelLiteral = new Literal())
                    {
                        labelLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", dropDownList.ID, label);
                        labelCell.Attributes.Add("class", "label-cell");

                        labelCell.Controls.Add(labelLiteral);
                        controlContainer.Attributes.Add("class", "control-cell");

                        using (HtmlTable controlTable = new HtmlTable())
                        {
                            using (HtmlTableRow row = new HtmlTableRow())
                            {
                                using (HtmlTableCell controlCell = new HtmlTableCell())
                                {
                                    controlCell.Controls.Add(dropDownList);

                                    if (required != null)
                                    {
                                        controlCell.Controls.Add(required);
                                    }

                                    row.Cells.Add(controlCell);
                                }

                                using (HtmlTableCell itemSelectorCell = new HtmlTableCell())
                                {
                                    if (itemSelectorAnchor != null)
                                    {
                                        itemSelectorCell.Controls.Add(itemSelectorAnchor);
                                        itemSelectorCell.Style.Add("width", "24px");
                                    }

                                    row.Cells.Add(itemSelectorCell);
                                }

                                controlTable.Style.Add("width", "100%");
                                controlTable.Style.Add("border-collapse", "collapse");
                                controlTable.Rows.Add(row);
                                controlTable.Attributes.Add("role", "item-selector-table");

                                controlContainer.Controls.Add(controlTable);

                                using (HtmlTableRow newRow = new HtmlTableRow())
                                {
                                    newRow.Cells.Add(labelCell);
                                    newRow.Cells.Add(controlContainer);
                                    htmlTable.Rows.Add(newRow);
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void AddField(string catalog, HtmlTable htmlTable, string resourceClassName, string itemSelectorPath, string columnName, string defaultValue, bool isSerial, bool isNullable, string dataType, string domain, int maxLength, string parentTableSchema, string parentTable, string parentTableColumn, string displayFields, string displayViews, bool useDisplayFieldAsParent, string selectedValues, string errorCssClass, bool disabled)
        {
            if (isSerial)
            {
                disabled = true;
            }

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
                if (ScrudTypes.Strings.Contains(dataType))
                {
                    ScrudTextBox.AddTextBox(htmlTable, resourceClassName, columnName, dataType, defaultValue, isNullable, maxLength, errorCssClass, disabled);
                }

                if (ScrudTypes.Shorts.Contains(dataType) || ScrudTypes.Integers.Contains(dataType) || ScrudTypes.Longs.Contains(dataType))
                {
                    ScrudNumberTextBox.AddNumberTextBox(htmlTable, resourceClassName, columnName, defaultValue, isSerial, isNullable, maxLength, domain, errorCssClass, disabled);
                }

                if (ScrudTypes.Decimals.Contains(dataType))
                {
                    ScrudDecimalTextBox.AddDecimalTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, maxLength, domain, errorCssClass, disabled);
                }

                if (ScrudTypes.Bools.Contains(dataType))
                {
                    ScrudRadioButtonList.AddRadioButtonList(htmlTable, resourceClassName, columnName, isNullable, Titles.Yes + "," + Titles.No, "true,false", defaultValue, errorCssClass, disabled);
                }

                if (ScrudTypes.Dates.Contains(dataType))
                {
                    ScrudDateTextBox.AddDateTextBox(htmlTable, resourceClassName, columnName, defaultValue, isNullable, errorCssClass, disabled);
                }

                if (ScrudTypes.Files.Contains(dataType))
                {
                    ScrudFileUpload.AddFileUpload(htmlTable, resourceClassName, columnName, isNullable, errorCssClass);
                }
            }
            else
            {
                ScrudDropDownList.AddDropDownList(catalog, htmlTable, resourceClassName, itemSelectorPath, columnName, isNullable, parentTableSchema, parentTable, parentTableColumn, defaultValue, displayFields, displayViews, useDisplayFieldAsParent, selectedValues, errorCssClass, disabled);
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

            using (HtmlTableCell labelCell = new HtmlTableCell())
            {
                using (HtmlTableCell controlCell = new HtmlTableCell())
                {
                    using (Literal labelLiteral = new Literal())
                    {
                        labelLiteral.Text = string.Format(Thread.CurrentThread.CurrentCulture, "<label for='{0}'>{1}</label>", controls[0].ID, label);
                        labelCell.Attributes.Add("class", "label-cell");

                        labelCell.Controls.Add(labelLiteral);
                        controlCell.Attributes.Add("class", "control-cell");

                        foreach (Control control in controls)
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

                        using (HtmlTableRow newRow = new HtmlTableRow())
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

            using (RequiredFieldValidator validator = new RequiredFieldValidator())
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