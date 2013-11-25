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

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    public static class Helper
    {
        public static void AddListItems(ListControl control, string keys, string values, string selectedValues)
        {
            if (control == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(keys))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(values))
            {
                return;
            }

            string[] key = keys.Split(',');
            string[] value = values.Split(',');

            if (key.Count() != value.Count())
            {
                throw new InvalidOperationException("Key or value count mismatch. Cannot add items to " + control.ID);
            }

            for (int i = 0; i < key.Length; i++)
            {
                ListItem item = new ListItem(key[i].Trim(), value[i].Trim());
                control.Items.Add(item);
            }

            foreach (ListItem item in control.Items)
            {
                //Checkbox list allows multiple selection.
                if (control is CheckBoxList)
                {
                    if (!string.IsNullOrWhiteSpace(selectedValues))
                    {
                        foreach (string selectedValue in selectedValues.Split(','))
                        {
                            if (item.Value.Trim().Equals(selectedValue.Trim()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(selectedValues))
                    {
                        if (item.Value.Trim().Equals(selectedValues.Split(',').Last().Trim()))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
