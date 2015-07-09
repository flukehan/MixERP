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

using MixERP.Net.Common;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void CreateHiddenFields(Control container)
        {
            this.selectedValuesHidden = new HiddenField();
            this.selectedValuesHidden.ID = "SelectedValuesHidden";
            container.Controls.Add(this.selectedValuesHidden);
        }

        private Collection<long> GetSelectedValues()
        {
            string selectedValues = this.selectedValuesHidden.Value;

            //Check if something was selected.
            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<long>();
            }

            //Create a collection object to store the IDs.
            Collection<long> values = new Collection<long>();

            //Iterate through each value in the selected values
            //and determine if each value is a number.
            foreach (string value in selectedValues.Split(','))
            {
                //Parse the value to integer.
                int val = Conversion.TryCastInteger(value);

                if (val > 0)
                {
                    values.Add(val);
                }
            }

            return values;
        }
    }
}