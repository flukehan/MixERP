/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public static class ScrudDateTextBox
    {
        public static MixERP.Net.WebControls.Common.DateTextBox GetDateTextBox(string id, bool required)
        {
            using (DateTextBox textBox = new DateTextBox())
            {
                textBox.ID = id;
                textBox.Required = required;
                textBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                textBox.Width = 100;
                return textBox;
            }
        }

        public static void AddDateTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable)
        {
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString(resourceClassName, columnName);

            MixERP.Net.WebControls.Common.DateTextBox textBox = GetDateTextBox(columnName + "_textbox", !isNullable);

            textBox.CssClass = "date";

            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                textBox.Text = MixERP.Net.Common.Conversion.TryCastDate(defaultValue).ToShortDateString();
            }


            if (!isNullable)
            {
                ScrudFactoryHelper.AddRow(htmlTable, label + Resources.ScrudResource.RequiredFieldIndicator, textBox);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, textBox);
        }

    }
}
