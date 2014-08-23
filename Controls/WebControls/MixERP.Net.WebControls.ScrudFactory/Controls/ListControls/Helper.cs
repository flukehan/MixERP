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
using System;
using System.Linq;
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

            var key = keys.Split(',');
            var value = values.Split(',');

            if (key.Count() != value.Count())
            {
                throw new InvalidOperationException("Key or value count mismatch. Cannot add items to " + control.ID);
            }

            for (var i = 0; i < key.Length; i++)
            {
                var item = new ListItem(key[i].Trim(), value[i].Trim());
                control.Items.Add(item);
            }

            foreach (ListItem item in control.Items)
            {
                //Checkbox list allows multiple selection.
                if (control is CheckBoxList)
                {
                    if (!string.IsNullOrWhiteSpace(selectedValues))
                    {
                        foreach (var selectedValue in selectedValues.Split(','))
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