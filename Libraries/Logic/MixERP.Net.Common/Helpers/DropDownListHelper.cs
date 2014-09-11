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