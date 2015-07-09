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

using MixERP.Net.Common;
using MixERP.Net.Common.jQueryHelper;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    internal static class ScrudDateTextBox
    {
        public static string GetLocalizedDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            DateTime date = Conversion.TryCastDate(value);

            if (date.Date == date)
            {
                return date.ToString("d");
            }

            return date.ToString("f");
        }

        internal static void AddDateTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable, string validatorCssClass, bool disabled)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

            string id = columnName + "_textbox";

            using (TextBox textBox = ScrudTextBox.GetTextBox(id, 100))
            {
                jQueryUI.AddjQueryUIDatePicker(null, id, null, null);

                string label = ScrudLocalizationHelper.GetResourceString(resourceClassName, columnName);

                textBox.Text = GetLocalizedDate(defaultValue);
                textBox.ReadOnly = disabled;
                textBox.CssClass = "date";

                using (CompareValidator dateValidator = GetDateValidator(textBox, validatorCssClass))
                {
                    if (!isNullable)
                    {
                        using (RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox, validatorCssClass))
                        {
                            ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, textBox, dateValidator, required);
                            return;
                        }
                    }

                    ScrudFactoryHelper.AddRow(htmlTable, label, textBox, dateValidator);
                }
            }
        }

        private static CompareValidator GetDateValidator(Control controlToValidate, string cssClass)
        {
            if (controlToValidate == null)
            {
                return null;
            }

            using (CompareValidator validator = new CompareValidator())
            {
                validator.ID = controlToValidate.ID + "CompareValidator";
                validator.ErrorMessage = @"<br/>" + Titles.InvalidDate;
                validator.CssClass = cssClass;
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;
                validator.Type = ValidationDataType.Date;
                validator.Operator = ValidationCompareOperator.DataTypeCheck;

                return validator;
            }
        }
    }
}