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
    internal static class ScrudTextBox
    {
        internal static void AddTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string dataType, string defaultValue, bool isNullable, int maxLength, string errorCssClass, bool disabled)
        {
            if (htmlTable == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return;
            }

            bool isPasswordField = columnName.ToUpperInvariant().Contains("PASSWORD");

            if (isPasswordField && disabled)
            {
                defaultValue = "fake-password";
            }

            using (TextBox textBox = GetTextBox(columnName + "_textbox", maxLength))
            {
                string label = ScrudLocalizationHelper.GetResourceString(resourceClassName, columnName);


                if (dataType.ToUpperInvariant().Equals("COLOR"))
                {
                    textBox.CssClass = "color";
                }

                if (isPasswordField)
                {
                    textBox.Attributes.Add("type", "password");
                }

                if (disabled && !isPasswordField)
                {
                    textBox.ReadOnly = true;
                }

                textBox.Text = defaultValue;


                if (!isNullable)
                {
                    using (RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(textBox, errorCssClass))
                    {
                        ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, textBox, required);
                        return;
                    }
                }

                ScrudFactoryHelper.AddRow(htmlTable, label, textBox);
            }
        }

        internal static TextBox GetTextBox(string id, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            using (TextBox textBox = new TextBox())
            {
                textBox.ID = id;
                textBox.ClientIDMode = ClientIDMode.Static;

                if (maxLength > 0)
                {
                    textBox.MaxLength = maxLength;
                }


                return textBox;
            }
        }
    }
}