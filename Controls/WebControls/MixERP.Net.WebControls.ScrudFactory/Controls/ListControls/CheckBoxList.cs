/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    public static class ScrudCheckBoxList
    {
        public static void AddCheckBoxList(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable, string keys, string values, string selectedValues)
        {
            var checkBoxList = GetCheckBoxList(columnName + "_radiobuttonlist", keys, values, selectedValues);
            var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

            if (!isNullable)
            {
                var required = ScrudFactoryHelper.GetRequiredFieldValidator(checkBoxList);
                ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, checkBoxList, required);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, checkBoxList);
        }

        private static CheckBoxList GetCheckBoxList(string id, string keys, string values, string selectedValues)
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
