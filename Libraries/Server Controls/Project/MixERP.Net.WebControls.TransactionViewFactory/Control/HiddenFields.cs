using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private HiddenField selectedValuesHidden;

        private void AddHiddenFields(Control container)
        {
            selectedValuesHidden = new HiddenField();
            selectedValuesHidden.ID = "SelectedValuesHidden";
            container.Controls.Add(selectedValuesHidden);
        }
    }
}