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
    public partial class ScrudRadioButtonList
    {
        public static void AddRadioButtonList(HtmlTable t, string columnName, bool isNullable, string keys, string values, string selectedValue)
        {
            if (t == null)
            {
                return;
            }

            using (RadioButtonList radioButtonList = GetRadioButtonList(columnName + "_radiobuttonlist", keys, values, selectedValue))
            {
                string label = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", columnName);

                if (!isNullable)
                {
                    using (RequiredFieldValidator required = ScrudFactoryHelper.GetRequiredFieldValidator(radioButtonList))
                    {
                        ScrudFactoryHelper.AddRow(t, label + Resources.ScrudResource.RequiredFieldIndicator, radioButtonList, required);
                        return;
                    }
                }

                ScrudFactoryHelper.AddRow(t, label, radioButtonList);
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
                list.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                list.RepeatDirection = RepeatDirection.Horizontal;
                Helper.AddListItems(list, keys, values, selectedValues);
                return list;
            }
        }
    }
}
