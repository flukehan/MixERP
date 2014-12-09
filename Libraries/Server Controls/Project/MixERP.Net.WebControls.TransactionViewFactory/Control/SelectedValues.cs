using MixERP.Net.Common;
using System.Collections.ObjectModel;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private Collection<int> GetSelectedValues()
        {
            string selectedValues = this.selectedValuesHidden.Value;

            //Check if something was selected.
            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<int>();
            }

            //Create a collection object to store the IDs.
            Collection<int> values = new Collection<int>();

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