/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Web.UI.HtmlControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public static class ScrudDateTextBox
    {
        public static DateTextBox GetDateTextBox(string id, bool required)
        {
            using (var textBox = new DateTextBox())
            {
                textBox.ID = id;
                textBox.Required = required;
                textBox.ClientIDMode = ClientIDMode.Static;
                textBox.Width = 100;
                return textBox;
            }
        }

        public static void AddDateTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable)
        {
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

            var textBox = GetDateTextBox(columnName + "_textbox", !isNullable);

            textBox.CssClass = "date";

            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                textBox.Text = Conversion.TryCastDate(defaultValue).ToShortDateString();
            }


            if (!isNullable)
            {
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, textBox);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, textBox);
        }

    }
}
