using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public static class ScrudDecimalTextBox
    {
        public static void AddDecimalTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable, int maxLength, string domain)
        {
            var textBox = ScrudTextBox.GetTextBox(columnName + "_textbox", maxLength);
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

            var validator = GetDecimalValidator(textBox, domain);
            textBox.Text = defaultValue;

            if(!isNullable)
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox);
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, textBox, validator, required);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, textBox, validator);
        }

        private static CompareValidator GetDecimalValidator(Control controlToValidate, string domain)
        {
            using (var validator = new CompareValidator())
            {
                validator.ID = controlToValidate.ID + "DecimalValidator";
                validator.ErrorMessage = @"<br/>" + ScrudResource.OnlyNumbersAllowed;
                validator.CssClass = "form-error";
                validator.ControlToValidate = controlToValidate.ID;
                validator.EnableClientScript = true;
                validator.SetFocusOnError = true;
                validator.Display = ValidatorDisplay.Dynamic;
                validator.Type = ValidationDataType.Double;

                //MixERP strict data type
                if (domain.Contains("strict"))
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
