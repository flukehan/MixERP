using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        private void AddJavaScript()
        {
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.TransactionViewFactory.TransactionViewFactory.js", "transaction_view", typeof(TransactionView));
        }
    }
}