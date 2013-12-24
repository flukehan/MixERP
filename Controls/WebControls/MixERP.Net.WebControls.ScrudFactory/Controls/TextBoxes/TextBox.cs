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
    public static class ScrudTextBox
    {
        public static void AddTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable, int maxLength)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

            using (TextBox textBox = GetTextBox(columnName + "_textbox", maxLength))
            {
                string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString(resourceClassName, columnName);

                textBox.Text = defaultValue;

                if (!isNullable)
                {
                    RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox);
                    ScrudFactoryHelper.AddRow(htmlTable, label + Resources.ScrudResource.RequiredFieldIndicator, textBox, required);
                    return;
                }

                ScrudFactoryHelper.AddRow(htmlTable, label, textBox);
            }
        }

        public static TextBox GetTextBox(string id, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            using (TextBox textBox = new TextBox())
            {
                textBox.ID = id;

                if (maxLength > 0)
                {
                    textBox.MaxLength = maxLength;
                }

                textBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                return textBox;
            }
        }

    }
}
