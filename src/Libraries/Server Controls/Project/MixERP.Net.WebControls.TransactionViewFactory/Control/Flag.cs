using MixERP.Net.WebControls.Flag;
using System.Web.UI;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private void AddFlagControl(Control container)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";
                flag.Updated += this.Flag_Updated;
                flag.Catalog = this.Catalog;

                container.Controls.Add(flag);
            }
        }
    }
}