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

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    internal static class ScrudRadioButtonList
    {
        internal static void AddRadioButtonList(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable, string keys, string values, string selectedValue, string errorCssClass, bool disabled)
        {
            if (htmlTable == null)
            {
                return;
            }

            using (RadioButtonList radioButtonList = GetRadioButtonList(columnName + "_radiobuttonlist", keys, values, selectedValue))
            {
                string label = ScrudLocalizationHelper.GetResourceString(resourceClassName, columnName);

                radioButtonList.Enabled = !disabled;

                if (!isNullable)
                {
                    using (RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(radioButtonList, errorCssClass))
                    {
                        ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, radioButtonList, required);
                        return;
                    }
                }

                ScrudFactoryHelper.AddRow(htmlTable, label, radioButtonList);
            }
        }

        private static RadioButtonList GetRadioButtonList(string id, string keys, string values, string selectedValues)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            using (RadioButtonList list = new RadioButtonList())
            {
                list.ID = id;
                list.ClientIDMode = ClientIDMode.Static;
                list.RepeatDirection = RepeatDirection.Horizontal;

                Helper.AddListItems(list, keys, values, selectedValues);
                
                return list;
            }
        }
    }
}