using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Sales.Confirmation
{
    public partial class Delivery : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TransactionCheckList1.ViewReportButtonText = Resources.Titles.ViewThisDelivery;
            TransactionCheckList1.EmailReportButtonText = Resources.Titles.EmailThisDelivery;
            TransactionCheckList1.CustomerReportButtonText = Resources.Titles.ViewCustomerCopy;
            base.OnControlLoad(sender, e);
        }
    }
}