using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private void AddJavascript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.TransactionViewFactory.TransactionViewFactory.js", "TransactionView", typeof(TransactionView));
        }
    }
}