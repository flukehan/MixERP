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
    public static class ScrudRadioButtonList
    {
        public static void AddRadioButtonList(HtmlTable htmlTable, string resourceClassName, string columnName, bool isNullable, string keys, string values, string selectedValue)
        {
            if (htmlTable == null)
            {
                return;
            }

            using (var radioButtonList = GetRadioButtonList(columnName + "_radiobuttonlist", keys, values, selectedValue))
            {
                var label = LocalizationHelper.GetResourceString(resourceClassName, columnName);

                if (!isNullable)
                {
                    using (var required = ScrudFactoryHelper.GetRequiredFieldValidator(radioButtonList))
                    {
                        ScrudFactoryHelper.AddRow(htmlTable, label + ScrudResource.RequiredFieldIndicator, radioButtonList, required);
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

            using (var list = new RadioButtonList())
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
