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

using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    internal static class ScrudCheckBoxList
    {
        internal static void AddCheckBoxList(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable, string keys, string values, string selectedValues, string errorCssClass, Assembly assembly)
        {
            var checkBoxList = GetCheckBoxList(columnName + "_radiobuttonlist", keys, values, selectedValues);
            var label = ScrudLocalizationHelper.GetResourceString(assembly, resourceClassName, columnName);

            if (!isNullable)
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(checkBoxList, errorCssClass);
                ScrudFactoryHelper.AddRow(htmlTable, label + Titles.RequiredFieldIndicator, checkBoxList,
                    required);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, checkBoxList);
        }

        internal static CheckBoxList GetCheckBoxList(string id, string keys, string values, string selectedValues)
        {
            using (var list = new CheckBoxList())
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