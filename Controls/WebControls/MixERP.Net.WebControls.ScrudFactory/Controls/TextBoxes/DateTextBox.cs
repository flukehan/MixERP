/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public partial class ScrudDateTextBox
    {
        public static void AddDateTextBox(HtmlTable t, string columnName, string defaultValue, bool isNullable, int maxLength)
        {
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", columnName);

            TextBox textBox = ScrudTextBox.GetTextBox(columnName + "_textbox", maxLength);
            textBox.CssClass = "date";

            CompareValidator validator = GetDateValidator(textBox);
            AjaxControlToolkit.CalendarExtender extender = new AjaxControlToolkit.CalendarExtender();

            textBox.Width = 70;
            extender.ID = textBox.ID + "_calendar_extender";
            extender.TargetControlID = textBox.ID;
            extender.PopupButtonID = textBox.ID;

            if(!string.IsNullOrWhiteSpace(defaultValue))
            {
                textBox.Text = MixERP.Net.Common.Conversion.TryCastDate(defaultValue).ToShortDateString();
            }


            if(!isNullable)
            {
                RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox);
                ScrudFactoryHelper.AddRow(t, label + Resources.ScrudResource.RequiredFieldIndicator, textBox, validator, required, extender);
                return;
            }

            ScrudFactoryHelper.AddRow(t, label, textBox, validator, extender);
        }

        private static TextBox GetDateTextBox(string id, int maxLength)
        {
            TextBox textBox = ScrudTextBox.GetTextBox(id, maxLength);
            textBox.Attributes["type"] = "date";
            return textBox;
        }

        private static CompareValidator GetDateValidator(Control controlToValidate)
        {
            CompareValidator validator = new CompareValidator();
            validator.ID = controlToValidate.ID + "DateValidator";
            validator.ErrorMessage = "<br/>" + Resources.ScrudResource.InvalidDate;
            validator.CssClass = "form-error";
            validator.ControlToValidate = controlToValidate.ID;
            validator.EnableClientScript = true;
            validator.SetFocusOnError = true;
            validator.Display = ValidatorDisplay.Dynamic;
            validator.Type = ValidationDataType.Date;
            validator.Operator = ValidationCompareOperator.GreaterThan;
            validator.ValueToCompare = "1-1-1900";

            return validator;
        }

    }
}
