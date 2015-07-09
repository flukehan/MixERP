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
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    internal static class ScrudDecimalTextBox
    {
        internal static void AddDecimalTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable, int maxLength, string domain, string errorCssClass, bool disabled)
        {
            using (TextBox textBox = ScrudTextBox.GetTextBox(columnName + "_textbox", maxLength))
            {
                string label = ScrudLocalizationHelper.GetResourceString(resourceClassName, columnName);

                using (CompareValidator validator = GetDecimalValidator(textBox, domain, errorCssClass))
                {
                    textBox.Text = defaultValue;
                    textBox.ReadOnly = disabled;
                    textBox.CssClass = "decimal";

                    if (!isNullable)
                    {
                        RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox, errorCssClass);
                        ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, textBox, validator, required);
                        return;
                    }

                    ScrudFactoryHelper.AddRow(htmlTable, label, textBox, validator);
                }
            }
        }

        private static CompareValidator GetDecimalValidator(Control controlToValidate, string domain, string cssClass)
        {
            using (CompareValidator validator = new CompareValidator())
            {
                validator.ID = controlToValidate.ID + "DecimalValidator";
                validator.ErrorMessage = @"<br/>" + Titles.OnlyNumbersAllowed;
                validator.CssClass = cssClass;
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;
                validator.Type = ValidationDataType.Currency;

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