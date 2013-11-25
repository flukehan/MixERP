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

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    public static class ScrudCheckBoxList
    {
        public static void AddCheckBoxList(HtmlTable htmlTable, string columnName, bool isNullable, string keys, string values, string selectedValues)
        {
            CheckBoxList checkBoxList = GetCheckBoxList(columnName + "_radiobuttonlist", keys, values, selectedValues);
            string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", columnName);

            if (!isNullable)
            {
                RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(checkBoxList);
                ScrudFactoryHelper.AddRow(htmlTable, label + Resources.ScrudResource.RequiredFieldIndicator, checkBoxList, required);
                return;
            }

            ScrudFactoryHelper.AddRow(htmlTable, label, checkBoxList);
        }

        private static CheckBoxList GetCheckBoxList(string id, string keys, string values, string selectedValues)
        {
            using (CheckBoxList list = new CheckBoxList())
            {
                list.ID = id;
                list.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                list.RepeatDirection = RepeatDirection.Horizontal;
                Helper.AddListItems(list, keys, values, selectedValues);
                return list;
            }
        }
    }
}
