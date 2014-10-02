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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MixERP.Net.Common.Helpers
{
    public static class DropDownListHelper
    {
        /// <summary>
        /// Selects the item in the list control that contains the specified value, if it exists.
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="selectedValue">The value of the item in the list control to select</param>
        /// <returns>Returns true if the value exists in the list control, false otherwise</returns>
        public static bool SetSelectedValue(ListControl dropDownList, String selectedValue)
        {
            if (dropDownList != null)
            {
                dropDownList.ClearSelection();

                ListItem selectedListItem = dropDownList.Items.FindByValue(selectedValue);

                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                    return true;
                }
            }

            return false;
        }
    }
}