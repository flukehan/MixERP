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

        public static MixERP.Net.WebControls.Common.DateTextBox GetDateTextBox(string id, bool required)
        {
            MixERP.Net.WebControls.Common.DateTextBox textBox = new MixERP.Net.WebControls.Common.DateTextBox();
            textBox.ID = id;
            textBox.Required = required;
            textBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            textBox.Width = 100;
            return textBox;
        }

        public static void AddDateTextBox(HtmlTable t, string columnName, string defaultValue, bool isNullable)
        {
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", columnName);

            MixERP.Net.WebControls.Common.DateTextBox textBox = GetDateTextBox(columnName + "_textbox", !isNullable);

            textBox.CssClass = "date";

            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                textBox.Text = MixERP.Net.Common.Conversion.TryCastDate(defaultValue).ToShortDateString();
            }


            if (!isNullable)
            {
                ScrudFactoryHelper.AddRow(t, label + Resources.ScrudResource.RequiredFieldIndicator, textBox);
                return;
            }

            ScrudFactoryHelper.AddRow(t, label, textBox);
        }

        private static TextBox GetDateTextBox(string id, int maxLength)
        {
            TextBox textBox = ScrudTextBox.GetTextBox(id, maxLength);
            textBox.Attributes["type"] = "date";
            return textBox;
        }
    }
}
