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
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.TextBoxes
{
    public static class ScrudDateTextBox
    {
        public static void AddDateTextBox(HtmlTable htmlTable, string resourceClassName, string columnName, string defaultValue, bool isNullable, string cssClass)
        {
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

            var textBox = GetDateTextBox(columnName + "_textbox", !isNullable);

            textBox.CssClass = cssClass;

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
    }
}