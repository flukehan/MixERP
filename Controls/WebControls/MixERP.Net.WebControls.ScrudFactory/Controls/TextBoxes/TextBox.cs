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
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public static class ScrudTextBox
    {
        public static void AddTextBox(HtmlTable htmlTable, string resourceClassName, string columnName,
            string defaultValue, bool isNullable, int maxLength, string errorCssClass)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

            using (var textBox = GetTextBox(columnName + "_textbox", maxLength))
            {
                var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

                textBox.Text = defaultValue;

                if (!isNullable)
                {
                    var required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox, errorCssClass);
                    ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, textBox, required);
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

            using (var textBox = new TextBox())
            {
                textBox.ID = id;

                if (maxLength > 0)
                {
                    textBox.MaxLength = maxLength;
                }

                textBox.ClientIDMode = ClientIDMode.Static;

                return textBox;
            }
        }
    }
}