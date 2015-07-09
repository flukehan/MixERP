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
using System;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls.ListControls
{
    internal static class Helper
    {
        internal static void AddListItems(ListControl control, string keys, string values, string selectedValues)
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

            char separator = ",".ToString(Thread.CurrentThread.CurrentCulture).ToCharArray()[0];

            string[] key = keys.Split(separator);
            string[] value = values.Split(',');

            if (key.Count() != value.Count())
            {
                throw new InvalidOperationException(Errors.KeyValueMismatch);
            }

            for (int i = 0; i < key.Length; i++)
            {
                ListItem item = new ListItem(key[i].Trim(), value[i].Trim());
                control.Items.Add(item);
            }

            foreach (ListItem item in control.Items)
            {
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