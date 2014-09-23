using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class DirectSales : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TransactionCheckList1.ViewReportButtonText = Resources.Titles.ViewThisInvoice;
            TransactionCheckList1.EmailReportButtonText = Resources.Titles.EmailThisInvoice;
            TransactionCheckList1.CustomerReportButtonText = Resources.Titles.ViewCustomerCopy;

            base.OnControlLoad(sender, e);
        }
    }
}