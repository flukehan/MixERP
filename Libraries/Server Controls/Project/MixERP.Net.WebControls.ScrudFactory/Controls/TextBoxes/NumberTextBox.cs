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

using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    internal static class ScrudNumberTextBox
    {
        internal static void AddNumberTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isSerial, bool isNullable, int maxLength, string domain, string errorCssClass, Assembly assembly)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

            var textBox = GetNumberTextBox(columnName + "_textbox", maxLength);
            var numberValidator = GetNumberValidator(textBox, domain, errorCssClass);
            var label = ScrudLocalizationHelper.GetResourceString(assembly, resourceClassName, columnName);

            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                if (!defaultValue.StartsWith("nextVal", StringComparison.OrdinalIgnoreCase))
                {
                    textBox.Text = defaultValue;
                }
            }

            if (isSerial)
            {
                textBox.ReadOnly = true;
            }
            else
            {
                if (!isNullable)
                {
                    var required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox, errorCssClass);
                    ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, textBox,
                        numberValidator, required);
                    return;
                }
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, textBox, numberValidator);
        }

        private static TextBox GetNumberTextBox(string id, int maxLength)
        {
            var textBox = ScrudTextBox.GetTextBox(id, maxLength);
            //textBox.Attributes["type"] = "number";
            return textBox;
        }

        private static CompareValidator GetNumberValidator(Control controlToValidate, string domain, string cssClass)
        {
            using (var validator = new CompareValidator())
            {
                validator.ID = controlToValidate.ID + "NumberValidator";
                validator.ErrorMessage = @"<br/>" + Titles.OnlyNumbersAllowed;
                validator.CssClass = cssClass;
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;
                validator.Type = ValidationDataType.Integer;

                //MixERP strict data type
                if (domain.EndsWith("strict"))
                {
                    validator.Operator = ValidationCompareOperator.GreaterThan;
                }
                else
                {
                    validator.Operator = ValidationCompareOperator.GreaterThanEqual;
                }

                validator.ValueToCompare = "0";

                return validator;
            }
        }
    }
}